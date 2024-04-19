namespace Models
{
    public class PatientOPListExt : PatientOPList
    {
        public string ptEncounterId { get; set; }

        public string BedNo { get; set; }

        public string PatName { get; set; }

        /// <summary>
        /// 手術同意書
        /// </summary>
        /// <remarks>Y or Empty</remarks>
        public string CHK_Consent1 { get; set; }

        /// <summary>
        /// 自費同意書
        /// </summary>
        /// <remarks>Y or Empty</remarks>
        public string CHK_Consent2 { get; set; }

        /// <summary>
        /// 麻醉前評估
        /// </summary>
        /// <remarks>1-麻醉前評估與麻醉計畫單;2-無此需要</remarks>
        public string AnesthesiaAse { get; set; }

        /// <summary>
        /// 手術部位標記(NIS)
        /// </summary>
        public string OPSiteMark { get; set; }

        /// <summary>
        /// 傳送工具
        /// </summary>
        public string TransferTool { get; set; }

        /// <summary>
        /// 傳送地點
        /// </summary>
        public string TransferSite { get; set; }

        /// <summary>
        /// 傳送派工時間 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string TransferDispatchTime { get; set; }

        ///// <summary>
        ///// 傳送完工時間 yyyy-MM-dd HH:mm:ss
        ///// </summary>
        //public string TransferCompletedTime { get; set; }

    }
}
