using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_TubeMag")]
    public class TubeMag
    {
        [Key]
        public string REC_NO_TB { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string TBID { get; set; }

        public string PRSNO { get; set; }

        public string TBType { get; set; }

        public string TBMaterial { get; set; }

        public string TBSize { get; set; }

        public string TBRegion { get; set; }

        public string TBSite { get; set; }

        public string TBNumber { get; set; }

        public string FixLength { get; set; }

        public string FixUnit { get; set; }

        public string IfCharge { get; set; }

        public string TBMagMemo { get; set; }

        public string TBCareFreq { get; set; }

        public string TBCareFreqUnit { get; set; }

        public string NextChangeDTM { get; set; }

        public string TreatType { get; set; }

        public string TBTreatDTM { get; set; }

        public string TB_HISID { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_NAME { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string TBPlace { get; set; }

        public string TBEndReason { get; set; }

        /// <summary>
        /// A:氣管內管Endo/Tr.、B:中心靜脈導管CVC、C:留置導尿管Foley
        /// </summary>
        [NotMapped]
        public string TBKind { get; set; }

        [NotMapped]
        public string REC_DTM_begin { get; set; }

        [NotMapped]
        public string REC_DTM_end { get; set; }

        [NotMapped]
        public string TreatTypeName { get; set; }

    }
}

