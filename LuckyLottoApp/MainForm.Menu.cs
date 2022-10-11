using LuckyLottoApp.Forms;
using LuckyLottoLibrary;

namespace LuckyLottoApp
{
    public partial class MainForm
    {
        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var item = e.ClickedItem.Text;

            if (!item.Equals("파 일"))
            {
                panel3.Visible = false;
                panel2.Visible = true;
            }
        }

        #region 파일 메뉴
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ConditionMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private async void MakeBasicFileMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
            FixedList.AddRange(Array.Empty<int>());
            ExceptList.AddRange(Array.Empty<int>());
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            StopButton.Enabled = true;
            PercentStatusLabel.Text = "데이터 준비중...";
            int cnt = (int)SetCountNumericUpDown.Value;
            await Task.Run(() => ReadCombine());
            toolStripProgressBar1.Maximum = cnt;

            try
            {
                var progress = new Progress<int>(value =>
                {
                    double val = value * 100.0 / cnt;
                    toolStripProgressBar1.PerformStep();
                    PercentStatusLabel.Text = val.ToString("F2") + "%";
                });
                var rst = await MakeOfPassedBasicConditon(cnt, progress, token);
                token.ThrowIfCancellationRequested();
                await Task.Delay(1000);
                toolStripProgressBar1.Value = 0;
                string path = Application.StartupPath + @"\DataFiles\alldang.csv";
                string s = string.Join(Environment.NewLine, rst.Select(x => LuckyLibrary.ListToString(x)));
                File.WriteAllText(path, s);
                await Task.Delay(1000);

                //파일복사
                string destpath2 = @"D:\Git\GoldenLotto\GoldenLotto\bin\Debug\Data\alldang.csv";
                string destpath3 = @"D:\Git\PerfectLottoApp\PerfectLottoApp\bin\Debug\DataFiles\alldang.csv";
                string destpath4 = @"D:\Study\LottoMethod\LottoMethordApp\LottoMethordApp\bin\Debug\Data\alldang.csv";

                File.Copy(path, destpath2, true);
                File.Copy(path, destpath3, true);
                File.Copy(path, destpath4, true);

                MessageBox.Show("작업완료");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("작업 취소됨.");
            }
            catch (IOException oe)
            {
                MessageBox.Show(oe.Message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _cts?.Dispose();
                StopButton.Enabled = false;
                toolStripProgressBar1.Value = 0;
                PercentStatusLabel.Text = string.Empty;
            }
        }
        #endregion

        #region 데이터베이스 메뉴
        //데이터베이스 테이블에 데이터 삽입
        private void SqlInsertMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<DataInsertForm>().Any())
            {
                var frm = new DataInsertForm();
                frm.Show();
            }
        }
        //데이터베이스 테이블 데이터 조회
        private void SqlQueryMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<DataQueryForm>().Any())
            {
                var frm = new DataQueryForm();
                frm.Show();
            }
        }
        //시퀸스 이격으로 격자데이터 조회
        private void GapGridDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<GapQueryForm>().Any())
            {
                var frm = new GapQueryForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터 요소갯수 3,5 검사
        private void Element35MenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<SamOhDataForm>().Any())
            {
                var frm = new SamOhDataForm(_lastOrder);
                frm.Show();
            }
        }
        //당번 데이터 사선,직선 검사
        private void SaJikseonMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<SequenceGapForm>().Any())
            {
                var frm = new SequenceGapForm(_lastOrder);
                frm.Show();
            }
        }
        //당번출현 누적갯수로 행별검사
        private void EachRowShownMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<HaengByeolForm>().Any())
            {
                var frm = new HaengByeolForm(_lastOrder);
                frm.Show();
            }
        }
        //당번출현 회차 비교방식으로 회기, 주기검사
        private void HoikijukiMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<HoeikijukiForm>().Any())
            {
                var frm = new HoeikijukiForm(_lastOrder);
                frm.Show();
            }
        }
        //당번의 구간출현 간격 검사
        private void ShownGapMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ChulKankyeokForm>().Any())
            {
                var frm = new ChulKankyeokForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터의 이동평균 검사
        private void MoveAvgMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<MoveAvgForm>().Any())
            {
                var frm = new MoveAvgForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터의 캔들차트 검사
        private void CandleChartMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ChulsuCandleForm>().Any())
            {
                var frm = new ChulsuCandleForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터의 여러항목 검사
        private void MultipleCheckMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 자료 조합검사
        //고정수 단순회귀 검사
        private void SimpleRegressionMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<LinearForm>().Any())
            {
                var frm = new LinearForm(_lastOrder);
                frm.Show();
            }
        }
        //당번 출현간격 회귀검사
        private void DangbeonRegressionMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<DangbeonRegressionForm>().Any())
            {
                var frm = new DangbeonRegressionForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터 자료요소 7-15 회귀검사
        private void DataRegressionMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ChulsuRegressionForm>().Any())
            {
                var frm = new ChulsuRegressionForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터 마코프확률 검사
        private void DataMarkovMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<MarkovProcessForm>().Any())
            {
                var frm = new MarkovProcessForm();
                frm.Show();
            }
        }
        //격자데이터 종횡5, 종횡7, 종횡9 쿼리검사
        private void FixedQueryMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<QueryFixDataForm>().Any())
            {
                var frm = new QueryFixDataForm();
                frm.Show();
            }
        }
        //외곽선프레임 실수데이터 검사
        private void OutFrameMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<OutlineFrameForm>().Any())
            {
                var frm = new OutlineFrameForm(_lastOrder);
                frm.Show();
            }
        }
        //당번 출현 도트타입 검사
        private void DangDotTypeMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ChulsuDisitalForm>().Any())
            {
                var frm = new ChulsuDisitalForm(_lastOrder);
                frm.Show();
            }
        }
        //데이터 폴리곤 픽셀검사
        private void PolygonPixelMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<PolygonPixelForm>().Any())
            {
                var frm = new PolygonPixelForm(_lastOrder);
                frm.Show();
            }
        }
        #endregion

        #region 자료 필터검사
        //출수데이터 필터
        private void ChulsuDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ChulsuFilterForm>().Any())
            {
                var frm = new ChulsuFilterForm(_lastOrder);
                frm.Show();
            }
        }
        //고정데이터 필터
        private void KojeongDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<FixChulsuFilterForm>().Any())
            {
                var frm = new FixChulsuFilterForm(_lastOrder);
                frm.Show();
            }
        }
        //열별데이터 필터
        private void YeolbyeolDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<NonChulsuFilterForm>().Any())
            {
                var frm = new NonChulsuFilterForm(_lastOrder);
                frm.Show();
            }
        }
        //출합데이터 필터
        private void ChulhapDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<HapChulsuForm>().Any())
            {
                var frm = new HapChulsuForm(_lastOrder);
                frm.Show();
            }
        }
        //내부상자 자료검사
        private void BoxDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<InnerBoxForm>().Any())
            {
                var frm = new InnerBoxForm();
                frm.Show();
            }
        }
        //출수타입 자료검사
        private void TypeDataMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ChulTypeForm>().Any())
            {
                var frm = new ChulTypeForm(_lastOrder);
                frm.Show();
            }
        }
        #endregion

        #region 자료 통계검사

        #endregion
    }
}
