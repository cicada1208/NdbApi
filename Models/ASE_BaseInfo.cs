using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_ASE_BaseInfo")]
    public class ASE_BaseInfo
    {
        [Key]
        public string ptEncounterID { get; set; }

        [Key]
        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string AdmissionDate { get; set; }

        public string AdmissionDiagram { get; set; }

        public string AdmissionType { get; set; }

        public string AdmissionWay { get; set; }

        public string EducationLevel { get; set; }

        public string UsedLanguage { get; set; }

        public string Career1 { get; set; }

        public string Career2 { get; set; }

        public string NonContact { get; set; }

        public string InfoFrom { get; set; }

        public string CareGiverInHospital { get; set; }

        public string Marriage { get; set; }

        public string FamilyTreeSave { get; set; }

        public string FamilyRecord { get; set; }

        public string NonDrafReason { get; set; }

        public string NonFamilyDisHis { get; set; }

        public string NonDisHis { get; set; }

        public string NonOperation { get; set; }

        public string NonDrugAllergyHis { get; set; }

        /// <summary>
        /// 其他藥物過敏
        /// </summary>
        public string OtherAllergy { get; set; }

        public string FoodAllergy { get; set; }

        public string CheckAllergyHis { get; set; }

        public string AvoidTreatRegion { get; set; }

        public string SpecNote { get; set; }

        public string MD_MAN { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        /// <summary>
        /// 其他過敏
        /// </summary>
        public string OthAllergy { get; set; }

        public string EndTrtRsn { get; set; }

        public string AdmissionRsn { get; set; }

        public string ExpectPt { get; set; }

        public string ExpectFm { get; set; }

        public string TransType { get; set; }

        public string PatAdd { get; set; }

        public string AdmissionTypeTr { get; set; }

    }
}

