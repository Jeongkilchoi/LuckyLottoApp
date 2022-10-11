using System.Data;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Entities;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 회기및 주기 검사하는 폼 클래스
    /// </summary>
    public partial class HoeikijukiForm : Form
    {
        #region Field

        private readonly int _lastOrder;
        private int _currentOrder;
        private int _gapCount = 10;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public HoeikijukiForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
        }

        private void HoeikijukiForm_Load(object sender, EventArgs e)
        {
            WayComboBox.SelectedIndex = 1;
            OrderNumericUpDown.Maximum = _lastOrder;
            OrderNumericUpDown.Value = _lastOrder;

            ResultListView.Columns.Add("회 차", 50, HorizontalAlignment.Left);
            for (int i = 1; i <= 6; i++)
            {
                string s = i + "구";
                ResultListView.Columns.Add(s, 50, HorizontalAlignment.Center);
            }

            ResultListView.Columns.Add("회 차", 100, HorizontalAlignment.Right);
            for (int i = 1; i <= 6; i++)
            {
                string s = i + "구";
                ResultListView.Columns.Add(s, 50, HorizontalAlignment.Center);
            }
            ResultListView.Columns.Add("출합", 50, HorizontalAlignment.Center);
        }

        private void OrderNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _currentOrder = (int)OrderNumericUpDown.Value;
            PresentListView();
        }

        private void IntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _gapCount = (int)IntervalNumericUpDown.Value;
            PresentListView();
        }

        private void CountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            PresentListView();
        }

        private void HokiRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PresentListView();
        }

        private void ResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResultListBox.SelectedIndex > -1)
            {
                RealLabel.Text = "후방연속: ";
                MaxLabel.Text = "연속최대: ";

                int n = (int)ResultListBox.SelectedItem;
                IntervalNumericUpDown.Value = n;
            }
        }

        private async void DirectButton_Click(object sender, EventArgs e)
        {
            HokiTextBox.Enabled = true;
            DirectButton.Enabled = false;
            int conlimit = (int)ContinueNumericUpDown.Value;
            int maxCountinue = 10;
            int mark = WayComboBox.SelectedIndex;
            var tpls = new List<(int ord, int idx, int dup, int num)>();
            var revpairs = await GetAllDatas(_currentOrder);

            //회기검사 우측은 늘 동일하니까 먼저 10개 당번 추출
            var targetlist = revpairs.Take(maxCountinue + 1).ToDictionary(x => x.Key, x => x.Value).Values.ToList();//11개

            for (int i = 1; i <= _currentOrder - 20; i++)
            {
                int last = revpairs.Keys.Max() - i;
                int start = last - maxCountinue;
                var lefts = revpairs.Where(x => x.Key >= start && x.Key <= last).Select(x => x.Value).ToList(); //11개

                //중복갯수 파악
                for (int j = 0; j < 6; j++)
                {
                    int dub = 0;
                    for (int k = 1; k < maxCountinue; k++)
                    {
                        var dang = targetlist[k];
                        int a = lefts[k][j];

                        if (dang.Contains(a))
                            dub++;
                        else
                            break;
                    }

                    if ((mark == 0 && dub == conlimit) || (mark == 1 && dub >= conlimit) || (mark == 2 && dub <= conlimit))
                    {
                        int n = lefts[0][j];
                        tpls.Add((i, j, dub, n));
                    }
                }
            }

            var ordertpls = tpls.OrderBy(x => x.dup);
            var nums = tpls.Select(x => x.num).OrderBy(x => x).Select(x => x.ToString("00"));
            var rst = ordertpls.Select(x => $"{x.ord:000}회기 [{x.idx}]: {x.dup} 중복\t상하선 {x.num} 번");
            HokiTextBox.Text = rst.Any() ? string.Join(Environment.NewLine, rst) + "\r\n\r\n" + String.Join(",", nums): "없음";
            HokiTextBox.SelectionStart = HokiTextBox.Text.Length;
            HokiTextBox.ScrollToCaret();
        }

        private async void DiagonalButton_Click(object sender, EventArgs e)
        {
            HokiTextBox.Enabled = true;
            int conlimit = (int)ContinueNumericUpDown.Value;
            int maxCountinue = 10;
            int mark = WayComboBox.SelectedIndex;
            var selNumbers = new List<int>();
            var rstlist = new List<string>();
            var revpairs = await GetAllDatas(_currentOrder);

            //회기검사 우측은 늘 동일하니까 먼저 10개 당번 추출
            var targetlist = revpairs.Take(maxCountinue + 1).ToDictionary(x => x.Key, x => x.Value).Values.ToList();//11개

            for (int i = 1; i <= _currentOrder - 20; i++)
            {
                int last = revpairs.Keys.Max() - i;
                int start = last - maxCountinue;
                var lefts = revpairs.Where(x => x.Key >= start && x.Key <= last).Select(x => x.Value).ToList(); //11개

                var xpos = new List<List<(int r, int c)>>
                {
                    new List<(int r, int c)>() { (1,1), (2,2), (3,3), (4,4), (5,5) },   //0,0
                    new List<(int r, int c)>() { (1,2), (2,3), (3,4), (4,5) },          //0,1
                    new List<(int r, int c)>() { (1,3), (2,4), (3,5) },                 //0,2
                    new List<(int r, int c)>() { (1,4), (2,5) }                         //0,3
                };
                var ypos = new List<List<(int r, int c)>>
                {
                    new List<(int r, int c)>() { (1,4), (2,3), (3,2), (4,1), (5,0) },   //0,5
                    new List<(int r, int c)>() { (1,3), (2,2), (3,1), (4,0) },          //0,4
                    new List<(int r, int c)>() { (1,2), (2,1), (3,0) },                 //0,3
                    new List<(int r, int c)>() { (1,1), (2,0) }                         //0,2
                };

                //좌사선 검사
                for (int p = 0; p < xpos.Count; p++)
                {
                    var pos = xpos[p];
                    int dup = 0;
                    foreach (var (r, c) in pos)
                    {
                        int row = r;
                        int col = c;
                        var dang = targetlist[row];
                        int a = lefts[row][col];

                        if (dang.Contains(a))
                        {
                            dup++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if ((mark == 0 && dup == conlimit) || (mark == 1 && dup >= conlimit) || (mark == 2 && dup <= conlimit))
                    {
                        int n = lefts[0][p];
                        string s = $"{i:000}회기 : {dup} 중복\t좌사선 {n}번";
                        rstlist.Add(s);
                        selNumbers.Add(n);
                    }
                }

                //우사선 검사
                for (int p = 0; p < ypos.Count; p++)
                {
                    var pos = ypos[p];
                    int dup = 0;
                    foreach (var (r, c) in pos)
                    {
                        int row = r;
                        int col = c;
                        var dang = targetlist[row];
                        int a = lefts[row][col];

                        if (dang.Contains(a))
                        {
                            dup++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if ((mark == 0 && dup == conlimit) || (mark == 1 && dup >= conlimit) || (mark == 2 && dup <= conlimit))
                    {
                        int n = lefts[0][5 - p];
                        string s = $"{i:000}회기 : {dup} 중복\t우사선 {n}번";
                        rstlist.Add(s);
                        selNumbers.Add(n);
                    }
                }
            }

            var nums = selNumbers.OrderBy(x => x).Select(x => x.ToString("00"));
            HokiTextBox.Text = rstlist.Any() ? String.Join(Environment.NewLine, rstlist) + "\r\n\r\n" + String.Join(",", nums) : "없음";
            HokiTextBox.SelectionStart = HokiTextBox.Text.Length;
            HokiTextBox.ScrollToCaret();
        }

        private async void GoodButton_Click(object sender, EventArgs e)
        {
            ResultListBox.Enabled = true;
            ResultListBox.Items.Clear();
            RealLabel.Text = "후방연속: ";
            MaxLabel.Text = "연속최대: ";
            PercentTextBox.Text = "";
            WinningTextBox.Text = "";
            int limit = (int)LimitNumericUpDown.Value;
            var rstlist = new List<(int, int)>();
            var revpairs = await GetAllDatas(_currentOrder);

            if (HokiRadioButton.Checked)
            {
                int cnt = revpairs.Count / 2;
                for (int i = 1; i <= cnt; i++)
                {
                    //회기는 회차의 간격은 1 이고 좌우간격은 _gapCount
                    var leftpairs = revpairs.Where(x => x.Key <= revpairs.Keys.First() - i)
                                            .ToDictionary(x => x.Key, x => x.Value);
                    
                    var dups = new List<int>();
                    for (int j = 1; j < 20; j++)
                    {
                        var lkey = leftpairs.ElementAt(j).Key;
                        var rkey = revpairs.ElementAt(j).Key;
                        var lval = leftpairs[lkey];
                        var rval = revpairs[rkey];
                        int dup = lval.Intersect(rval).Count();
                        dups.Add(dup);
                    }
                    var revs = dups.AsEnumerable().Reverse();
                    if (dups[0] == 0)
                    {
                        var (realCount, maxCount) = NextReal.RealMaxCount(revs);
                        if (realCount >= limit && realCount >= maxCount)
                        {
                            rstlist.Add((i, realCount));
                        }
                    }

                    GoodButton.Text = i + "번째 검사중...";
                }
            }
            else
            {
                int cnt = revpairs.Count / 10;
                for (int i = 2; i <= cnt; i++)
                {
                    var ords = new List<int>();
                    for (int j = revpairs.Keys.Max(); j > 0; j -= i)
                    {
                        ords.Add(j);
                    }

                    var rightpairs = revpairs.Where(x => ords.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                    var leftpairs = rightpairs.Skip(1).ToDictionary(x => x.Key, x => x.Value);

                    var dups = new List<int>();
                    for (int j = 1; j < 20; j++)
                    {
                        int lkey = -1, rkey = -1;

                        try
                        {
                            lkey = leftpairs.ElementAt(j).Key;
                            rkey = rightpairs.ElementAt(j).Key;

                            var lval = leftpairs[lkey];
                            var rval = rightpairs[rkey];
                            int dup = lval.Intersect(rval).Count();
                            dups.Add(dup);
                        }
                        catch
                        {
                            break;
                        }
                    }

                    if (dups.Any())
                    {
                        var revs = dups.AsEnumerable().Reverse();
                        if (dups[0] == 0)
                        {
                            var (realCount, maxCount) = NextReal.RealMaxCount(revs);
                            if (realCount >= limit && realCount >= maxCount)
                            {
                                rstlist.Add((i, realCount));
                            }
                        }
                    }
                    GoodButton.Text = i + "번째 검사중...";
                }
            }

            if (rstlist.Any())
            {
                var sorted = rstlist.OrderByDescending(x => x.Item2);

                foreach (var item in sorted)
                {
                    ResultListBox.Items.Add(item.Item1);
                }
            }
            GoodButton.Text = "회기및 주기호번   자동검사";
        }




        //**********************  내부메서드  **********************



        /// <summary>
        /// 좌우 리스트뷰에 결과 출력하기
        /// </summary>
        private void PresentListView()
        {
            ResultListView.Items.Clear();
            
            //당번전체 데이터
            using var db = new LottoDBContext();
            var pair1s = db.BasicTbl.Where(x => x.Orders <= _currentOrder)
                .ToDictionary(x => x.Orders, x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }.ToArray());
            pair1s.Add(_currentOrder + 1, _currentOrder + 1 <= _lastOrder ? Utility.DangbeonOfOrder(_currentOrder + 1) : 
                        Enumerable.Repeat(0, 6).ToArray());
            var allpairs = pair1s.Reverse().ToDictionary(x => x.Key, x => x.Value);
            
            var leftpairs = new Dictionary<int, int[]>();
            var rightpairs = new Dictionary<int, int[]>();
            var cnts = new List<int>();

            if (HokiRadioButton.Checked)
            {
                //회기는 회차의 간격은 1 이고 좌우간격은 _gapCount
                leftpairs = allpairs.Where(x => x.Key <= allpairs.Keys.First() - _gapCount)
                                    .ToDictionary(x => x.Key, x => x.Value);
                rightpairs = allpairs.Take(leftpairs.Count).ToDictionary(x => x.Key, x => x.Value);
            }
            else
            {
                //주기는 회차의 간격은 _gapCount 이고 좌우간격은 1
                var ords = new List<int>();
                for (int i = allpairs.Keys.Max(); i > 0; i -= _gapCount)
                {
                    ords.Add(i);
                }

                rightpairs = allpairs.Where(x => ords.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                leftpairs = rightpairs.Skip(1).ToDictionary(x => x.Key, x => x.Value);
            }

            ResultListView.BeginUpdate();

            for (int i = 0; i < leftpairs.Count; i++)
            {
                var lkey = leftpairs.ElementAt(i).Key;
                var rkey = rightpairs.ElementAt(i).Key;
                var lval = leftpairs[lkey];
                var rval = rightpairs[rkey];
                int dup = lval.Intersect(rval).Count();
                cnts.Add(dup);
                var lvi = new ListViewItem(lkey.ToString()) { UseItemStyleForSubItems = false };
                foreach (var item in lval)
                {
                    lvi.SubItems.Add(item.ToString());
                }

                lvi.SubItems.Add(rkey.ToString());

                foreach (var item in rval)
                {
                    if (item == 0)
                    {
                        lvi.SubItems.Add("?");
                    }
                    else
                    {
                        lvi.SubItems.Add(item.ToString());
                    }
                }

                if (dup >= 1)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (rval.Contains(lval[j]))
                        {
                            lvi.SubItems[j + 1].BackColor = Color.Cyan;
                        }

                        if (lval.Contains(rval[j]))
                        {
                            lvi.SubItems[j + 8].BackColor = Color.Cyan;
                        }
                    }
                }

                lvi.SubItems.Add(i == 0 ? "" : dup.ToString());
                ResultListView.Items.Add(lvi);
            }
            ResultListView.EndUpdate();
            var revs = cnts.AsEnumerable().Reverse();
            var (realCount, maxCount) = NextReal.RealMaxCount(revs);
            int shwcnt = cnts.Count(x => x >= 1);
            double d = shwcnt * 100.0 / cnts.Count;
            string per = "전체갯수/출수:  " + cnts.Count + "/" + shwcnt + "\t출수률: " + d.ToString("F2") + "%";
            PercentTextBox.Text = per;
            RealLabel.Text += realCount;
            MaxLabel.Text += maxCount;
            WinningTextBox.Text = string.Join(",", leftpairs.First().Value.Select(x => x.ToString("00")));
        }

        /// <summary>
        /// 당번전체 데이터
        /// </summary>
        /// <param name="lastOrder">최종회차</param>
        /// <returns>딕셔너리(회차, 당번배열)</returns>
        private async Task<Dictionary<int, int[]>> GetAllDatas(int lastOrder)
        {
            var revpairs = new Dictionary<int, int[]>();
            var task = Task.Run(() =>
            {
                using var db = new LottoDBContext();
                var pairs = db.BasicTbl.Where(x => x.Orders <= lastOrder)
                              .ToDictionary(x => x.Orders, x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }.ToArray());
                pairs.Add(lastOrder + 1, lastOrder + 1 <= _lastOrder ? Utility.DangbeonOfOrder(lastOrder + 1) : Enumerable.Repeat(0, 6).ToArray());
                revpairs = pairs.Reverse().ToDictionary(x => x.Key, x => x.Value);
            });

            await task;
            return revpairs;
        }


    }
}
