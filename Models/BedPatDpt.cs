namespace Models
{
    public class BedPatDpt
    {
        /// <summary>
        /// 科別
        /// </summary>
        public string dpt { get; set; }

        /// <summary>
        /// 科別名稱
        /// </summary>
        public string dptName { get; set; }

        /// <summary>
        /// 在床數
        /// </summary>
        public int beds { get; set; }

        /// <summary>
        /// 出床數
        /// </summary>
        public int obeds { get; set; }

        /// <summary>
        /// 入床數
        /// </summary>
        public int ibeds { get; set; }
    }
}
