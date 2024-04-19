using Params;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_VitalSign")]
    public class VitalSign 
    {
        [Key]
        public string REC_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string HBFreq { get; set; }

        public string HBType { get; set; }

        public string NonDoReason_HB { get; set; }

        public string BTHFreq { get; set; }

        public string NonDoReason_BTH { get; set; }

        public string SPO2Num { get; set; }

        public string NonDoReason_SPO2 { get; set; }

        public string OTType { get; set; }

        public string OTNum { get; set; }

        public string OTUnit { get; set; }

        public string VType { get; set; }

        public string VMode { get; set; }

        public string VTV { get; set; }

        public string VRR { get; set; }

        public string Voxygon { get; set; }

        public string VPEEP { get; set; }

        public string MAP { get; set; }

        public string ABP { get; set; }

        public string CVP { get; set; }

        public string CVPUnit { get; set; }

        public string ABP_MAP { get; set; }

        public string ICP { get; set; }

        public string CPP { get; set; }

        public string NonDoReason_CVP { get; set; }

        public string NoPain { get; set; }

        public string MovNum1 { get; set; }

        public string MovAmount1 { get; set; }

        public string MovType1 { get; set; }

        public string MovColor1 { get; set; }

        public string MovShape1 { get; set; }

        public string MovPipeLine1 { get; set; }

        public string NonDoReason_Mov1 { get; set; }

        public string MovNum2 { get; set; }

        public string MovAmount2 { get; set; }

        public string MovType2 { get; set; }

        public string MovColor2 { get; set; }

        public string MovShape2 { get; set; }

        public string MovPipeLine2 { get; set; }

        public string NonDoReason_Mov2 { get; set; }

        /// <summary>
        /// 特殊處置
        /// </summary>
        public string SpecialTr { get; set; }

        public string ComaScaleE { get; set; }

        public string ComaScaleV { get; set; }

        public string ComaScaleM { get; set; }

        public string ComaTotalGrade { get; set; }

        public string ArmMP_L { get; set; }

        public string ForeArmMP_L { get; set; }

        public string UpperArmMP_L { get; set; }

        public string LegMP_L { get; set; }

        public string ThighMP_L { get; set; }

        public string LowerLegMP_L { get; set; }

        public string ArmMP_R { get; set; }

        public string ForeArmMP_R { get; set; }

        public string UpperArmMP_R { get; set; }

        public string LegMP_R { get; set; }

        public string ThighMP_R { get; set; }

        public string LowerLegMP_R { get; set; }

        public string EPReaction_R { get; set; }

        public string EPSize_R { get; set; }

        public string EPReaction_L { get; set; }

        public string EPSize_L { get; set; }

        public string Height { get; set; }

        public string NonDoReason_Height { get; set; }

        public string Weight { get; set; }

        public string WeightUnit { get; set; }

        public string WeightSPMark { get; set; }

        public string NonDoReason_Weight { get; set; }

        public string BMI { get; set; }

        public string HeadPRFR { get; set; }

        public string NeckPRFR { get; set; }

        public string ChestPRFR { get; set; }

        public string BellyPRFR { get; set; }

        public string WaistPRFR { get; set; }

        public string ButtocksPRFR { get; set; }

        public string ArmPRFR_L { get; set; }

        public string ForeArmPRFR_L { get; set; }

        public string UpperArmPRFR_L { get; set; }

        public string ArmPRFR_R { get; set; }

        public string ForeArmPRFR_R { get; set; }

        public string UpperArmPRFR_R { get; set; }

        public string LegPRFR_L { get; set; }

        public string ThighPRFR_L { get; set; }

        public string LowerLegPRFR_L { get; set; }

        public string LegPRFR_R { get; set; }

        public string ThighPRFR_R { get; set; }

        public string LowerLegPRFR_R { get; set; }

        public string AnklePRFR_L { get; set; }

        public string AnklePRFR_R { get; set; }

        public string NonDoReason_PRFR { get; set; }

        public string PtPose { get; set; }

        public string VSMemo { get; set; }

        public string UP_EMR { get; set; }

        public string EMRRecNo { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string InstructorName { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        public string ScVO2 { get; set; }

        public string PCWP { get; set; }

        public string CO { get; set; }

        public string CI { get; set; }

        public string SVR { get; set; }

        public string PVR { get; set; }

        public string IAP { get; set; }

        public string FBReaction_R { get; set; }

        public string FBReaction_L { get; set; }

        public string ANReaction_R { get; set; }

        public string ANReaction_L { get; set; }

        public string OtherReaction_R { get; set; }

        public string OtherReaction_L { get; set; }

        public string OtherReaction_LName { get; set; }

        public string OtherReaction_RName { get; set; }

        public string EMR_YN { get; set; }

        public string PAP { get; set; }

        public string RASS { get; set; }

        public string NonDoReason_GCS { get; set; }

        public string CAM_ICU_SPT { get; set; }

        public string CAM_ICU_ATN { get; set; }

        public string CAM_ICU_CON { get; set; }

        public string CAM_ICU_THK { get; set; }

        /// <summary>
        /// 是否譫妄 Y:譫妄 N:沒有譫妄 U:無法評估
        /// </summary>
        public string CAM_ICU_RST { get; set; }

        public string ETCO2 { get; set; }

        public string SPO2Side { get; set; }

        public string ScaleVCode { get; set; }

        [NotMapped]
        public string REC_DTM_begin { get; set; }

        [NotMapped]
        public string REC_DTM_end { get; set; }

        /// <summary>
        /// 體溫
        /// </summary>
        [NotMapped]
        public string BT { get; set; }

        /// <summary>
        /// BP(MBP)
        /// </summary>
        [NotMapped]
        public string NBPMBP { get; set; }

        /// <summary>
        /// ABP(MAP)
        /// </summary>
        [NotMapped]
        public string ABPMAP { get; set; }

        /// <summary>
        /// 中心靜脈壓
        /// </summary>
        [NotMapped]
        public string CVPressure { get; set; }

        /// <summary>
        /// 疼痛
        /// </summary>
        [NotMapped]
        public string Pain { get; set; }

        /// <summary>
        /// 藥物
        /// </summary>
        [NotMapped]
        public string Med { get; set; }

        /// <summary>
        /// 脈博強度
        /// </summary>
        [NotMapped]
        public string Pulse { get; set; }

        /// <summary>
        /// 脈搏
        /// </summary>
        [NotMapped]
        public string HR { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        [NotMapped]
        public string RR { get; set; }

        /// <summary>
        /// SPO2
        /// </summary>
        [NotMapped]
        public string SPO2 { get; set; }

        /// <summary>
        /// 使用氧氣療法
        /// </summary>
        [NotMapped]
        public string OT { get; set; }

        /// <summary>
        /// 使用呼吸器
        /// </summary>
        [NotMapped]
        public string Ventilator { get; set; }

        /// <summary>
        /// 呼吸模式
        /// </summary>
        [NotMapped]
        public string OTVT { get; set; }

        /// <summary>
        /// 昏迷指數
        /// </summary>
        [NotMapped]
        public string GCS { get; set; }

        /// <summary>
        /// 肌力
        /// </summary>
        [NotMapped]
        public string MP { get; set; }

        /// <summary>
        /// 瞳孔反射 
        /// </summary>
        [NotMapped]
        public string EPReaction { get; set; }

        /// <summary>
        /// 今日排便
        /// </summary>
        [NotMapped]
        public string MovToday { get; set; }

        /// <summary>
        /// 昨日排便
        /// </summary>
        [NotMapped]
        public string MovYesterday { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        [NotMapped]
        public string Ht { get; set; }

        /// <summary>
        /// 體重
        /// </summary>
        [NotMapped]
        public string Wt { get; set; }

        /// <summary>
        /// 圍長
        /// </summary>
        [NotMapped]
        public string PRFR { get; set; }

    }
}

