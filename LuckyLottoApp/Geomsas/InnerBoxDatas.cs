using LuckyLottoApp.Utilities;

namespace LuckyLottoApp.Geomsas
{
    internal class InnerBoxDatas
    {
        private IEnumerable<int[]> _collection;
        private readonly int[][] _indexDatas = new int[][] { };

        /// <summary>
        /// 생성자
        /// </summary>
        public InnerBoxDatas(IEnumerable<int[]> collection)
        {
            _collection = collection;
            int row = collection.Count();
            int col = collection.First().Length;

            int[] rowdivs = { 5, 7, 9 };
            int[] coldivs = { 9, 7, 5 };

            if (!rowdivs.Contains(row) || !coldivs.Contains(col))
            {
                throw new Exception("검사할 수 없습니다.");
            }

            var originIndexs = new int[row][];
            for (int i = 0; i < row; i++)
            {
                int[] arr = new int[col];
                for (int j = 0; j < col; j++)
                {
                    int n = i * col + j;
                    arr[j] = n;
                }
                originIndexs[i] = arr;
            }

            _indexDatas = originIndexs;

            //가로 3연속 //빈칸은 0처리
            var horcontinues = new List<int[]>();
            foreach (var item in originIndexs)
            {
                var johap = Utility.GetCombinations(item, 3).Select(x => x.ToArray());
                horcontinues.AddRange(johap);
            }
            BoxPairs.Add("가로연속", horcontinues);

            //세로 3연속
            var vercontinues = new List<int[]>();
            for (int i = 0; i < col; i++)
            {
                var each = originIndexs.Select(x => x.ElementAt(i));
                var johap = Utility.GetCombinations(each, 3).Select(x => x.ToArray());
                vercontinues.AddRange(johap);
            }
            BoxPairs.Add("세로연속", vercontinues);

            //정슬래쉬
            var rfslashs = new List<int[]>();
            for (int i = 2; i < row; i++)
            {
                for (int j = 0; j < col - 2; j++)
                {
                    var list = new List<int>();
                    for (int x = j, y = i; ; x++, y--)
                    {
                        int n = originIndexs[y][x];
                        list.Add(n);
                        if (x == col - 1 || y == 0)
                        {
                            break;
                        }
                    }

                    if (list.Count >= 3)
                    {
                        var johap = Utility.GetCombinations(list, 3).Select(x => x.ToArray());
                        rfslashs.AddRange(johap);
                    }
                }
            }
            BoxPairs.Add("정상사선", rfslashs);

            //역슬래쉬
            var lfslashs = new List<int[]>();
            for (int i = 2; i < row; i++)
            {
                for (int j = col - 1; j >= 0; j--)
                {
                    var list = new List<int>();
                    for (int x = j, y = i; ; x--, y--)
                    {
                        int n = originIndexs[y][x];
                        list.Add(n);
                        if (x == 0 || y == 0)
                        {
                            break;
                        }
                    }

                    if (list.Count >= 3)
                    {
                        var johap = Utility.GetCombinations(list, 3).Select(x => x.ToArray());
                        lfslashs.AddRange(johap);
                    }
                }
            }
            BoxPairs.Add("역순사선", lfslashs);

            //정기역
            var regkiyeok = new List<int[]>();
            for (int i = 0; i < row - 1; i++)
            {
                var each = originIndexs[i];
                var johaps = Utility.GetCombinations(each, 2).Select(x => x.ToArray());
                foreach (var item in johaps)
                {
                    var list = new List<int>();
                    list.AddRange(item);
                    int n = item[1] + col;
                    list.Add(n);
                    regkiyeok.Add(list.ToArray());
                }
            }
            BoxPairs.Add("정상기역", regkiyeok);

            //역기역
            var revkiyeok = new List<int[]>();
            for (int i = 0; i < row - 1; i++)
            {
                var each = originIndexs[i];
                var johaps = Utility.GetCombinations(each, 2).Select(x => x.ToArray());
                foreach (var item in johaps)
                {
                    var list = new List<int>();
                    list.AddRange(item);
                    int n = item[0] + col;
                    list.Add(n);
                    revkiyeok.Add(list.ToArray());
                }
            }
            BoxPairs.Add("역순기역", revkiyeok);

            //정니은
            var regneun = new List<int[]>();
            for (int i = 1; i < row; i++)
            {
                var each = originIndexs[i];
                var johaps = Utility.GetCombinations(each, 2).Select(x => x.ToArray());
                foreach (var item in johaps)
                {
                    var list = new List<int>();
                    list.AddRange(item);
                    int n = item[0] - col;
                    list.Add(n);
                    regneun.Add(list.ToArray());
                }
            }
            BoxPairs.Add("정상니은", regneun);

            //역니은
            var revneun = new List<int[]>();
            for (int i = 1; i < row; i++)
            {
                var each = originIndexs[i];
                var johaps = Utility.GetCombinations(each, 2).Select(x => x.ToArray());
                foreach (var item in johaps)
                {
                    var list = new List<int>();
                    list.AddRange(item);
                    int n = item[1] - col;
                    list.Add(n);
                    revneun.Add(list.ToArray());
                }
            }
            BoxPairs.Add("역순니은", revneun);

            //상방꺽쇠
            var upkkeyk = new List<int[]>();
            for (int i = 0; i < row - 1; i++)
            {
                var range = new Range(1, col - 1);
                var each = originIndexs[i][range];
                foreach (var item in each)
                {
                    var list = new List<int> { item };
                    int n1 = item + (col - 1);
                    int n2 = item + (col + 1);
                    list.Add(n1);
                    list.Add(n2);
                    upkkeyk.Add(list.ToArray());
                }
            }
            BoxPairs.Add("상방꺽쇠", upkkeyk);

            //하방꺽쇠
            var dnkkeyk = new List<int[]>();
            for (int i = 1; i < row; i++)
            {
                var range = new Range(1, col - 1);
                var each = originIndexs[i][range];
                foreach (var item in each)
                {
                    var list = new List<int> { item };
                    int n1 = item - (col + 1);
                    int n2 = item - (col - 1);
                    list.Add(n1);
                    list.Add(n2);
                    dnkkeyk.Add(list.ToArray());
                }
            }
            BoxPairs.Add("하방꺽쇠", dnkkeyk);

            //좌측꺽쇠
            var lfkkeyk = new List<int[]>();
            for (int i = 1; i < row - 1; i++)
            {
                var range = new Range(0, col - 1);
                var each = originIndexs[i][range];
                foreach (var item in each)
                {
                    var list = new List<int> { item };
                    int n1 = item - (col - 1);
                    int n2 = item + (col + 1);
                    list.Add(n1);
                    list.Add(n2);
                    lfkkeyk.Add(list.ToArray());
                }
            }
            BoxPairs.Add("좌측꺽쇠", lfkkeyk);

            //우측꺽쇠
            var rfkkeyk = new List<int[]>();
            for (int i = 1; i < row - 1; i++)
            {
                var range = new Range(1, col);
                var each = originIndexs[i][range];
                foreach (var item in each)
                {
                    var list = new List<int> { item };
                    int n1 = item - (col + 1);
                    int n2 = item + (col - 1);
                    list.Add(n1);
                    list.Add(n2);
                    rfkkeyk.Add(list.ToArray());
                }
            }
            BoxPairs.Add("우측꺽쇠", rfkkeyk);
        }

        /// <summary>
        /// 내부상자의 전체인덱스 딕셔너리
        /// </summary>
        public Dictionary<string, List<int[]>> BoxPairs { get; set; } = new();

        public Dictionary<string, List<int[]>> BoxDataPairs()
        {
            var pairs = new Dictionary<string, List<int[]>>();
            var collection = _collection.ToArray();

            foreach (var key in BoxPairs.Keys)
            {
                var item = BoxPairs[key];
                var each = new List<int[]>();
                foreach (int[] array in item)
                {
                    var ea = new List<int>();
                    foreach (int index in array)
                    {
                        int y = _indexDatas.Select((val, idx) => (val, idx)).Where(x => x.val.Contains(index)).Select(x => x.idx).Single();
                        int x = Array.IndexOf(_indexDatas[y], index);
                        int n = collection[y][x];

                        if (n != 0)
                        {
                            ea.Add(n);
                        }
                    }

                    if (ea.Any() && ea.Count == 3)
                    {
                        each.Add(ea.ToArray());
                    }
                }

                pairs.Add(key, each);
            }

            return pairs;
        }
    }
}
