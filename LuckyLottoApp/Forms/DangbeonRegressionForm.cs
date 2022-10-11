using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;
using System.Data;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 당첨회차의 회기검사 폼 클래스
    /// </summary>
    public partial class DangbeonRegressionForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private int _number = 1;
        private int _section = 50;
        private List<int> _badIndex;
        private List<(int num, int sect, string bagod, float predic, float chai)> _allvalues = null;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public DangbeonRegressionForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            for (int i = 20; i <= 110; i += 5)
            {
                SectionComboBox.Items.Add(i);
            }
        }

        private void DangbeonRegressionForm_Load(object sender, EventArgs e)
        {
            SectionComboBox.SelectedItem = 50;
        }

        private void SectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SectionComboBox.SelectedIndex > -1)
            {
                int sel = Convert.ToInt32(SectionComboBox.SelectedItem);
                _section = sel;

                PresentPlot();
                pictureBox1.Invalidate();
            }
        }

        private void NumNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _number = (int)NumNumericUpDown.Value;
            PresentPlot();
        }

        private void GoodRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (GoodRadioButton.Checked)
            {
                var finds = _allvalues.Where(x => x.bagod.Equals("호번") && !_badIndex.Contains(x.num)).OrderBy(x => x.chai);
                if (finds?.Any() ?? false)
                {
                    foreach (var (num, sect, bagod, predic, chai) in finds)
                    {
                        string s = $"{num:00}/{sect:000}/{bagod}/{predic:0.0000}/{chai:0.0000}";
                        listBox1.Items.Add(s);
                    }
                }
            }
        }

        private void BadRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (BadRadioButton.Checked)
            {
                var finds = _allvalues.Where(x => x.bagod.Equals("악번")).OrderByDescending(x => x.chai);
                if (finds?.Any() ?? false)
                {
                    foreach (var (num, sect, bagod, predic, chai) in finds)
                    {
                        string s = $"{num:00}/{sect:000}/{bagod}/{predic:0.0000}/{chai:0.0000}";
                        listBox1.Items.Add(s);
                    }
                }
            }
        }

        private void AllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (AllRadioButton.Checked)
            {
                foreach (var (num, sect, bagod, predic, chai) in _allvalues)
                {
                    string s = $"{num:00}/{sect:000}/{bagod}/{predic:0.0000}/{chai:0.0000}";
                    listBox1.Items.Add(s);
                }
            }
        }

        private void PerfectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (PerfectRadioButton.Checked)
            {
                var finds = _allvalues.Where(x => x.bagod.Equals("최적") && !_badIndex.Contains(x.num)).OrderBy(x => Math.Abs(x.chai));
                if (finds?.Any() ?? false)
                {
                    foreach (var (num, sect, bagod, predic, chai) in finds)
                    {
                        string s = $"{num:00}/{sect:000}/{bagod}/{predic:0.0000}/{chai:0.0000}";
                        listBox1.Items.Add(s);
                    }
                }
            }
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SimpleRadioButton.Checked)
            {
                string s = listBox1.SelectedItem.ToString();
                string[] items = s.Split('/');
                int num = int.Parse(items[0]);
                int sec = int.Parse(items[1]);
                _section = sec;
                _number = num;
                SectionComboBox.SelectedItem = sec;
                NumNumericUpDown.Value = num;
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            AllRadioButton.Checked = true;
            float f = (float)LimitNumericUpDown.Value;

            if (SimpleRadioButton.Checked)
            {
                var rstlists = new List<(int num, int sect, string bagod, float predic, float chai)>();
                _badIndex = new List<int>();
                for (int i = 1; i <= 45; i++)
                {
                    var rstlist = new List<(int sect, double predic, double chai, double abs, double rsqr)>();
                    for (int j = 0; j < SectionComboBox.Items.Count; j++)
                    {
                        int sec = Convert.ToInt32(SectionComboBox.Items[j]);
                        var (ords, sqrs) = BetweenGapOfDangData(i, sec);
                        var model = new ScottPlot.Statistics.LinearRegressionLine(ords, sqrs);
                        double nxtpredic = (_lastOrder + 1) * model.slope + model.offset;
                        double gap = sqrs[^1] + 1.0 - nxtpredic;
                        rstlist.Add((sec, nxtpredic, gap, Math.Abs(gap), model.rSquared));
                    }
                    var finds = rstlist.Where(x => x.rsqr >= f);
                    if (finds?.Any() ?? false)
                    {
                        
                        //차이가 양수중 최대값
                        var yanss = finds.Where(x => x.chai > 2);
                        if (yanss?.Any() ?? false)
                        {
                            var yans = yanss.OrderByDescending(x => x.chai).First();
                            rstlists.Add((i, yans.sect, "악번", (float)yans.predic, (float)yans.chai));
                            _badIndex.Add(i);
                        }

                        //차이가 최소인 최적값
                        var peftmps = finds.Where(x => x.abs <= 2);
                        if (peftmps?.Any() ?? false)
                        {
                            var (sect, predic, chai, abs, rsqr) = peftmps.OrderBy(x => x.abs).First();
                            rstlists.Add((i, sect, "최적", (float)predic, (float)chai));
                        }

                        //차이가 음수중 최대값
                        var emuss = finds.Where(x => x.chai < -2);
                        if (emuss?.Any() ?? false)
                        {
                            var emus = emuss.OrderBy(x => x.chai).First();
                            rstlists.Add((i, emus.sect, "호번", (float)emus.predic, (float)emus.chai));
                        }
                    }
                }

                if (rstlists?.Any() ?? false)
                {
                    var ordlists = rstlists.OrderBy(x => x.num);
                    _allvalues = new(ordlists);
                    foreach (var (num, sect, bagod, predic, chai) in ordlists)
                    {
                        string s = $"{num:00}/{sect:000}/{bagod}/{predic:0.0000}/{chai:0.0000}";
                        listBox1.Items.Add(s);
                    }
                }
            }
            else
            {
                listBox1.Items.Clear();
                var secalldatas = SkipedAllDangbeon();
                var ems = Enumerable.Range(0, 3);
                var rsts = new List<(int[] arr, double predic, double chai)>();
                foreach (int[] sequence in LuckyLibrary.NewSequenceLists().Values)
                {
                    var divids = LuckyLibrary.HorizontalFlowInts(sequence, 15);
                    foreach (int[] divid in divids)
                    {
                        var eachs = new List<double[]>();
                        foreach (int[] array in secalldatas)
                        {
                            var each = divid.Select(x => (double)array[x - 1]).ToArray();
                            eachs.Add(each);
                        }
                        var sqns = Enumerable.Range(1, eachs.Count).Select(x => (double)x).ToArray();
                        var p = MultipleRegression.QR(eachs.ToArray(), sqns, intercept: true);
                        double d = _lastOrder + 1.0;
                        double predict = p.Skip(1).Select(x => d * x).Sum() + p[0];
                        double chai = sqns[^1] + 1.0 - predict;
                        var pts = new List<double>();
                        foreach (double[] item in eachs)
                        {
                            double d1 = ems.Select(idx => item[idx] * p[idx + 1]).Sum() + p[0];
                            pts.Add(d1);
                        }
                        double rsquared = GoodnessOfFit.RSquared(pts, sqns);
                        if (rsquared > f)
                        {
                            rsts.Add((divid, predict, chai));
                        }
                    }
                }

                var ords = rsts.OrderBy(x => Math.Abs(x.chai));

                foreach (var item in ords)
                {
                    string s = $"{string.Join(",", item.arr.Select(x => x.ToString("00")))}/{item.predic:0.0000}/{item.chai:0.0000}";
                    listBox1.Items.Add(s);
                }
            }
        }




        //********************  메서드  ************************



        /// <summary>
        /// 선형회귀 결과를 출력
        /// </summary>
        private void PresentPlot()
        {
            string path = Application.StartupPath + @"\DataFiles\tmp.png";

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            var plt = new ScottPlot.Plot(813, 600);
            var (ords, sqrs) = BetweenGapOfDangData(_number, _section);
            double x1 = ords[0];
            double x2 = ords[^1];

            var model = new ScottPlot.Statistics.LinearRegressionLine(ords, sqrs);

            string s = "Linear Regression\nY = {model.slope:0.0000}x + {model.offset:0.0} " +
                      $"(R² = {model.rSquared:0.0000})";
            plt.Title(s);
            plt.AddScatter(ords, sqrs, lineWidth: 0);
            plt.AddLine(model.slope, model.offset, (x1, x2), lineWidth: 2);
            plt.SaveFig(path);
            double d = (_lastOrder + 1.0) * model.slope + model.offset;
            double cha = sqrs[^1] + 1.0 - d;
            PredicTextBox.Text = $"예상:{d:0.0000}   차이:{cha:0.0000}";
            using (FileStream stream = new(path, FileMode.Open, FileAccess.Read))
            {
                pictureBox1.Image = Image.FromStream(stream);
                stream.Dispose();
            }
            File.Delete(path);
        }

        /// <summary>
        /// 번호가 나온회차 차이와 순차시퀸스
        /// </summary>
        /// <param name="n">찾을 번호</param>
        /// <param name="sec">검사구간 </param>
        /// <returns>튜플(이전 당번과 차이 정수배열, 순차 정수 시퀸스배열)</returns>
        private static (double[] ords, double[] sqrs) BetweenGapOfDangData(int n, int sec)
        {
            using var context = new LottoDBContext();
            var hoicha = context.BasicTbl
                              .Where(x => x.Gu1 == n || x.Gu2 == n || x.Gu3 == n || x.Gu4 == n || x.Gu5 == n || x.Gu6 == n)
                              .Select(x => x.Orders).ToList();
            var ords = hoicha.Skip(hoicha.Count - sec).Select(x => (double)x).ToArray();
            var sqrs = Enumerable.Range(1, ords.Length).Select(x => (double)x).ToArray();
            return (ords, sqrs);
        }

        /// <summary>
        /// 구간최소 출에 맞춘 전체 출현회차
        /// </summary>
        /// <returns>리스트(회차배열)</returns>
        private static List<int[]> SkipedAllDangbeon()
        {
            var lists = new List<int[]>();

            var cntlst = new List<int>();
            var orders = new List<List<int>>();
            for (int n = 1; n <= 45; n++)
            {
                using var context = new LottoDBContext();
                var hoicha = context.BasicTbl
                                  .Where(x => x.Gu1 == n || x.Gu2 == n || x.Gu3 == n || x.Gu4 == n || x.Gu5 == n || x.Gu6 == n)
                                  .Select(x => x.Orders).ToList();
                cntlst.Add(hoicha.Count);
                orders.Add(hoicha);
            }
            int min = Utility.TruncateValue(cntlst.Min());
            var skips = orders.Select(x => x.Skip(x.Count - min).ToArray()).ToList();
            var pivots = new List<int[]>();
            for (int i = 0; i < min; i++)
            {
                var each = skips.Select(x => x[i]).ToArray();
                pivots.Add(each);
            }

            return pivots;
        }
    }
}
