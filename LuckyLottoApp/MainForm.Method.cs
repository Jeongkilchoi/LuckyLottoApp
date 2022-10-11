using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using LuckyLottoApp.Geomsas;
using LuckyLottoLibrary;
using Util = LuckyLottoApp.Utilities.Utility;

namespace LuckyLottoApp
{

    public partial class MainForm
    {
        #region 필드
        private bool _dragging = false;
        private int _mousePosX;
        private int _mousePosY;
        private readonly int _lastOrder;
        private readonly CheckBox[] _conditionBoxes;
        private CancellationTokenSource _cts = null;
        private Dictionary<int, List<int>> _typePairs = new();
        private Dictionary<int, List<int[]>> _chulPairs = new();
        private Dictionary<int, List<(int[], int[])>> _sumPairs = new();
        #endregion

        #region 속성
        /// <summary>
        /// 제외수 리스트
        /// </summary>
        public List<int> ExceptList { get; set; } = new();
        /// <summary>
        /// 고정수 리스트
        /// </summary>
        public List<int> FixedList { get; set; } = new();
        #endregion

        /// <summary>
        /// 1-45 사이의 임의번호 6개
        /// </summary>
        /// <returns>정수배열</returns>
        private int[] CreateDangbeon()
        {
            int[] sequence = Enumerable.Range(1, 45).ToArray();
            int[] indexs = Enumerable.Range(0, 6).ToArray();
            var remindlist = Util.Except(sequence, FixedList, ExceptList);

            if (remindlist.Count() <= 6)
            {
                throw new Exception("제외하고 남은 갯수가 6개 이하입니다.");
            }

            var array = remindlist.OrderBy(x => Util.Next()).Take(6).ToArray();

            if (FixedList.Any())
            {
                var shuffle = Util.Shuffle(indexs).ToList();

                for (int i = 0; i < FixedList.Count; i++)
                {
                    int num = FixedList[i];
                    int idx = shuffle[i];
                    array[idx] = num;
                }
            }

            return array;
        }

        /// <summary>
        /// 1-45 사이의 임의번호 6개
        /// </summary>
        /// <param name="random">고급난수</param>
        /// <returns>정수배열</returns>
        /// <exception cref="Exception"></exception>
        private int[] CreateDangbeon(RandomNumberGenerator random)
        {
            int[] sequence = Enumerable.Range(1, 45).ToArray();
            int[] indexs = Enumerable.Range(0, 6).ToArray();
            var remindlist = Util.Except(sequence, FixedList, ExceptList);

            if (remindlist.Count() <= 6)
            {
                throw new Exception("제외하고 남은 갯수가 6개 이하입니다.");
            }

            var array = remindlist.OrderBy(x => Util.Next(random)).Take(6).ToArray();

            if (FixedList.Any())
            {
                var shuffle = Util.Shuffle(random, indexs).ToList();

                for (int i = 0; i < FixedList.Count; i++)
                {
                    int num = FixedList[i];
                    int idx = shuffle[i];
                    array[idx] = num;
                }
            }

            return array;
        }

        /// <summary>
        /// 1-45 사이의 임의번호 6개
        /// </summary>
        /// <param name="random">고급난수</param>
        /// <param name="selectedGoodList">호번리스트</param>
        /// <returns>정수배열</returns>
        /// <exception cref="Exception"></exception>
        private int[] CreateDangbeon(RandomNumberGenerator random, List<int> selectedGoodList)
        {
            int[] sequence = Enumerable.Range(1, 45).ToArray();
            int[] indexs = Enumerable.Range(0, 6).ToArray();

            if (!selectedGoodList.Any())
            {
                throw new Exception("선택한 호번리스트가 없음.");
            }

            int goodnum = selectedGoodList.OrderBy(x => Util.Next(random, 0, 256)).Take(1).Single();
            var fixlist = new List<int>(FixedList)
            {
                goodnum
            };

            fixlist = fixlist.Distinct().ToList();
            var remindlist = Util.Except(sequence, fixlist, ExceptList);

            if (remindlist.Count() <= 6 || fixlist.Count >= 6)
            {
                throw new Exception("제외 혹은 선택 오류.");
            }

            var array = remindlist.OrderBy(x => Util.Next(random, 0, 256)).Take(6).ToArray();

            if (fixlist.Any())
            {
                var shuffle = Util.Shuffle(indexs).ToList();

                for (int i = 0; i < fixlist.Count; i++)
                {
                    int num = fixlist[i];
                    int idx = shuffle[i];
                    array[idx] = num;
                }
            }

            return array;
        }

        /// <summary>
        /// 검사세트의 당번리스트
        /// </summary>
        /// <param name="count">1세트 갯수</param>
        /// <returns>배열 리스트</returns>
        private List<int[]> MakeSetDangbeon(int count, IProgress<int> progress, CancellationToken token)
        {
            var lists = new ConcurrentBag<int[]>();
            using var random = RandomNumberGenerator.Create();

            Parallel.For(0, count, (i, state) =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        state.Stop();
                        break;
                    }

                    int[] array = CreateDangbeon(random);

                    if (IsContainsData(lists, array))
                    {
                        lists.Add(array);
                        //button3.Invoke(new Action(() =>
                        //{
                        //    string s = string.Format("{0:#,0}", lists.Count);
                        //    button3.Text = s;
                        //}));
                        progress?.Report(lists.Count);
                        //pictureBox1.Invalidate();
                        break;
                    }
                }
            });

            return lists.ToList();
        }

        /// <summary>
        /// 출수검사 통과한 당번데이터
        /// </summary>
        /// <param name="count">검사 총갯수</param>
        /// <param name="progress">프로그레스</param>
        /// <param name="token">토큰</param>
        /// <returns>컨커렌트백(당번배열)</returns>
        private async Task<ConcurrentBag<int[]>> MakeOfPassedBasicConditon(int count, IProgress<int> progress, CancellationToken token)
        {
            var lists = new ConcurrentBag<int[]>();
            using var random = RandomNumberGenerator.Create();
            var gm = new Geomsa();
            var allfixdatas = await Task.Run(() => gm.CalculateOfFixData());

            var task = Task.Run(() =>
            {
                Parallel.For(0, count, (i, state) =>
                {
                    while (true)
                    {
                        if (token.IsCancellationRequested)
                        {
                            state.Stop();
                            break;
                        }

                        int[] array = PassedDangbeon(allfixdatas);

                        if (IsContainsData(lists, array))
                        {
                            lists.Add(array);
                            progress?.Report(lists.Count);
                            break;
                        }
                    }
                });
            });

            await task;
            return lists;
        }

        /// <summary>
        /// fixchulsu 데이터의 당번과 검사 통과한 당번
        /// </summary>
        /// <param name="collection">fixchulsu 데이터 컬렉션</param>
        /// <returns>당번배열</returns>
        private int[] PassedDangbeon(IEnumerable<(string, List<int[]>)> collection)
        {
            int[] array = new int[6];

            while (true)
            {
                var dang = CreateDangbeon();
                var pairs = new Dictionary<string, (int[], int)>();
                foreach (var tpls in collection)
                {
                    var chuls = tpls.Item2.Select(x => x.Intersect(dang).Count()).ToArray();
                    int type = Util.ConvertTypeIndex(chuls);

                    pairs.Add(tpls.Item1, (chuls, type));
                }

                var tpl = PassEnumers(pairs);

                if (tpl.All(x => x == true))
                {
                    dang.CopyTo(array, 0);
                    break;
                }
            }

            return array;
        }

        /// <summary>
        /// fixchulsu 데이터의 당번과 결과 결과
        /// </summary>
        /// <param name="pairs">fixchulsu 딕셔너리</param>
        /// <returns>컬렉션(부울)</returns>
        private IEnumerable<bool> PassEnumers(Dictionary<string, (int[], int)> pairs)
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                int error = 0;
                var val = pairs.ElementAt(i).Value.Item1;
                var tpl = pairs.ElementAt(i).Value.Item2;
                var chulsu = _chulPairs[i];
                var typesu = _typePairs[i];
                var sumsu = _sumPairs[i];

                if (!typesu.Contains(tpl))
                {
                    error++;
                    break;
                }

                for (int j = 0; j < chulsu.Count; j++)
                {
                    int[] chul = chulsu[j];

                    if (!chul.Contains(val[j]))
                    {
                        error++;
                        break;
                    }
                }

                foreach (var item in sumsu)
                {
                    int[] idx = item.Item1;
                    int sum = idx.Select(x => val[x]).Sum();
                    int[] vs = item.Item2;

                    if (!vs.Contains(sum))
                    {
                        error++;
                        break;
                    }
                }

                if (error > 0)
                {
                    yield return false;
                    break;
                }
                else
                {
                    yield return true;
                }
            }
        }

        /// <summary>
        /// 검사세트의 당번리스트
        /// </summary>
        /// <param name="count">1세트 갯수</param>
        /// <param name="selectedGoodList">호번리스트</param>
        /// <returns>배열 리스트</returns>
        private List<int[]> MakeSetDangbeon(int count, List<int> selectedGoodList)
        {
            var lists = new ConcurrentBag<int[]>();
            using var random = RandomNumberGenerator.Create();

            while (lists.Count <= count)
            {
                int[] array = CreateDangbeon(random, selectedGoodList);

                if (IsContainsData(lists, array))
                {
                    lists.Add(array);
                }
            }

            return lists.ToList();
        }

        /// <summary>
        /// 당번 중복여부
        /// </summary>
        /// <param name="target">중복검사 통과한 배열리스트</param>
        /// <param name="source">당번배열</param>
        /// <returns>중복된것이 없으면 참</returns>
        private bool IsContainsData(ConcurrentBag<int[]> target, int[] source)
        {
            bool pass = false;

            if (!target.Any())
            {
                pass = true;
            }
            else if (target.Count <= 10_000)
            {
                pass = target.All(x => x.Intersect(source).Count() != source.Length);
            }
            else
            {
                var currentBag = new ConcurrentBag<bool>();

                Parallel.ForEach(target, (array, state) =>
                {
                    int dup = array.Intersect(source).Count();

                    if (dup == source.Length)
                    {
                        currentBag.Add(false);
                        state.Stop();
                    }
                    else
                    {
                        currentBag.Add(true);
                    }
                });

                pass = currentBag.All(x => x);
            }

            return pass;
        }

        /// <summary>
        /// xml 설정을 검사조건 체크박스에 맞추기
        /// </summary>
        private void SettingCheckbox()
        {
            string path = Application.StartupPath + @"\DataFiles\condition.json";
            JObject jobj = JObject.Parse(File.ReadAllText(path));
            var pairs = jobj.ToObject<Dictionary<string, bool>>();

            foreach (var key in pairs.Keys)
            {
                var value = pairs[key];
                int idx = _conditionBoxes.Select((val, idx) => (val, idx)).Where(x => x.val.Name == key)
                                         .First().idx;

                _conditionBoxes[idx].Checked = value;
            }
        }

        //사용자 폼 생성이벤트
        private void ManualFormInitialized()
        {
            panel1.MouseUp += (s, e) => { _dragging = false; };
            panel1.MouseMove += (s, e) =>
            {
                if (_dragging)
                    SetDesktopLocation(Cursor.Position.X - _mousePosX, Cursor.Position.Y - _mousePosY);
            };
            panel1.MouseDown += (s, e) =>
            {
                _dragging = true;
                _mousePosX = e.X;
                _mousePosY = e.Y;
            };

            MouseUp += (s, e) => { _dragging = false; };
            MouseMove += (s, e) =>
            {
                if (_dragging)
                    this.SetDesktopLocation(Cursor.Position.X - _mousePosX, Cursor.Position.Y - _mousePosY);
            };
            MouseDown += (s, e) =>
            {
                _dragging = true;
                _mousePosX = e.X;
                _mousePosY = e.Y;
            };
        }

        /// <summary>
        /// chulsu, typesu, chulsum data read
        /// </summary>
        public void ReadCombine()
        {
            string chulsupath = Application.StartupPath + @"\DataFiles\chulsu.dat";
            string typesupath = Application.StartupPath + @"\DataFiles\typesu.dat";
            string chusumpath = Application.StartupPath + @"\DataFiles\chulsusum.dat";

            var chulsus = File.ReadAllLines(chulsupath);
            var typesus = File.ReadAllLines(typesupath);
            var chusums = File.ReadAllLines(chusumpath);

            for (int i = 0; i <= 81; i++)
            {
                string item = i + "/";

                var chul = chulsus.Where(x => x.StartsWith(item)).Single();
                var type = typesus.Where(x => x.StartsWith(item)).Single();
                var sums = chusums.Where(x => x.StartsWith(item));

                var typ = type[item.Length..];
                var chu = chul[item.Length..];

                _typePairs.Add(i, LuckyLibrary.StringToList(typ));
                var chusplit = chu.Split('/').Select(x => LuckyLibrary.StringToList(x).ToArray()).ToList();
                _chulPairs.Add(i, chusplit);

                var splits = new List<(int[], int[])>();
                foreach (string sum in sums)
                {
                    var sumsplit1 = LuckyLibrary.StringToList(sum.Split('/')[1]).ToArray();
                    var sumsplit2 = LuckyLibrary.StringToList(sum.Split('/')[2]).ToArray();
                    splits.Add((sumsplit1, sumsplit2));
                }
                _sumPairs.Add(i, splits);
            }
        }
    }
}
