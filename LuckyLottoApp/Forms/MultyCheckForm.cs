using System.Collections.Concurrent;
using System.Data;
using System.Text;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;
using LuckyLottoLibrary;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 데이터 복합검사하는 폼 클래스
    /// </summary>
    public partial class MultyCheckForm : Form
    {
        #region Field

        private readonly int _lastOrder;
        private readonly string[] _itemNames;
        private Dictionary<int, List<int>> _allPairs;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public MultyCheckForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            _itemNames = new string[]
            {
                "추첨일 악번검사", "당번 주변수검사", "10주 구간검사",  "이월당번 악번검사", "당번다음 악번검사",
                "진수 호악번검사", "도트 호악번검사", "산출식 호악번검사", "상생, 상극검사", "이월,동격,연속,연끝"
            };
        }

        private async void MultyCheckForm_Load(object sender, EventArgs e)
        {
            _allPairs = await GetAllWinningNumber();

            _itemNames.ToList().ForEach(x => ColumnComboBox.Items.Add(x));
            ColumnComboBox.SelectedIndex = 0;
        }

        private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColumnComboBox.SelectedIndex > -1)
            {
                ViewListView.Clear();
                UpperTextBox.Text = string.Empty;
                DownTextBox.Text = string.Empty;
                int sel = ColumnComboBox.SelectedIndex;

                switch (sel)
                {
                    case 0:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = true;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckWinningDay();
                        break;
                    case 1:
                        SectionNumericUpDown.Visible = true;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckAround();
                        break;
                    case 2:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckTenInterval();
                        break;
                    case 3:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckCarryOver();
                        break;
                    case 4:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckDangNext();
                        break;
                    case 5:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckJinsu();
                        break;
                    case 6:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckDangbeonSanchul();
                        break;
                    case 7:
                        SectionNumericUpDown.Visible = false;
                        BeforeNumericUpDown.Visible = true;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckFormula();
                        break;
                    case 8:
                        SectionNumericUpDown.Visible = true;
                        BeforeNumericUpDown.Visible = true;
                        LimitNumericUpDown.Visible = true;
                        WriteButton.Visible = true;
                        CheckTogether();
                        break;
                    default:
                        SectionNumericUpDown.Visible = true;
                        BeforeNumericUpDown.Visible = false;
                        LimitNumericUpDown.Visible = false;
                        WriteButton.Visible = false;
                        CheckCarriedForward();
                        break;
                }
            }
        }

        private void LimitNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int sec = (int)SectionNumericUpDown.Value;
            int start = _lastOrder - sec + 1;

            DownTextBox.Text = NowHatePair(start);
        }

        private void BeforeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ColumnComboBox.SelectedIndex == 0)
            {
                ViewListView.Clear();
                CheckWinningDay();
            }
            else if (ColumnComboBox.SelectedIndex == 2)
            {
                ViewListView.Clear();
                CheckTenInterval();
            }
            else if (ColumnComboBox.SelectedIndex == 5)
            {
                BeforeNumericUpDown.Minimum = 1;
                BeforeNumericUpDown.Maximum = 50;
                ViewListView.Clear();

                ViewListView.Columns.Add("회 차", 50, HorizontalAlignment.Center);

                for (int i = 1; i <= 6; i++)
                {
                    ViewListView.Columns.Add(i + "구", 40, HorizontalAlignment.Center);
                }

                ViewListView.Columns.Add("회 차", 50, HorizontalAlignment.Center);

                for (int i = 1; i <= 6; i++)
                {
                    ViewListView.Columns.Add(i + "공", 40, HorizontalAlignment.Center);
                }

                //전체가 아니라 100 구간만 출력
                int start = _lastOrder - 99;
                int bfr = (int)BeforeNumericUpDown.Value;
                int cnt = 0;
                int lastord = 0;

                for (int i = start; i <= _lastOrder; i++)
                {
                    int v = i - bfr;
                    lastord = v;
                    var lvi = new ListViewItem(v.ToString()) { UseItemStyleForSubItems = false };
                    var sun = Utility.SunDangOfOrder(v);
                    var jinsus = sun.Select(x => Convert.ToString(x, 2).PadLeft(6, '0'));
                    List<int> imsi = new();
                    //pivot
                    for (int j = 0; j < 6; j++)
                    {
                        var ver = jinsus.Select(x => x.ElementAt(j)).ToArray();
                        string s = new(ver);

                        //이진문자열을 정수로 바꾸기
                        int m = Convert.ToInt32(s, 2);
                        imsi.Add(m);
                        string n = m >= 1 && m <= 45 ? m.ToString() : "";

                        lvi.SubItems.Add(n);
                    }

                    lvi.SubItems.Add(i.ToString());

                    var dan = Utility.DangbeonOfOrder(i).ToArray();
                    cnt += (!dan.Intersect(imsi).Any()) ? 0 : 1;

                    for (int k = 0; k < 6; k++)
                    {
                        int item = dan[k];
                        lvi.SubItems.Add(item.ToString());

                        if (imsi.Contains(item))
                        {
                            lvi.SubItems[8 + k].BackColor = Color.Cyan;
                        }
                    }

                    ViewListView.Items.Add(lvi);
                }

                var lastlvi = new ListViewItem((lastord + 1).ToString());
                var sung = Utility.SunDangOfOrder(lastord + 1);
                var dot = ConvertJinsu(sung).ToList();

                for (int i = 0; i < 6; i++)
                {
                    if (i < dot.Count)
                    {
                        lastlvi.SubItems.Add(dot[i].ToString());
                    }
                    else
                    {
                        lastlvi.SubItems.Add("");
                    }
                }

                lastlvi.SubItems.Add("출률");
                lastlvi.SubItems.Add(cnt.ToString());

                for (int i = 0; i < 5; i++)
                {
                    lastlvi.SubItems.Add("");
                }

                ViewListView.Items.Add(lastlvi);

                ViewListView.EnsureVisible(100);
            }
            else if (ColumnComboBox.SelectedIndex == 6)
            {
                BeforeNumericUpDown.Minimum = 1;
                BeforeNumericUpDown.Maximum = 50;
                ViewListView.Clear();

                ViewListView.Columns.Add("회 차", 50, HorizontalAlignment.Center);
                ViewListView.Columns.Add(" 해당번호 ", 300, HorizontalAlignment.Center);
                ViewListView.Columns.Add("회 차", 50, HorizontalAlignment.Center);

                for (int i = 1; i <= 6; i++)
                {
                    ViewListView.Columns.Add(i + "공", 40, HorizontalAlignment.Center);
                }

                //전체가 아니라 100 구간만 출력
                int bfr = (int)BeforeNumericUpDown.Value;
                int start = _lastOrder - 99;
                int cnt = 0;
                int lastod = 0;

                for (int i = start; i <= _lastOrder; i++)
                {
                    int v = i - bfr;
                    lastod = v;
                    var lvi = new ListViewItem(v.ToString()) { UseItemStyleForSubItems = false };
                    var sun = Utility.SunDangOfOrder(v);
                    var dots = ConvertDotsu(sun);
                    lvi.SubItems.Add(string.Join(", ", dots.Select(x => x.ToString("00"))));
                    lvi.SubItems.Add(i.ToString());

                    var dan = Utility.DangbeonOfOrder(i).ToArray();
                    cnt += (!dan.Intersect(dots).Any()) ? 0 : 1;

                    for (int k = 0; k < 6; k++)
                    {
                        int item = dan[k];
                        lvi.SubItems.Add(item.ToString());

                        if (dots.Contains(item))
                        {
                            lvi.SubItems[3 + k].BackColor = Color.Cyan;
                        }
                    }

                    ViewListView.Items.Add(lvi);
                }

                var lastlvi = new ListViewItem((lastod + 1).ToString());
                var sung = Utility.SunDangOfOrder(lastod + 1);
                var dot = ConvertDotsu(sung);
                string s2 = string.Join(", ", dot.Select(x => x.ToString("00"))) + "   출률:" + cnt + "%";
                lastlvi.SubItems.Add(s2);
                lastlvi.SubItems.Add("");

                for (int i = 0; i < 6; i++)
                {
                    lastlvi.SubItems.Add("");
                }

                ViewListView.Items.Add(lastlvi);

                ViewListView.EnsureVisible(100);
            }
            else
            {
                BeforeNumericUpDown.Minimum = 0;
                BeforeNumericUpDown.Maximum = 90;
            }
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DownTextBox.Text))
            {
                int sel = ColumnComboBox.SelectedIndex;

                try
                {
                    string filename = (sel == 8) ? Application.StartupPath + @"\DataFiles\sanggeuk.dat" : 
                                                   Application.StartupPath + @"\DataFiles\chuldot.dat";

                    File.WriteAllText(filename, DownTextBox.Text);
                    MessageBox.Show("파일 작성됨.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }




        //********************  메서드  **************************



        /// <summary>
        /// 추첨일 악번검사
        /// </summary>
        private void CheckWinningDay()
        {
            ViewListView.Columns.Add("회차", 50, HorizontalAlignment.Center);

            for (int i = 1; i <= 6; i++)
            {
                string s = i + "구";
                ViewListView.Columns.Add(s, 50, HorizontalAlignment.Center);
            }

            int before = (int)BeforeNumericUpDown.Value;
            int lastday = LuckyLibrary.DateOfOrder(_lastOrder + 1 - before).Day;

            //같은 날 회차찾기
            List<int> sameOrders;
            using (var db = new LottoDBContext())
            {
                sameOrders = db.BasicTbl.Where(x => x.PobDate.Day == lastday)
                               .Select(x => x.Orders).ToList();

            }

            var dangs = new List<int>();

            foreach (var order in sameOrders)
            {
                var lvi = new ListViewItem(order.ToString());
                var dang = Utility.DangbeonOfOrder(order);
                dangs.AddRange(dang);

                foreach (var n in dang)
                {
                    lvi.SubItems.Add(n.ToString());
                }

                ViewListView.Items.Add(lvi);
            }

            ViewListView.EnsureVisible(sameOrders.Count - 1);

            //갯수 파악
            var dic = new Dictionary<int, int>();

            for (int i = 1; i <= 45; i++)
            {
                int n = dangs.Count(x => x == i);
                dic.Add(i, n);
            }

            var vals = dic.Values.Distinct().OrderBy(x => x);
            var lists = new List<string>();

            foreach (var val in vals)
            {
                var list = dic.Where(x => x.Value == val).Select(x => x.Key);
                string s1 = "[" + val.ToString("00") + "]: " + string.Join(",", list.OrderBy(x => x).Select(x => x.ToString("00")));
                lists.Add(s1);
            }

            UpperTextBox.Text = string.Join(Environment.NewLine, lists);
        }

        /// <summary>
        /// 당번주변수 검사
        /// </summary>
        private void CheckAround()
        {
            ViewListView.Columns.Add("회차", 45, HorizontalAlignment.Center);

            for (int i = 1; i <= 45; i++)
            {
                string s = "c" + i;
                ViewListView.Columns.Add(s, 35, HorizontalAlignment.Center);
            }

            int sec = (int)SectionNumericUpDown.Value;
            int start = _lastOrder - sec + 1;
            var allLists = _allPairs.Where(x => x.Key >= start).ToDictionary(x => x.Key, x => x.Value);
            var lists = new List<List<int>>();

            foreach (var key in allLists.Keys)
            {
                var data = allLists[key];
                var lvi = new ListViewItem(key.ToString());
                var lst = new List<int>();

                for (int i = 1; i <= 45; i++)
                {
                    var around = JubeonsuInts(i);
                    int n = data.Intersect(around).Count();
                    lst.Add(n);
                    lvi.SubItems.Add(n.ToString());
                }

                ViewListView.Items.Add(lvi);
                lists.Add(lst);
            }

            ViewListView.EnsureVisible(sec - 1);

            var rstlist = new List<(int, int, int)>();
            var good = new List<(int, int, int)>();
            var bad = new List<(int, int, int)>();

            //pivot
            for (int i = 0; i < 45; i++)
            {
                var colist = lists.Select(x => x.ElementAt(i));

                var (realContinue, maxContinue) = NextReal.RealMaxCount(colist);

                if (realContinue >= maxContinue)
                {
                    rstlist.Add((i + 1, colist.Last(), realContinue));
                }

                //0출과 2,3 출일때
                if (colist.Last() == 0)
                {
                    good.Add((i + 1, 0, realContinue));
                }

                if (colist.Last() >= 2)
                {
                    bad.Add((i + 1, colist.Last(), realContinue));
                }
            }

            int maxgood = good.Max(x => x.Item3);
            int maxbad = bad.Max(x => x.Item2);
            var ordgood = good.Where(x => x.Item3 == maxgood).Select(x => x);
            var ordbad = bad.Where(x => x.Item2 == maxbad).Select(x => x);

            var goodlist = rstlist.Where(x => x.Item2 == 0).Select(x => x.Item1);
            var badlist = rstlist.Where(x => x.Item2 != 0).Select(x => x.Item1);

            StringBuilder sb = new();

            if (goodlist.Any())
            {
                foreach (var item in goodlist)
                {
                    var dt = JubeonsuInts(item).OrderBy(x => x).Select(x => x.ToString("00"));
                    sb.AppendLine("호번:  " + item.ToString("00") + ": " + string.Join(",", dt));
                }
            }
            else
            {
                sb.AppendLine("호번:  ");
            }

            if (badlist.Any())
            {
                foreach (var item in badlist)
                {
                    var dt = JubeonsuInts(item).OrderBy(x => x).Select(x => x.ToString("00"));
                    sb.AppendLine("악번:  " + item.ToString("00") + ": " + string.Join(",", dt));
                }
            }
            else
            {
                sb.AppendLine("악번:  ");
            }

            sb.AppendLine("\r\n");

            if (ordgood.Any())
            {
                foreach (var ord in ordgood)
                {
                    var dt = JubeonsuInts(ord.Item1).OrderBy(x => x).Select(x => x.ToString("00"));
                    sb.AppendLine("0출: " + ord.Item3 + "개: " + ord.Item1.ToString("00") + ": " + string.Join(",", dt));
                }
            }
            else
            {
                sb.AppendLine("무출: ");
            }

            sb.AppendLine();

            if (ordbad.Any())
            {
                foreach (var ord in ordbad)
                {
                    var dt = JubeonsuInts(ord.Item1).OrderBy(x => x).Select(x => x.ToString("00"));
                    sb.AppendLine(ord.Item2 + "출: " + ord.Item3 + "개: " + ord.Item1.ToString("00") + ": " + string.Join(",", dt));
                }
            }
            else
            {
                sb.AppendLine("유출: ");
            }

            UpperTextBox.Text = sb.ToString();
        }

        /// <summary>
        /// 10주 구간 검사
        /// </summary>
        private void CheckTenInterval()
        {
            ViewListView.Columns.Add("회 차", 50, HorizontalAlignment.Center);

            for (int i = 0; i < 4; i++)
            {
                string s1 = i + "출";
                ViewListView.Columns.Add(s1, 50, HorizontalAlignment.Center);
            }

            ViewListView.Columns.Add("4출이상", 60, HorizontalAlignment.Center);

            int before = (int)BeforeNumericUpDown.Value;
            int loop = 300;
            int start = _lastOrder - loop + 1 - before;

            var lists = new List<int[]>();

            for (int i = start; i <= _lastOrder; i++)
            {
                var dang = _allPairs[i];
                var sedic = TenWeekData(i);
                var lvi = new ListViewItem(i.ToString());
                var array = new int[5];

                for (int j = 0; j < array.Length; j++)
                {
                    int n = sedic.ElementAt(j).Value.Intersect(dang).Count();
                    lvi.SubItems.Add(n.ToString());
                    array[j] = n;
                }

                lists.Add(array);
                ViewListView.Items.Add(lvi);
            }

            ViewListView.EnsureVisible(loop - 1);

            var lastdata = TenWeekData(_lastOrder + 1);
            var sb = new StringBuilder();

            //pivot
            for (int i = 0; i < lists[0].Length; i++)
            {
                var col = lists.Select(x => x.ElementAt(i)).ToList();
                var (real, max, nexts) = NextReal.RealMaxNextList(col);
                var (nextreal, nextmax) = NextReal.RealMaxCount(nexts);

                if (real >= max || (nexts.Count > 20 && nextreal >= nextmax))
                {
                    if (col.Last() == 0)
                    {
                        sb.AppendLine("호번:  " + i + "출 " + real + "개 " + col.Last() + "/" + nexts.Last());
                    }
                    else
                    {
                        if (nexts.Last() <= nexts.Max())
                        {
                            sb.AppendLine("악번:  " + i + "출 " + real + "개 " + col.Last() + "/" + nexts.Last());
                        }
                    }
                }
            }

            sb.AppendLine();

            foreach (var key in lastdata.Keys)
            {
                var dt = lastdata[key].OrderBy(x => x).Select(x => x.ToString("00"));
                sb.AppendLine(key + "출:   " + string.Join(",", dt));
            }

            UpperTextBox.Text = sb.ToString();
        }

        /// <summary>
        /// 이월당번 악번검사
        /// </summary>
        private void CheckCarryOver()
        {
            try
            {
                var pairs = new Dictionary<int, int[]>();
                using (var db = new LottoDBContext())
                {
                    pairs = db.BasicTbl.ToDictionary(x => x.Orders, x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }.ToArray());
                }

                var datas = pairs.Values.ToList();
                var rst = new List<string>() { "주기검사:" };

                //주기검사
                for (int i = 2; i <= 100; i++)
                {
                    var ors = new List<int>();
                    for (int j = datas.Count + 1; j >= 1; j -= i)
                    {
                        ors.Add(j);
                    }
                    ors.RemoveAt(0);
                    if (ors.Any() && ors.Count >= pairs.Count * 0.01)
                    {
                        var jukis = ors.Select(x => pairs[x]).ToList();
                        var zips = jukis.Zip(jukis.Skip(1), (a, b) => a.Intersect(b).Count());

                        if (zips.Max() < 2)
                        {
                            rst.Add(LuckyLibrary.ListToString(jukis.First()));
                        }
                        else
                        {
                            if (zips.First() > 2)
                            {
                                var (realCount, maxCount) = Utility.RealMaxCount(zips, false);
                                if (realCount >= maxCount)
                                {
                                    rst.Add(LuckyLibrary.ListToString(jukis.First()));
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                rst.Add("\r\n\r\n회기검사:");

                //회기검사 악번 찾기(이월수 악번)
                for (int i = 1; i <= 200; i++)
                {
                    var before = datas.AsEnumerable().Reverse().Skip(i);
                    var after = datas.AsEnumerable().Reverse();
                    var zips = before.Zip(after, (a, b) => a.Intersect(b).Count());
                    int first = zips.First();
                    if (zips.Max() <= 2)
                    {
                        rst.Add(LuckyLibrary.ListToString(before.First()));
                    }
                    else
                    {
                        if (first >= 2)
                        {
                            var (realCount, maxCount) = Utility.RealMaxCount(zips, false);
                            if (realCount >= maxCount)
                            {
                                rst.Add(LuckyLibrary.ListToString(before.First()));
                            }
                        }
                    }
                }
                
                UpperTextBox.Text = string.Join(Environment.NewLine, rst);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 당번다음 악번검사
        /// </summary>
        private void CheckDangNext()
        {
            var allpairs = new Dictionary<int, int[]>();
            using (var db = new LottoDBContext())
            {
                allpairs = db.BasicTbl.ToDictionary(x => x.Orders, x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }.ToArray());
            }

            ViewListView.Columns.Add("회차", 80, HorizontalAlignment.Left);

            for (int i = 1; i <= 6; i++)
            {
                ViewListView.Columns.Add(i + "구", 60, HorizontalAlignment.Center);
            }

            int start = _lastOrder - 50 + 1;
            for (int i = start; i <= _lastOrder; i++)
            {
                var lvi = new ListViewItem(i.ToString()) { UseItemStyleForSubItems = false };
                int[] nums = allpairs[i];
                for (int j = 0; j < 6; j++)
                {
                    lvi.SubItems.Add(nums[j].ToString());

                    if (i == _lastOrder)
                    {
                        lvi.BackColor = Color.Cyan;
                        lvi.SubItems[j + 1].BackColor = Color.Cyan;
                    }
                }

                ViewListView.Items.Add(lvi);
            }

            ViewListView.EnsureVisible(50 - 1);

            //당번 조합검사는 2조합만 한다
            var johaps = Utility.GetCombinations(Enumerable.Range(0, 6), 2).Select(x => x.ToArray()).ToList();
            var datas = allpairs.Values.ToList();
            var lastchul = datas.Last();
            var rst = new List<string>();

            foreach (int[] johap in johaps)
            {
                int[] chul = johap.Select(x => lastchul[x]).ToArray();

                var indexs = datas.Select((val, idx) => new { val, idx })
                                .Where(x => x.val.Intersect(chul).Count() == chul.Length).Select(x => x.idx + 1)
                                .Where(x => x < datas.Count).Select(x => datas[x]).ToList();

                var pairs = Utility.CountOfNumber(indexs);

                //최소출인 번호찾기
                var orded = pairs.Where(x => x.cnt <= 0).Select(x => x.ord).OrderBy(x => x).Select(x => x.ToString("00"));

                string s1 = "[" + string.Join(",", chul.Select(x => x.ToString("00"))) + "]  " +
                            string.Join(",", orded);
                rst.Add(s1);
            }

            DownTextBox.Text = string.Join(Environment.NewLine, rst);
        }

        /// <summary>
        /// 진수로 호악번 찾기
        /// </summary>
        private void CheckJinsu()
        {
            var allPairs = new Dictionary<int, int[]>();
            //전체순서당번
            using (var db = new LottoDBContext())
            {
                allPairs = db.BasicTbl.ToDictionary(x => x.Orders, x =>
                    new List<int> { x.SunGu1, x.SunGu2, x.SunGu3, x.SunGu4, x.SunGu5, x.SunGu6 }.ToArray());
            }

            var datas = allPairs.Values.ToList();
            var good = new List<(int, int, int)>();
            var bad = new List<(int, int, int)>();

            //자체부터 50간격 루프
            for (int i = 0; i <= 50; i++)
            {
                var zip = datas.Zip(datas.Skip(i), (before, now) => ConvertJinsu(before).Intersect(now).Count());
                int last = zip.Last();

                //호번찾기
                if (last == 0)
                {
                    var (realCount, maxCount) = NextReal.RealMaxCount(zip, Kiho.Gatum, 0);

                    if (realCount >= maxCount)
                    {
                        good.Add((i, realCount, maxCount));
                    }
                    else if (realCount >= 3)
                    {
                        good.Add((i, realCount, maxCount));
                    }
                }
                else
                {
                    var (realCount, maxCount) = NextReal.RealMaxCount(zip, Kiho.Darum, 0);

                    if (realCount >= maxCount)
                    {
                        bad.Add((i, realCount, maxCount));
                    }
                    else if (realCount >= 3)
                    {
                        bad.Add((i, realCount, maxCount));
                    }
                }
            }

            //정렬
            var ordgod = good.OrderByDescending(x => x.Item2).ThenBy(x => x.Item3).Take(5);
            var ordbad = bad.OrderByDescending(x => x.Item2).ThenBy(x => x.Item3).Take(5);

            string s = "진수호번 (간격/후방연속/연속최대)\r\n";

            if (ordgod.Any())
            {
                //결과 출력
                foreach (var item in ordgod)
                {
                    int gap = item.Item1;
                    int order = _lastOrder - gap + 1;

                    if (order <= _lastOrder)
                    {
                        var dang = allPairs[order];
                        var jins = ConvertJinsu(dang);
                        string s1 = item.Item1 + "/" + item.Item2 + "/" + item.Item3 +
                                    " 번호: " + string.Join(",", jins.OrderBy(x => x).Select(x => x.ToString("00")));
                        s += s1 + "\r\n";
                    }
                }

                int idx = ordgod.First().Item1;
                BeforeNumericUpDown.Value = idx;
            }
            else
            {
                BeforeNumericUpDown.Value = 10;
            }

            s += "\r\n진수악번\r\n";

            if (ordbad.Any())
            {
                foreach (var item in ordbad)
                {
                    int gap = item.Item1;
                    int order = _lastOrder - gap + 1;

                    if (order <= _lastOrder)
                    {
                        var dang = allPairs[order];
                        var jins = ConvertJinsu(dang);
                        string s1 = item.Item1 + "/" + item.Item2 + "/" + item.Item3 +
                                    " 번호: " + string.Join(",", jins.OrderBy(x => x).Select(x => x.ToString("00")));
                        s += s1 + "\r\n";
                    }
                }
            }

            UpperTextBox.Text = s;
        }

        /// <summary>
        /// 도트방식으로 호악번 찾기
        /// </summary>
        private void CheckDangbeonSanchul()
        {
            var allPairs = new Dictionary<int, int[]>();

            //전체순서당번
            using (var db = new LottoDBContext())
            {
                allPairs = db.BasicTbl.ToDictionary(x => x.Orders, x =>
                    new List<int> { x.SunGu1, x.SunGu2, x.SunGu3, x.SunGu4, x.SunGu5, x.SunGu6 }.ToArray());
            }

            var datas = allPairs.Values.ToList();
            var good = new List<(int, int, int)>();
            var bad = new List<(int, int, int)>();

            //자체부터 50간격 루프
            for (int i = 0; i <= 50; i++)
            {
                var zip = datas.Zip(datas.Skip(i), (before, now) => ConvertDotsu(before).Intersect(now).Count());
                int last = zip.Last();

                //호번찾기
                if (last == 0)
                {
                    var (realCount, maxCount) = NextReal.RealMaxCount(zip, Kiho.Gatum, 0);

                    if (realCount >= maxCount)
                    {
                        good.Add((i, realCount, maxCount));
                    }
                    else if (realCount >= 3)
                    {
                        good.Add((i, realCount, maxCount));
                    }
                }
                else
                {
                    var (realCount, maxCount) = NextReal.RealMaxCount(zip, Kiho.Darum, 0);

                    if (realCount >= maxCount)
                    {
                        bad.Add((i, realCount, maxCount));
                    }
                    else if (realCount >= 3)
                    {
                        bad.Add((i, realCount, maxCount));
                    }
                }
            }

            //정렬
            var ordgod = good.OrderByDescending(x => x.Item2).ThenBy(x => x.Item3).Take(5);
            var ordbad = bad.OrderByDescending(x => x.Item2).ThenBy(x => x.Item3).Take(5);

            string s = "도트호번 (간격/후방연속/연속최대)\r\n";

            if (ordgod.Any())
            {
                //결과 출력
                foreach (var item in ordgod)
                {
                    int gap = item.Item1;
                    int order = _lastOrder - gap + 1;
                    var dang = allPairs[order];
                    var jins = ConvertDotsu(dang);
                    string s1 = item.Item1 + "/" + item.Item2 + "/" + item.Item3 +
                                " 번호: " + string.Join(",", jins.OrderBy(x => x).Select(x => x.ToString("00")));
                    s += s1 + "\r\n";
                }

                int idx = ordgod.First().Item1;
                BeforeNumericUpDown.Value = idx;
            }
            else
            {
                BeforeNumericUpDown.Value = 10;
            }

            s += "\r\n도트악번\r\n";

            if (ordbad.Any())
            {
                foreach (var item in ordbad)
                {
                    int gap = item.Item1;
                    int order = _lastOrder - gap + 1;
                    if (order < _lastOrder)
                    {
                        var dang = allPairs[order];
                        var jins = ConvertDotsu(dang);
                        string s1 = item.Item1 + "/" + item.Item2 + "/" + item.Item3 +
                                    " 번호: " + string.Join(",", jins.OrderBy(x => x).Select(x => x.ToString("00")));
                        s += s1 + "\r\n";
                    }
                }
            }

            UpperTextBox.Text = s;
        }

        /// <summary>
        /// 산출식 호악번 찾기
        /// </summary>
        private void CheckFormula()
        {
            string s = SequenceFormulaDatas(3, out List<int> seqlist) + "\r\n";
            s += NextJohapFormulaDatas(4, out List<int> nxtlist);

            seqlist.AddRange(nxtlist);
            var ordlist = seqlist.OrderBy(x => x).Select(x => x.ToString("00"));
            UpperTextBox.Text = s + "\r\n" + string.Join(",", ordlist);
        }

        /// <summary>
        /// 상생수, 상극수 검사
        /// </summary>
        private void CheckTogether()
        {
            ViewListView.Columns.Add("번호", 40, HorizontalAlignment.Left);
            ViewListView.Columns.Add("번호", 500, HorizontalAlignment.Left);
            ViewListView.Columns.Add("번호", 500, HorizontalAlignment.Left);

            int sec = (int)SectionNumericUpDown.Value;
            int start = _lastOrder - sec + 1;
            var lastdatas = Utility.DangbeonOfOrder(_lastOrder);
            var badLists = new List<int[]>();
            var godLists = new List<int[]>();
            var pairs = NextShownLists(start);

            foreach (var key in pairs.Keys)
            {
                var lvi = new ListViewItem(key.ToString()) { UseItemStyleForSubItems = false };
                var data = pairs[key];

                //고유의 갯수
                var dict = data.Select(x => x.Item2).Distinct();

                //하위 5개
                var lowcnt = dict.Take(5);

                var badList = new List<int[]>();
                var badStrings = new List<string>();

                foreach (var n in lowcnt)
                {
                    var arr = data.Where(x => x.Item2 == n).Select(x => x.Item1).OrderBy(x => x).ToArray();
                    string s1 = "(" + string.Join(",", arr.Select(x => x.ToString("00"))) + ")" + n;
                    badStrings.Add(s1);
                    badList.Add(arr);
                }

                lvi.SubItems.Add(string.Join(",", badStrings));

                if (lastdatas.Contains(key))
                {
                    lvi.BackColor = Color.Cyan;
                    lvi.SubItems[1].BackColor = Color.LightCoral;

                    //최소출 번호만 리스트에 더한다
                    badLists.Add(badList.First());
                }

                //상위 5개
                var hghcnt = dict.Reverse().Take(5);

                var godList = new List<int[]>();
                var godStrings = new List<string>();

                foreach (var n in hghcnt)
                {
                    var arr = data.Where(x => x.Item2 == n).Select(x => x.Item1).ToArray();
                    string s1 = "(" + string.Join(",", arr.Select(x => x.ToString("00"))) + ")" + n;
                    godStrings.Add(s1);
                    godList.Add(arr);
                }

                lvi.SubItems.Add(string.Join(",", godStrings));

                if (lastdatas.Contains(key))
                {
                    lvi.SubItems[2].BackColor = Color.Cyan;

                    //최대출 번호만 리스트에 더한다
                    godLists.Add(godList.First());
                }

                ViewListView.Items.Add(lvi);
            }

            var badtmp = badLists.SelectMany(x => x).OrderBy(x => x);
            var godtmp = godLists.SelectMany(x => x).OrderBy(x => x);

            var sb = new StringBuilder();
            sb.AppendLine("상극수");
            sb.AppendLine(string.Join(",", badtmp.Select(x => x.ToString("00"))));
            sb.AppendLine("\r\n상생수");
            sb.AppendLine(string.Join(",", godtmp.Select(x => x.ToString("00"))));

            UpperTextBox.Text = sb.ToString();
            DownTextBox.Text = NowHatePair(start);
        }

        /// <summary>
        /// 이월,동간격,연속,연끝 검사
        /// </summary>
        private void CheckCarriedForward()
        {
            ViewListView.Columns.Add("회차", 50, HorizontalAlignment.Left);
            ViewListView.Columns.Add("이월수", 70, HorizontalAlignment.Left);
            ViewListView.Columns.Add("동간격", 70, HorizontalAlignment.Left);
            ViewListView.Columns.Add("연속수", 150, HorizontalAlignment.Left);
            ViewListView.Columns.Add("연끝수", 150, HorizontalAlignment.Left);

            var tuples = new List<(int[] johapIndex, int[] culusu)>();
            int sec = (int)SectionNumericUpDown.Value;
            int start = _lastOrder - sec + 1;
            var keyValuePairs = GetEtcDatas();
            string[] coma = { "," };
            var cntdic = new Dictionary<int, int[]>();

            //리스트뷰 출력용
            var overdic = keyValuePairs.Where(x => x.Key >= start).ToDictionary(x => x.Key, x => x.Value);

            #region 리스트뷰에 출력

            ViewListView.BeginUpdate();

            foreach (var key in overdic.Keys)
            {
                var lvi = new ListViewItem(key.ToString());
                var items = overdic[key];
                var temp = new List<int>();

                foreach (var item in items)
                {
                    lvi.SubItems.Add(item);
                    int lgh = string.IsNullOrEmpty(item) ? 0 : item.Split(coma, StringSplitOptions.RemoveEmptyEntries).Length;
                    temp.Add(lgh);
                }

                ViewListView.Items.Add(lvi);
                cntdic.Add(key, temp.ToArray());
            }

            ViewListView.EndUpdate();
            ViewListView.EnsureVisible(sec - 1);

            #endregion

            var secPairs = cntdic.Values.ToList();
            int[] lastdata = secPairs.Last();
            var sequence = Enumerable.Range(0, 4);
            var johaps = new List<int[]>();

            for (int i = 1; i <= 4; i++)
            {
                var johap = Utility.GetCombinations(sequence, i).Select(x => x.ToArray()).ToList();
                johaps.AddRange(johap);
            }

            //검사시작
            foreach (int[] johap in johaps)
            {
                var lastdt = CombinationOfArray(johap, lastdata);
                var (realCount, maxCount, nextLists) = NextReal.RealMaxNextLists(secPairs, johap);

                //후방연속이 연속최대보다 크면
                if (realCount <= 3 && maxCount > 0 && realCount >= maxCount)
                {
                    if (tuples.Count == 0)
                    {
                        tuples.Add((johap, lastdt));
                    }
                    else
                    {
                        bool pass = IsContainsOfIndex((johap, lastdt), tuples);

                        if (pass)
                        {
                            tuples.Add((johap, lastdt));
                        }
                    }
                }
                else if (realCount <= 3 && maxCount > 0)
                {
                    double hangey = _lastOrder * 0.25;

                    //다음출에서 최종출수가 나온적이 없으면
                    if (nextLists.Count > hangey)
                    {

                        var nextlastdt = nextLists.Last();
                        var (real, max) = NextReal.RealMaxCount(nextLists);

                        if (real >= max)
                        {
                            if (tuples.Count == 0)
                            {
                                tuples.Add((johap, nextlastdt));
                            }
                            else
                            {
                                bool pass = IsContainsOfIndex((johap, nextlastdt), tuples);

                                if (pass)
                                {
                                    tuples.Add((johap, nextlastdt));
                                }
                            }
                        }
                    }
                }
            }

            //결과의 인덱스를 문장으로 변경
            string[] names = { "ihweol", "donggap", "yeonsok", "yeonkkeut" };
            var tmp = new List<string>();

            foreach (var (johapIndex, culusu) in tuples)
            {
                var imsi = new List<string>();

                for (int i = 0; i < johapIndex.Length; i++)
                {
                    string s1 = names[johapIndex[i]] + " = " + culusu[i];
                    imsi.Add(s1);
                }

                string s2 = "!(" + string.Join(" & ", imsi) + ")";
                tmp.Add(s2);
            }

            DownTextBox.Text = string.Join(Environment.NewLine, tmp);
        }


        //*********************  메서드 끝  *********************
        //*********************  메서드 내부메서드  *********************

        /// <summary>
        /// 최종당번 3조합으로 산출식검사 결과를 문자열로 반환
        /// </summary>
        /// <param name="takeCount">후방검사 갯수</param>
        /// <returns></returns>
        private string NextJohapFormulaDatas(int takeCount, out List<int> numbers)
        {
            var lastord = _allPairs.Last().Key;
            var lastdts = _allPairs.Last().Value;
            var arrs = new List<int[]>();
            var adds = new List<int>();

            //1 - 3 조합하기
            for (int i = 1; i <= 3; i++)
            {
                var imsi = Utility.GetCombinations(lastdts, i).Select(x => x.ToArray()).ToList();
                arrs.AddRange(imsi);
            }

            var finds = new List<(int[], int[], List<int[]>)>();

            foreach (int[] arr in arrs)
            {
                var find = _allPairs.Where(x => x.Key < lastord && x.Value.Intersect(arr).Count() == arr.Length)
                                   .Select(x => x.Key);

                if (find.Count() >= takeCount)
                {
                    var skips = find.Skip(find.Count() - takeCount).ToArray();
                    var ords = skips.Select(x => x + 1).ToArray();
                    var dangs = ords.Select(x => _allPairs[x].ToArray()).ToList();

                    finds.Add((arr, ords, dangs));
                }
            }

            string s = string.Empty;

            foreach (var item in finds)
            {
                var nums = item.Item1;
                var ords = item.Item2;
                var dangs = item.Item3;

                var tpl1 = FindAddDatas(dangs);
                var tpl2 = FindIncreaseDatas(dangs);
                var tpl3 = FindSubtractionDatas(dangs);

                if (tpl1.Any() || tpl2.Any() || tpl3.Any())
                {
                    string s1 = "(" + string.Join(",", nums) + ")\t" + string.Join(",", ords) + "\t";

                    if (tpl1.Any())
                    {
                        var tmp = tpl1.Select(x => x.number);
                        adds.AddRange(tmp);
                        s1 += "더하기: ";

                        foreach (var (number, basenum, plus) in tpl1)
                        {
                            s1 += number + "/" + basenum + "/" + plus + "\t";
                        }
                    }

                    if (tpl2.Any())
                    {
                        var tmp = tpl2.Select(x => x.number);
                        adds.AddRange(tmp);
                        s1 += "증 가: ";

                        foreach (var (number, basenum, plus) in tpl2)
                        {
                            s1 += number + "/" + basenum + "/" + plus + "\t";
                        }
                    }

                    if (tpl3.Any())
                    {
                        var tmp = tpl3.Select(x => x.number);
                        adds.AddRange(tmp);
                        s1 += "빼 기: ";

                        foreach (var (number, basenum, plus) in tpl3)
                        {
                            s1 += number + "/" + basenum + "/" + plus + "\t";
                        }
                    }

                    s += s1 + "\r\n";
                }
            }

            numbers = adds;
            return s;
        }

        /// <summary>
        /// 최종 구간의 산출식검사 결과를 문자열로 반환
        /// </summary>
        /// <param name="takeCount"></param>
        /// <returns></returns>
        private string SequenceFormulaDatas(int takeCount, out List<int> numbers)
        {
            var takes = _allPairs.Skip(_allPairs.Count - takeCount);
            var ords = takes.Select(x => x.Key).ToArray();
            var dangs = takes.Select(x => x.Value.ToArray()).ToList();
            var adds = new List<int>();

            //검사시작
            var tpl1 = FindAddDatas(dangs);
            var tpl2 = FindIncreaseDatas(dangs);
            var tpl3 = FindSubtractionDatas(dangs);

            string s = string.Empty;

            if (tpl1.Any() || tpl2.Any() || tpl3.Any())
            {
                string s1 = string.Join(",", ords) + "\t";

                if (tpl1.Any())
                {
                    var tmp = tpl1.Select(x => x.number);
                    adds.AddRange(tmp);
                    s1 += "더하기: ";

                    foreach (var (number, basenum, plus) in tpl1)
                    {
                        s1 += number + "/" + basenum + "/" + plus + "\t";
                    }
                }

                if (tpl2.Any())
                {
                    var tmp = tpl2.Select(x => x.number);
                    adds.AddRange(tmp);
                    s1 += "증 가: ";

                    foreach (var (number, basenum, plus) in tpl2)
                    {
                        s1 += number + "/" + basenum + "/" + plus + "\t";
                    }
                }

                if (tpl3.Any())
                {
                    var tmp = tpl3.Select(x => x.number);
                    adds.AddRange(tmp);
                    s1 += "빼 기: ";

                    foreach (var (number, basenum, plus) in tpl3)
                    {
                        s1 += number + "/" + basenum + "/" + plus + "\t";
                    }
                }

                s += s1 + "\r\n";
            }

            numbers = adds;
            return s;
        }

        /// <summary>
        /// 행의 데이터에서 조합 인덱스의 요소를 반환
        /// </summary>
        /// <param name="johap">인덱스 조합배열</param>
        /// <param name="list">검사할 리스트</param>
        /// <returns>요소값 배열</returns>
        private static int[] CombinationOfArray(int[] johap, IEnumerable<int> list)
        {
            var arr = johap.Select(x => list.ToArray()[x]).ToArray();
            return arr;
        }

        /// <summary>
        /// 자신의 뺀 주변수의 데이터
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static int[] JubeonsuInts(int number)
        {
            var array = new[] { -8, -7, -6, -1, 1, 6, 7, 8 };
            var lst = new List<int>();

            foreach (int i in array)
            {
                lst.Add(Utility.ZeroToInt(i + number));
            }

            return lst.ToArray();
        }

        /// <summary>
        /// 10구간의 출수를 반환
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private Dictionary<int, int[]> TenWeekData(int order)
        {
            var dic = new Dictionary<int, int[]>();
            int start = order - 10;
            var section = new List<int>();
            var secdic = _allPairs.Where(x => x.Key >= start && x.Key < order).ToDictionary(x => x.Key, x => x.Value);

            foreach (var data in secdic.Values)
            {
                section.AddRange(data);
            }

            var shwdic = new Dictionary<int, int>();

            for (int i = 1; i <= 45; i++)
            {
                int n = section.Count(x => x == i);
                shwdic.Add(i, n);
            }

            //0출 ~ 4출 사이 루프
            for (int i = 0; i < 5; i++)
            {
                if (i < 4)
                {
                    var ea = shwdic.Where(x => x.Value == i).Select(x => x.Key).ToArray();
                    dic.Add(i, ea);
                }
                else
                {
                    var ea = shwdic.Where(x => x.Value >= i).Select(x => x.Key).ToArray();
                    dic.Add(i, ea);
                }
            }

            return dic;
        }

        /// <summary>
        /// 상극수 반환
        /// </summary>
        /// <param name="startOrder">시작회차</param>
        /// <returns>슬래쉬로 구분된 문자열</returns> 
        private string NowHatePair(int startOrder)
        {
            var bags = new ConcurrentBag<(int, int[])>();

            double limit = (double)LimitNumericUpDown.Value;
            var numbers = Enumerable.Range(1, 45);
            int loop = 0;

            Parallel.ForEach(numbers, number =>
            {
                var find = _allPairs.Where(x => x.Value.Contains(number) && x.Key >= startOrder)
                                    .SelectMany(x => x.Value);

                int val = Convert.ToInt32(find.Count() * limit);  //반올림정수
                var tpl = new List<(int, int)>();

                for (int i = 1; i <= 45; i++)
                {
                    int cnt = find.Count(x => x == i);

                    if (cnt < val)
                    {
                        tpl.Add((i, cnt));
                    }
                }

                if (tpl.Count > 0)
                {
                    //locking
                    Interlocked.Increment(ref loop);

                    int min = tpl.Min(x => x.Item2);
                    var array = tpl.Where(x => x.Item2 == min).Select(x => x.Item1).OrderBy(x => x).ToArray();

                    if (array.Length > 0)
                    {
                        if (bags.IsEmpty)
                        {
                            bags.Add((number, array));
                        }
                        else
                        {
                            //3-35 와 35 - 3 이 동일하므로 제외한다
                            bool isdup = bags.Any(x => array.Contains(x.Item1));

                            if (!isdup)
                            {
                                bags.Add((number, array));
                            }
                        }
                    }
                }
            });

            var sb = new StringBuilder();

            if (!bags.IsEmpty)
            {
                foreach (var bag in bags.OrderBy(x => x.Item1))
                {
                    int key = bag.Item1;
                    var each = bag.Item2.Select(x => x.ToString("00"));
                    string s = key.ToString("00") + "/" + string.Join(",", each);
                    sb.AppendLine(s);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 당번 전체 데이터
        /// </summary>
        /// <returns>회차, 당번 딕셔너리</returns>
        private async static Task<Dictionary<int, List<int>>> GetAllWinningNumber()
        {
            var dic = new Dictionary<int, List<int>>();

            var task = Task.Run(() =>
            {
                using var db = new LottoDBContext();
                dic = db.BasicTbl
                    .ToDictionary(x => x.Orders, x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 });
            });

            await task;
            return dic;
        }

        /// <summary>
        /// 번호출 다음 출수의 결과를 반환
        /// </summary>
        /// <param name="startOrder">시작회차</param>
        /// <returns>키:번호, 값:리스트(번호, 출수) 딕셔너리</returns>
        private Dictionary<int, List<(int, int)>> NextShownLists(int startOrder)
        {
            var dic = new Dictionary<int, List<(int, int)>>();
            ConcurrentBag<(int, List<(int, int)>)> bags = new();
            var numbers = Enumerable.Range(1, 45);
            int nocnt = 0;

            Parallel.ForEach(numbers, number =>
            {
                var find = _allPairs.Where(x => x.Value.Contains(number) && x.Key < _lastOrder && x.Key >= startOrder)
                                    .Select(x => x.Key + 1).SelectMany(x => _allPairs[x]);

                var list = new List<(int, int)>();

                for (int i = 1; i <= 45; i++)
                {
                    int n = find.Count(x => x == i);
                    list.Add((i, n));
                }

                var ordlst = list.OrderBy(x => x.Item2).ToList();

                Interlocked.Increment(ref nocnt);
                bags.Add((number, ordlst));
            });

            var orderdBag = bags.OrderBy(x => x.Item1);

            foreach (var bag in orderdBag)
            {
                dic.Add(bag.Item1, bag.Item2);
            }

            return dic;
        }

        /// <summary>
        /// Orders, Ihweol, Donggap, Yeonsok, Yeonkkeut의 전체 데이터
        /// </summary>
        /// <returns>회차, (이월수, 동간격수, 연속수, 연끝수)문자열 딕셔너리</returns>
        private static Dictionary<int, string[]> GetEtcDatas()
        {
            var dic = new Dictionary<int, string[]>();

            using (var db = new LottoDBContext())
            {
                dic = db.NonChulsuTbl.ToDictionary(x => x.Orders, x => new List<string>
                        { x.Ihweol.Trim(), x.Donggap.Trim(), x.Yeonsok.Trim(), x.Yeonkkeut.Trim() }.ToArray());
            }

            return dic;
        }

        /// <summary>
        /// 처음데이터에서 0 - 9 값을 더한것이 다음 회차에서도 동일한 것을 찾기
        /// 예: 5 + 2 = 7 + 2 = 9 + 2 = 11 최종값:11, 처믐번호:5, 증가값:2
        /// </summary>
        /// <param name="lists">당번배열 리스트</param>
        /// <returns>튜플(최종번호, 처음번호, 증가값)</returns>
        private static List<(int number, int basenum, int plus)> FindAddDatas(List<int[]> lists)
        {
            //앞의 2개만 추출해서 사용
            var realtwo = lists.Take(2);
            var btmdata = lists[0];
            var nowdata = lists[1];

            //앞 2개에서 맨처음번호에서 0-9를 더한것아 나온 리스트
            var find = new List<(int btmnum, int plus)>();

            //맨처음 데이터 루프
            foreach (int n in btmdata)
            {
                //각 번호에 0 ~ 9 를 더한다
                for (int k = 0; k <= 9; k++)
                {
                    int a = n + k;

                    //더한것이 다음에 포함되었다면 리스트에 추가
                    if (nowdata.Contains(a))
                    {
                        find.Add((n, k));
                    }
                }
            }

            var rst = new List<(int, int, int)>();

            if (find.Any())
            {
                //2개 비교해서 발견된 것 루프
                foreach (var (btmnum, plus) in find)
                {
                    int n = btmnum;

                    //맨처음은 이미 계산했기 때문에 건너뛴다.
                    var skips = lists.Skip(1).Select(x => x).ToList();
                    int error = 0;

                    foreach (int[] data in skips)
                    {
                        int v = Utility.ZeroToInt(n + plus);

                        if (data.Contains(v))
                        {
                            n = v;  //값 치환
                        }
                        else
                        {
                            error++;
                            break;
                        }
                    }

                    if (error == 0)
                    {
                        rst.Add((Utility.ZeroToInt(n + plus), btmnum, plus));
                    }
                }
            }

            return rst;
        }

        /// <summary>
        /// 처음데이터에서 0 - 9 값에  +1 한것아 다음 회차에서도 동일한것을 찾기
        /// 예: 5 + 1 = 6 + 2 = 8 + 3 = 11 최종값:11, 처믐번호:5, 증가초기값:1
        /// </summary>
        /// <param name="lists">당번배열 리스트</param>
        /// <returns>튜플(최종번호, 처음번호, 증가초기값)</returns>
        private static List<(int number, int basenum, int plus)> FindIncreaseDatas(List<int[]> lists)
        {
            var realtwo = lists.Take(2);
            var btmdata = lists[0];
            var nowdata = lists[1];

            var find = new List<(int btmnum, int plus)>();

            foreach (int n in btmdata)
            {
                //각 번호에 0 ~ 9 를 더한다
                for (int k = 0; k <= 9; k++)
                {
                    int a = n + k;

                    //더한것이 다음에 포함되었다면 리스트에 추가
                    if (nowdata.Contains(a))
                    {
                        find.Add((n, k));
                    }
                }
            }

            var rst = new List<(int, int, int)>();

            if (find.Any())
            {
                //2개 비교해서 발견된 것 루프
                foreach (var (btmnum, plus) in find)
                {
                    int n = btmnum;

                    var skips = lists.Skip(1).Select(x => x).ToList();
                    int error = 0;

                    for (int i = 0; i < skips.Count; i++)
                    {
                        int[] data = skips[i];

                        //1 씩 증가해서 검사
                        int v = Utility.ZeroToInt(n + plus + i);

                        if (data.Contains(v))
                        {
                            n = v;
                        }
                        else
                        {
                            error++;
                            break;
                        }
                    }

                    if (error == 0)
                    {
                        int rt = Utility.ZeroToInt(n + plus + skips.Count);
                        rst.Add((rt, btmnum, plus));
                    }
                }
            }

            return rst;
        }

        /// <summary>
        /// 처음데이터에서 1 - 9 값을 뺀것이 다음 회차에서도 동일한 것을 찾기
        /// 예: 25 - 2 = 23 - 2 = 21 - 2 = 19 최종값:19, 처믐번호:25, 감소값:2
        /// </summary>
        /// <param name="lists">당번배열 리스트</param>
        /// <returns>튜플(최종번호, 처음번호, 감소값)</returns>
        private static List<(int number, int basenum, int plus)> FindSubtractionDatas(List<int[]> lists)
        {
            var realtwo = lists.Take(2);
            var btmdata = lists[0];
            var nowdata = lists[1];

            var find = new List<(int btmnum, int minus)>();

            foreach (int n in btmdata)
            {
                //각 번호에 1 ~ 9 를 뺀다
                for (int k = 1; k <= 9; k++)
                {
                    int a = n - k;

                    //더한것이 다음에 포함되었다면 리스트에 추가
                    if (nowdata.Contains(a))
                    {
                        find.Add((n, k));
                    }
                }
            }

            var rst = new List<(int, int, int)>();

            if (find.Any())
            {
                //2개 비교해서 발견된 것 루프
                foreach (var (btmnum, minus) in find)
                {
                    int n = btmnum;

                    var skips = lists.Skip(1).Select(x => x).ToList();
                    int error = 0;

                    for (int i = 0; i < skips.Count; i++)
                    {
                        int[] data = skips[i];
                        int v = Utility.ZeroToInt(n - minus);

                        if (data.Contains(v))
                        {
                            n = v;
                        }
                        else
                        {
                            error++;
                            break;
                        }
                    }

                    if (error == 0)
                    {
                        rst.Add((Utility.ZeroToInt(n - minus), btmnum, minus));
                    }
                }
            }

            return rst;
        }

        /// <summary>
        /// 당번을 세로방향 진수로 바꾼 정수리스트
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static IEnumerable<int> ConvertJinsu(IEnumerable<int> list)
        {
            //정수를 6비트 이진문자열로 바꾸기
            var jinsus = list.Select(x => Convert.ToString(x, 2).PadLeft(6, '0'));

            //pivot
            for (int i = 0; i < 6; i++)
            {
                var ver = jinsus.Select(x => x.ElementAt(i)).ToArray();
                string s = new(ver);

                //이진문자열을 정수로 바꾸기
                int n = Convert.ToInt32(s, 2);

                if (n >= 1 && n <= 45)
                {
                    yield return n;
                }
            }
        }

        /// <summary>
        /// 당번을 두자리로 바꾼것을 각각 더한 리스트
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static IEnumerable<int> ConvertDotsu(IEnumerable<int> list)
        {
            string s = string.Empty;

            foreach (int n in list)
            {
                string s1 = n.ToString("00");
                s += s1;
            }

            var data = s.Select(x => int.Parse(x.ToString())).ToList();

            //먼저 앞의 2개를 더한다
            int sum = Utility.ZeroToInt(data[0] + data[1]);
            var rst = new List<int> { sum };

            for (int i = 2; i < data.Count - 1; i++)
            {
                int n = sum + data[i];
                sum = Utility.ZeroToInt(n);
                rst.Add(sum);
            }

            return rst.Distinct().OrderBy(x => x);
        }

        /// <summary>
        /// 결과에 이미 포함된 배열인지 여부
        /// </summary>
        /// <param name="source">(인덱스배열, 값배열)</param>
        /// <param name="target">검사통과한 (인덱스배열, 값배열) 리스트</param>
        /// <returns>리스트배열에 포함되지 않았으면 참</returns>
        private static bool IsContainsOfIndex((int[], int[]) source, IEnumerable<(int[], int[])> target)
        {
            bool pass = false;

            if (!target.Any())
            {
                pass = true;
            }
            else
            {
                int error = 0;

                foreach ((int[] idxs, int[] vals) in target)
                {
                    //두개의 길이가 같을때
                    if (idxs.Length == source.Item1.Length)
                    {
                        if (idxs.SequenceEqual(source.Item1) && vals.SequenceEqual(source.Item2))
                        {
                            error++;
                            break;
                        }
                    }
                    //두개의 길이가 다를때
                    else if (idxs.Length < source.Item1.Length)
                    {
                        var bases = Enumerable.Range(0, source.Item1.Length);
                        var johaps = Utility.GetCombinations(bases, idxs.Length);

                        foreach (var johap in johaps)
                        {
                            int[] idx = johap.Select(x => source.Item1[x]).ToArray();
                            int[] val = johap.Select(x => source.Item2[x]).ToArray();

                            if (idxs.SequenceEqual(idx) && vals.SequenceEqual(val))
                            {
                                error++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        var bases = Enumerable.Range(0, idxs.Length);
                        var johaps = Utility.GetCombinations(bases, source.Item1.Length);

                        foreach (var johap in johaps)
                        {
                            int[] idx = johap.Select(x => source.Item1[x]).ToArray();
                            int[] val = johap.Select(x => source.Item2[x]).ToArray();

                            if (idxs.SequenceEqual(idx) && vals.SequenceEqual(val))
                            {
                                error++;
                                break;
                            }
                        }
                    }
                }

                pass = (error == 0);
            }

            return pass;
        }

    }
}
