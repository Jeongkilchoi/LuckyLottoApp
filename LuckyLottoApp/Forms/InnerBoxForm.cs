using System.Data;
using LuckyLottoLibrary;
using LuckyLottoApp.Utilities;
using LuckyLottoApp.Geomsas;
using LuckyLottoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 3데이터 내부상자의 횡종연속, 정역슬래쉬, 정역기역, 정역니은, 상하꺽쇠, 좌우꺽쇠
    /// </summary>
    public partial class InnerBoxForm : Form
    {
        #region 필드

        private readonly string[] _keyNames;
        private readonly string[] _itemNames;
        private Dictionary<string, List<int[]>> _selectedPairs;
        private List<int[]> _selectedItems;
        private List<int[]> _chulsuDatas;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public InnerBoxForm()
        {
            InitializeComponent();

            _keyNames = new[]
            {
                "가로연속", "세로연속", "정상사선", "역순사선", "정상기역", "역순기역",
                "정상니은", "역순니은", "상방꺽쇠", "하방꺽쇠", "좌측꺽쇠", "우측꺽쇠"
            };

            _itemNames = new[]
            {
                "Hyeonsok", "Vyeonsok", "Fsaseon", "Rsaseon", "Fkiyeok", "Rkiyeok",
                "Fnieun", "Rnieun" , "Upkkeok", "Dnkkeok", "Lfkkeok", "Rfkkeok"
            };
        }

        private void InnerBoxForm_Load(object sender, EventArgs e)
        {
            var boxDatas = new InnerBoxDatas(LuckyLibrary.HorizontalFlowDatas(7));
            _selectedPairs = boxDatas.BoxDataPairs();

            KeyComboBox.Items.AddRange(_keyNames);
            KeyComboBox.SelectedIndex = 0;
        }

        private void KeyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyComboBox.SelectedIndex > -1)
            {
                var boxDatas = (HorFlowRadioButton.Checked) ? new InnerBoxDatas(LuckyLibrary.HorizontalFlowDatas(7)) :
                                                              new InnerBoxDatas(LuckyLibrary.VerticalFlowDatas(7));
                _selectedPairs = boxDatas.BoxDataPairs();
                ItemListBox.Items.Clear();
                _chulsuDatas = new List<int[]>();
                string head = (HorFlowRadioButton.Checked) ? "H" : "V";
                int sel = KeyComboBox.SelectedIndex;
                var vals = _selectedPairs.ElementAt(sel).Value;
                _selectedItems = new List<int[]>(vals);
                string item = _itemNames[sel];
                string name = head + item;
                string query = $"SELECT {name} FROM InnerBoxTbl";
                using var context = new LottoDBContext();
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string s = reader.GetString(0);
                    var array = ChangedCharNumber(s.Trim());
                    _chulsuDatas.Add(array);
                }

                //리스트 박스에 항목 추가
                for (int i = 0; i < vals.Count; i++)
                {
                    string s = item + "_" + i;
                    ItemListBox.Items.Add(s);
                }
                ItemListBox.SelectedIndex = 0;
            }
        }

        private void ItemListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemListBox.SelectedIndex > -1)
            {
                int sel = ItemListBox.SelectedIndex;

                ChulsuListView.Items.Clear();

                var coldata = _chulsuDatas.Select(x => x.ElementAt(sel)).ToList();
                ChulsuListView.BeginUpdate();
                for (int i = 0; i < coldata.Count; i++)
                {
                    var lvi = new ListViewItem((i + 1).ToString());
                    lvi.SubItems.Add(coldata[i].ToString());
                    ChulsuListView.Items.Add(lvi);
                }
                ChulsuListView.EndUpdate();
                ChulsuListView.EnsureVisible(coldata.Count - 1);
                NumberTextBox.Text = string.Join(",", _selectedItems[sel].OrderBy(x => x).Select(g => g.ToString("00")));

                NumberTextBox.BackColor = SystemColors.Window;
                panel3.BackColor = SystemColors.Control;
                NextTextBox.BackColor = SystemColors.Window;
                Next1TextBox.BackColor = SystemColors.Window;
                PresentResult(coldata);
            }
        }

        private void ResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BadListBox.SelectedIndex > -1)
            {
                GoodListBox.SelectedIndex = -1;
                string s = BadListBox.SelectedItem.ToString();
                var splits = s.Split('/').Select(x => x.Trim()).ToArray();

                int keyindex = Array.IndexOf(_itemNames, splits[0]);
                int itmindex = int.Parse(splits[1]);
                KeyComboBox.SelectedIndex = keyindex;
                ItemListBox.SelectedIndex = itmindex;
            }
        }

        private void GoodListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GoodListBox.SelectedIndex > -1)
            {
                BadListBox.SelectedIndex = -1;
                string s = GoodListBox.SelectedItem.ToString();
                var splits = s.Split('/').Select(x => x.Trim()).ToArray();

                int keyindex = Array.IndexOf(_itemNames, splits[0]);
                int itmindex = int.Parse(splits[1]);
                KeyComboBox.SelectedIndex = keyindex;
                ItemListBox.SelectedIndex = itmindex;
            }
        }

        private async void GoodButton_Click(object sender, EventArgs e)
        {
            GoodListBox.Items.Clear();
            BadListBox.Items.Clear();
            var tg = new Tonggwa();
            var cls = (HorFlowRadioButton.Checked) ? new InnerBoxDatas(LuckyLibrary.HorizontalFlowDatas(7)) :
                                                     new InnerBoxDatas(LuckyLibrary.VerticalFlowDatas(7));
            string head = (HorFlowRadioButton.Checked) ? "H" : "V";
            var numpairs = await Task.Run(cls.BoxDataPairs);

            for (int i = 0; i < _itemNames.Length; i++)
            {
                string key = _keyNames[i];
                List<int[]> nums = numpairs[key];
                string colname = head + _itemNames[i];
                List<int[]> chuls = GetColumnDatas(colname).ToList();

                for (int j = 0; j < nums.Count; j++)
                {
                    int[] numInts = nums[j];
                    int[] chulInts = chuls.Select(x => x.ElementAt(j)).ToArray();
                    //var pairs = await Task.Run(() => tg.CheckedPass(chulInts));
                    var pairs = await Task.Run(() => tg.CheckedTongka(chulInts, "Innerbox"));
                    var rstlst = pairs.Values.ToList();

                    string s = _itemNames[i] + " / " + j;
                    if (!(rstlst.Any(x => x == -1) && rstlst.Any(x => x == 1)))
                    {
                        if (rstlst.Any(x => x == 1))
                        {
                            GoodListBox.Items.Add(s);
                        }
                        else if (rstlst.Any(x => x == -1))
                        {
                            BadListBox.Items.Add(s);
                        }
                    }
                }
            }
        }




        //***************  메서드  *****************



        /*
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
        /// 라벨과 문자열상자 후방최대 데이터 출력
        /// </summary>
        /// <param name="coldata">열별 전체 출수 데이터</param>
        /// <exception cref="Exception"></exception>
        private async void PresentResult(List<int> coldata)
        {
            var tg = new Tonggwa();
            var pairs = await Task.Run(() => tg.CheckedTongka(coldata, "Innerbox"));
            RealLabel.Text = "후방연속: ";
            MaxLabel.Text = "연속최대: ";
            SameLabel.Text = "동출수: ";
            NoneLabel.Text = "무출수: ";
            ShownLabel.Text = "유출수: ";
            PercentLabel.Text = "출수율: ";
            ZeroLabel.Text = "0출: ";
            OneLabel.Text = "1출: ";
            TwoLabel.Text = "2출: ";
            ThreeLabel.Text = "3출: ";

            RealNextLabel.Text = "후방연속: ";
            MaxNextLabel.Text = "연속최대: ";

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

            RealLabel.Text += tg.RealContinue;
            MaxLabel.Text += tg.MaxContinue;
            SameLabel.Text += tg.LastValueCount;
            NoneLabel.Text += tg.ZeroCount;
            ShownLabel.Text += tg.NoneCount;
            PercentLabel.Text += tg.Percentage.ToString("F2") + "%";
            ZeroLabel.Text += coldata.Count(x => x == 0);
            OneLabel.Text += coldata.Count(x => x == 1);
            TwoLabel.Text += coldata.Count(x => x == 2);
            ThreeLabel.Text += coldata.Count(x => x == 3);

            if (tg.NextList.Any())
            {
                var (realCount, maxCount) = NextReal.RealMaxCount(tg.NextList);
                RealNextLabel.Text += realCount;
                MaxNextLabel.Text += maxCount;
                NextTextBox.Text = ResultChulsuData(tg.NextList);
            }

            ResultCombineData(tg.RealFiveLists);
            
            var tonglist = pairs.Values.ToList();
            if (tonglist.Any(x => x == 1))
            {
                var find = pairs.Where(x => x.Value == 1).Select(x => x.Key);
                string fndstr = string.Join(", ", find);
                PassLabel.Text += "호번-" + fndstr;

                foreach (string key in find)
                {
                    switch (key)
                    {
                        case "연속무출":
                        case "연속진가":
                        case "구간출합":
                            NumberTextBox.BackColor = Color.Cyan;
                            break;
                        case "후방연속":
                        case "후방진가":
                            panel3.BackColor = Color.Cyan;
                            break;
                        case "후방다음":
                        case "후방간격":
                            NextTextBox.BackColor = Color.Cyan;
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
                string fndstr = string.Join(", ", find);
                PassLabel.Text += " 악번-" + fndstr;
                foreach (string key in find)
                {
                    switch (key)
                    {
                        case "한계초과":
                        case "연속무출":
                        case "연속진가":
                        case "구간출합":
                            NumberTextBox.BackColor = Color.LightSalmon;
                            break;
                        case "후방연속":
                        case "후방진가":
                            panel3.BackColor= Color.LightSalmon;
                            break;
                        case "후방다음":
                        case "후방간격":
                            NextTextBox.BackColor = Color.LightSalmon;
                            break;
                        case "후방패턴":
                            Next1TextBox.BackColor = Color.LightSalmon;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 숫자로 이루어진 문장을 정수배열로 바꾸기
        /// </summary>
        /// <param name="sentence">문자열</param>
        /// <returns></returns>
        private static int[] ChangedCharNumber(string sentence)
        {
            return sentence.Select(x => int.Parse(x.ToString())).ToArray();
        }

        /// <summary>
        /// 데이터의 검사결과
        /// </summary>
        /// <param name="list">리스트</param>
        /// <param name="skipCount">출수표시 스킵갯수</param>
        /// <returns>문자열</returns>
        private static string ResultChulsuData(List<int> list, int skipCount = 190)
        {
            string s = string.Empty;
            var dups = new List<(int chul, int dup)>();
            var groups = list.GroupBy(x => x).Where(g => g.Any()).Select(g => g.Key);
            if (groups?.Any() ?? false)
            {
                for (int i = groups.Min(); i <= groups.Max(); i++)
                {
                    int n = list.Count(x => x == i);
                    dups.Add((i, n));
                }

                string s1 = string.Join(",  ", dups.Select(x => $"{x.chul}({x.dup})"));
                double d = dups.Where(x => x.chul > 0).Sum(x => x.dup) * 100.0 / list.Count;
                s1 += ",  출률: " + d.ToString("F2") + "%\r\n";
                s = s1 + string.Join("-", list.Skip(list.Count - skipCount));
            }

            return s;
        }

        /// <summary>
        /// 결합 데이터의 검사결과 출력
        /// </summary>
        /// <param name="lists">리스트</param>
        private void ResultCombineData(List<(int realCount, int maxCount, List<int> nextLis)> lists)
        {
            Real1Label.Text += lists[0].realCount;
            Max1Label.Text += lists[0].maxCount;
            Next1TextBox.Text = ResultChulsuData(lists[0].nextLis);

            Real2Label.Text += lists[1].realCount;
            Max2Label.Text += lists[1].maxCount;
            Next2TextBox.Text = ResultChulsuData(lists[1].nextLis);

            Real3Label.Text += lists[2].realCount;
            Max3Label.Text += lists[2].maxCount;
            Next3TextBox.Text = ResultChulsuData(lists[2].nextLis, 46);

            Real4Label.Text += lists[3].realCount;
            Max4Label.Text += lists[3].maxCount;
            Next4TextBox.Text = ResultChulsuData(lists[3].nextLis, 46);

            Real5Label.Text += lists[4].realCount;
            Max5Label.Text += lists[4].maxCount;
            Next5TextBox.Text = ResultChulsuData(lists[4].nextLis, 46);
        }

        /// <summary>
        /// 열이름의 전체데이터
        /// </summary>
        /// <param name="colname"></param>
        /// <returns></returns>
        private static IEnumerable<int[]> GetColumnDatas(string colname)
        {
            string query = $"SELECT {colname} FROM InnerBoxTbl";
            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string s = reader.GetString(0);
                var arr = ChangedCharNumber(s);
                yield return arr;
            }
        }


    }
}
