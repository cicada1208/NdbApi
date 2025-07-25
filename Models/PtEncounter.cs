using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_PtEncounter")]
    public class PtEncounter
    {
        [Key]
        public string ptEncounterId { get; set; }

        public string ptEncMapId { get; set; }

        public string ptHisId { get; set; }

        public string BeginDt { get; set; }

        public string BeginTime { get; set; }

        public string EndDt { get; set; }

        public string EndTime { get; set; }

        public string ClinicalNo { get; set; }

        public string BedNo { get; set; }

        public string Vs1Name { get; set; }

        public string Vs2Name { get; set; }

        public string PatDept { get; set; }

        public string encNhiClass { get; set; }

        public string encSpecDiscount { get; set; }

        public string encAdmitSource { get; set; }

        public string encNhiPartial { get; set; }

        public string encDischargeReason { get; set; }

        public string encIsAdmitPublic { get; set; }

        public string encDiagNo1 { get; set; }

        public string encDiagCh1 { get; set; }

        public string encDiagEn1 { get; set; }

        public string encDiagNo2 { get; set; }

        public string encDiagCh2 { get; set; }

        public string encDiagEn2 { get; set; }

        public string encDiagNo3 { get; set; }

        public string encDiagCh3 { get; set; }

        public string encDiagEn3 { get; set; }

        public string encDiagNo4 { get; set; }

        public string encDiagCh4 { get; set; }

        public string encDiagEn4 { get; set; }

        public string encDiagNo5 { get; set; }

        public string encDiagCh5 { get; set; }

        public string encDiagEn5 { get; set; }

        public string odrDiag { get; set; }

        public string statusId { get; set; }

        public string dbId { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

    }
}

