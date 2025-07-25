using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_FallASE_Child")]
    public class FallASE_Child
    {
        [Key]
        public string REC_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string ASEItem_VER { get; set; }

        public string ASEItem1 { get; set; }

        public string ASEItem2 { get; set; }

        public string ASEItem3 { get; set; }

        public string ASEItem4 { get; set; }

        public string ASEItem5 { get; set; }

        public string ASEItem6 { get; set; }

        public string ASEItem7 { get; set; }

        public string ASEItem8 { get; set; }

        public string ASEItem9 { get; set; }

        public string ASEItem10 { get; set; }

        public string ASEItem11 { get; set; }

        public string ASEItem12 { get; set; }

        public string ASEItem13 { get; set; }

        public string ASEItem14 { get; set; }

        public string ASEItem15 { get; set; }

        public string ASETotalGrade { get; set; }

        public string ASE_Others { get; set; }

        public string UP_EMR { get; set; }

        public string LastEMRRecNo { get; set; }

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

