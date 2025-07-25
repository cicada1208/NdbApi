using Lib;
using Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class VitalSignRepository : NISDBBaseRepository<VitalSign>
    {
        /// <summary>
        /// 查詢 VitalSign
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢24hr內最新O2</para>
        /// <para>3: 依NIS住院序號，查詢24hr內最新CAM</para>
        /// <para>4: 依NIS住院序號，查詢24hr內最新HR</para>
        /// </summary>
        public async Task<ApiResult<List<VitalSign>>> GetVitalSign(VitalSign param, int option = 0)
        {
            string sql = string.Empty;
            string REC_DTM = string.Empty;
            StringBuilder fieldVal = new StringBuilder(string.Empty);

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    REC_DTM = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
                    sql = $@"
                    select top 1 * 
                    from ni_VitalSign
                    where ptEncounterID = @ptEncounterID
                    and REC_DTM > '{REC_DTM}'
                    and (SPO2Num <> '' or SPO2Side <> '' or OTType <> '' or OTNum <> '' 
                    or VMode <> '' or VTV <> '' or VRR <> '' or Voxygon <> '' or VPEEP <> '')
                    and isnull(REC_STATUS,'') <> 'D'
                    order by REC_DTM desc";
                    break;
                case 3:
                    REC_DTM = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
                    sql = $@"
                    select top 1 * 
                    from ni_VitalSign
                    where ptEncounterID = @ptEncounterID
                    and REC_DTM > '{REC_DTM}'
                    and CAM_ICU_RST <> ''
                    and isnull(REC_STATUS,'') <> 'D'
                    order by REC_DTM desc";
                    break;
                case 4:
                    REC_DTM = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
                    sql = $@"
                    select top 1 * 
                    from ni_VitalSign
                    where ptEncounterID = @ptEncounterID
                    and REC_DTM > '{REC_DTM}'
                    and HBFreq <> ''
                    and isnull(REC_STATUS,'') <> 'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VitalSign>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(vs =>
            {
                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.SPO2Num, "%");
                SetFieldVal(ref fieldVal, vs.SPO2Side);
                vs.SPO2 = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.OTType);
                SetFieldVal(ref fieldVal, vs.OTNum, vs.OTUnit);
                vs.OT = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.VMode);
                SetFieldVal(ref fieldVal, vs.VTV, "ml/次");
                SetFieldVal(ref fieldVal, vs.VRR, "次/分");
                SetFieldVal(ref fieldVal, vs.Voxygon, "%");
                SetFieldVal(ref fieldVal, vs.VPEEP, "mmHg");
                vs.Ventilator = fieldVal.ToString();
            });

            return new ApiResult<List<VitalSign>>(queryList);
        }

        /// <summary>
        ///  查詢 VitalSign 報表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號、評估日期區間</para>
        /// </summary>
        public async Task<ApiResult<VitalSignRpt>> GetVitalSignRpt(VitalSign param, int option = 0)
        {
            string sql = string.Empty;
            VitalSignRpt vsRpt = new VitalSignRpt();

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select * 
                    from ni_VitalSign
                    where ptEncounterID = @ptEncounterID
                    and REC_DTM between @REC_DTM_begin and @REC_DTM_end
                    and isnull(REC_STATUS,'') <> 'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VitalSign>(sql, param, schemaOnly: option != 1);
            var vsList = query.ToList();

            string REC_NO = string.Join(",", vsList.Select(vs => $"'{vs.REC_NO}'"));

            var btList = REC_NO.IsNullOrWhiteSpace() ? new List<VSBT>() :
                (await DB.VSBTRepository.GetVSBTRpt(new VSBT
                {
                    REC_NO = REC_NO
                }, 2)).Data;

            var bpList = REC_NO.IsNullOrWhiteSpace() ? new List<VSBP>() :
                (await DB.VSBPRepository.GetVSBPRpt(new VSBP
                {
                    REC_NO = REC_NO
                }, 2)).Data;

            var painList = REC_NO.IsNullOrWhiteSpace() ? new List<VSPain>() :
                (await DB.VSPainRepository.GetVSPainRpt(new VSPain
                {
                    REC_NO = REC_NO
                }, 2)).Data;

            var medList = REC_NO.IsNullOrWhiteSpace() ? new List<VSMED>() :
                (await DB.VSMEDRepository.GetVSMED(new VSMED
                {
                    REC_NO = REC_NO
                }, 2)).Data;

            var PulseList = REC_NO.IsNullOrWhiteSpace() ? new List<VSPulse>() :
                (await DB.VSPulseRepository.GetVSPulse(new VSPulse
                {
                    REC_NO = REC_NO
                }, 2)).Data;

            StringBuilder fieldVal = new StringBuilder(string.Empty);
            string bpVal = string.Empty;
            vsList.ForEach(vs =>
            {
                vs.REC_DTM = DateTimeUtil.ConvertAD(vs.REC_DTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");

                btList.Where(bt => bt.REC_NO == vs.REC_NO).ForEach(bt =>
                  {
                      if (!vs.BT.IsNullOrWhiteSpace())
                          vs.BT += Environment.NewLine;
                      if (bt.BTNum.IsNullOrWhiteSpace())
                          vs.BT += bt.NonDoReason_BT;
                      else
                          vs.BT += bt.BTType + bt.BTNum + bt.BTUnit;
                  });

                bpList.Where(bp => bp.REC_NO == vs.REC_NO).ForEach(bp =>
                  {
                      bpVal = string.Empty;

                      if (bp.BPType.IsNullOrWhiteSpace())
                          bp.BPType = "NBP";

                      if (bp.SBP.IsNullOrWhiteSpace() && bp.DBP.IsNullOrWhiteSpace())
                          bpVal = bp.NonDoReason_BP;
                      else
                          bpVal = $"{bp.BPPose}{bp.BPRegion}{bp.SBP}/{bp.DBP}({bp.MAP_MBP})";

                      if (bp.BPType == "NBP")
                      {
                          if (!vs.NBPMBP.IsNullOrWhiteSpace())
                              vs.NBPMBP += Environment.NewLine;
                          vs.NBPMBP += $"{bp.BPType}:{bpVal}";
                      }
                      else if (bp.BPType == "ABP")
                      {
                          if (!vs.ABPMAP.IsNullOrWhiteSpace())
                              vs.ABPMAP += Environment.NewLine;
                          vs.ABPMAP += $"{bp.BPType}:{bpVal}";
                      }
                  });

                painList.Where(pain => pain.REC_NO == vs.REC_NO).ForEach(pain =>
                {
                    if (!vs.Pain.IsNullOrWhiteSpace())
                        vs.Pain += Environment.NewLine;

                    fieldVal.Clear();
                    SetFieldVal(ref fieldVal, pain.PainRegion);
                    SetFieldVal(ref fieldVal, pain.PainType, delimiter: "、");
                    SetFieldVal(ref fieldVal, pain.PainScale, delimiter: "、", nonDoReason: pain.NonDoReason_Pain);
                    vs.Pain += fieldVal.ToString();
                });

                medList.Where(med => med.REC_NO == vs.REC_NO).ForEach(med =>
                {
                    if (!vs.Med.IsNullOrWhiteSpace())
                        vs.Med += Environment.NewLine;

                    fieldVal.Clear();
                    SetFieldVal(ref fieldVal, med.OrderLabel);
                    SetFieldVal(ref fieldVal, med.FreeDose, med.FreeUnit, delimiter: " ");
                    vs.Med += fieldVal.ToString();
                });

                PulseList.Where(pulse => pulse.REC_NO == vs.REC_NO).ForEach(pulse =>
                {
                    if (!vs.Pulse.IsNullOrWhiteSpace())
                        vs.Pulse += Environment.NewLine;

                    fieldVal.Clear();
                    SetFieldVal(ref fieldVal, pulse.LeftArm, prefix: "左手", delimiter: "、");
                    SetFieldVal(ref fieldVal, pulse.RightArm, prefix: "右手", delimiter: "、");
                    SetFieldVal(ref fieldVal, pulse.LeftLeg, prefix: "左腳", delimiter: "、");
                    SetFieldVal(ref fieldVal, pulse.RightLeg, prefix: "右腳", delimiter: "、");
                    if (fieldVal.Length > 0)
                        vs.Pulse += $"{pulse.PulsePart}：{fieldVal.ToString()}";
                });

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.HBFreq, "次/分", vs.HBType == "心尖脈" ? vs.HBType : "",
                    nonDoReason: vs.NonDoReason_HB);
                vs.HR = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.BTHFreq, "次/分", nonDoReason: vs.NonDoReason_BTH);
                vs.RR = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.SPO2Num, "%");
                SetFieldVal(ref fieldVal, vs.SPO2Side, nonDoReason: vs.NonDoReason_SPO2);
                vs.SPO2 = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.OTType);
                SetFieldVal(ref fieldVal, vs.OTNum, vs.OTUnit);
                vs.OT = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.VMode);
                SetFieldVal(ref fieldVal, vs.VTV, "ml/次");
                SetFieldVal(ref fieldVal, vs.VRR, "次/分");
                SetFieldVal(ref fieldVal, vs.Voxygon, "%");
                SetFieldVal(ref fieldVal, vs.VPEEP, "mmHg");
                vs.Ventilator = fieldVal.ToString();

                vs.OTVT = $"{vs.OT}{(vs.OT.IsNullOrWhiteSpace() || vs.Ventilator.IsNullOrWhiteSpace() ? "" : Environment.NewLine)}{vs.Ventilator}";

                if (!vs.ComaTotalGrade.IsNullOrWhiteSpace())
                    vs.GCS = $"E{vs.ComaScaleE}M{vs.ComaScaleM}V{vs.ComaScaleV}";
                vs.GCS += vs.NonDoReason_GCS;

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.ArmMP_L, "分", "左手：");
                SetFieldVal(ref fieldVal, vs.ForeArmMP_L, "分", "左前臂：");
                SetFieldVal(ref fieldVal, vs.UpperArmMP_L, "分", "左上臂：");
                SetFieldVal(ref fieldVal, vs.ArmMP_R, "分", "右手：");
                SetFieldVal(ref fieldVal, vs.ForeArmMP_R, "分", "右前臂：");
                SetFieldVal(ref fieldVal, vs.UpperArmMP_R, "分", "右上臂：");
                SetFieldVal(ref fieldVal, vs.LegMP_L, "分", "左腳：");
                SetFieldVal(ref fieldVal, vs.ThighMP_L, "分", "左大腿：");
                SetFieldVal(ref fieldVal, vs.LowerLegMP_L, "分", "左小腿：");
                SetFieldVal(ref fieldVal, vs.LegMP_R, "分", "右腳：");
                SetFieldVal(ref fieldVal, vs.ThighMP_R, "分", "右大腿：");
                SetFieldVal(ref fieldVal, vs.LowerLegMP_R, "分", "右小腿：");
                vs.MP = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.EPReaction_L, "", "左眼反應：");
                SetFieldVal(ref fieldVal, vs.EPSize_L, "", "左眼大小：");
                SetFieldVal(ref fieldVal, vs.EPReaction_R, "", "右眼反應：");
                SetFieldVal(ref fieldVal, vs.EPSize_R, "", "右眼大小：");
                vs.EPReaction = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.CVP, vs.CVPUnit.IsNullOrWhiteSpace() ? "cmH2O" : vs.CVPUnit,
                    nonDoReason: vs.NonDoReason_CVP);
                vs.CVPressure = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.MovType1 == "正常" ? "" : vs.MovType1);
                SetFieldVal(ref fieldVal, vs.MovShape1);
                SetFieldVal(ref fieldVal, vs.MovColor1);
                SetFieldVal(ref fieldVal, vs.MovNum1, vs.MovType1 == "灌腸" ? "/E" : "/次");
                SetFieldVal(ref fieldVal, vs.MovAmount1, nonDoReason: vs.NonDoReason_Mov1);
                vs.MovYesterday = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.MovType2 == "正常" ? "" : vs.MovType1);
                SetFieldVal(ref fieldVal, vs.MovShape2);
                SetFieldVal(ref fieldVal, vs.MovColor2);
                SetFieldVal(ref fieldVal, vs.MovNum2, vs.MovType1 == "灌腸" ? "/E" : "/次");
                SetFieldVal(ref fieldVal, vs.MovAmount2, nonDoReason: vs.NonDoReason_Mov2);
                vs.MovToday = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.Height, "cm", nonDoReason: vs.NonDoReason_Height);
                vs.Ht = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.Weight, vs.WeightUnit, vs.WeightSPMark, nonDoReason: vs.NonDoReason_Weight);
                vs.Wt = fieldVal.ToString();

                fieldVal.Clear();
                SetFieldVal(ref fieldVal, vs.HeadPRFR, "", "頭圍：");
                SetFieldVal(ref fieldVal, vs.NeckPRFR, "", "頸圍：");
                SetFieldVal(ref fieldVal, vs.ChestPRFR, "", "胸圍：");
                SetFieldVal(ref fieldVal, vs.BellyPRFR, "", "腹圍：");
                SetFieldVal(ref fieldVal, vs.WaistPRFR, "", "腰圍：");
                SetFieldVal(ref fieldVal, vs.ButtocksPRFR, "", "臀圍：");
                SetFieldVal(ref fieldVal, vs.ArmPRFR_L, "", "左臂：");
                SetFieldVal(ref fieldVal, vs.ForeArmPRFR_L, "", "左前臂：");
                SetFieldVal(ref fieldVal, vs.UpperArmPRFR_L, "", "左上臂：");
                SetFieldVal(ref fieldVal, vs.ArmPRFR_R, "", "右臂：");
                SetFieldVal(ref fieldVal, vs.ForeArmPRFR_R, "", "右前臂：");
                SetFieldVal(ref fieldVal, vs.UpperArmPRFR_R, "", "右上臂：");
                SetFieldVal(ref fieldVal, vs.LegPRFR_L, "", "左腿：");
                SetFieldVal(ref fieldVal, vs.ThighPRFR_L, "", "左大腿：");
                SetFieldVal(ref fieldVal, vs.LowerLegPRFR_L, "", "左小腿：");
                SetFieldVal(ref fieldVal, vs.LegPRFR_R, "", "右腿：");
                SetFieldVal(ref fieldVal, vs.ThighPRFR_R, "", "右大腿：");
                SetFieldVal(ref fieldVal, vs.LowerLegPRFR_R, "", "右小腿：");
                SetFieldVal(ref fieldVal, vs.AnklePRFR_L, "", "左腳踝：");
                SetFieldVal(ref fieldVal, vs.AnklePRFR_R, "", "右腳踝：");
                vs.PRFR = fieldVal.ToString();

                if (vs.RASS.IsNumeric()) vs.RASS = (vs.RASS.ToInt() - 6).ToString(); // 代碼轉換成分數

                switch (vs.CAM_ICU_RST)
                {
                    case "Y":
                        vs.CAM_ICU_RST = "譫妄";
                        break;
                    case "N":
                        vs.CAM_ICU_RST = "沒有譫妄";
                        break;
                    case "U":
                        vs.CAM_ICU_RST = "無法評估";
                        break;
                }
            });

            vsRpt.VS = vsList;
            vsRpt.BT = btList.OrderBy(bt => bt.REC_DTM).ToList();
            vsRpt.BP = bpList.OrderBy(bp => bp.REC_DTM).ToList();
            vsRpt.Pain = painList.OrderBy(pain => pain.REC_DTM).ToList();

            return new ApiResult<VitalSignRpt>(vsRpt);
        }

        private void SetFieldVal(ref StringBuilder fieldVal, string setVal,
            string unit = "", string prefix = "", string suffix = "",
            string nonDoReason = "", string delimiter = "，")
        {
            if (!setVal.IsNullOrWhiteSpace())
                fieldVal.Append(fieldVal.IsNullOrWhiteSpace() ? "" : delimiter)
                    .Append($"{prefix}{setVal}{unit}{suffix}");
            else if (fieldVal.Length == 0 && !nonDoReason.IsNullOrWhiteSpace())
                fieldVal.Append(nonDoReason);
        }

        /// <summary>
        /// 查詢 VitalSign
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢24hr內最新RASS</para>
        /// </summary>
        public async Task<ApiResult<List<VitalSign>>> GetRASS(VitalSign param, int option = 0)
        {
            string sql = string.Empty;
            string REC_DTM = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    REC_DTM = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
                    sql = $@"
                    select top 1 * 
                    from ni_VitalSign
                    where ptEncounterID = @ptEncounterID
                    and REC_DTM > '{REC_DTM}'
                    and RASS <> ''
                    and isnull(REC_STATUS,'') <> 'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VitalSign>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(vs =>
            {
                if (vs.RASS.IsNumeric()) vs.RASS = (vs.RASS.ToInt() - 6).ToString(); // 代碼轉換成分數
            });

            return new ApiResult<List<VitalSign>>(queryList);
        }

    }
}
