using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyLottoLibrary;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace LuckyLottoApp.Forms
{
    public partial class ChulsuDisitalForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private int _term = 1;
        private int[] _sequenceInts;
        private List<(int order, int[] dangs)> _allDangbeon;
        private List<(string badgod, int beon, int term, int real, int max, int same, int count)> _values;
        #endregion

        public ChulsuDisitalForm(int lastOrder)
        {
            InitializeComponent();
            _lastOrder = lastOrder;
            int[] gapInts = LuckyLibrary.SeqenceGapInts;
            gapInts.ToList().ForEach(x => SequenceComboBox.Items.Add(x));
        }

        private async void ChulsuDisitalForm_Load(object sender, EventArgs e)
        {
            _allDangbeon = await GetAllDangbeon();
            SequenceComboBox.SelectedIndex = 0;
        }

        private void SequenceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SequenceComboBox.SelectedIndex > -1)
            {
                int sel = Convert.ToInt32(SequenceComboBox.SelectedItem);
                _sequenceInts = LuckyLibrary.NewSequenceLists()[sel];
                PresentListView(_sequenceInts);
            }
        }

        private void TermNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _term = (int)TermNumericUpDown.Value;
            PresentListView(_sequenceInts);
        }

        private void TermButton_Click(object sender, EventArgs e)
        {
            QueryTextBox.Text = string.Empty;
            _values = new();
            var enumers = Enumerable.Range(1, 45);
            var rst = new List<(string badgod, int beon, int term, int real, int max, int same, int count)>();
            for (int i = 1; i <= 100; i++)
            {
                var termords = GetTermOrder(i);
                var alldatas = _allDangbeon.Where(x => termords.Contains(x.order)).Select(x => x.dangs);
                var dotdatas = alldatas.Select(x => enumers.Select(g => x.Contains(g) ? 1 : 0).ToArray());
                //pivot
                for (int j = 0; j < 45; j++)
                {
                    var eacharr = dotdatas.Select(x => x[j]).ToArray();
                    var (realCount, maxCount) = NextReal.RealMaxCount(eacharr);
                    if (realCount >= maxCount && maxCount > -1)
                    {
                        if (eacharr[^1] == 1)
                        {
                            if (realCount >= 2)
                            {
                                int cnt = eacharr.Count(x => x == 1);
                                rst.Add(("악번", j + 1, i, realCount, maxCount, cnt, eacharr.Length));
                            }
                        }
                        else
                        {
                            int cnt = eacharr.Count(x => x == 0);
                            double d = cnt / (double)eacharr.Length;

                            if (cnt > 2 && d < 0.7)
                            {
                                rst.Add(("호번", j + 1, i, realCount, maxCount, cnt, eacharr.Length));
                            }
                        }
                    }
                }
            }

            if (rst.Any())
            {
                //번호순으로 정렬 다음에 회기 순으로 정렬
                var ordedlist = rst.OrderBy(x => x.beon).ThenBy(g => g.term).ThenBy(v => v.count);
                _values.AddRange(ordedlist);
                var result = new List<string>();
                foreach (var (badgod, beon, term, real, max, same, count) in ordedlist)
                {
                    var s1 = $"{badgod}\t번호:{beon}\t회기:{term}\t후방:{real}\t최대:{max}\t동출:{same}\t갯수:{count}";
                    result.Add(s1);
                }
                QueryTextBox.Text = string.Join(Environment.NewLine, result);
                FindNumericUpDown.Enabled = true;
            }
        }

        private void FindNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int num = (int)FindNumericUpDown.Value;
            if (_values.Any())
            {
                QueryTextBox.Text = string.Empty;
                string s = string.Empty;
                var selist = _values.Where(x => x.beon == num).OrderBy(g => g.badgod);
                if (selist.Any())
                {
                    foreach (var (badgod, beon, term, real, max, same, count) in selist)
                    {
                        var s1 = $"{badgod}\t번호:{beon}\t회기:{term}\t후방:{real}\t최대:{max}\t동출:{same}\t갯수:{count}";
                        s += s1 + Environment.NewLine;
                    }
                }
                QueryTextBox.Text = s;
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            var pairs = LuckyLibrary.NewSequenceLists();
            var alldangs = _allDangbeon.Select(x => x.dangs);
            string[] cases = { "1000", "0001", "1001", "0000" };
            var result = new List<string[]>();

            foreach (int key in pairs.Keys)
            {
                int[] seqarray = pairs[key];
                var table = MakeTable(seqarray);

                string[] colname = seqarray.Select(x => $"c{x:00}").ToArray();
                int[] lastdata = table.AsEnumerable().Where(x => x.Field<int>("Order") == _lastOrder).First()
                    .ItemArray.Skip(1).Select(x => Convert.ToInt32(x)).ToArray();
                int[] idxs = lastdata.Select((val, idx) => (val, idx)).Where(x => x.val == 1).Select(x => x.idx).ToArray();
                
                var lists = new List<(int[], string[])>();
                if (lastdata[0] == 1 && lastdata[^1] == 1)
                {
                    foreach (string item in cases)
                    {
                        for (int i = 0; i < idxs.Length - 1; i++)
                        {
                            if (item == cases[0])
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                            else if (item == cases[1])
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                            else if (item == cases[2])
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                            else
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                        }
                    }
                }
                else if (lastdata[0] == 1)
                {
                    foreach (string item in cases)
                    {
                        if (item == cases[0])
                        {
                            var tmp = new List<(int[], string[])>();
                            //1000
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }
                            Range rng = idxs[^1]..;
                            int[] arr = lastdata[rng];
                            if (arr.Length >= 2 && arr[0] == 1 && arr[^1] == 0)
                            {
                                string[] nam = colname[rng];
                                tmp.Add((arr, nam));
                            }
                            lists.AddRange(tmp);
                        }
                        else if (item == cases[1])
                        {
                            //0001
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                        }
                        else if (item == cases[2])
                        {
                            //1001
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                        }
                        else
                        {
                            var tmp = new List<(int[], string[])>();
                            //0000
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }
                            int a1 = idxs[^1] + 1;
                            Range rng = a1..;
                            int[] arr = lastdata[rng];
                            if (arr.Length >= 2 && arr[0] == 0 && arr[^1] == 0)
                            {
                                string[] nam = colname[rng];
                                tmp.Add((arr, nam));
                            }
                            lists.AddRange(tmp);
                        }
                    }
                }
                else if (lastdata[^1] == 1)
                {
                    foreach (string item in cases)
                    {
                        if (item == cases[0])
                        {
                            //1000
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                        }
                        else if (item == cases[1])
                        {
                            var tmp = new List<(int[], string[])>();
                            //0001
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }
                            int b1 = idxs[0] + 1;
                            Range rng = 0..b1;
                            int[] arr = lastdata[rng];
                            if (arr.Length >= 2 && arr[0] == 0 && arr[^1] == 1)
                            {
                                string[] nam = colname[rng];
                                tmp.Insert(0, (arr, nam));
                            }
                            lists.AddRange(tmp);
                        }
                        else if (item == cases[2])
                        {
                            //1001
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                        }
                        else
                        {
                            var tmp = new List<(int[], string[])>();
                            //0000
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }
                            int a1 = idxs[0];
                            Range rng = 0..a1;
                            int[] arr = lastdata[rng];
                            if (arr.Length >= 2 && arr[0] == 0 && arr[^1] == 0)
                            {
                                string[] nam = colname[rng];
                                tmp.Insert(0, (arr, nam));
                            }
                            lists.AddRange(tmp);
                        }
                    }
                }
                else
                {
                    foreach (string item in cases)
                    {
                        if (item == cases[0])
                        {
                            var tmp = new List<(int[], string[])>();
                            //1000
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }
                            Range rng = idxs[^1]..;
                            int[] arr = lastdata[rng];
                            if (arr.Length >= 2 && arr[0] == 1 && arr[^1] == 0)
                            {
                                string[] nam = colname[rng];
                                tmp.Add((arr, nam));
                            }
                            lists.AddRange(tmp);
                        }
                        else if (item == cases[1])
                        {
                            var tmp = new List<(int[], string[])>();
                            //0001
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }
                            int b1 = idxs[0] + 1;
                            Range rng = ..b1;
                            int[] arr = lastdata[rng];
                            if (arr.Length >= 2 && arr[0] == 0 && arr[^1] == 1)
                            {
                                string[] nam = colname[rng];
                                tmp.Insert(0, (arr, nam));
                            }
                            lists.AddRange(tmp);
                        }
                        else if (item == cases[2])
                        {
                            //1001
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i];
                                int b = idxs[i + 1] + 1;
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 1 && array1[^1] == 1)
                                {
                                    string[] name1 = colname[range1];
                                    lists.Add((array1, name1));
                                }
                            }
                        }
                        else
                        {
                            var tmp = new List<(int[], string[])>();
                            //0000
                            for (int i = 0; i < idxs.Length - 1; i++)
                            {
                                int a = idxs[i] + 1;
                                int b = idxs[i + 1];
                                Range range1 = new(a, b);
                                int[] array1 = lastdata[range1];
                                if (array1.Length >= 2 && array1[0] == 0 && array1[^1] == 0)
                                {
                                    string[] name1 = colname[range1];
                                    tmp.Add((array1, name1));
                                }
                            }

                            int a1 = idxs[0];
                            Range rng1 = 0..a1;
                            int[] arr1 = lastdata[rng1];
                            if (arr1.Length >= 2 && arr1[0] == 0 && arr1[^1] == 0)
                            {
                                string[] nam = colname[rng1];
                                tmp.Insert(0, ((arr1, nam)));
                            }

                            int b1 = idxs[^1] + 1;
                            Range rng2 = b1..;
                            int[] arr2 = lastdata[rng2];
                            if (arr2.Length >= 2 && arr2[0] == 0 && arr2[^1] == 0)
                            {
                                string[] nam = colname[rng2];
                                tmp.Add((arr2, nam));
                            }
                            lists.AddRange(tmp);
                        }
                    }
                }

                var rst = new List<string>();

                foreach (var tpl in lists)
                {
                    var zips = OrderedQueryString(tpl);
                    string query = string.Join(" AND ", zips);
                    var tuple = CheckedTableData(table, query);

                    if (tuple.same > _lastOrder * 0.02)
                    {
                        if (tuple.real > tuple.max)
                        {
                            if (IsContainsItem(result, zips))
                            {
                                result.Add(zips);
                            }
                        }
                    }
                    else if (tuple.same > 0)
                    {
                        if (tuple.real >= tuple.max)
                        {
                            if (IsContainsItem(result, zips))
                            {
                                result.Add(zips);
                            }
                        }
                    }
                    else
                    {
                        if (IsContainsItem(result, zips))
                        {
                            result.Add(zips);
                        }
                    }
                }
            }

            var changes = result.Select(x => string.Join(",", x));
            string s = string.Join(Environment.NewLine, changes);
            QueryTextBox.Text = s;
            SelectedButton.Enabled = true;
        }

        private void SelectedButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(QueryTextBox.Text))
            {
                QueryTextBox.SelectAll();
                QueryTextBox.Focus();
            }
        }




        //********************  메서드  *********************




        /// <summary>
        /// 출수배열과 열이름배열 섞은 문장배열
        /// </summary>
        /// <param name="tpl">튜플(출수배열, 열이름배열)</param>
        /// <returns>문장배열</returns>
        private static string[] OrderedQueryString((int[], string[]) tpl)
        {
            Dictionary<string, int> pairs = new Dictionary<string, int>();

            for (int i = 0; i < tpl.Item1.Length; i++)
            {
                int n = tpl.Item1[i];
                string s = tpl.Item2[i];
                pairs.Add(s, n);
            }

            var ordpairs = pairs.OrderBy(x => x.Key).ToDictionary(g => g.Key, g => g.Value);
            var zips = ordpairs.Keys.Zip(ordpairs.Values, (key, val) => $"{key}={val}");

            return zips.ToArray();
        }

        /// <summary>
        /// 쿼리검사 결과
        /// </summary>
        /// <param name="table">데이터 테이블</param>
        /// <param name="query">쿼리문</param>
        /// <returns>튜플(동출수, 후방연속, 연속최대)</returns>
        private (int same, int real, int max) CheckedTableData(DataTable table, string query)
        {
            int samecount = 0, realcount = 0, maxcount = 0;
            var queryresults = table.Select(query);

            if (queryresults.Any())
            {
                samecount = queryresults.Length;
                var ords = queryresults.AsEnumerable().Select(x => x.Field<int>("Order")).ToArray();
                var tpl = RealMaxCount(ords);
                realcount = tpl.r;
                maxcount = tpl.m;
            }

            return (samecount, realcount, maxcount);
        }

        /// <summary>
        /// 회차배열에서 연속갯수 찾기
        /// </summary>
        /// <param name="ords">오름차순 회차배열</param>
        /// <returns>튜플(후방연속, 연속최대)</returns>
        private (int r, int m) RealMaxCount(int[] ords)
        {
            //연이어 나온갯수로 후방연속 갯수와 연속최대 갯수 찾기
            //최종회차는 무조건 나옴
            int real = 1;

            var revInts = Enumerable.Reverse(ords).ToArray();
            for (int i = 0; i < revInts.Length - 1; i++)
            {
                int a = revInts[i];
                int b = revInts[i + 1];
                if (Math.Abs(a - b) == 1)
                    real++;
                else
                    break;
            }

            int max = 0, dup = 0; ;

            foreach (int ord in ords)
            {
                for (int i = 0; i < _lastOrder - 1; i++)
                {
                    int n = ord + i;
                    if (ords.Contains(n))
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
                        break;
                    }
                }
            }

            if (dup > max)
            {
                max = dup;
            }

            return (real, max);
        }

        /// <summary>
        /// 이미 포함되었다면 거짓
        /// </summary>
        /// <param name="targets">통과 모음 배열리스트</param>
        /// <param name="source">검사할 배열리스트</param>
        /// <returns></returns>
        private static bool IsContainsItem(List<string[]> targets, string[] source)
        {
            bool pass = false;

            if (targets.Any())
            {
                var sames = targets.Where(x => x.Length == source.Length);

                if (sames.Any())
                    pass = sames.All(x => !x.SequenceEqual(source));
                else
                    pass = true;
            }
            else
            {
                pass = true;
            }

            return pass;
        }

        /// <summary>
        /// 테이블 생성
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns>데이터 테이블</returns>
        private DataTable MakeTable(int[] sequence)
        {
            using var table = new DataTable();
            table.Columns.Add("Order", typeof(int));

            foreach (int item in sequence)
            {
                string s = $"c{item:00}";
                table.Columns.Add(s, typeof(int));
            }

            foreach (var (order, dangs) in _allDangbeon)
            {
                int ord = order;
                int[] dang = dangs;

                //출수가 나온 인덱스
                var array = sequence.Select(x => dang.Contains(x) ? 1 : 0).ToArray();

                DataRow row = table.NewRow();
                row[0] = ord;
                for (int i = 0; i < array.Length; i++)
                {
                    row[i + 1] = array[i];
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 당번의 전체 데이터
        /// </summary>
        /// <returns></returns>
        private async static Task<List<(int, int[])>> GetAllDangbeon()
        {
            var lists = new List<(int, int[])>();

            var task = Task.Run(() =>
            {
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                string query = "SELECT Orders, Gu1, Gu2, Gu3, Gu4, Gu5, Gu6 FROM BasicTbl";
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int n = reader.GetInt32(0);
                    var array = new int[6];
                    for (int i = 1; i < reader.FieldCount; i++)
                    {
                        array[i - 1] = reader.GetInt32(i);
                    }
                    lists.Add((n, array));
                }
            });
            await task;

            return lists;
        }

        /// <summary>
        /// 리스트뷰에 도트형식 출력
        /// </summary>
        /// <param name="sequenceInts"></param>
        /// <param name="section"></param>
        private void PresentListView(int[] sequenceInts)
        {
            listView1.Clear();

            listView1.Columns.Add("회차", 45, HorizontalAlignment.Left);
            foreach (int num in sequenceInts)
            {
                listView1.Columns.Add(num.ToString(), 26, HorizontalAlignment.Center);
            }

            //회기에 따른 전체 데이터
            var termorders = GetTermOrder();
            var termdangs =_allDangbeon.Where(x => termorders.Contains(x.order));

            if (termdangs.Any())
            {
                listView1.BeginUpdate();

                foreach (var (order, dangs) in termdangs)
                {
                    int ord = order;
                    int[] arr = dangs;

                    var lvi = new ListViewItem(ord.ToString());
                    foreach (int n in sequenceInts)
                    {
                        string s = arr.Contains(n) ? "●" : "";
                        lvi.SubItems.Add(s);
                    }

                    listView1.Items.Add(lvi);
                }
                var enumers = Enumerable.Range(1, 45);
                var seclist = termdangs.Select(x => enumers.Select(g => x.dangs.Contains(g) ? 1 : 0).ToArray()).ToList();
                var lvi1 = new ListViewItem((_lastOrder + 1).ToString());
                for (int i = 0; i < 45; i++)
                {
                    int realcnt = 0;
                    var eaInts = seclist.Select(x => x[i]).ToArray();

                    if (eaInts[^1] == 1)
                    {
                        var (real, max) = NextReal.RealMaxCount(eaInts);

                        if (real >= max)
                        {
                            realcnt = real;
                        }
                    }

                    string s1 = (realcnt == 0) ? string.Empty : realcnt.ToString();
                    lvi1.SubItems.Add(s1);
                }
                listView1.Items.Add(lvi1);

                listView1.EndUpdate();
                listView1.EnsureVisible(termdangs.Count());
            }
        }

        /// <summary>
        /// 회기 간격의 회차
        /// </summary>
        /// <returns>회차 배열</returns>
        private int[] GetTermOrder()
        {
            var list = new List<int>();
            int start = _lastOrder - _term + 1;

            for (int i = start; i > 0; i -= _term)
            {
                list.Add(i);
            }

            list.Sort();
            return list.ToArray();
        }

        /// <summary>
        /// 회기 간격의 회차
        /// </summary>
        /// <param name="term">회기간격</param>
        /// <returns>회차 배열</returns>
        private int[] GetTermOrder(int term)
        {
            var list = new List<int>();
            int start = _lastOrder - term + 1;

            for (int i = start; i > 0; i -= term)
            {
                list.Add(i);
            }

            list.Sort();
            return list.ToArray();
        }


    }
}
