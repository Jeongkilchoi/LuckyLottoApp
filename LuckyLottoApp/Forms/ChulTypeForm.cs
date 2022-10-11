using LuckyLottoApp.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;
using LuckyLottoApp.Geomsas;
using LuckyLottoApp.Utilities;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 출수타입 데이터 검사 폼 클래스
    /// </summary>
    public partial class ChulTypeForm : Form
    {
        #region 필드
        private readonly int _lastOrder;
        private int _section = 600;
        private readonly List<string> _columns;
        private List<(int order, int[] chulInts)> _allchuls;
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public ChulTypeForm(int lastOrder)
        {
            InitializeComponent();
            _columns = GetColumnName();
            _lastOrder = lastOrder;
        }

        private void ChulTypeForm_Load(object sender, EventArgs e)
        {
            _allchuls = GetAllTypeData();

            _columns.ForEach(x => ColumnComboBox.Items.Add(x));
            ColumnComboBox.SelectedIndex = 0;
            listView1.Columns.Add("회차", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("출수", 45, HorizontalAlignment.Center);

            var sectionList = new List<int>();
            for (int i = 50; i < _lastOrder; i += 50)
            {
                sectionList.Add(i);
            }
            sectionList.Add(0);
            sectionList.ForEach(x => SectionComboBox.Items.Add(x));
            SectionComboBox.SelectedIndex = SectionComboBox.Items.IndexOf(600);

            string[] strings = {"111111", "21111", "2211", "222", "3111", "321", "33", "411", "42", "51", "60" };
            var s = Enumerable.Range(0, strings.Length).Zip(strings, (idx, str) => $"{idx}: {str}");
            var front = s.Take(6);
            var reals = s.Skip(6);

            var s1 = string.Join("", front.Select(x => x.PadRight(12)));
            var s2 = string.Join("", reals.Select(x => x.PadRight(12)));
            ShowLabel.Text = s1 + "\r\n" + s2;
        }

        private void ColumnComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ColumnComboBox.SelectedIndex > -1)
            {
                int sel = ColumnComboBox.SelectedIndex;
                var datas = _section == 0 ? _allchuls.Select(x => (ord: x.order, chul: x.chulInts[sel])) :
                                            _allchuls.Skip(_lastOrder - _section).Select(x => (ord: x.order, chul: x.chulInts[sel]));
                PresentListView(datas);
                var collection = datas.Select(x => x.chul);
                PresentChulData(collection, "Chultype");
            }
        }

        private void SectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SectionComboBox.SelectedIndex > -1)
            {
                int sel = Convert.ToInt32(SectionComboBox.SelectedItem);
                _section = sel;
            }
        }

        private void GoodBadButton_Click(object sender, EventArgs e)
        {
            MultipleCombines();
        }



        //*******************  메서드  **********************




        /// <summary>
        /// 타입출수 전체 데이터
        /// </summary>
        /// <returns></returns>
        private List<(int, int[])> GetAllTypeData()
        {
            var result = new List<(int, int[])>();
            string query = $"SELECT Orders,{string.Join(",", _columns)} FROM TypeTbl";
            using var context = new LottoDBContext();
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int[] array = new int[reader.FieldCount - 1];
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    array[i - 1] = reader.GetInt32(i);
                }
                result.Add((reader.GetInt32(0), array));
            }
            return result;
        }

        /// <summary>
        /// 타입 테이블 컬럼이름
        /// </summary>
        /// <returns></returns>
        private static List<string> GetColumnName()
        {
            var columns = typeof(TypeTbl).GetProperties().Select(x => x.Name).Skip(1).ToList();
            return columns;
        }

        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        /// <param name="values"></param>
        private void PresentListView(IEnumerable<(int ord, int chul)> values)
        {
            listView1.Items.Clear();
            int last = values.Last().chul;
            listView1.BeginUpdate();
            foreach (var (ord, chul) in values)
            {
                var lvi = new ListViewItem(ord.ToString()) { UseItemStyleForSubItems = false };
                lvi.SubItems.Add(chul.ToString());
                listView1.Items.Add(lvi);
                if (chul == last)
                {
                    lvi.SubItems[0].BackColor = Color.Cyan;
                    lvi.SubItems[1].BackColor = Color.Cyan;
                }
            }
            listView1.EndUpdate();
            listView1.EnsureVisible(values.Count() - 1);
        }

        /// <summary>
        /// 라벨및 텍스트상자에 출력
        /// </summary>
        /// <param name="chuls">출수데이터</param>
        /// <param name="key">항목이름</param>
        private async void PresentChulData(IEnumerable<int> chuls, string key)
        {
            #region 라벨초기화
            NowRealLabel.Text = "후방연속: ";
            NowMaxLabel.Text = "연속최대: ";
            SameCountLabel.Text = "동수출: ";
            NonCountLabel.Text = "무출수: ";
            ShownCountLabel.Text = "유출수: ";
            PercentLabel.Text = "출수률: ";
            NextRealLabel.Text = "후방연속: ";
            NextMaxLabel.Text = "연속최대: ";
            Real1Label.Text = "후방연속: ";
            Max1Label.Text = "연속최대: ";
            Real2Label.Text = "후방연속: ";
            Max2Label.Text = "연속최대: ";
            Real3Label.Text = "후방연속: ";
            Max3Label.Text = "연속최대: ";
            Real4Label.Text = "후방연속: ";
            Max4Label.Text = "연속최대: ";
            Real5Label.Text = "후방연속: ";
            Max5Label.Text = "연속최대: ";
            PassLabel.Text = "검사결과: ";

            NextChulsuTextBox.Text = string.Empty;
            Next1TextBox.Text = string.Empty;
            Next2TextBox.Text = string.Empty;
            Next3TextBox.Text = string.Empty;
            Next4TextBox.Text = string.Empty;
            Next5TextBox.Text = string.Empty;

            NowRealLabel.BackColor = SystemColors.Control;
            NextChulsuTextBox.BackColor = SystemColors.Window;
            panel4.BackColor = SystemColors.Control;
            Next1TextBox.BackColor = SystemColors.Window;
            #endregion

            var tg = new Tonggwa();
            var pairs = await Task.Run(() => tg.CheckedTongka(chuls, key));
            NowRealLabel.Text += tg.RealContinue;
            NowMaxLabel.Text += tg.MaxContinue;
            SameCountLabel.Text += tg.LastValueCount;
            if (tg.ZeroCount != 0)
            {
                NonCountLabel.Text += tg.ZeroCount;
                ShownCountLabel.Text += tg.NoneCount;
                PercentLabel.Text += tg.Percentage.ToString("F2") + "%";
            }

            NextChulsuTextBox.Text = ResultChulsuData(tg.NextList);
            if (tg.NextList.Any())
            {
                var (realCount, maxCount) = NextReal.RealMaxCount(tg.NextList);
                NextRealLabel.Text += realCount;
                NextMaxLabel.Text += maxCount;
            }

            ResultCombineData(tg.RealFiveLists);

            var tonglist = pairs.Values.ToList();
            if (tonglist.Any(x => x == 1))
            {
                var find = pairs.Where(x => x.Value == 1).Select(x => x.Key);
                PassLabel.Text += string.Join(", ", find);
                foreach (string item in pairs.Where(x => x.Value == 1).Select(x => x.Key))
                {
                    switch (item)
                    {
                        case "연속무출":
                        case "연속진가":
                        case "구간출합":
                            NowRealLabel.BackColor = Color.Cyan;
                            break;
                        case "후방연속":
                        case "후방진가":
                            panel4.BackColor = Color.Cyan;
                            break;
                        case "후방다음":
                        case "후방간격":
                            NextChulsuTextBox.BackColor = Color.Cyan;
                            break;
                        case "후방패턴":
                            Next1TextBox.BackColor = Color.Cyan;
                            break;
                    }
                }
            }
            if (tonglist.Any(x => x == -1))
            {
                var find = pairs.Where(x => x.Value == -1).Select(x => x.Key);
                PassLabel.Text += string.Join(", ", find);
                foreach (string item in pairs.Where(x => x.Value == -1).Select(x => x.Key))
                {
                    switch (item)
                    {
                        case "연속무출":
                        case "연속진가":
                        case "구간출합":
                            NowRealLabel.BackColor = Color.LightSalmon;
                            break;
                        case "후방연속":
                        case "후방진가":
                            panel4.BackColor = Color.LightSalmon;
                            break;
                        case "후방다음":
                        case "후방간격":
                            NextChulsuTextBox.BackColor = Color.LightSalmon;
                            break;
                        case "후방패턴":
                            Next1TextBox.BackColor = Color.LightSalmon;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 데이터의 검사결과
        /// </summary>
        /// <param name="list">리스트</param>
        /// <param name="skipCount">출수표시 스킵갯수</param>
        /// <returns>문자열</returns>
        private static string ResultChulsuData(List<int> list, int skipCount = 100)
        {
            string s = string.Empty;
            var dups = new List<(int chul, int dup)>();
            if (list.Any())
            {
                var groups = list.GroupBy(x => x).Where(g => g.Any()).Select(g => g.Key);
                for (int i = groups.Min(); i <= groups.Max(); i++)
                {
                    int n = list.Count(x => x == i);
                    dups.Add((i, n));
                }

                string s1 = string.Join(",  ", dups.Select(x => $"{x.chul}({x.dup})"));
                double d = dups.Where(x => x.chul > 0).Sum(x => x.dup) * 100.0 / list.Count;
                s1 += ",  출률: " + d.ToString("F2") + "%\r\n";
                s = s1 + string.Join("-", list.Skip(list.Count - skipCount));
                return s;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 결합 데이터의 검사결과 출력
        /// </summary>
        /// <param name="lists">리스트</param>
        private void ResultCombineData(List<(int realCount, int maxCount, List<int> nextLis)> lists)
        {
            Real1Label.Text += lists[0].realCount;
            Max1Label.Text += lists[0].maxCount;
            Next1TextBox.Text = ResultChulsuData(lists[0].nextLis, 33);

            Real2Label.Text += lists[1].realCount;
            Max2Label.Text += lists[1].maxCount;
            Next2TextBox.Text = ResultChulsuData(lists[1].nextLis, 33);

            Real3Label.Text += lists[2].realCount;
            Max3Label.Text += lists[2].maxCount;
            Next3TextBox.Text = ResultChulsuData(lists[2].nextLis, 33);

            Real4Label.Text += lists[3].realCount;
            Max4Label.Text += lists[3].maxCount;
            Next4TextBox.Text = ResultChulsuData(lists[3].nextLis, 33);

            Real5Label.Text += lists[4].realCount;
            Max5Label.Text += lists[4].maxCount;
            Next5TextBox.Text = ResultChulsuData(lists[4].nextLis, 33);
        }

        private void MultipleCombines()
        {
            var sequence = Enumerable.Range(0, _columns.Count);
            var combinlists = new List<int[]>();
            for (int i = 1; i <= 3; i++)
            {
                var each = Utility.GetCombinations(sequence, i).Select(x => x.ToArray()).ToList();
                combinlists.AddRange(each);
            }

            foreach (int[] johapInts in combinlists)
            {
                if (johapInts.Length == 1)
                {
                    int idx = johapInts[0];
                    var alldatas = _allchuls.Select(x => x.chulInts[idx]);
                    var (min, max, explist) = DataOfLimitRange(alldatas.ToArray(), 0, 10);
                    if (explist.Count > 2)
                    {

                    }
                }
                else
                {
                    var alldatas = _allchuls.Select(x => johapInts.Select(g => x.chulInts[g]).ToArray());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ascCollection"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static (int minimum, int maximum, List<int> excepInts) DataOfLimitRange(IEnumerable<int> ascCollection, int bottom, int top)
        {
            if (top -1 <= bottom)
            {
                throw new Exception("최저, 최고 설정오류.");
            }

            if (!(ascCollection?.Any() ?? false))
            {
                throw new Exception("빈 컬렉션입니다.");
            }

            if (ascCollection.Distinct().Count() < 3)
            {
                return (ascCollection.Min() < 0 ? 0 : ascCollection.Min(), ascCollection.Max(), new List<int>());
            }
            else
            {
                int count = ascCollection.Count();
                double[] limits = { 0.01 * 0.3, 0.01 * 0.6, 0.01 * 0.9, 0.01 * 1.2, 0.01 * 2 };
                int limit = count switch
                {
                    <= 004 => Convert.ToInt32(count * limits[4]),
                    <= 012 => Convert.ToInt32(count * limits[3]),
                    <= 050 => Convert.ToInt32(count * limits[2]),
                    <= 100 => Convert.ToInt32(count * limits[1]),
                    _ => Convert.ToInt32(count * limits[0])
                };

                int sum = 0, min = 0, max = 0;
                var cumuls = Enumerable.Range(bottom, top - bottom + 1).Select(x => (chul: x, cnt: ascCollection.Count(g => g == x))).ToList();

                foreach (var (chul, cnt) in cumuls)
                {
                    sum += cnt;
                    if (limit < sum)
                    {
                        min = chul;
                        break;
                    }
                }
                sum = 0;
                foreach (var (chul, cnt) in cumuls.AsEnumerable().Reverse())
                {
                    sum += cnt;
                    if (limit < sum)
                    {
                        max = chul;
                        break;
                    }
                }

                if (2 <= max - min)
                {
                    var exp = cumuls.Where(x => x.cnt == 0 && x.chul > min && x.chul < max).Select(x => x.chul).ToList();

                    //후방연속 초과검사
                    var (realcon, maxcon, nexts) = NextReal.RealMaxNextList(ascCollection);
                    int last = ascCollection.Last();
                    if ((last < min && last < max) && realcon >= maxcon)
                    {
                        exp.Add(last);
                    }

                    //다음출에서 초과검사
                    if ((nexts?.Any() ?? false) && limit * 2 <= nexts.Count)
                    {
                        int nxlast = nexts[^1];
                        var (realCount, maxCount) = NextReal.RealMaxCount(nexts);
                        if ((nxlast < min && last < max) && realCount >= maxCount)
                        {
                            exp.Add(nxlast);
                        }

                        var nxcnts = Enumerable.Range(min, max - min + 1).Select(x => (nxch: x, nxct: nexts.Count(g => g == x)));
                        var zeros = nxcnts.Where(x => x.nxct == 0 && x.nxch > min && x.nxch < max).Select(x => x.nxch);
                        if (zeros.Any())
                        {
                            foreach (int n in zeros)
                            {
                                int chulcount = cumuls.Where(x => x.chul == n).Single().cnt;
                                if (chulcount < count / 5)
                                {
                                    exp.Add(n);
                                }
                            }
                        }
                    }

                    return (min, max, exp.Distinct().OrderBy(x => x).ToList());
                }
                else
                {
                    return (ascCollection.Min() < 0 ? 0 : ascCollection.Min(), ascCollection.Max(), new List<int>());
                }
            }
        }
    }
}
