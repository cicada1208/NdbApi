namespace Models
{
    public class BedInfo 
    {
        public string clinicalUnitId { get; set; }

        /// <summary>
        /// 總床數
        /// </summary>
        public int all { get; set; }

        /// <summary>
        /// 空床數
        /// </summary>
        public int empty { get; set; }

        /// <summary>
        /// 在床數
        /// </summary>
        public int inbed { get; set; }

        /// <summary>
        /// 加床
        /// </summary>
        public int add { get; set; }
    }
}
