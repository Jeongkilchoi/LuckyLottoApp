using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 가로3 - 끝수의 출수로 회귀검사하는 폼 클래스
    /// </summary>
    public partial class ChulsuRegressionForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private readonly List<string> _itemNames;
        private int _chulNumber = 0;
        private int _section = 100;
        private float _limitValue = 0.95f;
        private List<int> _badnums;
        private List<(int order, int[] chulArray)> _allChulsu;
        private List<(string gdstr, int index, int lastchul, int sec, double chai, double rsql)> _results;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public ChulsuRegressionForm(int lastOrder)
        {
            InitializeComponent();
            _lastOrder = lastOrder;
            _itemNames = new List<string>
            {
                "RH15", "RV15", "DH15", "DV15", "RH09", "RV09", "DH09", "DV09", "RH07", "RV07", "DH07", "DV07", "Kkeutbeon"
            };
        }

        private void ChulsuRegressionForm_Load(object sender, EventArgs e)
        {
            ItemsComboBox.Items.AddRange(_itemNames.ToArray());
            ItemsComboBox.SelectedIndex = 0;
            string item = ItemsComboBox.SelectedItem.ToString();
            _allChulsu = GetAllDataOfColumn(item);
            string sel = item[2..];
            var arr = new List<int>();
            var result = int.TryParse(sel, out int n);

            if (result)
                arr = Enumerable.Range(0, n).ToList();
            else
                arr = Enumerable.Range(0, 10).ToList();

            arr.ForEach(x => IndexComboBox.Items.Add(x));
            IndexComboBox.SelectedIndex = 0;
        }

        private void ItemsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemsComboBox.SelectedIndex > -1)
            {
                _allChulsu = new();
                IndexComboBox.Items.Clear();
                string item = ItemsComboBox.SelectedItem.ToString();
                _allChulsu = GetAllDataOfColumn(item);
                string sel = item[2..];
                var arr = new List<int>();
                var result = int.TryParse(sel, out int n);

                if (result)
                    arr = Enumerable.Range(0, n).ToList();
                else
                    arr = Enumerable.Range(0, 10).ToList();
                
                arr.ForEach(x => IndexComboBox.Items.Add(x));
                //IndexComboBox.SelectedIndex = 0;
                var (order, chulArray) = _allChulsu.Last();
                LastchulLabel.Text = "종출:   " + string.Join(", ", chulArray);
            }
        }

        private void IndexComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (IndexComboBox.SelectedIndex > -1)
            {
                PredictTextBox.Text = string.Empty;
                int sel = IndexComboBox.SelectedIndex;
                string[] flow = { "H", "V" };
                string item = ItemsComboBox.SelectedItem.ToString();
                string lngh = item[2..];
                int[] array = Array.Empty<int>();
                var result = int.TryParse(lngh, out int row);
                if (result)
                {
                    int way = (item[0] == 'R') ? 0 : 1;
                    array = (item[1] == 'H') ? LuckyLibrary.HorizontalFlowDatas(row, way)[sel] : LuckyLibrary.VerticalFlowDatas(row, way)[sel];
                }
                else
                {
                    array = LuckyLibrary.KkeutbeonDatas()[sel];
                }
                NumberLabel.Text = "해당번호: " +
                    string.Join(", ", array.Where(x => x >= 1 && x <= 45).OrderBy(x => x).Select(x => x.ToString("00")));

                var data = _allChulsu.Select(x => (ord: x.order, val: x.chulArray[sel]));
                var (predict, chai, length) = GetMultipleRegression(sel, _allChulsu);
                if (predict != double.NaN)
                {
                    var chuls = (_chulNumber == 2) ? data.Where(x => x.val >= 2).Select(x => x.ord).ToList() :
                                 data.Where(x => x.val == _chulNumber).Select(x => x.ord).ToList();
                    int sec = _section <= chuls.Count ? _section : TruncateValue(chuls.Count);
                    SectionNumericUpDown.Value = sec;
                    var ords = chuls.Skip(chuls.Count - sec).Select(x => (double)x).ToArray();
                    var sqns = Enumerable.Range(1, sec).Select(x => (double)x).ToArray();
                    string s1 = $"다중: {predict:0.00}  ";
                    PresentPlot(ords, sqns, s1);
                }
            }
        }

        private void IndexComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (IndexComboBox.SelectedIndex > -1)
            //{
            //    PredictTextBox.Text = string.Empty;
            //    int sel = IndexComboBox.SelectedIndex;
            //    string[] flow = { "H", "V"};
            //    string item = ItemsComboBox.SelectedItem.ToString();
            //    string lngh = item[2..];
            //    int[] array = Array.Empty<int>();
            //    var result = int.TryParse(lngh, out int row);
            //    if (result)
            //    {
            //        int way = (item[0] == 'R') ? 0 : 1;
            //        array = (item[1] == 'H') ? LuckyLibrary.HorizontalFlowDatas(row, way)[sel] : LuckyLibrary.VerticalFlowDatas(row, way)[sel];
            //    }
            //    else
            //    {
            //        array = LuckyLibrary.KkeutbeonDatas()[sel];
            //    }
            //    NumberLabel.Text = "해당번호: " + 
            //        string.Join(", ", array.Where(x => x >= 1 && x <= 45).OrderBy(x => x).Select(x => x.ToString("00")));
                
            //    var data = _allChulsu.Select(x => (ord: x.order, val: x.chulArray[sel]));
            //    var (predict, chai, length) = GetMultipleRegression(sel, _allChulsu);
            //    if (predict != double.NaN)
            //    {
            //        var chuls = (_chulNumber == 2) ? data.Where(x => x.val >= 2).Select(x => x.ord).ToList() :
            //                     data.Where(x => x.val == _chulNumber).Select(x => x.ord).ToList();
            //        int sec = _section <= chuls.Count ? _section : TruncateValue(chuls.Count);
            //        SectionNumericUpDown.Value = sec;
            //        var ords = chuls.Skip(chuls.Count - sec).Select(x => (double)x).ToArray();
            //        var sqns = Enumerable.Range(1, sec).Select(x => (double)x).ToArray();
            //        string s1 = $"다중: {predict:0.00}  ";
            //        PresentPlot(ords, sqns, s1);
            //    }
            //}
        }

        private void LimitNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _limitValue = (float)LimitNumericUpDown.Value;
        }

        private void ChulNumericUpDown_ValueChanged_1(object sender, EventArgs e)
        {
            _chulNumber = (int)ChulNumericUpDown.Value;
        }

        private void SectionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _section = (int)SectionNumericUpDown.Value;
        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            MultitextBox.Text = string.Empty;
            string name = ItemsComboBox.SelectedItem.ToString();
            var datas = await Task.Run(() => GetAllDataOfColumn(name));
            var lastarr = datas.Last().chulArray;
            int lengh = datas.First().chulArray.Length;
            var multis = new List<(string item, int index, double predict, double chait, int sect)>();
            if (SimpleRadioButton.Checked)
            {
                MultitextBox.Enabled = true;
                var rsts = new List<(string gdstr, int index, int lastchul, int sec, double chai, double rsql)>();
                _badnums = new List<int>();

                for (int i = 0; i < lengh; i++)
                {
                    var chuls = datas.Select(x => (ord: x.order, chul: x.chulArray[i]));
                    int j = lastarr[i];
                    var each = (j == 2) ? chuls.Where(x => x.chul >= 2).Select(g => g.ord) : chuls.Where(x => x.chul == j).Select(g => g.ord);
                    int sec = TruncateValue(each.Count());
                    var tmp = new List<(int sec, double chai, double rsql)>();
                    for (int k = 20; k <= 300; k += 5)
                    {
                        if (k <= sec)
                        {
                            var skips = each.Skip(each.Count() - k);
                            double[] ords = skips.Select(x => (double)x).ToArray();
                            double[] sqns = Enumerable.Range(1, k).Select(x => (double)x).ToArray();
                            var model = new ScottPlot.Statistics.LinearRegressionLine(ords, sqns);
                            if (model.rSquared > _limitValue)
                            {
                                double predicts = (_lastOrder + 1.0) * model.slope + model.offset;
                                double chai = sqns[^1] + 1.0 - predicts;
                                double abs = Math.Abs(chai);
                                tmp.Add((k, chai, model.rSquared));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    var mintmp = tmp.Where(x => x.rsql > _limitValue);
                    if (mintmp?.Any() ?? false)
                    {
                        var addtmps = new List<(int sec, double chai, double rsql)>();
                        var badtmps = mintmp.Where(x => x.chai > 2);

                        if (badtmps?.Any() ?? false)
                        {
                            var badtmp = badtmps.OrderByDescending(g => g.chai).First();
                            if (IsNotContain(addtmps, badtmp))
                            {
                                rsts.Add(("악번", i, j, badtmp.sec, badtmp.chai, badtmp.rsql));
                                _badnums.Add(i);
                            }
                        }

                        var prftmps = mintmp.Where(x => Math.Abs(x.chai) <= 2);
                        if (prftmps?.Any() ?? false)
                        {
                            var prftmp = prftmps.OrderBy(x => Math.Abs(x.chai)).First();
                            if (IsNotContain(addtmps, prftmp))
                            {
                                rsts.Add(("최적", i, j, prftmp.sec, prftmp.chai, prftmp.rsql));
                            }
                        }

                        var godtmps = mintmp.Where(x => x.chai < -2);
                        if (godtmps?.Any() ?? false)
                        {
                            var godtmp = godtmps.OrderBy(g => g.chai).First();
                            if (IsNotContain(addtmps, godtmp))
                            {
                                rsts.Add(("호번", i, j, godtmp.sec, godtmp.chai, godtmp.rsql));
                            }
                        }
                    }

                    var (predict, chait, sect) = GetMultipleRegression(i, datas);
                    if (predict != double.NaN)
                    {
                        multis.Add((name, i, predict, chait, sect));
                    }
                }

                if (rsts?.Any() ?? false)
                {
                    _results = new(rsts);
                    foreach (var (gdstr, index, lastchul, sec, chai, rsql) in rsts)
                    {
                        string s = $"{index}/{lastchul}/{sec}/{gdstr}/{chai:0.000000}/{rsql:0.000000}";
                        listBox1.Items.Add(s);
                    }
                }
                if (multis.Any())
                {
                    var mulstrings = new List<string>();
                    var ordeds = multis.OrderBy(x => Math.Abs(x.chait)).ToList();
                    foreach (var item in ordeds)
                    {
                        string s = $"{name}/{item.index}/{item.sect}/{item.chait:0.000000}/{item.predict:0.000000}";
                        mulstrings.Add(s);
                    }
                    MultitextBox.Text = string.Join(Environment.NewLine, mulstrings);
                }
            }
            else
            {
                MultitextBox.Enabled = false;
                var lists = new List<(string item, int index, double predict, double chai, int sec)>();
                foreach (string item in _itemNames)
                {
                    var chdatas = await Task.Run(() => GetAllDataOfColumn(item));
                    int chlengh = chdatas.First().chulArray.Length;
                    for (int i = 0; i < chlengh; i++)
                    {
                        var (predict, chai, sec) = GetMultipleRegression(i, chdatas);
                        if (predict != double.NaN)
                        {
                            lists.Add((item, i, predict, chai, sec));
                        }
                    }
                }

                var ords = lists.OrderBy(x => Math.Abs(x.chai));

                foreach (var (item, index, predict, chai, sec) in ords)
                {
                    string s = $"{item}/{index}/{sec}/{chai:0.000000}/{predict:0.000000}";
                    listBox1.Items.Add(s);
                }
            }
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = listBox1.SelectedItem.ToString();
            var split = item.Split('/');
            if (MultipleRadioButton.Checked)
            {
                string items = split[0];
                int itemidx = _itemNames.IndexOf(items);
                ItemsComboBox.SelectedIndex = itemidx;
                string sel = items[2..];
                var arr = new List<int>();
                var result = int.TryParse(sel, out int n);

                if (result)
                    arr = Enumerable.Range(0, n).ToList();
                else
                    arr = Enumerable.Range(0, 10).ToList();

                arr.ForEach(x => IndexComboBox.Items.Add(x));
                string ids = split[1];
                //int index = int.Parse(split[1]);
                int sec = TruncateValue(int.Parse(split[2]));
                _section = sec;
                _chulNumber = 0;
                ChulNumericUpDown.Value = 0;
                SectionNumericUpDown.Value = sec;
                IndexComboBox.SelectedIndex = IndexComboBox.FindString(ids);
            }
            else
            {
                int index = int.Parse(split[0]);
                int chul = int.Parse(split[1]);
                int sec = int.Parse(split[2]);
                _section = sec;
                _chulNumber = chul;
                SectionNumericUpDown.Value = sec;
                IndexComboBox.SelectedIndex = index;
                ChulNumericUpDown.Value = chul;
            }
        }

        private void AllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var (gdstr, index, lastchul, sec, chai, rsql) in _results)
            {
                string s = $"{index}/{lastchul}/{sec}/{gdstr}/{chai:0.000000}/{rsql:0.000000}";
                listBox1.Items.Add(s);
            }
        }

        private void GoodRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var tmps = _results.Where(x => x.gdstr.Equals("호번") && !_badnums.Contains(x.index));
            if (tmps?.Any() ?? false)
            {
                foreach (var (gdstr, index, lastchul, sec, chai, rsql) in tmps)
                {
                    string s = $"{index}/{lastchul}/{sec}/{gdstr}/{chai:0.000000}/{rsql:0.000000}";
                    listBox1.Items.Add(s);
                }
            }
        }

        private void BadRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var tmps = _results.Where(x => x.gdstr.Equals("악번"));
            if (tmps?.Any() ?? false)
            {
                foreach (var (gdstr, index, lastchul, sec, chai, rsql) in tmps)
                {
                    string s = $"{index}/{lastchul}/{sec}/{gdstr}/{chai:0.000000}/{rsql:0.000000}";
                    listBox1.Items.Add(s);
                }
            }
        }

        private void PerfectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var tmps = _results.Where(x => x.gdstr.Equals("최적") && !_badnums.Contains(x.index));
            if (tmps?.Any() ?? false)
            {
                foreach (var (gdstr, index, lastchul, sec, chai, rsql) in tmps)
                {
                    string s = $"{index}/{lastchul}/{sec}/{gdstr}/{chai:0.000000}/{rsql:0.000000}";
                    listBox1.Items.Add(s);
                }
            }
        }




        //**************************  메서드  *******************************



        /// <summary>
        /// 끝수의 버림 결과반환
        /// </summary>
        /// <param name="value">입력 정수</param>
        /// <returns>반환 정수</returns>
        private static int TruncateValue(int value)
        {
            int n;

            if (value % 5 == 0)
            {
                n = value;
            }
            else
            {
                string[] lows = { "1", "2", "3", "4" };
                string[] highs = { "6", "7", "8", "9" };
                string num = value.ToString();
                var chars = num.ToCharArray();
                string last = num[^1].ToString();
                if (lows.Contains(last))
                {
                    chars[^1] = '0';
                    num = new string(chars);
                }
                else if (highs.Contains(last))
                {
                    chars[^1] = '5';
                    num = new string(chars);
                }

                n = int.Parse(num);
            }
            return n;
        }

        /// <summary>
        /// 선형회귀 결과를 출력
        /// </summary>
        private void PresentPlot(double[] ords, double[] sqns, string input)
        {
            string path = Application.StartupPath + @"\DataFiles\chul.png";

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            var plt = new ScottPlot.Plot(770, 615);
            double x1 = ords[0];
            double x2 = ords[^1];

            var model = new ScottPlot.Statistics.LinearRegressionLine(ords, sqns);

            string s = "Linear Regression\nY = {model.slope:0.0000}x + {model.offset:0.0} " +
                      $"(R² = {model.rSquared:0.0000})";
            plt.Title(s);
            plt.AddScatter(ords, sqns, lineWidth: 0);
            plt.AddLine(model.slope, model.offset, (x1, x2), lineWidth: 2);
            plt.SaveFig(path);
            double predict = (_lastOrder + 1.0) * model.slope + model.offset;
            double cha = sqns[^1] + 1.0 - predict;
            PredictTextBox.Text = input + $" 예상:{predict:0.00}   차이:{cha:0.00}";
            using (FileStream stream = new(path, FileMode.Open, FileAccess.Read))
            {
                pictureBox1.Image = Image.FromStream(stream);
                stream.Dispose();
            }
            File.Delete(path);
        }

        /// <summary>
        /// 선택항목의 전체출수 데이터
        /// </summary>
        /// <param name="colname">열이름</param>
        /// <returns>튜플(회차, 출수배열)</returns>
        private static List<(int order, int[] chulArray)> GetAllDataOfColumn(string colname)
        {
            var datas = new List<(int, int[])>();
            using var context = new LottoDBContext();
            var command = context.Database.GetDbConnection().CreateCommand();
            string query = $"SELECT Orders, {colname} FROM FixChulsuTbl";
            command.CommandText = query;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ord = reader.GetInt32(0);
                string row = reader.GetString(1).Trim();
                var arr = row.Select(x => int.Parse(x.ToString())).ToArray();
                datas.Add((ord, arr));
            }

            return datas;
        }
        
        /// <summary>
        /// 이미 존재하지 않으면 참
        /// </summary>
        /// <param name="source">통과모음 튜플리스트</param>
        /// <param name="target">검사할 튜플</param>
        /// <returns>존재하지 않으면 참</returns>
        private static bool IsNotContain(List<(int sec, double chai, double rsql)> source, (int sec, double chai, double rsql) target)
        {
            bool pass;
            double tolerance = 0.0000001;
            if (source?.Any() ?? false)
            {
                int error = 0;
                foreach (var (sec, chai, rsql) in source)
                {
                    if (sec == target.sec && Math.Abs(chai - target.chai) < tolerance && Math.Abs(rsql - target.rsql) < tolerance)
                    {
                        error++;
                        break;
                    }
                }

                pass = (error == 0);

                if (pass)
                {
                    source.Add(target);
                }
            }
            else
            {
                source.Add(target);
                pass = true;
            }

            return pass;
        }

        /// <summary>
        /// 다중회귀 예상값과 차이찾기
        /// </summary>
        /// <param name="index">항목의 인덱스</param>
        /// <param name="datas">항목 전체 데이터튜플</param>
        /// <returns></returns>
        private (double predict, double chai, int length) GetMultipleRegression(int index, List<(int order, int[] chulArray)> datas)
        {
            float f = (float)LimitNumericUpDown.Value;
            var selectData = datas.Select(x => (x.order, chulval: x.chulArray[index]));
            var findOrders = new List<double[]>();
            //0-2 출이 나온 회차
            for (int i = 0; i <= 2; i++)
            {
                var findOrder = (i == 2) ? selectData.Where(x => x.chulval >= i).Select(g => (double)g.order).ToArray() :
                                           selectData.Where(x => x.chulval == i).Select(g => (double)g.order).ToArray();
                findOrders.Add(findOrder);
            }

            //최소출로 갯수 맞추기
            int minshown = findOrders.Min(x => x.Length);
            var adjust = findOrders.Select(x => x.Skip(x.Length - minshown).ToArray()).ToList();

            //pivot
            var pivots = new List<double[]>();
            for (int i = 0; i < adjust[0].Length; i++)
            {
                var ea = adjust.Select(x => x[i]).ToArray();
                pivots.Add(ea);
            }

            var sqns = Enumerable.Range(1, minshown).Select(x => (double)x).ToArray();

            var p = MultipleRegression.QR(pivots.ToArray(), sqns, intercept: true);
            double d = _lastOrder + 1.0;
            double predict = p.Skip(1).Select(x => d * x).Sum() + p[0];
            double chai = minshown - predict;

            var ems = Enumerable.Range(0, p.Length - 1);
            var pts = new List<double>();
            foreach (double[] item in pivots)
            {
                double d1 = ems.Select(idx => item[idx] * p[idx + 1]).Sum() + p[0];
                pts.Add(d1);
            }
            double rsquared = GoodnessOfFit.RSquared(pts, sqns);
            if (rsquared > f)
            {
                return (predict, chai, minshown);
            }
            else
            {
                return (double.NaN, double.NaN, 0);
            }
        }


    }
}
