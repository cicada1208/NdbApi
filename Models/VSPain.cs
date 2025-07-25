using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_VSPain")]
    public class VSPain
    {
        [Key]
        public string REC_NO { get; set; }

        [Key]
        public string SEQ_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string PainRegion { get; set; }

        public string PainType { get; set; }

        public string PainScale { get; set; }

        public string NonDoReason_Pain { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string EvaluationForm { get; set; }

        [NotMapped]
        public string REC_DTM { get; set; }


        [NotMapped]
        public string PainVal { get; set; }

    }
}

