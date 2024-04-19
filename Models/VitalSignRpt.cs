using System.Collections.Generic;

namespace Models
{
    public class VitalSignRpt 
    {
        /// <summary>
        /// VitalSign
        /// </summary>
        public List<VitalSign> VS { get; set; }

        /// <summary>
        /// 體溫
        /// </summary>
        public List<VSBT> BT { get; set; }

        /// <summary>
        /// 血壓
        /// </summary>
        public List<VSBP> BP { get; set; }

        /// <summary>
        /// 疼痛
        /// </summary>
        public List<VSPain> Pain { get; set; }
    }
}
