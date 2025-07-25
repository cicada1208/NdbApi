using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_RestraintMag")]
    public class RestraintMag
    {
        [Key]
        public string REC_NO_RT { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string REC_BedNo { get; set; }

        public string RTReason { get; set; }

        public string RTRegion { get; set; }

        public string RTTool { get; set; }

        public string RTMedicine { get; set; }

        public string RecordNS { get; set; }

        public string RTEndDTM { get; set; }

        public string RTComplication { get; set; }

        public string CloseNS { get; set; }

        public string CloseBedNo { get; set; }

        public string RT_HISID { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_NAME { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        [NotMapped]
        public string REC_DTM_begin { get; set; }

        [NotMapped]
        public string REC_DTM_end { get; set; }

    }
}

