using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_VSMED")]
    public class VSMED
    {
        [Key]
        public string REC_NO { get; set; }

        [Key]
        public string SEQ_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string ptOrderId { get; set; }

        public string MedPRS { get; set; }

        public string OrderLabel { get; set; }

        public string OrderNote { get; set; }

        public string Dose { get; set; }

        public string DoseDe { get; set; }

        public string DoseUnit { get; set; }

        public string Route { get; set; }

        public string Freq { get; set; }

        public string ExeDt { get; set; }

        public string OrderStatus { get; set; }

        public string FreeNote { get; set; }

        public string FreeDose { get; set; }

        public string FreeUnit { get; set; }

        public string Item3 { get; set; }

        public string Item4 { get; set; }

        public string Item5 { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_NAME { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

    }
}

