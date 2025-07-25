using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_VSBP")]
    public class VSBP
    {
        [Key]
        public string REC_NO { get; set; }

        [Key]
        public string SEQ_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string SBP { get; set; }

        public string DBP { get; set; }

        public string BPPose { get; set; }

        public string BPRegion { get; set; }

        public string NonDoReason_BP { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string BPType { get; set; }

        public string MAP_MBP { get; set; }

        [NotMapped]
        public string REC_DTM { get; set; }

    }
}

