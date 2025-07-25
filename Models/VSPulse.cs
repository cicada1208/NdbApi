using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_VSPulse")]
    public class VSPulse
    {
        [Key]
        public string REC_NO { get; set; }

        [Key]
        public string SEQ_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string PulsePart { get; set; }

        public string LeftArm { get; set; }

        public string RightArm { get; set; }

        public string LeftLeg { get; set; }

        public string RightLeg { get; set; }

        public string REC_STATUS { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

