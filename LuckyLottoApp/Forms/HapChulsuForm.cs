using System.Data;
using Newtonsoft.Json.Linq;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Geomsas;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 합계출수 데이터 필터 폼 클래스
    /// </summary>
    public partial class HapChulsuForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private int _rowIndex = -1;
        private bool _useListBox = false;
        private int _section = 600;
        private List<GridItem> _gridItems;
        private Dictionary<string, int[]> _chulPairs;
        private Dictionary<string, (int bottom, int top)> _btmtopPairs;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public HapChulsuForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            RemoveToolStripMenuItem.Click += RemoveItemToolStripMenuItem_Click;
            CancelToolStripMenuItem.Click += CancelWorkToolStripMenuItem_Click;
            InitialDataGridView();
        }

        private async void HapChulsuForm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("회차", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("출수", 45, HorizontalAlignment.Center);

            await GetColumnName();

            _gridItems = new List<GridItem>();
            string filePath = Application.StartupPath + @"\GeomsaFiles\hapchulsufilter.json";
            var read = File.ReadAllText(filePath);
            var jObject = JObject.Parse(read);
            var pairs = jObject.ToObject<Dictionary<string, object[]>>();

            foreach (var key in pairs.Keys)
            {
                var obj = pairs[key];

                _gridItems.Add(new GridItem
                {
                    Title = key,
                    Minval = Convert.ToInt32(obj[0]),
                    Maxval = Convert.ToInt32(obj[1]),
                    Except = Convert.ToString(obj[2]),
                    Checked = Convert.ToBoolean(obj[3])
                });
            }

            dataGridView1.DataSource = _gridItems;
        }

        private void CancelWorkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
        }

        private void RemoveItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BadListBox.CheckedItems.Count > 0)
            {
                foreach (var item in BadListBox.CheckedItems.OfType<string>().ToList())
                {
                    BadListBox.Items.Remove(item);
                }
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gridView = (DataGridView)sender;

            try
            {
                if (gridView.SelectedCells.Count > 0 && !_useListBox)
                {
                    int row = dataGridView1.CurrentCell.RowIndex;

                    if (row != _rowIndex)
                    {
                        _rowIndex = row;
                        string key = _chulPairs.ElementAt(_rowIndex).Key;
                        DisplayRowData(key);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;

            if (_rowIndex != row)
            {
                _useListBox = false;
                _rowIndex = row;
                string key = _chulPairs.ElementAt(row).Key;
                DisplayRowData(key);
            }

            if (e.ColumnIndex == 3)
            {
                WriteTojson();
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowidx = e.RowIndex;

            if (rowidx != _rowIndex)
            {
                _useListBox = false;
                _rowIndex = rowidx;
                string key = _chulPairs.ElementAt(rowidx).Key;
                DisplayRowData(key);
            }

            int colidx = e.ColumnIndex;

            if (colidx == 1 || colidx == 2)
            {
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[colidx];

                if (comboBoxCell != null)
                {
                    WriteTojson();
                    dataGridView1.Invalidate();
                }
            }
            else if (colidx == 4)
            {
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[colidx];

                if (checkBoxCell != null)
                {
                    WriteTojson();
                    dataGridView1.Invalidate();
                }
            }
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception != null && e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show(e.Context.ToString());
            }
        }

        private void SectionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _section = (int)SectionNumericUpDown.Value;
            _useListBox = false;
            string key = _chulPairs.ElementAt(_rowIndex).Key;
            DisplayRowData(key);
        }

        private void PerfectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //항목: [극소, 극대, 최소, 최대, 최적소, 최적대, 제외번호 ]
            try
            {
                _gridItems = new List<GridItem>();
                InitialDataGridView();
                string filePath = Application.StartupPath + @"\GeomsaFiles\hapchulsumax.json";
                var read = File.ReadAllText(filePath);
                var jObject = JObject.Parse(read);
                var pairs = jObject.ToObject<Dictionary<string, object[]>>();

                if (PerfectRadioButton.Checked)
                {
                    //최적값
                    foreach (var key in pairs.Keys)
                    {
                        var obj = pairs[key];
                        bool chk = true;

                        if (string.IsNullOrEmpty(Convert.ToString(obj[6])))
                        {
                            int btm = Convert.ToInt32(obj[0]);
                            int top = Convert.ToInt32(obj[1]);
                            int min = Convert.ToInt32(obj[2]);
                            int max = Convert.ToInt32(obj[3]);
                            int low = Convert.ToInt32(obj[4]);
                            int hgh = Convert.ToInt32(obj[5]);

                            if (hgh < 1 || low == hgh || (btm == low && top == hgh))
                            {
                                chk = false;
                            }
                        }

                        _gridItems.Add(new GridItem
                        {
                            Title = key,
                            Minval = Convert.ToInt32(obj[4]),
                            Maxval = Convert.ToInt32(obj[5]),
                            Except = Convert.ToString(obj[6]),
                            Checked = chk
                        });
                    }
                }
                else if (MinMaxRadioButton.Checked)
                {
                    //최소최대
                    foreach (var key in pairs.Keys)
                    {
                        var obj = pairs[key];

                        if (Convert.ToInt32(obj[3]) < 1)
                        {
                            _gridItems.Add(new GridItem
                            {
                                Title = key,
                                Minval = Convert.ToInt32(obj[2]),
                                Maxval = 1,
                                Except = "",
                                Checked = true
                            });
                        }
                        else
                        {
                            _gridItems.Add(new GridItem
                            {
                                Title = key,
                                Minval = Convert.ToInt32(obj[2]),
                                Maxval = Convert.ToInt32(obj[3]),
                                Except = "",
                                Checked = true
                            });
                        }
                    }
                }
                else
                {
                    //초기화
                    foreach (var key in pairs.Keys)
                    {
                        var obj = pairs[key];

                        _gridItems.Add(new GridItem
                        {
                            Title = key,
                            Minval = Convert.ToInt32(obj[0]),
                            Maxval = Convert.ToInt32(obj[1]),
                            Except = "",
                            Checked = true
                        });
                    }
                }

                dataGridView1.DataSource = _gridItems;

                //파일 작성
                WriteTojson();
                MessageBox.Show("파일 작성됨.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GoodistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GoodListBox.SelectedItems.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
                BadListBox.SelectedIndex = -1;
                _useListBox = true;
                UseGridButton.Enabled = true;
                dataGridView1.ClearSelection();
                string key = GoodListBox.SelectedItem.ToString();
                DisplayRowData(key);

                int index = Array.IndexOf(_chulPairs.Keys.ToArray(), key);

                if (index > -1)
                {
                    _useListBox = true;
                    dataGridView1.Rows[index].Selected = true;
                    dataGridView1.Rows[index].Cells[0].Selected = true;
                }
            }
        }

        private void BadListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BadListBox.SelectedIndex > -1)
            {
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
                GoodListBox.SelectedIndex = -1;
                _useListBox = true;
                UseGridButton.Enabled = true;
                dataGridView1.ClearSelection();
                string key = BadListBox.SelectedItem.ToString();
                DisplayRowData(key);

                int index = Array.IndexOf(_chulPairs.Keys.ToArray(), key);

                if (index > -1)
                {
                    _useListBox = true;
                    dataGridView1.Rows[index].Selected = true;
                    dataGridView1.Rows[index].Cells[0].Selected = true;
                }
            }
        }

        private void UseGridButton_Click(object sender, EventArgs e)
        {
            _useListBox = false;
            UseGridButton.Enabled = false;
        }

        private void AddFilterButton_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\GeomsaFiles\jeoefilter.json";

            if (BadListBox.Items.Count > 0)
            {
                int passcount = 0;
                var read = File.ReadAllText(path);
                var jObject = JObject.Parse(read);

                var jsonPairs = jObject.ToObject<Dictionary<string, object[]>>();
                var leftjson = jsonPairs.Where(x => Convert.ToString(x.Value[0]) != "" && Convert.ToString(x.Value[1]) != "")
                                        .ToDictionary(x => x.Key, x => x.Value);

                //기록된 전체 항목리스트
                var lists = leftjson.Values.Select(x => LuckyLibrary.StringToList(Convert.ToString(x[1])));
                var (key, count) = LastRecordJson(path);
                int cnt = (count == 0) ? 1 : int.Parse(key) + 1;

                for (int i = 0; i < BadListBox.Items.Count; i++)
                {
                    int n = cnt + i;
                    string s = n.ToString();

                    string item = (string)BadListBox.Items[i];
                    var each = _chulPairs[item];
                    var same = lists.Where(x => x.Count == each.Length).Select(x => x);
                    bool pass = !same.Any() || same.All(x => x.Intersect(each).Count() != each.Length);

                    if (pass)
                    {
                        int length = each.Length >= 6 ? 6 : each.Length;
                        string data = string.Join(",", each);
                        int max = length - 1;

                        //해당 배열을 불러와 수정하기
                        var jarr = (JArray)jObject[s];
                        jarr[0] = item;
                        jarr[1] = data;
                        jarr[2] = 0;
                        jarr[3] = max;
                        jarr[4] = true;
                        passcount++;
                    }
                }

                File.WriteAllText(path, jObject.ToString());

                if (passcount > 0)
                {
                    MessageBox.Show("파일 수정됨.");
                }
            }
        }

        private async void LimitWriteButton_Click(object sender, EventArgs e)
        {
            //항목: [극소, 극대, 최소, 최대, 최적소, 최적대, 제외번호]
            //극소: 이론적 하한값   극대: 이론적 상한값
            //최소: sql에서 하한값     최대: sql에서 상한값
            //최적소:구간내 최적하한값     최적대:구간내 최적상한값
            //제외번호: 최소와 최대 사이에서 제외할 번호

            try
            {
                var tg = new Tonggwa();
                JObject jObject = new();

                for (int i = 0; i < _chulPairs.Count; i++)
                {
                    string key = _chulPairs.ElementAt(i).Key;

                    int[] vals = _chulPairs[key][^_section..];
                    var (bottom, top) = _btmtopPairs[key];

                    //var (lowValue, highValue, exceptList) = await Task.Run(() => NextReal.FindMinMaxExceptList(vals));
                    var (lowerval, upperval, explist) = await Task.Run(() => tg.ExtractedCollection(vals, key));
                    JArray jArray1 = new(bottom, top, vals.Min(), vals.Max(), lowerval, upperval, string.Join(",", explist));
                    jObject.Add(key, jArray1);
                }

                string path = Application.StartupPath + @"\GeomsaFiles\hapchulsumax.json";
                File.WriteAllText(path, jObject.ToString());
                PerfectRadioButton_CheckedChanged(sender, e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void GoodBadButton_Click(object sender, EventArgs e)
        {
            var tg = new Tonggwa();
            Cursor = Cursors.WaitCursor;
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
            GoodListBox.Items.Clear();
            BadListBox.Items.Clear();
            _useListBox = false;
            Range range = (_lastOrder - _section)..;

            foreach (var key in _chulPairs.Keys)
            {
                GoodBadButton.Text = key + " 검사중...";
                int[] chuls = _chulPairs[key][range];
                var pairs = await Task.Run(() => tg.CheckedTongka(chuls, key));
                var rstlst = pairs.Values.ToList();
                if (!(rstlst.Any(x => x == -1) && rstlst.Any(x => x == 1)))
                {
                    if (rstlst.Any(x => x == 1))
                    {
                        GoodListBox.Items.Add(key);
                    }
                    else if (rstlst.Any(x => x == -1))
                    {
                        BadListBox.Items.Add(key);
                    }
                }
                //int n = await Task.Run(() => NextReal.FindVerticalFiveData(chuls, 5));
                //var tg = new Tonggwa();
                //var t = tg.ExtractedCollection(chuls, key);
                //if (n != 0)
                //{
                //    if (n == 1)
                //        GoodListBox.Items.Add(key);
                //    else
                //        BadListBox.Items.Add(key);
                //}
                //else
                //{
                //    int num = await Task.Run(() => NextReal.FindBadGoodNumber(chuls));

                //    if (num != 0)
                //    {
                //        if (num == 1)
                //            GoodListBox.Items.Add(key);
                //        else
                //            BadListBox.Items.Add(key);
                //    }
                //}
            }

            GoodBadButton.Text = "호번, 악번 찾기";
            Cursor = Cursors.Default;
            AddFilterButton.Enabled = true;
            UseGridButton.Enabled = true;
        }




        //**************************  메서드  ***************************



        /// <summary>
        /// DataGridView 초기화
        /// </summary>
        private void InitialDataGridView()
        {
            dataGridView1.Columns.Clear();

            DataGridViewTextBoxColumn textBoxColumn = new()
            {
                Name = "Title",
                Width = 90,
                HeaderText = "열이름",
                DataPropertyName = "Title",
                ReadOnly = true,
                Visible = true
            };

            dataGridView1.Columns.Add(textBoxColumn);

            DataGridViewComboBoxColumn comboBoxColumn = new()
            {
                Name = "Minval",
                FlatStyle = FlatStyle.Flat,
                Width = 60,
                HeaderText = "최소",
                DataPropertyName = "Minval",
                ReadOnly = false,
                Visible = true
            };

            Enumerable.Range(0, 160).ToList().ForEach(x => comboBoxColumn.Items.Add(x));
            dataGridView1.Columns.Add(comboBoxColumn);

            DataGridViewComboBoxColumn comboBoxColumn1 = new()
            {
                Name = "Maxval",
                FlatStyle = FlatStyle.Flat,
                Width = 60,
                HeaderText = "최대",
                DataPropertyName = "Maxval",
                ReadOnly = false,
                Visible = true
            };

            Enumerable.Range(0, 250).ToList().ForEach(x => comboBoxColumn1.Items.Add(x));
            dataGridView1.Columns.Add(comboBoxColumn1);

            DataGridViewTextBoxColumn textBoxColumn1 = new()
            {
                Name = "Except",
                Width = 200,
                HeaderText = "제외",
                DataPropertyName = "Except",
                ReadOnly = false,
                Visible = true
            };

            dataGridView1.Columns.Add(textBoxColumn1);

            DataGridViewCheckBoxColumn checkBoxColumn = new()
            {
                Name = "Checked",
                HeaderText = "체크",
                Width = 40,
                DataPropertyName = "Checked"
            };
            dataGridView1.Columns.Add(checkBoxColumn);

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.MultiSelect = false;

            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        /// <summary>
        /// 레이블 초기화
        /// </summary>
        private void InitialLabel()
        {
            NowRealLabel.Text = "후방연속: ";
            NowMaxLabel.Text = "연속최대: ";
            SameCountLabel.Text = "동수출: ";
            NonCountLabel.Text = "무출수: ";
            ShownCountLabel.Text = "유출수: ";
            PercentLabel.Text = "출수률: ";
            NextRealLabel.Text = "후방연속: ";
            NextMaxLabel.Text = "연속최대: ";
            Real1Label.Text = "후방연속: ";
            Max1Label.Text = "연속최대: ";
            Real2Label.Text = "후방연속: ";
            Max2Label.Text = "연속최대: ";
            Real3Label.Text = "후방연속: ";
            Max3Label.Text = "연속최대: ";
            Real4Label.Text = "후방연속: ";
            Max4Label.Text = "연속최대: ";
            Real5Label.Text = "후방연속: ";
            Max5Label.Text = "연속최대: ";
            NextChulsuTextBox.Text = string.Empty;
            Next1TextBox.Text = string.Empty;
            Next2TextBox.Text = string.Empty;
            Next3TextBox.Text = string.Empty;
            Next4TextBox.Text = string.Empty;
            Next5TextBox.Text = string.Empty;
            NextChulsuTextBox.BackColor = SystemColors.Window;
            panel4.BackColor = SystemColors.Control;
            NowRealLabel.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// 데이터를 화면에 출력
        /// </summary>
        /// <param name="key">검사항목</param>
        private async void DisplayRowData(string key)
        {
            try
            {
                listView1.Items.Clear();
                InitialLabel();
                Range range = (_lastOrder - _section)..;
                int[] chuls = _chulPairs[key][range];
                var tpl = _btmtopPairs[key];

                PresentListView(chuls);

                var tg = new Tonggwa();
                var pairs = await Task.Run(() => tg.CheckedTongka(chuls, key));//await Task.Run(() => tg.CheckedPass(chuls, tpl));

                NowRealLabel.Text += tg.RealContinue;
                NowMaxLabel.Text += tg.MaxContinue;
                SameCountLabel.Text += tg.LastValueCount;

                if (tpl.top < 11)
                {
                    NonCountLabel.Text += tg.ZeroCount;
                    ShownCountLabel.Text += tg.NoneCount;
                    double d = chuls.Count(x => x != 0) * 100.0 / _section;
                    PercentLabel.Text += tg.Percentage.ToString("F2") + "%";
                }

                NextChulsuTextBox.Text = ResultChulsuData(tg.NextList, tpl);
                if (tg.NextList.Any())
                {
                    var (realCount, maxCount) = NextReal.RealMaxCount(tg.NextList);
                    NextRealLabel.Text += realCount;
                    NextMaxLabel.Text += maxCount;
                }

                if (tg.RealFiveLists.Any())
                {
                    ResultCombineData(tg.RealFiveLists, tpl);

                    var tonglist = pairs.Values.ToList();
                    if (tonglist.Any(x => x == 1))
                    {
                        foreach (string item in pairs.Where(x => x.Value == 1).Select(x => x.Key))
                        {
                            switch (item)
                            {
                                case "연속무출":
                                case "연속진가":
                                case "구간출합":
                                    NowRealLabel.BackColor = Color.Cyan;
                                    break;
                                case "후방연속":
                                case "후방진가":
                                    panel4.BackColor = Color.Cyan;
                                    break;
                                case "후방다음":
                                case "후방간격":
                                    NextChulsuTextBox.BackColor = Color.Cyan;
                                    break;
                                case "후방패턴":
                                    Next1TextBox.BackColor = Color.Cyan;
                                    break;
                            }
                        }
                    }
                    if (tonglist.Any(x => x == -1))
                    {
                        foreach (string item in pairs.Where(x => x.Value == -1).Select(x => x.Key))
                        {
                            switch (item)
                            {
                                case "한계초과":
                                case "연속무출":
                                case "연속진가":
                                case "구간출합":
                                    NowRealLabel.BackColor = Color.LightSalmon;
                                    break;
                                case "후방연속":
                                case "후방진가":
                                    panel4.BackColor = Color.LightSalmon;
                                    break;
                                case "후방다음":
                                case "후방간격":
                                    NextChulsuTextBox.BackColor = Color.LightSalmon;
                                    break;
                                case "후방패턴":
                                    Next1TextBox.BackColor = Color.LightSalmon;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 데이터의 검사결과
        /// </summary>
        /// <param name="list">리스트</param>
        /// <param name="tpl">튜플(극저값,극고값)</param>
        /// <param name="skipCount">출수표시 스킵갯수</param>
        /// <returns>결과 문자열</returns>
        private static string ResultChulsuData(List<int> list, (int bottom, int top) tpl, int skipCount = 100)
        {
            string s = string.Empty;

            if (!list.Any())
            {
                return string.Empty;
            }
            else
            {
                var dups = new List<(int chul, int dup)>();
                var groups = list.GroupBy(x => x).Where(g => g.Any()).Select(g => g.Key);
                for (int i = groups.Min(); i <= groups.Max(); i++)
                {
                    int n = list.Count(x => x == i);
                    dups.Add((i, n));
                }

                if (dups.Any())
                {
                    string s1 = string.Join(",  ", dups.Select(x => $"{x.chul}({x.dup})"));
                    double d = dups.Where(x => x.chul > 0).Sum(x => x.dup) * 100.0 / list.Count;
                    s1 += (tpl.top < 11) ? ",  출률: " + d.ToString("F2") + "%\r\n" : "\r\n";
                    s = s1 + string.Join("-", list.Skip(list.Count - skipCount));
                    return s;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 결합 데이터의 검사결과 출력
        /// </summary>
        /// <param name="lists">리스트</param>
        private void ResultCombineData(List<(int realCount, int maxCount, List<int> nextLis)> lists, (int bottom, int top) tpl)
        {
            Real1Label.Text += lists[0].realCount;
            Max1Label.Text += lists[0].maxCount;
            Next1TextBox.Text = ResultChulsuData(lists[0].nextLis, tpl, 33);

            Real2Label.Text += lists[1].realCount;
            Max2Label.Text += lists[1].maxCount;
            Next2TextBox.Text = ResultChulsuData(lists[1].nextLis, tpl, 33);

            Real3Label.Text += lists[2].realCount;
            Max3Label.Text += lists[2].maxCount;
            Next3TextBox.Text = ResultChulsuData(lists[2].nextLis, tpl, 33);

            Real4Label.Text += lists[3].realCount;
            Max4Label.Text += lists[3].maxCount;
            Next4TextBox.Text = ResultChulsuData(lists[3].nextLis, tpl, 33);

            Real5Label.Text += lists[4].realCount;
            Max5Label.Text += lists[4].maxCount;
            Next5TextBox.Text = ResultChulsuData(lists[4].nextLis, tpl, 33);
        }

        /// <summary>
        /// 리스트뷰에 구간출수를 출력
        /// </summary>
        /// <param name="chuls">출수 데이터</param>
        private void PresentListView(int[] chuls)
        {
            int ord = _lastOrder - _section;
            listView1.BeginUpdate();

            foreach (int chul in chuls)
            {
                ord++;
                var lvi = new ListViewItem(ord.ToString()) { UseItemStyleForSubItems = false };
                lvi.SubItems.Add(chul.ToString());

                if (chul == chuls[^1])
                {
                    lvi.BackColor = Color.Cyan;
                    lvi.SubItems[1].BackColor = Color.Cyan;
                }

                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();
            listView1.EnsureVisible(_section - 1);
        }

        /// <summary>
        /// 그리드뷰의 변경사항을 json 파일에 작성
        /// </summary>
        private void WriteTojson()
        {
            try
            {
                string filePath = Application.StartupPath + @"\GeomsaFiles\hapchulsufilter.json";
                JObject jObject = new();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var data = new List<object>();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        var val = cell.Value;
                        data.Add(val);
                    }

                    string key = (string)data[0];
                    JArray jArray = new(data.Skip(1).ToArray());
                    jObject.Add(key, jArray);
                }

                File.WriteAllText(filePath, jObject.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 검사에 사용할 출수 데이터와 번호 데이터를 필드에 저장
        /// </summary>
        /// <returns></returns>
        private async Task GetColumnName()
        {
            _chulPairs = new Dictionary<string, int[]>();
            _btmtopPairs = new Dictionary<string, (int bottom, int top)>();

            var task = Task.Run(() =>
            {
                string[] basicname =
                {
                    "Gu1", "Gu2", "Gu3", "Gu4", "Gu5", "Gu6", "Bonus",
                    "SunGu1", "SunGu2", "SunGu3", "SunGu4", "SunGu5", "SunGu6"
                };

                int[] basicmin = { 01, 02, 03, 04, 05, 10, 01, 01, 01, 01, 01, 01, 01 };
                int[] basicmax = { 40, 41, 42, 43, 44, 45, 45, 45, 45, 45, 45, 45, 45 };
                int basicnt = 0;
                foreach (var item in basicname)
                {
                    var list = new List<int>();
                    string chul1query = $"SELECT {item} FROM BasicTbl";
                    using var context = new LottoDBContext();
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = chul1query;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int n = reader.GetInt32(0);
                        list.Add(n);
                    }
                    _chulPairs.Add(item, list.ToArray());
                    _btmtopPairs.Add(item, (basicmin[basicnt], basicmax[basicnt]));
                    basicnt++;
                }

                string[] chulnames =
                {
                    "Danhap1", "Danhap2", "Danhap3", "Danhap4", "Danhap5", "Danhap6", "Danhap7",
                    "Danhap8", "Danhap9", "Danhap10", "Danhap11", "Danhap12",
                    "Sunhap1", "Sunhap2", "Sunhap3", "Sunhap4", "Sunhap5", "Sunhap6", "Sunhap7",
                    "Sunhap8", "Sunhap9", "Sunhap10", "Sunhap11", "Sunhap12",
                    "Hapgey", "Aphap", "Dwihap", "Apdwihap", "Gojeocha", "Gojeohap",
                    "Dgapcha1", "Dgapcha2", "Dgapcha3", "Dgapcha4", "Dgapcha5",
                    "Sgapcha1", "Sgapcha2", "Sgapcha3", "Sgapcha4", "Sgapcha5",
                    "Dgaphap1", "Dgaphap2", "Dgaphap3", "Dgaphap4", "Dgaphap5",
                    "Sgaphap1","Sgaphap2", "Sgaphap3", "Sgaphap4", "Sgaphap5",
                    "Dankkeut1", "Dankkeut2", "Dankkeut3", "Dankkeut4", "Dankkeut5", "Dankkeut6",
                    "Sunkkeut1", "Sunkkeut2", "Sunkkeut3", "Sunkkeut4", "Sunkkeut5", "Sunkkeut6",
                    "Acval1", "Acval2", "Acval3", "Acval4",
                    "Dway1", "Dway2", "Dway3", "Dway4", "Dway5",
                    "Sway1", "Sway2", "Sway3", "Sway4", "Sway5",
                    "Updw1", "Updw2", "Updw3", "Updw4", "Updw5", "Updw6", "Updw7", "Updw8", "Updw9"
                };

                int[] chulmin = 
                { 
                    003, 003, 003, 003, 003, 003, 003, 004, 004, 004, 004, 004, 003, 003, 003, 003, 003, 003, 003, 003, 003, 003, 003, 003,
                    025, 003, 003, 005, 005, 005, 001, 001, 001, 001, 001, 001, 001, 001, 001, 001, 003, 003, 003, 003, 003, 003, 003, 003, 003, 003,
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 00, 00, 00, 00, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                };
                int[] chulmax = 
                { 
                    089, 089, 089, 089, 089, 089, 108, 150, 150, 150, 150, 150, 089, 089, 089, 089, 089, 089, 150, 150, 150, 150, 150, 150,
                    250, 054, 054, 099, 044, 085, 044, 044, 044, 044, 044, 044, 044, 044, 044, 044, 089, 089, 089, 089, 089, 089, 089, 089, 089, 089,
                    9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 3, 3, 3, 3, 3, 3, 3, 3, 3
                };
                int cnt = 0;
                foreach (var item in chulnames)
                {
                    var list = new List<int>();
                    string chul1query = $"SELECT {item} FROM ChulsuTbl";
                    using var context = new LottoDBContext();
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = chul1query;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int n = reader.GetInt32(0);
                        list.Add(n);
                    }
                    _chulPairs.Add(item, list.ToArray());
                    _btmtopPairs.Add(item, (chulmin[cnt], chulmax[cnt]));
                    cnt++;
                }

                var lists = new List<int[]>();
                using (var context = new LottoDBContext())
                {
                    string chul1query = $"SELECT Dgap FROM NonChulsuTbl";
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = chul1query;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string s = reader.GetString(0).Trim();
                        int[] arr = ConvertToArray(s);
                        lists.Add(arr);
                    }
                    context.Database.CloseConnection();
                }
                //pivot
                for (int i = 0; i < lists[0].Length; i++)
                {
                    var each = lists.Select(x => x.ElementAt(i)).ToArray();
                    string s = $"Dgap_{i}";
                    _chulPairs.Add(s, each);
                    _btmtopPairs.Add(s, (0, 3));
                }
            });

            await task;
        }

        /// <summary>
        /// 입력 문장을 정수배열로 바꾸기
        /// </summary>
        /// <param name="s">입력 문장</param>
        /// <returns>문장을 정수로 바꾼것을 배열로 반환</returns>
        private static int[] ConvertToArray(string s)
        {
            var array = s.Select(x => int.Parse(x.ToString())).ToArray();

            return array;
        }

        /// <summary>
        /// 제외필터에 정상기록된 최종 튜플
        /// </summary>
        /// <param name="path">파일 경로</param>
        /// <returns>(항목: 갯수) 튜플</returns>
        private static (string key, int count) LastRecordJson(string path)
        {
            var read = File.ReadAllText(path);
            var jObject = JObject.Parse(read);
            var pairs = jObject.ToObject<Dictionary<string, object[]>>();

            var find = pairs.Where(x => Convert.ToString(x.Value[0]) != "" && Convert.ToString(x.Value[1]) != "" &&
                                        Convert.ToBoolean(x.Value[4]) == true)
                            .ToDictionary(x => x.Key, x => x.Value);

            if (find.Count == 0)
            {
                return ("", 0);
            }
            else
            {
                return (find.Last().Key, find.Count);
            }
        }


        /// <summary>
        /// 그리드 항목 (내부 클래스)
        /// </summary>
        private class GridItem
        {
            /// <summary>
            /// 항목이름
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 최소값
            /// </summary>
            public int Minval { get; set; }

            /// <summary>
            /// 최대값
            /// </summary>
            public int Maxval { get; set; }

            /// <summary>
            /// 제외리스트
            /// </summary>
            public string Except { get; set; }

            /// <summary>
            /// 체크여부 (1: 체크됨, 0: 체크안됨)
            /// </summary>
            public bool Checked { get; set; }
        }


    }
}
