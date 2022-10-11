using System.Collections.Concurrent;
using System.Data;
using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 데이터 단순회귀 검사 폼 클래스
    /// </summary>
    public partial class LinearForm : Form
    {
        #region 필드

        private readonly int _lastOrder;
        private readonly string[] _columnNames;
        private readonly List<int[]> _items;
        private List<List<int[]>> _allNumbers;
        private List<int[]> _numbers;
        private List<int[]> _selectedDatas;
        private int _section = 5;
        private const int Loop = 20;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public LinearForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            _columnNames = new[]
            {
                "Number", "Beondae", "Slipsu", "Kkeutbeon",
                "RH03", "RV03", "RH05", "RV05", "RH07", "RV07", "RH09", "RV09", "RH15", "RV15",
                "Col03", "Col04", "Col05", "Col06", "Col07", "Col08", "Col09", "Col10", "Col11", "Col12", "Col15"
            };

            _items = new List<int[]>
            {
                Enumerable.Range(1, 45).ToArray(),
                Enumerable.Range(0, 5).ToArray(),
                Enumerable.Range(0, 6).ToArray(),
                Enumerable.Range(0, 10).ToArray(),

                Enumerable.Range(0, 3).ToArray(),
                Enumerable.Range(0, 3).ToArray(),
                Enumerable.Range(0, 5).ToArray(),
                Enumerable.Range(0, 5).ToArray(),
                Enumerable.Range(0, 7).ToArray(),
                Enumerable.Range(0, 7).ToArray(),
                Enumerable.Range(0, 9).ToArray(),
                Enumerable.Range(0, 9).ToArray(),
                Enumerable.Range(0, 15).ToArray(),
                Enumerable.Range(0, 15).ToArray(),

                Enumerable.Range(0, 267).ToArray(),
                Enumerable.Range(0, 106).ToArray(),
                Enumerable.Range(0, 303).ToArray(),
                Enumerable.Range(0, 42).ToArray(),
                Enumerable.Range(0, 72).ToArray(),
                Enumerable.Range(0, 50).ToArray(),
                Enumerable.Range(0, 143).ToArray(),
                Enumerable.Range(0, 91).ToArray(),
                Enumerable.Range(0, 131).ToArray(),
                Enumerable.Range(0, 63).ToArray(),
                Enumerable.Range(0, 45).ToArray()
            };
        }

        private async void LinearForm_Load(object sender, EventArgs e)
        {
            _allNumbers = await GetAllNumbers();

            ColumnComboBox.Items.AddRange(_columnNames);
            ColumnComboBox.SelectedIndex = 12;
            SelectListView.Columns.Add("회차", 50, HorizontalAlignment.Left);
            SelectListView.Columns.Add("출수", 50, HorizontalAlignment.Center);
        }

        private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColumnComboBox.SelectedIndex > -1)
            {
                ItemComboBox.Items.Clear();
                _selectedDatas = new();
                int sel = ColumnComboBox.SelectedIndex;
                string col = ColumnComboBox.SelectedItem.ToString();
                int[] items = _items[sel];
                items.ToList().ForEach(x => ItemComboBox.Items.Add(x));

                if (sel == 0)
                {
                    for (int i = 1; i <= 45; i++)
                    {
                        string v = "c" + i;
                        string query = "SELECT Orders FROM AppearTbl WHERE " + v + "=0";
                        List<int> orders = new();
                        using var context = new LottoDBContext();
                        using var command = context.Database.GetDbConnection().CreateCommand();
                        command.CommandText = query;
                        context.Database.OpenConnection();
                        using var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int ord = reader.GetInt32(0);
                            orders.Add(ord);
                        }
                        _selectedDatas.Add(orders.ToArray());
                    }
                }
                else if (sel < 14)
                {
                    _numbers = _allNumbers[sel];
                    string query = "SELECT Orders, " + col + " FROM FixChulsuTbl";
                    using var context = new LottoDBContext();
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string str = reader.GetString(1).Trim();
                        int[] array = ConvertToArray(str);
                        _selectedDatas.Add(array);
                    }
                }
                else
                {
                    _numbers = _allNumbers[sel];
                    string query = "SELECT Orders, " + col + " FROM GridTbl";
                    using var context = new LottoDBContext();
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string str = reader.GetString(1).Trim();
                        int[] array = ConvertToArray(str);
                        _selectedDatas.Add(array);
                    }
                }
            }
        }

        private void ItemComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var combobox = sender as ComboBox;
            SelectListView.Items.Clear();
            RealTextBox.Text = string.Empty;
            MaxTextBox.Text = string.Empty;
            NextTextBox.Text = string.Empty;

            int val = (int)combobox.SelectedItem;
            int sel = ColumnComboBox.SelectedIndex;

            if (sel == 0)
            {
                //단순 회귀로 계산 vals length + 1 로 계산
                double[] vals = _selectedDatas[val - 1].Select(x => (double)x).ToArray();
                double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();
                var (squard, intercept, slop) = LinearRegression(ords, vals);
                var predict = (slop * ords.Last() + 1) + intercept;
                var chai = predict - vals.Last();

                PredictTextBox.Text = predict.ToString("F4");
                GapTextBox.Text = chai.ToString("F4");
                LastTextBox.Text = string.Empty;
            }
            else
            {
                //5구간 이동평균으로 계산
                double[] tempvals = _selectedDatas.Select(x => (double)x.ElementAt(val)).ToArray();
                var vals = FiveGapOrders().Select(x => tempvals.Skip(x.Min()).Take(_section).Sum()).ToArray();
                var tens = vals.Skip(vals.Length - 100);
                double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();
                var (squard, intercept, slop) = LinearRegression(ords, vals);
                var predict = (slop * ords.Last() + 1) + intercept;
                var lastlist = tempvals.Skip(tempvals.Length - _section - 1);
                var lastsum = lastlist.Sum();
                var chai = predict - lastsum;
                double avg = vals.Average();
                LastTextBox.Text = "출합: " + lastsum.ToString() + "\t최종출: " + string.Join("-", lastlist);
                PredictTextBox.Text = predict.ToString("F4") + "\t평균: " + avg.ToString("F4");
                GapTextBox.Text = chai.ToString("F4") + "\t편차: " + (avg - lastsum).ToString("F4");
                NumberTextBox.Text = string.Join(",", _numbers[val].OrderBy(x => x).Select(x => x.ToString("00")));

                int n = _lastOrder - 99;

                foreach (var ten in tens)
                {
                    var lvi = new ListViewItem(n.ToString());
                    lvi.SubItems.Add(ten.ToString());
                    SelectListView.Items.Add(lvi);
                    n++;
                }

                SelectListView.EnsureVisible(99);
                var (realContinue, maxContinue, nextList) = NextReal.RealMaxNextList(vals.Select(x => (int)x));
                RealTextBox.Text = realContinue.ToString();
                MaxTextBox.Text = maxContinue.ToString();

                string s4 = string.Empty;

                if (nextList.Count > 1)
                {
                    for (int i = nextList.Min(); i <= nextList.Max(); i++)
                    {
                        int n1 = nextList.Count(x => x == i);
                        string s5 = "[" + i + "]출: " + n1 + "     ";
                        s4 += s5;

                        if (i > 0 && i % 3 == 0)
                        {
                            s4 += "\r\n";
                        }
                    }

                    NextTextBox.Text = s4;
                }
                else
                {
                    NextTextBox.Text = string.Empty;
                }
            }
        }

        private void SectionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _section = (int)SectionNumericUpDown.Value;
        }

        private void ResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResultListBox.SelectedIndex > -1)
            {
                SelectListView.Items.Clear();
                RealTextBox.Text = string.Empty;
                MaxTextBox.Text = string.Empty;
                NextTextBox.Text = string.Empty;

                string s = (string)ResultListBox.SelectedItem;
                string[] tmp = { "\t" };
                var split = s.Split(tmp, StringSplitOptions.RemoveEmptyEntries);
                string s2 = split[0].Trim();
                string s3 = s2.Replace("]", "");
                string s1 = s3[1..];
                int val = int.Parse(s1);

                //5구간 이동평균으로 계산
                double[] tempvals = _selectedDatas.Select(x => (double)x.ElementAt(val)).ToArray();
                var vals = FiveGapOrders().Select(x => tempvals.Skip(x.Min()).Take(_section).Sum()).ToArray();

                var tens = vals.Skip(vals.Length - 100);
                double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();
                var (squard, intercept, slop) = LinearRegression(ords, vals);
                var predict = (slop * ords.Last() + 1) + intercept;
                var lastlist = tempvals.Skip(tempvals.Length - _section - 1);
                var lastsum = lastlist.Sum();
                var chai = predict - lastsum;
                double avg = vals.Average();
                LastTextBox.Text = "출합: " + lastsum.ToString() + "\t종출: " + string.Join("-", lastlist);
                PredictTextBox.Text = predict.ToString("F4") + "\t평균: " + avg.ToString("F4");
                GapTextBox.Text = chai.ToString("F4") + "\t편차: " + (avg - lastsum).ToString("F4");
                NumberTextBox.Text = string.Join(",", _numbers[val].OrderBy(x => x).Select(x => x.ToString("00")));

                int n = _lastOrder - 99;

                foreach (var ten in tens)
                {
                    var lvi = new ListViewItem(n.ToString());
                    lvi.SubItems.Add(ten.ToString());
                    SelectListView.Items.Add(lvi);
                    n++;
                }

                SelectListView.EnsureVisible(99);
                var (realContinue, maxContinue, nextList) = NextReal.RealMaxNextList(vals.Select(x => (int)x));
                RealTextBox.Text = realContinue.ToString();
                MaxTextBox.Text = maxContinue.ToString();

                string s4 = string.Empty;
                if (nextList.Count > 1)
                {
                    for (int i = nextList.Min(); i <= nextList.Max(); i++)
                    {
                        int n1 = nextList.Count(x => x == i);
                        string s5 = "[" + i + "]출: " + n1 + "     ";
                        s4 += s5;

                        if (i > 0 && i % 3 == 0)
                        {
                            s4 += "\r\n";
                        }
                    }

                    NextTextBox.Text = s4;
                }
                else
                {
                    NextTextBox.Text = string.Empty;
                }
            }
        }

        private async void AllCheckButton_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\DataFiles\regression.dat";
            var lines = File.ReadAllLines(path);

            ResultTextBox.Text = string.Empty;
            AllCheckButton.Enabled = false;
            string nums = "#항목 / 악번 / 호번\r\n";
            int[] arrays = { 5, 10, 20, 30, 40, 50 };
            var dangs = new Dictionary<int, int[]>();
            var itemnames = new List<string>();

            //21개 당번
            for (int i = _lastOrder; i >= _lastOrder - Loop; i--)
            {
                var each = Utility.DangbeonOfOrder(i);
                dangs.Add(i, each.ToArray());
            }

            for (int i = 1; i < ColumnComboBox.Items.Count; i++)
            {
                var selectedDatas = new List<int[]>();
                var numbers = _allNumbers[i];
                string col = ColumnComboBox.Items[i].ToString();

                if (i < 14)
                {
                    string query = "SELECT Orders, " + col + " FROM FixChulsuTbl";
                    using var context = new LottoDBContext();
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string str = reader.GetString(1).Trim();
                        int[] array = ConvertToArray(str);
                        selectedDatas.Add(array);
                    }
                }
                else
                {
                    string query = "SELECT Orders, " + col + " FROM GridTbl";
                    using var context = new LottoDBContext();
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string str = reader.GetString(1).Trim();
                        int[] array = ConvertToArray(str);
                        selectedDatas.Add(array);
                    }
                }

                var rstdic = new Dictionary<int, (int, int, int, int)>();
                int[] items = _items[i];
                string s2 = string.Empty;

                foreach (int section in arrays)
                {
                    var lowdubs = new List<int>();
                    var topdubs = new List<int>();

                    for (int j = 1; j <= Loop; j++)
                    {
                        var conlist = new ConcurrentBag<(int, double)>();
                        var datas = await Task.Run(() => FiveGapOrders(j, section));
                        var lastords = datas.Last().Skip(1);
                        int max = lastords.Max();
                        var skiptakes = await Task.Run(() => FiveCombineDatas(j, section, selectedDatas));
                        var lasttakse = await Task.Run(() => LastCombineDatas(lastords, selectedDatas));

                        AllCheckButton.Invoke((Action)delegate
                        {
                            AllCheckButton.Text = i + " / " + section + " / " + j;
                        });

                        await Task.Run(() =>
                        {
                            int counter = 0;

                            Parallel.ForEach(items, index =>
                            {
                                double dub = LinearEachAsync(index, skiptakes, lasttakse);
                                Interlocked.Increment(ref counter);
                                conlist.Add((index, dub));
                            });
                        });

                        var ordered = conlist.OrderBy(x => x.Item2);
                        int lowkey = ordered.First().Item1;
                        int topkey = ordered.Last().Item1;

                        var lowdata = numbers[lowkey];
                        var topdata = numbers[topkey];
                        var dang = dangs[max + 1];

                        int lowdub = lowdata.Intersect(dang).Count();
                        int topdub = topdata.Intersect(dang).Count();

                        lowdubs.Add(lowdub);
                        topdubs.Add(topdub);
                    }

                    int lowzero = lowdubs.Count(x => x == 0);
                    int lowsum = lowdubs.Sum();
                    int topzero = topdubs.Count(x => x == 0);
                    int topsum = topdubs.Sum();

                    rstdic.Add(section, (lowzero, lowsum, topzero, topsum));
                }

                //정렬
                var lowdic = rstdic.OrderByDescending(x => x.Value.Item1)
                    .ThenBy(x => x.Value.Item2).ToDictionary(x => x.Key, x => x.Value);
                var topdic = rstdic.OrderBy(x => x.Value.Item3)
                    .ThenByDescending(x => x.Value.Item4).ToDictionary(x => x.Key, x => x.Value);

                var (badValue, goodValue) = GetItemOfLines(lines, col);
                string bad = (badValue == lowdic.Keys.First()) ? lowdic.Keys.First().ToString("00") : lowdic.Keys.First().ToString("00") + "*";
                string god = (goodValue == topdic.Keys.First()) ? topdic.Keys.First().ToString("00") : topdic.Keys.First().ToString("00") + "*";

                string s1 = $"{col} / {bad} / {god}";
                itemnames.Add(s1);
            }

            AllCheckButton.Text = "구간전체 검사";
            AllCheckButton.Enabled = true;
            ResultTextBox.Text = nums + string.Join(Environment.NewLine, itemnames);
            File.WriteAllText(path, ResultTextBox.Text);
        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            ExecuteButton.Enabled = false;
            ResultTextBox.Text = string.Empty;
            ResultListBox.Items.Clear();
            ResultListBox.Enabled = true;
            SelectListView.Items.Clear();
            RealTextBox.Text = string.Empty;
            MaxTextBox.Text = string.Empty;
            NextTextBox.Text = string.Empty;
            string nums = string.Empty;
            int[] arrays = { 5, 10, 20, 30, 40, 50 };

            var dangs = new Dictionary<int, int[]>();

            //21개 당번
            for (int i = _lastOrder; i >= _lastOrder - Loop; i--)
            {
                var each = Utility.DangbeonOfOrder(i);
                dangs.Add(i, each.ToArray());
            }

            if (ColumnComboBox.SelectedIndex == 0)
            {
                ExecuteButton.Enabled = true;
                var addrst = new List<(int, int)>();

                for (int j = Loop; j > 0; j--)
                {
                    int max = _lastOrder - j;
                    var dic = new Dictionary<int, double>();

                    for (int i = 0; i < 45; i++)
                    {
                        var vals = _selectedDatas[i].Where(x => x <= max).Select(x => (double)x).ToArray();
                        double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();
                        var (squard, intercept, slop) = LinearRegression(ords, vals);
                        var predict = (slop * ords.Last() + 1) + intercept;
                        var chai = predict - vals.Last();
                        dic.Add(i + 1, chai);
                    }

                    var ordic = dic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                    //하위 7개
                    var lows = ordic.Take(7).ToDictionary(x => x.Key, x => x.Value);

                    //상위 7개
                    var hghs = ordic.Skip(45 - 7).ToDictionary(x => x.Key, x => x.Value);

                    var dang = Utility.DangbeonOfOrder(max + 1);

                    int lowchu = lows.Keys.Intersect(dang).Count();
                    int hghchu = hghs.Keys.Intersect(dang).Count();

                    addrst.Add((lowchu, hghchu));
                }

                nums += "하위출: " + string.Join(",", addrst.Select(x => x.Item1)) + "\r\n\r\n";
                nums += "상위출: " + string.Join(",", addrst.Select(x => x.Item2));

                ResultTextBox.Text = nums;
            }
            else
            {
                var rstdic = new Dictionary<int, (int, int, int, int)>();
                int[] items = _items[ColumnComboBox.SelectedIndex];
                string s2 = string.Empty;

                foreach (int section in arrays)
                {
                    var lowdubs = new List<int>();
                    var topdubs = new List<int>();

                    for (int j = 1; j <= Loop; j++)
                    {
                        var conlist = new ConcurrentBag<(int, double)>();
                        var datas = FiveGapOrders(j, section);
                        var lastords = datas.Last().Skip(1);
                        int max = lastords.Max();
                        var skiptakes = await Task.Run(() => FiveCombineDatas(j, section));
                        var lasttakse = await Task.Run(() => LastCombineDatas(lastords));

                        ExecuteButton.Text = section + " / " + j;

                        await Task.Run(() =>
                        {
                            int counter = 0;

                            Parallel.ForEach(items, index =>
                            {
                                double dub = LinearEachAsync(index, skiptakes, lasttakse);
                                Interlocked.Increment(ref counter);
                                conlist.Add((index, dub));
                            });
                        });

                        var ordered = conlist.OrderBy(x => x.Item2);
                        int lowkey = ordered.First().Item1;
                        int topkey = ordered.Last().Item1;

                        var lowdata = _numbers[lowkey];
                        var topdata = _numbers[topkey];
                        var dang = dangs[max + 1];

                        int lowdub = lowdata.Intersect(dang).Count();
                        int topdub = topdata.Intersect(dang).Count();

                        lowdubs.Add(lowdub);
                        topdubs.Add(topdub);
                    }

                    string s3 = "구간: " + section + "\r\n";
                    int lowzero = lowdubs.Count(x => x == 0);
                    int lowsum = lowdubs.Sum();
                    int topzero = topdubs.Count(x => x == 0);
                    int topsum = topdubs.Sum();
                    s3 += $"악번: {string.Join(",", lowdubs)} \r\n0출: {lowzero} 출합: {lowsum}\r\n";
                    s3 += $"호번: {string.Join(",", topdubs)} \r\n0출: {topzero} 출합: {topsum}";

                    nums += s3 + "\r\n\r\n";
                    rstdic.Add(section, (lowzero, lowsum, topzero, topsum));
                }

                ExecuteButton.Enabled = true;
                ExecuteButton.Text = "구간사전 검사";

                //정렬
                var lowdic = rstdic.OrderByDescending(x => x.Value.Item1)
                    .ThenBy(x => x.Value.Item2).ToDictionary(x => x.Key, x => x.Value);
                var topdic = rstdic.OrderBy(x => x.Value.Item3)
                    .ThenByDescending(x => x.Value.Item4).ToDictionary(x => x.Key, x => x.Value);

                nums += "\r\n악번구간: " + lowdic.Keys.First() + "\t호번구간: " + topdic.Keys.First();
                ResultTextBox.Text = nums;
                ResultTextBox.SelectionStart = ResultTextBox.Text.Length;
                ResultTextBox.ScrollToCaret();
            }
        }

        private async void EachColumnButton_Click(object sender, EventArgs e)
        {
            ResultListBox.Items.Clear();
            ResultListBox.Enabled = true;
            SelectListView.Items.Clear();
            RealTextBox.Text = string.Empty;
            MaxTextBox.Text = string.Empty;
            NextTextBox.Text = string.Empty;
            string nums;

            if (ColumnComboBox.SelectedIndex == 0)
            {
                var dic = new Dictionary<int, double>();

                for (int i = 0; i < 45; i++)
                {
                    double[] vals = _selectedDatas[i].Select(x => (double)x).ToArray();
                    double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();
                    var (squard, intercept, slop) = LinearRegression(ords, vals);
                    var predict = (slop * ords.Last() + 1) + intercept;
                    var chai = predict - vals.Last();
                    dic.Add(i + 1, chai);
                }

                var ordic = dic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                //하위 7개
                var lowdic = ordic.Take(7).ToDictionary(x => x.Key, x => x.Value);

                //상위 7개
                var hghdic = ordic.Skip(45 - 7).ToDictionary(x => x.Key, x => x.Value);

                nums = "예상근접\r\n";

                foreach (var key in lowdic.Keys)
                {
                    double d = lowdic[key];
                    string s = key.ToString("00") + "번\t" + d.ToString("F4") + "\r\n";
                    nums += s;
                }

                nums += "\r\n예상초과\r\n";

                foreach (var key in hghdic.Keys)
                {
                    double d = hghdic[key];
                    string s = "[" + key + "]\t" + d.ToString("F4") + "\r\n";
                    nums += s;
                }

                NumberTextBox.Text = string.Empty;
            }
            else
            {
                nums = "음수면 초과, 양수면 미달(호번)\r\n\r\n";
                int[] items = _items[ColumnComboBox.SelectedIndex];
                var dic = new Dictionary<int, double>();

                foreach (var i in items)
                {
                    var task = Task.Run(() =>
                    {
                        double[] tempvals = _selectedDatas.Select(x => (double)x.ElementAt(i)).ToArray();
                        var valdic = _selectedDatas.Select(x => (double)x.ElementAt(i))
                                                .Select((val, idx) => new { val, idx })
                                                .ToDictionary(x => x.idx + 1, x => x.val);

                        var datas = FiveGapOrders(Loop + 1, _section);
                        var vals = datas.Select(x => valdic.Where(y => x.Contains(y.Key))
                                                .Select(x => x.Value).Sum()).Select(x => (double)x).ToArray();

                        double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();
                        var (squard, intercept, slop) = LinearRegression(ords, vals);
                        var predict = (slop * ords.Last() + 1) + intercept;
                        var lastsum = tempvals.Skip(tempvals.Length - _section - 1).Sum();
                        var chai = predict - lastsum;

                        dic.Add(i, chai);
                    });

                    await task;
                }

                var ordic = dic.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                foreach (var key in ordic.Keys)
                {
                    double d = ordic[key];
                    string s = "[" + key + "]\t" + d.ToString("F4") + "\r\n";
                    ResultListBox.Items.Add(s);
                    nums += s;
                }
            }
        }






        //**********************  내부메서드  *************************





        /// <summary>
        /// 열개수의 번호데이터 딕셔너리
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, List<int[]>> GetColumnNumber()
        {
            int[] idxs = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
            var pairs = new Dictionary<int, List<int[]>>();

            foreach (int idx in idxs)
            {
                string path = Application.StartupPath + @"DataFiles\" + "col" + idx.ToString("00") + ".csv";
                var lines = File.ReadAllLines(path);
                var arrays = new List<int[]>();

                foreach (string line in lines)
                {
                    var array = line.Split(',').Select(x => int.Parse(x.ToString())).ToArray();
                    arrays.Add(array);
                }

                pairs.Add(idx, arrays);
            }

            return pairs;
        }

        /// <summary>
        /// 검사에 사용할 전체 데이터의 해당번호
        /// </summary>
        /// <returns></returns>
        private async Task<List<List<int[]>>> GetAllNumbers()
        {
            var pairs = await Task.Run(GetColumnNumber);
            var lists = new List<List<int[]>>
            {
                new List<int[]>() { Array.Empty<int>() },
                LuckyLibrary.BeondaeDatas(),
                LuckyLibrary.SlipDatas(),
                LuckyLibrary.KkeutbeonDatas(),

                LuckyLibrary.HorizontalFlowDatas(3),
                LuckyLibrary.VerticalFlowDatas(3),
                LuckyLibrary.HorizontalFlowDatas(5),
                LuckyLibrary.VerticalFlowDatas(5),
                LuckyLibrary.HorizontalFlowDatas(7),
                LuckyLibrary.VerticalFlowDatas(7),
                LuckyLibrary.HorizontalFlowDatas(9),
                LuckyLibrary.VerticalFlowDatas(9),
                LuckyLibrary.HorizontalFlowDatas(15),
                LuckyLibrary.VerticalFlowDatas(15),
                pairs[3], pairs[4], pairs[5], pairs[6], pairs[7], pairs[8], pairs[9], pairs[10], pairs[11], pairs[12], pairs[15]
            };

            return lists;
        }

        /// <summary>
        /// 입력 문장을 정수배열로 바꾸기
        /// </summary>
        /// <param name="line">숫자문장 (쉼표나 콤마가 없음)</param>
        /// <returns>정수배열</returns>
        private static int[] ConvertToArray(string line)
        {
            var array = line.Select(x => int.Parse(x.ToString())).ToArray();

            return array;
        }

        /// <summary>
        /// 구간전체출수에대한 선형회귀 결과
        /// </summary>
        /// <param name="index">인덱스</param>
        /// <param name="datas">구간출수합 배열전체 리스트</param>
        /// <param name="lastarray">마지막 구간출수합 배열</param>
        /// <returns></returns>
        private static double LinearEachAsync(int index, List<int[]> datas, int[] lastarray)
        {
            double[] vals = datas.Select(x => x.ElementAt(index)).Select(x => (double)x).ToArray();
            double[] ords = Enumerable.Range(1, vals.Length).Select(x => (double)x).ToArray();

            var (squard, intercept, slop) = LinearRegression(ords, vals);
            var predict = slop * (ords.Last() + 1.0) + intercept;
            var lastsum = lastarray.ElementAt(index);
            var chai = predict - lastsum;

            return chai;
        }

        /// <summary>
        /// 선형회귀 값 반환 (y = ax + b)
        /// </summary>
        /// <param name="xVals">x 축의 값들</param>
        /// <param name="yVals">y 축의 값들</param>
        /// <returns>튜플(행의 r^2 값(y), 절편값(b), 기울기(a))</returns>
        /// <exception cref="Exception"></exception>
        private static (double rSquared, double yIntercept, double slope) LinearRegression(double[] xVals, double[] yVals)
        {
            if (xVals.Length != yVals.Length)
            {
                throw new Exception("Input values should be with the same length.");
            }

            var tpl = new ScottPlot.Statistics.LinearRegressionLine(xVals, yVals);

            return (tpl.rSquared, tpl.offset, tpl.slope);
        }

        private static (double rsquared, double slope, double intercept) LinearRegression1(double[] xVals, double[] yVals)
        {
            if (xVals.Length != yVals.Length)
            {
                throw new Exception("Input values should be with the same length.");
            }

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (var i = 0; i < xVals.Length; i++)
            {
                var x = xVals[i];
                var y = yVals[i];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }

            var count = xVals.Length;
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            //var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            var dblR = rNumerator / Math.Sqrt(rDenom);

            var rSquare = dblR * dblR;
            var yIntercept = meanY - ((sCo / ssX) * meanX);
            var slope = sCo / ssX;

            return (rSquare, slope, yIntercept);
        }

        /// <summary>
        /// 5구간씩 회차를 묶은 리스트
        /// </summary>
        /// <returns></returns>
        private List<int[]> FiveGapOrders()
        {
            int n = _lastOrder % _section;
            List<int[]> orders = new();

            for (int i = n - 1; i < _lastOrder; i++)
            {
                var list = new List<int>();

                for (int j = 0; j < _section; j++)
                {
                    int v = i + j;

                    if (v >= _lastOrder)
                    {
                        break;
                    }
                    else
                    {
                        list.Add(v);
                    }
                }

                if (list.Count == _section)
                {
                    orders.Add(list.ToArray());
                }
            }

            return orders;
        }

        /// <summary>
        /// 5구간씩 회차를 묶은 리스트
        /// </summary>
        /// <param name="start">시작회차</param>
        /// <param name="section">검사구간</param>
        /// <returns></returns>
        private List<int[]> FiveGapOrders(int start, int section)
        {
            var lists = new List<int[]>();
            int last = _lastOrder - Loop + (start - 1);

            for (int i = start; i <= last; i++)
            {
                var each = Enumerable.Range(i, section).ToArray();

                if (each.Max() > last)
                    break;
                else
                    lists.Add(each);
            }

            return lists;
        }

        /// <summary>
        /// 구간출수합 배열전체 리스트
        /// </summary>
        /// <param name="start">시작회차</param>
        /// <param name="section">검사구간</param>
        /// <returns></returns>
        private List<int[]> FiveCombineDatas(int start, int section)
        {
            var lists = FiveGapOrders(start, section);

            var datas = new List<int[]>();

            foreach (int[] array in lists)
            {
                int min = array.Min() - 1;

                var dts = _selectedDatas.Skip(min).Take(section);
                int[] cols = new int[_selectedDatas[0].Length];

                for (int i = 0; i < _selectedDatas[0].Length; i++)
                {
                    int n = dts.Select(x => x.ElementAt(i)).Sum();
                    cols[i] = n;
                }

                datas.Add(cols);
            }

            return datas;
        }

        /// <summary>
        /// 구간출수합 배열전체 리스트
        /// </summary>
        /// <param name="start">시작회차</param>
        /// <param name="section">검사구간</param>
        /// <param name="selectedDatas">선택한 항목의 전체출수 데이터</param>
        /// <returns></returns>
        private List<int[]> FiveCombineDatas(int start, int section, List<int[]> selectedDatas)
        {
            var lists = FiveGapOrders(start, section);

            var datas = new List<int[]>();

            foreach (int[] array in lists)
            {
                int min = array.Min() - 1;

                var dts = selectedDatas.Skip(min).Take(section);
                int[] cols = new int[selectedDatas[0].Length];

                for (int i = 0; i < selectedDatas[0].Length; i++)
                {
                    int n = dts.Select(x => x.ElementAt(i)).Sum();
                    cols[i] = n;
                }

                datas.Add(cols);
            }

            return datas;
        }

        /// <summary>
        /// 마지막구간출수합 배열
        /// </summary>
        /// <param name="lastord">마지막 회차구간</param>
        /// <returns>열별합 배열</returns>
        private int[] LastCombineDatas(IEnumerable<int> lastord)
        {
            var dts = _selectedDatas.Skip(lastord.Min() - 1).Take(lastord.Count());
            int[] cols = new int[_selectedDatas[0].Length];

            for (int i = 0; i < _selectedDatas[0].Length; i++)
            {
                int n = dts.Select(x => x.ElementAt(i)).Sum();
                cols[i] = n;
            }

            return cols;
        }

        /// <summary>
        /// 마지막구간출수합 배열
        /// </summary>
        /// <param name="lastord">마지막 회차구간</param>
        /// <param name="selectedDatas">선택한 항목의 전체출수 데이터</param>
        /// <returns>열별합 배열</returns>
        private static int[] LastCombineDatas(IEnumerable<int> lastord, List<int[]> selectedDatas)
        {
            var dts = selectedDatas.Skip(lastord.Min() - 1).Take(lastord.Count());
            int[] cols = new int[selectedDatas[0].Length];

            for (int i = 0; i < selectedDatas[0].Length; i++)
            {
                int n = dts.Select(x => x.ElementAt(i)).Sum();
                cols[i] = n;
            }

            return cols;
        }

        /// <summary>
        /// 문장 라인에서 항목의 악번, 호번을 가져오기
        /// </summary>
        /// <param name="lines">전체문장</param>
        /// <param name="colname">항목이름</param>
        /// <returns>튜플(악번값, 호번값)</returns>
        private static (int badValue, int goodValue) GetItemOfLines(string[] lines, string colname)
        {
            string bad = string.Empty, god = string.Empty;

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                {
                    var splits = line.Split('/').Select(x => x.Trim()).ToArray();
                    if (colname.Equals(splits[0]))
                    {
                        bad = splits[1][0..2];
                        god = splits[2][0..2];
                        break;
                    }
                }
            }

            return (int.Parse(bad), int.Parse(god));
        }
    }
}
