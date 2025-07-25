using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_Tube")]
    public class Tube
    {
        [Key]
        public string TBID { get; set; }

        public string PRSNO { get; set; }

        public string TBType { get; set; }

        public string TBMaterial { get; set; }

        public string TBSize { get; set; }

        public string TBCareFreq { get; set; }

        public string TBCareFreqUnit { get; set; }

        public string TBChangeFreq { get; set; }

        public string TBChangeFreqUnit { get; set; }

        public string BundleCare { get; set; }

        public string TBKind { get; set; }

        public string Item2 { get; set; }

        public string Item3 { get; set; }

        public string Item4 { get; set; }

        public string isActive { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

