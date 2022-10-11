using System.Data;
using LuckyLottoApp.Entities;

namespace LuckyLottoApp.Forms
{
    /// <summary>
    /// 데이터 삽입 폼 클래스
    /// </summary>
    public partial class DataQueryForm : Form
    {
        #region Field

        private bool _isDesc = true;
        private readonly BindingSource _bindingSource = new();

        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public DataQueryForm()
        {
            InitializeComponent();
        }

        private void DataQueryForm_Load(object sender, EventArgs e)
        {
            TableNameComboBox.SelectedIndex = 0;
        }

        private void DescRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DescRadioButton.Checked)
                _isDesc = true;
            else
                _isDesc = false;
            
        }

        private void TableNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TableNameComboBox.SelectedIndex > -1)
            {
                try
                {
                    int sel = TableNameComboBox.SelectedIndex;
                    using var db = new LottoDBContext();

                    if (sel == 0)
                    {
                        var data = _isDesc ? db.BasicTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.BasicTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else if (sel == 1)
                    {
                        var data = _isDesc ? db.ForeignTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.ForeignTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else if (sel == 2)
                    {
                        var data = _isDesc ? db.AppearTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.AppearTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else if (sel == 3)
                    {
                        var data = _isDesc ? db.ChulsuTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.ChulsuTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else if (sel == 4)
                    {
                        var data = _isDesc ? db.TypeTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.TypeTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else if (sel == 5)
                    {
                        var data = _isDesc ? db.SingleTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.SingleTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else if (sel == 6)
                    {
                        var data = _isDesc ? db.FixChulsuTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.FixChulsuTbl.ToList();
                        _bindingSource.DataSource = data;
                    }
                    else
                    {
                        var data = _isDesc ? db.NonChulsuTbl.OrderByDescending(x => x.Orders).ToList() :
                                             db.NonChulsuTbl.ToList();
                        _bindingSource.DataSource = data;
                    }

                    QueryDataGridView.DataSource = _bindingSource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
