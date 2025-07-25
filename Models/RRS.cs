using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_RRS")]
    public class RRS
    {
        [Key]
        public string REC_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string DOC_TYPE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string BedNo { get; set; }

        public string DNR { get; set; }

        public string Condition { get; set; }

        public string Trend1 { get; set; }

        public string Trend2 { get; set; }

        public string Trend3 { get; set; }

        public string Dr1 { get; set; }

        public string Dr2 { get; set; }

        public string Dr3 { get; set; }

        public string Dr4 { get; set; }

        public string SendType { get; set; }

        public string SendToName { get; set; }

        public string SendTo { get; set; }

        public string SendPhone { get; set; }

        public string SendShortPhone { get; set; }

        public string SendTime { get; set; }

        public string NurseStartTime { get; set; }

        public string NurseEndTime { get; set; }

        public string NurseKeyWord { get; set; }

        public string UP_REC { get; set; }

        public string REC_STATUS { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string DutyYes { get; set; }

        public string VS_REC_NO { get; set; }

        /// <summary>
        /// Âà¤¶¸ê°T
        /// </summary>
        [NotMapped]
        public string Info { get; set; }

    }
}

