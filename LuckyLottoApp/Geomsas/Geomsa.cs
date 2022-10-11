using System.Collections.Concurrent;
using System.Globalization;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Geomsas
{
    /// <summary>
    /// 검사에 사용할 전체데이터 클래스
    /// </summary>
    internal class Geomsa
    {
        #region 필드
        private readonly int _lastOrder;
        private readonly int _hangey;
        #endregion
        #region 속성
        /// <summary>
        /// 회차의 출수 딕셔너리(키:회차, 값:출수배열리스트)
        /// </summary>
        public Dictionary<int, List<int[]>> ChulsuPairs { get; set; }
        /// <summary>
        /// 회차의 출수타입 딕셔너리(키:회차, 값:타입번호)
        /// </summary>
        public Dictionary<int, int[]> TypePairs { get; set; }
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public Geomsa() 
        { 
            int count = Utility.GetLastOrder();
            _lastOrder = count;
            _hangey = Convert.ToInt32(count * 0.01);
        }

        /// <summary>
        /// ChulsuPairs, TypePairs 속성에 값을 대입하기
        /// </summary>
        public void GetDataOfFixSql()
        {
            var fixcols = CalculateOfFixData().Select(x => x.Item1).ToList();
            fixcols.Insert(0, "Orders");

            var allpairs = new Dictionary<int, (List<int[]>, int[])>();
            string sql = "SELECT " + string.Join(",", fixcols) + " FROM FixChulsuTbl";
            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var rows = new List<int[]>();
                var typs = new List<int>();

                for (int i = 1; i < reader.FieldCount; i++)
                {
                    try
                    {
                        string s1 = reader.GetString(i).Trim();
                        int[] row = s1.Select(x => int.Parse(x.ToString())).ToArray();
                        int type = Utility.ConvertTypeIndex(row);
                        rows.Add(row);
                        typs.Add(type);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                allpairs.Add(reader.GetInt32(0), (rows, typs.ToArray()));
            }

            ChulsuPairs = allpairs.ToDictionary(x => x.Key, x => x.Value.Item1);
            TypePairs = allpairs.ToDictionary(x => x.Key, x => x.Value.Item2);
        }

        /// <summary>
        /// 검사조건 파일을 작성 (가끔씩 파일 작성할것)
        /// </summary>
        public void WriteCombine()
        {
            Parallel.Invoke
            (
                () => WriteTypesu(),
                () => WriteChulsu(),
                () => WriteChulsuSum()
            );
        }




        /// <summary>
        /// 정규가로 전체 해당번호 (RH03 - RH15)
        /// </summary>
        /// <returns>딕셔너리 (키:항목이름, 값:정규가로 해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> RegularHoriziontalDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            for (int i = 3; i <= 15; i++)
            {
                string s = "RH" + i.ToString("00");
                var data = LuckyLibrary.HorizontalFlowDatas(i);
                dic.Add(s, data);
            }

            return dic;
        }

        /// <summary>
        /// 사선가로 전체 해당번호 (DH03 - DH15)
        /// </summary>
        /// <returns>딕셔너리 (키:항목이름, 값:사선가로 해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> DiagonalHoriziontalDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            for (int i = 3; i <= 15; i++)
            {
                string s = "DH" + i.ToString("00");
                var data = LuckyLibrary.HorizontalFlowDatas(i, 1);
                dic.Add(s, data);
            }

            return dic;
        }

        /// <summary>
        /// 나선가로 전체 해당번호 (SH03 - SH15)
        /// </summary>
        /// <returns>딕셔너리 (키:항목이름, 값:나선가로 해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> SpiralHoriziontalDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            for (int i = 3; i <= 15; i++)
            {
                string s = "SH" + i.ToString("00");
                var data = LuckyLibrary.HorizontalFlowDatas(i, 2);
                dic.Add(s, data);
            }

            return dic;
        }

        /// <summary>
        /// 정규세로 전체 해당번호 (RV03 - RV15)
        /// </summary>
        /// <returns>딕셔너리 (키:항목이름, 값:정규세로 해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> RegularVerticalDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            for (int i = 3; i <= 15; i++)
            {
                string s = "RV" + i.ToString("00");
                var data = LuckyLibrary.VerticalFlowDatas(i);
                dic.Add(s, data);
            }

            return dic;
        }

        /// <summary>
        /// 사선세로 전체 해당번호 (DV03 - DV15)
        /// </summary>
        /// <returns>딕셔너리 (키:항목이름, 값:사선세로 해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> DiagonalVerticalDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            for (int i = 3; i <= 15; i++)
            {
                string s = "DV" + i.ToString("00");
                var data = LuckyLibrary.VerticalFlowDatas(i, 1);
                dic.Add(s, data);
            }

            return dic;
        }

        /// <summary>
        /// 나선세로 전체 해당번호 (SV03 - SV15)
        /// </summary>
        /// <returns>딕셔너리 (값:항목이름, 값:나선세로 해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> SpiralVerticalDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            for (int i = 3; i <= 15; i++)
            {
                string s = "SV" + i.ToString("00");
                var data = LuckyLibrary.VerticalFlowDatas(i, 2);
                dic.Add(s, data);
            }

            return dic;
        }

        /// <summary>
        /// FixChulsuTbl 항목 전체 해당번호
        /// </summary>
        /// <returns>딕셔너리(키:열이름, 값:해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> FixChulsuDatas()
        {
            var dic = new Dictionary<string, List<int[]>>(CombineFixDatas())
            {
                { "Lowhigh", LuckyLibrary.JeokosuDatas() },
                { "Oddeven", LuckyLibrary.HoljjackDatas() },
                { "Innout", LuckyLibrary.AnbakDatas() },
                { "Topleft", LuckyLibrary.SanghajwauDatas() },
                { "LDiagonal", LuckyLibrary.LeftsaseonDatas() },
                { "RDiagonal", LuckyLibrary.RightsaseonDatas() },
                { "Sosamhap", LuckyLibrary.SosamhapDatas() },
                { "Beondae", LuckyLibrary.BeondaeDatas() },
                { "Slipsu", LuckyLibrary.SlipDatas() },
                { "Kkeutbeon", LuckyLibrary.KkeutbeonDatas() }
            };

            return dic;
        }

        /// <summary>
        /// NonChulsuTbl 항목 전체 해당번호
        /// </summary>
        /// <returns>딕셔너리(키:열이름, 값:해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> NonChulsuDatas()
        {
            var dic = new Dictionary<string, List<int[]>>();

            foreach (var key in CombineNonDatas().Keys)
            {
                string s = "col" + key;
                var val = CombineNonDatas()[key];
                dic.Add(s, val);
            }

            dic.Add("Nine", LuckyLibrary.NainDatas());
            dic.Add("Nonthree", NonthreeDatas());
            dic.Add("Nonfive", NonfiveDatas());

            return dic;
        }

        /// <summary>
        /// 정규격자, 사선격자, 나선격자 전체 해당번호 묶음
        /// </summary>
        /// <returns>딕셔너리(키:항목이름, 값:해당번호배열 리스트)</returns>
        public Dictionary<string, List<int[]>> CombineFixDatas()
        {
            //딕셔너리 리스트 만들기
            var diclist = new List<Dictionary<string, List<int[]>>>
            {
                RegularHoriziontalDatas(),
                RegularVerticalDatas(),
                DiagonalHoriziontalDatas(),
                DiagonalVerticalDatas(),
                SpiralHoriziontalDatas(),
                SpiralVerticalDatas()
            };

            //전체 출수 데이터 묶기
            var dic = diclist.SelectMany(dict => dict)
                             .ToDictionary(pair => pair.Key, pair => pair.Value);

            return dic;
        }

        /// <summary>
        /// 정규격자, 사선격자, 나선격자 전체 해당번호에서 중복된것을 제외한 고유한 해당번호 
        /// </summary>
        /// <returns>딕셔너리(키:행갯수, 값:해당번호배열 리스트)</returns>
        public Dictionary<int, List<int[]>> CombineNonDatas()
        {
            int[] sequence = Enumerable.Range(1, 45).ToArray();
            var pairs = new Dictionary<int, List<int[]>>();
            var allist = new List<int[]>();

            for (int i = 15; i >= 3; i--)
            {
                foreach (GridDirection direct in Enum.GetValues(typeof(GridDirection)))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        var datas = (j == 0) ? LuckyLibrary.HorizontalFlowInts(sequence, i, direct) :
                                               LuckyLibrary.VerticalFlowInts(sequence, i, direct);

                        foreach (int[] data in datas)
                        {
                            var each = data.Where(x => x >= 1 && x <= 45).OrderBy(x => x).ToArray();

                            if (each.Length >= 3 && each.Length <= 15)
                            {
                                //중복검사
                                bool pass = IsPass(allist, each);

                                if (pass)
                                {
                                    allist.Add(each);
                                }
                            }
                        }
                    }
                }
            }

            for (int i = 3; i <= 15; i++)
            {
                var lst = allist.Where(x => x.Length == i).Select(x => x.ToArray()).ToList();

                if (lst.Any())
                {
                    pairs.Add(i, lst);
                }
            }

            return pairs;
        }

        /// <summary>
        /// 당번데이터 2-3조합 합계
        /// </summary>
        /// <param name="data">당번데이터</param>
        /// <returns>딕셔너리(키:조합인덱스 문자열, 값:조합 정수 합계값)</returns>
        public Dictionary<string, int> IndexValueOfSums(IEnumerable<int> data)
        {
            var dic = new Dictionary<string, int>();
            var list = data.ToList();

            var idxs = Utility.GetManySequences(Enumerable.Range(0, list.Count), 2, 3);

            foreach (var idx in idxs)
            {
                dic.Add(string.Join("", idx), idx.Select(x => list[x]).Sum());
            }

            return dic;
        }

        /// <summary>
        /// 이전당번과 계산에 사용하는 항목 해당번호 (핫왐콜3,해회당번2,삼각수4,정낙첨6,역낙첨6)
        /// </summary>
        /// <param name="order">검사회차</param>
        /// <returns>딕셔너리(키항목이름:, 값:해당번호 배열)</returns>
        public Dictionary<string, int[]> BeforeCompareDatas(int order)
        {
            var dic = new Dictionary<string, int[]>();
            var hotwam = HotWarmColdDatas(order).ToList();

            dic.Add("Hotsu", hotwam[0]);
            dic.Add("Warmsu", hotwam[1]);
            dic.Add("Coldsu", hotwam[2]);
            dic.Add("Daluksu", MonthData(order).ToArray());
            dic.Add("Ihutsu", AroundData(order).ToArray());
            dic.Add("Binsu", EmptyData(order).ToArray());

            var auscan = ForeignDatas(order);

            dic.Add("Autsu", auscan[0]);
            dic.Add("Cansu", auscan[1]);

            var samk = TriAngularDatas(order).ToList();

            dic.Add("Samkak1", samk[0]);
            dic.Add("Samkak2", samk[1]);
            dic.Add("Samkak3", samk[2]);
            dic.Add("Samkak4", samk[3]);

            var snak = NaksutDatas(order, 0).ToList();

            dic.Add("Snaksu1", snak[0]);
            dic.Add("Snaksu2", snak[1]);
            dic.Add("Snaksu3", snak[2]);
            dic.Add("Snaksu4", snak[3]);
            dic.Add("Snaksu5", snak[4]);
            dic.Add("Snaksu6", snak[5]);

            var rnak = NaksutDatas(order, 1).ToList();

            dic.Add("Rnaksu1", rnak[0]);
            dic.Add("Rnaksu2", rnak[1]);
            dic.Add("Rnaksu3", rnak[2]);
            dic.Add("Rnaksu4", rnak[3]);
            dic.Add("Rnaksu5", rnak[4]);
            dic.Add("Rnaksu6", rnak[5]);

            return dic;
        }




        /// <summary>
        /// 앞끝수 합, 뒤끝수 합, 앞뒤끝수 합
        /// </summary>
        /// <param name="datas">당번데이터</param>
        /// <returns>튜플(앞끝합, 뒤끝합, 앞뒤끝합)</returns>
        public (int front, int back, int sum) FrontAndBackOfSum(IEnumerable<int> data)
        {
            var rt = data.Select(x => ChangeOfNumber(x));
            int apt = rt.Sum(x => x.front);
            int dwi = rt.Sum(x => x.back);
            int sum = apt + dwi;

            return (apt, dwi, sum);
        }

        /// <summary>
        /// nonthree.csv 의 배열리스트
        /// </summary>
        /// <returns>요소개수 3의 해당번호 배열 리스트</returns>
        public List<int[]> NonthreeDatas()
        {
            var list = new List<int[]>();
            string path = Application.StartupPath + @"\DataFiles\nonthree.csv";

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    list.Add(LuckyLibrary.StringToList(line).ToArray());
                }
            }

            return list;
        }

        /// <summary>
        /// nonfive.csv 의 배열리스트
        /// </summary>
        /// <returns>요소개수 5의 해당번호 배열 리스트</returns>
        public List<int[]> NonfiveDatas()
        {
            var list = new List<int[]>();
            string path = Application.StartupPath + @"\DataFiles\nonfive.csv";

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    list.Add(LuckyLibrary.StringToList(line).ToArray());
                }
            }

            return list;
        }

        /// <summary>
        /// 호주당번과 캐나다당번 리스트 (내부적으로 -1 회차 데이터)
        /// </summary>
        /// <param name="order">현재회차</param>
        /// <returns>호주,캐나다당번 배열 리스트</returns>
        public List<int[]> ForeignDatas(int order)
        {
            if (order < 507)
            {
                return new List<int[]>() { Array.Empty<int>(), Array.Empty<int>() };
            }

            List<int> auts;

            using (var db = new LottoDBContext())
            {
                auts = db.ForeignTbl.Where(x => x.Orders == order - 1)
                    .Select(x => new List<int>
                    {
                        x.Aus1, x.Aus2, x.Aus3, x.Aus4,
                        x.Aus5, x.Aus6, x.Aus7, x.Aus8
                    }).Single();
            }

            List<int> cans;

            using (var db = new LottoDBContext())
            {
                cans = db.ForeignTbl.Where(x => x.Orders == order - 1)
                    .Select(x => new List<int>
                    {
                        x.Can1, x.Can2, x.Can3, x.Can4,
                        x.Can5, x.Can6, x.Can7
                    }).Single();
            }

            if (auts.Any() && cans.Any())
            {
                var rst = new List<int[]>
                {
                    auts.ToArray(),
                    cans.ToArray()
                };

                return rst;
            }
            else
            {
                return new List<int[]>() { Array.Empty<int>(), Array.Empty<int>() };
            }
        }

        /// <summary>
        /// 호주당번과 캐나다당번 리스트 (내부적으로 -1 회차 데이터)
        /// </summary>
        /// <returns>호주, 캐나다당번배열 리스트</returns>
        public List<int[]> ForeignDatas()
        {
            List<int> auts;

            using (var db = new LottoDBContext())
            {
                auts = db.ForeignTbl.Where(x => x.Orders == _lastOrder)
                    .Select(x => new List<int>
                    {
                        x.Aus1, x.Aus2, x.Aus3, x.Aus4,
                        x.Aus5, x.Aus6, x.Aus7, x.Aus8
                    }).Single();
            }

            List<int> cans;

            using (var db = new LottoDBContext())
            {
                cans = db.ForeignTbl.Where(x => x.Orders == _lastOrder)
                    .Select(x => new List<int>
                    {
                        x.Can1, x.Can2, x.Can3, x.Can4,
                        x.Can5, x.Can6, x.Can7
                    }).Single();
            }

            if (auts.Any() && cans.Any())
            {

                var rst = new List<int[]>
                {
                    auts.ToArray(),
                    cans.ToArray()
                };

                return rst;
            }
            else
            {
                return new List<int[]>() { Array.Empty<int>(), Array.Empty<int>() };
            }
        }




        /// <summary>
        /// 달력수 데이터(내부적으로 -1 회차 데이터)
        /// 달력수란 양력월일과 음력월일을 역순으로 문자열로 바꾼것을 순환돌면 더합값
        /// </summary>
        /// <param name="order">현재회차</param>
        /// <returns>달력수 해당번호 컬렉션</returns>
        public IEnumerable<int> MonthData(int order)
        {
            if (order < 10)
            {
                return Enumerable.Empty<int>();
            }

            var today = Utility.DateOfOrder(order - 1);
            KoreanLunisolarCalendar ksc = new();
            var sums = new List<int>();

            //양력 월
            int month = today.Month;

            //양력 일
            int day = today.Day;

            //양력 월 + 일 문자열
            string smd = month.ToString("00") + day.ToString("00");

            //음력 월
            int kmonth = ksc.GetMonth(today);

            //음력일
            int kday = ksc.GetDayOfMonth(today);

            //음력 월 + 일 문자열
            string skd = kmonth.ToString("00") + kday.ToString("00");

            //역순으로 바꾼 문자열
            string sear1 = ReverseString(smd);
            string sear2 = ReverseString(skd);
            var each = new List<string> { sear1, sear2 };

            foreach (var item in each)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i != j)
                        {
                            string tmp = item[i].ToString() + item[j].ToString();
                            int n = ZeroTo45(int.Parse(tmp));
                            sums.Add(n);
                        }
                    }
                }
            }

            var rst = sums.Distinct().OrderBy(x => x);
            return rst;
        }

        /// <summary>
        /// 달력수 데이터(내부적으로 -1 회차 데이터)
        /// 달력수란 양력월일과 음력월일을 역순으로 문자열로 바꾼것을 순환돌면 더합값
        /// </summary>
        /// <returns>달력수 해당번호 컬렉션</returns>
        public IEnumerable<int> MonthData()
        {
            var today = Utility.DateOfOrder(_lastOrder);
            KoreanLunisolarCalendar ksc = new();
            var sums = new List<int>();

            //양력 월
            int month = today.Month;

            //양력 일
            int day = today.Day;

            //양력 월 + 일 문자열
            string smd = month.ToString("00") + day.ToString("00");

            //음력 월
            int kmonth = ksc.GetMonth(today);

            //음력일
            int kday = ksc.GetDayOfMonth(today);

            //음력 월 + 일 문자열
            string skd = kmonth.ToString("00") + kday.ToString("00");

            //역순으로 바꾼 문자열
            string sear1 = ReverseString(smd);
            string sear2 = ReverseString(skd);
            var each = new List<string> { sear1, sear2 };

            foreach (var item in each)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i != j)
                        {
                            string tmp = item[i].ToString() + item[j].ToString();
                            int n = ZeroTo45(int.Parse(tmp));
                            sums.Add(n);
                        }
                    }
                }
            }

            var rst = sums.Distinct().OrderBy(x => x);
            return rst;
        }

        /// <summary>
        /// 이웃수 데이터 (내부적으로 -1 회차 데이터)
        /// 이웃수란 당번, 당번+1, 당번+3 한 리스트
        /// </summary>
        /// <param name="order">현재회차</param>
        /// <returns>이웃수 해당번호 컬렉션</returns>
        public IEnumerable<int> AroundData(int order)
        {
            if (order < 10)
            {
                return Enumerable.Empty<int>();
            }

            var datas = Utility.DangbeonOfOrder(order - 1);
            var lst = new List<int>();

            foreach (int i in datas)
            {
                lst.Add(ZeroTo45(i + 1));
                lst.Add(ZeroTo45(i + 2));
                lst.Add(ZeroTo45(i));
            }

            return lst.Distinct().OrderBy(x => x);
        }

        /// <summary>
        /// 이웃수 데이터(내부적으로 -1 회차 데이터)
        /// 이웃수란 당번, 당번+1, 당번+3 한 리스트
        /// </summary>
        /// <returns>이웃수 해당번호 컬렉션</returns>
        public IEnumerable<int> AroundData()
        {
            var datas = Utility.DangbeonOfOrder(_lastOrder);
            var lst = new List<int>();

            foreach (int i in datas)
            {
                lst.Add(ZeroTo45(i + 1));
                lst.Add(ZeroTo45(i + 2));
                lst.Add(ZeroTo45(i));
            }

            return lst.Distinct().OrderBy(x => x);
        }

        /// <summary>
        /// 빈수 데이터(내부적으로 -1 회차 데이터)
        /// 빈수란 번호의 끝수를 더한것을 45개 번호에서 제외한 리스트
        /// </summary>
        /// <param name="order">현재회차</param>
        /// <returns>빈수 해당번호 컬렉션</returns>
        public IEnumerable<int> EmptyData(int order)
        {
            if (order < 10)
            {
                return Enumerable.Empty<int>();
            }

            var data = Utility.DangbeonOfOrder(order - 1).ToList();
            var sequence = Enumerable.Range(1, 45);
            var lst = new List<int>();
            var kkeut = data.Select(x => x % 10).ToList();
            var expt = Enumerable.Range(0, 10).Except(kkeut).OrderBy(x => x).ToList();

            //빠진 끝수 리스트 중에서 중간값
            int middle = expt[expt.Count / 2];

            for (int i = 0; i < kkeut.Count; i++)
            {
                //끝수
                int n = kkeut[i] == 0 ? 10 : kkeut[i];

                for (int j = 0; j < 6; j++)
                {
                    int v = data[j];
                    if (i != j)
                    {
                        //끝수를 뺀것, 더한것, 자체번호 리스트에 저장
                        lst.Add(ZeroTo45(v + n));
                        lst.Add(ZeroTo45(v - n));
                        lst.Add(ZeroTo45(v + middle));
                        lst.Add(v);
                    }
                }
            }

            return sequence.Except(lst);
        }

        /// <summary>
        /// 빈수 데이터(내부적으로 -1 회차 데이터)
        /// 빈수란 번호의 끝수를 더한것을 45개 번호에서 제외한 리스트
        /// </summary>
        /// <returns>빈수 해당번호 컬렉션</returns>
        public IEnumerable<int> EmptyData()
        {
            var data = Utility.DangbeonOfOrder(_lastOrder).ToList();
            var sequence = Enumerable.Range(1, 45);
            var lst = new List<int>();
            var kkeut = data.Select(x => x % 10).ToList();
            var expt = Enumerable.Range(0, 10).Except(kkeut).OrderBy(x => x).ToList();

            //빠진 끝수 리스트 중에서 중간값
            int middle = expt[expt.Count / 2];

            for (int i = 0; i < kkeut.Count; i++)
            {
                //끝수
                int n = kkeut[i] == 0 ? 10 : kkeut[i];

                for (int j = 0; j < 6; j++)
                {
                    if (i != j)
                    {
                        //끝수를 뺀것, 더한것, 자체번호 리스트에 저장
                        lst.Add(ZeroTo45(data[j] + n));
                        lst.Add(ZeroTo45(data[j] - n));
                        lst.Add(ZeroTo45(data[j] + middle));
                        lst.Add(data[j]);
                    }
                }
            }

            var result = sequence.Except(lst);
            return result;
        }

        /// <summary>
        /// 1 - 44번 의 당번차이값 출현개수
        /// </summary>
        /// <param name="dang">당번데이터</param>
        /// <returns>1-44 번호의 중복수 컬렉션</returns>
        public IEnumerable<int> GapchulInts(IEnumerable<int> dang)
        {
            var johap = Utility.GetCombinations(dang, 2).Select(x => Math.Abs(x.First() - x.Last()));
            var sequence = Enumerable.Range(1, 44).Select(x => johap.Count(g => g == x));

            return sequence;
        }

        /// <summary>
        /// 삼각수 전체 컬렉션
        /// </summary>
        /// <param name="order">검사회차 (내부에서 -1로 진행)</param>
        /// <returns>해당번호 배열 컬렉션</returns>
        public IEnumerable<int[]> TriAngularDatas(int order)
        {
            if (order < 10)
            {
                for (int i = 0; i < 4; i++)
                {
                    yield return Array.Empty<int>();
                }
            }
            else
            {
                //samkak1 (번호 차)
                var datas = Utility.SunDangOfOrder(order - 1);
                var swap = datas.ToList();
                var result = new List<int>();

                while (true)
                {
                    if (swap.Count < 2)
                    {
                        break;
                    }

                    var zip = swap.Zip(swap.Skip(1), (a, b) => Math.Abs(a - b));
                    result.AddRange(zip);
                    swap = new List<int>(zip);
                }

                var sam1 = result.OrderBy(x => x).Distinct().ToArray();
                yield return sam1;

                //samkak2 (번호 합)
                var swap1 = datas.ToList();
                var result1 = new List<int>();

                while (true)
                {
                    if (swap1.Count < 2)
                    {
                        break;
                    }

                    var zip = swap1.Zip(swap1.Skip(1), (a, b) => ZeroTo45(a + b));
                    result1.AddRange(zip);
                    swap1 = new List<int>(zip);
                }

                var sam2 = result1.OrderBy(x => x).Distinct().ToArray();
                yield return sam2;

                //samkak3, samkak4
                var result3 = new List<int>();
                var result4 = new List<int>();

                var data = datas.ToArray();

                for (int i = 0; i < 6; i++)
                {
                    var each = new List<int>();
                    var each1 = new List<int>();

                    for (int j = 0; j < 6; j++)
                    {
                        if (i != j)
                        {
                            int n = Math.Abs(data[j] - data[i]);
                            int n1 = ZeroTo45(data[j] + data[i]);
                            if (n < 1 || n > 45)
                            {
                                throw new Exception("번호추출 오류.");
                            }
                            each.Add(n);
                            each1.Add(n1);
                        }
                    }

                    result3.AddRange(each);
                    result4.AddRange(each1);
                }

                var sam3 = result3.OrderBy(x => x).Distinct().ToArray();
                yield return sam3;

                var sam4 = result4.OrderBy(x => x).Distinct().ToArray();
                yield return sam4;
            }
        }

        /// <summary>
        /// 삼각수 전체 컬렉션
        /// </summary>
        /// <param name="order">검사회차 (내부에서 -1로 진행)</param>
        /// <returns>해당번호 배열 컬렉션</returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<int[]> TriAngularDatas()
        {
            //samkak1 (번호 차)
            var datas = Utility.SunDangOfOrder(_lastOrder - 1);
            var swap = datas.ToList();
            var result = new List<int>();

            while (true)
            {
                if (swap.Count < 2)
                {
                    break;
                }

                var zip = swap.Zip(swap.Skip(1), (a, b) => Math.Abs(a - b));
                result.AddRange(zip);
                swap = new List<int>(zip);
            }

            var sam1 = result.OrderBy(x => x).Distinct().ToArray();
            yield return sam1;

            //samkak2 (번호 합)
            var swap1 = datas.ToList();
            var result1 = new List<int>();

            while (true)
            {
                if (swap1.Count < 2)
                {
                    break;
                }

                var zip = swap1.Zip(swap1.Skip(1), (a, b) => ZeroTo45(a + b));
                result1.AddRange(zip);
                swap1 = new List<int>(zip);
            }

            var sam2 = result1.OrderBy(x => x).Distinct().ToArray();
            yield return sam2;

            //samkak3, samkak4
            var result3 = new List<int>();
            var result4 = new List<int>();

            var data = datas.ToArray();

            for (int i = 0; i < 6; i++)
            {
                var each = new List<int>();
                var each1 = new List<int>();

                for (int j = 0; j < 6; j++)
                {
                    if (i != j)
                    {
                        int n = Math.Abs(data[j] - data[i]);
                        int n1 = ZeroTo45(data[j] + data[i]);
                        if (n < 1 || n > 45)
                        {
                            throw new Exception("번호추출 오류.");
                        }
                        each.Add(n);
                        each1.Add(n1);
                    }
                }

                result3.AddRange(each);
                result4.AddRange(each1);
            }

            var sam3 = result3.OrderBy(x => x).Distinct().ToArray();
            yield return sam3;

            var sam4 = result4.OrderBy(x => x).Distinct().ToArray();
            yield return sam4;
        }

        /// <summary>
        /// FixChulsuTbl에 사용할 해당번호 컬렉션
        /// </summary>
        /// <returns>(항목이름, 해당번호배열 리스트) 컬렉션</returns>
        public IEnumerable<(string, List<int[]>)> CalculateOfFixData()
        {
            string[] headers = { "R", "D", "S" };
            string[] directs = { "H", "V" };
            int[] divids = Enumerable.Range(3, 15 - 3 + 1).ToArray();

            foreach (int divid in divids)
            {
                for (int i = 0; i < headers.Length; i++)
                {
                    string header = headers[i];
                    foreach (string direct in directs)
                    {
                        string column = $"{header}{direct}{divid:00}";
                        var datas = direct.Equals("H") ? LuckyLibrary.HorizontalFlowDatas(divid, i) :
                                                         LuckyLibrary.VerticalFlowDatas(divid, i);

                        yield return (column, datas);
                    }
                }
            }

            yield return ("Sosamhap", LuckyLibrary.SosamhapDatas());
            yield return ("Beondae", LuckyLibrary.BeondaeDatas());
            yield return ("Slipsu", LuckyLibrary.SlipDatas());
            yield return ("Kkeutbeon", LuckyLibrary.KkeutbeonDatas());
        }

        /// <summary>
        /// 핫수, 미지근수, 차가운수 컬렉션
        /// </summary>
        /// <param name="order">검사회차</param>
        /// <returns>해당번호 배열 컬렉션</returns>
        public IEnumerable<int[]> HotWarmColdDatas(int order)
        {
            //19회 미만은 빈 배열
            if (order < 10)
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return Array.Empty<int>();
                }
            }

            int kugan = 5;
            var sequence = Enumerable.Range(1, 45);
            var hot = SectionOfAllData(order - 1, kugan).OrderBy(x => x).ToArray();
            var warm = SectionOfAllData(order - 1, kugan * 2).Except(hot).OrderBy(x => x).ToArray();
            var cold = Utility.Except(sequence, warm, hot).Distinct().OrderBy(x => x).ToArray();

            yield return hot;
            yield return warm;
            yield return cold;
        }

        /// <summary>
        /// 핫수, 미지근수, 차가운수 컬렉션
        /// </summary>
        /// <param name="order">검사회차</param>
        /// <returns>해당번호 배열 컬렉션</returns>
        public IEnumerable<int[]> HotWarmColdDatas()
        {
            int kugan = 5;
            var sequence = Enumerable.Range(1, 45);
            var hot = SectionOfAllData(_lastOrder - 1, kugan).OrderBy(x => x).ToArray();
            var warm = SectionOfAllData(_lastOrder - 1, kugan * 2).Except(hot).OrderBy(x => x).ToArray();
            var cold = Utility.Except(sequence, warm, hot).Distinct().OrderBy(x => x).ToArray();

            yield return hot;
            yield return warm;
            yield return cold;
        }

        /// <summary>
        /// 낙수데이터의 열별 데이터 (내부적으로 -1 회차 데이터)
        /// </summary>
        /// <param name="order">현재회차</param>
        /// <param name="naksuIndex">0:정낙수 1:역낙수</param>
        /// <returns>낙수배열 리스트</returns>
        public IEnumerable<int[]> NaksutDatas(int order, int naksuIndex)
        {
            if (order < 49)
            {
                for (int i = 0; i < 6; i++)
                {
                    var arr = Array.Empty<int>();
                    yield return arr;
                }
            }
            else
            {
                List<int[]> data = naksuIndex switch
                {
                    0 => RegularMissing(order - 1).Values.ToList(),
                    1 => ReverseMissing(order - 1).Values.ToList(),
                    _ => throw new Exception("인덱스는 0 - 1 사이의 정수입니다.")
                };
                int rowCount = data.Count;
                int colCount = data.Select(x => x.Length).Max();

                //배열의 크기 루프
                for (int i = 0; i < colCount; i++)
                {
                    var each = new List<int>();

                    for (int j = 0; j < rowCount; j++)
                    {
                        if (i < data[j].Length)
                        {
                            each.Add(data[j][i]);
                        }
                    }

                    var arr = each.OrderBy(x => x).ToArray();
                    yield return arr;
                }
            }
        }

        /// <summary>
        /// 낙수데이터 컬렉션
        /// </summary>
        /// <param name="naksuIndex">0:정낙수 1:역낙수</param>
        /// <returns>낙수 해당번호 배열 컬렉션</returns>
        public IEnumerable<int[]> NaksutDatas(int naksuIndex)
        {
            List<int[]> data = naksuIndex switch
            {
                0 => RegularMissing(_lastOrder).Values.ToList(),
                1 => ReverseMissing(_lastOrder).Values.ToList(),
                _ => throw new Exception("인덱스는 0 - 1 사이의 정수입니다.")
            };

            int rowCount = data.Count;
            int colCount = data.Select(x => x.Length).Max();

            //배열의 크기 루프
            for (int i = 0; i < colCount; i++)
            {
                var each = new List<int>();

                for (int j = 0; j < rowCount; j++)
                {
                    if (i < data[j].Length)
                    {
                        each.Add(data[j][i]);
                    }
                }

                var arr = each.OrderBy(x => x).ToArray();
                yield return arr;
            }
        }

        /// <summary>
        /// 동간격 컬렉션
        /// </summary>
        /// <param name="order">회차</param>
        /// <returns>동간격 컬렉션</returns>
        public IEnumerable<int> SameIntervalData(int order)
        {
            var dang = Utility.DangbeonOfOrder(order);
            var zip = ZipSubtractData(dang);

            return zip.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).OrderBy(x => x);
        }

        /// <summary>
        /// 연속수 리스트
        /// </summary>
        /// <param name="order">회차</param>
        /// <returns>튜플(연속수 짝1, 짝2) 컬렉션</returns>
        public IEnumerable<(int num1, int num2)> ContinueIndexData(int order)
        {
            var dang = Utility.DangbeonOfOrder(order).ToList();
            var idxzip = ZipSubtractData(dang).Select((val, idx) => (val, idx))
                                              .Where(x => x.val == 1).Select(x => x.idx);

            if (idxzip.Any())
            {
                var list = new List<(int, int)>();
                foreach (var i in idxzip)
                {
                    var tuple = (dang[i], dang[i + 1]);
                    yield return tuple;
                }
            }
        }

        /// <summary>
        /// AC 값 컬렉션
        /// AC값은 삭각수를 모아놓은 갯수에서 5를 뺀 값
        /// </summary>
        /// <param name="data">순서당번 데이터(중요)</param>
        /// <returns>AC값 컬렉션</returns>
        public IEnumerable<int> AcValueData(IEnumerable<int> data)
        {
            //ac1
            var swap1 = data.ToList();
            var result1 = new List<int>();

            while (true)
            {
                if (swap1.Count < 2)
                {
                    break;
                }

                var zip = swap1.Zip(swap1.Skip(1), (a, b) => Math.Abs(a - b));
                result1.AddRange(zip);
                swap1 = new List<int>(zip);
            }

            var disc1 = result1.Distinct();
            int cnt1 = disc1.Count() - 5;
            yield return cnt1;

            //ac2
            var swap2 = data.ToList();
            var result2 = new List<int>();

            while (true)
            {
                if (swap2.Count < 2)
                {
                    break;
                }

                var zip = swap2.Zip(swap2.Skip(1), (a, b) => ZeroTo45(a + b));
                result2.AddRange(zip);
                swap2 = new List<int>(zip);
            }

            var disc2 = result2.Distinct();
            int cnt2 = disc2.Count() - 5;
            yield return cnt2;

            //ac3
            var swap3 = data.ToArray();
            var result3 = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                var each = new List<int>();

                for (int j = 0; j < 6; j++)
                {
                    if (i != j)
                    {
                        int n = Math.Abs(swap3[j] - swap3[i]);
                        each.Add(n);
                    }
                }

                result3.AddRange(each);
            }

            var disc3 = result3.Distinct();
            int cnt3 = disc3.Count() - 5;
            yield return cnt3;

            //ac4
            var swap4 = data.ToArray();
            var result4 = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                var each = new List<int>();

                for (int j = 0; j < 6; j++)
                {
                    if (i != j)
                    {
                        int n = ZeroTo45(swap4[j] + swap4[i]);
                        each.Add(n);
                    }
                }

                result4.AddRange(each);
            }

            var disc4 = result4.Distinct();
            int cnt4 = disc4.Count() - 5;
            yield return cnt4;
        }

        /// <summary>
        /// 동끝수 컬렉션
        /// </summary>
        /// <param name="order">회차</param>
        /// <returns>동끝배열 컬렉션</returns>
        public IEnumerable<int[]> SameEndsData(int order)
        {
            var list = new List<int[]>();

            var dang = Utility.DangbeonOfOrder(order);
            var chul = LuckyLibrary.KkeutbeonDatas().Select(x => x.Intersect(dang).ToArray());

            foreach (var arr in chul)
            {
                if (arr.Length > 1)
                {
                    yield return arr;
                }
            }
        }

        /// <summary>
        /// 당번의 앞뒤번호 차이값 컬렉션
        /// </summary>
        /// <param name="datas">당번데이터</param>
        /// <returns>차이값 컬렉션</returns>
        public IEnumerable<int> ZipSubtractData(IEnumerable<int> data)
        {
            return data.Zip(data.Skip(1), (a, b) => Math.Abs(a - b));
        }

        #region 내부 메서드
        private List<(int[], int[])> LowSumResult(IEnumerable<int[]> pivots)
        {
            int length = pivots.First().Length;
            var sequence = Enumerable.Range(0, length);
            var rst = new List<(int[], int[])>();

            for (int i = 2; i < length; i++)
            {
                var johaps = Utility.GetCombinations(sequence, i);

                foreach (var johap in johaps)
                {
                    var row = SelectedIndexLists(pivots, johap.ToArray());

                    //합계 값과 갯수
                    var qrps = row.GroupBy(x => x.Sum())
                        .Select(g => new { Value = g.Key, Count = g.Count() })
                        .OrderBy(x => x.Value).AsEnumerable().Select(x => (x.Value, x.Count));
                    int min = qrps.Min(x => x.Value);
                    int max = qrps.Max(x => x.Value);

                    //하한에서 상한으로 이동
                    int sum = 0;
                    int low = 0, top = 0;

                    for (int j = min; j < max; j++)
                    {
                        int n = !qrps.Any(x => x.Value == j) ? 0 : qrps.Where(x => x.Value == j).First().Count;
                        sum += n;

                        if (sum >= _hangey)
                        {
                            low = j;
                            break;
                        }
                    }

                    //상한에서 하한으로 이동
                    sum = 0;
                    for (int j = max; j > min; j--)
                    {
                        int n = !qrps.Any(x => x.Value == j) ? 0 : qrps.Where(x => x.Value == j).First().Count;
                        sum += n;

                        if (sum >= _hangey)
                        {
                            top = j;
                            break;
                        }
                    }

                    if (low > 0 || top < 6)
                    {
                        rst.Add((johap.ToArray(), Enumerable.Range(low, top - low + 1).ToArray()));
                    }
                }
            }

            return rst;
        }

        private int[] LowResult(Dictionary<int, int> pairs)
        {
            //하한에서 시작
            int sum = 0;
            int low = 0, top = 0;
            foreach (int key in pairs.Keys)
            {
                int val = pairs[key];
                sum += val;

                if (sum >= _hangey)
                {
                    low = key;
                    break;
                }
            }

            //상한에서 역순으로
            sum = 0;
            var revpairs = pairs.OrderByDescending(pairs => pairs.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (int key in revpairs.Keys)
            {
                int val = revpairs[key];
                sum += val;

                if (sum >= _hangey)
                {
                    top = key;
                    break;
                }
            }

            var sequence = Enumerable.Range(low, top - low + 1).ToArray();

            //최소 최대 내의 출수중에서 빠진것 찾기
            var explist = pairs.Where(pair => pair.Key > low && pair.Key < top)
                               .Where(x => x.Value <= _hangey).Select(pair => pair.Key).ToArray();

            var rstlist = sequence.Except(explist).ToArray();
            return rstlist;
        }

        /// <summary>
        /// typesu.dat 파일 작성
        /// </summary>
        private void WriteTypesu()
        {
            if (ChulsuPairs == null || !ChulsuPairs.Any())
            {
                GetDataOfFixSql();
            }

            int length = TypePairs.Values.First().Length;
            var ranges = Enumerable.Range(0, 10 + 1);   //타입은 0 - 10 사이값
            var rst = new List<string>();

            for (int i = 0; i < length; i++)
            {
                var pivots = TypePairs.Values.Select(x => x.ElementAt(i));
                var pairs = ranges.ToDictionary(row => row, row => pivots.Count(x => x == row));
                int[] row = LowResult(pairs);
                string s1 = i + "/" + string.Join(",", row);
                rst.Add(s1);
            }

            string path = Application.StartupPath + @"\DataFiles\typesu.dat";
            File.WriteAllText(path, string.Join(Environment.NewLine, rst));
        }

        /// <summary>
        /// chusu.dat 파일 작성
        /// </summary>
        private void WriteChulsu()
        {
            if (ChulsuPairs == null || !ChulsuPairs.Any())
            {
                GetDataOfFixSql();
            }

            int length = ChulsuPairs.Values.First().Count;
            var ranges = Enumerable.Range(0, 6 + 1);
            var rts = new List<string>();
            for (int i = 0; i < length; i++)
            {
                var pivots = ChulsuPairs.Values.Select(x => x.ElementAt(i));
                var rows = new List<string>();

                for (int j = 0; j < pivots.First().Length; j++)
                {
                    //열별 합계를 정하기
                    LowSumResult(pivots);

                    var pvts = pivots.Select(x => x.ElementAt(j));
                    var pairs = ranges.ToDictionary(row => row, row => pvts.Count(x => x == row));

                    int[] row = LowResult(pairs);
                    rows.Add(string.Join(",", row));
                }

                string s1 = i + "/" + string.Join("/", rows);
                rts.Add(s1);
            }

            string path = Application.StartupPath + @"\DataFiles\chulsu.dat";
            File.WriteAllText(path, string.Join(Environment.NewLine, rts));
        }

        /// <summary>
        /// chulsusum.dat 파일 작성
        /// </summary>
        private void WriteChulsuSum()
        {
            if (ChulsuPairs == null || !ChulsuPairs.Any())
            {
                GetDataOfFixSql();
            }

            int length = ChulsuPairs.Values.First().Count;
            var ranges = Enumerable.Range(0, 6 + 1);
            var lists = new List<string>();

            for (int i = 0; i < length; i++)
            {
                var pivots = ChulsuPairs.Values.Select(x => x.ElementAt(i));
                var rows = new List<int[]>();

                //열별 합계를 정하기
                var rst = LowSumResult(pivots);

                var data = ExceptContainLists(rst);

                foreach (var item in data)
                {
                    var idx = item.Item1;
                    var val = item.Item2;

                    string s1 = i + "/" + string.Join(",", idx) + "/" + string.Join(",", val);
                    lists.Add(s1);
                }
            }

            string path = Application.StartupPath + @"\DataFiles\chulsusum.dat";
            File.WriteAllText(path, string.Join(Environment.NewLine, lists));
        }

        /// <summary>
        /// 배열 컬렉션에서 인덱스의 값을 반환
        /// </summary>
        /// <param name="lists">배열 컬렉션</param>
        /// <param name="idxs">인덱스 배열</param>
        /// <returns>지연 컬렉션(정수값 배열)</returns>
        private IEnumerable<int[]> SelectedIndexLists(IEnumerable<int[]> lists, params int[] idxs)
        {
            foreach (var list in lists)
            {
                var array = idxs.Select(x => list[x]).ToArray();
                yield return array;
            }
        }

        /// <summary>
        /// 고유한 배열검사 결과 리스트
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        private IEnumerable<(int[], int[])> ExceptContainLists(List<(int[], int[])> lists)
        {
            var valist = lists.Select(x => x.Item1).ToList();
            var conbag = new ConcurrentBag<int[]>();

            foreach (var item in lists)
            {
                var idx = item.Item1;
                var val = item.Item2;
                bool pass = IsPerfectSame(conbag, idx);

                if (pass)
                {
                    conbag.Add(idx);
                    yield return ((idx, val));
                }
                else
                {
                    var findidxs = valist.Select((value, index) => (value, index))
                                    .Where(x => IsCountSame(conbag, x.value))
                                    .Select(x => x.index);

                    bool sameval = findidxs.All(x => !lists[x].Item2.SequenceEqual(val));

                    if (sameval)
                    {
                        conbag.Add(idx);
                        yield return ((idx, val));
                    }
                }
            }
        }

        /// <summary>
        /// 배열의 크기가 같고 요소가 고유하면 참
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsPerfectSame(ConcurrentBag<int[]> source, int[] target)
        {
            bool pass = false;

            if (!source.Any())
            {
                pass = true;
            }
            else
            {
                pass = source.All(x => !x.SequenceEqual(target));
            }

            return pass;
        }

        /// <summary>
        /// 배열의 크기가 다르고 요소중복수가 고유하면 참
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsCountSame(ConcurrentBag<int[]> source, int[] target)
        {
            bool pass = false;

            if (!source.Any())
            {
                pass = true;
            }
            else
            {
                //이미 같은것은 검사했으므로 길이가 다른것 찾기
                var diff = source.Where(x => x.Length != target.Length);

                if (!diff.Any())
                {
                    pass = true;
                }
                else
                {
                    pass = diff.All(x => x.Intersect(target).Count() != target.Length);
                }
            }

            return pass;
        }

        /// <summary>
        /// 중복되지 않으면 참
        /// </summary>
        /// <param name="lists">전체 배열리스트</param>
        /// <param name="array">검사할 배열</param>
        /// <returns>중된것이 없으면 참</returns>
        private bool IsPass(List<int[]> lists, int[] array)
        {
            bool pass = false;

            if (!lists.Any())
            {
                pass = true;
            }
            else
            {
                var each = lists.Where(x => x.Length == array.Length);
                pass = lists.All(x => x.Intersect(array).Count() != array.Length);
            }

            return pass;
        }

        /// <summary>
        /// 문자열 역순으로 바꾸기
        /// </summary>
        /// <param name="s">문자열</param>
        /// <returns>역순으로 바꾼 문자열</returns>
        private string ReverseString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        /// <summary>
        /// 입력값을 1-45 사이값으로 바꾸어 반환
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private int ZeroTo45(int number)
        {
            int n;

            if (number > 0)
            {
                //양수일때
                int tmp = number % 45;
                n = tmp == 0 ? 45 : tmp;
            }
            else if (number == 0)
            {
                //0 일때
                n = 45;
            }
            else
            {
                //음수일때
                int tmp = Math.Abs(number) % 45;
                n = tmp == 0 ? 45 : 45 - tmp;
            }

            return n;
        }

        /// <summary>
        /// 번호를 두자리로 바꾼 앞과 뒤의 값을 배열로 반환
        /// </summary>
        /// <param name="number">정수</param>
        /// <returns>정수튜플(앞자리값, 뒷자리값)</returns>
        private (int front, int back) ChangeOfNumber(int number)
        {
            string s = number.ToString("00");
            (int, int) tuple = (int.Parse(s[0].ToString()), int.Parse(s[1].ToString()));

            return tuple;
        }

        /// <summary>
        /// 회차의 범위안의 모든 당번들
        /// </summary>
        /// <param name="lastord">구간의 마지막 회차(포함)</param>
        /// <param name="section">검사 구간</param>
        /// <returns>고유 번호 컬렉션</returns>
        private IEnumerable<int> SectionOfAllData(int lastord, int section = 5)
        {
            int start = lastord - section + 1;
            var alldangs = new List<List<int>>();

            using (var db = new LottoDBContext())
            {
                alldangs = db.BasicTbl.Where(x => x.Orders >= start && x.Orders <= lastord)
                        .Select(x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }).ToList();
            }

            //SelectMany 로 개별단위로 리스트 만든것
            return alldangs.SelectMany(x => x).Distinct();
        }

        /// <summary>
        /// 정낙수 데이터
        /// 딕셔너리의 [0]은 최종회차임. (내림차순정렬)
        /// </summary>
        /// <param name="order">최종회차(포함)</param>
        /// <returns>낙수딕셔너리(회차의 인덱스, 해당 배열)</returns>
        private Dictionary<int, int[]> RegularMissing(int order)
        {
            var lst = new List<int>();
            var dic = new Dictionary<int, int[]>();

            //회차 - 100 이면 45개의 번호를 추출할수 있으므로 
            for (int i = order; i > order - 100; i--)
            {
                var dang = Utility.DangbeonOfOrder(i).ToArray();

                if (lst.Count == 0)
                {
                    dic.Add(i, dang);
                    lst.AddRange(dang);
                }
                else
                {
                    var exp = dang.Except(lst).ToArray();

                    if (exp.Any())
                    {
                        dic.Add(i, exp);
                        var temp = lst.Union(exp);
                        lst = new List<int>(temp);
                    }

                    if (lst.Count >= 45)
                    {
                        break;
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// 역낙첨 데이터 (다음회 당번과 비교해서 사용해야함)
        /// 딕셔너리의 [0]은 시작회차임. (오름차순정렬)
        /// </summary>
        /// <param name="order">최종회차(포함)</param>
        /// <returns></returns>
        private Dictionary<int, int[]> ReverseMissing(int order)
        {
            var lst = new List<int>();
            var dic = new Dictionary<int, int[]>();
            int start = RegularMissing(order).Keys.Last();

            //30 구간 루프
            for (int i = start; i <= order; i++)
            {
                var dang = Utility.DangbeonOfOrder(i).ToArray();

                if (lst.Count == 0)
                {
                    dic.Add(i, dang);
                    lst.AddRange(dang);
                }
                else
                {
                    var exp = dang.Except(lst).ToArray();

                    if (exp.Length != 0)
                    {
                        dic.Add(i, exp);
                        var temp = lst.Union(exp);
                        lst = new List<int>(temp);
                    }

                    if (lst.Count >= 45)
                    {
                        break;
                    }
                }
            }

            return dic;
        }
        #endregion

    }

}
