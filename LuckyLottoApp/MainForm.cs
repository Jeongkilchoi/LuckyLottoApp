using LuckyLottoApp.Utilities;
using LuckyLottoLibrary;

namespace LuckyLottoApp
{
    /// <summary>
    /// 행운로또 주화면 폼 클래스
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            int order = LuckyLottoApp.Utilities.Utility.GetLastOrder();
            _lastOrder = order;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("번호", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("출수", 45, HorizontalAlignment.Center);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = @"G:\Program Files\LuckyLotto\GeomsaFiles\";
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var graphic = e.Graphics;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var lastNums = Utility.DangbeonOfOrder(Utility.GetLastOrder());

            //가로선 그리기
            for (int i = 0; i < 8; i++)
            {
                using var pen = new Pen(Color.Gray, 0.5F);
                var pt1 = new Point(25, 25 + (i * 50));
                var pt2 = i != 7 ? new Point(25 + (50 * 7), 25 + (i * 50)) :
                                   new Point(25 + (50 * 3), 25 + (i * 50));

                graphic.DrawLine(pen, pt1, pt2);
            }

            //세로선 그리기
            for (int i = 0; i < 8; i++)
            {
                using var pen = new Pen(Color.Gray, 0.5F);
                var pt1 = new Point(25 + (i * 50), 25);
                var pt2 = i < 4 ? new Point(25 + (i * 50), 25 + (50 * 7)) :
                                  new Point(25 + (i * 50), 25 + (50 * 6));

                graphic.DrawLine(pen, pt1, pt2);
            }

            int n = 0;
            var size = new Size(50, 50);
            var points = new List<Point>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    n++;

                    if (n <= 45)
                    {
                        //숫자 그리기
                        using var font = new Font("Swis721 Cn BT", 16, FontStyle.Regular, GraphicsUnit.Point);
                        var rect = new Rectangle(25 + (50 * j), 25 + (50 * i), size.Width, size.Height);
                        var rect1 = new Rectangle(25 + (50 * j) + 22, 25 + (50 * i) + 22, 6, 6);
                        StringFormat stringFormat = new()
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        graphic.DrawString(n.ToString(), font, Brushes.Black, rect, stringFormat);

                        if (lastNums.Contains(n))
                        {
                            //최종당번이면 가운데 빨간점 그리기
                            var brush = new SolidBrush(Color.FromArgb(150, Color.Red));
                            graphic.FillEllipse(brush, rect1);

                            //가운 포인트 저장
                            var pt = new Point(rect.X + 25, rect.Y + 25);
                            points.Add(pt);
                        }

                        //제외수 포함되면 X 표시
                        if (ExceptList.Any() && ExceptList.Contains(n))
                        {
                            var exppts1 = new Point[2];
                            exppts1[0] = new Point(25 + (50 * j), 25 + (50 * i));
                            exppts1[1] = new Point(25 + (50 * j) + 50, 25 + (50 * i) + 50);
                            var exppts2 = new Point[2];
                            exppts2[0] = new Point(25 + (50 * j) + 50, 25 + (50 * i));
                            exppts2[1] = new Point(25 + (50 * j), 25 + (50 * i) + 50);

                            graphic.DrawLines(new Pen(Color.Red, 2), exppts1);
                            graphic.DrawLines(new Pen(Color.Red, 2), exppts2);
                        }

                        //고정수 포함되면 ㅁ 표시
                        if (FixedList.Any() && FixedList.Contains(n))
                        {
                            var rect2 = new Rectangle(25 + (50 * j) + 5, 25 + (50 * i) + 5, 40, 40);
                            var brush = new SolidBrush(Color.FromArgb(100, Color.Black));
                            graphic.FillEllipse(brush, rect2);
                        }
                    }
                }
            }

            //당번번호 선잇기
            using (var danpen = new Pen(Color.Green, 1))
            {
                graphic.DrawLines(danpen, points.ToArray());
            }

            var seven = LuckyLibrary.HorizontalFlowDatas(7);
            var idxpoint = lastNums.Select(x => Convexhull.IndexToPoint(x, seven)).ToList();
            var hull = Convexhull.GetConvexHull(idxpoint);
            hull.Add(hull.First());
            var hullpts = hull.Select(g => new Point(g.Y * 50 + 50, g.X * 50 + 50));
            //외곽선 채우기
            using (var hullbrush = new SolidBrush(Color.FromArgb(100, Color.Cyan)))
            {
                graphic.FillPolygon(hullbrush, hullpts.ToArray());
            }

            e.Dispose();
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            var size = new Size(50, 50);

            //그림판에서 마우스 클릭한 위치
            var coordinate = e.Location;
            int left = coordinate.X;
            int top = coordinate.Y;

            var pos = new List<int>();
            for (int i = 1; i < 7; i++)
            {
                int n = i * 50 + 25;
                pos.Add(n);
            }

            int x = left < pos.First() ? 0 : pos.Select((v, i) => (v, i)).Where(x => x.v < left).Select(x => x.i).Last() + 1;
            int y = top < pos.First() ? 0 : pos.Select((v, i) => (v, i)).Where(x => x.v < top).Select(x => x.i).Last() + 1;
            int number;

            //하단 우측은 빈공간이 더 넒으므로 45번으로 처리
            if (y == 6 && x > 2)
            {
                number = 45;
            }
            else
            {
                number = LuckyLibrary.HorizontalFlowDatas(7)[y][x];
            }

            //마우스 왼클릭하면 제외수 추가
            if (e.Button == MouseButtons.Left)
            {
                //제외리스트에 포함되어 있지 않으면 추가
                if (!FixedList.Contains(number) & !ExceptList.Contains(number))
                {
                    ExceptList.Add(number);
                }
                else
                {
                    //이미 제외리스트에 포함되었다면 제거
                    ExceptList.Remove(number);
                }
            }

            //마우스 우클릭하면 고정수 추가
            if (e.Button == MouseButtons.Right)
            {
                if (!ExceptList.Contains(number) & !FixedList.Contains(number))
                {
                    FixedList.Add(number);
                }
                else
                {
                    FixedList.Remove(number);
                }
            }

            FixedList.Sort();
            ExceptList.Sort();

            ExceptTextBox.Text = String.Join(",", ExceptList);
            FixedTextBox.Text = String.Join(",", FixedList);

            pictureBox1.Invalidate();
        }
    }
}