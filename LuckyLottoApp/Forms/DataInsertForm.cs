using System.Data;
using System.Text;
using HtmlAgilityPack;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Geomsas;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// Sql에 데이터 삽입하는 클래스
    /// </summary>
    public partial class DataInsertForm : Form
    {
        #region 필드

        private const string Quato = "'";

        #endregion

        public DataInsertForm()
        {
            InitializeComponent();
        }

        private async void DangParsingButton_Click(object sender, EventArgs e)
        {
            int noword = Utility.GetNowOrder();
            int sqlord = Utility.GetLastOrder();
            var (order, hoki, dangInts) = await Task.Run(LinkMygoHomepage);
            var docter = await Task.Run(LinklottodrHomepage);

            if (!(order != -1 && docter.Any() && docter.Distinct().Count() == 6))
            {
                throw new Exception("당번 파싱에러.");
            }

            try
            {
                DangOrderTextBox.Text = order.ToString();
                PopDateTextBox.Text = Utility.DateOfOrder(order).ToString("yyyy-MM-dd");
                txtdang1.Text = dangInts[0].ToString();
                txtdang2.Text = dangInts[1].ToString();
                txtdang3.Text = dangInts[2].ToString();
                txtdang4.Text = dangInts[3].ToString();
                txtdang5.Text = dangInts[4].ToString();
                txtdang6.Text = dangInts[5].ToString();
                txtbonus.Text = dangInts[6].ToString();

                txtsung1.Text = docter[0].ToString();
                txtsung2.Text = docter[1].ToString();
                txtsung3.Text = docter[2].ToString();
                txtsung4.Text = docter[3].ToString();
                txtsung5.Text = docter[4].ToString();
                txtsung6.Text = docter[5].ToString();
                txthoki.Text = hoki.ToString();

                if (noword - sqlord != 1)
                {
                    DangInsertButton.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DangInsertButton_Click(object sender, EventArgs e)
        {
            string date = "'" + PopDateTextBox.Text + "'";
            string sub = $"{DangOrderTextBox.Text}, {txtdang1.Text}, {txtdang2.Text}, {txtdang3.Text}, " +
                $"{txtdang4.Text}, {txtdang5.Text}, {txtdang6.Text}, {txtbonus.Text}, {txtsung1.Text}, " +
                $"{txtsung2.Text}, {txtsung3.Text}, {txtsung4.Text}, {txtsung5.Text}, {txtsung6.Text}, " +
                $"{txthoki.Text}, {date} ";

            string query = "INSERT INTO BasicTbl VALUES(" + sub + ")";

            try
            {
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                int row = command.ExecuteNonQuery();
                if (row > 0)
                {
                    DangInsertButton.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void ForeignParsingButton_Click(object sender, EventArgs e)
        {
            int basicord;
            int foregord;

            using (var db = new LottoDBContext())
            {
                basicord = db.BasicTbl.Max(x => x.Orders);
                foregord = db.ForeignTbl.Max(x => x.Orders);
            }
            var aust = await Task.Run(LinkAustraliaLotto);
            var cant = await Task.Run(LinkCanadaLotto);

            if (!(aust.Any() && cant.Any()))
            {
                throw new Exception("해외당번 파싱오류.");
            }

            try
            {
                int order = (basicord == foregord) ? basicord : foregord + 1;
                ForeignOrderTextBox.Text = order.ToString();
                txtaus1.Text = aust[0].ToString();
                txtaus2.Text = aust[1].ToString();
                txtaus3.Text = aust[2].ToString();
                txtaus4.Text = aust[3].ToString();
                txtaus5.Text = aust[4].ToString();
                txtaus6.Text = aust[5].ToString();
                txtaus7.Text = aust[6].ToString();
                txtaus8.Text = aust[7].ToString();

                txtcan1.Text = cant[0].ToString();
                txtcan2.Text = cant[1].ToString();
                txtcan3.Text = cant[2].ToString();
                txtcan4.Text = cant[3].ToString();
                txtcan5.Text = cant[4].ToString();
                txtcan6.Text = cant[5].ToString();
                txtcan7.Text = cant[6].ToString();

                if (basicord != foregord)
                {
                    ForeignInsertButton.Enabled = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ForeignInsertButton_Click(object sender, EventArgs e)
        {
            string sub = $"{ ForeignOrderTextBox.Text }, { txtaus1.Text }, { txtaus2.Text }, { txtaus3.Text }, " +
                $"{ txtaus4.Text }, { txtaus5.Text }, { txtaus6.Text }, { txtaus7.Text }, { txtaus8.Text }, " +
                $"{ txtcan1.Text }, { txtcan2.Text }, { txtcan3.Text }, { txtcan4.Text }, { txtcan5.Text }, " +
                $"{ txtcan6.Text }, { txtcan7.Text }";

            string query = "INSERT INTO ForeignTbl VALUES(" + sub + ")";

            try
            {
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                int row = command.ExecuteNonQuery();

                if (row > 0)
                {
                    RemindInsertButton.Enabled = true;
                    ForeignInsertButton.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemindInsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                int basicord;
                int foregord;
                int fixedord;

                using (var db = new LottoDBContext())
                {
                    basicord = db.BasicTbl.Max(x => x.Orders);
                    foregord = db.ForeignTbl.Max(x => x.Orders);
                    fixedord = db.FixChulsuTbl.Max(x => x.Orders);
                }

                if ((basicord == foregord) && (foregord != fixedord) )
                {
                    InsertFixChulsuTbl(basicord);
                    InsertNonChulsuTbl(basicord);
                    InsertChulsuTbl(basicord);
                    InsertTypeTbl(basicord);
                    InsertSingleTbl(basicord);
                    InsertAppearTbl(basicord);
                    InsertGridTbl(basicord);
                    InsertHorflowTbl(basicord);
                    InsertVerflowTbl(basicord);
                    InsertInnerBoxTbl(basicord);
                    InsertPolygonTbl(basicord);
                    UpdateShowOrder(basicord);

                    MessageBox.Show("데이터 삽입 완료.");
                }
                else if (basicord == foregord && foregord == fixedord)
                {
                    return;
                }
                else
                {
                    throw new Exception("나머지 데이터 입력 사전작업 오류.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 데이터 삽입,수정 메서드

        /// <summary>
        /// FixChulsuTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order">회차</param>
        /// <param name="dang">정렬당번</param>
        private static async void InsertFixChulsuTbl(int order)
        {
            var gs = new Geomsa();
            var alls = await Task.Run(() => gs.FixChulsuDatas());
            var addstr = new List<string>();
            var dang = Utility.DangbeonOfOrder(order);

            foreach (var list in alls.Values)
            {
                string quato = "'";
                var counts = list.Select(x => x.Intersect(dang).Count());
                string s1 = quato + string.Join("", counts) + quato;
                addstr.Add(s1);
            }

            string sub = string.Join(",", addstr);

            string pos7 = Quato + string.Join("",
                          dang.Select(x => LuckyLibrary.PositionOfData(x, 7)).Select(g => "c" + g.xIndex + "r" + g.yIndex)) + Quato;

            string query = $"INSERT INTO FixChulsuTbl VALUES({order}, {sub}, {pos7})";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// NonChulsuTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order">회차</param>
        /// <param name="dang">정렬당번</param>
        private static async void InsertNonChulsuTbl(int order)
        {
            var gs = new Geomsa();
            string quato = "'";
            var dang = Utility.DangbeonOfOrder(order);
            var sung = Utility.SunDangOfOrder(order);
            var alls = await Task.Run(gs.NonChulsuDatas);
            var addstr = new List<string>();
            var before = Utility.DangbeonOfOrder(order - 1);
            var ninedata = LuckyLibrary.NainDatas();
            var donggapdata = gs.SameIntervalData(order);
            var yeonsokdata = gs.ContinueIndexData(order);
            var yeonkkeutdata = gs.SameEndsData(order);

            foreach (var list in alls.Values)
            {
                var counts = list.Select(x => x.Intersect(dang).Count());
                string s1 = quato + string.Join("", counts) + quato;
                addstr.Add(s1);
            }

            string sub = string.Join(",", addstr);

            //dgap
            string dgap = quato + string.Join("", gs.GapchulInts(dang)) + quato;

            //이월수
            string ihweol = quato + string.Join(",", before.Intersect(dang)) + quato;

            //동간격
            string donggap = quato + string.Join(",", donggapdata) + quato;

            //연속수
            string yeonsok = quato + string.Join(",", yeonsokdata.Select(x => x.num1 + "-" + x.num2)) + quato;

            //연끝수
            string yeonkkeut = quato + string.Join(",", yeonkkeutdata.Select(x => string.Join("-", x))) + quato;

            sub += "," + dgap + "," + ihweol + "," + donggap + "," + yeonsok + "," + yeonkkeut;

            string query = $"INSERT INTO NonChulsuTbl VALUES({order}, {sub})";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// ChulsuTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static async void InsertChulsuTbl(int order)
        {
            var gs = new Geomsa();
            var sb = new StringBuilder();
            sb.Append(order + ",");

            var dang = Utility.DangbeonOfOrder(order);
            var sung = Utility.SunDangOfOrder(order);

            var danhap = gs.IndexValueOfSums(dang);
            var sunhap = gs.IndexValueOfSums(sung);
            sb.Append(string.Join(",", danhap.Values) + ",");
            sb.Append(string.Join(",", sunhap.Values) + ",");

            int hapgey = dang.Sum();
            sb.Append(hapgey + ",");
            var (front, back, sum) = gs.FrontAndBackOfSum(dang);
            int ahp = front;
            int dwi = back;
            int apdwi = sum;
            sb.Append(ahp + "," + dwi + "," + apdwi + ",");
            int kocha = dang.Max() - dang.Min();
            int kohap = dang.Max() + dang.Min();
            sb.Append(kocha + "," + kohap + ",");
            var dgapcha = dang.Zip(dang.Skip(1), (a, b) => Math.Abs(a - b));
            sb.Append(string.Join(",", dgapcha) + ",");
            var sgapcha = sung.Zip(sung.Skip(1), (a, b) => Math.Abs(a - b));
            sb.Append(string.Join(",", sgapcha) + ",");
            var dgaphap = dang.Zip(dang.Skip(1), (a, b) => a + b);
            sb.Append(string.Join(",", dgaphap) + ",");
            var sgaphap = sung.Zip(sung.Skip(1), (a, b) => a + b);
            sb.Append(string.Join(",", sgaphap) + ",");
            var dkkeut = dang.Select(x => x % 10);
            sb.Append(string.Join(",", dkkeut) + ",");
            var skkeut = sung.Select(x => x % 10);
            sb.Append(string.Join(",", skkeut) + ",");
            var beisus = LuckyLibrary.BaesuDatas().Select(x => x.Intersect(dang).Count());
            sb.Append(string.Join(",", beisus) + ",");
            int donghyun = LuckyLibrary.DonghyeongInts().Intersect(dang).Count();
            sb.Append(donghyun + ",");
            var acs = gs.AcValueData(sung);
            sb.Append(string.Join(",", acs) + ",");
            int jekob = LuckyLibrary.JekobsuInts().Intersect(dang).Count();
            int ihang = LuckyLibrary.IhangInts().Intersect(dang).Count();
            int pivo = LuckyLibrary.PivonachInts().Intersect(dang).Count();
            int ypivo = LuckyLibrary.YeokPivonachInts().Intersect(dang).Count();
            sb.Append(jekob + "," + ihang + "," + pivo + "," + ypivo + ",");

            //이전과 비교 계산
            var befores = await Task.Run(() => gs.BeforeCompareDatas(order));

            var befdata = befores.Values.Select(x => x.Intersect(dang).Count());
            sb.Append(string.Join(",", befdata) + ",");

            //방향
            var dway = Utility.DirectionIndexInts(dang);
            var sway = Utility.DirectionIndexInts(sung);

            sb.Append(string.Join(",", dway) + ",");
            sb.Append(string.Join(",", sway) + ",");

            var updws = LuckyLibrary.UpDownJohapLists();
            var ups = updws.Select(x => x.Select(y => y.Intersect(dang).Count() == 2 ? 1 : 0).Sum());

            sb.Append(string.Join(",", ups));
            string sbs = sb.ToString();

            string query = "INSERT INTO ChulsuTbl VALUES (" + sbs + ")";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// TypeTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static async void InsertTypeTbl(int order)
        {
            var gs = new Geomsa();

            //컬럼이름
            var columns = new List<string>();
            var getcols = typeof(TypeTbl).GetProperties().Select(x => x.Name).Skip(1);

            foreach (string item in getcols)
            {
                //맨 앞의 T 글자를 삭제
                string s = item.Remove(0, 1);
                columns.Add(s);
            }
            
            var dang = Utility.DangbeonOfOrder(order);

            //타입인덱스
            var types = new List<int>();
            var datas = await Task.Run(() => gs.FixChulsuDatas());
            foreach (var col in columns)
            {
                var data = datas[col];
                var chul = data.Select(x => dang.Intersect(x).Count());
                var type = Utility.ConvertTypeIndex(chul);
                types.Add(type);
            }

            string sub = string.Join(",", types);
            string query = $"INSERT INTO TypeTbl VALUES ({order}, {sub})";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// SingleTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static void InsertSingleTbl(int order)
        {
            var dang = Utility.DangbeonOfOrder(order);

            int[] cols = { 5, 7, 9 };
            var lists = new List<float>();

            foreach (var col in cols)
            {
                var data = LuckyLibrary.HorizontalFlowDatas(col);
                var points = dang.Select(v => Convexhull.IndexToPoint(v, data)).ToList();
                var hulls = Convexhull.GetConvexHull(points);
                hulls.Add(hulls.First());

                var angles = Convexhull.CalculateAngle(hulls);
                float angsum = angles.Count(x => x != 0); //갯수처리 하지 않으면 0 - 540 가 되기 때문에
                var lines = Convexhull.DistancePoints(hulls);
                float linsum = (float)Math.Round(lines.Sum(), 4);

                var pos = Convexhull.DangbeonPosition(dang, data);
                var kakdo = Convexhull.AngleBetweens(pos);
                var area = Convexhull.Area(hulls);

                lists.AddRange(angles);
                lists.Add(angsum);
                lists.AddRange(lines);
                lists.Add(linsum);
                lists.AddRange(kakdo);
                lists.Add(area);
            }

            var avg = (float)Math.Round(dang.Average(), 4);
            lists.Add(avg);

            string query = $"INSERT INTO SingleTbl VALUES ({order}, {string.Join(", ", lists)})";;

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// AppearTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static void InsertAppearTbl(int order)
        {
            var dang = Utility.DangbeonOfOrder(order);

            var lst = LastAppearData(order);
            int[] array = new int[45];

            for (int j = 0; j < 45; j++)
            {
                int n = lst[j];

                if (dang.Contains(j + 1))
                {
                    array[j] = 0;
                }
                else
                {
                    array[j] = n + 1;
                }
            }

            string sub = string.Join(",", array);
            string query = $"INSERT INTO AppearTbl VALUES ({order}, {sub})";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// GridTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static void InsertGridTbl(int order)
        {
            var dang = Utility.DangbeonOfOrder(order);

            //사용할 데이터 불러오기
            int[] idxs = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 15 };
            var pairs = new Dictionary<string, List<int[]>>();

            foreach (int idx in idxs)
            {
                string col = "C" + idx.ToString("00");
                string path = Application.StartupPath + @"DataFiles\col" + idx.ToString("00") + ".csv";
                var lines = File.ReadAllLines(path);

                var datas = new List<int[]>();
                foreach (string line in lines)
                {
                    var data = line.Split(',').Select(x => int.Parse(x)).ToArray();
                    datas.Add(data);
                }

                pairs.Add(col, datas);
            }

            var strs = new List<string>();

            foreach (var key in pairs.Keys)
            {
                var vals = pairs[key];
                var dubs = vals.Select(x => x.Intersect(dang).Count());
                string s1 = "'" + string.Join("", dubs) + "'";
                strs.Add(s1);
            }

            var sub = order + "," + string.Join(",", strs);

            string query = "INSERT INTO GridTbl VALUES (" + sub + ")";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// HorflowTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static void InsertHorflowTbl(int order)
        {
            var dang = Utility.DangbeonOfOrder(order);
            var sequence = Enumerable.Range(1, 45).ToArray();
            string[] head = { "NM", "LS", "RS", "LN", "RN", "LZ", "RZ" };
            int[] cols = { 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15 };
            var pairs = new Dictionary<string, List<int[]>>();

            foreach (var col in cols)
            {
                foreach (GridDirection direction in Enum.GetValues(typeof(GridDirection)))
                {
                    int idx = (int)direction;
                    var list = LuckyLibrary.HorizontalFlowInts(sequence, col, direction);
                    string name = head[idx] + col.ToString("00");

                    pairs.Add(name, list);
                }
            }

            var eachs = new List<string>();

            foreach (string key in pairs.Keys)
            {
                var vals = pairs[key].Select(x => x.Intersect(dang).Count());
                string s1 = "'" + string.Join("", vals) + "'";
                eachs.Add(s1);
            }

            string sub = order + "," + string.Join(",", eachs);
            string query = "INSERT INTO HorflowTbl VALUES (" + sub + ")";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// VerflowTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static void InsertVerflowTbl(int order)
        {
            var dang = Utility.DangbeonOfOrder(order);
            var sequence = Enumerable.Range(1, 45).ToArray();
            string[] head = { "NM", "LS", "RS", "LN", "RN", "LZ", "RZ" };
            int[] cols = { 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15 };
            var pairs = new Dictionary<string, List<int[]>>();

            foreach (var col in cols)
            {
                foreach (GridDirection direction in Enum.GetValues(typeof(GridDirection)))
                {
                    int idx = (int)direction;
                    var list = LuckyLibrary.VerticalFlowInts(sequence, col, direction);
                    string name = head[idx] + col.ToString("00");

                    pairs.Add(name, list);
                }
            }

            var eachs = new List<string>();

            foreach (string key in pairs.Keys)
            {
                var vals = pairs[key].Select(x => x.Intersect(dang).Count());
                string s1 = "'" + string.Join("", vals) + "'";
                eachs.Add(s1);
            }

            string sub = order + "," + string.Join(",", eachs);
            string query = "INSERT INTO VerflowTbl VALUES (" + sub + ")";

            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
            //using var conn = new SqlConnection(Connection);
            //var cmd = new SqlCommand(query, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// InnerBoxTbl 에 데이터 삽입
        /// </summary>
        /// <param name="basicord"></param>
        private static void InsertInnerBoxTbl(int basicord)
        {
            string qo = "'";

            var hib = new InnerBoxDatas(LuckyLibrary.HorizontalFlowDatas(7));
            var vib = new InnerBoxDatas(LuckyLibrary.VerticalFlowDatas(7));
            var horpairs = hib.BoxDataPairs();
            var verpairs = vib.BoxDataPairs();

            var dang = Utility.DangbeonOfOrder(basicord);
            var hs = new List<string>();
            foreach (var key in horpairs.Keys)
            {
                var vals = horpairs[key];
                var cnts = vals.Select(x => x.Intersect(dang).Count());
                string s = qo + string.Join("", cnts) + qo;
                hs.Add(s);
            }

            var vs = new List<string>();
            foreach (var key in verpairs.Keys)
            {
                var vals = verpairs[key];
                var cnts = vals.Select(x => x.Intersect(dang).Count());
                string s = qo + string.Join("", cnts) + qo;
                vs.Add(s);
            }

            string sub = string.Join(", ", hs) + ", " + string.Join(", ", vs);

            string query = $"INSERT INTO InnerBoxTbl VALUES ({basicord}, {sub})";
            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// PolygonTbl 에 데이터 삽입
        /// </summary>
        /// <param name="order"></param>
        private static void InsertPolygonTbl(int order)
        {
            int[] rowCounts = { 5, 7, 9 };
            string[] heads = { "H", "V" };
            string qt = "'";

            List<string> lines = new();
            var dangs = Utility.DangbeonOfOrder(order);

            foreach (int rowCount in rowCounts)
            {
                foreach (string head in heads)
                {
                    foreach (int gap in LuckyLibrary.SeqenceGapInts)
                    {
                        int[] sequence = LuckyLibrary.NewSequenceLists()[gap];
                        List<int[]> datas = (head.Equals("H")) ? LuckyLibrary.HorizontalFlowInts(sequence, rowCount) :
                                                                 LuckyLibrary.VerticalFlowInts(sequence, rowCount);

                        var (dangPos, polyPos) = PolygonToDotString(datas, dangs);
                        string pos = qt + dangPos + "/" + polyPos + qt;
                        lines.Add(pos);
                    }
                }
            }

            string sub = order + "," + string.Join(",", lines);
            string query = $"INSERT INTO PolygonTbl VALUES ( {sub} )";
            using var context = new LottoDBContext();
            using var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = query;
            context.Database.OpenConnection();
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// ShowOrderTbl 에 데이터 변경
        /// </summary>
        /// <param name="order">회차</param>
        private static void UpdateShowOrder(int order)
        {
            var data = Utility.DangbeonOfOrder(order);

            foreach (int i in data)
            {
                string num = "C" + i;
                int two;
                string query = "SELECT TOP(1) Cnt FROM ShowOrderTbl WHERE " + num + " IS NOT NULL ORDER BY Cnt DESC";
                using (var context = new LottoDBContext())
                {
                    using var command = context.Database.GetDbConnection().CreateCommand();
                    command.CommandText = query;
                    context.Database.OpenConnection();
                    two = (int)command.ExecuteScalar();
                }


                //가져 최종위치 + 1 에 기록
                using var contxt = new LottoDBContext();
                string upqu = $"UPDATE ShowOrderTbl SET {num}={order} WHERE Cnt={two + 1}";// + num + "=" + order + " WHERE cnt = " + (two + 1);
                using var cmd = contxt.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = upqu;
                contxt.Database.OpenConnection();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        /// <summary>
        /// 마이고 홈페이에서 회차, 호기, 당번(보너스포함) 추출
        /// </summary>
        /// <returns>튜플(회차, 호기, 정렬당번 배열(보너스포함))</returns>
        private (int order, int hoki, int[] dangInts) LinkMygoHomepage()
        {
            var tuple = (-1, -1, new[] { -1, -1, -1, -1, -1, -1, -1 });

            try
            {
                var uri = @"http://lotto.mygo.co.kr/";
                var web = new HtmlWeb()
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.Default
                };

                var doc = web.Load(uri);

                int[] dang = Array.Empty<int>();

                //회차
                string ordText = doc.DocumentNode.SelectSingleNode("//div [@id = 'mygoCount']").InnerText;

                if (!int.TryParse(ordText, out int ord))
                {
                    throw new Exception("회차추출 오류.");
                };

                //보너스번호
                string bonusText = doc.DocumentNode.SelectSingleNode("//div [@id = 'mygoBoNum']/img").OuterHtml;

                //문장에서 마직막 슬래쉬 위치와 마지막 포인드 위차 찾기
                int slash = bonusText.LastIndexOf('/') + 1;
                int point = bonusText.LastIndexOf('.');
                var bonus = bonusText[slash..point];

                if (!int.TryParse(bonus, out int bns))
                {
                    throw new Exception("보너스추출 오류.");
                }

                //호기
                string hokiText = doc.DocumentNode.SelectSingleNode("//div [@id = 'mygoWinInfo']").InnerText.Trim();
                var hokis = hokiText.Substring(hokiText.LastIndexOf('/') + 1, 1);//.Replace("호기", "");

                if (!int.TryParse(hokis, out int hok))
                {
                    throw new Exception("호기추출 오류.");
                }

                //당번
                var dangdata = doc.DocumentNode.SelectNodes("//div [@id = 'mygoWinNum']/img");
                var tmp = new List<int>();

                foreach (var node in dangdata)
                {
                    var each = node.Attributes["src"].Value.Trim();
                    int btidx = each.LastIndexOf('/') + 1;
                    int enidx = each.LastIndexOf('.');

                    string sub = each[btidx..enidx];
                    bool pass = int.TryParse(sub, out var num);

                    if (pass)
                    {
                        tmp.Add(num);
                    }
                }

                tmp.Add(bns);

                if (tmp.Count != 7)
                {
                    throw new Exception("당번추출 오류");
                }

                tuple = (ord, hok, tmp.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return tuple;
        }

        /// <summary>
        /// 로또박사 홈페이지에서 공나온 순서 추출
        /// </summary>
        /// <returns>순서당번 배열</returns>
        private int[] LinklottodrHomepage()
        {
            var list = new List<int>();

            var uri = @"http://lottodr.kr/";
            var web = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.Default
            };

            var doc = web.Load(uri);
            var data = doc.DocumentNode.SelectNodes("//div [@class='numberBox_2017_smallBall']/img");

            foreach (var node in data)
            {
                var each = node.Attributes["src"].Value.Trim();
                int btidx = each.LastIndexOf('_') + 1;
                int enidx = each.LastIndexOf('.');

                string sub = each[btidx..enidx];
                bool pass = int.TryParse(sub, out var num);

                if (pass)
                {
                    list.Add(num);
                }
            }

            //공나온순서(보너스 포함) + 전회모의추첨번호(보너스포함) = 14
            if (list.Count != 14)
            {
                return new int[] { -1, -1, -1, -1, -1, -1 };
            }
            else
            {
                return list.Take(6).ToArray();
            }
        }

        /// <summary>
        /// 호주로또 당번 (보너스포함 8개)
        /// </summary>
        /// <returns>당번리스트</returns>
        public static List<int> LinkAustraliaLotto()
        {
            List<int> rst;

            try
            {
                string url = "https://www.ozlotteries.com/saturday-lotto/results";
                HtmlWeb web = new();
                var htmlDocument = web.Load(url);
                var mainlist = new List<int>();
                var bonslist = new List<int>();

                //최종 찾고자 하는 하위요소에서 우클릭 outerhtml 요소 복사
                var drawsnodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='results-number-set__number--ns1 css-n11vpb eik1jin0']");
                var bonusnodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='results-number-set__number--ns2 css-n11vpb eik1jin0']");

                if (drawsnodes == null || !drawsnodes.Any())
                {
                    throw new Exception("호주당번 추출 실패.");
                }
                else
                {
                    foreach (var node in drawsnodes)
                    {
                        var find = node.InnerText;

                        mainlist.Add(int.Parse(find));

                        if (mainlist.Count >= 6)
                        {
                            break;
                        }
                    }
                }

                if (bonusnodes == null || !bonusnodes.Any())
                {
                    throw new Exception("호주당번 추출 실패.");
                }
                else
                {
                    foreach (var node in bonusnodes)
                    {
                        var find = node.InnerText;

                        bonslist.Add(int.Parse(find));

                        if (bonslist.Count >= 2)
                        {
                            break;
                        }
                    }
                }

                if (mainlist.Count != 6 || bonslist.Count != 2)
                {
                    throw new Exception("호주당번 추출 실패.");
                }

                mainlist.Sort();
                bonslist.Sort();

                rst = new List<int>(mainlist);
                rst.AddRange(bonslist);
            }
            catch (Exception)
            {
                throw;
            }

            return rst;
        }

        /// <summary>
        /// 캐나다로또 당번 (보너스포함 7개)
        /// </summary>
        /// <returns></returns>
        private List<int> LinkCanadaLotto()
        {
            List<int> nums = new();

            try
            {
                string url = "https://www.lotterycritic.com/lottery-results/canada-lottery/ontario/";
                HtmlWeb web = new();
                var htmlDocument = web.Load(url);

                var regularnodes = htmlDocument.DocumentNode.SelectNodes("//div/dd[@class='noStyle lotteryResultsTable-ball']");
                var imsi = new List<string>();

                foreach (var node in regularnodes)
                {
                    imsi.Add(node.InnerText.Trim());
                }

                var temp = imsi.Skip(15).Take(6).ToList();
                nums = temp.Select(x => int.Parse(x)).ToList();

                var bonusnodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='lotteryResultsTable-bonuses']");

                var bons = new List<int>();

                foreach (var node in bonusnodes)
                {
                    string s = node.InnerText.Trim();
                    string s1 = s.Replace("Bonus", "");

                    int n = int.Parse(s1);
                    bons.Add(n);
                }

                int bonus = bons[2];
                nums.Add(bonus);

                if (nums.Count != 7)
                {
                    throw new Exception("캐나다 당번추출 실패.");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nums;
        }

        /// <summary>
        /// AppearTbl 이전 데이터를 반환
        /// </summary>
        /// <param name="order">회차 (내부적으로 - 1을 함)</param>
        /// <returns>정수배열</returns>
        private static List<int> LastAppearData(int order)
        {
            var list = new List<int>();
            string s1 = "SELECT * FROM AppearTbl WHERE Orders = " + (order - 1);
            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = s1;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    int n = reader.GetInt32(i);
                    list.Add(n);
                }
            }
            return list;
        }

        /// <summary>
        /// 폴리곤 픽셀데이터
        /// </summary>
        /// <param name="datas">행열데이터</param>
        /// <param name="dangs">당번배열</param>
        /// <returns>튜플(당번위치 문자열, 폴리곤 픽셀문자열) (앞뒤구분자'/' 행구분자',') </returns>
        private static (string dangPos, string polyPos) PolygonToDotString(List<int[]> datas, int[] dangs)
        {
            int row = datas.Count;
            int col = datas.Max(x => x.Length);
            var dangPoints = Convexhull.DangbeonPosition(dangs, datas);

            var size = new Size(40, 40);
            int xg = 20;
            int yg = 20;

            //당번 위치
            var pos = dangs.Select(x => Convexhull.IndexToPoint(x, datas)).ToList();

            //외곽선 위치
            var hull = Convexhull.GetConvexHull(pos);
            hull.Add(hull.First());

            //외곽선 그림판 위치
            var hullPoints = hull.Select(g => new Point(g.Y * 40 + 40, g.X * 40 + 40)).ToList();

            //외곽선 영역
            GraphicsPath graphicsPath = new();
            graphicsPath.AddLines(hullPoints.ToArray());
            Region region = new(graphicsPath);

            List<string> danglist = new();
            List<string> pixelist = new();
            for (int y = 0; y < row; y++)
            {
                string dangmun = string.Empty;
                string pixelmun = string.Empty;

                for (int x = 0; x < col; x++)
                {
                    var pt = new Point(x * size.Width + xg, y * size.Height + yg);
                    var rect = new Rectangle(pt, size);
                    string danch, pixch;
                    if (dangPoints.Any(g => g.X == x && g.Y == y))
                        danch = "1";
                    else
                        danch = "0";
                    dangmun += danch;

                    if (region.IsVisible(rect))
                        pixch = "1";
                    else
                        pixch = "0";
                    pixelmun += pixch;
                }
                danglist.Add(dangmun);
                pixelist.Add(pixelmun);
            }

            return (string.Join(",", danglist), string.Join(",", pixelist));
        }
    }
}
