using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;

namespace LuckyLottoApp.Utilities
{
    /// <summary>
    /// 공용 사용 메서드 클래스
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Sql에 기록된 마지막 회차
        /// </summary>
        /// <returns>최종회차</returns>
        public static int GetLastOrder()
        {
            int order = 0;

            using (var db = new LottoDBContext())
            {
                order = db.BasicTbl.OrderByDescending(x => x.Orders).Select(x => x.Orders).First();
            }

            return order;
        }

        /// <summary>
        /// 금주의 로또회차
        /// </summary>
        /// <returns>금주회차</returns>
        public static int GetNowOrder()
        {
            int saturdayIndex = 6;                                  //토요일의 한주의 인덱스값
            int dayofWeek = (int)DateTime.Today.DayOfWeek;          //오늘의 한주의 인덱스값

            //이번주 토요일
            var saturday = DateTime.Today.AddDays(saturdayIndex - dayofWeek);

            //1회 로또 발표일
            var fromday = new DateTime(2002, 12, 7);
            var dayCount = saturday.Subtract(fromday);
            int order = (dayCount.Days / 7) + 1;                    //이번주말 이므로 1을 더함

            return order;
        }

        /// <summary>
        /// 회차의 추첨일을 반환
        /// </summary>
        /// <param name="order">회차</param>
        /// <returns>날짜데이터</returns>
        public static DateTime DateOfOrder(int order)
        {
            var fromday = new DateTime(2002, 12, 7);
            var saturday = fromday.AddDays((order - 1) * 7);

            return saturday;
        }

        /// <summary>
        /// 데이터에서 1 ~ 45 번이 출현한 갯수를 딕셔너리로 반환
        /// </summary>
        /// <param name="datas">리스트 배열</param>
        /// <returns></returns>
        public static IEnumerable<(int ord, int cnt)> CountOfNumber(IEnumerable<IEnumerable<int>> datas)
        {
            var data = datas.SelectMany(x => x);

            for (int i = 1; i <= 45; i++)
            {
                int n = data.Count(x => x == i);
                yield return (i, n);
            }
        }

        /// <summary>
        /// 정수 컬렉션의 후방연속, 최대연속
        /// </summary>
        /// <param name="lists">정수 컬렉션</param>
        /// <param name="isAsc">오름차순여부 (기본값: 참)</param>
        /// <returns>튜플(후방연속, 연속최대)</returns>
        /// <exception cref="Exception"></exception>
        public static (int realCount, int maxCount) RealMaxCount(IEnumerable<int> lists, bool isAsc = true)
        {
            if (!lists?.Any() ?? false)
            {
                throw new Exception("배열에 요소가 없습니다.");
            }

            int[] ascArray = isAsc ? lists.ToArray() : lists.Reverse().ToArray();
            int real = 0;
            int last = ascArray[^1];

            //후방연속
            for (int i = ascArray.Length - 1; i >= 0; i--)
            {
                if (last <= ascArray[i])
                    real++;
                else
                    break;
            }

            //연속최대
            int[] ascRemind = ascArray[..^real];
            int max = 0, dup = 0;

            foreach (int n in ascRemind)
            {
                if (n >= last)
                {
                    dup++;
                }
                else
                {
                    if (dup > max)
                    {
                        max = dup;
                    }

                    dup = 0;
                }
            }

            if (dup > max)
            {
                max = dup;
            }

            return (real, max);
        }

        /// <summary>
        /// 난수로 섞인것을 반환
        /// Fisher-Yates Shuffle Algorithm
        /// </summary>
        /// <param name="sequence">리스트</param>
        /// <returns>섞인 리스트</returns>
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> sequence)
        {
            T[] array = sequence.ToArray();
            int length = array.Length;

            for (int i = 0; i < length - 1; i++)
            {
                int index = RandomNumberGenerator.GetInt32(i, length);
                (array[i], array[index]) = (array[index], array[i]);
            }

            return array;
        }

        public static IEnumerable<T> Shuffle<T>(RandomNumberGenerator random, IEnumerable<T> sequence)
        {
            T[] array = sequence.ToArray();
            int length = array.Length;

            for (int i = 0; i < length - 1; i++)
            {
                int index = Next(random, i, length);
                (array[i], array[index]) = (array[index], array[i]);
            }

            return array;
        }

        /// <summary>
        /// 음수가 아닌 임의의 정수를 반환
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            return RandomNumberGenerator.GetInt32(256);
        }

        public static int Next(RandomNumberGenerator random)
        {
            var data = new byte[sizeof(int)];
            random.GetBytes(data);
            return BitConverter.ToInt32(data, 0) & (int.MaxValue - 1);
        }

        public static int Next(RandomNumberGenerator random, int min, int max)
        {
            if (min > max)
            {
                throw new Exception("최소값이 최대값 보다 큽니다.");
            }

            max--;
            var bytes = new byte[sizeof(uint)];  //4bytes
            random.GetNonZeroBytes(bytes);
            uint val = BitConverter.ToUInt32(bytes); //ToInt32(bytes);
            var result = ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;

            return (int)result;
        }

        /// <summary>
        /// 리스트의 차집합을 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists"></param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T>(params IEnumerable<T>[] lists)
        {
            return lists.Where(g => g.Any()).Aggregate((a, b) => a.Except(b));
        }

        /// <summary>
        /// 순열결과를 반환 (앞뒤 중복제거)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">리스트</param>
        /// <param name="length">조합개수</param>
        /// <returns>IEnumerable<IEnumerable<T>></returns>
        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length) where T : IComparable
        {
            if (!list.Any())
            {
                throw new Exception("리스트의 요소가 없습니다.");
            }

            if (length == 0 || length > list.Count())
            {
                throw new Exception("조합갯수는 1 ~ 배열의 크기 사이여야 합니다.");
            }

            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetCombinations(list, length - 1).SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// 조합결과를 반환 (앞뒤 중복포함)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">리스트</param>
        /// <param name="length">조합개수</param>
        /// <returns>IEnumerable<IEnumerable<T>></returns>
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (!list.Any())
            {
                throw new Exception("리스트의 요소가 없습니다.");
            }

            if (length == 0 || length > list.Count())
            {
                throw new Exception("조합갯수는 1 ~ 배열의 크기 사이여야 합니다.");
            }

            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// 전체 조합결과를 반환 (동일번호 중복포함)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">리스트</param>
        /// <param name="length">조합개수</param>
        /// <returns>IEnumerable<IEnumerable<T>></returns>
        public static IEnumerable<IEnumerable<T>> GetAllPermutations<T>(IEnumerable<T> list, int length)
        {
            if (!list.Any())
            {
                throw new Exception("리스트의 요소가 없습니다.");
            }

            if (length == 0 || length > list.Count())
            {
                //throw new Exception("조합갯수는 1 ~ 배열의 크기 사이여야 합니다.");
            }

            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetAllPermutations(list, length - 1).SelectMany(t => list, (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        /// <summary>
        /// 연속연결 조합
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">리스트</param>
        /// <param name="length">조합갯수</param>
        /// <returns></returns>
        public static IEnumerable<T[]> GetSequences<T>(IEnumerable<T> list, int length)
        {
            if (!list.Any())
            {
                throw new Exception("리스트의 요소가 없습니다.");
            }

            if (length == 0 || length > list.Count())
            {
                throw new Exception("조합갯수는 1 ~ 배열의 크기 사이여야 합니다.");
            }


            T[] arrays = list.ToArray();

            for (int i = 0; i < arrays.Length; i++)
            {
                T[] temp = new T[length];

                for (int j = 0; j < length; j++)
                {
                    int idx = i + j;
                    int n = idx >= arrays.Length ? idx - arrays.Length : idx;
                    temp[j] = arrays[n];
                }

                yield return temp;
            }
        }

        /// <summary>
        /// 연속연결 조합
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">리스트</param>
        /// <param name="startLength">시작 조합갯수</param>
        /// <returns></returns>
        public static List<T[]> GetManySequences<T>(IEnumerable<T> list, int startLength, int endLength)
        {
            if (!list.Any())
            {
                throw new Exception("리스트의 요소가 없습니다.");
            }

            if (endLength > list.Count())
            {
                throw new Exception("리스트 개수보다 조합갯수가 큽니다.");
            }

            var lists = new List<T[]>();

            for (int i = startLength; i <= endLength; i++)
            {
                var enumerble = GetSequences(list, i);
                lists.AddRange(enumerble);
            }

            return lists;
        }

        /// <summary>
        /// 컬렉션 내의 고유한 배열을 반환
        /// </summary>
        /// <param name="collection">정수배열 컬렉션</param>
        /// <returns>정수배열 컬렉션</returns>
        public static List<int[]> DistinctByList(IEnumerable<int[]> collection)
        {
            var addlists = new List<int[]>();

            if (!collection?.Any() ?? false)
            {
                throw new Exception("배열에 요소가 없습니다.");
            }

            foreach (int[] array in collection)
            {
                if (!addlists.Any())
                {
                    addlists.Add(array);
                }
                else
                {
                    if (addlists.All(x => !x.SequenceEqual(array)))
                    {
                        addlists.Add(array);
                    }
                }
            }

            List<int[]> ord = new();
            int length = addlists.First().Length;
            ord = length switch
            {
                1 => ord = addlists.OrderBy(x => x[0]).ToList(),
                2 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ToList(),
                3 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ToList(),
                4 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ToList(),
                5 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ThenBy(x => x[4]).ToList(),
                6 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ThenBy(x => x[4])
                                   .ThenBy(x => x[5]).ToList(),
                7 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ThenBy(x => x[4])
                                   .ThenBy(x => x[5]).ThenBy(x => x[6]).ToList(),
                8 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ThenBy(x => x[4])
                                   .ThenBy(x => x[5]).ThenBy(x => x[6]).ThenBy(x => x[7]).ToList(),
                9 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ThenBy(x => x[4])
                                   .ThenBy(x => x[5]).ThenBy(x => x[6]).ThenBy(x => x[7]).ThenBy(x => x[8]).ToList(),
                10 => ord = addlists.OrderBy(x => x[0]).ThenBy(x => x[1]).ThenBy(x => x[2]).ThenBy(x => x[3]).ThenBy(x => x[4])
                                   .ThenBy(x => x[5]).ThenBy(x => x[6]).ThenBy(x => x[7]).ThenBy(x => x[8]).ThenBy(x => x[9]).ToList(),
                _ => throw new Exception("정렬할 배열의 길이는 1 - 10 까지 입니다.")
            };

            return ord;
        }

        /// <summary>
        /// 입력리스트를 갯수로 나눈배열 리스트
        /// </summary>
        /// <param name="list">문자열 검사 데이터</param>
        /// <param name="divid">나눌 갯수</param>
        /// /// <returns>문자열배열 리스트</returns>
        public static List<string[]> SplitCollectionStrings(IEnumerable<string> list, int divid)
        {
            var data = list.Select((val, idx) => new { val, idx }).GroupBy(x => x.idx / divid)
                           .Select(x => x.Select(v => v.val).ToArray()).ToList();

            return data;
        }

        /// <summary>
        /// 행렬의 곱셈의 결과 (행별 합계 1.0인 방식)
        /// </summary>
        /// <param name="matrix1">실수배열</param>
        /// <param name="matrix2">리스트 실수배열</param>
        /// <returns>실수배열</returns>
        public static double[] MultipleMatrix(double[] matrix1, IEnumerable<double[]> matrix2)
        {
            //행렬1의 열과 행렬2의 행의 값이 다르면 계산할 수 없습니다.
            if (matrix1.Any() && matrix2.Any())
            {
                if (matrix1.Length != matrix2.Count() || matrix2.Any(x => x.Length != matrix1.Length))
                {
                    throw new Exception("행렬 길이가 다름.");
                }

                //배열의 크기는 두번째 행렬의 열의 갯수
                double[] array = new double[matrix2.First().Length];
                var tmpMatrix = matrix2.ToArray();

                for (int j = 0; j < array.Length; j++)
                {
                    double a = 0.0;

                    for (int k = 0; k < matrix1.Length; k++)
                    {
                        a += matrix1[k] * tmpMatrix[k][j];
                    }

                    array[j] = a;
                }

                return array;
            }
            else
            {
                throw new Exception("리스트에 요소가 없음.");
            }
        }

        /// <summary>
        /// 출수를 타입 인덱스로 반환
        /// (111111-0, 21111-1, 2211-2, 222-3, 3111-4, 321-5, 33-6, 411-7, 42-8, 51-9, 60-10)
        /// </summary>
        /// <param name="chulsuInts">출수리스트</param>
        /// <returns>인덱스</returns>
        public static int ConvertTypeIndex1(IEnumerable<int> chulsuInts)
        {
            int idx;

            switch (chulsuInts.Max())
            {
                case 1:
                    idx = 0;    //111111
                    break;
                case 2:
                    {
                        int dup = chulsuInts.Count(x => x == 2);
                        idx = (dup == 3) ? 3 : (dup == 2) ? 2 : 1;  //222, 2211, 21111
                        break;
                    }
                case 3:
                    {
                        int dup = chulsuInts.Count(x => x == 3);
                        idx = (dup == 2) ? 6 : chulsuInts.Contains(2) ? 5 : 4; //33, 321, 3111
                        break;
                    }
                case 4:
                    {
                        idx = chulsuInts.Contains(2) ? 8 : 7;   //42, 411
                        break;
                    }
                case 5:
                    idx = 9;    //51
                    break;
                case 6:
                    idx = 10;   //60
                    break;
                default:
                    throw new Exception("타입을 검사할 수 없습니다.");
            }

            return idx;
            //case 1:
            //    i = 0;
            //    break;
            //case 2:
            //    {
            //        int n = chulsuInts.Count(x => x == 2);

            //        if (n == 1)
            //        {
            //            i = 1;
            //        }
            //        else if (n == 2)
            //        {
            //            i = 2;
            //        }
            //        else
            //        {
            //            i = 3;
            //        }

            //        break;
            //    }

            //case 3:
            //    {
            //        int n = chulsuInts.Count(x => x == 3);

            //        if (n == 1)
            //        {
            //            i = chulsuInts.Contains(2) ? 5 : 4;
            //        }
            //        else
            //        {
            //            i = 6;
            //        }

            //        break;
            //    }

            //case 4:
            //    i = chulsuInts.Contains(2) ? 8 : 7;
            //    break;
            //case 5:
            //    i = 9;
            //    break;
            //default:
            //    i = 10;
            //    break;
        }

        /// <summary>
        /// 출수를 타입 인덱스로 반환
        /// (111111-0, 21111-1, 2211-2, 222-3, 3111-4, 321-5, 33-6, 411-7, 42-8, 51-9, 60-10)
        /// </summary>
        /// <param name="chulsuInts">출수리스트</param>
        /// <returns>타입 인덱스</returns>
        public static int ConvertTypeIndex(IEnumerable<int> chulsuInts)
        {
            if (chulsuInts.Sum() != 6)
            {
                throw new Exception("나온갯수 합이 6이 아닙니다.");
            }

            var indexs = new List<int[]>
            {
                new[] { 1, 1, 1, 1, 1, 1 }, new[] { 2, 1, 1, 1, 1 }, new[] { 2, 2, 1, 1 }, new[] { 2, 2, 2 }, new[] { 3, 1, 1, 1 },
                new[] { 3, 2, 1 }, new[] { 3, 3 }, new[] { 4, 1, 1 }, new[] { 4, 2 }, new[] { 5, 1 }, new[] { 6 }
            };
            var nonzeros = chulsuInts.Where(x => x != 0).OrderByDescending(x => x).ToList();
            int idx = indexs.FindIndex(x => x.SequenceEqual(nonzeros));
            if (idx == -1)
            {
                throw new Exception("타입 인덱스를 찾을수 없음.");
            }
            return idx;
        }

        /// <summary>
        /// 당번의 앞뒤 사이에서 출현방향을 인덱스로 반환
        /// 0(→), 1(↗), 2(↑), 3(↖), 4(←), 5(↙), 6(↓), 7(↘)
        /// </summary>
        /// <param name="dang">검사당번</param>
        /// <returns>방향인덱스 배열</returns>
        public static int[] DirectionIndexInts(IEnumerable<int> dang)
        {
            var data = dang.ToList();
            var array = new int[5];

            for (int i = 0; i < 5; i++)
            {
                int frontnum = data[i];
                int backnum = data[i + 1];

                //yIndex = rowIndex 이고 xIndex = columnIndex
                var pt1 = LuckyLibrary.PositionOfData(frontnum, 7);
                var pt2 = LuckyLibrary.PositionOfData(backnum, 7);
                int pos;

                if (pt1.yIndex == pt2.yIndex)   
                {
                    //같은 행인덱스에 있다면 0, 4
                    pos = frontnum < backnum ? 0 : 4;
                }
                else if (pt1.xIndex == pt2.xIndex)
                {
                    //같은 열인덱스에 있다면 2, 6
                    pos = frontnum < backnum ? 6 : 2;
                }
                else if (pt1.xIndex < pt2.xIndex && pt1.yIndex > pt2.yIndex)
                {
                    pos = 1;
                }
                else if (pt1.xIndex > pt2.xIndex && pt1.yIndex > pt2.yIndex)
                {
                    pos = 3;
                }
                else if (pt1.xIndex < pt2.xIndex && pt1.yIndex < pt2.yIndex)
                {
                    pos = 7;
                }
                else
                {
                    pos = 5;
                }

                array[i] = pos;
            }

            return array;
        }

        #region 수학용 메서드

        /// <summary>
        /// 입력번호를 1 ~ 45 사이 번호로 반환
        /// </summary>
        /// <param name="number">정수번호</param>
        /// <returns>정수번호</returns>
        public static int ZeroToInt(int number)
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

        /// <summary>
        /// 데이터에서 기울기와 절편값을 반환 (y = ax + b 에서 기울기 a 값과, 절편 b 값을 반환)
        /// 최소제곱법으로 직선의 기울기와 절편을 찾기
        /// </summary>
        /// <param name="arrayIntsX">x축의 배열(1,2,3,4,5)</param>
        /// <param name="arrayIntsY">y축의 배열(810,814,818,824,826)</param>
        /// <returns>튜플(기울기, 절편)</returns>
        public static (float slope, float intercept) LeastSquare(int[] arrayIntsX, int[] arrayIntsY)
        {
            if (arrayIntsX.Length != arrayIntsY.Length)
            {
                throw new Exception("배열의 크기가 서로 다릅니다.");
            }

            var averagex = arrayIntsX.Average();
            var averagey = arrayIntsY.Average();
            double abase = 0.0D;
            double bbase = 0.0D;

            for (int i = 0; i < arrayIntsY.Length; i++)
            {
                // (x축 값 - x축의 평균값) ^ 2 
                abase += Math.Pow(arrayIntsX[i] - averagex, 2);
                bbase += (arrayIntsY[i] - averagey) * (arrayIntsX[i] - averagex);
            }

            double a = bbase / abase;
            double b = averagey - (a * averagex);

            return ((float)a, (float)b);
        }

        /// <summary>
        /// 데이터에서 기울기와 절편값을 반환 (y = ax + b 에서 기울기 a 값과, 절편 b 값을 반환)
        /// 최소제곱법으로 직선의 기울기와 절편을 찾기
        /// </summary>
        /// <param name="arrayIntsX">x축의 배열(1,2,3,4,5)</param>
        /// <param name="arrayIntsY">y축의 배열(810,814,818,824,826)</param>
        /// <returns>튜플(기울기, 절편)</returns>
        public static (float slope, float intercept) LeastSquare(double[] arrayIntsX, double[] arrayIntsY)
        {
            if (arrayIntsX.Length != arrayIntsY.Length)
            {
                throw new Exception("배열의 크기가 서로 다릅니다.");
            }

            var averagex = arrayIntsX.Average();
            var averagey = arrayIntsY.Average();
            double abase = 0.0D;
            double bbase = 0.0D;

            for (int i = 0; i < arrayIntsY.Length; i++)
            {
                // (x축 값 - x축의 평균값) ^ 2 
                abase += Math.Pow(arrayIntsX[i] - averagex, 2);
                bbase += (arrayIntsY[i] - averagey) * (arrayIntsX[i] - averagex);
            }

            double a = bbase / abase;
            double b = averagey - (a * averagex);

            return ((float)a, (float)b);
        }

        /// <summary>
        /// 실수에서 소수점 아래 자르기
        /// </summary>
        /// <param name="value">실수</param>
        /// <param name="precision">소수점 갯수 (1이상 정수)</param>
        /// <returns>실수</returns>
        public static double TruncatedDouble(double value, int precision)
        {
            double step = Math.Pow(10, precision);
            double temp = Math.Truncate(step * value);
            return temp / step;
        }

        /// <summary>
        /// 끝수의 버림 결과반환
        /// </summary>
        /// <param name="value">입력 정수</param>
        /// <returns>반환 정수</returns>
        public static int TruncateValue(int value)
        {
            int n;

            if (value % 5 == 0)
            {
                n = value;
            }
            else
            {
                string[] lows = { "1", "2", "3", "4" };
                string[] highs = { "6", "7", "8", "9" };
                string num = value.ToString();
                var chars = num.ToCharArray();
                string last = num[^1].ToString();
                if (lows.Contains(last))
                {
                    chars[^1] = '0';
                    num = new string(chars);
                }
                else if (highs.Contains(last))
                {
                    chars[^1] = '5';
                    num = new string(chars);
                }

                n = int.Parse(num);
            }
            return n;
        }

        /// <summary>
        /// 배열을 2차월 배열로 반환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="height">행의 크기</param>
        /// <param name="width">열의 크기</param>
        /// <returns></returns>
        public static T[,] Make2DArray<T>(T[] input, int height, int width)
        {
            T[,] output = new T[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output[i, j] = input[i * width + j];
                }
            }

            return output;
        }

        #endregion

        /// <summary>
        /// 회차의 정렬당번
        /// </summary>
        /// <param name="order">회차</param>
        /// <param name="isBonus">보너스 포함여부 (기본: 거짓)</param>
        /// <returns>당번 정수배열</returns>
        public static int[] DangbeonOfOrder(int order, bool isBonus = false)
        {
            int[] orders = Array.Empty<int>();

            if (isBonus)
            {
                using var db = new LottoDBContext();
                var dangs = db.BasicTbl.Where(x => x.Orders == order)
                    .Select(x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6, x.Bonus }).Single();

                orders = dangs.ToArray();
            }
            else
            {
                using var db = new LottoDBContext();
                var dangs = db.BasicTbl.Where(x => x.Orders == order)
                    .Select(x => new List<int> { x.Gu1, x.Gu2, x.Gu3, x.Gu4, x.Gu5, x.Gu6 }).Single();

                orders = dangs.ToArray();
            }

            return orders;
        }

        /// <summary>
        /// 회차의 순서당번
        /// </summary>
        /// <param name="order">회차</param>
        /// <param name="isBonus">보너스 포함여부 (기본: 거짓)</param>
        /// <returns>당번 정수배열</returns>
        public static int[] SunDangOfOrder(int order, bool isBonus = false)
        {
            int[] orders = Array.Empty<int>();

            if (isBonus)
            {
                using var db = new LottoDBContext();
                var dangs = db.BasicTbl.Where(x => x.Orders == order)
                    .Select(x => new List<int> { x.SunGu1, x.SunGu2, x.SunGu3, x.SunGu4, x.SunGu5, x.SunGu6, x.Bonus }).Single();

                orders = dangs.ToArray();
            }
            else
            {
                using var db = new LottoDBContext();
                var dangs = db.BasicTbl.Where(x => x.Orders == order)
                    .Select(x => new List<int> { x.SunGu1, x.SunGu2, x.SunGu3, x.SunGu4, x.SunGu5, x.SunGu6 }).Single();

                orders = dangs.ToArray();
            }

            return orders;
        }

        /// <summary>
        /// 주기에 해당하는 회차를 반환
        /// </summary>
        /// <param name="juki">주기간격</param>
        /// <param name="order">최종회차</param>
        /// <param name="count">데이터 갯수</param>
        /// <returns></returns>
        public static int[] OrderOfJukiInts(int juki, int order, int count)
        {
            var list = new List<int>();
            int start = order + 1 - juki;

            for (int i = start; i > 0; i -= juki)
            {
                list.Add(i);

                if (list.Count >= count)
                {
                    break;
                }
            }

            return list.AsEnumerable().Reverse().ToArray();
        }

        /// <summary>
        /// 다중 문자열 컬럼에서 인덱스의 전체 데이터
        /// </summary>
        /// <param name="tableName">테이블 명</param>
        /// <param name="columnName">문자열 컬럼명</param>
        /// <param name="index">찾고자 하는 인덱스</param>
        /// <returns>정수 리스트</returns>
        public static List<int> GetColumnIndexData(string tableName, string columnName, int index)
        {
            var list = new List<int>();
            try
            {
                string query = $"SELECT CONVERT(INT, SUBSTRING({columnName}, {index + 1}, 1)) FROM {tableName}";
                using var context = new LottoDBContext();
                var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int n = reader.GetInt32(0);
                    list.Add(n);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        /// <summary>
        /// 정수 컬럼에서 출수다음으로 나타난 출수와 갯수
        /// </summary>
        /// <param name="tableName">테이블명</param>
        /// <param name="columnName">정수 컬럼명</param>
        /// <param name="lastchul">검사 출수</param>
        /// <returns>정수튜플(출수, 갯수)</returns>
        public static List<(int chulnumber, int dupcount)> GetNextTupleOfColumn(string tableName, string columnName, int lastchul)
        {
            List<(int, int)> list = new();

            try
            {
                string query = $"SELECT {columnName} AS [번호], COUNT({columnName}) AS [출수] FROM {tableName} WHERE Orders IN " + 
                               $"(SELECT Orders+1 FROM {tableName} WHERE {columnName}={lastchul}) GROUP BY {columnName} ORDER BY {columnName}";
                using var context = new LottoDBContext();
                var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int n = reader.GetInt32(0);
                    int val = reader.GetInt32(1);
                    list.Add((n, val));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        /// <summary>
        /// 정수 컬럼에서 전체 출수의 중복갯수
        /// </summary>
        /// <param name="tableName">테이블명</param>
        /// <param name="columnName">정수 컬럼명</param>
        /// <returns></returns>
        public static List<(int chulnumber, int dupcount)> GetTupleOfColumnData(string tableName, string columnName)
        {
            List<(int, int)> list = new();

            try
            {
                string query = $"SELECT {columnName} AS [번호], COUNT({columnName}) AS [출수] FROM {tableName} "+ 
                               $"GROUP BY {columnName} ORDER BY {columnName}";
                using var context = new LottoDBContext();
                var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int n = reader.GetInt32(0);
                    int val = reader.GetInt32(1);
                    list.Add((n, val));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

    }
}
