using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Geomsas;
using LuckyLottoApp.Utilities;
using Microsoft.EntityFrameworkCore;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuckyLottoApp.Forms
{
    public partial class ChulsuCandleForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private readonly string[] _candleTypes;
        private readonly int[] _xPosition = new int[50];
        private Dictionary<string, int[]> _geomsaPairs;
        private Dictionary<string, int[]> _chulsuPairs;
        private List<(int ord, int start, int end, int top, int bottom, int sum, string type, double avg)> _selectDatas;
        private string _key = "Col03_0";
        #endregion

        public ChulsuCandleForm(int lastOrder)
        {
            InitializeComponent();
            _lastOrder = lastOrder;
            _candleTypes = new string[] { "대양봉", "중양봉", "소양봉", "中양봉", "┻양봉", "┳양봉", "대음봉", "중음봉", "소음봉",
                                          "中음봉", "┻음봉", "┳음봉", "─중봉", "╋중봉", "┻중봉", "┳중봉" };

            _geomsaPairs = AllGeomsaDatas();
            _chulsuPairs = AllChulsuDatas();
            for (int i = 0; i < 50; i++)
            {
                int n = 40 + (i * 18);
                _xPosition[i] =  n;
            }
        }

        private void ChulsuCandleForm_Load(object sender, EventArgs e)
        {
            foreach (var key in _geomsaPairs.Keys)
            {
                SelectItemComboBox.Items.Add(key);
            }

            SelectItemComboBox.SelectedIndex = 0;
        }

        private void CandlePictureBox_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (!(_selectDatas?.Any() ?? false))
            {
                var secdatas = DataOfSection();
                var seldatas = secdatas.Skip(secdatas.Count - 50).ToList();
                _selectDatas = seldatas;
            }

            DrawLine(g);
            DrawCandle(g);
        }

        private void SelectItemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectItemComboBox.SelectedIndex > -1)
            {
                LastArrayLabel.Text = "최종출수: ";
                _key = SelectItemComboBox.SelectedItem.ToString();
                var secdatas = DataOfSection();
                var seldatas = secdatas.Skip(secdatas.Count - 50).ToList();
                _selectDatas = seldatas;
                var tpl = secdatas.Last();
                var lastarray = _chulsuPairs[_key][^5..];
                LastArrayLabel.Text += String.Join(", ", lastarray);
                BeforeLabel.Text = $"시가:{tpl.start},  종가:{tpl.end},  저가:{tpl.bottom},  고가:{tpl.top},  출합:{tpl.sum},  평균:{tpl.avg:0.00}";
                var seldt = _geomsaPairs[_key];
                NumberTextBox.Text = string.Join(",", seldt.OrderBy(x => x).Where(x => x >= 1 && x <= 45).Select(x => x.ToString("00")));
                CandlePictureBox.Invalidate();
            }
        }

        private void ResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentItem = ResultListBox.SelectedItem.ToString();
            _key = currentItem;

            SelectItemComboBox.SelectedIndex = SelectItemComboBox.FindString(currentItem);
        }

        private void CandlePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PositionTextBox.Text = string.Empty;
            int x = e.X;
            int y = e.Y;

            int basey = 30 + (13 * 28);
            for (int i = 0; i < _xPosition.Length; i++)
            {
                int pos = _xPosition[i];
                if (pos - 3 <= x && x <= pos + 3)
                {
                    var sel = _selectDatas[i];
                    string type = sel.type[^2..];
                    int py = 0;
                    if (type.Equals("음봉"))
                    {
                        int max = Math.Max(sel.start, sel.top);
                        py = basey - (max * 28);
                    }
                    else
                    {
                        int max = Math.Max(sel.end, sel.top);
                        py = basey - (max * 28);
                    }

                    if (py - 5 <= y && y <= py + 5)
                    {
                        PositionTextBox.Text = $"시가:{sel.start} 종가:{sel.end} 저가:{sel.bottom} 고가:{sel.top}" + 
                            $"\r\n출합:{sel.sum} 평균:{sel.avg:0.00} 타입:{sel.type}";
                    }
                }
            }

        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            ResultListBox.Enabled = true;
            foreach (string key in _chulsuPairs.Keys)
            {
                var tpl = DataOfSection(key);
                var sectpl = tpl.Skip(tpl.Count - 50).ToList();
                var lastdt = tpl.Last();
                var nexttpl = tpl.Select((val, idx) => (val, idx))
                    .Where(g => g.val.type.Equals(lastdt.type)).Select(g => g.idx + 1).Where(g => g < tpl.Count)
                    .Select(x => tpl[x]).ToList();

                if ((nexttpl?.Any() ?? false) && nexttpl.Count > 20 && sectpl.Select(x => x.avg).Average() > lastdt.avg)
                {
                    double sumavg = nexttpl.Select(x => x.avg).Average();
                    var nxtlst = nexttpl.Select(x => x.end).ToList();
                    if (sumavg > lastdt.avg && nxtlst.Last() == 0)
                    {
                        var tpls = NextReal.RealMaxCount(nxtlst);
                        if (tpls.realCount >= tpls.maxCount)
                        {
                            ResultListBox.Items.Add(key);
                        }
                    }
                }
            }
        }



        //**********************  메서드  ***********************



        /// <summary>
        /// 가로 세로 선 그리기
        /// </summary>
        /// <param name="g"></param>
        private void DrawLine(Graphics g)
        {
            int lfblank = 40, tpblank = 30;
            var size = new Size(18, 28);

            //가로선 그리기
            for (int i = 0; i < 14; i++)
            {
                var pt1 = new Point(lfblank - 10, tpblank + (i * size.Height));
                var pt2 = new Point(lfblank + (50 * size.Width), tpblank + (i * size.Height));
                using (var gridpen = new Pen(Color.FromArgb(80, Color.Cyan)))
                {
                    g.DrawLine(gridpen, pt1, pt2);
                }
            }
            //세로선 그리기
            for (int i = 0; i <= 50; i++)
            {
                var pt1 = new Point(lfblank + (i * size.Width), tpblank);
                var pt2 = new Point(lfblank + (i * size.Width), tpblank + 10 + (13 * size.Height));
                using (var gridpen = new Pen(Color.FromArgb(80, Color.Cyan)))
                {
                    g.DrawLine(gridpen, pt1, pt2);
                }
            }

            //하단에 역순으로 회차순번 그리기
            for (int i = 0; i <= 50; i++)
            {
                int n = 50 - i;
                if (i % 5 == 0)
                {
                    var cnpt = new Point((i * size.Width) + lfblank, size.Height * 14 + 20);
                    var rect = new Rectangle(cnpt.X - 18, cnpt.Y - 10, 36, 20);

                    using var font = new Font("맑은고딕", 10, FontStyle.Regular, GraphicsUnit.Point);
                    StringFormat stringFormat = new()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    string s = n == 0 ? "0" : "-" + n;
                    g.DrawString(s, font, Brushes.Black, rect, stringFormat);
                }
            }

            //좌측에 출수 그리기
            int[] chularr = { 6, 5, 4, 3, 2, 1, 0, 6, 5, 4, 3, 2, 1, 0 };
            for (int i = 0; i < chularr.Length; i++)
            {
                var cnpt = new Point(lfblank - 20, (size.Height * i) + tpblank);
                var rect = new Rectangle(cnpt.X - 10, cnpt.Y - 10, 20, 20);

                using var font = new Font("맑은고딕", 10, FontStyle.Regular, GraphicsUnit.Point);
                StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(chularr[i].ToString(), font, Brushes.Black, rect, stringFormat);
            }
        }

        /// <summary>
        /// 캔들챠트 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <exception cref="Exception"></exception>
        private void DrawCandle(Graphics g)
        {
            int lfblank = 40, tpblank = 30;
            var size = new Size(18, 28);
            int[] chularr = { 6, 5, 4, 3, 2, 1, 0, 6, 5, 4, 3, 2, 1, 0 };
            float average = (float)_selectDatas.Select(x => x.avg).Average();
            
            //위쪽 0 인 세로축 위치 (0 인 위치이므로 0 위치에서 차이만큼 뺀다)
            int upzeroy = tpblank + (size.Height * 6);
            int dwzeroy = tpblank + (size.Height * 13);
            float gap = average * size.Height;
            var pey1 = new PointF(lfblank, upzeroy - gap);
            var pey2 = new PointF(lfblank + (50 * size.Width), upzeroy - gap);

            //구간 전체 평균선 그리기
            using (var grpen = new Pen(Color.Green, 1.6f))
            {
                g.DrawLine(grpen, pey1, pey2);
            }

            var eaavgs = new PointF[_selectDatas.Count];
            for (int i = 0; i < _selectDatas.Count; i++)
            {
                var dt = _selectDatas[i];
                int x = lfblank + (i * size.Width);

                //고저 사이 선 그리기
                if (dt.top != dt.bottom)
                {
                    var pt1 = new PointF(x, dwzeroy - (dt.top * size.Height));
                    var pt2 = new PointF(x, dwzeroy - (dt.bottom * size.Height));
                    using (var pen = new Pen(Color.Black, 2))
                    {
                        g.DrawLine(pen, pt1, pt2);
                    }
                }

                //개별 평균선 
                float eagap = (float)(dt.avg * size.Height);
                var eapt1 = new PointF((float)x, upzeroy - eagap);
                eaavgs[i] = eapt1;

                //타입 행태 그리기
                string sel = dt.type;
                string type = sel[^2..];
                if (type.Equals("중봉"))
                {
                    //중봉 - 선 그리기
                    if (dt.start == dt.end)
                    {
                        var ppt1 = new Point(x - 5, dwzeroy - (dt.end * size.Height));
                        var ppt2 = new Point(x + 5, dwzeroy - (dt.end * size.Height));
                        using (var blpen = new Pen(Color.Black, 2))
                        {
                            g.DrawLine(blpen, ppt1, ppt2);
                        }
                    }
                }
                else if (type.Equals("양봉"))
                {
                    //양봉막대 그리기
                    var rect = new Rectangle(x - 5, dwzeroy - (size.Height * dt.end), 10, Math.Abs(dt.end - dt.start) * size.Height);
                    using (var brush = new SolidBrush(Color.Red))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    //음봉막대 그리기
                    var rect = new Rectangle(x - 5, dwzeroy - (size.Height * dt.start), 10, Math.Abs(dt.end - dt.start) * size.Height);
                    using (var brush = new SolidBrush(Color.Blue))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
            }

            //개별 평균값 선 그리기
            using (var blpen = new Pen(Color.Blue, 1.6f))
            {
                g.DrawLines(blpen, eaavgs);
            }
        }

        /// <summary>
        /// 검사에 사용할 번호데이터 딕셔너리
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, int[]> AllGeomsaDatas()
        {
            int[] collength = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
            string[] fixChulsuTblColumns = { "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon" };
            var dic = new Dictionary<string, int[]>();

            foreach (int length in collength)
            {
                string path = Application.StartupPath + @"DataFiles\" + "col" + length.ToString("00") + ".csv";
                var lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith("#"))
                    {
                        var nums = line.Split(',').Select(x => int.Parse(x.ToString())).ToArray();
                        string s1 = "Col" + length.ToString("00") + "_" + i;
                        dic.Add(s1, nums);
                    }
                }
            }

            var gs = new Geomsa();
            foreach (var name in fixChulsuTblColumns)
            {
                string s = name + "_";
                var data = gs.FixChulsuDatas()[name];

                for (int i = 0; i < data.Count; i++)
                {
                    var dt = data[i];
                    dic.Add(s + i, dt);
                }
            }

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
            var hwc = gs.HotWarmColdDatas().ToList();//HotwarmcoldDatas();
            dic.Add("Hotsu", hwc[0]);
            dic.Add("Warmsu", hwc[1]);
            dic.Add("Coldsu", hwc[2]);
            dic.Add("Daluksu", gs.MonthData().ToArray());
            dic.Add("Ihutsu", gs.AroundData().ToArray());
            dic.Add("Binsu", gs.EmptyData().ToArray());
            var triangle = gs.TriAngularDatas().ToList();//TriangularDatas();
            dic.Add("Samkak1", triangle[0]);
            dic.Add("Samkak2", triangle[1]);
            dic.Add("Samkak3", triangle[2]);
            dic.Add("Samkak4", triangle[3]);
            var nak1 = gs.NaksutDatas(0).ToList();
            dic.Add("Snaksu1", nak1[0]);
            dic.Add("Snaksu2", nak1[1]);
            dic.Add("Snaksu3", nak1[2]);
            dic.Add("Snaksu4", nak1[3]);
            dic.Add("Snaksu5", nak1[4]);
            dic.Add("Snaksu6", nak1[5]);
            var nak2 = gs.NaksutDatas(1).ToList();
            dic.Add("Rnaksu1", nak2[0]);
            dic.Add("Rnaksu2", nak2[1]);
            dic.Add("Rnaksu3", nak2[2]);
            dic.Add("Rnaksu4", nak2[3]);
            dic.Add("Rnaksu5", nak2[4]);
            dic.Add("Rnaksu6", nak2[5]);

            return dic;
        }

        /// <summary>
        /// 검사에 사용할 출수데이터 딕셔너리
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, int[]> AllChulsuDatas()
        {
            int[] collength = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
            string[] fixChulsuTblColumns = { "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon" };

            var dic = new Dictionary<string, int[]>();

            foreach (int length in collength)
            {
                string s = "Col" + length.ToString("00");
                string query = "SELECT " + s + " FROM GridTbl";

                var list = new List<int[]>();
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string s1 = reader.GetString(0);
                    int[] array = s1.Select(x => int.Parse(x.ToString())).ToArray();
                    list.Add(array);
                }
                //pivot
                for (int i = 0; i < list.First().Length; i++)
                {
                    string s2 = s + "_" + i;
                    var each = list.Select(x => x.ElementAt(i)).ToArray();
                    dic.Add(s2, each);
                }
            }

            foreach (var name in fixChulsuTblColumns)
            {
                string s = name + "_";
                string query = "SELECT " + name + " FROM FixChulsuTbl";

                var list = new List<int[]>();
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string s1 = reader.GetString(0);
                    int[] array = s1.Select(x => int.Parse(x.ToString())).ToArray();
                    list.Add((array));
                }
                //pivot
                for (int i = 0; i < list.First().Length; i++)
                {
                    var each = list.Select(x => x.ElementAt(i)).ToArray();
                    dic.Add(s + i, each);
                }
            }

            string[] chulname =
            {
                "Beisu4", "Beisu5", "Beisu6", "Beisu7", "Beisu8", "Beisu9",
                "Jekobsu", "Ihangsu", "Pivosu", "Revpivo",
                "Hotsu", "Warmsu", "Coldsu", "Daluksu", "Ihutsu", "Binsu",
                "Samkak1", "Samkak2", "Samkak3", "Samkak4",
                "Snaksu1", "Snaksu2", "Snaksu3", "Snaksu4", "Snaksu5", "Snaksu6",
                "Rnaksu1", "Rnaksu2", "Rnaksu3", "Rnaksu4", "Rnaksu5", "Rnaksu6",
            };

            foreach (var name in chulname)
            {
                string query = "SELECT " + name + " FROM ChulsuTbl";

                var list = new List<int>();
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int ord = reader.GetInt32(0);
                    list.Add(ord);
                }
                dic.Add(name, list.ToArray());
            }

            return dic;
        }

        /// <summary>
        /// 구간 출수 데이터
        /// </summary>
        /// <param name="gap">검사구간</param>
        /// <returns>튜플(회차, 시가, 종가, 고가, 저가, 구갑합, 타입, 평균값)</returns>
        private List<(int ord, int start, int end, int top, int bottom, int sum, string type, double avg)> DataOfSection(int gap = 5)
        {
            var alls = _chulsuPairs[_key];
            var lists = new List<(int ord, int start, int end, int top, int bottom, int sum, string type, double avg)>();
            for (int i = gap; i <= alls.Length; i++)
            {
                var secarr = alls.Skip(i - gap).Take(5).ToArray();
                lists.Add((i, secarr[0], secarr[^1], secarr.Max(), secarr.Min(), secarr.Sum(), CandleType(secarr), secarr.Sum() / (double)gap));
            }

            return lists;
        }

        /// <summary>
        /// 구간 출수 데이터
        /// </summary>
        /// <param name="key">검사항목</param>
        /// <param name="gap">검사구간</param>
        /// <returns>튜플(회차, 시가, 종가, 고가, 저가, 구갑합, 타입, 평균값)</returns>
        private List<(int ord, int start, int end, int top, int bottom, int sum, string type, double avg)> DataOfSection(string key, int gap = 5)
        {
            var alls = _chulsuPairs[key];
            var lists = new List<(int ord, int start, int end, int top, int bottom, int sum, string type, double avg)>();
            for (int i = gap; i <= alls.Length; i++)
            {
                var secarr = alls.Skip(i - gap).Take(5).ToArray();
                lists.Add((i, secarr[0], secarr[^1], secarr.Max(), secarr.Min(), secarr.Sum(), CandleType(secarr), secarr.Sum() / (double)gap));
            }

            return lists;
        }

        /// <summary>
        /// 5구간출수의 타입을 반환
        /// </summary>
        /// <param name="array">5구간 출수 배열</param>
        /// <returns></returns>
        private string CandleType(int[] array)
        {
            int start = array.First();
            int end = array.Last();
            int min = array.Min();
            int max = array.Max();

            if (start < end)
            {
                //대, 중, 소양봉
                if (start <= min && max <= end)
                {
                    int val = end - start;

                    return val switch
                    {
                        >= 3 => _candleTypes[0],
                        >= 2 => _candleTypes[1],
                        >= 1 => _candleTypes[2],
                        _ => throw new Exception("참양봉")
                    };
                }
                else if (start > min && max > end)
                {
                    //+양봉
                    return _candleTypes[3];

                }
                else if (start <= min && max > end)
                {
                    //ㅗ양봉
                    return _candleTypes[4];
                }
                else if (start > min && max <= end)
                {
                    //ㅜ양봉
                    return _candleTypes[5];
                }
                else
                {
                    throw new Exception("무양봉");
                }
            }
            else if (start > end)
            {
                //대, 중, 소음봉
                if (start >= max && min >= end)
                {
                    int val = start - end;

                    return val switch
                    {
                        >= 3 => _candleTypes[6],
                        >= 2 => _candleTypes[7],
                        >= 1 => _candleTypes[8],
                        _ => throw new Exception("참음봉")
                    };
                }
                else if (start < max && min < end)
                {
                    //+음봉
                    return _candleTypes[9];

                }
                else if (start < max && min >= end)
                {
                    //ㅗ음봉
                    return _candleTypes[10];
                }
                else if (start >= max && min < end)
                {
                    //ㅜ음봉
                    return _candleTypes[11];
                }
                else
                {
                    throw new Exception("무음봉");
                }
            }
            else
            {
                if (start == min && end == max)
                {
                    //-중봉
                    return _candleTypes[12];
                }
                else if (start > min && start < max)
                {
                    //+중봉
                    return _candleTypes[13];
                }
                else if (start < max)
                {
                    //ㅗ중봉
                    return _candleTypes[14];
                }
                else if (start > min)
                {
                    //ㅜ중봉
                    return _candleTypes[15];
                }
                else
                {
                    throw new Exception("무중봉");
                }
            }
        }


    }
}
