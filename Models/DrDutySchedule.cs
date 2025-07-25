namespace Models
{
    public class DrDutySchedule
    {
        /// <summary>
        /// 線別
        /// </summary>
        public string SchCode { get; set; }

        /// <summary>
        /// 線別名稱
        /// </summary>
        public string SchName { get; set; }

        /// <summary>
        /// 醫師身份
        /// </summary>
        public string DrType { get; set; }

        /// <summary>
        /// 醫師員編
        /// </summary>
        public string DrEmpNo { get; set; }

        /// <summary>
        /// 醫師姓名
        /// </summary>
        public string DrName { get; set; }

        /// <summary>
        /// 醫師 Mvpn
        /// </summary>
        public string Mvpn { get; set; }

    }
}
