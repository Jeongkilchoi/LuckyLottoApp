using System.Data;
using System.Text;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 출현간격 검사하는 폼 클래스
    /// </summary>
    public partial class ChulKankyeokForm : Form
    {
        #region 필드

        private readonly int _lastOrder;
        private int _startOrder;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public ChulKankyeokForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;
        }

        private void ChulKankyeokForm_Load(object sender, EventArgs e)
        {
            var list = new List<int>();
            for (int i = 50; i <= _lastOrder; i += 50)
            {
                list.Add(i);
            }

            if (list.Last() != _lastOrder)
            {
                list.Add(_lastOrder);
            }

            list.ForEach(x => SectionComboBox.Items.Add(x));
            SectionComboBox.SelectedIndex = 7;

            int start = _lastOrder - 400 + 1;
            OrderNumericUpDown.Value = start;
            OrderNumericUpDown.Maximum = _lastOrder;

            listView1.Columns.Add("순번", 40, HorizontalAlignment.Left);
            for (int i = 1; i <= 45; i++)
            {
                listView1.Columns.Add(i.ToString(), 40, HorizontalAlignment.Center);
            }
        }

        private void OrderNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _startOrder = (int)OrderNumericUpDown.Value;
            PresentListview();
        }

        private void SectionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var combobox = sender as ComboBox;
            int num = (int)combobox.SelectedItem;
            int start = _lastOrder - num + 1;
            OrderNumericUpDown.Value = start;
        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            SumaryTextBox.Enabled = true;
            ResultTextBox.Enabled = true;
            var lastshown = new Dictionary<int, int>();
            var badStrings = new List<string>();
            var goodList = new List<int>();

            for (int i = 1; i <= 45; i++)
            {
                var data = await Task.Run(() => FindNumberOfShown(_startOrder, i));
                data.Add(_lastOrder + 1);
                var zip = data.Zip(data.Skip(1), (a, b) => b - a).ToList();

                zip.Insert(0, data[0]);
                int befor = zip[^2];
                int last = zip[^1];
                lastshown.Add(i, last);
                var (realCount, maxCount) = NextReal.RealMaxCount(zip);

                //연속출이 아니면
                if (realCount == 1)
                {
                    //최종값이 나온적이 없으면
                    if (maxCount == 0)
                    {
                        badStrings.Add(i.ToString("00"));
                    }
                    else
                    {
                        var exceptlist = new List<int>(zip);
                        exceptlist.RemoveRange(zip.Count - realCount, realCount);

                        //최종값을 뺀 나머지에서 직전번호가 나온 인덱스+1 리스트
                        var indexlist = exceptlist.Select((val, idx) => (val, idx))
                                                  .Where(x => x.val == befor).Select(x => x.idx + 1);

                        if (indexlist.Any())
                        {
                            var valist = indexlist.Select(x => zip[x]);

                            //최종값과 같으면 호번처리
                            if (valist.Count(x => x == last) >= 2)
                            {
                                goodList.Add(i);
                            }
                        }
                    }
                }
                else
                {
                    if (realCount >= maxCount)
                    {
                        badStrings.Add(i.ToString("00"));
                    }
                }

                var keylist = lastshown.Where(x => x.Value == 1).Select(x => x.Key);
                var remind = goodList.Except(keylist).OrderBy(x => x).Distinct()
                                     .Select(x => x.ToString("00"));

                string rst = "악번: " + string.Join(",", badStrings) + "\r\n\r\n" +
                             "호번: " + string.Join(",", remind);

                ResultTextBox.Text = rst;

                var zero = lastshown.Where(x => x.Value <= 2).Select(x => x.Key).Select(x => x.ToString("00"));
                var stringBuild = new StringBuilder();
                stringBuild.AppendLine("00 - 02:  " + string.Join(",", zero));
                var one = lastshown.Where(x => x.Value >= 3 && x.Value <= 5).Select(x => x.Key)
                                        .Select(x => x.ToString("00"));
                stringBuild.AppendLine("03 - 05:  " + string.Join(",", one));
                var two = lastshown.Where(x => x.Value >= 6 && x.Value <= 10).Select(x => x.Key)
                                   .Select(x => x.ToString("00"));
                stringBuild.AppendLine("06 - 10:  " + string.Join(",", two));
                var three = lastshown.Where(x => x.Value >= 11 && x.Value <= 15).Select(x => x.Key)
                                     .Select(x => x.ToString("00"));
                stringBuild.AppendLine("11 - 15:  " + string.Join(",", three));
                var four = lastshown.Where(x => x.Value >= 16 && x.Value <= 20).Select(x => x.Key)
                                    .Select(x => x.ToString("00"));
                stringBuild.AppendLine("16 - 20:  " + string.Join(",", four));
                var five = lastshown.Where(x => x.Value >= 21 && x.Value <= 25).Select(x => x.Key)
                                    .Select(x => x.ToString("00"));
                stringBuild.AppendLine("21 - 25:  " + string.Join(",", five));
                var six = lastshown.Where(x => x.Value > 25).Select(x => x.Key).Select(x => x.ToString("00"));
                stringBuild.AppendLine("26 이상:  " + string.Join(",", six));

                string s2 = stringBuild.ToString();
                SumaryTextBox.Text = s2;
            }
        }






        //********************* 내부메서드  *************************





        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        private async void PresentListview()
        {
            listView1.Items.Clear();

            var alldatas = new List<int[]>();
            for (int i = 1; i <= 45; i++)
            {
                var data = await Task.Run(() => FindNumberOfShown(_startOrder, i));
                data.Add(_lastOrder + 1);
                alldatas.Add(data.ToArray());
            }

            int max = alldatas.Select(x => x.Length).Max();
            var ordlvi = new ListViewItem("회차");
            for (int i = 0; i < 45; i++)
            {
                int n = alldatas[i][0];
                ordlvi.SubItems.Add(n.ToString());
            }
            listView1.Items.Add(ordlvi);

            listView1.BeginUpdate();
            for (int i = 1; i < max; i++)
            {
                var lvi = new ListViewItem(i.ToString());
                for (int j = 0; j < 45; j++)
                {
                    if (alldatas[j].Length > i)
                    {
                        int n1 = alldatas[j][i];
                        int n2 = alldatas[j][i - 1];
                        int n = n1 - n2;
                        lvi.SubItems.Add(n.ToString());
                    }
                    else
                    {
                        lvi.SubItems.Add("");
                    }
                }
                listView1.Items.Add(lvi);
            }

            listView1.EndUpdate();
            listView1.EnsureVisible(max - 1);
        }

        /// <summary>
        /// 번호가 나온 회차리스트를 반환
        /// </summary>
        /// <param name="start">시작회차</param>
        /// <param name="number">검사할 번호</param>
        /// <returns></returns>
        private static List<int> FindNumberOfShown(int start, int number)
        {
            var list = new List<int>();

            string s = "c" + number;
            string query = "SELECT * FROM AppearTbl WHERE Orders >= " + start + " AND " + s + " = 0";
            using var context = new LottoDBContext();
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            context.Database.OpenConnection();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int n = reader.GetInt32(0);
                list.Add(n);
            }

            return list;
        }
    }
}
