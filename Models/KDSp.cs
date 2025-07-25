using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_KDSp")]
    public class KDSp
    {
        [Key]
        public string ptEncounterID { get; set; }

        [Key]
        public string SEQ_NO { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string Sp_Time { get; set; }

        public string Sp_Item { get; set; }

        public string Sp_Value { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_Name { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

