using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 출수위치의 행별위치로 검사하는 폼 클래스
    /// </summary>
    public partial class HaengByeolForm : Form
    {
        #region 필드

        private readonly int _lastOrder;
        private readonly List<string> _colNames = new();
        private List<int> _colValues;
        private List<int[]> _allDatas;
        private List<int> _numberlastCounts;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public HaengByeolForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
            Enumerable.Range(1, 45).ToList().ForEach(x => _colNames.Add("c" + x));
        }

        private async void HaengByeolForm_Load(object sender, EventArgs e)
        {
            listView1.Enabled = false;
            ExecuteButton.Enabled = false;

            await PresentListview();

            listView1.Enabled = true;
            ExecuteButton.Enabled = true;
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            ResultTextBox.Text = string.Empty;
            SelectTextBox.Text = string.Empty;

            var lasts = _allDatas.Last().Select((val, idx) => (val, idx))
                                 .Where(x => x.val != 0).Select(x => x.idx);
            var list = new List<string>();
            var sumlist = new List<int>();
            if (UpdwRadioButton.Checked)
            {
                foreach (var item in lasts)
                {
                    int val = item + (_colValues.Min());
                    var findlist = FindLastExceptNumber(val);
                    if (findlist.Any())
                    {
                        string s = val + ":\t" + string.Join(",", findlist.Select(x => x.ToString("00")));
                        list.Add(s);
                        sumlist.AddRange(findlist);
                    }
                }
            }
            else
            {
                int limit = (int)LimitNumUpDown.Value;
                foreach (int item in lasts)
                {
                    int val = item + (_colValues.Min());
                    int lsd = _allDatas.Count - 1;
                    var leftns = new List<int>();
                    var rightns = new List<int>();

                    //사선으로 3이상 나오면 악번
                    for (int i = 1; i < limit; i++)
                    {
                        int ln, rn;

                        try
                        {
                            ln = _allDatas[lsd - i][item - i];
                            //rn = _allDatas[lsd + i][item - i];
                        }
                        catch
                        {
                            ln = 0;
                        }

                        try
                        {
                            rn = _allDatas[lsd + i][item - i];
                        }
                        catch
                        {
                            rn = 0;
                        }
                        leftns.Add(ln);
                        rightns.Add(rn);
                    }

                    bool leftpass = leftns.All(x => x != 0);

                    if (leftpass)
                    {
                        var findlist = FindLastExceptNumber(val + 1);
                        if (findlist.Any())
                        {
                            string s = (val + 1) + ":\t" + string.Join(",", findlist.Select(x => x.ToString("00")));
                            list.Add(s);
                            sumlist.AddRange(findlist);
                        }
                    }

                    bool rightpass = rightns.All(x => x != 0);

                    if (rightpass)
                    {
                        var findlist = FindLastExceptNumber(val - 1);
                        if (findlist.Any())
                        {
                            string s = (val - 1) + ":\t" + string.Join(",", findlist.Select(x => x.ToString("00")));
                            list.Add(s);
                            sumlist.AddRange(findlist);
                        }
                    }
                }
            }

            ResultTextBox.Text = string.Join(Environment.NewLine, list) + "\r\n\r\n" + 
                                 string.Join(",", sumlist.OrderBy(x => x).Select(x => x.ToString("00")));
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //열인덱스 + 열이름 최소 - 1
            int val = e.Column + (_colValues.Min() - 1);
            var findlist = FindLastExceptNumber(val);
            SelectTextBox.Text = String.Join(",", findlist);
        }




        //*********************  내부메서드  ********************



        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        /// <returns></returns>
        private async Task PresentListview()
        {
            listView1.Clear();
            ResultTextBox.Text = "";
            SelectTextBox.Text = "";

            int start = _lastOrder - 200;   //200회차 구간이면 충분하게 행별 크기를 찾을 수 있음
            var find = await Task.Run(FindAllMaxCount);
            _numberlastCounts = new List<int>(find);
            var lists = new List<(int, int[])>();
            for (int i = start; i <= _lastOrder; i++)
            {
                var array = await Task.Run(() => GetLastRowCountInts(i));
                lists.Add((i, array));
            }

            int min = lists.SelectMany(x => x.Item2).Min();
            int max = lists.SelectMany(x => x.Item2).Max();
            int length = max - min;
            listView1.Columns.Add("회차", 45, HorizontalAlignment.Left);
            _colValues = new List<int>();
            _allDatas = new List<int[]>();
            for (int i = min; i <= max; i++)
            {
                _colValues.Add(i);
                string s = i.ToString();
                listView1.Columns.Add(s, 35, HorizontalAlignment.Center);
            }

            var limits = lists.Skip(lists.Count - length);
            listView1.BeginUpdate();

            foreach (var item in limits)
            {
                var lvi = new ListViewItem(item.Item1.ToString()) { UseItemStyleForSubItems = false };
                var values = item.Item2;
                int[] colInts = new int[_colValues.Count];
                for (int i = 0; i < _colValues.Count; i++)
                {
                    int n = _colValues[i];
                    if (values.Contains(n))
                    {
                        int cnt = values.Count(x => x == n);
                        lvi.SubItems.Add(cnt.ToString());
                        colInts[i] = cnt;
                        if (item.Item1 == _lastOrder)
                        {
                            lvi.BackColor = Color.Cyan;
                            lvi.SubItems[i + 1].BackColor = Color.Cyan;
                        }
                        else
                        {
                            lvi.SubItems[i + 1].BackColor = Color.LightCyan;
                        }
                    }
                    else
                    {
                        lvi.SubItems.Add(string.Empty);
                        colInts[i] = 0;
                    }
                }

                listView1.Items.Add(lvi);
                _allDatas.Add(colInts);
            }

            listView1.EndUpdate();
            listView1.EnsureVisible(limits.Count() - 1);
        }

        /// <summary>
        /// 출현갯수에 해당하는 행번호
        /// </summary>
        /// <param name="number">출현개수</param>
        /// <returns></returns>
        private List<int> FindLastExceptNumber(int number)
        {
            var list = new List<int>();

            for (int i = 0; i < 45; i++)
            {
                //번호의 최종출현갯수
                int n = _numberlastCounts[i];

                //출현갯수 + 1 (다음에 나왔을 경우이기 때문)
                if (n + 1 == number)
                {
                    list.Add(i + 1);
                }
            }
            return list;
        }

        /// <summary>
        /// 최종출수에 해당하는 카운트를 반환
        /// </summary>
        /// <returns>정수 리스트</returns>
        private List<int> FindAllMaxCount()
        {
            var list = new List<int>();

            foreach (string s in _colNames)
            {
                string query = $"SELECT cnt FROM ShowOrderTbl WHERE {s} = (SELECT MAX({s}) FROM ShowOrderTbl)";

                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                int n = (int)command.ExecuteScalar();
                list.Add(n);
            }

            return list;
        }

        /// <summary>
        /// 당번의 총출현갯수
        /// </summary>
        /// <param name="order">회차</param>
        /// <returns>정수배열</returns>
        private static int[] GetLastRowCountInts(int order)
        {
            var nums = Utilities.Utility.DangbeonOfOrder(order);
            var idxs = new List<int>();

            foreach (int num in nums)
            {
                string s = "c" + num;
                string query = $"SELECT cnt FROM ShowOrderTbl WHERE {s} = {order}";
                using var context = new LottoDBContext();
                using var command =context.Database.GetDbConnection().CreateCommand();
                command.CommandText= query;
                context.Database.OpenConnection();
                int n = (int)command.ExecuteScalar();
                idxs.Add(n);
            }

            return idxs.ToArray();
        }
    }
}
