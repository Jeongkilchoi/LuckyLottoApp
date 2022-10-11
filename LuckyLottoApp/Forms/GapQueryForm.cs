using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 이격간격으로 시퀸스 데이터 조회하는 폼 클래스
    /// </summary>
    public partial class GapQueryForm : Form
    {
        #region 필드

        private readonly int _lastOrder;
        private int _rowCount = 3;
        private int _order;
        private int _wayIndex = 0;
        private bool _isHor = true;
        private int[] _gapInts;
        private int[] _sequenceInts;
        private readonly string[] _wayItems;
        private List<GridDirection> _gridDirections = new();

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public GapQueryForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            _gapInts = LuckyLibrary.SeqenceGapInts;
            _wayItems = new[]
            {
                "정 상", "좌사선", "우사선", "좌나선", "우나선", "좌지그", "우지그"
            };

            foreach (GridDirection direction in Enum.GetValues(typeof(GridDirection)))
            {
                _gridDirections.Add(direction);
            }

        }

        private void GapQueryForm_Load(object sender, EventArgs e)
        {
            OrderNumericUpDown.Value = _lastOrder;
            OrderNumericUpDown.Maximum = _lastOrder;
            _gapInts.ToList().ForEach(x => GapComboBox.Items.Add(x));
            GapComboBox.SelectedIndex = 0;
            WayComboBox.Items.AddRange(_wayItems);
            WayComboBox.SelectedIndex = 0;
        }

        private void ViewPictureBox_Paint(object sender, PaintEventArgs e)
        {
            var size = new Size(36, 36);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var datas = (_isHor) ? LuckyLibrary.HorizontalFlowInts(_sequenceInts, _rowCount, _gridDirections[_wayIndex]) :
                                   LuckyLibrary.VerticalFlowInts(_sequenceInts, _rowCount, _gridDirections[_wayIndex]);
            var dang = Utility.DangbeonOfOrder(_order);

            DisplayHorizontal(datas, dang);
            DisplayVertical(datas, dang);

            for (int i = 0; i < _rowCount; i++)
            {
                var array = datas[i];
                for (int j = 0; j < array.Length; j++)
                {
                    int n = array[j];

                    var point = new Point(j * size.Width, i * size.Height);
                    var rect = new Rectangle(point, size);

                    using (var pen = new Pen(Color.Gray, 0.1F))
                    {
                        g.DrawRectangle(pen, rect);
                    }

                    //숫자 그리기
                    using (var font = new Font("맑은고딕", 10))
                    {
                        var stringFormat = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        string s = (n == 0) ? "" : n.ToString();
                        g.DrawString(s, font, Brushes.Black, rect, stringFormat);
                    }

                    var circl = new Rectangle(point.X + 4, point.Y + 4, 36 - 8, 36 - 8);

                    //당번에 원 그리기
                    if (dang.Contains(n))
                    {
                        using var pen = new Pen(Color.Red, 2);
                        g.DrawEllipse(pen, circl);
                    }
                }
            }
        }

        private void HorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (HorRadioButton.Checked)
                _isHor = true;
            else
                _isHor = false;
        }

        private void GapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GapComboBox.SelectedIndex > -1)
            {
                int sel = (int)GapComboBox.SelectedItem;
                _sequenceInts = LuckyLibrary.NewSequenceLists()[sel];
                ViewPictureBox.Invalidate();
            }
        }

        private void WayComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (WayComboBox.SelectedIndex > -1)
            {
                _wayIndex = WayComboBox.SelectedIndex;
                ViewPictureBox.Invalidate();
            }
        }

        private void RowCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _rowCount = (int)RowCountNumericUpDown.Value;
            ViewPictureBox.Invalidate();
        }

        private void OrderNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _order = (int)OrderNumericUpDown.Value;
            ViewPictureBox.Invalidate();
        }




        //*****************************  메서드  *****************************



        /// <summary>
        /// 세로방향 출수의 인덱스 표시
        /// </summary>
        /// <param name="datas">검사 번호리스트</param>
        /// <param name="dang">당번</param>
        private void DisplayVertical(List<int[]> datas, IEnumerable<int> dang)
        {
            int col = datas.Max(x => x.Length);
            var chulsu = new List<int>();

            var allchullists = new List<List<int>>()
            {
                new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>()
            };

            //데이터를 세로로 읽기
            for (int i = 0; i < col; i++)
            {
                var list = new List<int>();

                for (int j = 0; j < datas.Count; j++)
                {
                    try
                    {
                        int n = datas[j][i];
                        list.Add(n);
                    }
                    catch
                    {
                        continue;
                    }
                }

                int dub = list.Intersect(dang).Count();
                chulsu.Add(dub);

                for (int j = 0; j <= 6; j++)
                {
                    var lst = allchullists[j];
                    if (dub == j)
                    {
                        lst.Add(i);
                    }
                }
            }

            string s = "세로방향 검색\r\n\r\n";
            s += $"출수: {string.Join(",", chulsu)}\r\n";
            s += $"타입: {Utility.ConvertTypeIndex(chulsu)}\r\n\r\n";

            for (int i = 0; i < allchullists.Count; i++)
            {
                var ea = allchullists[i];
                if (ea.Any())
                {
                    s += $"{i}출 인덱스: {string.Join(",", ea)}\r\n";
                }
            }

            VerFlowTextBox.Text = s;
        }

        /// <summary>
        /// 가로방향 출수의 인덱스 표시
        /// </summary>
        /// <param name="datas">검사 번호리스트</param>
        /// <param name="dang">당번</param>
        private void DisplayHorizontal(List<int[]> datas, IEnumerable<int> dang)
        {
            var chulsu = datas.Select(x => x.Intersect(dang).Count());
            var allchullists = new List<List<int>>();
            var strs = new List<string>();
            for (int i = 0; i <= 6; i++)
            {
                var ea = chulsu.Select((val, idx) => (val, idx)).Where(x => x.val == i).Select(x => x.idx).ToList();

                if (ea.Any())
                {
                    string s1 = $"{i}출 인덱스: {string.Join(",", ea)}";
                    strs.Add(s1);
                }
            }

            string s = "가로방향 검색\r\n\r\n";
            s += $"출수: {string.Join(",", chulsu)}\r\n";
            s += $"타입: {Utility.ConvertTypeIndex(chulsu)}\r\n\r\n";
            s += string.Join(Environment.NewLine, strs);

            HorFlowTextBox.Text = s;
        }
    }
}
