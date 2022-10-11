using LuckyLottoApp.Utilities;
using Newtonsoft.Json.Linq;

namespace LuckyLottoApp.Geomsas
{
    /// <summary>
    /// 열의 전체출수로 검사하는 클래스
    /// </summary>
    internal class Tonggwa
    {
        #region 필드
        private readonly Dictionary<string, (int, int)> _minmaxPairs = new();
        #endregion

        #region 속성

        /// <summary>
        /// 한계값 실수배열
        /// </summary>
        public static double[] LimitInts { get; } = { 0.01 * 0.3, 0.01 * 0.6, 0.01 * 0.9, 0.01 * 1.2, 0.01 * 2 };

        /// <summary>
        /// 최저 한계값 (출수 최대가 100 이상 0.01*0.3)
        /// </summary>
        public int BottomLimitValue { get; private set; }
        /// <summary>
        /// 하한 한계값 (출수 최대가 50-100 0.01*0.6)
        /// </summary>
        public int LowLimitValue { get; private set; }
        /// <summary>
        /// 기본 한계값 (출수 최대가 12 - 50 0.01*0.9)
        /// </summary>
        public int BasicLimitValue { get; private set; }
        /// <summary>
        /// 중간 한계값 (출수 최대가 4-12 0.01*1.2)
        /// </summary>
        public int UpperLimitValue { get; private set; }
        /// <summary>
        /// 최고 한계값 (출수 최대가 4 이하 0.01*2.0)
        /// </summary>
        public int TopLimitValue { get; private set; }


        /// <summary>
        /// 후방연속 갯수
        /// </summary>
        public int RealContinue { get; private set; } = -1;
        /// <summary>
        /// 연속최대 갯수
        /// </summary>
        public int MaxContinue { get; private set; } = -1;
        /// <summary>
        /// 무출 갯수
        /// </summary>
        public int ZeroCount { get; private set; } = -1;
        /// <summary>
        /// 유출 갯수
        /// </summary>
        public int NoneCount { get; private set; } = -1;
        /// <summary>
        /// 출수률
        /// </summary>
        public double Percentage { get; private set; } = 0.0;
        /// <summary>
        /// 최종출 갯수
        /// </summary>
        public int LastValueCount { get; private set; } = 0;
        /// <summary>
        /// 최종출수 출현간격 리스트
        /// </summary>
        public List<int> IntervalList { get; private set; } = new();
        /// <summary>
        /// 최종출 다음출수 리스트
        /// </summary>
        public List<int> NextList { get; private set; } = new();
        /// <summary>
        /// 후방 5패턴 다음출수 리스트
        /// </summary>
        public List<(int realCount, int maxCount, List<int> nextLis)> RealFiveLists { get; private set; } = new();

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public Tonggwa() 
        {
            _minmaxPairs = AllOfMinMax(); 
        }

        /*
         * 한계초과: 범위초과하면 악번
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
        /// 열출수 검사 결과를 반환
        /// </summary>
        /// <param name="collection">컬렉션</param>
        /// <param name="key">항목이름</param>
        /// <returns>딕셔너리(키: 항목, 값(-1: 불합격, 0: 무시, 1: 합격, -9: 에러))</returns>
        /// <exception cref="Exception"></exception>
        public Dictionary<string, int> CheckedTongka(IEnumerable<int> collection, string key)
        {
            var pairs = new Dictionary<string, int>();

            if (!(collection?.Any() ?? false))
            {
                throw new Exception("컬렉션이 널 혹은 비었습니다.");
            }

            int distcount = collection.Distinct().Count();
            if (distcount < 2)
            {
                pairs.Add("연속무출", -9);
                pairs.Add("연속진가", -9);
                pairs.Add("구간출합", -9);
                pairs.Add("후방연속", -9);
                pairs.Add("후방다음", -9);
                pairs.Add("후방간격", -9);
                pairs.Add("후방진가", -9);
                pairs.Add("후방패턴", -9);
                return pairs;
            }
            else
            {
                int top = _minmaxPairs[key].Item2;
                int count = collection.Count();
                double d = 0.01 * count;
                int limit = top switch
                {
                    <= 4 => Convert.ToInt32(d * 1.5),
                    <= 10 => Convert.ToInt32(d * 1.2),
                    <= 50 => Convert.ToInt32(d * 0.9),
                    <= 100 => Convert.ToInt32(d * 0.6),
                    _ => Convert.ToInt32(d * 0.3),
                };

                var (realCount, maxCount, nextList) = NextReal.RealMaxNextList(collection);
                var (sameCount, gaps) = NextReal.GetGapList(collection);

                RealContinue = realCount;
                MaxContinue = maxCount;
                NextList = nextList;
                ZeroCount = 0;
                NoneCount = 0;
                Percentage = 0.0;
                LastValueCount = sameCount;
                IntervalList = gaps;
                RealFiveLists = NextReal.RealMaxNextCombine(collection);
                int last = collection.Last();
                var realfive = collection.Skip(collection.Count() - 5);
                (int dotreal, int dotmax) dottpls = (-1, 0);
                var seclist = NextReal.GetSectionCount(collection, 10);
                bool secpass = seclist[^1] <= seclist.Min();
                bool secfail = seclist[^1] > seclist.Max();

                if (_minmaxPairs[key].Item1 < 1)
                {
                    ZeroCount = collection.Count(x => x == 0);
                    NoneCount = collection.Count(x => x != 0);
                    Percentage = NoneCount * 100.0 / (ZeroCount + NoneCount);
                    var dotlists = collection.Select(x => x == 0 ? 0 : 1).ToList();
                    dottpls = NextReal.RealMaxCount(dotlists);
                }

                //검사시작
                if (realCount > maxCount)
                {
                    string s = $"연속무출: {last} / {realCount} / {maxCount}";
                    pairs.Add("연속무출", last == 0 ? 1 : -1);
                }
                else if (secpass || secfail)
                {
                    string s = $"구간출합: {seclist[^1]} / {seclist.Min()} / {seclist.Max()}";
                    if (last == 9 && secpass)
                        pairs.Add("구간출합", 1);

                    if (secfail)
                        pairs.Add("구간출합", -1);
                }
                else if (dottpls.dotreal > dottpls.dotmax)
                {
                    if (collection.Min() == 0 && top < 7)
                    {
                        string s = $"연속진가: {last} / {dottpls.dotreal} / {dottpls.dotmax}";
                        pairs.Add("연속진가", last == 0 ? 1 : -1);
                    }
                }
                else
                {
                    if (realCount <= 5 && nextList.Count >= limit)
                    {
                        var nextfive = nextList.Skip(nextList.Count - 5);
                        var gapfive = gaps.Skip(gaps.Count - 5);
                        var tpls = new List<(int real, int max, List<int> nexts)>();
                        for (int i = 1; i <= 5; i++)
                        {
                            var tpl = NextReal.RealMaxNextList(collection, i);
                            tpls.Add(tpl);
                        }
                        var tplslast = tpls.Select(x => x.nexts.Last());
                        var copys = new List<(int real, int max, List<int> nexts)>(tpls);
                        copys.RemoveAt(realCount - 1);
                        var ditcnt = realfive.Distinct().ToList();
                        bool pass1 = copys.Where(x => x.nexts.Count >= 2).All(x => x.real < x.max);

                        //후방연속검사
                        if (realCount >= maxCount)
                        {
                            string s = $"후방연속: {last} / {realCount} / {maxCount} / {string.Join(",", realfive)}";
                            pairs.Add("후방연속", last == 0 ? 1 : -1);
                        }
                        //후방다음
                        else if (nextList?.Any() ?? false && nextList.Count >= 2)
                        {
                            if (nextList.All(x => x > last))
                            {
                                string s = $"후방다음: {last} / {nextList.Min()} / {nextList.Max()} / {string.Join(",", nextfive)}";
                                pairs.Add("후방다음", 1);
                            }
                            if (nextList.All(x => x < last))
                            {
                                string s = $"후방다음: {last} / {nextList.Min()} / {nextList.Max()} / {string.Join(",", nextfive)}";
                                pairs.Add("후방다음", -1);
                            }
                        }
                        //후방간격
                        else if (gapfive?.Any() ?? false && gapfive.Count() >= 2)
                        {
                            var gaptpl = NextReal.RealMaxCount(gaps);
                            if (last == 0)
                            {
                                if (gaptpl.realCount >= gaptpl.maxCount)
                                {
                                    string s = $"후방간격: {last} / {gaps.Last()} / {gaptpl.realCount} / {gaptpl.maxCount} / {string.Join(",", gapfive)}";
                                    pairs.Add("후방간격", 1);
                                }
                            }
                            else
                            {
                                var (minGap, maxGap, lastGap, countGap, isbad) = NextReal.MinMaxGapCount(collection);
                                if (sameCount < UpperLimitValue && isbad)
                                {
                                    string s = $"후방간격: {last} / {gaps.Last()} / {gaptpl.realCount} / {gaptpl.maxCount} / {string.Join(",", gapfive)}";
                                    pairs.Add("후방간격", -1);
                                }
                            }
                        }
                        //후방진가검사
                        else if (dottpls.dotreal >= dottpls.dotmax)
                        {
                            if (collection.Min() == 0 && top < 7)
                            {
                                string s = $"후방진가: {last} / {dottpls.dotreal} / {dottpls.dotmax} / {string.Join(",", realfive)}";
                                pairs.Add("후방진가", last == 0 ? 1 : -1);
                            }
                        }
                        //패턴5 검사
                        else if (copys.All(x => x.nexts.Any()) && ditcnt.Count >= 3 && pass1)
                        {
                            bool pass2 = (last == 0) ? copys.Count(x => x.nexts.Last() == 0) >= 3 : copys.Count(x => x.nexts.Last() != 0) >= 3;
                            if (pass2)
                            {
                                string s = $"후방패턴: {last} / {string.Join(",", tpls.Select(x => x.real))} / {string.Join(",", tplslast)}";
                                pairs.Add("후방패턴", last == 0 ? 1 : -1);
                            }
                        }
                    }
                }
                
                return pairs;
            }
        }

        /// <summary>
        /// 컬렉션의 하한,상한,제외리스트 추출
        /// </summary>
        /// <param name="collection">컬렉션</param>
        /// <param name="key">극저극고의 항목</param>
        /// <returns>튜틀(하한, 상한, 제외리스트)</returns>
        public (int lowerval, int upperval, List<int> explist) ExtractedCollection(IEnumerable<int> collection, string key)
        {
            int top = _minmaxPairs[key].Item2;
            int min = collection.Min();
            int max = collection.Max();
            int last = collection.Last();
            int count = collection.Count();

            int limit = top switch
            {
                <= 4 => Convert.ToInt32(count * LimitInts[4]),
                <= 12 => Convert.ToInt32(count * LimitInts[3]),
                <= 50 => Convert.ToInt32(count * LimitInts[2]),
                <= 100 => Convert.ToInt32(count * LimitInts[1]),
                _ => Convert.ToInt32(count * LimitInts[0]),
            };

            //누적한계로 하한과 상한으로 수정
            //하한에서 상한의 누적
            int minsum = 0, maxsum = 0;
            int cumin = min, cumax = max;
            for (int i = min; i < max - 1; i++)
            {
                int n = collection.Count(x => x == i);
                minsum += n;
                if (minsum >= limit)
                {
                    cumin = i;
                    break;
                }
            }
            //상한에서 하한의 누적
            for (int i = max; i > 0; i--)
            {
                int n = collection.Count(x => x == i);
                maxsum += n;
                if (maxsum >= limit)
                {
                    cumax = i;
                    break ;
                }
            }

            var ranges = Enumerable.Range(cumin, cumax - cumin + 1);
            var blankchuls = ranges.Where(x => !collection.Any(g => g == x)).ToList();
            int chmin = cumin, chmax = cumax;
            var reminds = new List<int>();
            if (blankchuls?.Any() ?? false)
            {
                //하한과 상한 사이에서 무출인것 뺀 제외수 찾기
                //저수 -> 고수 무출포함 제거
                for (int i = cumin + 1; i < cumax; i++)
                {
                    if (blankchuls.Contains(i))
                        chmin = i;
                    else
                        break;
                }
                //고수 -> 저수 무출초함 제거
                for (int i = cumax - 1; i > cumin; i--)
                {
                    if (blankchuls.Contains(i))
                        chmax = i;
                    else
                        break;
                }
                reminds = blankchuls.Where(x => x > chmin && x < chmax).ToList();
            }

            //다음 출수 검사에서 하한상한 제외 중복되것 찾기
            var nextlist = NextReal.NextList(collection).ToList();

            if (nextlist?.Any() ?? false && nextlist.Count >= limit)
            {
                int nxtlast = nextlist[^1];
                var imsi = new List<int>();
                if (!nextlist.Contains(last))
                {
                    imsi.Add(last);
                }
                var (realCount, maxCount) = NextReal.RealMaxCount(nextlist);
                if (realCount >= maxCount)
                {
                    imsi.Add(nxtlast);
                }
                var distlist = new List<int>();
                var zeros = new List<int>();
                if (nextlist.Distinct().Count() >= top / 2)
                {
                    var tuple = Enumerable.Range(chmin, chmax + 1)
                                          .Select(x => (chul : x, cnt: nextlist.Count(g => g == x))).ToList();

                    zeros = tuple.Where(x => x.cnt < 1).Select(x => x.chul).ToList();
                }
                imsi.AddRange(zeros);
                imsi.AddRange(reminds);
                distlist = imsi.OrderBy(x => x).Where(x => x > chmin && x < chmax).Distinct().ToList();

                int rtmin = chmin, rtmax = chmax;
                //다시한번 하한과 상한에서 제외
                for (int i = chmin + 1; i < chmax; i++)
                {
                    if (distlist.Contains(i))
                        rtmin = i;
                    else
                        break;
                }

                for (int i = chmax - 1; i > chmin; i--)
                {
                    if (distlist.Contains(i))
                        rtmax = i;
                    else
                        break;
                }
                var rtlist = distlist.Where(x => x > rtmin && x < rtmax).ToList();
                return (rtmin, rtmax, rtlist);
            }
            else
            {
                return (chmin, chmax, reminds);
            }
        }

        /// <summary>
        /// 컬렉션의 하한,상한,제외리스트 추출
        /// </summary>
        /// <param name="collection">컬렉션</param>
        /// <param name="valength">해당번호배열의 길이</param>
        /// <param name="key">극저극고의 항목</param>
        /// <returns>튜틀(하한, 상한, 제외리스트)</returns>
        public (int lowerval, int upperval, List<int> explist) ExtractedCollection(IEnumerable<int> collection, int valength, string key)
        {
            if (valength < 1)
            {
                return (0, 0, new List<int>());
            }
            else if (valength == 1)
            {
                return (0, 1, new List<int>());
            }
            else
            {
                int top = _minmaxPairs[key].Item2;
                int min = collection.Min();
                int max = collection.Max();
                int last = collection.Last();
                int count = collection.Count();

                int limit = top switch
                {
                    <= 4 => Convert.ToInt32(count * LimitInts[4]),
                    <= 12 => Convert.ToInt32(count * LimitInts[3]),
                    <= 50 => Convert.ToInt32(count * LimitInts[2]),
                    <= 100 => Convert.ToInt32(count * LimitInts[1]),
                    _ => Convert.ToInt32(count * LimitInts[0]),
                };

                //누적한계로 하한과 상한으로 수정
                //하한에서 상한의 누적
                int minsum = 0, maxsum = 0;
                int cumin = min, cumax = max;
                for (int i = min; i < max - 1; i++)
                {
                    int n = collection.Count(x => x == i);
                    minsum += n;
                    if (minsum >= limit)
                    {
                        cumin = i;
                        break;
                    }
                }
                //상한에서 하한의 누적
                for (int i = max; i > 0; i--)
                {
                    int n = collection.Count(x => x == i);
                    maxsum += n;
                    if (maxsum >= limit)
                    {
                        cumax = i;
                        break;
                    }
                }

                var ranges = Enumerable.Range(cumin, cumax - cumin + 1);
                var blankchuls = ranges.Where(x => !collection.Any(g => g == x)).ToList();
                int chmin = cumin, chmax = cumax;
                var reminds = new List<int>();
                if (blankchuls?.Any() ?? false)
                {
                    //하한과 상한 사이에서 무출인것 뺀 제외수 찾기
                    //저수 -> 고수 무출포함 제거
                    for (int i = cumin + 1; i < cumax; i++)
                    {
                        if (blankchuls.Contains(i))
                            chmin = i;
                        else
                            break;
                    }
                    //고수 -> 저수 무출초함 제거
                    for (int i = cumax - 1; i > cumin; i--)
                    {
                        if (blankchuls.Contains(i))
                            chmax = i;
                        else
                            break;
                    }
                    reminds = blankchuls.Where(x => x > chmin && x < chmax).ToList();
                }

                //다음 출수 검사에서 하한상한 제외 중복되것 찾기
                var nextlist = NextReal.NextList(collection).ToList();
                if (nextlist?.Any() ?? false && nextlist.Count >= limit)
                {
                    int nxtlast = nextlist[^1];
                    var imsi = new List<int>();
                    if (!nextlist.Contains(last))
                    {
                        imsi.Add(last);
                    }
                    var (realCount, maxCount) = NextReal.RealMaxCount(nextlist);
                    if (realCount >= maxCount)
                    {
                        imsi.Add(nxtlast);
                    }
                    var distlist = new List<int>();
                    if (nextlist.Distinct().Count() >= top / 2)
                    {
                        var tuple = Enumerable.Range(chmin, chmax + 1)
                                              .Select(x => (chul: x, cnt: nextlist.Count(g => g == x))).ToList();

                        var zeros = tuple.Where(x => x.cnt < 1).Select(x => x.chul).ToList();
                        imsi.AddRange(zeros);
                        imsi.AddRange(reminds);
                        distlist = imsi.OrderBy(x => x).Where(x => x > chmin && x < chmax).Distinct().ToList();
                    }
                    return (chmin, chmax, distlist);
                }
                else
                {
                    return (chmin, chmax, reminds);
                }
            }
        }

        /// <summary>
        /// 항목의 극저, 극대값 딕셔너리
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, (int, int)> AllOfMinMax()
        {
            var pairs = new Dictionary<string, (int, int)>();
            string path = Application.StartupPath + @"\GeomsaFiles\allminmax.json";
            JObject jObject = JObject.Parse(File.ReadAllText(path));
            pairs = jObject.ToObject<Dictionary<string, int[]>>().ToDictionary(x => x.Key, x => (x.Value[0], x.Value[1]));
            ////1 - 45,89,150,250(단독) 150 이상갯수 22
            //var overs = minmaxPairs.Where(x => x.Value.Item1 >= 1).ToDictionary(g => g.Key, g => g.Value);   //63개               
            //var sixs = minmaxPairs.Where(x => x.Value.Item1 < 1 && x.Value.Item2 <= 6).ToDictionary(g => g.Key, g => g.Value);//0 - 6
            //var mdls = minmaxPairs.Where(x => x.Value.Item1 < 1 && x.Value.Item2 > 6).ToDictionary(g => g.Key, g => g.Value); //0 - 7,9,10 26개
            return pairs;
        }

    }
}
