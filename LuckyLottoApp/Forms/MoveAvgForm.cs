using System.Data;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Geomsas;
using LuckyLottoLibrary;
using System.Collections.Concurrent;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 이동평균 검사하는 폼 클래스
    /// </summary>
    public partial class MoveAvgForm : Form
    {
        #region 필드

        private readonly int _lastOrder;
        private Dictionary<string, int[]> _geomsaPairs;
        private Dictionary<string, int[]> _chulsuPairs;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="lastOrder"></param>
        public MoveAvgForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
        }

        private async void MoveAvgForm_Load(object sender, EventArgs e)
        {
            ExecuteButton.Enabled = false;
            _geomsaPairs = await AllGeomsaDatas();
            _chulsuPairs = await AllChulsuDatas();
            ExecuteButton.Enabled = true;

            foreach (var key in _geomsaPairs.Keys)
            {
                CheckItemComboBox.Items.Add(key);
            }

            CheckItemComboBox.SelectedIndex = 0;
            await Task.Delay(100);
            PresentListView("Col03_0");
        }

        private void CheckItemComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var combox = sender as ComboBox;

            if (combox.SelectedIndex > -1)
            {
                string key = combox.SelectedItem.ToString();
                PresentListView(key);
            }
        }

        private void ResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = ResultListBox.SelectedItem.ToString();
            CheckItemComboBox.SelectedIndex = CheckItemComboBox.FindString(key);
            PresentListView(key);
        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            ExecuteButton.Enabled = false;
            var rstlist = new List<string>();
            
            ExecuteButton.Text = "검사 시작";

            foreach( var key in _chulsuPairs.Keys )
            {
                var datas = _chulsuPairs[key];
                var pairs = new Dictionary<int, (int chul, List<double> ds)>();
                for (int i = 30; i < _lastOrder; i++)
                {
                    var ds = new List<double>();
                    for (int j = 1; j <= 6; j++)
                    {
                        int n = j * 5;
                        double d = datas.Skip(i - n).Take(n).Average();
                        ds.Add(d);
                    }

                    pairs.Add(i + 1, (datas[i], ds));
                }

                var task = Task.Run(() =>
                {
                    int mincnt = 0;
                    var passlst = new List<bool>();
                    for (int i = 0; i < 6; i++)
                    {
                        var each = pairs.Values.Select(x => x.ds[i]).ToArray();
                        double max = each.Max(), min = each.Min();
                        double d = Math.Abs(max - min) / 10;
                        double last = each[^1];
                        bool pass = false;
                        if (last <= min + d)
                        {
                            pass = true;
                        }

                        var (real, maxt) = RealMaxCount(each);
                        if (pass && real >= maxt)
                        {
                            mincnt++;
                        }
                        passlst.Add(pass);
                    }

                    if (mincnt > 0 && (passlst.Count(x => x == true) >= 2 || passlst[0] == true))
                    {
                        rstlist.Add(key);
                    }
                });
                await task;
            }

            if (rstlist.Any())
            {
                rstlist.OrderBy(x => x).ToList().ForEach(x => ResultListBox.Items.Add(x));
                ResultListBox.Enabled = true;
            }

            ExecuteButton.Enabled = true;
            ExecuteButton.Text = "검사하기";
        }




        //*****************  내부 메서드  ********************



        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        private void PresentListView(string key)
        {
            listView1.Clear();

            listView1.Columns.Add("회 차", 80, HorizontalAlignment.Center);
            listView1.Columns.Add("출 수", 50, HorizontalAlignment.Center);

            int[] datas = _chulsuPairs[key];
            for (int i = 5; i <= 30; i += 5)
            {
                string s = $"{i}구간";
                listView1.Columns.Add(s, 100, HorizontalAlignment.Center);
            }

            var pairs = new Dictionary<int, (int chul, List<double> ds)>();
            for (int i = 30; i < _lastOrder; i++)
            {
                var ds = new List<double>();
                for (int j = 1; j <= 6; j++)
                {
                    int n = j * 5;
                    double d = datas.Skip(i - n).Take(n).Average();
                    ds.Add(d);
                }

                pairs.Add(i + 1, (datas[i], ds));
            }

            var eachs = new List<double[]>();
            for (int i = 0; i < 6; i++)
            {
                var each = pairs.Values.Select(x => x.ds[i]).ToArray();
                eachs.Add(each);
            }

            listView1.BeginUpdate();
            foreach (int ord in pairs.Keys)
            {
                var lvi = new ListViewItem(ord.ToString()) {  UseItemStyleForSubItems = false };
                var vals = pairs[ord];
                lvi.SubItems.Add(vals.chul.ToString());
                var list = vals.ds;

                for (int i = 0; i < list.Count; i++)
                {
                    double d = list[i];
                    lvi.SubItems.Add(d.ToString("F4"));
                    
                    //최소값 최대값 찾기
                    double min = eachs[i].Min();
                    double max = eachs[i].Max();

                    double ps = Math.Abs(min - max) / 10;
                    if (d <= min + ps)
                    {
                        lvi.SubItems[i + 2].BackColor = Color.Cyan;
                    }
                    if (max - ps <= d)
                    {
                        lvi.SubItems[i + 2].BackColor = Color.LightSalmon;
                    }
                }

                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();

            //맨 아래에 평균값을 넣는다
            var btm = new ListViewItem("평 균");
            btm.SubItems.Add(pairs.Values.Select(x => x.chul).Average().ToString("F4"));
            btm.SubItems.Add(eachs[0].Average().ToString("F4"));
            btm.SubItems.Add(eachs[1].Average().ToString("F4"));
            btm.SubItems.Add(eachs[2].Average().ToString("F4"));
            btm.SubItems.Add(eachs[3].Average().ToString("F4"));
            btm.SubItems.Add(eachs[4].Average().ToString("F4"));
            btm.SubItems.Add(eachs[5].Average().ToString("F4"));

            listView1.Items.Add(btm);
            listView1.EnsureVisible(pairs.Count);

            var numbers = _geomsaPairs[key].OrderBy(x => x);
            SelectNumberTextBox.Text = String.Join(",", numbers.Select(x => x.ToString("00"))) +
                                        "\r\n\r\n" + "갯수: " + numbers.Count() + "개";
        }

        /// <summary>
        /// 검사에 사용할 번호데이터 딕셔너리
        /// </summary>
        /// <returns></returns>
        private static async Task<Dictionary<string, int[]>> AllGeomsaDatas()
        {
            int[] collength = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
            string[] fixChulsuTblColumns = { "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon" };
            var dic = new Dictionary<string, int[]>();

            var task = Task.Run(() =>
            {
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
            });

            await task;
            return dic;
        }

        /// <summary>
        /// 검사에 사용할 출수데이터 딕셔너리
        /// </summary>
        /// <returns></returns>
        private static async Task<Dictionary<string, int[]>> AllChulsuDatas()
        {
            int[] collength = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
            string[] fixChulsuTblColumns = { "Sosamhap", "Beondae", "Slipsu", "Kkeutbeon" };

            var dic = new Dictionary<string, int[]>();

            foreach (int length in collength)
            {
                string s = "Col" + length.ToString("00");
                string query = "SELECT " + s + " FROM GridTbl";

                var task = Task.Run(() =>
                {
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
                });

                await task;
            }

            foreach (var name in fixChulsuTblColumns)
            {
                string s = name + "_";
                string query = "SELECT " + name + " FROM FixChulsuTbl";

                var task = Task.Run(() =>
                {
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
                        var each = list.Select(x => x.ElementAt(i)).ToArray();
                        dic.Add(s + i, each);
                    }
                });

                await task;
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

                var task = Task.Run(() =>
                {
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
                });

                await task;
            }

            return dic;
        }

        /// <summary>
        /// 후방연속, 연속최대값
        /// </summary>
        /// <param name="array">실수 배열</param>
        /// <returns>튜플(후방연속 갯수, 연속최대값)</returns>
        private (int real, int max) RealMaxCount(double[] array)
        {
            double last = array[^1];

            int real = 0, max = 0, dub = 0;
            real = array.AsEnumerable().Reverse().TakeWhile(x => x.CompareTo(last) == 0).Count();
            var arr = array[..^real];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].CompareTo(last) == 0)
                {
                    dub++;
                }
                else
                {
                    if (dub > max)
                    {
                        max = dub;
                    }
                    dub = 0;
                }
            }
            if (dub > max)
            {
                max = dub;
            }

            return (real, max);
        }

    }
}
