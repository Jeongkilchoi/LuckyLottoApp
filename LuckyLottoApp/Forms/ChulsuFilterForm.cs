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
    /// 고정데이터 필터 폼 클래스
    /// </summary>
    public partial class ChulsuFilterForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private int _rowIndex = -1;
        private bool _useListBox = false;
        private int _section = 600;
        private List<GridItem> _gridItems;
        private Dictionary<string, int[]> _chulPairs;
        private Dictionary<string, int[]> _numsPairs;
        private Dictionary<string, (int bottom, int top)> _btmtopPairs;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public ChulsuFilterForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            RemoveToolStripMenuItem.Click += RemoveItemToolStripMenuItem_Click;
            CancelToolStripMenuItem.Click += CancelWorkToolStripMenuItem_Click;
            InitialDataGridView();
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
                        string key = _numsPairs.ElementAt(_rowIndex).Key;
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
                string key = _numsPairs.ElementAt(row).Key;
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
                string key = _numsPairs.ElementAt(rowidx).Key;
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
            string key = _numsPairs.ElementAt(_rowIndex).Key;
            DisplayRowData(key);
        }

        private async void KojeongFilterForm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("회차", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("출수", 45, HorizontalAlignment.Center);

            await GetColumnName();

            _gridItems = new List<GridItem>();
            string filePath = Application.StartupPath + @"\GeomsaFiles\jeongchulsufilter.json";
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

        private void PerfectRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //항목: [극소, 극대, 최소, 최대, 최적소, 최적대, 제외번호 ]
            try
            {
                _gridItems = new List<GridItem>();
                InitialDataGridView();
                string filePath = Application.StartupPath + @"\GeomsaFiles\jeongchulsumax.json";
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

                int index = Array.IndexOf(_numsPairs.Keys.ToArray(), key);

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

                int index = Array.IndexOf(_numsPairs.Keys.ToArray(), key);

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
                    var each = _numsPairs[item];
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

                for (int i = 0; i < _numsPairs.Count; i++)
                {
                    string key = _numsPairs.ElementAt(i).Key;

                    int[] nums = _numsPairs[key].Where(x => 1 <= x && x <= 45).OrderBy(x => x).ToArray();
                    int[] vals = _chulPairs[key][^_section..];
                    int btm = _btmtopPairs[key].bottom;
                    int top = _btmtopPairs[key].top;
                    int min = vals.Min() < 0 ? 0 : vals.Min();

                    var (lowerval, upperval, explist) = await Task.Run(() => tg.ExtractedCollection(vals, nums.Length, key));
                    JArray jArray1 = new(btm, top, min, vals.Max(), lowerval, upperval, string.Join(",", explist));
                    jObject.Add(key, jArray1);
                }

                string path = Application.StartupPath + @"\GeomsaFiles\jeongchulsumax.json";
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
                var nums = _numsPairs[key];

                if (nums.Length > 1 && nums.Length <= 15)
                {
                    //var pairs = await Task.Run(() => tg.CheckedPass(chuls));
                    var pairs = await Task.Run(() => tg.CheckedTongka(chuls, key));
                    var tonglist = pairs.Values.ToList();

                    if (!(tonglist.Any(x => x == -1) && tonglist.Any(x => x == 1)))
                    {
                        if (tonglist.Any(x => x == 1))
                        {
                            GoodListBox.Items.Add(key);
                        }
                        else if (tonglist.Any(x => x == -1))
                        {
                            BadListBox.Items.Add(key);
                        }
                    }
                }
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
            DataCountLabel.Text = "데이터 인수갯수: ";
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
            PassLabel.Text = "검사결과: ";
            NumberTextBox.Text = string.Empty;
            NextChulsuTextBox.Text = string.Empty;
            Next1TextBox.Text = string.Empty;
            Next2TextBox.Text = string.Empty;
            Next3TextBox.Text = string.Empty;
            Next4TextBox.Text = string.Empty;
            Next5TextBox.Text = string.Empty;
            NumberTextBox.BackColor = SystemColors.Window;
            NextChulsuTextBox.BackColor = SystemColors.Window;
            panel4.BackColor = SystemColors.Control;
            Next1TextBox.BackColor = SystemColors.Window;
        }

        /*
        * 연속무출: 후방연속 > 연속최대
        * 연속진가: 종출을 0과 1로 바꾼다음 후방연속 > 연속최대
        * 구간출합: 후방 9구간 출합이 10 구간의 출합보다 크거나 작은 경우
        * 후방연속: 후방연속갯수 <= 5 일때 후방연속 >= 연속최대
        * 후방다음: 종출 다음 리스트에서 전부 종출 보다 크거나 작은 경우
        * 후방간격: 후방간격 > 간격최대
        * 후방진가: 후방연속갯수 <= 5 일때 종출을 0과 1로 바꾼다음 후방연속 >= 연속최대
        * 후방패턴: 후방5 모두검사 결과 끝수가 0 혹은 0 아닌 갯수가 3 이상이면
        * 
        */
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
                int[] nums = _numsPairs[key];
                int[] chuls = _chulPairs[key][range];

                PresentListView(chuls);

                NumberTextBox.Text = string.Join(",", nums.OrderBy(x => x).Select(x => x.ToString("00")));
                DataCountLabel.Text += nums.Length;

                var tg = new Tonggwa();
                var pairs = await Task.Run(() => tg.CheckedTongka(chuls, key));

                NowRealLabel.Text += tg.RealContinue;
                NowMaxLabel.Text += tg.MaxContinue;
                SameCountLabel.Text += tg.LastValueCount;
                NonCountLabel.Text += tg.ZeroCount;
                ShownCountLabel.Text += tg.NoneCount;
                PercentLabel.Text += tg.Percentage.ToString("F2") + "%";

                NextChulsuTextBox.Text = ResultChulsuData(tg.NextList);
                if (tg.NextList.Any())
                {
                    var (realCount, maxCount) = NextReal.RealMaxCount(tg.NextList);
                    NextRealLabel.Text += realCount;
                    NextMaxLabel.Text += maxCount;
                }

                ResultCombineData(tg.RealFiveLists);

                var tonglist = pairs.Values.ToList();
                if (tonglist.Any(x => x == 1))
                {
                    var find = pairs.Where(x => x.Value == 1).Select(x => x.Key);
                    PassLabel.Text += string.Join(", ", find);
                    foreach (string item in pairs.Where(x => x.Value == 1).Select(x => x.Key))
                    {
                        switch (item)
                        {
                            case "연속무출":
                            case "연속진가":
                            case "구간출합":
                                NumberTextBox.BackColor = Color.Cyan;
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
                    var find = pairs.Where(x => x.Value == -1).Select(x => x.Key);
                    PassLabel.Text += string.Join(", ", find);
                    foreach (string item in pairs.Where(x => x.Value == -1).Select(x => x.Key))
                    {
                        switch (item)
                        {
                            case "연속무출":
                            case "연속진가":
                            case "구간출합":
                                NumberTextBox.BackColor = Color.LightSalmon;
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 데이터의 검사결과
        /// </summary>
        /// <param name="list">리스트</param>
        /// <param name="skipCount">출수표시 스킵갯수</param>
        /// <returns>문자열</returns>
        private  static string ResultChulsuData(List<int> list, int skipCount = 100)
        {
            string s = string.Empty;
            var dups = new List<(int chul, int dup)>();
            if (list.Any())
            {
                var groups = list.GroupBy(x => x).Where(g => g.Any()).Select(g => g.Key);
                for (int i = groups.Min(); i <= groups.Max(); i++)
                {
                    int n = list.Count(x => x == i);
                    dups.Add((i, n));
                }

                string s1 = string.Join(",  ", dups.Select(x => $"{x.chul}({x.dup})"));
                double d = dups.Where(x => x.chul > 0).Sum(x => x.dup) * 100.0 / list.Count;
                s1 += ",  출률: " + d.ToString("F2") + "%\r\n";
                s = s1 + string.Join("-", list.Skip(list.Count - skipCount));
                return s;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 결합 데이터의 검사결과 출력
        /// </summary>
        /// <param name="lists">리스트</param>
        private void ResultCombineData(List<(int realCount, int maxCount, List<int> nextLis)> lists)
        {
            Real1Label.Text += lists[0].realCount;
            Max1Label.Text += lists[0].maxCount;
            Next1TextBox.Text = ResultChulsuData(lists[0].nextLis, 33);

            Real2Label.Text += lists[1].realCount;
            Max2Label.Text += lists[1].maxCount;
            Next2TextBox.Text = ResultChulsuData(lists[1].nextLis, 33);

            Real3Label.Text += lists[2].realCount;
            Max3Label.Text += lists[2].maxCount;
            Next3TextBox.Text = ResultChulsuData(lists[2].nextLis, 33);

            Real4Label.Text += lists[3].realCount;
            Max4Label.Text += lists[3].maxCount;
            Next4TextBox.Text = ResultChulsuData(lists[3].nextLis, 33);

            Real5Label.Text += lists[4].realCount;
            Max5Label.Text += lists[4].maxCount;
            Next5TextBox.Text = ResultChulsuData(lists[4].nextLis, 33);
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
                string filePath = Application.StartupPath + @"\GeomsaFiles\jeongchulsufilter.json";
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
            //먼저 각자 출수로 검사하는 항목
            //[ChulsuTbl]"Beisu4", "Beisu5", "Beisu6", "Beisu7", "Beisu8", "Beisu9", "Jekobsu", "Ihangsu", "Pivosu", "Revpivo", "Donhyeong"
            //[ChulsuTbl]"Daluksu", "Ihutsu", "Binsu", "Autsu", "Cansu", "Samkak1", "Samkak2", "Samkak3", "Samkak4",
            //[NonChulsuTbl]"Nine", "Nonthree", "Nonfive", "Dgap"
            //짝을 이루어 나오는 출수로 검사하는 항목
            //[FixChulsuTbl]"Lowhigh", "Oddeven", "Innout", "Topleft", "LDiagonal", "RDiagonal", "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon"
            //[ChulsuTbl]"Hotsu", "Warmsu", "Coldsu", "Snaksu1", "Snaksu2", "Snaksu3", "Snaksu4", "Snaksu5", "Snaksu6", "Rnaksu1", "Rnaksu2", "Rnaksu3", "Rnaksu4", "Rnaksu5", "Rnaksu6"
            

            _chulPairs = new Dictionary<string, int[]>();
            var (numpair, btmpair) = await Task.Run(DataOfColumn);
            _numsPairs = numpair;
            _btmtopPairs = btmpair;

            var task = Task.Run(() =>
            {
                //여기서부터는 자신의 번호를 갖고 있다
                string[] chulnames1 =
                {
                    "Beisu4", "Beisu5", "Beisu6", "Beisu7", "Beisu8", "Beisu9",
                    "Jekobsu", "Ihangsu", "Pivosu", "Revpivo", "Donhyeong",
                    "Daluksu", "Ihutsu", "Binsu", "Autsu", "Cansu", "Samkak1", "Samkak2", "Samkak3", "Samkak4"
                };

                var chul1list = new List<int[]>();
                using (var context = new LottoDBContext())
                {
                    var command = context.Database.GetDbConnection().CreateCommand();
                    string chul1query = $"SELECT {string.Join(",", chulnames1)} FROM ChulsuTbl";
                    command.CommandText = chul1query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int[] array = new int[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            array[i] = reader.GetInt32(i);
                        }
                        chul1list.Add(array);
                    }
                    context.Database.CloseConnection();
                }
                //pivot
                for (int i = 0; i < chul1list[0].Length; i++)
                {
                    string key = chulnames1[i];
                    var each = chul1list.Select(x => x[i]).ToArray();
                    _chulPairs.Add(key, each);
                }

                string[] nonchuls = { "Nine", "Nonthree", "Nonfive" };

                foreach (string item in nonchuls)
                {
                    string key = item.Equals("Nine") ? "Nine" : item.Equals("Nonthree") ? "NThree" : "NFive";
                    var lists = new List<int[]>();
                    using var context = new LottoDBContext();
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    string chul1query = $"SELECT {item} FROM NonChulsuTbl";
                    cmd.CommandText = chul1query;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string s1 = reader.GetString(0).Trim();
                        var arr = ConvertToArray(s1);
                        lists.Add(arr);
                    }
                    //pivot
                    for (int i = 0; i < lists[0].Length; i++)
                    {
                        var arr = lists.Select(x => x.ElementAt(i)).ToArray();
                        string s1 = $"{key}_{i}";
                        _chulPairs.Add(s1, arr);
                    }
                }

                string[] fixednames =
                {
                    "Lowhigh", "Oddeven", "Innout", "Topleft", "LDiagonal", "RDiagonal", "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon"
                };

                foreach (string item in fixednames)
                {
                    var lists = new List<int[]>();
                    using var context = new LottoDBContext();
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    string fixquery = $"SELECT {item} FROM FixChulsuTbl";
                    cmd.CommandText = fixquery;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string s1 = reader.GetString(0).Trim();
                        int[] array = ConvertToArray(s1);
                        lists.Add(array);
                    }
                    //pivot
                    for (int i = 0; i < lists[0].Length; i++)
                    {
                        var each = lists.Select(x => x.ElementAt(i)).ToArray();
                        string s2 = $"{item}_{i}";
                        _chulPairs.Add(s2, each);
                    }
                }

                string[] chulname4 = { "Hotsu", "Warmsu", "Coldsu" };

                for (int i = 0; i < 3; i++)
                {
                    string s2 = $"Hotcold{i + 1}";
                    string name = chulname4[i];
                    var list = new List<int>();
                    string fixquery = $"SELECT {name} FROM ChulsuTbl";
                    using var context = new LottoDBContext();
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = fixquery;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int n = reader.GetInt32(0);
                        list.Add(n);
                    }
                    _chulPairs.Add(s2, list.ToArray());

                    //_btmtopPairs 값을 최대출수 + 1로 변경
                    int max = list.Max() >= 5 ? 6 : list.Max() + 1;
                    var tpl = _btmtopPairs[s2];
                    tpl.top = max;
                    _btmtopPairs[s2] = tpl;
                }
                string[] chulnames2 =
                {
                    "Snaksu1", "Snaksu2", "Snaksu3", "Snaksu4", "Snaksu5", "Snaksu6",
                    "Rnaksu1", "Rnaksu2", "Rnaksu3", "Rnaksu4", "Rnaksu5", "Rnaksu6"
                };

                foreach (string item in chulnames2)
                {
                    var list = new List<int>();
                    string chul2query = $"SELECT {item} FROM ChulsuTbl";
                    using var context = new LottoDBContext();
                    using var cmd = context.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = chul2query;
                    context.Database.OpenConnection();
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int n = reader.GetInt32(0);
                        list.Add(n);
                    }
                    _chulPairs.Add(item, list.ToArray());

                    //_btmtopPairs 값을 최대출수 + 1로 변경
                    int max = list.Max() >= 5 ? 6 : list.Max() + 1;
                    var tpl = _btmtopPairs[item];
                    tpl.top = max;
                    _btmtopPairs[item] = tpl;
                }
            });

            await task;
        }

        /// <summary>
        /// 전체 항목의 데이터 딕셔너리 (현재 당번과 계산에 사용)
        /// </summary>
        /// <returns></returns>
        public static (Dictionary<string, int[]> numpair, Dictionary<string, (int bottom, int top)> btmpair) DataOfColumn()
        {
            //먼저 각자 출수로 검사하는 항목
            //[ChulsuTbl]"Beisu4", "Beisu5", "Beisu6", "Beisu7", "Beisu8", "Beisu9", "Jekobsu", "Ihangsu", "Pivosu", "Revpivo", "Donhyeong"
            //[ChulsuTbl]"Daluksu", "Ihutsu", "Binsu", "Autsu", "Cansu", "Samkak1", "Samkak2", "Samkak3", "Samkak4",
            //[NonChulsuTbl]"Nine", "Nonthree", "Nonfive", "Dgap"
            //짝을 이루어 나오는 출수로 검사하는 항목
            //[FixChulsuTbl]"Lowhigh", "Oddeven", "Innout", "Topleft", "LDiagonal", "RDiagonal", "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon"
            //[ChulsuTbl]"Hotsu", "Warmsu", "Coldsu", "Snaksu1", "Snaksu2", "Snaksu3", "Snaksu4", "Snaksu5", "Snaksu6", "Rnaksu1", "Rnaksu2", "Rnaksu3", "Rnaksu4", "Rnaksu5", "Rnaksu6"
            var dic = new Dictionary<string, int[]>();
            var par = new Dictionary<string, (int bottom, int top)>();
            var gd = new Geomsa();

            string[] chulname1 = { "Beisu4", "Beisu5", "Beisu6", "Beisu7", "Beisu8", "Beisu9", "Jekobsu", "Ihangsu", "Pivosu", "Revpivo", "Donhyeong" };
            dic.Add("Beisu4", LuckyLibrary.BaesuInts(4));
            dic.Add("Beisu5", LuckyLibrary.BaesuInts(5));
            dic.Add("Beisu6", LuckyLibrary.BaesuInts(6));
            dic.Add("Beisu7", LuckyLibrary.BaesuInts(7));
            dic.Add("Beisu8", LuckyLibrary.BaesuInts(8));
            dic.Add("Beisu9", LuckyLibrary.BaesuInts(9));
            dic.Add("Jekobsu", LuckyLibrary.JekobsuInts());
            dic.Add("Ihangsu", LuckyLibrary.IhangInts());
            dic.Add("Pivosu", LuckyLibrary.PivonachInts());
            dic.Add("Revpivo", LuckyLibrary.YeokPivonachInts());
            dic.Add("Donhyeong", LuckyLibrary.DonghyeongInts());

            foreach (string item in chulname1)
            {
                int n = dic[item].Length >= 6 ? 6 : dic[item].Length;
                par.Add(item, (0, n));
            }

            var forgins = gd.ForeignDatas();
            var samkaks = gd.TriAngularDatas().ToList();//TriangularDatas();

            dic.Add("Daluksu", gd.MonthData().ToArray());
            dic.Add("Ihutsu", gd.AroundData().ToArray());
            dic.Add("Binsu", gd.EmptyData().ToArray());
            dic.Add("Autsu", forgins[0]);
            dic.Add("Cansu", forgins[1]);
            dic.Add("Samkak1", samkaks[0]);
            dic.Add("Samkak2", samkaks[1]);
            dic.Add("Samkak3", samkaks[2]);
            dic.Add("Samkak4", samkaks[3]);

            string[] chulname2 = { "Daluksu", "Ihutsu", "Binsu", "Autsu", "Cansu", "Samkak1", "Samkak2", "Samkak3", "Samkak4" };

            foreach (string item in chulname2)
            {
                var val = dic[item];
                int n = !val.Any() ? 1 : val.Length >= 6 ? 6 : val.Length;
                par.Add(item, (0, n));
            }

            var nines = LuckyLibrary.NainDatas();
            var non3 = gd.NonthreeDatas();
            var non5 = gd.NonfiveDatas();

            for (int i = 0; i < nines.Count; i++)
            {
                string s = $"Nine_{i}";
                dic.Add(s, nines[i]);
                par.Add(s, (0, 6));
            }

            for (int i = 0; i < non3.Count; i++)
            {
                string s = $"NThree_{i}";
                dic.Add(s, non3[i]);
                par.Add(s, (0, 3));
            }

            for (int i = 0; i < non5.Count; i++)
            {
                string s = $"NFive_{i}";
                dic.Add(s, non5[i]);
                par.Add(s, (0, 5));
            }

            dic.Add("Lowhigh_0", LuckyLibrary.JeokosuDatas()[0]);
            dic.Add("Lowhigh_1", LuckyLibrary.JeokosuDatas()[1]);
            dic.Add("Oddeven_0", LuckyLibrary.HoljjackDatas()[0]);
            dic.Add("Oddeven_1", LuckyLibrary.HoljjackDatas()[1]);
            dic.Add("Innout_0", LuckyLibrary.AnbakDatas()[0]);
            dic.Add("Innout_1", LuckyLibrary.AnbakDatas()[1]);
            dic.Add("Topleft_0", LuckyLibrary.SanghajwauDatas()[0]);
            dic.Add("Topleft_1", LuckyLibrary.SanghajwauDatas()[1]);
            dic.Add("LDiagonal_0", LuckyLibrary.LeftsaseonDatas()[0]);
            dic.Add("LDiagonal_1", LuckyLibrary.LeftsaseonDatas()[1]);
            dic.Add("RDiagonal_0", LuckyLibrary.RightsaseonDatas()[0]);
            dic.Add("RDiagonal_1", LuckyLibrary.RightsaseonDatas()[1]);
            dic.Add("Sosamhap_0", LuckyLibrary.SosamhapDatas()[0]);
            dic.Add("Sosamhap_1", LuckyLibrary.SosamhapDatas()[1]);
            dic.Add("Sosamhap_2", LuckyLibrary.SosamhapDatas()[2]);
            dic.Add("Beondae_0", LuckyLibrary.BeondaeDatas()[0]);
            dic.Add("Beondae_1", LuckyLibrary.BeondaeDatas()[1]);
            dic.Add("Beondae_2", LuckyLibrary.BeondaeDatas()[2]);
            dic.Add("Beondae_3", LuckyLibrary.BeondaeDatas()[3]);
            dic.Add("Beondae_4", LuckyLibrary.BeondaeDatas()[4]);
            dic.Add("Slipsu_0", LuckyLibrary.SlipDatas()[0]);
            dic.Add("Slipsu_1", LuckyLibrary.SlipDatas()[1]);
            dic.Add("Slipsu_2", LuckyLibrary.SlipDatas()[2]);
            dic.Add("Slipsu_3", LuckyLibrary.SlipDatas()[3]);
            dic.Add("Slipsu_4", LuckyLibrary.SlipDatas()[4]);
            dic.Add("Slipsu_5", LuckyLibrary.SlipDatas()[5]);
            dic.Add("Kkeutbeon_0", LuckyLibrary.KkeutbeonDatas()[0]);
            dic.Add("Kkeutbeon_1", LuckyLibrary.KkeutbeonDatas()[1]);
            dic.Add("Kkeutbeon_2", LuckyLibrary.KkeutbeonDatas()[2]);
            dic.Add("Kkeutbeon_3", LuckyLibrary.KkeutbeonDatas()[3]);
            dic.Add("Kkeutbeon_4", LuckyLibrary.KkeutbeonDatas()[4]);
            dic.Add("Kkeutbeon_5", LuckyLibrary.KkeutbeonDatas()[5]);
            dic.Add("Kkeutbeon_6", LuckyLibrary.KkeutbeonDatas()[6]);
            dic.Add("Kkeutbeon_7", LuckyLibrary.KkeutbeonDatas()[7]);
            dic.Add("Kkeutbeon_8", LuckyLibrary.KkeutbeonDatas()[8]);
            dic.Add("Kkeutbeon_9", LuckyLibrary.KkeutbeonDatas()[9]);

            string[] chulname3 = 
            { 
                "Lowhigh_0", "Lowhigh_1", "Oddeven_0", "Oddeven_1", "Innout_0", "Innout_1", "Topleft_0", "Topleft_1", 
                "LDiagonal_0", "LDiagonal_1", "RDiagonal_0", "RDiagonal_1", "Sosamhap_0", "Sosamhap_1", "Sosamhap_2",
                "Beondae_0", "Beondae_1", "Beondae_2", "Beondae_3", "Beondae_4", "Slipsu_0", "Slipsu_1", "Slipsu_2", "Slipsu_3", "Slipsu_4", "Slipsu_5", 
                "Kkeutbeon_0", "Kkeutbeon_1", "Kkeutbeon_2", "Kkeutbeon_3", "Kkeutbeon_4", "Kkeutbeon_5", 
                "Kkeutbeon_6", "Kkeutbeon_7", "Kkeutbeon_8", "Kkeutbeon_9" 
            };

            foreach (string item in chulname3)
            {
                var val = dic[item];
                int n = val.Length >= 6 ? 6 : val.Length;
                par.Add(item, (0, n));
            }

            var hotwam = gd.HotWarmColdDatas().ToList();// HotwarmcoldDatas();
            var snak = gd.NaksutDatas(0).ToList();
            var rnak = gd.NaksutDatas(1).ToList();
            dic.Add("Hotcold1", hotwam[0]);
            dic.Add("Hotcold2", hotwam[1]);
            dic.Add("Hotcold3", hotwam[2]);
            dic.Add("Snaksu1", snak[0]);
            dic.Add("Snaksu2", snak[1]);
            dic.Add("Snaksu3", snak[2]);
            dic.Add("Snaksu4", snak[3]);
            dic.Add("Snaksu5", snak[4]);
            dic.Add("Snaksu6", snak[5]);
            dic.Add("Rnaksu1", rnak[0]);
            dic.Add("Rnaksu2", rnak[1]);
            dic.Add("Rnaksu3", rnak[2]);
            dic.Add("Rnaksu4", rnak[3]);
            dic.Add("Rnaksu5", rnak[4]);
            dic.Add("Rnaksu6", rnak[5]);

            string[] chulname4 = 
            { 
                "Hotcold1", "Hotcold2", "Hotcold3", "Snaksu1", "Snaksu2", "Snaksu3", "Snaksu4", "Snaksu5", "Snaksu6", 
                "Rnaksu1", "Rnaksu2", "Rnaksu3", "Rnaksu4", "Rnaksu5", "Rnaksu6"
            };

            foreach (string item in chulname4)
            {
                var val = dic[item];
                var n = !val.Any() ? 1 : val.Length >= 6 ? 6 : val.Length;
                par.Add(item, (0, n));
            }

            return (dic, par);
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
