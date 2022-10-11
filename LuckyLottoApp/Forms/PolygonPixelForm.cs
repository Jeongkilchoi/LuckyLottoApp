using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 폴리곤 내부영역 도트검사 폼 클래스
    /// </summary>
    public partial class PolygonPixelForm : Form
    {
        #region 필드
        private readonly string[] _itemName;
        private readonly int _lastOrder;
        private int _order;
        private int _gap = 1;
        private string _selectItem = "H05";
        private Dictionary<int, string[]> _itemAllDatas;
        private DataTable _dataTable;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public PolygonPixelForm(int lastOrder)
        {
            InitializeComponent();

            _itemName = new[] { "H05", "V05", "H07", "V07", "H09", "V09" };
            _lastOrder = lastOrder;
        }

        private void DotRegionForm_Load(object sender, EventArgs e)
        {
            var gaps = LuckyLibrary.SeqenceGapInts;
            gaps.ToList().ForEach(x => GapComboBox.Items.Add(x));
            GapComboBox.SelectedIndex = 0;
            ItemComboBox.Items.AddRange(_itemName);
            ItemComboBox.SelectedIndex = 0;
            OrderNumericUpDown.Value = _lastOrder;
            OrderNumericUpDown.Maximum = _lastOrder;
        }

        private void ItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemComboBox.SelectedIndex > -1)
            {
                _selectItem = ItemComboBox.SelectedItem.ToString();
                string item = $"{_selectItem}{_gap:00}";
                _itemAllDatas = GetItemSqlDatas(item);
                _dataTable = CreateTable();
                QueryTextBox.Text = string.Empty;
                var lastrow = _itemAllDatas.Last().Value[0];
                string qt = "'";
                string qrs = $"row0 LIKE " + qt + lastrow + qt;
                QueryTextBox.Text = qrs;
                listBox1.Items.Clear();
                pictureBox1.Invalidate();
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            StringFormat stringFormat = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            int[] dangs = Utility.DangbeonOfOrder(_order);
            
            var sequence = LuckyLibrary.NewSequenceLists()[_gap];
            int rowCount = int.Parse(_selectItem[1..]);
            var flowdatas = _selectItem[0].Equals('H') ? LuckyLibrary.HorizontalFlowInts(sequence, rowCount) : 
                                                         LuckyLibrary.VerticalFlowInts(sequence, rowCount);

            var (dangdots, pixeldots) = GetPolygonDatas(_selectItem[0].ToString(), rowCount, _gap, _order);
            int row = flowdatas.Count;
            int col = flowdatas.Max(x => x.Length);

            var size = new Size(40, 40);
            int xg = 20, yg = 20;
            int hx = size.Height / 2;
            int hy = size.Width / 2;

            for (int y = 0; y < row; y++)
            {
                string danstr = dangdots[y];
                string pixstr = pixeldots[y];

                for (int x = 0; x < col; x++)
                {
                    int n = flowdatas[y][x];

                    var pt = new Point((x * size.Width) + xg, (y * size.Height) + yg);
                    var rect = new Rectangle(pt, size);

                    //사각형 그리기
                    using (var pen = new Pen(Color.Black, 1))
                    {
                        g.DrawRectangle(pen, rect);
                    }

                    //var centerpoint = new Point((x * size.Width) + (xg + hx), (y * size.Height) + (yg + hy));
                    char danch = danstr[x];
                    char pixch = pixstr[x];

                    //폴리라인에 걸치면 사각형채우기
                    if (pixch.Equals('1'))
                    {
                        using var brush = new SolidBrush(Color.FromArgb(100, Color.Gray));
                        g.FillRectangle(brush, rect);
                    }

                    //당번에 해당하면 원그리기
                    if (danch.Equals('1'))
                    {
                        var clpt = new Point((x * size.Width) + (xg + 6), (y * size.Height) + (yg + 6));
                        var clrect = new Rectangle(clpt.X, clpt.Y, 28, 28);
                        using var clpen = new Pen(Color.Red, 1.5f);
                        g.DrawEllipse(clpen, clrect);
                    }

                    if (n > 0 && n <= 45)
                    {
                        //숫자 그리기
                        string s = n.ToString();
                        using var font = new Font("맑은고딕", 12, FontStyle.Regular, GraphicsUnit.Point);
                        g.DrawString(s, font, Brushes.Black, rect, stringFormat);
                    }
                }
            }

            //당번 위치
            var pos = dangs.Select(x => Convexhull.IndexToPoint(x, flowdatas)).ToList();

            //외곽선 위치
            var hull = Convexhull.GetConvexHull(pos);
            hull.Add(hull.First());

            //외곽선 그림판 위치
            var hullPoints = hull.Select(g => new Point(g.Y * 40 + 40, g.X * 40 + 40)).ToList();

            //폴리곤 선 그리기
            using var polypen = new Pen(Color.Blue, 1);
            g.DrawLines(polypen, hullPoints.ToArray());
        }

        private void OrderNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _order = (int)OrderNumericUpDown.Value;
            listBox1.Items.Clear();
            pictureBox1.Invalidate();
        }

        private void GapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GapComboBox.SelectedIndex > -1)
            {
                var sel = GapComboBox.SelectedItem.ToString();
                _gap = int.Parse(sel);
                string item = $"{_selectItem}{_gap:00}";
                _itemAllDatas = GetItemSqlDatas(item);
                _dataTable = CreateTable();
                listBox1.Items.Clear();
                pictureBox1.Invalidate();
            }
        }

        private void FindOrderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentItem = listBox1.SelectedItem.ToString();
            int ord = int.Parse(currentItem);
            _order = ord;
            pictureBox1.Invalidate();
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QueryTextBox.Text))
            {
                return;
            }
            listBox1.Items.Clear();
            try
            {
                string expression = QueryTextBox.Text;
                var findRows = _dataTable.Select(expression);
                var findords = new List<int>();
                if (findRows.Any())
                {
                    foreach (var row in findRows)
                    {
                        int ord = row.Field<int>("Id") + 1;

                        if (ord <= _lastOrder)
                        {
                            findords.Add(ord);
                            listBox1.Items.Add(ord);
                        }
                    }
                }

                if (findords?.Any() ?? false)
                {
                    CheckButton.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            ResultTextBox.Text = string.Empty;
            var temp = new List<string[]>();
            foreach (var item in listBox1.Items)
            {
                int ord = int.Parse(item.ToString());
                var seldata = _dataTable.AsEnumerable().Where(x => x.Field<int>("Id") == ord).Single();
                var tmps = new List<string>();
                for (int i = 1; i < _dataTable.Columns.Count; i++)
                {
                    var tmp = seldata[i].ToString();
                    tmps.Add(tmp);
                }
                temp.Add(tmps.ToArray());
            }

            //pivot
            var pivots = new List<string[]>();
            for (int i = 0; i < temp[0].Length; i++)
            {
                var ea = temp.Select(x => x[i]).ToArray();
                pivots.Add(ea);
            }

            var rsts = new List<string>();
            for (int i = 0; i < pivots.Count; i++)
            {
                var each = pivots[i];
                var arrs = new List<int[]>();
                for (int j = 0; j < each.Length; j++)
                {
                    int[] arr = StringToArray(each[j]);
                    arrs.Add(arr);
                }

                for (int j = 0; j < arrs[0].Length; j++)
                {
                    var ea = arrs.Select(x => x[j]).ToArray();
                    var dit = ea.Distinct().ToList();
                    if (dit.Count == 1)
                    {
                        string s1 = $"[{i}] / {j} - {dit.First()}";
                        rsts.Add(s1);
                    }
                }
            }
            ResultTextBox.Text = string.Join(Environment.NewLine, rsts);
        }

        private async void AllCheckButton_Click(object sender, EventArgs e)
        {
            AllCheckButton.Enabled = false;
            AllCheckButton.Text = "작업중";
            AllCheckListBox.Items.Clear();
            foreach (string name in _itemName)
            {
                foreach (int gap in LuckyLibrary.SeqenceGapInts)
                {
                    var task = Task.Run(() =>
                    {
                        string key = $"{name}{gap:00}";
                        var pairs = GetItemSqlDatas(key);
                        var table = CreateTable(pairs);
                        string[] last = pairs.Last().Value;
                        var enumers = Enumerable.Range(0, last.Length);
                        var combines = CombinedIndex(enumers, 2);

                        foreach (int[] idxArray in combines)
                        {
                            string exception = string.Join(" AND ", idxArray.Select(x => $"row{x}='{last[x]}'"));
                            var findrows = table.Select(exception);

                            if (findrows.Any() && findrows.Length >= _lastOrder * 0.01)
                            {
                                var ords = new List<int>();
                                for (int i = 0; i < findrows.Length; i++)
                                {
                                    int ord = findrows[i].Field<int>("Id") + 1;
                                    if (ord <= _lastOrder)
                                    {
                                        ords.Add(ord);
                                    }
                                }

                                var finddatas = table.AsEnumerable().Where(x => ords.Contains(x.Field<int>("Id"))).ToList();
                                var temp = new List<string[]>();
                                foreach (DataRow row in finddatas)
                                {
                                    var tmps = new List<string>();
                                    for (int i = 1; i < table.Columns.Count; i++)
                                    {
                                        var tmp = row[i].ToString();
                                        tmps.Add(tmp);
                                    }
                                    temp.Add(tmps.ToArray());
                                }

                                //pivots
                                var pivots = new List<string[]>();  //행9 열24
                                for (int i = 0; i < temp[0].Length; i++)
                                {
                                    var ea = temp.Select(x => x[i]).ToArray();
                                    pivots.Add(ea);
                                }

                                for (int i = 0; i < pivots.Count; i++)
                                {
                                    var each = pivots[i];
                                    var arrs = new List<int[]>();
                                    for (int j = 0; j < each.Length; j++)
                                    {
                                        int[] arr = StringToArray(each[j]);
                                        arrs.Add(arr);
                                    }

                                    for (int j = 0; j < arrs[0].Length; j++)
                                    {
                                        var ea = arrs.Select(x => x[j]).ToArray();
                                        var dit = ea.Distinct().ToList();
                                        if (dit.Count == 1 && idxArray.Contains(i))
                                        {
                                            string s1 = $"{key} / {string.Join(",", idxArray)} / {temp.Count} / {i} / {j} / {dit.First()}";

                                            if (AllCheckListBox.InvokeRequired)
                                            {
                                                AllCheckListBox.Invoke((Action)(() =>
                                                {
                                                    AllCheckListBox.Items.Add(s1);
                                                }));
                                            }
                                            else
                                            {
                                                AllCheckListBox.Items.Add(s1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    });
                    await task;
                }
            }
            AllCheckButton.Enabled = true;
            AllCheckButton.Text = "전체 자동검사";
        }





        //**************************  메서드  **************************




        /// <summary>
        /// 항목의 sql 전체 데이터
        /// </summary>
        /// <param name="item">컬럼명</param>
        /// <returns>딕셔너리(키:회차, 값:문자열배열)</returns>
        private static Dictionary<int, string[]> GetItemSqlDatas(string item)
        {
            var pairs = new Dictionary<int, string[]>();
            string query = $"SELECT Orders,{item} FROM Polygontbl";
            using var context = new LottoDBContext();
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int ord = reader.GetInt32(0);
                var line = reader.GetString(1);
                string polyline = line.Trim().Split('/')[1];
                string[] strings = polyline.Split(',');
                pairs.Add(ord, strings);
            }

            return pairs;
        }

        /// <summary>
        /// 데이터테이블 생성
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private DataTable CreateTable()
        {
            if (!_itemAllDatas.Any())
            {
                throw new Exception("선택항목의 sql 데이터가 비었습니다.");
            }

            using var table = new DataTable();
            table.Clear();
            table.Columns.Add("Id", typeof(int));
            for (int i = 0; i < _itemAllDatas.First().Value.Length; i++)
            {
                string s = $"row{i}";
                table.Columns.Add(s, typeof(string));
            }

            foreach (int key in _itemAllDatas.Keys)
            {
                string[] vals = _itemAllDatas[key];
                DataRow row = table.NewRow();
                row[0] = key;

                for (int i = 0; i < vals.Length; i++)
                {
                    row[i + 1] = vals[i];
                }
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// 데이터테이블 생성
        /// </summary>
        /// <param name="pairs">항목전체 딕셔너리</param>
        /// <returns></returns>
        private DataTable CreateTable(Dictionary<int, string[]> pairs)
        {
            if (!pairs.Any())
            {
                throw new Exception("선택항목의 sql 데이터가 비었습니다.");
            }

            using var table = new DataTable();
            table.Clear();
            table.Columns.Add("Id", typeof(int));
            for (int i = 0; i < pairs.First().Value.Length; i++)
            {
                string s = $"row{i}";
                table.Columns.Add(s, typeof(string));
            }

            foreach (int key in pairs.Keys)
            {
                string[] vals = pairs[key];
                DataRow row = table.NewRow();
                row[0] = key;

                for (int i = 0; i < vals.Length; i++)
                {
                    row[i + 1] = vals[i];
                }
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// sql에서 데이터 가져오기
        /// </summary>
        /// <param name="head">H 혹은 V</param>
        /// <param name="rowCount">행갯수</param>
        /// <param name="gapNumber">시퀸스 이격번호</param>
        /// <param name="order">검사회차</param>
        /// <returns>튜플(당번위치 문자배열, 폴리곤픽셀 문자배열)</returns>
        private static (string[] danposArray, string[] polyposArray) GetPolygonDatas(string head, int rowCount, int gapNumber, int order)
        {
            string item = $"{head}{rowCount:00}{gapNumber:00}";
            using var context = new LottoDBContext();
            var command = context.Database.GetDbConnection().CreateCommand();
            string query = $"SELECT {item} FROM PolygonTbl WHERE Orders = {order}";
            command.CommandText = query;
            context.Database.OpenConnection();
            string line = (string)command.ExecuteScalar();
            var splits = line.Split('/');
            string[] danpos = splits[0].Split(',');
            string[] polpos = splits[1].Split(',');

            return (danpos, polpos);
        }

        /// <summary>
        /// 문자열을 정수배열로 반환
        /// </summary>
        /// <param name="input">문자열</param>
        /// <returns>정수배열</returns>
        private int[] StringToArray(string input)
        {
            return input.Select(x => int.Parse(x.ToString())).ToArray();
        }

        /// <summary>
        /// 전체조합 배열
        /// </summary>
        /// <param name="enumers">인덱스 시퀸스</param>
        /// <param name="length">조합갯수</param>
        /// <returns>정수배열 리스트</returns>
        private List<int[]> CombinedIndex(IEnumerable<int> enumers, int length)
        {
            List<int[]> result = new List<int[]>();

            for (int i = 1; i <= length; i++)
            {
                var johap = Utility.GetCombinations(enumers, i).Select(x => x.ToArray());
                result.AddRange(johap);
            }

            return result;
        }
    }
}
