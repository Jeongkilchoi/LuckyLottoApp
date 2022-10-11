using System.Collections.Concurrent;
using System.Data;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 전체당번 시퀸스이격 으로 당번의 직선, 사선검사하는 폼 클래스
    /// </summary>
    public partial class SequenceGapForm : Form
    {
        #region Field

        private readonly int _lastOrder;
        private Size _size = new(14, 14);
        private readonly int[] _gapInts;
        private int _index = 0;
        private int _colgap = 1;
        private int _rowgap = 1;
        private List<int> _idxs;
        private List<(int row, int col)> _wayLists;
        private readonly Dictionary<int, List<(int, int[])>> _originChulPairs;
        private List<DiagonalData> _diagonalDatas;

        #endregion

        #region Property

        /// <summary>
        /// 선택한 회차
        /// </summary>
        public int Order { get; set; } = 900;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public SequenceGapForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            Order = lastOrder;
            _gapInts = LuckyLibrary.SeqenceGapInts;
            _originChulPairs = GetOriginChulData();
        }

        private void SequenceGapForm_Load(object sender, EventArgs e)
        {
            _idxs = new List<int>();
            _wayLists = new List<(int row, int col)>();

            //인덱스 항목 추가
            for (int i = -44; i < 0; i++)
            {
                _idxs.Add(i);
            }

            for (int i = 0; i < 45; i++)
            {
                _idxs.Add(i);
            }

            _idxs.ForEach(x => IndexComboBox.Items.Add(x));
            IndexComboBox.SelectedIndex = 44;

            //방법 항목 추가 (01-12 :앞자리는 행간격, 뒷자리는 열간격)
            for (int i = 1; i < 10; i++)
            {
                foreach (int gap in _gapInts)
                {
                    _wayLists.Add((i, gap));
                    string s = i.ToString("00") + "-" + gap.ToString("00");
                    WayComboBox.Items.Add(s);
                }
            }

            WayComboBox.SelectedIndex = 0;

            OrderNumericUpDown.Value = _lastOrder;
            OrderNumericUpDown.Maximum = _lastOrder;
        }

        private void MainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawLine(g);
            DrawResult(g);
        }

        private void WayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WayComboBox.SelectedIndex > -1)
            {
                string sel = (string)WayComboBox.SelectedItem;
                _colgap = int.Parse(sel[3..]);
                _rowgap = int.Parse(sel[..2]);
                MainPictureBox.Invalidate();
            }
        }

        private void OrderNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Order = (int)OrderNumericUpDown.Value;
            MainPictureBox.Invalidate();
        }

        private void IndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IndexComboBox.SelectedIndex > -1)
            {
                _index = (int)IndexComboBox.SelectedItem;
                MainPictureBox.Invalidate();
            }
        }

        private void NumberNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            SortTextBox.Text = string.Empty;
            int number = (int)NumberNumericUpDown.Value;
            string s = string.Empty;

            if (_diagonalDatas.Any())
            {
                var sels = _diagonalDatas.Where(x => x.Number == number).OrderBy(x => x.IsDuplicate)
                    .ThenBy(x => x.Way).ThenBy(x => x.Direction);

                if (sels.Any())
                {
                    foreach (var data in sels)
                    {
                        string s4 = data.Direction + " / " + data.Way + " / " + data.DuplicateCount + " / " +
                                    data.Number + " / " + data.IsDuplicate;
                        s += s4 + "\r\n";
                    }
                }
            }

            SortTextBox.Text = s;
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            if (!_diagonalDatas.Any())
            {
                return;
            }

            string s = string.Empty;
            string s1 = string.Empty;

            if (DupRadioButton.Checked)
            {
                if (AllRadioButton.Checked)
                {
                    var dic = _diagonalDatas.Where(x => x.IsDuplicate != "간격").OrderBy(x => x.Number);
                    var list = new List<int>();

                    //전체검사
                    foreach (var dt in dic)
                    {
                        s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                        list.Add(dt.Number);
                    }

                    list.Sort();
                    s1 = string.Join(",", list.Select(x => x.ToString("00")));
                }
                else if (LeftRadioButton.Checked)
                {
                    //좌사선
                    var dic = _diagonalDatas.Where(x => x.Direction == "좌사선" && x.IsDuplicate != "간격").OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
                else if (RightRadioButton.Checked)
                {
                    //우사선
                    var dic = _diagonalDatas.Where(x => x.Direction == "우사선" && x.IsDuplicate != "간격").OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
                else if (DiagonalRadioButton.Checked)
                {
                    //좌우사선
                    var dic = _diagonalDatas.Where(x => x.IsDuplicate != "간격" && (x.Direction == "좌사선" || x.Direction == "우사선"))
                                               .OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
                else
                {
                    //상하직선
                    var dic = _diagonalDatas.Where(x => x.Direction == "상하선" && x.IsDuplicate != "간격").OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
            }
            else
            {
                if (AllRadioButton.Checked)
                {
                    var dic = _diagonalDatas.Where(x => x.IsDuplicate == "간격").OrderBy(x => x.Number);
                    var list = new List<int>();

                    //전체검사
                    foreach (var dt in dic)
                    {
                        s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                        list.Add(dt.Number);
                    }

                    list.Sort();
                    s1 = string.Join(",", list.Select(x => x.ToString("00")));
                }
                else if (LeftRadioButton.Checked)
                {
                    //좌사선
                    var dic = _diagonalDatas.Where(x => x.Direction == "좌사선" && x.IsDuplicate == "간격").OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
                else if (RightRadioButton.Checked)
                {
                    //우사선
                    var dic = _diagonalDatas.Where(x => x.Direction == "우사선" && x.IsDuplicate == "간격").OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
                else if (DiagonalRadioButton.Checked)
                {
                    //좌우사선
                    var dic = _diagonalDatas.Where(x => x.IsDuplicate == "간격" && (x.Direction == "좌사선" || x.Direction == "우사선"))
                                               .OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
                else
                {
                    //상하직선
                    var dic = _diagonalDatas.Where(x => x.IsDuplicate == "간격" && x.Direction == "상하선").OrderBy(x => x.Number);

                    if (dic.Any())
                    {
                        var list = new List<int>();

                        foreach (var dt in dic)
                        {
                            s += dt.Direction + " / " + dt.Way + " / " + dt.DuplicateCount + " / " + dt.Number + " / " + dt.IsDuplicate + "\r\n";
                            list.Add(dt.Number);
                        }

                        list.Sort();
                        s1 = string.Join(",", list.Select(x => x.ToString("00")));
                    }
                }
            }

            ResultTextBox.Text = s + "\r\n\r\n" + s1;
            ResultTextBox.SelectionStart = ResultTextBox.Text.Length;
            ResultTextBox.ScrollToCaret();
        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            ExecuteButton.Enabled = false;
            int san = (int)DiagonalNumupdw.Value;
            int din = (int)DirectNumupdw.Value;
            int mln = (int)MultyNumupdw.Value;
            var rst = new List<DiagonalData>();

            try
            {
                var tpdwdatas = new List<DiagonalData>();
                await foreach (var item in TopdownCheck(din))
                {
                    tpdwdatas.Add(item);
                    rst.Add(item);
                }

                var digonals = new List<DiagonalData>();
                await foreach (var item in DiagonalCheck(san))
                {
                    digonals.Add(item);
                    rst.Add(item);
                }

                var strlist = new List<string>();
                foreach (var data in rst.OrderBy(x => x.Number))
                {
                    string s = data.Direction + " / " + data.Way + " / " + data.DuplicateCount + " / " + data.Number + " / " + data.IsDuplicate;
                    strlist.Add(s);
                }
                ResultTextBox.Text = string.Join(Environment.NewLine, strlist);
                _diagonalDatas = new List<DiagonalData>(rst);
                var boklist = new List<int>();
                for (int i = 1; i <= 45; i++)
                {
                    var find = rst.Where(x => x.Number == i);

                    if (find.Count() >= mln)
                    {
                        int juncnt = find.Where(x => x.IsDuplicate.Equals("중복")).Select(x => x.Direction).Distinct().Count();
                        int kancnt = find.Where(x => x.IsDuplicate.Equals("간격")).Select(x => x.Direction).Distinct().Count();
                        var junway = find.Where(x => x.IsDuplicate.Equals("중복")).Select(x => x.Way[..2]).Count();
                        var kanway = find.Where(x => x.IsDuplicate.Equals("간격")).Select(x => x.Way[..2]).Count();

                        if (juncnt >= 2 && junway >= mln)// kancnt >= 2 && junway >= mln && kanway >= mln)
                        {
                            boklist.Add(i);
                        }
                    }
                }

                var tpdwpairs = tpdwdatas.Select(x => x.Number).GroupBy(g => g).ToDictionary(x => x.Key, x => x.Count());
                var digopairs = digonals.Select(x => x.Number).GroupBy(g => g).ToDictionary(x => x.Key, x => x.Count());
                DirectTextBox.Text = string.Join("\t", tpdwpairs.OrderBy(x => x.Key).Select(x => x.Key + "-" + x.Value));
                DiagonalTextBox.Text = string.Join("\t", digopairs.OrderBy(x => x.Key).Select(x => x.Key + "-" + x.Value));
                BokhapTextBox.Text = string.Join(",", boklist.Select(x => x.ToString("00")));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ExecuteButton.Enabled = true;
                ExecuteButton.Text = "검사하기";
                panel3.Enabled = true;
                QueryButton.Enabled = true;
                ResultTextBox.Enabled = true;
                NumberNumericUpDown.Enabled = true;
            }
        }



        //******************  내부 메서드  ***********************



        /// <summary>
        /// 회차, 번호, 도트 그리기
        /// </summary>
        /// <param name="g">그래픽 인스턴스</param>
        private void DrawResult(Graphics g)
        {
            var pairs = GetDotChulData();

            var ords = pairs.Keys.ToList();
            ords.Add(pairs.Keys.Max() + _rowgap);

            //회차 그리기
            for (int i = 0; i < ords.Count; i++)
            {
                int ord = ords[i];
                string s = ord.ToString();

                //상자 가운데 번호 그리기
                var rect = new Rectangle(10, 10 + (i * _size.Height), 40, 14);
                using var font = new Font("맑은고딕", 7.5F);
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(s, font, Brushes.Black, rect, stringFormat);
            }

            //하단에 번호 그리기
            int[] seqs = LuckyLibrary.NewSequenceLists()[_colgap];
            int val = _index < 0 ? 45 - Math.Abs(_index) : _index;
            int[] nums = LinkedCircularList(seqs, val);

            for (int i = 0; i < nums.Length; i++)
            {
                string s = nums[i].ToString("00");

                //상자 가운데 번호 그리기
                var rect = new Rectangle(50 + (i * _size.Width), 10 + (44 * _size.Height), _size.Width, _size.Height);

                using var font = new Font("맑은고딕", 6.5F);
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(s, font, Brushes.Black, rect, stringFormat);
            }

            //나온 번호에 점찍기
            for (int i = 0; i < 44; i++)
            {
                var dt = pairs.ElementAt(i).Value;

                for (int j = 0; j < 45; j++)
                {
                    var num = dt[j];

                    if (num != 0)
                    {
                        var rec = new Rectangle(50 + 2 + (j * 14), 10 + 2 + (i * 14), 10, 10);

                        using var brush = new SolidBrush(Color.Black);
                        g.FillEllipse(brush, rec);
                    }
                }
            }
        }

        /// <summary>
        /// 가로, 세로 라인 그리기
        /// </summary>
        /// <param name="g">그래픽 인스턴스</param>
        private void DrawLine(Graphics g)
        {
            //가로줄 그리기
            for (int i = 0; i <= 45; i++)
            {
                Point p1 = new(10, 10 + (i * _size.Height));
                Point p2 = new(10 + 40 + (45 * _size.Width), 10 + (i * _size.Height));

                using var pen = new Pen(Color.FromArgb(100, Color.Gray), 0.1F);
                g.DrawLine(pen, p1, p2);
            }

            //좌측 끝선 그리기
            Point l1 = new(10, 10);
            Point l2 = new(10, 10 + (45 * _size.Height));

            using (var pen = new Pen(Color.FromArgb(100, Color.Gray), 0.1F))
            {
                g.DrawLine(pen, l1, l2);
            }

            //회차칸 선 그리기
            Point h1 = new(10 + 40, 10);
            Point h2 = new(10 + 40, 10 + (45 * _size.Height));

            using (var pen = new Pen(Color.FromArgb(100, Color.Gray), 0.1F))
            {
                g.DrawLine(pen, h1, h2);
            }

            //세로줄 그리기
            for (int i = 1; i <= 45; i++)
            {
                Point p1 = new(50 + (i * _size.Width), 10);
                Point p2 = new(50 + (i * _size.Width), 10 + (45 * _size.Height));

                using var pen = new Pen(Color.FromArgb(100, Color.Gray), 0.1F);
                g.DrawLine(pen, p1, p2);
            }

            //좌사선 (파란선) 그리기
            Point b1 = new(50, 10);
            Point b2 = new(50 + (44 * _size.Width), 10 + (44 * _size.Height));

            using (var pen = new Pen(Color.Blue, 1F))
            {
                g.DrawLine(pen, b1, b2);
            }

            //우사선 (빨간선) 그리기
            Point r1 = new(50 + (45 * _size.Width), 10);
            Point r2 = new(50 + (1 * _size.Width), 10 + (44 * _size.Height));

            using (var pen = new Pen(Color.Red, 1F))
            {
                g.DrawLine(pen, r1, r2);
            }
        }

        /// <summary>
        /// 주기(행간격)의 당번출현 데이터
        /// </summary>
        /// <returns>튜플(행간격, 리스트((회차, 당번배열)))</returns>
        private Dictionary<int, List<(int, int[])>> GetOriginChulData()
        {
            var pairs = new Dictionary<int, List<(int, int[])>>();

            for (int i = 1; i < 10; i++)
            {
                var jukis = Utility.OrderOfJukiInts(i, Order, 44);
                var tpl = jukis.Select(x => (x, Utility.DangbeonOfOrder(x).ToArray())).ToList();

                pairs.Add(i, tpl);
            }

            return pairs;
        }

        /// <summary>
        /// 구간 출수를 도트출수로 바꾼 딕셔너리
        /// </summary>
        /// <returns>딕셔너리(키: 회차, 값:도트출수)</returns>
        private Dictionary<int, int[]> GetDotChulData()
        {
            var pairs = new Dictionary<int, int[]>();

            //원본당번을 도트출수로 바꾸기
            var tpls = _originChulPairs[_rowgap];
            int[] seqs = LuckyLibrary.NewSequenceLists()[_colgap];
            int val = _index < 0 ? 45 - Math.Abs(_index) : _index;
            int[] chgs = LinkedCircularList(seqs, val);

            foreach (var tpl in tpls)
            {
                var chul = tpl.Item2;
                var dots = chgs.Select(x => chul.Contains(x) ? 1 : 0).ToArray();
                pairs.Add(tpl.Item1, dots);
            }

            return pairs;
        }

        /// <summary>
        /// 리스트의 변경되 인덱스의 배열
        /// </summary>
        /// <param name="lists">시퀸스 배열</param>
        /// <param name="index">검사시작 인덱스</param>
        /// <returns>변경되 배열</returns>
        private static int[] LinkedCircularList(int[] lists, int index)
        {
            var linkedList = new LinkedList<int>();
            linkedList.AddFirst(lists[0]);
            var linkNode = linkedList.Find(lists.First());

            foreach (var list in lists.Skip(1))
            {
                var node1 = new LinkedListNode<int>(list);
                linkedList.AddAfter(linkNode, node1);
                linkNode = node1;
            }

            int startValue = lists.ToList()[index];
            var node = linkedList.Find(startValue);

            var result = new List<int>();

            for (int i = 0; i < lists.Length; i++)
            {
                result.Add(node.Value);
                node = node.Next ?? linkedList.First;
            }

            return result.ToArray();
        }

        /// <summary>
        /// 출수의 간격체크
        /// </summary>
        /// <param name="collection">리스트</param>
        /// <param name="hangey">한계값 (기본:2)</param>
        /// <returns>튜플(검사통과여부, 후방연속갯수)</returns>
        private static  async Task<(bool pass, int real)> EachCheck(IEnumerable<int> collection, int hangey = 2)
        {
            bool isbad = false;
            int last = collection.Last();
            int limit = (int)(collection.Count() * 0.1);
            int realCount = 0;

            if (last == 0)
            {
                var find1 = collection.Select((n, i) => (n, i)).Where(x => x.n == 1).Select(x => x.i).ToList();
                find1.Add(collection.Count());

                var zip1 = find1.Zip(find1.Skip(1), (a, b) => b - a).ToList();
                zip1.Insert(0, find1.First() + 1);

                if (zip1.Count >= 5)
                {
                    var (real, max) = await Task.Run(() => NextReal.RealMaxCount(zip1));

                    //최종간격이 한계이상이면서 후방연속갯수가 2 이상이고 후방연속 > 연속최대 이면 악번
                    if (zip1.Last() >= limit && real >= hangey && real > max)
                    {
                        isbad = true;
                    }

                    realCount = real;
                }

                return (isbad, realCount);
            }
            else
            {
                var (real, max) = await Task.Run(() => NextReal.RealMaxCount(collection));

                //후방연속갯수가 2 이상이고 후방연속 > 연속최대 이면 악번
                if (real >= hangey && real > max)
                {
                    isbad = true;
                }

                realCount = real;
                return (isbad, realCount);
            }
        }

        /// <summary>
        /// 사선검사에 사용할 데이터 클래스
        /// </summary>
        private class DiagonalData
        {
            /// <summary>
            /// 좌사선, 우사선, 상하선
            /// </summary>
            public string Direction { get; set; }

            /// <summary>
            /// 5-1, 1-1, 5-5 방법
            /// </summary>
            public string Way { get; set; }

            /// <summary>
            /// 중복, 간격, 최소
            /// </summary>
            public string IsDuplicate { get; set; }

            /// <summary>
            /// 중복의 갯수
            /// </summary>
            public int DuplicateCount { get; set; }

            /// <summary>
            /// 해당번호
            /// </summary>
            public int Number { get; set; }

            public int Index { get; set; }
        }

        /// <summary>
        /// 상하검사 결과를 반환
        /// </summary>
        /// <param name="limit">한계값</param>
        /// <returns>지연 리스트<DiagonalData></returns>
        private async IAsyncEnumerable<DiagonalData> TopdownCheck(int limit)
        {
            //전체데이터 불러오기
            //연결리스트 체크할 필요없음 (어차피 인덱스로 검사할 것이기 때문에)
            var cols = LuckyLibrary.SeqenceGapInts;
            var numbers = Enumerable.Range(1, 45);
            for (int row = 1; row <= 20; row++)
            {
                ExecuteButton.Invoke((Action) delegate
                {
                    ExecuteButton.Text = "직선: " + row + " / 20";
                });
                var jukis = Utility.OrderOfJukiInts(row, _lastOrder, 44);
                var judangs = jukis.Select(x => Utility.DangbeonOfOrder(x));
                var judots = judangs.Select(x => ChangeDangbeonToDots(numbers, x));

                for (int i = 0; i < 45; i++)
                {
                    var pivotcols = judots.Select(x => x.ElementAt(i)).ToArray();
                    var (pass, real) = await EachCheck(pivotcols, limit);

                    if (pass)
                    {
                        var dt = new DiagonalData
                        {
                            Direction = "상하선",
                            Way = row.ToString("00") + "-01",
                            IsDuplicate = pivotcols[^1] == 1 ? "중복" : "간격",
                            DuplicateCount = real,
                            Number = numbers.ToArray()[i]
                        };

                        yield return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 좌우사선검사 결과를 반환
        /// </summary>
        /// <param name="limit">한계값</param>
        /// <returns>지연 리스트<DiagonalData></returns>
        private async IAsyncEnumerable<DiagonalData> DiagonalCheck(int limit)
        {
            //전체데이터 불러오기
            var cols = LuckyLibrary.SeqenceGapInts;
            var passedlefts = new ConcurrentBag<(int[], int[])>();
            var passedrights = new ConcurrentBag<(int[], int[])>();
            for (int row = 1; row < 10; row++)
            {
                ExecuteButton.Invoke((Action)delegate
                {
                    ExecuteButton.Text = "사선: " + row + " / 9";
                });
                var jukis = Utility.OrderOfJukiInts(row, _lastOrder, 44);
                var judangs = jukis.Select(x => Utility.DangbeonOfOrder(x));

                foreach (var col in cols)
                {
                    var arr = LuckyLibrary.NewSequenceLists()[col];
                    var tasks = new List<Task>();
                    for (int j = 0; j < 45; j++)
                    {
                        var linkInts = LinkedCircularList(arr, j);
                        var lists = judangs.Select(x => ChangeDangbeonToDots(linkInts, x)).ToList();
                        var (leftlist, rightlist) = GetDiagonalData(lists);

                        //좌사선검사
                        var (pass, real) = await EachCheck(leftlist, limit);
                        bool isleft = await Task.Run(() => IsContainChecked(passedlefts, jukis, leftlist));
                        if (leftlist.Count(x => x != 0) >= 5 && isleft && pass)
                        {
                            var dt = new DiagonalData
                            {
                                Direction = "좌사선",
                                Way = row.ToString("00") + "-" + col.ToString("00"),
                                IsDuplicate = leftlist[^1] == 1 ? "중복" : "간격",
                                DuplicateCount = real,
                                Number = linkInts[^1],
                                Index = j
                            };
                            passedlefts.Add((jukis, leftlist));
                            yield return dt;
                        }

                        //우사선검사
                        var rightpl = await EachCheck(rightlist, limit);
                        bool isright = await Task.Run(() => IsContainChecked(passedrights, jukis, rightlist));
                        if (rightlist.Count(x => x != 0) >= 5 && isright && rightpl.pass)
                        {
                            var dt = new DiagonalData
                            {
                                Direction = "우사선",
                                Way = row.ToString("00") + "-" + col.ToString("00"),
                                IsDuplicate = rightlist[^1] == 1 ? "중복" : "간격",
                                DuplicateCount = rightpl.real,
                                Number = linkInts[0],
                                Index = j
                            };
                            passedrights.Add((jukis, rightlist));
                            yield return dt;
                        }
                    } 
                }
            }
        }

        /// <summary>
        /// 좌우사선 검사에 이미 포함된지 여부
        /// </summary>
        /// <param name="passedlists">검사통과한 리스트</param>
        /// <param name="jukis">주기회차 배열</param>
        /// <param name="colist">사선도트 배열</param>
        /// <returns>포함되지 않았으면 참</returns>
        private static bool IsContainChecked(ConcurrentBag<(int[], int[])> passedlists, int[] jukis, int[] colist)
        {
            if (!passedlists.Any())
            {
                return true;
            }
            else
            {
                bool pass = passedlists.All(x => !(x.Item1.SequenceEqual(jukis) && x.Item2.SequenceEqual(colist)));
                return pass;
            }
        }

        /// <summary>
        /// 당번을 시퀸스 도트형태로 바꾼것
        /// </summary>
        /// <param name="sequence">45개 시퀀스</param>
        /// <param name="dang">당번</param>
        /// <returns>정수배열</returns>
        private static int[] ChangeDangbeonToDots(IEnumerable<int> sequence, IEnumerable<int> dang)
        {
            int[] array = new int[45];

            //당번이 나온 인덱스 컬렉션
            var idxs = sequence.Select((val, idx) => (val, idx)).Where(x => dang.Contains(x.val)).Select(x => x.idx);

            for (int i = 0; i < 45; i++)
            {
                if (idxs.Contains(i))
                {
                    array[i] = 1;
                }
            }

            return array;
        }

        /// <summary>
        /// 좌사선, 우사선 데이터
        /// </summary>
        /// <param name="lists">배열 리스트</param>
        /// <returns>튜플<좌사선데이터, 우사선데이터></returns>
        private static (int[] leftlist, int[] rightlist) GetDiagonalData(List<int[]> lists)
        {
            if (!lists?.Any() ?? false || lists.Any(x => x.Length != 45))
            {
                throw new Exception("리스트가 비었거나, 요소의 길이가 45가 아닙니다.");
            }

            var lefts = new List<int>();
            var rights = new List<int>();
            for (int i = 0; i < lists.Count; i++)
            {
                int maxindex = lists[0].Length - 1;

                //좌사선은 행열 인덱스가 같음
                int left = lists[i][i];

                //우사선은 열인덱스 감소
                int right = lists[i][maxindex - i];
                lefts.Add(left);
                rights.Add(right);
            }

            return (lefts.ToArray(), rights.ToArray());
        }
    }
}
