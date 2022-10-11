namespace LuckyLottoLibrary
{
    public class LuckyLibrary
    {
        /// <summary>
        /// 전체 당번배열 (1- 45)
        /// </summary>
        public static IEnumerable<int> Sequece { get; private set; } = Enumerable.Range(1, 45);

        /// <summary>
        /// 금회 추첨회차
        /// </summary>
        /// <returns>회차</returns>
        public static int NowWeekLottoCount()
        {
            int saturdayIndex = 6;                                  //토요일의 한주의 인덱스값
            int dayofWeek = (int)DateTime.Today.DayOfWeek;          //오늘의 한주의 인덱스값

            //이번주 토요일
            var saturday = DateTime.Today.AddDays(saturdayIndex - dayofWeek);

            //1회 로또 발표일
            var fromday = new DateTime(2002, 12, 7);

            var dayCount = saturday.Subtract(fromday);

            int n = (dayCount.Days / 7) + 1;        //이번주말 이므로 1을 더함

            return n;
        }

        /// <summary>
        /// 회차의 추첨일을 반환
        /// </summary>
        /// <param name="order"></param>
        /// <returns>날짜데이터</returns>
        public static DateTime DateOfOrder(int order)
        {
            var fromday = new DateTime(2002, 12, 7);
            var saturday = fromday.AddDays((order - 1) * 7);
            return saturday;
        }

        /// <summary>
        /// 시퀸스 이격간격 (24개)
        /// </summary>
        public static int[] SeqenceGapInts { get; } =
        {
            1, 2, 4, 7, 8, 11, 13, 14, 16, 17, 19, 22, 23, 26, 28, 29, 31, 32, 34, 37, 38, 41, 43, 44
        };

        /// <summary>
        /// 일정 이격 으로 만든 고유요소 45개 전체 리스트
        /// </summary>
        public static Dictionary<int, int[]> NewSequenceLists()
        {
            int num = 1;
            var temps = new Dictionary<int, int[]>();

            for (int i = 1; i <= 45; i++)
            {
                //앞뒤 번호 사이값 증가하면서 45개 번호 만들기
                var temp = new List<int> { 1 };

                for (int j = 0; j < 44; j++)
                {
                    int n = ZeroToInt(num + i);
                    num = n;
                    temp.Add(num);
                }

                if (temp.Count == 45 && temp.Distinct().Count() == 45)
                {
                    temps.Add(i, temp.ToArray());
                }

                num = 1;
            }

            return temps;
        }

        /// <summary>
        /// DataTable 에서 사용할 수 있게 1개 라인을 쿼리문장으로 바꾸기
        /// </summary>
        /// <param name="line">1 라인 문장</param>
        /// <returns>쿼리문장</returns>
        public static string ChangeMungang(string line)
        {
            string line1 = line.Trim();
            string rst;
            string[] bnk = { " " };
            var bnkSplit = line1.Split(bnk, StringSplitOptions.RemoveEmptyEntries);

            if (bnkSplit.Length < 3)
            {
                rst = string.Empty;
            }
            else
            {
                string[] copy = (string[])bnkSplit.Clone();

                for (int i = 0; i < bnkSplit.Length; i++)
                {
                    string s = bnkSplit[i].Trim();

                    if (s.Contains('!'))
                    {
                        s = s.Replace("!", "NOT ");
                    }

                    if (s.Contains('&'))
                    {
                        s = s.Replace("&", "AND");
                    }

                    if (s.Contains('|'))
                    {
                        s = s.Replace("|", "OR");
                    }

                    copy[i] = s;
                }

                //빈칸으로 나눈것을 다시 합치기
                rst = string.Join(" ", copy);
            }

            return rst;
        }

        /// <summary>
        /// 리스트를 문자열로 반환
        /// </summary>
        /// <param name="list">리스트</param>
        /// <param name="mark">구분 문자</param>
        /// <returns>문자열</returns>
        public static string ListToString(IEnumerable<int> list, string mark = ",")
        {
            string s = string.Join(mark, list.Select(x => x.ToString("00")));
            return s;
        }

        /// <summary>
        /// 격자에서 번호의 x,y 인덱스를 반환
        /// </summary>
        /// <param name="number">번호</param>
        /// <param name="rowCount">행의 갯수</param>
        /// <param name="isHor">가로방향 여부 (기본 참)</param>
        /// <returns>튜플 (xIndex: 열의 인덱스, yIndex: 행의 인덱스)</returns>
        public static (int xIndex, int yIndex) PositionOfData(int number, int rowCount, bool isHor = true)
        {
            var data = isHor ? HorizontalFlowDatas(rowCount) : VerticalFlowDatas(rowCount);
            int y = data.FindIndex(g => g.Contains(number));
            int x = Array.IndexOf(data[y], number);
            (int, int) tuple = (x, y);

            return tuple;
        }

        /// <summary>
        /// 문자열을 정수 리스트로 반환
        /// </summary>
        /// <param name="line">숫자를 포함한 문장열</param>
        /// <returns>정수 리스트</returns>
        public static List<int> StringToList(string line, string coma = ",")
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("빈 문장입니다.");
            }

            var split = line.Split(coma, StringSplitOptions.RemoveEmptyEntries);

            if (!split?.Any() ?? false)
            {
                throw new Exception("구분자로 나눌수 없습니다.");
            }

            var list = new List<int>();
            foreach (string s in split)
            {
                if (int.TryParse(s, out var n))
                {
                    list.Add(n);
                }
                else
                {
                    list = new List<int>();
                    break;
                }
            }

            return list;
        }

        /// <summary>
        /// 문자열을 컬렉션으로 반환
        /// </summary>
        /// <param name="line">문장</param>
        /// <param name="mark">구분자 (기본값: ",")</param>
        /// <returns>지연리스트(정수)</returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<int> StringToCollection(string line, string mark = ",")
        {
            var splits = line.Split(mark, StringSplitOptions.RemoveEmptyEntries);

            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("빈 문장입니다.");
            }
            if (!splits?.Any() ?? false)
            {
                throw new Exception("문자열을 컬렉션으로 변환할 수 없습니다.");
            }

            foreach (string s in splits)
            {
                if (int.TryParse(s, out var n))
                {
                    yield return n;
                }
                else
                {
                    throw new Exception("단어를 정수로 변환할 수 없습니다.");
                }
            }
        }

        /// <summary>
        /// 저수,고수 데이터
        /// </summary>
        /// <returns>저수 고수배열 리스트</returns>
        public static List<int[]> JeokosuDatas()
        {
            var jeo = Enumerable.Range(1, 22);

            var list = new List<int[]>
            {
                jeo.ToArray(),
                Sequece.Except(jeo).ToArray()
            };

            return list;
        }

        /// <summary>
        /// 홀수,짝수 데이터
        /// </summary>
        /// <returns>홀수 짝수배열 리스트</returns>
        public static List<int[]> HoljjackDatas()
        {
            var hol = Sequece.Where(x => x % 2 != 0);
            var list = new List<int[]>
            {
                hol.ToArray(),
                Sequece.Except(hol).ToArray()
            };

            return list;
        }

        /// <summary>
        /// 상하,좌우 데이터
        /// (상하수란 슬립용지의 위아래의 번호들)
        /// </summary>
        /// <returns>상하, 좌우배열 리스트</returns>
        public static List<int[]> SanghajwauDatas()
        {
            int[] up = new[] { 01, 02, 03, 04, 05, 06, 09, 10, 11, 12, 13, 17, 18, 25, 32, 33, 38, 39, 40, 41, 44, 45 };
            var list = new List<int[]>
            {
                up,
                Sequece.Except(up).ToArray()
            };

            return list;
        }

        /// <summary>
        /// 안쪽, 바깥 데이터
        /// (바깥수란 슬립용지의 외곽부분의 번도들)
        /// </summary>
        /// <returns>안쪽, 바깥쪽배열 리스트</returns>
        public static List<int[]> AnbakDatas()
        {
            var inn = new[] { 09, 10, 11, 12, 13, 16, 17, 18, 19, 20, 23, 24, 25, 26, 27, 30, 31, 32, 33, 34, 37, 44 };
            var list = new List<int[]>
            {
                inn,
                Sequece.Except(inn).ToArray()
            };

            return list;
        }

        /// <summary>
        /// 좌사선상, 좌사선하 데이터
        /// (좌사선상은 슬립용지에서 좌사선으로 그은 가상의 선 위부분의 번호들)
        /// </summary>
        /// <returns>좌사선상, 좌사선하배열 리스트</returns>
        public static List<int[]> LeftsaseonDatas()
        {
            var up = new[] { 02, 03, 04, 05, 06, 07, 10, 11, 12, 13, 14, 18, 19, 20, 21, 26, 27, 28, 34, 35, 42, 45 };
            var list = new List<int[]>
            {
                up,
                Sequece.Except(up).ToArray()
            };

            return list;
        }

        /// <summary>
        /// 우사선상, 우사선하 데이터
        /// (우사선상이란 슬립용지에서 우사선으로 그은 가상의 선 위부분의 번호들)
        /// </summary>
        /// <returns>우사선상, 우사선하배열 리스트</returns>
        public static List<int[]> RightsaseonDatas()
        {
            var up = new[] { 01, 02, 03, 04, 05, 06, 08, 09, 10, 11, 12, 15, 16, 17, 18, 22, 23, 24, 29, 30, 36, 43 };
            var list = new List<int[]>
            {
                up,
                Sequece.Except(up).ToArray()
            };

            return list;
        }

        /// <summary>
        /// 소삼합 리스트
        /// </summary>
        /// <returns>소수, 3배수, 합성수배열 리스트</returns>
        public static List<int[]> SosamhapDatas()
        {
            var sosu = new[] { 01, 02, 05, 07, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43 };
            var sam = Sequece.Where(x => x % 3 == 0).ToArray();
            var imsi = sosu.Union(sam);

            var list = new List<int[]>
            {
                sosu,
                sam,
                Sequece.Except(imsi).ToArray()
            };


            return list;
        }

        /// <summary>
        /// 번대수 데이터
        /// </summary>
        /// <returns>단번대, 10번대, 20번대, 30번대, 40번대배열 리스트</returns>
        public static List<int[]> BeondaeDatas()
        {
            var list = new List<int[]>
            {
                new[] { 01, 02, 03, 04, 05, 06, 07, 08, 09 },
                new[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 },
                new[] { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 },
                new[] { 30, 31, 32, 33, 34, 35, 36, 37, 38, 39 },
                new[] { 40, 41, 42, 43, 44, 45 }
            };

            return list;
        }

        /// <summary>
        /// 슬립수 데이터
        /// </summary>
        /// <returns>좌사선, 우사선, 상방, 하방, 좌측, 우측배열 리스트</returns>
        public static List<int[]> SlipDatas()
        {
            var list = new List<int[]>
            {
                new[] { 01, 09, 17, 25, 33, 41 },
                new[] { 07, 13, 19, 31, 37, 43 },
                new[] { 02, 03, 04, 05, 06, 10, 11, 12, 18 },
                new[] { 32, 38, 39, 40, 44, 45 },
                new[] { 08, 15, 16, 22, 23, 24, 29, 30, 36 },
                new[] { 14, 20, 21, 26, 27, 28, 34, 35, 42 }
            };

            return list;
        }

        /// <summary>
        /// 1중복허용 전체 3열 배열리스트
        /// </summary>
        /// <param name="fileName">3열 파일명</param>
        /// <returns>지연반환 배열리스트</returns>
        public static IEnumerable<List<int[]>> PureThreeColumnDatas(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                var each = new List<int[]>();
                var sequence = StringToList(line).ToArray();

                if (sequence.Any() && sequence.Length == 45)
                {
                    for (int i = 0; i < 45 / 3; i++)
                    {
                        int a = i * 3;
                        Range range = a..(a + 3);
                        int[] array = sequence[range];
                        each.Add(array);
                    }

                    yield return each;
                }
                else
                {
                    yield break;
                    throw new Exception("문자열 변환수가 45가 아닙니다.");
                }
            }
        }

        /// <summary>
        /// 2중복허용 전체 5열 배열리스트
        /// </summary>
        /// <param name="fileName">5열 파일명</param>
        /// <returns>지연반환 배열리스트</returns>
        public static IEnumerable<List<int[]>> PureFiveColumnDatas(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                var each = new List<int[]>();
                var sequence = StringToList(line).ToArray();

                if (sequence.Any() && sequence.Length == 45)
                {
                    for (int i = 0; i < 45 / 5; i++)
                    {
                        int a = i * 5;
                        Range range = a..(a + 5);
                        int[] array = sequence[range];
                        each.Add(array);
                    }

                    yield return each;
                }
                else
                {
                    yield break;
                    throw new Exception("문자열 변환수가 45가 아닙니다.");
                }
            }
        }

        /// <summary>
        /// 2중복허용 전체 7열 배열리스트
        /// </summary>
        /// <param name="fileName">7열 파일명</param>
        /// <returns>지연반환 배열리스트</returns>
        public static IEnumerable<List<int[]>> PureSevenColumnDatas(string fileName)
        {
            var ranges = new List<Range>
            {
                new Range(0, 7),
                new Range(7, 14),
                new Range(14, 21),
                new Range(21, 27),
                new Range(27, 33),
                new Range(33, 39),
                new Range(39, 45)
            };
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                var each = new List<int[]>();
                var sequence = StringToList(line).ToArray();

                if (sequence.Any() && sequence.Length == 45)
                {
                    foreach (var range in ranges)
                    {
                        int[] array = sequence[range];
                        each.Add(array);
                    }

                    yield return each;
                }
                else
                {
                    yield break;
                    throw new Exception("문자열 변환수가 45가 아닙니다.");
                }
            }
        }

        /// <summary>
        /// 종7 상하수 조합(7개)과 좌우사선의 상하수 조합(2개) 리스트
        /// 1-8, 8-15.. 2-9, 9-16..1-9, 9-17..7-13, 13-19...
        /// </summary>
        /// <returns></returns>
        public static List<List<int[]>> UpDownJohapLists()
        {
            var lists = VerticalFlowDatas(7).Select(x => x.Zip(x.Skip(1), (a, b) =>
                            new List<int> { a, b }.ToArray()).ToList()).ToList();

            var slips = SlipDatas().Take(2).Select(x => x.Zip(x.Skip(1), (a, b) =>
                            new List<int> { a, b }.ToArray()).ToList()).ToList();

            lists.AddRange(slips);

            return lists;
        }

        /// <summary>
        /// 4배수 - 9배수 데이터
        /// </summary>
        /// <returns>4배수 ~ 9배수배열 리스트</returns>
        public static List<int[]> BaesuDatas()
        {
            var list = new List<int[]>();

            for (int i = 4; i <= 9; i++)
            {
                var array = BaesuInts(i);
                list.Add(array);
            }

            return list;
        }

        /// <summary>
        /// 제곱수 데이터
        /// </summary>
        /// <returns>정수 리스트</returns>
        public static int[] JekobsuInts()
        {
            return new[] { 01, 04, 09, 16, 25, 36 };
        }

        /// <summary>
        /// 피보나치 데이터
        /// </summary>
        /// <returns>정수 리스트</returns>
        public static int[] PivonachInts()
        {
            return new[] { 01, 02, 03, 05, 08, 13, 21, 34 };
        }

        /// <summary>
        /// 역피보나치 데이터
        /// </summary>
        /// <returns>정수 리스트</returns>
        public static int[] YeokPivonachInts()
        {
            return new[] { 45, 44, 43, 41, 38, 33, 25, 12 };
        }

        /// <summary>
        /// 이항계수 데이터
        /// </summary>
        /// <returns>정수 리스트</returns>
        public static int[] IhangInts()
        {
            return new[] { 01, 03, 06, 10, 15, 21, 28, 36, 45 };
        }

        /// <summary>
        /// 쌍둥이수 데이터
        /// </summary>
        /// <returns></returns>
        public static int[] DonghyeongInts()
        {
            return new[] { 06, 09, 12, 13, 14, 21, 23, 24, 31, 32, 34, 41, 42, 43 };
        }

        /// <summary>
        /// 배수 데이터
        /// </summary>
        /// <param name="devide">나눌수 (3~9)</param>
        /// <returns>정수 리스트</returns>
        public static int[] BaesuInts(int devide)
        {
            if (devide < 3 || devide > 9)
            {
                throw new Exception("3 ~ 9 범위오류.");
            }

            return Sequece.Where(x => x % devide == 0).ToArray();
        }

        /// <summary>
        /// 전회당번의 주변 사각 꼭지점수
        /// </summary>
        /// <param name="dang"></param>
        /// <returns></returns>
        public static int[] JubeonSakakInts(IEnumerable<int> nums)
        {
            var arrs = new List<int>();
            nums.ToList().ForEach(x => arrs.AddRange(JubeonSakakInt(x)));
            return arrs.Distinct().OrderBy(x => x).ToArray();
        }

        /// <summary>
        /// 번호의 주변 사각 꼭지점수
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int[] JubeonSakakInt(int num)
        {
            int[] pos = { -8, -6, 6, 8 };
            return pos.Select(x => ZeroToInt(num + x)).ToArray();
        }

        /// <summary>
        /// 번호의 주변 마름모 꼭지점수
        /// </summary>
        /// <param name="dang"></param>
        /// <returns></returns>
        public static int[] JubeonMalumoInt(int num)
        {
            int[] pos = { -7, -1, 1, 7 };
            return pos.Select(x => ZeroToInt(num + x)).ToArray();
        }

        /// <summary>
        /// 전회당번의 주변 마름모 꼭지점수
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] JubeonMalumoInts(IEnumerable<int> nums)
        {
            var arrs = new List<int>();
            nums.ToList().ForEach(x => arrs.AddRange(JubeonMalumoInt(x)));
            return arrs.Distinct().OrderBy(x => x).ToArray();
        }

        /// <summary>
        /// 번호의 주변수 (주변사각 + 주변마름모)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int[] JubeonInks(int num)
        {
            int[] arrays = Array.Empty<int>();
            int[] pos = { -8, -7, -6, -1, 1, 6, 7, 8 };

            return pos.Select(x => ZeroToInt(num + x)).ToArray();
        }

        /// <summary>
        /// 끝번대 전체 데이터 (0끝 - 9끝)
        /// </summary>
        /// <returns>정수배열 리스트</returns>
        public static List<int[]> KkeutbeonDatas()
        {
            var array = new List<int[]>
            {
                new[] { 10, 20, 30, 40 },
                new[] { 01, 11, 21, 31, 41 },
                new[] { 02, 12, 22, 32, 42 },
                new[] { 03, 13, 23, 33, 43 },
                new[] { 04, 14, 24, 34, 44 },
                new[] { 05, 15, 25, 35, 45 },
                new[] { 06, 16, 26, 36 },
                new[] { 07, 17, 27, 37 },
                new[] { 08, 18, 28, 38 },
                new[] { 09, 19, 29, 39 }
            };

            return array;
        }

        /// <summary>
        /// 나인수 전체 데이터
        /// </summary>
        /// <returns>정수배열 리스트</returns>
        public static List<int[]> NainDatas()
        {
            var array = new List<int[]>();
            int[] basics = { 01, 02, 03, 10, 11, 12, 20, 21, 22 };
            var list = new List<int[]>();

            for (int i = 0; i < 45; i++)
            {
                var arr = basics.Select(x => ZeroToInt(x + i)).ToArray();
                list.Add(arr);
            }

            return list;
        }

        /// <summary>
        /// 가로방향 분할 데이터
        /// </summary>
        /// <param name="rowCount">행의 갯수</param>
        /// <param name="wayIndex">검사방법 인덱스 (0: 정방향, 1: 사선방향, 2: 회오리방향)</param>
        /// <returns>정수배열 리스트</returns>
        public static List<int[]> HorizontalFlowDatas(int rowCount, int wayIndex = 0)
        {
            var sequence = Enumerable.Range(1, 45).ToArray();

            var list = wayIndex switch
            {
                0 => HorizontalFlowInts(sequence, rowCount, GridDirection.Nomal),
                1 => HorizontalFlowInts(sequence, rowCount, GridDirection.LeftSaseon),
                2 => HorizontalFlowInts(sequence, rowCount, GridDirection.RightNaseon),
                _ => throw new Exception("인덱스는 0-2 사이값."),
            };

            //var grid = new int[,] { };

            //if (wayIndex == 1)
            //{
            //    grid = DiagonalGridDatas(rowCount);
            //}
            //else if (wayIndex == 2)
            //{
            //    grid = SpiralGridDatas(rowCount);
            //}
            //else if (wayIndex == 0)
            //{
            //    grid = FlowGridDatas(rowCount);
            //}

            //for (int i = 0; i < grid.GetLength(0); i++)
            //{
            //    var arr = new List<int>();

            //    for (int j = 0; j < grid.GetLength(1); j++)
            //    {
            //        int n = grid[i, j];
            //        arr.Add(n);
            //    }

            //    var exp = arr.Where(x => x >= 1 && x <= 45).ToArray();
            //    list.Add(exp);
            //}

            return list;
        }

        /// <summary>
        /// 세로방향 분할 데이터
        /// </summary>
        /// <param name="rowCount">행의 갯수</param>
        /// <param name="wayIndex">검사방법 인덱스 (0: 정방향, 1: 사선방향, 2: 회오리방향)</param>
        /// <returns>정수배열 리스트</returns>
        public static List<int[]> VerticalFlowDatas(int rowCount, int wayIndex = 0)
        {
            var sequence = Enumerable.Range(1, 45).ToArray();

            var list = wayIndex switch
            {
                0 => VerticalFlowInts(sequence, rowCount, GridDirection.Nomal),
                1 => VerticalFlowInts(sequence, rowCount, GridDirection.LeftSaseon),
                2 => VerticalFlowInts(sequence, rowCount, GridDirection.RightNaseon),
                _ => throw new Exception("인덱스는 0-2 사이값."),
            };
            //var list = new List<int[]>();
            //var grid = new int[,] { };

            //if (wayIndex == 1)
            //{
            //    grid = DiagonalGridDatas(rowCount);
            //}
            //else if (wayIndex == 2)
            //{
            //    grid = SpiralGridDatas(rowCount);
            //}
            //else if (wayIndex == 0)
            //{
            //    grid = FlowGridDatas(rowCount);
            //}

            //for (int i = 0; i < grid.GetLength(1); i++)
            //{
            //    var arr = new List<int>();

            //    for (int j = 0; j < grid.GetLength(0); j++)
            //    {
            //        int n = grid[j, i];
            //        arr.Add(n);
            //    }

            //    var exp = arr.Where(x => x >= 1 && x <= 45).ToArray();
            //    list.Add(exp);
            //}

            return list;
        }

        /// <summary>
        /// 가로방향 분할 데이터
        /// </summary>
        /// <param name="sequenceArray">간격 시퀸스배열</param>
        /// <param name="rowCount">행의 갯수</param>
        /// <param name="direction">열거형 검사방향</param>
        /// <returns>정수배열 리스트</returns>
        public static List<int[]> HorizontalFlowInts(int[] sequenceArray, int rowCount, GridDirection direction = GridDirection.Nomal)
        {
            int colCount = (45 % rowCount == 0) ? 45 / rowCount : 1 + (45 / rowCount);

            //배열의 기본 인덱스
            int[,] matrix = new int[rowCount, colCount];

            int n = 0;

            //가로방향 2차원 배열 만들기
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    if (n < 45)
                        matrix[row, col] = sequenceArray[n];
                    else
                        matrix[row, col] = 0;

                    n++;
                }
            }

            var lists = GetIntsList(matrix, direction);

            return lists;
        }

        /// <summary>
        /// 세로방향 분할 데이터
        /// </summary>
        /// <param name="sequenceArray">간격 시퀸스배열</param>
        /// <param name="rowCount">행의 갯수</param>
        /// <param name="direction">열거형 검사방향</param>
        /// <returns>정수배열 리스트</returns>
        public static List<int[]> VerticalFlowInts(int[] sequenceArray, int rowCount, GridDirection direction = GridDirection.Nomal)
        {
            int colCount = (45 % rowCount == 0) ? 45 / rowCount : 1 + (45 / rowCount);

            //배열의 기본 인덱스
            int[,] matrix = new int[rowCount, colCount];

            int n = 0;

            //가로방향 2차원 배열 만들기
            for (int col = 0; col < colCount; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    if (n < 45)
                        matrix[row, col] = sequenceArray[n];
                    else
                        matrix[row, col] = 0;

                    n++;
                }
            }

            var lists = GetIntsList(matrix, direction);

            return lists;
        }








        //     *****************   기초데이터   ******************





        /// <summary>
        /// 2차원배열을 검사방향 배열리스트로 반환
        /// </summary>
        /// <param name="matrix">2차원 배열</param>
        /// <param name="direction">열거형 검사방향</param>
        /// <returns>배열 리스트</returns>
        private static List<int[]> GetIntsList(int[,] matrix, GridDirection direction) => direction switch
        {
            GridDirection.LeftSaseon => LeftSaseonInts(matrix),
            GridDirection.RightSaseon => RightSaseonInts(matrix),
            GridDirection.LeftNaseon => LeftNaseonInts(matrix),
            GridDirection.RightNaseon => RightNaseonInts(matrix),
            GridDirection.LeftZigjag => LeftZigjagInts(matrix),
            GridDirection.RightZigjag => RightZigjagInts(matrix),
            _ => NomalInts(matrix)
        };

        /// <summary>
        /// 2차원배열을 정규방향 배열리스트로 반환
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private static List<int[]> NomalInts(int[,] matrix)
        {
            var lists = new List<int[]>();

            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                int[] array = new int[col];

                for (int j = 0; j < col; j++)
                {
                    array[j] = matrix[i, j];
                }

                lists.Add(array);
            }

            return lists;
        }

        /// <summary>
        /// 2D 배열에서 좌사선 배열리스트로 반환 (↗)
        /// </summary>
        /// <param name="matrix">2D Array</param>
        /// <returns>튜플(행인덱스, 열인덱스) 리스트)</returns>
        private static List<int[]> LeftSaseonInts(int[,] matrix)
        {
            var movepos = new List<(int y, int x)>();

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            int loop = 0;
            int y = 0, x = 0;

            while (loop < rowCount + colCount - 1)
            {
                var imsi = LeftDiagonal(y, x, matrix);
                movepos.AddRange(imsi);

                if (y < matrix.GetLength(0) - 1)
                {
                    y++;
                }
                else if (x < matrix.GetLength(1) - 1)
                {
                    x++;
                }

                loop++;
            }

            var colist = new List<int[]>();

            for (int i = 0; i < rowCount; i++)
            {
                int n = i * colCount;
                var cols = movepos.Skip(n).Take(colCount).Select(v => matrix[v.y, v.x]).ToArray();
                colist.Add(cols);
            }

            return colist;
        }

        /// <summary>
        /// 2D 배열에서 우사선 배열리스트로 반환 (↖)
        /// </summary>
        /// <param name="matrix">2D Array</param>
        /// <returns>튜플(행인덱스, 열인덱스) 리스트)</returns>
        private static List<int[]> RightSaseonInts(int[,] matrix)
        {
            var movepos = new List<(int y, int x)>();

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            int loop = 0;
            int y = 0, x = colCount - 1;

            while (loop < rowCount + colCount - 1)
            {
                var imsi = RightDiagonal(y, x, matrix);
                movepos.AddRange(imsi);

                if (y < matrix.GetLength(0) - 1)
                {
                    y++;
                }
                else if (x >= 0)
                {
                    x--;
                }

                loop++;
            }

            var colist = new List<int[]>();

            for (int i = 0; i < rowCount; i++)
            {
                int n = i * colCount;
                var cols = movepos.Skip(n).Take(colCount).Select(v => matrix[v.y, v.x]).ToArray();
                colist.Add(cols);
            }

            return colist;
        }

        /// <summary>
        /// 2D 배열에서 좌나선 배열리스트로 반환 (왼쪽소용돌이)
        /// </summary>
        /// <param name="matrix">2D Array</param>
        /// <returns>튜플(행인덱스, 열인덱스) 리스트)</returns>
        private static List<int[]> LeftNaseonInts(int[,] matrix)
        {
            var movepos = new List<(int y, int x)>();

            int x = 0, y = 0;
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            var remindrows = Enumerable.Range(0, rowCount).ToList();    //행인덱스 리스트
            var remindcols = Enumerable.Range(0, colCount).ToList();    //열인덱스 리스트

            while (movepos.Count < rowCount * colCount)
            {
                //행, 열인덱스 리스트가 비었다면 나가기
                if (!remindcols.Any() || !remindrows.Any())
                {
                    break;
                }

                //변경전 최소, 최대값
                int colmin = remindcols.Min();
                int colmax = remindcols.Max();

                #region 좌측이동

                for (int i = colmax; i >= colmin; i--)
                {
                    movepos.Add((y, i));

                    if (i == colmin)
                    {
                        //좌측에 도달했으면 행인덱스 삭제, 열인덱스 최소값
                        remindrows.Remove(y);
                        x = colmin;
                    }
                }

                //행인덱스 리스트가 비었다면 나가기
                if (remindrows.Count == 0)
                {
                    break;
                }

                //행인덱스 변경되어기 때문에 최소,최대 검사
                int rowmin = remindrows.Min();
                int rowmax = remindrows.Max();

                #endregion

                #region 하방이동

                for (int i = rowmin; i <= rowmax; i++)
                {
                    movepos.Add((i, x));

                    if (i == rowmax)
                    {
                        //하측에 도달했으면 열인덱스 삭제, 행인덱스는 최대값
                        remindcols.Remove(x);
                        y = rowmax;
                    }
                }

                //열인덱스 리스트가 비었다면 나가기
                if (remindcols.Count == 0)
                {
                    break;
                }

                colmin = remindcols.Min();
                colmax = remindcols.Max();

                #endregion

                #region 우측이동

                for (int i = colmin; i <= colmax; i++)
                {
                    movepos.Add((y, i));

                    if (i == colmax)
                    {
                        //우측에 도달했으면 행인덱스 삭제, 열인덱스 는 최대값
                        remindrows.Remove(y);
                        x = colmax;
                    }
                }

                //행인덱스 리스트가 비었다면 나가기
                if (remindrows.Count == 0)
                {
                    break;
                }

                rowmin = remindrows.Min();
                rowmax = remindrows.Max();

                #endregion

                #region 상방이동

                for (int i = rowmax; i >= rowmin; i--)
                {
                    movepos.Add((i, x));

                    if (i == rowmin)
                    {
                        //하측에 도달했으면 열인덱스 삭제, 행인덱스 최소값
                        remindcols.Remove(x);
                        y = rowmin;
                    }
                }

                #endregion
            }

            var colist = new List<int[]>();

            for (int i = 0; i < rowCount; i++)
            {
                int n = i * colCount;
                var cols = movepos.Skip(n).Take(colCount).Select(v => matrix[v.y, v.x]).ToArray();
                colist.Add(cols);
            }

            return colist;
        }

        /// <summary>
        /// 2D 배열에서 우나선 배열리스트로 반환 (오른쪽소용돌이)
        /// </summary>
        /// <param name="matrix">2D Array</param>
        /// <returns>튜플(행인덱스, 열인덱스) 리스트)</returns>
        private static List<int[]> RightNaseonInts(int[,] matrix)
        {
            var movepos = new List<(int y, int x)>();

            int x = 0, y = 0;
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            var remindrows = Enumerable.Range(0, rowCount).ToList();    //행인덱스 리스트
            var remindcols = Enumerable.Range(0, colCount).ToList();    //열인덱스 리스트

            while (movepos.Count < rowCount * colCount)
            {
                //행, 열인덱스 리스트가 비었다면 나가기
                if (!remindcols.Any() || !remindrows.Any())
                {
                    break;
                }

                //변경전 최소, 최대값
                int colmin = remindcols.Min();
                int colmax = remindcols.Max();

                #region 우측이동

                for (int i = colmin; i <= colmax; i++)
                {
                    movepos.Add((y, i));

                    if (i == colmax)
                    {
                        //우측에 도달했으면 행인덱스 삭제, 열인덱스 최대값
                        remindrows.Remove(y);
                        x = colmax;
                    }
                }

                //행인덱스 리스트가 비었다면 나가기
                if (remindrows.Count == 0)
                {
                    break;
                }

                //행인덱스 변경되어기 때문에 최소,최대 검사
                int rowmin = remindrows.Min();
                int rowmax = remindrows.Max();

                #endregion

                #region 하방이동

                for (int i = rowmin; i <= rowmax; i++)
                {
                    movepos.Add((i, x));

                    if (i == rowmax)
                    {
                        //하측에 도달했으면 열인덱스 삭제, 행인덱스는 최대값
                        remindcols.Remove(x);
                        y = rowmax;
                    }
                }

                //열인덱스 리스트가 비었다면 나가기
                if (remindcols.Count == 0)
                {
                    break;
                }

                colmin = remindcols.Min();
                colmax = remindcols.Max();

                #endregion

                #region 좌측이동

                for (int i = colmax; i >= colmin; i--)
                {
                    movepos.Add((y, i));

                    if (i == colmin)
                    {
                        //좌측에 도달했으면 행인덱스 삭제, 열인덱스 는 최소값
                        remindrows.Remove(y);
                        x = colmin;
                    }
                }

                //행인덱스 리스트가 비었다면 나가기
                if (remindrows.Count == 0)
                {
                    break;
                }

                rowmin = remindrows.Min();
                rowmax = remindrows.Max();

                #endregion

                #region 상방이동

                for (int i = rowmax; i >= rowmin; i--)
                {
                    movepos.Add((i, x));

                    if (i == rowmin)
                    {
                        //하측에 도달했으면 열인덱스 삭제, 행인덱스 최소값
                        remindcols.Remove(x);
                        y = rowmin;
                    }
                }

                #endregion
            }

            var colist = new List<int[]>();

            for (int i = 0; i < rowCount; i++)
            {
                int n = i * colCount;
                var cols = movepos.Skip(n).Take(colCount).Select(v => matrix[v.y, v.x]).ToArray();
                colist.Add(cols);
            }

            return colist;
        }

        /// <summary>
        /// 2D 배열에서 좌고비 배열리스트로 반환 (왼쪽 지그재그 세로데이터)
        /// </summary>
        /// <param name="matrix">2D Array</param>
        /// <returns>튜플(행인덱스, 열인덱스) 리스트)</returns>
        private static List<int[]> LeftZigjagInts(int[,] matrix)
        {
            var colist = new List<int[]>();

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                var temp = new List<int>();

                for (int j = 0; j < colCount; j++)
                {
                    temp.Add(matrix[i, j]);
                }

                if (i % 2 == 0)
                {
                    temp.Reverse();
                    colist.Add(temp.ToArray());
                }
                else
                {
                    colist.Add(temp.ToArray());
                }
            }

            var datas = new List<int>();

            //이제 세로방향으로 이동해서 리스트 채우기
            for (int i = 0; i < colist[0].Length; i++)
            {
                var data = colist.Select(x => x.ElementAt(i)).ToArray();
                datas.AddRange(data);
            }

            var rst = new List<int[]>();

            for (int i = 0; i < rowCount; i++)
            {
                int n = i * colCount;
                var cols = datas.Skip(n).Take(colCount).Select(v => v).ToArray();
                rst.Add(cols);
            }

            return rst;
        }

        /// <summary>
        /// 2D 배열에서 우고비 배열리스트로 반환 (오른쪽 지그재그 세로데이터)
        /// </summary>
        /// <param name="matrix">2D Array</param>
        /// <returns>튜플(행인덱스, 열인덱스) 리스트)</returns>
        private static List<int[]> RightZigjagInts(int[,] matrix)
        {
            var colist = new List<int[]>();

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                var temp = new List<int>();

                for (int j = 0; j < colCount; j++)
                {
                    temp.Add(matrix[i, j]);
                }

                if (i % 2 == 0)
                {
                    colist.Add(temp.ToArray());
                }
                else
                {
                    temp.Reverse();
                    colist.Add(temp.ToArray());
                }
            }

            var datas = new List<int>();

            //이제 세로방향으로 이동해서 리스트 채우기
            for (int i = 0; i < colist[0].Length; i++)
            {
                var data = colist.Select(x => x.ElementAt(i)).ToArray();
                datas.AddRange(data);
            }

            var rst = new List<int[]>();

            for (int i = 0; i < rowCount; i++)
            {
                int n = i * colCount;
                var cols = datas.Skip(n).Take(colCount).Select(v => v).ToArray();
                rst.Add(cols);
            }

            return rst;
        }

        /// <summary>
        /// 주어진 위치에서 좌사선위치 리스트
        /// </summary>
        /// <param name="y">행인덱스</param>
        /// <param name="x">열인덱스</param>
        /// <param name="matrix">2차원 배열</param>
        /// <returns>튜플(행, 열 인덱스) 리스트</returns>
        private static List<(int, int)> LeftDiagonal(int y, int x, int[,] matrix)
        {
            var lists = new List<(int, int)>();
            int col = matrix.GetLength(1);

            while (y >= 0 && x < col)
            {
                lists.Add((y, x));

                //좌사선은 행인덱스 감소, 열인덱스 증가
                y--;
                x++;
            }

            return lists;
        }

        /// <summary>
        /// 주어진 위치에서 우사선위치 리스트
        /// </summary>
        /// <param name="y">행인덱스</param>
        /// <param name="x">열인덱스</param>
        /// <param name="matrix">2차원 배열</param>
        /// <returns>튜플(행, 열 인덱스) 리스트</returns>
        private static List<(int, int)> RightDiagonal(int y, int x, int[,] matrix)
        {
            var lists = new List<(int, int)>();
            int row = matrix.GetLength(0);

            while ((y >= 0 && y < row) && x >= 0)
            {
                lists.Add((y, x));

                //우사선은 행인덱스 감소, 행인덱스 감소
                y--;
                x--;
            }

            return lists;
        }

        /// <summary>
        /// 입력번호를 1 ~ 45 사이 번호로 반환
        /// </summary>
        /// <param name="number">정수 번호</param>
        /// <returns>정수</returns>
        private static int ZeroToInt(int number)
        {
            int n;

            //음수일 경우
            if (number < 0)
            {
                int val = Math.Abs(number);
                n = (val % 45 == 0) ? 45 : 45 - (val % 45);
            }
            else if (number == 0)
            {
                n = 45;
            }
            else
            {
                int tmp = number % 45;
                n = (tmp == 0) ? 45 : tmp;
            }

            return n;
        }
    }

    /// <summary>
    /// 2차원 배열 검사방향
    /// </summary>
    public enum GridDirection
    {
        /// <summary>
        /// 정상방향
        /// </summary>
        Nomal,
        /// <summary>
        /// 좌사선 (왼쪽에서 우측으로 상향이동)
        /// </summary>
        LeftSaseon,
        /// <summary>
        /// 우사선 (오른쪽에서 좌측으로 상향이동)
        /// </summary>
        RightSaseon,
        /// <summary>
        /// 좌나선 (왼쪽 소용돌이)
        /// </summary>
        LeftNaseon,
        /// <summary>
        /// 우나선 (오른쪽 소용돌이)
        /// </summary>
        RightNaseon,
        /// <summary>
        /// 좌지그 (왼쪽, 오른쪽 아래로 이동)
        /// </summary>
        LeftZigjag,
        /// <summary>
        /// 우지그 (오른쪽, 왼쪽 아래로 이동)
        /// </summary>
        RightZigjag
    }
}