using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mi_mbed")]
    public class Mi_mbed_PatInfo : Mi_mbed_Ext
    {
        /// <summary>
        /// 轉入註記：病人姓名
        /// </summary>
        [NotMapped]
        public string bed_i_pat_name { get; set; }

        /// <summary>
        /// 轉入註記：病人性別
        /// </summary>
        [NotMapped]
        public string bed_i_pat_sex { get; set; }
    }
}
