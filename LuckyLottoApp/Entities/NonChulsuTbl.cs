using System.ComponentModel.DataAnnotations;

namespace LuckyLottoApp.Entities
{
    public class NonChulsuTbl
    {
        [Key]
        public int Orders { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }
        public string Col5 { get; set; }
        public string Col6 { get; set; }
        public string Col7 { get; set; }
        public string Col8 { get; set; }
        public string Col9 { get; set; }
        public string Col10 { get; set; }
        public string Col11 { get; set; }
        public string Col12 { get; set; }
        public string Col15 { get; set; }
        public string Nine { get; set; }
        public string Nonthree { get; set; }
        public string Nonfive { get; set; }
        public string Dgap { get; set; }
        public string Ihweol { get; set; } = null;
        public string Donggap { get; set; } = null;
        public string Yeonsok { get; set; } = null;
        public string Yeonkkeut { get; set; } = null;
    }
}
