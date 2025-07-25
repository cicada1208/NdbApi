using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_OPNSChkRec")]
    public class OPNSChkRec
    {
        [Key]
        public string REC_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string OPDT { get; set; }

        public string OPName { get; set; }

        public string PtTPR { get; set; }

        public string PtHW { get; set; }

        public string CHK_OPSP { get; set; }

        public string CHK_PTID { get; set; }

        public string CHK_Clean { get; set; }

        public string CHK_Consent1 { get; set; }

        public string CHK_Consent2 { get; set; }

        public string CHK_Chart { get; set; }

        public string OldChart { get; set; }

        public string CHK_PACS { get; set; }

        public string CHK_Blood { get; set; }

        public string BloodGroup { get; set; }

        public string RhType { get; set; }

        public string PreBlood_YN { get; set; }

        public string PreBlood_Memo { get; set; }

        public string LabRptEMR_YN { get; set; }

        public string LabRptEMR_Memo { get; set; }

        public string EKGOnEMR_YN { get; set; }

        public string CHK_EKGToChart1 { get; set; }

        public string CHK_EKGToChart2 { get; set; }

        public string CHK_OPSM { get; set; }

        public string OPSiteMark { get; set; }

        public string OnIV_YN { get; set; }

        public string IVMemo1 { get; set; }

        public string IVMemo2 { get; set; }

        public string NotifyAnes_YN { get; set; }

        public string MAR_YN { get; set; }

        public string MAR_Memo { get; set; }

        public string MedToOPR_YN { get; set; }

        public string MedToOPR_Memo { get; set; }

        public string NPO_YN { get; set; }

        public string NPOTime { get; set; }

        public string FoodContext { get; set; }

        public string FBBody_YN { get; set; }

        public string FBBody { get; set; }

        public string IDisease_YN { get; set; }

        public string IDisease { get; set; }

        public string OPStatus { get; set; }

        public string OPStatus_Memo { get; set; }

        public string ShowOPStatus_YN { get; set; }

        public string Memo { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_NAME { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string Anesthesia { get; set; }

        public string BeforeWash_YN { get; set; }

        public string BeforeWash { get; set; }

        public string BefShave { get; set; }

        public string BefShaveTool { get; set; }

        public string BefPartWash { get; set; }

        public string OPNO { get; set; }

    }
}

