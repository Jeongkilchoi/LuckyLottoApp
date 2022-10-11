using System.ComponentModel.DataAnnotations;

namespace LuckyLottoApp.Entities
{
    public class InnerBoxTbl
    {
        [Key]
        public int Orders { get; set; }

        public string HHyeonsok { get; set; }

        public string HVyeonsok { get; set; }

        public string HFsaseon { get; set; }

        public string HRsaseon { get; set; }

        public string HFkiyeok { get; set; }

        public string HRkiyeok { get; set; }

        public string HFnieun { get; set; }

        public string HRnieun { get; set; }

        public string HUpkkeok { get; set; }

        public string HDnkkeok { get; set; }

        public string HLfkkeok { get; set; }

        public string HRfkkeok { get; set; }



        public string VHyeonsok { get; set; }

        public string VVyeonsok { get; set; }

        public string VFsaseon { get; set; }

        public string VRsaseon { get; set; }

        public string VFkiyeok { get; set; }

        public string VRkiyeok { get; set; }

        public string VFnieun { get; set; }

        public string VRnieun { get; set; }

        public string VUpkkeok { get; set; }

        public string VDnkkeok { get; set; }

        public string VLfkkeok { get; set; }

        public string VRfkkeok { get; set; }
    }
}
