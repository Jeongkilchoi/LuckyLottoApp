using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using LuckyLottoApp.Entities;
using LuckyLottoApp.Utilities;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 3열과 5열 데이터파일로 검사하는 폼 클래스
    /// </summary>
    public partial class SamOhDataForm : Form
    {
        #region Field

        private readonly int _lastOrder;
        private CancellationTokenSource _cts;
        private Dictionary<int, int[]> _allthree;
        private Dictionary<int, int[]> _allfive;
        private Dictionary<int, int[]> _selectedPairs = null;
        private IEnumerable<int[]> _threeDatas;
        private IEnumerable<int[]> _fiveDatas;
        private List<string> _resultLists;
        private int _section;
        private int _limit = 1;

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public SamOhDataForm(int lastOrder)
        {
            InitializeComponent();

            _lastOrder = lastOrder;

            RemoveToolStripMenuItem.Click += (s, e) =>
            {
                if (ResultListBox.CheckedItems.Count > 0)
                {
                    foreach (var item in ResultListBox.CheckedItems.OfType<string>().ToList())
                    {
                        ResultListBox.Items.Remove(item);
                    }
                }
            };

            CancelToolStripMenuItem.Click += (s, e) =>
            {
                RemoveContextMenuStrip.Close();
            };
        }

        private void SamOhDataForm_Load(object sender, EventArgs e)
        {
            var items = new List<int>();

            for (int i = 50; i <= _lastOrder; i += 50)
            {
                items.Add(i);
            }

            if (items.Last() != _lastOrder)
            {
                items.Add(_lastOrder);
            }

            items.ForEach(x => SectionComboBox.Items.Add(x));
            SectionComboBox.SelectedIndex = items.Count - 1;
            _section = _lastOrder;


            string path3 = Application.StartupPath + @"\DataFiles\nonthree.csv";
            string path5 = Application.StartupPath + @"\DataFiles\nonfive.csv";
            _threeDatas = LinesToCollection(path3);
            _fiveDatas = LinesToCollection(path5);
            _allthree = ShownAllThreeDatas().Select((v, i) => (v, i)).ToDictionary(x => x.i + 1, x => x.v);
            _allfive = ShownAllFiveDatas().Select((v, i) => (v, i)).ToDictionary(x => x.i + 1, x => x.v);
            _selectedPairs = new Dictionary<int, int[]>(_allthree);
            PresentListView();
        }

        private async void NonTwoButton_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            ResultListBox.Items.Clear();
            SelectedNumberTextBox.Text = "";
            NonTwoButton.Enabled = false;
            StopButton.Enabled = true;
            int limit = (int)LimitNumericUpDown.Value;

            //최종회차의 데이터
            var lastfind = _selectedPairs.Values.Last();

            //0출인 인덱스
            var zeroidx = lastfind.Select((val, idx) => new { val, idx })
                                  .Where(x => x.val == 0).Select(x => x.idx);

            var johaplist = await Task.Run(() => Utility.GetCombinations(zeroidx, 2).Select(x => x.ToArray()), token);
            await Task.Delay(1000);

            try
            {
                progressBar1.Maximum = johaplist.Count();
                var rsts = GetNoneCollection(johaplist, token);
                token.ThrowIfCancellationRequested();
                if (rsts.Any())
                {
                    var task = Task.Run(() =>
                    {
                        var orded = rsts.OrderByDescending(x => x.real).Select(x => x.name);

                        foreach (var item in orded)
                        {
                            ResultListBox.Invoke((Action) delegate
                            {
                                ResultListBox.Items.Add(item);
                            });
                            
                        }
                    }, token);

                    await task;
                    token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("작업 취소됨.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _cts?.Dispose();
                NonTwoButton.Enabled = true;
                StopButton.Enabled = false;
                progressBar1.Value = 0;
            }
        }

        private async void NonThreeButton_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            ResultListBox.Items.Clear();
            SelectedNumberTextBox.Text = "";
            NonThreeButton.Enabled = false;
            StopButton.Enabled = true;
            int limit = (int)LimitNumericUpDown.Value;

            //최종회차의 데이터
            var lastfind = _selectedPairs.Values.Last();

            //0출인 인덱스
            var zeroidx = lastfind.Select((val, idx) => new { val, idx })
                                  .Where(x => x.val == 0).Select(x => x.idx);
            var johaplist = await Task.Run(() => Utility.GetCombinations(zeroidx, 3).Select(x => x.ToArray()), token);

            //바로 작업하지 못하게 1초 시간을 지연
            await Task.Delay(1000);

            try
            {
                progressBar1.Maximum = johaplist.Count();
                var rsts = GetNoneCollectionAsync(johaplist, token);
                var finals = await Consume(rsts, token);
                if (finals.Any())
                {
                    var orded = finals.OrderByDescending(x => x.real).Select(x => x.name);

                    foreach (var item in orded)
                    {
                        ResultListBox.Invoke((Action)delegate
                        {
                            ResultListBox.Items.Add(item);
                        });
                    }
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("작업 취소됨.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _cts?.Dispose();
                NonThreeButton.Enabled = true;
                StopButton.Enabled = false;
                progressBar1.Value = 0;
            }
        }

        private async void ThreeShownButton_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            ResultListBox.Items.Clear();
            SelectedNumberTextBox.Text = "";
            WriteButton.Enabled = true;
            ThreeShownButton.Enabled = false;
            StopButton.Enabled = true;
            panel2.Enabled = true;
            Limit1CheckBox.Checked = false;
            Limit2CheckBox.Checked = false;
            int limit = (int)LimitNumericUpDown.Value;

            //최종회차의 데이터
            var lastfind = _selectedPairs.Values.Last();

            //유출인 인덱스
            var zeroidx = lastfind.Select((val, idx) => new { val, idx })
                                  .Where(x => x.val != 0).Select(x => x.idx);

            var johaplist = await Task.Run(() => Utility.GetCombinations(zeroidx, 3).Select(x => x.ToArray()), token);
            await Task.Delay(1000);

            try
            {
                progressBar1.Maximum = johaplist.Count();
                var rsts = GetShownCollection(johaplist, token);
                token.ThrowIfCancellationRequested();
                if (rsts.Any())
                {
                    var task = Task.Run(() =>
                    {
                        var orded = rsts.OrderByDescending(x => x.real).Select(x => x.name);
                        _resultLists = new List<string>(orded);
                        foreach (var item in orded)
                        {
                            ResultListBox.Invoke((Action)delegate
                            {
                                ResultListBox.Items.Add(item);
                            });

                        }
                    }, token);

                    await task;
                    token.ThrowIfCancellationRequested();
                    panel3.Enabled = true;
                }
            }
            catch (OperationCanceledException)
            {
                _resultLists = new();
                MessageBox.Show("작업 취소됨.");
            }
            catch (Exception ex)
            {
                _resultLists = new();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _cts?.Dispose();
                ThreeShownButton.Enabled = true;
                StopButton.Enabled = false;
                progressBar1.Value = 0;
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\DataFiles\threeshown.dat";

            if (ResultListBox.Items.Count > 0)
            {
                var list = new List<string>();

                foreach (var item in ResultListBox.Items.OfType<string>().ToList())
                {
                    string s = (string)item;
                    list.Add(s);
                }

                string rst = string.Join(Environment.NewLine, list);
                File.WriteAllText(path, rst);
            }

            MessageBox.Show("파일 작성됨.");
            ChangeButton.Enabled = true;
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            SaveTextBox.Text = string.Empty;
            string path = Application.StartupPath + @"\DataFiles\threeshown.dat";
            var lines = File.ReadAllLines(path);

            var temps = new List<string>();

            foreach (var line in lines)
            {
                var tmp = new List<string>();
                string s = line.Trim();
                string[] comma = { "," };
                var splits = s.Split(comma, StringSplitOptions.RemoveEmptyEntries);

                foreach (string s1 in splits)
                {
                    string s2 = s1[1..];
                    int n = int.Parse(s2) + 1;

                    string s3 = "Item" + n;
                    tmp.Add(s3);
                }

                string s4 = string.Join(",", tmp);
                temps.Add(s4);
            }

            string s5 = string.Join(Environment.NewLine, temps);
            SaveTextBox.Text = s5;
        }

        private void NoneCheckedButton_Click(object sender, EventArgs e)
        {
            while (ResultListBox.CheckedIndices.Count > 0)
            {
                ResultListBox.SetItemChecked(ResultListBox.CheckedIndices[0], false);
            }
        }

        private void InitialButton_Click(object sender, EventArgs e)
        {
            if (_resultLists?.Any() ?? false)
            {
                ResultListBox.Items.Clear();
                ResultListBox.Items.AddRange(_resultLists.ToArray());
            }
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            var chklist = panel2.Controls.OfType<CheckBox>().ToList();
            var chkval = new List<int> { (int)Limit1NumericUpDown.Value, (int)Limit2NumericUpDown.Value, (int)Limit3NumericUpDown.Value };
            var checkfind = chklist.Where(x => x.Checked).ToList();
            int length = (int)LimitNumericUpDown.Value;
            int limit = (int)Limit1NumericUpDown.Value;
            if (checkfind?.Any() ?? false && ResultListBox.Items.Count > 0)
            {
                for (int i = 0; i < ResultListBox.Items.Count; i++)
                {
                    string item = ResultListBox.Items[i].ToString();
                    var splits = item.Split(',');
                    int[] ints = new int[splits.Length];
                    for (int i1 = 0; i1 < splits.Length; i1++)
                    {
                        string split = splits[i1];
                        string s1 = split.Trim();
                        string s2 = s1.Replace("c", "");
                        ints[i1] = int.Parse(s2);
                    }

                    var tmp = new List<(int, int[])>();
                    foreach (int key in _allthree.Keys)
                    {
                        var vals = _allthree[key];
                        var arr = ints.Select(x => vals[x]).ToArray();
                        tmp.Add((key, arr));
                    }

                    var revs = Enumerable.Reverse(tmp).ToList();
                    var tps = Enumerable.Range(1, 3)
                        .Select(x => (tag: x, limt: chkval[x - 1], cnt: revs[limit + x - 1].Item2.Count(g => g != 0))).ToList();
                    var bools = new List<bool>();
                    foreach (CheckBox box in checkfind)
                    {
                        int tag = Convert.ToInt32(box.Tag);
                        var find = tps.Where(x => x.tag == tag).Single();
                        if (find.limt <= find.cnt)
                            bools.Add(true);
                        else
                            bools.Add(false);
                    }

                    if (bools.All(x => x == true))
                    {
                        ResultListBox.SetItemChecked(i, true);
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void SectionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            _section = (int)comboBox.SelectedItem;
            PresentListView();
        }

        private void ThreeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ThreeRadioButton.Checked)
                _selectedPairs = new Dictionary<int, int[]>(_allthree);
            else
                _selectedPairs = new Dictionary<int, int[]>(_allfive);
            
            PresentListView();
        }

        private void LimitNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            _limit = (int)LimitNumericUpDown.Value;
        }

        private void BeforeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BeforeCheckBox.Checked)
            {
                for (int i = 0; i < ResultListBox.Items.Count - 1; i++)
                {
                    if (ResultListBox.GetItemChecked(i))
                    {
                        ResultListBox.SetItemChecked(i, false);
                    }
                    else
                    {
                        ResultListBox.SetItemChecked(i, true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ResultListBox.Items.Count - 1; i++)
                {
                    if (!ResultListBox.GetItemChecked(i))
                    {
                        ResultListBox.SetItemChecked(i, true);
                    }
                    else
                    {
                        ResultListBox.SetItemChecked(i, false);
                    }
                }
            }
        }

        private async void ResultListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ResultListBox.SelectedIndex > -1)
            {
                string item = (string)ResultListBox.SelectedItem;
                string[] comma = { "," };
                var splits = item.Split(comma, StringSplitOptions.RemoveEmptyEntries);

                var ints = new List<int>();

                foreach (var split in splits)
                {
                    string s1 = split.Trim();
                    string s2 = s1.Replace("c", "");

                    ints.Add(int.Parse(s2));
                }

                var tmp = new List<(int, int[])>();
                foreach (int key in _selectedPairs.Keys)
                {
                    var vals = _selectedPairs[key];
                    var arr = ints.Select(x => vals[x]).ToArray();
                    tmp.Add((key, arr));
                }

                //int imsicount = 0;
                //var bag = new ConcurrentBag<(int, int[])>();

                //var paraltask = Task.Run(() =>
                //{
                //    Parallel.For(0, _selectedPairs.Count, (index) =>
                //    {

                //        var key = _selectedPairs.ElementAt(index).Key;
                //        var val = _selectedPairs.ElementAt(index).Value;
                //        var arr = ints.Select(x => val[x]).ToArray();
                //        Interlocked.Increment(ref imsicount);
                //        bag.Add((key, arr));
                //    });
                //});

                //await paraltask;

                ////무작위기 때문에 다시 정렬시킨다
                //var ordic = bag.OrderBy(x => x.Item1).ToList();

                //리스트뷰에 출력
                PresentListView(tmp, ints);
                var list = await Task.Run(() => GetIndexData(ints));
                SelectedNumberTextBox.Text = string.Join(",", list.Select(x => x.ToString("00")));
            }
        }

        private void DicListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int selColumn = e.Column;

            if (selColumn > 0)
            {
                SaveTextBox.Text = string.Empty;
                string colName = DicListView.Columns[selColumn].Text;
                string s = colName.Replace("c", "");
                int idx = int.Parse(s);

                var dic = _selectedPairs.ToDictionary(x => x.Key, x => x.Value[idx]);
                var data = dic.Values.ToList();
                var rst = new List<string>();

                int shown = data.Where(x => x > 0).Count();
                double d = shown * 100.0 / data.Count;
                var nums = GetIndexData(new[] { idx });
                rst.Add("번호: " + string.Join(",", nums.Select(x => x.ToString("00"))) + "\r\n");
                rst.Add("출수률: " + d.ToString("F2") + "%\r\n");

                //번호, 출수률, 최소 ~ 최대 사이 출수값
                int min = data.Min();
                int max = data.Max();

                for (int i = 0; i <= nums.Count; i++)
                {
                    int n = data.Count(x => x == i);
                    string s1 = "[" + i + "] : " + n;
                    rst.Add(s1);
                }

                string s2 = string.Join(Environment.NewLine, rst);
                SaveTextBox.Text = s2;
            }
        }




        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        private void PresentListView()
        {
            DicListView.Clear();
            var pairs = ThreeRadioButton.Checked ? _allthree : _allfive;
            int start = _lastOrder == _section ? 1 : _lastOrder - (_section - 1);
            var selpairs = pairs.Where(x => x.Key >= start).ToDictionary(x => x.Key, x => x.Value);

            DicListView.Columns.Add("id", 45, HorizontalAlignment.Left);
            for (int i = 0; i < selpairs.First().Value.Length; i++)
            {
                string s = $"c{i}";
                DicListView.Columns.Add(s, 45, HorizontalAlignment.Center);
            }

            DicListView.BeginUpdate();

            foreach (var item in selpairs)
            {
                var lvi = new ListViewItem(item.Key.ToString());
                foreach (int n in item.Value)
                {
                    lvi.SubItems.Add(n.ToString());
                }
                DicListView.Items.Add(lvi);
            }
            DicListView.EndUpdate();
            DicListView.EnsureVisible(selpairs.Count - 1);
        }

        /// <summary>
        /// 리스트뷰에 출력
        /// </summary>
        /// <param name="pairs">튜플(정수, 정수배열)</param>
        /// <param name="list">리스트</param>
        private void PresentListView(List<(int, int[])> pairs, List<int> list)
        {
            DicListView.Clear();

            int start = _lastOrder == _section ? 1 : _lastOrder - (_section - 1);
            var dic = pairs.Where(x => x.Item1 >= start).ToDictionary(x => x.Item1, x => x.Item2);

            DicListView.Columns.Add("id", 45, HorizontalAlignment.Center);

            for (int i = 0; i < list.Count; i++)
            {
                string s = "c" + list[i];
                DicListView.Columns.Add(s, 45, HorizontalAlignment.Center);
            }

            DicListView.BeginUpdate();

            var last = dic.Values.Last();

            foreach (var key in dic.Keys)
            {
                var lvi = new ListViewItem(key.ToString()) { UseItemStyleForSubItems = false };
                var data = dic[key];

                foreach (int n in data)
                {
                    lvi.SubItems.Add(n.ToString());
                }

                if (last[0] == 0)
                {
                    if (last.SequenceEqual(data))
                    {
                        lvi.BackColor = Color.Cyan;
                    }
                }
                else
                {
                    if (data.Min() > 0)
                    {
                        lvi.BackColor = Color.Cyan;
                    }
                }

                DicListView.Items.Add(lvi);
            }

            DicListView.EndUpdate();
            DicListView.EnsureVisible(dic.Count - 1);
        }

        /// <summary>
        /// 조합무출의 한계치 이하인 컬렉션
        /// </summary>
        /// <param name="johapcollection">조합배열 컬렉션</param>
        /// <param name="token">토큰</param>
        /// <returns></returns>
        private IEnumerable<(int real, string name)> GetNoneCollection(IEnumerable<int[]> johapcollection, CancellationToken token)
        {
            foreach (int[] johapInts in johapcollection)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                var lists = _selectedPairs.Values.Select(x => johapInts.Select(y => x[y]).ToArray());

                //먼저 후방연속 갯수 파악하기
                int frtreal = lists.Reverse().TakeWhile(x => x.All(y => y == 0)).Count();

                if (frtreal >= _limit)
                {
                    var tpl = johapInts.Select(x => (Kiho.Gatum, 0)).ToList();
                    var (rc, mc) = NextReal.RealMaxCount(lists, tpl);

                    if (rc >= mc)
                    {
                        string s1 = string.Join(",", johapInts.Select(x => "c" + x.ToString()));
                        yield return (rc, s1);
                    }
                }

                progressBar1.Invoke((Action)delegate
                {
                    progressBar1.PerformStep();
                });
            }
        }

        /// <summary>
        /// 조합무출의 한계치 이하인 컬렉션
        /// </summary>
        /// <param name="johapcollection">조합배열 컬렉션</param>
        /// <param name="token">토큰</param>
        /// <returns>지연컬렉션(튜플(후방연속, 항목이름))</returns>
        private async IAsyncEnumerable<(int real, string name)> GetNoneCollectionAsync(IEnumerable<int[]> johapcollection, 
            [EnumeratorCancellation] CancellationToken token)
        {
            foreach (int[] johapInts in johapcollection)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                    break;
                }
                var lists = _selectedPairs.Values.Select(x => johapInts.Select(y => x[y]).ToArray());

                //먼저 후방연속 갯수 파악하기
                int frtreal = lists.Reverse().TakeWhile(x => x.All(y => y == 0)).Count();

                if (frtreal >= _limit)
                {
                    var tpl = johapInts.Select(x => (Kiho.Gatum, 0)).ToList();
                    var (rc, mc) = await Task.Run(() => NextReal.RealMaxCount(lists, tpl));

                    if (rc >= mc)
                    {
                        string s1 = string.Join(",", johapInts.Select(x => "c" + x.ToString()));
                        yield return (rc, s1);
                    }
                }

                progressBar1.Invoke((Action)delegate
                {
                    progressBar1.PerformStep();
                });
            }
        }

        /// <summary>
        /// 취소토큰 사용할 작업 테스크
        /// </summary>
        /// <param name="asyncEnumerable">지연컬렉션</param>
        /// <param name="token">토큰</param>
        /// <returns>리스트(튜플(후방연속, 항목이름))</returns>
        private static async Task<List<(int real, string name)>> Consume(IAsyncEnumerable<(int real, string name)> asyncEnumerable, 
            CancellationToken token)
        {
            var rst = new List<(int real, string name)>();
            await foreach (var (real, name) in asyncEnumerable.WithCancellation(token))
            {
                //단지 루프돌며 지연컬렉션항목을 새로운 컬렉션에 투영
                rst.Add((real, name));
            }
            return rst;
        }

        /// <summary>
        /// 조합유출의 한계치 이하인 컬렉션
        /// </summary>
        /// <param name="johapcollection">조합배열 컬렉션</param>
        /// <param name="token">토큰</param>
        /// <returns>지연컬렉션 (튜플(후방연속갯수, 패스된 항목이름))</returns>
        private IEnumerable<(int real, string name)> GetShownCollection(IEnumerable<int[]> johapcollection, CancellationToken token)
        {
            foreach (int[] johapInts in johapcollection)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
                var lists = _selectedPairs.Values.Select(x => johapInts.Select(y => x[y]).ToArray());

                //먼저 후방연속 갯수 파악하기
                int frtreal = lists.Reverse().TakeWhile(x => x.All(y => y >= 1)).Count();

                if (frtreal >= _limit)
                {
                    var tpl = johapInts.Select(x => (Kiho.Isang, 1)).ToList();
                    var (rc, mc) = NextReal.RealMaxCount(lists, tpl);

                    if (rc >= mc)
                    {
                        string s1 = string.Join(",", johapInts.Select(x => "c" + x.ToString()));
                        yield return (rc, s1);
                    }
                }

                progressBar1.Invoke((Action)delegate
                {
                    progressBar1.PerformStep();
                });
            }
        }

        /// <summary>
        /// 인덱스에 해당하는 번호를 반환
        /// </summary>
        /// <param name="indexInt">인덱스 배열</param>
        /// <returns></returns>
        private List<int> GetIndexData(IEnumerable<int> indexInt)
        {
            var list = new List<int>();

            var nums = _selectedPairs.First().Value.Length > 200 ? _threeDatas : _fiveDatas;

            foreach (var i in indexInt)
            {
                var data = nums.ToList()[i];
                list.AddRange(data);
            }

            list.Sort();
            return list;
        }

        /// <summary>
        /// 3열 데이터 출수 전체 데이터
        /// </summary>
        /// <returns>지연컬렉션(배열)</returns>
        private static IEnumerable<int[]> ShownAllThreeDatas()
        {
            using var db = new LottoDBContext();
            var datas = db.NonChulsuTbl.Select(x => x.Nonthree);
            foreach (string s1 in datas)
            {
                string s = s1.Trim();

                if (!string.IsNullOrEmpty(s))
                {
                    var array = StringToArray(s, ' ');
                    yield return array;
                }
            }
        }

        /// <summary>
        /// 5열 데이터 출수 전체 데이터
        /// </summary>
        /// <returns>지연컬렉션(배열)</returns>
        private static IEnumerable<int[]> ShownAllFiveDatas()
        {
            using var db = new LottoDBContext();
            var datas= db.NonChulsuTbl.Select(x => x.Nonfive);
            foreach (string s1 in datas)
            {
                string s = s1.Trim();

                if (!string.IsNullOrEmpty(s))
                {
                    var array = StringToArray(s, ' ');
                    yield return array;
                }
            }
        }

        /// <summary>
        /// 파일을 읽어와 배열컬렉션으로 반환
        /// </summary>
        /// <param name="path">파일경로</param>
        /// <returns>지연컬렉션(배열)</returns>
        private static IEnumerable<int[]> LinesToCollection(string path)
        {
            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {
                var array = StringToArray(line);
                yield return array;
            }
        }

        /// <summary>
        /// 문자열 라인을 배열로 반환
        /// </summary>
        /// <param name="line">문자열 라인</param>
        /// <param name="ch">구분자</param>
        /// <returns>정수배열</returns>
        /// <exception cref="Exception"></exception>
        private static int[] StringToArray(string line, char ch = ',')
        {
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
            {
                throw new Exception("정수배열로 변환오류");
            }

            if (ch.Equals(','))
            {
                var split = line.Split(ch).Select(x => int.Parse(x));
                return split.ToArray();
            }
            else
            {
                var split = line.Select(x => int.Parse(x.ToString()));
                return split.ToArray();
            }
        }


    }
}
