using System.Data;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 고정데이터 쿼리검사하는 폼 클래스
    /// </summary>
    public partial class QueryFixDataForm : Form
    {
        #region 필드
        private int _rowCount = 7;
        private int _colCount = 7;
        private CheckBox[] _guCheckBox;
        private CheckBox[] _horCheckBox;
        private CheckBox[] _verCheckBox;
        private CheckBox[] _typeCheckBox;
        private DataTable _dataTable = null;
        private List<string> _combineStrs = new();
        private const string ConnectionString = "Data Source=CHOI-PC;Initial Catalog=LottoDB;Integrated Security=True";
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public QueryFixDataForm()
        {
            InitializeComponent();
            TypeFlowControls();
        }

        private void QueryFixDataForm_Load(object sender, EventArgs e)
        {
            RowCountComboBox.SelectedIndex = 1;
            string[] strings = { "111111", "21111", "2211", "222", "3111", "321", "33", "411", "42", "51", "60" };
            var s = Enumerable.Range(0, strings.Length).Zip(strings, (idx, str) => $"{idx}: {str}");
            var front = s.Take(5);
            var reals = s.Skip(5);

            var s1 = string.Join("", front.Select(x => x.PadRight(12)));
            var s2 = string.Join("", reals.Select(x => x.PadRight(12)));
            ShowLabel.Text = s1 + "\r\n" + s2;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var dang = Utility.DangbeonOfOrder(Utility.GetLastOrder());
            int num = 0;
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _colCount; j++)
                {
                    num++;

                    if (num <= 45)
                    {
                        var rect = new Rectangle(j * 24, i * 24, 24, 24);
                        using (var pen = new Pen(Color.Gray, 0.1F))
                        {
                            g.DrawRectangle(pen, rect);
                        }

                        //번호 그리기
                        StringFormat stringFormat = new()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        using (var font = new Font("맑음고딕", 9, FontStyle.Regular, GraphicsUnit.Point))
                        {
                            g.DrawString(num.ToString(), font, Brushes.Black, rect, stringFormat);
                        }

                        if (dang.Contains(num))
                        {
                            var (xIndex, yIndex) = LuckyLibrary.PositionOfData(num, _rowCount);
                            if (i == yIndex && j == xIndex)
                            {
                                //원그리기
                                var redRect = new Rectangle((j * 24) + 1, (i * 24) + 1, 22, 22);
                                using var blackPen = new Pen(Color.Red, 2);
                                g.DrawEllipse(blackPen, redRect);
                            }
                        }
                    }
                }

            }
        }

        private async void RowCountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RowCountComboBox.SelectedIndex > -1)
            {
                _dataTable = null;
                Cursor.Current = Cursors.WaitCursor;
                listView1.Clear();
                ResultTextBox.Text = string.Empty;
                ATextBox.Text = string.Empty;
                BTextBox.Text = string.Empty;
                CTextBox.Text = string.Empty;
                DTextBox.Text = string.Empty;
                CombineTextBox.Text = string.Empty;
                HorCheckBox.Checked = false;
                VerCheckBox.Checked = false;

                int rowCount = int.Parse(RowCountComboBox.SelectedItem.ToString());
                _rowCount = rowCount;
                int colCount = rowCount == 5 ? 9 : rowCount == 9 ? 5 : 7;
                _colCount = colCount;
                HorFlowLayoutPanel.Controls.Clear();
                VerFlowLayoutPanel.Controls.Clear();
                GuFlowControls();
                HorFlowControls(rowCount);
                VerFlowControls(colCount);
                pictureBox1.Invalidate();

                await Task.Run(CreateTable);
                Cursor.Current= Cursors.Default;
            }
        }

        private void GuCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var chklist = GuFlowLayoutPanel.Controls.OfType<CheckBox>().ToList();

            if (GuCheckBox.CheckState == CheckState.Checked)
            {
                _guCheckBox.ToList().ForEach(x => x.Checked = true);
                chklist.ForEach(x => x.Checked = true);
            }
            else if (GuCheckBox.CheckState == CheckState.Unchecked)
            {
                _guCheckBox.ToList().ForEach(x => x.Checked = false);
                chklist.ForEach(x => x.Checked = false);
            }
        }

        private void HorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HorCheckBox.CheckState ==  CheckState.Checked)
            {
                _horCheckBox.ToList().ForEach(x => x.Checked = true);
            }
            else if (HorCheckBox.CheckState == CheckState.Unchecked)
            {
                _horCheckBox.ToList().ForEach(x => x.Checked = false);
            }
        }

        private void VerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (VerCheckBox.CheckState == CheckState.Checked)
            {
                _verCheckBox.ToList().ForEach(x => x.Checked = true);
            }
            else if (VerCheckBox.CheckState == CheckState.Unchecked)
            {
                _verCheckBox.ToList().ForEach(x => x.Checked = false);
            }
        }

        private void TypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TypeCheckBox.CheckState == CheckState.Checked)
                _typeCheckBox.ToList().ForEach(x => x.Checked = true);
            
            else if ( TypeCheckBox.CheckState == CheckState.Unchecked)
                _typeCheckBox.ToList().ForEach (x => x.Checked = false);
        }

        private void GuBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_guCheckBox.All(x => x.Checked == true))
            {
                GuCheckBox.CheckState = CheckState.Checked;
            }
            else if (_guCheckBox.All(x => x.Checked == false))
            {
                GuCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                GuCheckBox.CheckState = CheckState.Indeterminate;
            }
        }

        private void HorBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_horCheckBox.All(x => x.Checked == true))
            {
                HorCheckBox.CheckState = CheckState.Checked;
            }
            else if (_horCheckBox.All(x => x.Checked == false))
            {
                HorCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                HorCheckBox.CheckState = CheckState.Indeterminate;
            }
        }

        private void VerBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_verCheckBox.All(x => x.Checked == true))
            {
                VerCheckBox.CheckState = CheckState.Checked;
            }
            else if (_verCheckBox.All(x => x.Checked == false))
            {
                VerCheckBox.CheckState = CheckState.Unchecked;
            }
            else
            {
                VerCheckBox.CheckState = CheckState.Indeterminate;
            }
        }

        private void TypeBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_typeCheckBox.All(x => x.Checked == true))
                TypeCheckBox.CheckState = CheckState.Checked;
            else if (_typeCheckBox.All(x => x.Checked == false))
                TypeCheckBox.CheckState = CheckState.Unchecked;
            else
                TypeCheckBox.CheckState = CheckState.Indeterminate;
        }

        private void ATextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(ATextBox.Text))
                {
                    if (!_combineStrs.Contains("A"))
                    {
                        _combineStrs.Add("A");
                    }
                }
                else
                {
                    _combineStrs.Remove("A");
                }

                if (_combineStrs.Any())
                {
                    _combineStrs.Sort();
                    CombineTextBox.Text = string.Join(" AND ", _combineStrs);
                }
            }
        }

        private void ATextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ATextBox.Text))
            {
                if (!_combineStrs.Contains("A"))
                {
                    _combineStrs.Add("A");
                }
            }
            else
            {
                _combineStrs.Remove("A");
            }

            if (_combineStrs.Any())
            {
                _combineStrs.Sort();
                CombineTextBox.Text = string.Join(" AND ", _combineStrs);
            }
        }

        private void BTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(BTextBox.Text))
                {
                    if (!_combineStrs.Contains("B"))
                    {
                        _combineStrs.Add("B");
                    }
                }
                else
                {
                    _combineStrs.Remove("B");
                }

                if (_combineStrs.Any())
                {
                    _combineStrs.Sort();
                    CombineTextBox.Text = string.Join(" AND ", _combineStrs);
                }
            }
        }

        private void BTextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BTextBox.Text))
            {
                if (!_combineStrs.Contains("B"))
                {
                    _combineStrs.Add("B");
                }
            }
            else
            {
                _combineStrs.Remove("B");
            }

            if (_combineStrs.Any())
            {
                _combineStrs.Sort();
                CombineTextBox.Text = string.Join(" AND ", _combineStrs);
            }
        }

        private void CTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(CTextBox.Text))
                {
                    if (!_combineStrs.Contains("C"))
                    {
                        _combineStrs.Add("C");
                    }
                }
                else
                {
                    _combineStrs.Remove("C");
                }

                if (_combineStrs.Any())
                {
                    _combineStrs.Sort();
                    CombineTextBox.Text = string.Join(" AND ", _combineStrs);
                }
            }
        }

        private void CTextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CTextBox.Text))
            {
                if (!_combineStrs.Contains("C"))
                {
                    _combineStrs.Add("C");
                }
            }
            else
            {
                _combineStrs.Remove("C");
            }

            if (_combineStrs.Any())
            {
                _combineStrs.Sort();
                CombineTextBox.Text = string.Join(" AND ", _combineStrs);
            }
        }

        private void DTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(DTextBox.Text))
                {
                    if (!_combineStrs.Contains("D"))
                    {
                        _combineStrs.Add("D");
                    }
                }
                else
                {
                    _combineStrs.Remove("D");
                }

                if (_combineStrs.Any())
                {
                    _combineStrs.Sort();
                    CombineTextBox.Text = string.Join(" AND ", _combineStrs);
                }
            }
        }

        private void DTextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DTextBox.Text))
            {
                if (!_combineStrs.Contains("D"))
                {
                    _combineStrs.Add("D");
                }
            }
            else
            {
                _combineStrs.Remove("D");
            }

            if (_combineStrs.Any())
            {
                _combineStrs.Sort();
                CombineTextBox.Text = string.Join(" AND ", _combineStrs);
            }
        }

        private void ETextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(ETextBox.Text))
                {
                    if (!_combineStrs.Contains("E"))
                    {
                        _combineStrs.Add("E");
                    }
                }
                else
                {
                    _combineStrs.Remove("E");
                }

                if (_combineStrs.Any())
                {
                    _combineStrs.Sort();
                    CombineTextBox.Text = string.Join(" AND ", _combineStrs);
                }
            }
        }

        private void ETextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ETextBox.Text))
            {
                if (!_combineStrs.Contains("E"))
                {
                    _combineStrs.Add("E");
                }
            }
            else
            {
                _combineStrs.Remove("E");
            }

            if (_combineStrs.Any())
            {
                _combineStrs.Sort();
                CombineTextBox.Text = string.Join(" AND ", _combineStrs);
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            _combineStrs = new();
            var lastord = Utility.GetLastOrder();
            var horvals = new List<string>();
            var vervals = new List<string>();
            using (var context = new LottoDBContext())
            {
                string query = $"SELECT RH{_rowCount:00} FROM FixChulsuTbl WHERE Orders={lastord}";
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                string hors = command.ExecuteScalar().ToString().Trim();
                horvals =  hors.Select(x => x.ToString()).ToList();
                context.Database.CloseConnection();
            }

            using (var context = new LottoDBContext())
            {
                string query = $"SELECT RV{_colCount:00} FROM FixChulsuTbl WHERE Orders={lastord}";
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                string vers = command.ExecuteScalar().ToString().Trim();
                vervals = vers.Select(x => x.ToString()).ToList();
                context.Database.CloseConnection();
            }

            string[] typename = { "TRH05", "TRV05", "TRH07", "TRV07", "TRH09", "TRV09" };
            var typepairs = new Dictionary<string, int>();
            using (var context = new LottoDBContext())
            {
                string query = $"SELECT TRH05, TRV05, TRH07, TRV07, TRH09, TRV09 FROM TypeTbl WHERE Orders={lastord}";
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        typepairs.Add(typename[i], reader.GetInt32(i));
                    }
                }
                context.Database.CloseConnection();
            }

            var horpairs = new Dictionary<string, string>();
            var verpairs = new Dictionary<string, string>();
            for (int i = 0; i < _rowCount; i++)
            {
                string s1 = $"Hor{_rowCount}_{i}";
                horpairs.Add(s1, horvals[i]);
            }
            
            for (int i = 0; i < _colCount; i++)
            {
                string s1 = $"Ver{_colCount}_{i}";
                verpairs.Add(s1, vervals[i]);
            }

            //가로
            var horstr = new List<string>();
            foreach (string key in horpairs.Keys)
            {
                string val = horpairs[key];
                var cons = HorFlowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(g => g.Text);
                if (cons.Contains(key))
                {
                    string s1 = $"{key}={val}";
                    horstr.Add(s1);
                }
            }

            CTextBox.Text = string.Join(" AND ", horstr);
            if (horstr.Any())
            {
                if (!_combineStrs.Contains("C"))
                {
                    _combineStrs.Add("C");
                }
            }

            //세로
            var verstr = new List<string>();
            foreach (string key in verpairs.Keys)
            {
                string val = verpairs[key];
                var cons = VerFlowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(g => g.Text);
                if (cons.Contains(key))
                {
                    string s1 = $"{key}={val}";
                    verstr.Add(s1);
                }
            }

            DTextBox.Text = string.Join(" AND ", verstr);
            if (verstr.Any())
            {
                if (!_combineStrs.Contains("D"))
                {
                    _combineStrs.Add("D");
                }
            }

            //타입
            var typestr = new List<string>();
            foreach (var key in typepairs.Keys)
            {
                int val = typepairs[key];
                var cons = TypeFlowLayout.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(g => g.Text);
                if (cons.Contains(key))
                {
                    string s1 = $"{key}={val}";
                    typestr.Add(s1);
                }
            }

            ETextBox.Text = string.Join(" AND ", typestr);
            if (typestr.Any())
            {
                if (!_combineStrs.Contains("E"))
                {
                    _combineStrs.Add("E");
                }
            }

            CombineTextBox.Text = string.Join(" AND ", _combineStrs);
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            ResultTextBox.Text = string.Empty;
            var guname = new List<string>();
            var horname = new List<string>();
            var vername = new List<string>();
            var typename = new List<string>();
            var names = new List<string>(); //회차를 뺀 나머지 열이름

            var guchklist = GuFlowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(x => x.Text).Distinct();
            var horchklist = HorFlowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(x => x.Text).Distinct();
            var verchklist = VerFlowLayoutPanel.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(x => x.Text).Distinct();
            var typechklist = TypeFlowLayout.Controls.OfType<CheckBox>().Where(x => x.Checked).Select(x => x.Text).Distinct();

            if (guchklist.Any())
            {
                foreach (var item in guchklist)
                {
                    if (!guname.Contains(item))
                    {
                        guname.Add(item);
                        names.Add(item);
                    }
                }
            }
            
            if (horchklist.Any())
            {
                foreach (var item in horchklist)
                {
                    if (!horname.Contains(item))
                    {
                        horname.Add(item);
                        names.Add(item);
                    }
                }
            }

            if (verchklist.Any())
            {
                foreach (var item in verchklist)
                {
                    if (!vername.Contains(item))
                    {
                        vername.Add(item);
                        names.Add(item);
                    }
                }
            }

            if (typechklist.Any())
            {
                foreach (var item in typechklist)
                {
                    if (!typename.Contains(item))
                    {
                        typename.Add(item);
                        names.Add(item);
                    }
                }
            }

            if (!names.Any())
            {
                MessageBox.Show("검사항목이 없습니다.");
                return;
            }

            #region 리스트뷰 컬럼
            listView1.Columns.Add("Orders", 70, HorizontalAlignment.Left);
            names.ForEach(x => listView1.Columns.Add(x, 70, HorizontalAlignment.Left));
            #endregion

            var colIndex = new List<int>();
            //체크된 항목의 이름과 데이터베이스의 열 인덱스 딕셔너리
            var pairs = new List<(string, int)>();
            for (int i = 0; i < _dataTable.Columns.Count; i++)
            {
                string col = _dataTable.Columns[i].ColumnName;

                if (names.Contains(col))
                {
                    colIndex.Add(i);
                    pairs.Add((col, i));
                }
            }

            var selquery = CombineCondition();
            DataRow[] finds = new DataRow[] {};
            try
            {
                finds = _dataTable.Select(selquery);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //먼저 조건검색에 해당하는 회차 찾기
            if (finds.Any())    //DataRow 널이 없으므로 Any()로 검사
            {
                var ords = new List<int>();
                foreach (DataRow row in finds)
                {
                    //찾은 회차 다음의 데이터기 때문에 +1
                    int n = Convert.ToInt32(row[0]) + 1;

                    if (n < _dataTable.Rows.Count)
                    {
                        ords.Add(n);
                    }
                }

                if (ords.Any())
                {
                    ords.Sort();
                    var gulists = new List<int[]>();
                    //인덱스별출수 리스트
                    var chulists = new List<int[]>();
                    //다음회차의 데이터 찾기
                    var queryresults = _dataTable.AsEnumerable().Where(row => ords.Contains(row.Field<int>("Orders")));
                    foreach (DataRow row in queryresults)
                    {
                        var chularr = new List<int>();
                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            if (i == 0 || colIndex.Contains(i))
                            {
                                int n = Convert.ToInt32(row[i]);
                                chularr.Add(n);
                            }
                        }
                        chulists.Add(chularr.ToArray());
                    }

                    #region 리스트뷰에 출력
                    listView1.BeginUpdate();

                    for (int i = 0; i < chulists.Count; i++)
                    {
                        var arr = chulists[i];
                        var lvi = new ListViewItem(arr[0].ToString());
                        for (int j = 1; j < arr.Length; j++)
                        {
                            lvi.SubItems.Add(arr[j].ToString());
                        }

                        listView1.Items.Add(lvi);
                    }

                    listView1.EndUpdate();
                    listView1.EnsureVisible(chulists.Count - 1);
                    #endregion

                    string resultStrings = string.Empty;
                    //Gu1 - Gu6 체크되어 있다면 1 - 45번 사이에 출수를 표시
                    string gustring = string.Empty;
                    string hvstring = string.Empty;
                    if (guname.Any())
                    {
                        var guIndex = names.Select((val, idx) => (val, idx)).Where(x => guname.Contains(x.val)).Select(g => g.idx + 1);
                        List<int> guaddlist = new();
                        foreach (int[] array in chulists)
                        {
                            var gusarr = guIndex.Select(x => array[x]).ToArray();
                            guaddlist.AddRange(gusarr);
                        }

                        var enumers = Enumerable.Range(1, 45);
                        var guchul = enumers.Select(num => guaddlist.Count(g => g == num));
                        var gucoms = enumers.Zip(guchul, (a, b) => $"{a}-{b}");
                        gustring = string.Join("\t", gucoms);
                    }

                    if (horname.Any() || vername.Any() || typename.Any())
                    {
                        var horIndex = names.Select((val, idx) => (val, idx)).Where(x => horname.Contains(x.val)).Select(g => g.idx + 1);
                        var verIndex = names.Select((val, idx) => (val, idx)).Where(x => vername.Contains(x.val)).Select(g => g.idx + 1);
                        var typeIndex = names.Select((val, idx) => (val, idx)).Where(x => typename.Contains(x.val)).Select(g => g.idx + 1);
                        if (horIndex.Any())
                        {
                            var horchul = new List<int[]>();
                            foreach (int[] array in chulists)
                            {
                                var each = horIndex.Select(x => array[x]).ToArray();
                                horchul.Add(each);
                            }
                            List<string> horarrs = new();
                            for (int i = 0; i < horname.Count; i++)
                            {
                                string col = horname[i];
                                var colarr = horchul.Select(x => x[i]).ToArray();
                                List<string> horstr = new();
                                for (int j = 0; j <= colarr.Max(); j++)
                                {
                                    int n = colarr.Count(x => x == j);
                                    string s1 = $"{j}-{n}";
                                    horstr.Add(s1);
                                }

                                string s2 = $"{col}:\t{string.Join("\t", horstr)}";
                                horarrs.Add(s2);
                            }
                            hvstring = string.Join(Environment.NewLine, horarrs) + "\r\n";
                        }

                        if (verIndex.Any())
                        {
                            var verchul = new List<int[]>();
                            foreach (int[] array in chulists)
                            {
                                var each = verIndex.Select(x => array[x]).ToArray();
                                verchul.Add(each);
                            }
                            List<string> verarrs = new();
                            for (int i = 0; i < vername.Count; i++)
                            {
                                string col = vername[i];
                                var colarr = verchul.Select(x => x[i]).ToArray();
                                List<string> verstr = new();
                                for (int j = 0; j <= colarr.Max(); j++)
                                {
                                    int n = colarr.Count(x => x == j);
                                    string s1 = $"{j}-{n}";
                                    verstr.Add(s1);
                                }

                                string s2 = $"{col}:\t{string.Join("\t", verstr)}";
                                verarrs.Add(s2);
                            }
                            hvstring += string.Join(Environment.NewLine, verarrs) + "\r\n";
                        }

                        if (typeIndex.Any())
                        {
                            var typechul = new List<int[]>();
                            foreach (int[] array in chulists)
                            {
                                var each = typeIndex.Select(x => array[x]).ToArray();
                                typechul.Add(each);
                            }
                            List<string> typearrs = new();
                            for (int i = 0; i < typename.Count; i++)
                            {
                                string col = typename[i];
                                var colarr = typechul.Select(x => x[i]).ToArray();
                                List<string> typestr = new();
                                for (int j = 0; j <= colarr.Max(); j++)
                                {
                                    int n = colarr.Count(x => x == j);
                                    string s1 = $"{j}-{n}";
                                    typestr.Add(s1);
                                }

                                string s2 = $"{col}:\t{string.Join("\t", typestr)}";
                                typearrs.Add(s2);
                            }
                            hvstring += string.Join(Environment.NewLine, typearrs) + "\r\n";
                        }
                    }

                    resultStrings = $"{gustring}\r\n{hvstring}\r\n출현갯수:\t{chulists.Count}\r\n";
                    var rowlists = new List<int[]>();
                    var rowIndex = names.Select((val, idx) => (val, idx))
                                        .Where(x => horname.Contains(x.val) || vername.Contains(x.val) || typename.Contains(x.val))
                                        .Select(g => g.idx + 1);
                    if (rowIndex.Any())
                    {
                        foreach (int[] array in chulists)
                        {
                            var each = rowIndex.Select(x => array[x]).ToArray();
                            rowlists.Add(each);
                        }
                    }
                    var rowstrs = new List<string>();
                    if (rowlists.Any())
                    {
                        //당번이 아닌 경우 항목의 전출검사
                        //000 - 444 중 안 나온것 찾기
                        int[] maxindex = new int[rowlists[0].Length];
                        for (int i = 0; i < rowlists[0].Length; i++)
                        {
                            var each = rowlists.Select(x => x[i]).Max();
                            maxindex[i] = each;
                        }
                        int min = rowlists.SelectMany(x => x).Min();
                        int max = rowlists.SelectMany(x => x).Max();
                        var johaps = Utility.GetAllPermutations(Enumerable.Range(min, max - min + 1), rowlists[0].Length);
                        foreach (var johap in johaps)
                        {
                            var each = johap.ToArray();
                            bool zero = rowlists.Any(x => x.SequenceEqual(johap));
                            if (IsElementIndex(each, maxindex) && !zero)
                            {
                                string s1 = $"[{string.Join(",", johap)}]";
                                rowstrs.Add(s1);
                            }
                        }
                    }

                    ResultTextBox.Text = resultStrings + "무출조합:\t" + string.Join("\t", rowstrs);
                }
            }
        }





        //******************  메서드  **********************





        private void GuFlowControls()
        {
            _guCheckBox = new CheckBox[6];
            for (int i = 0; i < 6; i++)
            {
                CheckBox checkBox = new()
                {
                    Checked = true,
                    Size = new Size(50, 19),
                    Name = $"gu{i}",
                    Text = $"Gu{i + 1}"
                };
                _guCheckBox[i] = checkBox;
                checkBox.CheckedChanged += GuBox_CheckedChanged;
                GuFlowLayoutPanel.Controls.Add(checkBox);
            }
        }

        private void HorFlowControls(int row)
        {
            _horCheckBox = new CheckBox[row];
            for (int i = 0; i < row; i++)
            {
                CheckBox checkBox = new()
                {
                    Checked = false,
                    Size = new Size(80, 19),
                    Name = $"horchk{i}",
                    Text = $"Hor{row}_{i}"
                };
                _horCheckBox[i] = checkBox;
                checkBox.CheckedChanged += HorBox_CheckedChanged;
                HorFlowLayoutPanel.Controls.Add(checkBox);
            }
        }

        private void VerFlowControls(int colCount)
        {
            _verCheckBox = new CheckBox[colCount];
            for (int i = 0; i < colCount; i++)
            {
                CheckBox checkBox = new()
                {
                    Checked = false,
                    Size = new Size(80, 19),
                    Name = $"verchk{i}",
                    Text = $"Ver{colCount}_{i}"
                };
                _verCheckBox[i] = checkBox;
                checkBox.CheckedChanged += VerBox_CheckedChanged;
                VerFlowLayoutPanel.Controls.Add(checkBox);
            }
        }

        private void TypeFlowControls()
        {
            string[] names = { "TRH05", "TRV05", "TRH07", "TRV07", "TRH09", "TRV09" };
            _typeCheckBox = new CheckBox[6];
            for (int i = 0; i < 6; i++)
            {
                CheckBox checkBox = new()
                {
                    Checked = false,
                    Padding = new Padding(8, 0, 0, 0),
                    Size = new Size(80, 19),
                    Name = $"typechk{i}",
                    Text = names[i]
                };
                _typeCheckBox[i] = checkBox;
                checkBox.CheckedChanged += TypeBox_CheckedChanged;
                TypeFlowLayout.Controls.Add(checkBox);
            }
        }

        /// <summary>
        /// 쿼리에 사용할수 있는 문장만들기
        /// </summary>
        /// <returns></returns>
        private string CombineCondition()
        {
            string rst = string.Empty;
            string condition = CombineTextBox.Text;
            var addstrs = new List<string>();

            if (!string.IsNullOrEmpty(condition) && _combineStrs.Any())
            {
                foreach (string str in _combineStrs)
                {
                    string amun = string.Empty;
                    string bmun = string.Empty;
                    if (str.Equals("A"))
                    {
                        string[] gus = { "Gu1", "Gu2", "Gu3", "Gu4", "Gu5", "Gu6" };
                        var strings = ATextBox.Text.Split(',');

                        if (strings.Any())
                        {
                            var list = new List<string>();
                            foreach (var item in strings)
                            {
                                var s2 = gus.Select(x => x + "=" + item);
                                list.Add("(" + string.Join(" OR ", s2) + ")");
                            }

                            amun = string.Join(" AND ", list);
                        }
                        else
                        {
                            amun = string.Empty;
                        }
                    }
                    else
                    {
                        amun = str.Equals("B") ? BTextBox.Text : str.Equals("C") ? CTextBox.Text : str.Equals("D") ? DTextBox.Text : ETextBox.Text;
                    }

                    addstrs.Add(amun);
                }
            }

            if (addstrs.Any())
            {
                rst = string.Join(" AND ", addstrs);
            }

            return rst;
        }

        /// <summary>
        /// 새로운 데이터테이블 만들기
        /// </summary>
        private async void CreateTable()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add(new DataColumn("Orders", typeof(int)));

            foreach (var box in _guCheckBox)
            {
                _dataTable.Columns.Add(new DataColumn(box.Text, typeof(int)));
            }
            foreach (var box in _horCheckBox)
            {
                _dataTable.Columns.Add(new DataColumn(box.Text, typeof(int)));
            }
            foreach (var box in _verCheckBox)
            {
                _dataTable.Columns.Add(new DataColumn(box.Text, typeof(int)));
            }
            foreach (var box in _typeCheckBox)
            {
                _dataTable.Columns.Add(new DataColumn(box.Text, typeof(int)));
            }

            var basiclists = new List<int[]>();
            using (var db = new LottoDBContext())
            {
                basiclists = db.BasicTbl.Select(x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }.ToArray()).ToList();
            }

            var typelists = new List<int[]>();
            using (var db = new LottoDBContext())
            {
                typelists = db.TypeTbl.Select(x => new List<int> { x.TRH05, x.TRV05, x.TRH07, x.TRV07, x.TRH09, x.TRV09 }.ToArray()).ToList();
            }

            var horlists = new List<int[]>();
            var hortask = Task.Run(() =>
            {
                string hor = $"RH{_rowCount:00}";

                using (var context = new LottoDBContext())
                {
                    string query = $"SELECT {hor} FROM FixChulsuTbl";
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string s = reader.GetString(0).Trim();
                        var arr = s.Select(x => int.Parse(x.ToString())).ToArray();
                        horlists.Add(arr);
                    }
                    context.Database.CloseConnection();
                }
            });
            await hortask;

            var verlists = new List<int[]>();
            var vertask = Task.Run(() =>
            {
                string ver = $"RV{_colCount:00}";

                using (var context = new LottoDBContext())
                {
                    string query = $"SELECT {ver} FROM FixChulsuTbl";
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    using var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string s = reader.GetString(0).Trim();
                        var arr = s.Select(x => int.Parse(x.ToString())).ToArray();
                        verlists.Add(arr);
                    }
                    context.Database.CloseConnection();
                }
            });
            await vertask;

            //전체회차 기록
            for (int i = 0; i < basiclists.Count; i++)
            {
                int[] basicInts = basiclists[i];
                int[] horInts = horlists[i];
                int[] verInts = verlists[i];
                int[] typeInts = typelists[i];
                var objs = new List<object>
                {
                    i + 1
                };
                basicInts.ToList().ForEach(x => objs.Add(x));
                horInts.ToList().ForEach(x => objs.Add(x));
                verInts.ToList().ForEach(x => objs.Add(x));
                typeInts.ToList().ForEach(x => objs.Add(x));

                _dataTable.Rows.Add(objs.ToArray());
            }
        }

        /// <summary>
        /// 열별로 전부 최대값 이하이면 참
        /// </summary>
        /// <param name="source">조합배열</param>
        /// <param name="target">열별최대배열</param>
        /// <returns></returns>
        private bool IsElementIndex(int[] source, int[] target)
        {
            if (source.Length != target.Length)
            {
                throw new Exception("배열의 길이가 다릅니다.");
            }

            var pass = Enumerable.Range(0, source.Length).All(x => source[x] <= target[x]);
            return pass;
        }


    }
}
