namespace Models
{
    public class DisasterAssistance
    {
        /// <summary>
        /// 日常生活活動
        /// </summary>
        public string ADL { get; set; }

        /// <summary>
        /// 床號
        /// </summary>
        /// <remarks>逗號分隔</remarks>
        public string Beds { get; set; }
    }
}
