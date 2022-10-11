using System.Data;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 외곽선 실수 검사하는 폼 클래스
    /// </summary>
    public partial class OutlineFrameForm : Form
    {
        #region 필드
        private int _rowCount = 7;
        private readonly int _lastOrder;
        private int _order;
        private int _selectedIndex = 1;
        private List<float[]> _allDatas;
        private readonly string[] _columnName;
        private readonly double[] _limits = { 0.01 * 0.33, 0.01 * 0.67, 0.01, 0.01 * 2, 0.01 * 3 };
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public OutlineFrameForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            _order = lastOrder;
            _columnName = new string[]
            {
                "외각1", "외각2", "외각3", "외각4", "외각5", "외각6", "외각합",
                "선분1", "선분2", "선분3", "선분4", "선분5", "선분6", "선분합",
                "내각1", "내각2", "내각3", "내각4", "내각5", "면적", "평균"
            };
        }

        private async void OutlineFrameForm_Load(object sender, EventArgs e)
        {
            _allDatas = await Task.Run(GetAllDatas);

            RowCountComboBox.SelectedIndex = 1;
            OrderNumericUpDown.Value = _lastOrder;
            OrderNumericUpDown.Maximum =  _lastOrder;

            listView1.Columns.Add("회차", 50, HorizontalAlignment.Center);
            _columnName.ToList().ForEach(x => listView1.Columns.Add(x, 50, HorizontalAlignment.Center));
        }

        private void RowCountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RowCountComboBox.SelectedIndex > -1)
            {
                int sel = RowCountComboBox.SelectedIndex;
                _selectedIndex = sel;
                string s = RowCountComboBox.SelectedItem.ToString();
                _rowCount = int.Parse(s);
                PresentListView(sel);
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int colCount = (_rowCount == 7) ? 7 : (_rowCount == 5) ? 9 : 5;
            var dang = Utility.DangbeonOfOrder(_order);
            var size = new Size(36, 36);
            int w = size.Width;
            int h = size.Height;    
            int num = 0;
            var points = new List<Point>();

            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    num++;
                    

                    if (num <= 45)
                    {
                        var rect = new Rectangle(j * w, i * h, w, h);
                        using var greyPen = new Pen(Color.Gray, 0.5F);
                        g.DrawRectangle(greyPen, rect);

                        //숫자 그리기
                        using var font = new Font("맑은고딕", 12, FontStyle.Regular, GraphicsUnit.Point);
                        StringFormat stringFormat = new()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        g.DrawString(num.ToString(), font, Brushes.Black, rect, stringFormat);

                        if (dang.Contains(num))
                        {
                            var (xIndex, yIndex) = LuckyLibrary.PositionOfData(num, _rowCount);

                            if (xIndex == j && yIndex == i)
                            {
                                //원그리기
                                using var redPen = new Pen(Color.Green, 1);
                                var circle = new Rectangle((xIndex * w) + 14, (yIndex * h) + 14, w - 28, h - 28);
                                //g.DrawEllipse(redPen, circle);
                                g.FillEllipse(Brushes.Green, circle);
                                var point = new Point((xIndex * w) + (w / 2), (yIndex * h) + (h / 2));
                                points.Add(point);
                            }
                        }
                    }
                }
            }

            //당번 연결선 그리기
            using (var greenPen = new Pen(Color.Red))
            {
                g.DrawLines(greenPen, points.ToArray());
            }

            var datas = LuckyLibrary.HorizontalFlowDatas(_rowCount);
            var idxpoint = dang.Select(x => Convexhull.IndexToPoint(x, datas)).ToList();
            var hull = Convexhull.GetConvexHull(idxpoint);
            hull.Add(hull.First());
            var hullpts = hull.Select(g => new Point(g.Y * w + w / 2, g.X * h + h / 2));

            //외곽선 그리기
            using (var polPen = new Pen(Color.Blue))
            {
                g.DrawPolygon(polPen, hullpts.ToArray());
            }

            //외곽선 채우기
            using var hullbrush = new SolidBrush(Color.FromArgb(100, Color.Cyan));
            g.FillPolygon(hullbrush, hullpts.ToArray());
        }

        private void OrderNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _order = (int)OrderNumericUpDown.Value;
            PresentListView(_selectedIndex);
            pictureBox1.Invalidate();
        }

        private void MinGapButton_Click(object sender, EventArgs e)
        {

        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            ExecuteButton.Text = "작업중...";
            ExecuteButton.Enabled = false;
            var alldatas = _allDatas.Select(x => x.Skip(1).ToArray()).ToList();
            var lastdata = alldatas.Last();
            var document1 = new List<string>();

            var task = Task.Run(() =>
            {
                //1 - 3 조합
                for (int i = 1; i <= 3; i++)
                {
                    if (i == 1)
                    {
                        for (int j = 0; j < lastdata.Length; j++)
                        {
                            var datas = alldatas.Select(x => x[j]);
                            var (lowValue, highValue, exceptList) = FindMinMaxExceptList(datas);
                            int chk = 1;

                            if (!exceptList.Any() && (int)datas.Min() == lowValue && (int)datas.Max() == highValue)
                            {
                                chk = 0;
                            }

                            string s = j + "/" + lowValue + "/" + highValue + "/" + string.Join(",", exceptList) + "/" + chk;
                            document1.Add(s);
                        }
                    }
                    else
                    {
                        var johaps = Utility.GetCombinations(Enumerable.Range(0, lastdata.Length), i)
                                            .Select(x => x.ToArray()).ToList();
                        foreach (int[] idx in johaps)
                        {
                            var datas = alldatas.Select(x => idx.Select(y => x[y]).ToArray()).ToList();
                        }
                    }
                }
            });

            await task;
            ResultTextBox.Text = string.Join(Environment.NewLine, document1);

            ExecuteButton.Text = "검사하기";
            ExecuteButton.Enabled = true;
        }




        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        /// <param name="index">콤보박스 인덱스</param>
        private void PresentListView(int index)
        {
            listView1.Items.Clear();
            int start = _order - 100;

            var datas = _allDatas.Where(x => x[0] > start && x[0] <= _order).Select(g => g.ToArray());
            var takes = new List<float[]>();

            foreach (float[] data in datas)
            {
                switch (index)
                {
                    case 0:
                        var temp = data.Take(21).ToList();
                        temp.Add(data.Last());
                        takes.Add(temp.ToArray());
                        break;
                    case 1:
                        var temp1 = data.Skip(20).Take(21).ToList();
                        temp1.Insert(0, data[0]);
                        temp1.Add(data.Last());
                        takes.Add(temp1.ToArray());
                        break;
                    default:
                        var temp2 = data.Skip(41).ToList();
                        temp2.Insert(0, data[0]);
                        temp2.Add(data.Last());
                        takes.Add(temp2.ToArray());
                        break;
                }
            }

            foreach (var data in takes)
            {
                var lvi = new ListViewItem(data[0].ToString());

                for (int i = 1; i < data.Length; i++)
                {
                    lvi.SubItems.Add(data[i].ToString());
                }
                listView1.Items.Add(lvi);
            }

            listView1.TopItem = listView1.Items[^1];
        }

        /// <summary>
        /// 실수 전체 데이터
        /// </summary>
        /// <returns>실수배열 리스트</returns>
        private List<float[]> GetAllDatas()
        {
            var lists = new List<float[]>();
            try
            {
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "SELECT * FROM SingleTbl";
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ord = reader.GetInt32(0);
                    float[] array = new float[reader.FieldCount];
                    array[0] = ord;
                    for (int i = 1; i < reader.FieldCount; i++)
                    {
                        float f = reader.GetFloat(i);
                        array[i] = f;
                    }

                    lists.Add(array);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lists;
        }

        //private void GetExcept(IEnumerable<float> ascCollection)
        //{
        //    float lastflt = ascCollection.Last();
        //    int lastnum = (int)lastflt;
        //    int min = (int)ascCollection.Min();
        //    int max = (int)ascCollection.Max();
        //    int chai = max - min;

        //    if (chai > 10)
        //    {
        //        for (int i = 0; i < chai / 2; i++)
        //        {
        //            if (i == 0)
        //            {
        //                var tpl = RealMaxNextList(ascCollection);

        //            }
        //            else
        //            {
        //                //끝출에서 +1 이동하면서 후방연속 연속최대 검사 
        //                int a = lastnum + i > max ? max : lastnum + i;
        //                int b = lastnum - i < min ? min : lastnum - i;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        var tpl = RealMaxCount(ascCollection);
        //    }
        //}

        /// <summary>
        /// 임시로 하한, 상한, 최소간격, 고유요소의 갯수 파악
        /// </summary>
        /// <returns></returns>
        private void FindGapMinimum()
        {
            ResultTextBox.Text = "";
            var datas = _allDatas.Select(x => x.Skip(1).ToArray()).ToList();
            var seldatas = new List<float[]>();

            seldatas = _selectedIndex switch
            {
                0 => datas.Select(x => x.Take(20).ToArray()).ToList(),
                1 => datas.Select(x => x.Skip(20).Take(20).ToArray()).ToList(),
                _ => datas.Select(x => x.Skip(40).ToArray()).ToList(),
            };

            var rst = new Dictionary<string, (float, float, float, int)>();   //최소값, 최대값, 차이값최소, 차이갯수

            for (int i = 0; i < seldatas[0].Length; i++)
            {
                var data = seldatas.Select(x => x[i]);
                var disct = data.Distinct().OrderBy(x => x);
                var zip = disct.Zip(disct.Skip(1), (a, b) => b - a).ToList();
                float min = data.Min();
                float max = data.Max();
                float f1 = zip.Min();
                int cnt = zip.Count;
                rst.Add(_columnName[i], (min, max, f1, cnt));
            }

            string s = string.Empty;

            foreach (var key in rst.Keys)
            {
                var val = rst[key];
                string s1 = string.Join(", ", val);
                s += key + ": " + s1 + "\r\n";
            }

            ResultTextBox.Text = s;
        }

        /// <summary>
        /// 전체데이터의 하한, 상한, 제외리스트
        /// </summary>
        /// <param name="ascCollection">오름차순 데이터</param>
        /// <returns></returns>
        public (int lowValue, int highValue, List<int> exceptList) FindMinMaxExceptList(IEnumerable<float> ascCollection)
        {
            int distcount = ascCollection.Distinct().Count();
            int renum = (int)ascCollection.Max() - (int)ascCollection.Min() + 1;
            int gap = renum switch
            {
                <= 10 => 1,
                <= 50 => 2,
                <= 100 => 3,
                <= 150 => 4,
                <= 200 => 5,
                <= 250 => 6,
                _ => 7
            };

            var (low, high, exceptList) = NowChcekData(ascCollection, gap);

            //다음 출수 검사
            var nexts = NextList(ascCollection);

            //범위내에 있는것
            var exps = nexts.Where(x => x > low && x < high).Except(exceptList);

            if (exps is not null && exps.Any())
            {
                if (exps.Count() >= _limits[3])
                {
                    int last = exps.Last();
                    var (realCount, maxCount) = NextReal.RealMaxCount(exps);

                    if (realCount >= maxCount)
                    {
                        exceptList.Add(last);
                    }
                }
            }

            var tpl = ChangeMinMaxExceptList(low, high, gap, exceptList);

            return tpl;
        }

        /// <summary>
        /// 현재출수 기준으로 하한, 상한, 제외리스트
        /// </summary>
        /// <param name="ascCollection"></param>
        /// <returns></returns>
        private (int low, int high, List<int> exceptList) NowChcekData(IEnumerable<float> ascCollection, int gap)
        {
            int distcount = ascCollection.Distinct().Count();
            int min = (int)ascCollection.Min();
            int mok = (int)ascCollection.Max();
            float nameozi = ascCollection.Max() - mok;
            int max = (nameozi == 0) ? mok : mok + 1;
            int lastNumber = (int)ascCollection.Last();
            var pairs = new List<(int key, int count)>();
            //최대값 미만으로 루프 (현재값 + 갭을 더하므로)
            for (int i = min; i < max; i += gap)
            {
                int a = i + gap;
                int n = ascCollection.Count(x => x >= i && x < a);
                pairs.Add((i, n));
            }

            int datacnt = ascCollection.Count();
            int limit = distcount switch
            {
                <= 10 => Convert.ToInt32(_limits[2] * datacnt),
                <= 100 => Convert.ToInt32(_limits[1] * datacnt),
                _ => Convert.ToInt32(_limits[0] * datacnt)
            };

            if (pairs.Count >= 3)
            {
                var chgpairs = GetAccumulatList(pairs, limit);
                min = chgpairs.Min(x => x.key);
                max = chgpairs.Max(x => x.key);
                var (sameCount, gaps) = GetGapList(ascCollection, gap);
                var zerokeys = chgpairs.Where(x => x.val == 0).Select(x => x.key).ToList();
                var (realCount, maxCount) = RealMaxCount(ascCollection, gap);
                if (realCount >= maxCount || (gaps.Count > datacnt * _limits[2] && gaps.Last() > gaps.Max()))
                {
                    zerokeys.Add(lastNumber);
                }

                var tpl = ChangeMinMaxExceptList(min, max, gap, zerokeys.Distinct().ToList());
                return tpl;
            }
            else
            {
                return (min, max, new List<int>());
            }
        }

        /// <summary>
        /// 최종출과 동일한 다음출 리스트
        /// </summary>
        /// <param name="ascCollection">오름차순 데이터</param>
        /// <returns></returns>
        public static IEnumerable<int> NextList(IEnumerable<float> ascCollection)
        {
            var list = ascCollection.ToList();
            int real = CountOfRealSame(ascCollection);
            int last = (int)ascCollection.Last();
            var remind = ascCollection.Take(ascCollection.Count() - real).ToList();

            var idxs = remind.Select((val, idx) => (val, idx)).Where(x => x.val >= last && x.val < last + 1)
                             .Select(x => x.idx).Where(x => x < remind.Count);

            var rst = new List<int>();

            if (idxs.Any())
            {
                foreach (int idx in idxs)
                {
                    int sameCount = 0;

                    for (int i = 0; i < real; i++)
                    {
                        int a = idx + i;
                        float f = list[a];

                        if (f >= last && f < last + 1)
                            sameCount++;
                        else
                            break;
                    }

                    if (sameCount == real)
                    {
                        int a = idx + real;
                        int n = (int)list[a];
                        rst.Add(n);
                    }
                }
            }

            return rst;
        }

        /// <summary>
        /// 후방연속, 연속최대 갯수 (연속최대가 -1:전부동일, 0:끝출외 무출)
        /// </summary>
        /// <param name="ascCollection">오름차순 데이터 배열</param>
        /// <returns>튜플(후방연속갯수, 연속최대갯수)</returns>
        public static (int realCount, int maxCount) RealMaxCount(IEnumerable<float> ascCollection, int gap)
        {
            float[] ascArray = ascCollection.ToArray();

            int real = 0;
            int last = (int)ascArray[^1];

            //후방연속
            for (int i = ascArray.Length - 1; i >= 0; i--)
            {
                if (ascArray[i] >= last && ascArray[i] < last + gap)
                    real++;
                else
                    break;
            }

            //연속최대
            float[] ascRemind = ascArray[..^real];
            int max = 0, dup = 0;

            foreach (int n in ascRemind.Select(v => (int)v))
            {
                if (n >= last && n < last + gap)
                {
                    dup++;
                }
                else
                {
                    if (dup > max)
                    {
                        max = dup;
                    }

                    dup = 0;
                }
            }

            if (dup > max)
            {
                max = dup;
            }

            return (real, max);
            
        }

        /// <summary>
        /// 후방연속, 연속최대 갯수, 다음출리스트 (연속최대가 -1:전부동일, 0:끝출외 무출)
        /// </summary>
        /// <param name="ascCollection">오름차순 데이터 배열</param>
        /// <returns>튜플(후방연속갯수, 연속최대갯수)</returns>
        public static (int realCount, int maxCount, List<int> nextlist) RealMaxNextList(IEnumerable<float> ascCollection)
        {
            if (!ascCollection?.Any() ?? false)
            {
                throw new Exception("배열에 요소가 없습니다.");
            }

            float[] ascArray = ascCollection.ToArray();

            if (DistinctList(ascArray).Count >= 2)
            {
                int real = 0;
                int last = (int)ascArray[^1];

                //후방연속
                for (int i = ascArray.Length - 1; i >= 0; i--)
                {
                    if (ascArray[i] >= last && ascArray[i] < last + 1)
                        real++;
                    else
                        break;
                }

                //연속최대
                float[] ascRemind = ascArray[..^real];
                int max = 0, dup = 0;
                var idxs = new List<int>();

                for (int i = 0; i < ascRemind.Length; i++)
                {
                    float n = ascRemind[i];

                    if (n >= last && n < last + 1)
                    {
                        dup++;
                        idxs.Add(i);
                    }
                    else
                    {
                        if (dup > max)
                        {
                            max = dup;
                        }

                        dup = 0;
                    }
                }

                if (dup > max)
                {
                    max = dup;
                }

                var rst = new List<int>();

                if (idxs.Any())
                {
                    foreach (int idx in idxs)
                    {
                        int sameCount = 0;

                        for (int i = 0; i < real; i++)
                        {
                            int a = idx + i;
                            float f = ascArray[a];

                            if (f >= last && f < last + 1)
                                sameCount++;
                            else
                                break;
                        }

                        if (sameCount == real)
                        {
                            int a = idx + real;
                            int n = (int)ascArray[a];
                            rst.Add(n);
                        }
                    }
                }

                return (real, max, rst);
            }
            else
            {
                return ((int)ascArray.Min(), (int)ascArray.Max(), new List<int>());
            }
        }

        /// <summary>
        /// 최종출과 동일한 후방연속갯수
        /// </summary>
        /// <param name="ascCollection">오름차순 전체데이터</param>
        /// <returns></returns>
        public static int CountOfRealSame(IEnumerable<float> ascCollection)
        {
            if (!ascCollection?.Any() ?? false)
            {
                throw new Exception("컬렉션에 요소가 없습니다.");
            }

            int last = (int)ascCollection.Last();
            int real = 0;

            foreach (int n in ascCollection.Reverse().Select(v => (int)v))
            {
                if (n >= last && n < last + 1)
                    real++;
                else
                    break;
            }

            return real;
        }

        /// <summary>
        /// 최종출의 동출수, 출현간격
        /// </summary>
        /// <param name="ascCollection">오름차순 전체데이터</param>
        /// <returns></returns>
        public static (int sameCount, List<int> gaps) GetGapList(IEnumerable<float> ascCollection, int gap)
        {
            int last = (int)ascCollection.Last();
            var idxs = ascCollection.Select((val, idx) => (val, idx)).Where(x => x.val >= last && x.val < last + gap).Select(x => x.idx).ToList();
            int cnt = idxs.Count;

            //최종출을 더함 (죄종출과 같은 인덱스를 찾기 때문에 최종출 + 1)
            idxs.Add(ascCollection.Count());

            var zip = idxs.Zip(idxs.Skip(1), (a, b) => b - a).ToList();

            //첫출을 삽입 (인덱스므로 +1)
            zip.Insert(0, idxs.First() + 1);

            return (cnt, zip);
        }

        /// <summary>
        /// 리스트에서 고유요소 리스트 반환
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        private static List<float> DistinctList(IEnumerable<float> lists)
        {
            return lists.Distinct().OrderBy(x => x).ToList();
        }

        /// <summary>
        /// 제외번호가 하한과 상한 연속부분을 제거한 하한, 상한, 제외리스트
        /// </summary>
        /// <param name="min">하한값</param>
        /// <param name="max">상한값</param>
        /// <param name="zerolists">제외리스트</param>
        /// <returns></returns>
        private static (int low, int high, List<int> exceptList) ChangeMinMaxExceptList(int min, int max, int gap, List<int> zerolists)
        {
            if (!zerolists?.Any() ?? false)
            {
                return (min, max, new List<int>());
            }
            else if (zerolists.Count == 1)
            {
                int n = zerolists.First();
                List<int> temp;

                if (min + gap == n)
                {
                    min = n;
                    temp = new List<int>();
                }
                else if (max - gap == n)
                {
                    max = n;
                    temp = new List<int>();
                }
                else
                {
                    temp = new List<int>(zerolists);
                }

                return (min, max, temp);
            }
            else
            {
                int lowplus = 0, hghplus = 0;

                //하단에서부터 검사
                for (int i = 0; i < zerolists.Count - 1; i += gap)
                {
                    int n = zerolists[i];

                    if (min + i == n)
                        lowplus++;
                    else
                        break;
                }

                //상단에서부터 검사
                for (int i = zerolists.Count - 1; i > 0; i -= gap)
                {
                    int n = zerolists[i];

                    if (max - i == n)
                        hghplus++;
                    else
                        break;
                }

                min += lowplus * gap;
                max -= hghplus * gap;

                //중복되것 삭제
                var exp = zerolists.Where(x => x > min && x < max).ToList();
                exp.Sort();

                return (min, max, exp);
            }
        }

        /// <summary>
        /// 하한과 상한의 누적한계값 이내의 튜플리스트
        /// </summary>
        /// <param name="lists">튜플리스트</param>
        /// <param name="limit">한계값</param>
        /// <returns></returns>
        private static List<(int key, int val)> GetAccumulatList(List<(int key, int count)> lists, int limit)
        {
            int lowsum = 0, highsum = 0;
            int low = 0, hgh = 1;

            //하한
            for (int i = 0; i < lists.Count; i++)
            {
                (int key, int count) tpl = lists[i];
                lowsum += tpl.count;

                if (lowsum >= limit)
                {
                    low = lists[i + 1].key;
                    break;
                }
            }

            //상한
            for (int i = lists.Count - 1; i > 0; i--)
            {
                (int key, int count) tpl = lists[i];
                highsum += tpl.count;

                if (highsum >= limit)
                {
                    hgh = lists[i - 1].key;
                    break;
                }
            }

            var rst = lists.Where(x => x.key >= low && x.key <= hgh).ToList();

            return rst;
        }
    }
}
