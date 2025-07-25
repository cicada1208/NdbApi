using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mi_mbed")]
    public class Mi_mbed_PatDpt : Mi_mbed_Ext
    {
        /// <summary>
        /// 該床病人科別
        /// </summary>
        [NotMapped]
        public string bed_pat_dpt { get; set; }

        /// <summary>
        /// 該床病人出院日期
        /// </summary>
        [NotMapped]
        public int? ipd_out_dt { get; set; }

        /// <summary>
        /// 轉入註記：病人科別
        /// </summary>
        [NotMapped]
        public string bed_i_pat_dpt { get; set; }

    }
}
