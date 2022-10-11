using System.Data;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    public partial class MarkovProcessForm : Form
    {
        #region 필드
        private int _jeoniValue = 3;
        private int _matrixLength = 30;
        private readonly string[] _columnNames;
        private Dictionary<string, List<(int order, int[] chulInt)>> _fixPairs;
        private Dictionary<int, int[]> _dangPairs;
        private Dictionary<string, List<int[]>> _numPairs;
        private readonly ListViewColumnSorter lvwColumnSorter;
        private readonly string[] _itemStrs = { "5 데이터 검사", "7 데이터 검사" , "9 데이터 검사" , "고정데이터 검사" , "당번데이터 검사" };
        #endregion

        public MarkovProcessForm()
        {
            InitializeComponent();

            _columnNames = new[]
            {
                "RH05", "RV05", "DH05", "DV05", "SH05", "SV05",
                "RH07", "RV07", "DH07", "DV07", "SH07", "SV07",
                "RH09", "RV09", "DH09", "DV09", "SH09", "SV09",
                "Beondae", "Slipsu", "Kkeutbeon"
            };

            ItemComboBox.Items.AddRange(_itemStrs);

            lvwColumnSorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = lvwColumnSorter;
        }

        private void MarkovProcessForm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("항 목", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("해당번호", 230, HorizontalAlignment.Left);
            listView1.Columns.Add("최종출", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("마코프확률", 386, HorizontalAlignment.Left);

            _fixPairs = GetAllFixDatas();
            _numPairs = GetFixNumbers();
            _dangPairs = GetDangbeon();
            ItemComboBox.SelectedIndex = 0;
        }

        private void ListView1_Click(object sender, EventArgs e)
        {
            int sel = ItemComboBox.SelectedIndex;

            if (sel > -1 && sel < 4)
            {
                var selectedItem = listView1.SelectedItems[0].SubItems[1].Text;
                NumberTextBox.Text = selectedItem;
            }
        }

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //열이름에 화살표 이미 있다면 지우기
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView1.Columns[i].Text = listView1.Columns[i].Text.Replace(" ▼", "");
                listView1.Columns[i].Text = listView1.Columns[i].Text.Replace(" ▲", "");
            }

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + " ▼";
                }
                else
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + " ▲";
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                listView1.Columns[e.Column].Text = listView1.Columns[e.Column].Text + " ▲";
            }

            // Perform the sort with these new sort options.
            listView1.Sort();
        }

        private async void ItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemComboBox.SelectedIndex > -1)
            {
                listView1.Items.Clear();
                NumberTextBox.Clear();
                int sel = ItemComboBox.SelectedIndex;
                List<string[]> datas;

                switch (sel)
                {
                    case 0:
                        string[] strs1 = { "RH05", "RV05", "DH05", "DV05", "SH05", "SV05" };
                        datas = CheckedFixData(strs1);
                        break;
                    case 1:
                        string[] strs3 = { "RH07", "RV07", "DH07", "DV07", "SH07", "SV07" };
                        datas = CheckedFixData(strs3);
                        break;
                    case 2:
                        string[] strs4 = { "RH09", "RV09", "DH09", "DV09", "SH09", "SV09" };
                        datas = CheckedFixData(strs4);
                        break;
                    case 3:
                        string[] strs5 = { "Beondae", "Slipsu", "Kkeutbeon" };
                        datas = CheckedFixData(strs5);
                        break;
                    default:
                        panel1.Enabled = false;
                        datas = await CheckedDangDataAsync();
                        panel1.Enabled = true;
                        break;
                }

                listView1.BeginUpdate();
                foreach (string[] data in datas)
                {
                    var lvi = new ListViewItem(data[0]) { UseItemStyleForSubItems = false };

                    for (int i = 1; i < data.Length; i++)
                    {
                        lvi.SubItems.Add(data[i]);

                        if (data[2].Equals("0"))
                        {
                            lvi.BackColor = Color.Cyan;
                        }
                    }

                    listView1.Items.Add(lvi);
                }
                listView1.EndUpdate();
            }
        }

        private void ChulNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _matrixLength = (int)ChulNumericUpDown.Value;
        }

        private void JeoniNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _jeoniValue = (int)JeoniNumericUpDown.Value;
        }




        //*********************  메서드  **************************



        /// <summary>
        /// 당번출수 마코프
        /// </summary>
        /// <returns></returns>
        private async Task<List<string[]>> CheckedDangDataAsync()
        {
            var lists = new List<string[]>();
            
            int[] lastInts = _dangPairs.Last().Value;
            var danglists = new List<double[]>();
            var chullists = new List<double[]>();
            var enumers = Enumerable.Range(1, 45);
            var task = Task.Run(() =>
            {
                for (int i = 1; i <= 45; i++)
                {
                    //번호가 나온 회차 찾기
                    var ords = _dangPairs.Where(x => x.Value.Contains(i)).Select(g => g.Key + 1)
                                            .Where(g => g <= _dangPairs.Keys.Max()).Select(g => _dangPairs[g]);

                    var chulskip = ords.Skip(ords.Count() - 15);
                    var dangskip = ords.Skip(ords.Count() - 30);
                    double sum = enumers.Select(num => chulskip.SelectMany(x => x).Count(g => g == num)).Sum();

                    var chularr = new double[45];
                    var dangarr = new double[45];
                    for (int j = 0; j < 45; j++)
                    {
                        int n = chulskip.SelectMany(x => x).Count(g => g == (j + 1));
                        chularr[j] = n / sum;

                        int v = dangskip.SelectMany(x => x).Count(g => g == (j + 1));
                        double d = dangskip.Count() * 6.0;
                        dangarr[j] = v / d;
                    }
                    chullists.Add(chularr);

                    if (lastInts.Contains(i))
                    {
                        danglists.Add(dangarr);
                    }
                }
            });

            await task;

            //단순 당번다음 검사 결과
            var simpairs = new Dictionary<int, double>();

            for (int i = 0; i < 45; i++)
            {
                double ea = danglists.Select(x => x.ElementAt(i)).Sum();
                simpairs.Add(i + 1, ea);
            }

            double[] dans = new double[45];

            foreach (int num in lastInts)
            {
                dans[num - 1] = 1.0 / 6.0;
            }

            var mtxpairs = await Task.Run(() => Utility.MultipleMatrix(dans, chullists)
                                     .Select((v, i) => (v, i)).ToDictionary(x => x.i + 1, x => x.v));
            var ordpairs = mtxpairs.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            foreach (var key in ordpairs.Keys)
            {
                var val = ordpairs[key];
                string s1 = key.ToString("00") + "번";
                string s2 = val.ToString("F7");
                string s3 = simpairs[key].ToString("F7");
                string[] s = { s1, s3, "", s2 };

                lists.Add(s);
            }

            return lists;
        }

        /// <summary>
        /// 고정데이터 마코프
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        private List<string[]> CheckedFixData(string[] strs)
        {
            //0-최대출 사이 마코프 결과 출력 (항목, 번호, 최종출, 마코프배열)
            var ealine = new List<string[]>();
            //최종출 전체 마코프 결과
            var smline = new List<string[]>();

            foreach (string item in strs)
            {
                var tpl = _fixPairs[item];
                var nums = _numPairs[item];
                var data = tpl.Select(x => x.chulInt).ToList();
                int[] lastdata = data.Last();
                double[] d1 = lastdata.Select(x => x / 6.0).ToArray();

                List<double[]> sums = new();
                var pairs = new Dictionary<string, (string, string, double[])>();
                var gdidx = new List<int>();

                for (int i = 0; i < lastdata.Length; i++)
                {
                    string s = item + "_" + i;
                    string s1 = string.Join(",", nums[i].Where(x => x >= 1  && x <= 45).OrderBy(x => x).Select(x => x.ToString("00")));

                    //열별 전체 출수
                    var each = data.Select(x => x.ElementAt(i)).ToList();
                    int last = each[^1];
                    var dicts = each.Distinct().Where(x => each.Count(g => g == x) > each.Count * 0.01)
                                    .OrderBy(x => x).ToArray();
                    var chulpers = dicts.Select(x => NextShownList(each, dicts, x)).ToList();
                    double[] source = new double[dicts.Length];

                    //0 - 최대출 사이 마코프검사
                    if (dicts.Contains(last))
                    {
                        int a = Array.IndexOf(dicts, last);
                        source[a] = 1.0;
                        double[] geysan = Utility.MultipleMatrix(source, chulpers);
                        double d = geysan[a];
                        double v1 = Math.Round(geysan.Max(), 7);
                        double v2 = Math.Round(d, 7);

                        if (v1.Equals(v2))
                        {
                            var zip = dicts.Zip(geysan, (a, b) => "[" + a + "] " + b.ToString("F3"));
                            string s3 = string.Join(",  ", zip);
                            string[] strings = { s, s1, last.ToString(), s3 };
                            ealine.Add(strings);
                            gdidx.Add(i);
                        }
                    }

                    //전체출수 마코프검사
                    var (realCount, maxCount, nextList, ordList) = RealMaxNextList(each);

                    //다음출 인덱스중 후방 3개 추출
                    if (ordList.Count > _jeoniValue)
                    {
                        var realords = ordList.Skip(ordList.Count - _jeoniValue);
                        var eas = new List<int[]>();
                        foreach (int ord in realords)
                        {
                            int[] ea = tpl[ord].chulInt;
                            eas.Add(ea);
                        }

                        //pivot
                        double[] sms = new double[eas[0].Length];
                        string[] col = new string[eas[0].Length];
                        for (int j = 0; j < eas[0].Length; j++)
                        {
                            int n = eas.Select(x => x.ElementAt(j)).Sum();
                            string s2 = string.Join(",", eas.Select(x => x.ElementAt(j)));
                            col[j] = s2;
                            double d = _jeoniValue * 6.0;

                            sms[j] = n / d;
                        }

                        if (realCount < 3 && nextList.Count > data.Count * 0.02)
                        {
                            pairs.Add(s, (s1, col[i], sms));
                        }
                    }
                }

                if (pairs.Any() && pairs.Count == lastdata.Length)
                {
                    //전이행렬 계산
                    var dts = pairs.Values.Select(x => x.Item3).ToList();
                    var chs = Utility.MultipleMatrix(d1, dts);
                    double d3 = Math.Round(chs.Max(), 7);

                    for (int i = 0; i < chs.Length; i++)
                    {
                        double d4 = Math.Round(chs[i], 7);

                        if (gdidx.Contains(i) && d3.Equals(d4))
                        {
                            string st1 = item + "_" + i;
                            string st2 = string.Join(",", nums[i].Where(x => x > 0 && x < 46).OrderBy(x => x).Select(x => x.ToString("00")));
                            string st3 = lastdata[i].ToString();
                            string st4 = chs[i].ToString("F7");

                            string[] lm = { st1, st2, st3, st4 };
                            smline.Add(lm);
                        }
                    }
                }
            }

            ealine.AddRange(smline);

            return ealine;
        }

        /// <summary>
        /// 고정데이터 해당번호
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, List<int[]>> GetFixNumbers()
        {
            var pairs = new Dictionary<string, List<int[]>>();
            string[] directs = { "R", "D", "S" };
            string[] flows = { "H", "V" };
            string[] cols = { "05", "07", "09" };

            var finds = _columnNames.Where(x => x.Length == 4 && (x.StartsWith("R") || x.StartsWith("D") || x.StartsWith("S"))).ToArray();

            foreach (string item in finds)
            {
                int col = int.Parse(item[2..]);
                int idx = Array.IndexOf(directs, item[0].ToString());
                int hdx = Array.IndexOf(flows, item[1].ToString());

                var numslist = (hdx == 0) ? LuckyLibrary.HorizontalFlowDatas(col, idx) : LuckyLibrary.VerticalFlowDatas(col, idx);
                pairs.Add(item, numslist);
            }

            pairs.Add("Beondae", LuckyLibrary.BeondaeDatas());
            pairs.Add("Slipsu", LuckyLibrary.SlipDatas());
            pairs.Add("Kkeutbeon", LuckyLibrary.KkeutbeonDatas());

            return pairs;
        }

        /// <summary>
        /// 전체 고정데이터
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, List<(int, int[])>> GetAllFixDatas()
        {
            var pairs = new Dictionary<string, List<(int, int[])>>();

            foreach (string name in _columnNames)
            {
                var lists = new List<(int, int[])>();
                string query = "SELECT Orders, " + name + " FROM FixChulsuTbl";
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ord = reader.GetInt32(0);
                    string s = reader.GetString(1).Trim();
                    int[] arr = ChangeToInts(s);

                    lists.Add((ord, arr));
                }

                pairs.Add(name, lists);
            }

            return pairs;
        }

        /// <summary>
        /// 전체회차의 당번
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, int[]> GetDangbeon()
        {
            var pairs = new Dictionary<int, int[]>();

            using (var db = new LottoDBContext())
            {
                pairs = db.BasicTbl.ToDictionary(x => x.Orders, x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }.ToArray());
            }

            return pairs;
        }

        /// <summary>
        /// 문자열 내용을 정수배열 반환
        /// </summary>
        /// <param name="s">빈공간 없는 문자열</param>
        /// <returns>정수배열</returns>
        private static int[] ChangeToInts(string s)
        {
            return s.Select(x => int.Parse(x.ToString())).ToArray();
        }

        /// <summary>
        /// 후방연속, 연속최대, 다음출리스트 (연속최대가 -1:전부동일, 0:끝출외 무출)
        /// </summary>
        /// <param name="ascCollection">오름차순 데이터 배열</param>
        /// <returns>튜플(후방연속갯수, 연속최대갯수, 다음출리스트)</returns>
        private static (int realCount, int maxCount, List<int> nextList, List<int> ordList) RealMaxNextList
                      (IEnumerable<int> ascCollection)
        {
            if (!ascCollection?.Any() ?? false)
            {
                throw new Exception("배열에 요소가 없습니다.");
            }

            int[] ascArray = ascCollection.ToArray();

            int real = 0;
            int last = ascArray[^1];

            //후방연속
            for (int i = ascArray.Length - 1; i >= 0; i--)
            {
                if (last == ascArray[i])
                    real++;
                else
                    break;
            }

            //연속최대
            int[] ascRemind = ascArray[..^real];
            int max = 0, dup = 0;
            List<int> indxs = new();
            List<int> nexts = new();

            for (int i = 0; i < ascRemind.Length; i++)
            {
                int n = ascRemind[i];

                if (n == last)
                {
                    dup++;
                    indxs.Add(i);
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

            var ords = new List<int>();

            if (indxs.Any())
            {
                foreach (int i in indxs)
                {
                    int sameCount = 0;

                    for (int j = 0; j < real; j++)
                    {
                        int a = i + j;

                        if (a < ascArray.Length)
                        {
                            int n = ascArray[a];

                            if (n == last)
                                sameCount++;
                            else
                                break;
                        }
                    }

                    if (sameCount == real)
                    {
                        int a = i + real;

                        if (a < ascArray.Length)
                        {
                            int n = ascArray[a];
                            ords.Add(a);
                            nexts.Add(n);
                        }
                    }
                }
            }

            return (real, max, nexts, ords);
        }

        /// <summary>
        /// 다음출 퍼센트
        /// </summary>
        /// <param name="lists">열별 전체 출수</param>
        /// <param name="dicts">최소 최대출 배열</param>
        /// <param name="number">최종출</param>
        /// <param name="length">후방간격</param>
        /// <returns></returns>
        private double[] NextShownList(List<int> lists, int[] dicts, int number)
        {
            var rst = new List<int>();

            var idxs = lists.Select((v, i) => (v, i))
                            .Where(x => x.v == number).Select(x => x.i + 1)
                            .Where(x => x < lists.Count);

            if (idxs.Any())
            {
                var skips = idxs.Skip(idxs.Count() - _matrixLength);
                foreach (var idx in skips)
                {
                    int n = lists[idx];
                    rst.Add(n);
                }

                var chuls = new List<int>();
                foreach (int dict in dicts)
                {
                    int n = rst.Count(x => x == dict);
                    chuls.Add(n);
                }

                double sum = chuls.Sum();
                var arr = chuls.Select(x => x / sum).ToArray();
                return arr;
            }
            else
            {
                return Array.Empty<double>();
            }
        }
    }
}
