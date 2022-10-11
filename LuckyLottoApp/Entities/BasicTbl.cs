using System.ComponentModel.DataAnnotations;

namespace LuckyLottoApp.Entities
{
    public class BasicTbl
    {
        [Key]
        public int Orders { get; set; }
        public int Gu1 { get; set; }
        public int Gu2 { get; set; }
        public int Gu3 { get; set; }
        public int Gu4 { get; set; }
        public int Gu5 { get; set; }
        public int Gu6 { get; set; }
        public int Bonus { get; set; }
        public int SunGu1 { get; set; }
        public int SunGu2 { get; set; }
        public int SunGu3 { get; set; }
        public int SunGu4 { get; set; }
        public int SunGu5 { get; set; }
        public int SunGu6 { get; set; }
        public int? Hoki { get; set; }
        public DateTime PobDate { get; set; }
    }
}
