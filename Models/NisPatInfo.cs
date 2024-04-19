using System.Collections.Generic;

namespace Models
{
    public class NisPatInfo
    {
        #region ni_PtEncounter

        /// <summary>
        /// NIS住院序號
        /// </summary>
        public string ptEncounterId { get; set; }

        /// <summary>
        /// 最新HIS住院序號
        /// </summary>
        public string hisIpdNo { get; set; }

        /// <summary>
        /// 入院日
        /// </summary>
        public string BeginDt { get; set; }

        /// <summary>
        /// 出院日
        /// </summary>
        public string EndDt { get; set; }

        /// <summary>
        /// 住院天數
        /// </summary>
        public int? Days { get; set; }

        public string Vs1Name { get; set; }

        public string Vs1No { get; set; }

        public string Vs2Name { get; set; }

        public string Vs2No { get; set; }

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

        #endregion

        #region ni_HisPatient

        public string PatNo { get; set; }

        public string PatIdNo { get; set; }

        public string PatName { get; set; }

        public string PatBirth { get; set; }

        public string PatAge { get; set; }

        public string PatGender { get; set; }

        public string PatBloodType { get; set; }

        public string PatBloodRh { get; set; }

        #endregion

        #region ni_Bed

        public string clinicalUnitId { get; set; }

        public string clinicalHisId { get; set; }

        public string bedId { get; set; }

        public string bedLabel { get; set; }

        public string bedLabelClinical { get; set; }

        public string bedLabelNo { get; set; }

        #endregion

        /// <summary>
        /// 是否為ICU
        /// </summary>
        public bool? Icu { get; set; }

        /// <summary>
        /// 運送等級
        /// </summary>
        public string TsmitGrade { get; set; }

        /// <summary>
        /// DNR
        /// </summary>
        public List<Ch_dnr> DNR { get; set; }

        /// <summary>
        /// 血液感染
        /// </summary>
        public bool? Blood { get; set; }

        /// <summary>
        /// 病人過敏藥物
        /// </summary>
        public List<Allergy> Allergy { get; set; }

        /// <summary>
        /// 跌倒總分
        /// </summary>
        public int? FallTotalGrade { get; set; }

        /// <summary>
        /// 壓力性損傷總分
        /// </summary>
        public int? BedSoresTotalGrade { get; set; }

        /// <summary>
        /// 咳嗽
        /// </summary>
        public bool? Cough { get; set; }

        /// <summary>
        /// 發燒
        /// </summary>
        public bool? Fever { get; set; }

        /// <summary>
        /// 隔離(傳染性)
        /// </summary>
        public bool? Infectious { get; set; }

        /// <summary>
        /// 出院準備服務高危險群篩選表及轉介資訊
        /// </summary>
        public string DHRL { get; set; }

        /// <summary>
        /// 情緒壓力篩檢表及轉介資訊
        /// </summary>
        public string ESS { get; set; }

        /// <summary>
        /// CAS及轉介資訊
        /// </summary>
        public string CAS { get; set; }

        /// <summary>
        /// Endo/Tr.未結管路
        /// </summary>
        public bool? EndoTr { get; set; }

        /// <summary>
        /// CVC未結管路
        /// </summary>
        public string CVC { get; set; }

        /// <summary>
        /// Foley未結管路
        /// </summary>
        public string Foley { get; set; }

        /// <summary>
        /// Chest tube未結管路
        /// </summary>
        public string ChestTube { get; set; }

        /// <summary>
        /// DL./Hickman未結管路
        /// </summary>
        public bool? DLHick { get; set; }

        /// <summary>
        /// O2
        /// </summary>
        public string O2 { get; set; }

        /// <summary>
        /// 約束
        /// </summary>
        public bool? Restraint { get; set; }

        /// <summary>
        /// 轉入轉出類別
        /// </summary>
        public string TransferType { get; set; }

        /// <summary>
        /// 轉入轉出床號
        /// </summary>
        public string TransferBedLabel { get; set; }

        /// <summary>
        /// 傳送工具
        /// </summary>
        public string TransferTool { get; set; }

        /// <summary>
        /// RASS
        /// </summary>
        public int? RASS { get; set; }

        /// <summary>
        /// 是否譫妄 Y:譫妄 N:沒有譫妄 U:無法評估
        /// </summary>
        public string CAM { get; set; }

        /// <summary>
        /// 疼痛
        /// </summary>
        public string Pain { get; set; }

        /// <summary>
        /// HR 脈搏
        /// </summary>
        public int? HR { get; set; }

        /// <summary>
        /// AP-II
        /// </summary>
        public string AP2 { get; set; }

        /// <summary>
        /// 前一日I/O差值
        /// </summary>
        public string T_IODif_T_Yesterday { get; set; }

    }
}
