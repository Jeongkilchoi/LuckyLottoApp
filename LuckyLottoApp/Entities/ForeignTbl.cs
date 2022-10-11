using System.ComponentModel.DataAnnotations;

namespace LuckyLottoApp.Entities
{
    public class ForeignTbl
    {
        [Key]
        public int Orders { get; set; }
        public int Aus1 { get; set; }
        public int Aus2 { get; set; }
        public int Aus3 { get; set; }
        public int Aus4 { get; set; }
        public int Aus5 { get; set; }
        public int Aus6 { get; set; }
        public int Aus7 { get; set; }
        public int Aus8 { get; set; }
        public int Can1 { get; set; }
        public int Can2 { get; set; }
        public int Can3 { get; set; }
        public int Can4 { get; set; }
        public int Can5 { get; set; }
        public int Can6 { get; set; }
        public int Can7 { get; set; }
    }
}
