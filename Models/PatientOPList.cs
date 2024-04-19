namespace Models
{
    public class PatientOPList
    {
        /// <summary>
        /// 病歷號
        /// </summary>
        public int? PATNO { get; set; }

        /// <summary>
        /// 手術日期 yyyMMdd
        /// </summary>
        public int? iOPdate { get; set; }

        /// <summary>
        /// 主刀醫師姓名
        /// </summary>
        public string DrName { get; set; }

        /// <summary>
        /// 主刀醫師科別
        /// </summary>
        public string DrSubject { get; set; }

        /// <summary>
        /// 部位標記
        /// </summary>
        public string OperationSite { get; set; }

        /// <summary>
        /// 術式
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string Anesthesia { get; set; }

        /// <summary>
        /// 手術地點
        /// </summary>
        public string OPStation { get; set; }

        /// <summary>
        /// 狀態代碼
        /// </summary>
        public string STATUSID { get; set; }

        /// <summary>
        /// 狀態 / 動態
        /// </summary>
        public string STATUS { get; set; }

        /// <summary>
        /// 恢復室時間 HH:mm
        /// </summary>
        public string PARTIME { get; set; }

        /// <summary>
        /// 手術預估時間 min
        /// </summary>
        public int? ExpectedTime { get; set; }

        /// <summary>
        /// 排定日期 yyyy/MM/dd
        /// </summary>
        public string OPStartDate { get; set; }

        /// <summary>
        /// 排定時間 HH:mm、time follow
        /// </summary>
        public string OPStartTime { get; set; }

        /// <summary>
        /// 開始日期時間 yyyy/MM/dd HH:mm:ss
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 上台刀：手術預估時間 min
        /// </summary>
        public int? PreExpectedTime { get; set; }

        /// <summary>
        /// 上台刀：開始日期時間 yyyy/MM/dd HH:mm:ss
        /// </summary>
        public string PreStartTime { get; set; }

        /// <summary>
        /// 急診級數
        /// </summary>
        public string ERClass { get; set; }

        /// <summary>
        /// 手術房間
        /// </summary>
        public string OPRoom { get; set; }

        /// <summary>
        /// 手術單號
        /// </summary>
        /// <remarks>not_no_zd+not_no_dt+not_no_nm (16碼)</remarks>
        public string NOTNO { get; set; }

        /// <summary>
        /// 麻諮門診是否完成
        /// </summary>
        public bool IsPAAComplete { get; set; }
    }
}
