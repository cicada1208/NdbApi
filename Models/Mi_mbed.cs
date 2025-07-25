using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mi_mbed")]
    public class Mi_mbed
    {
        [Key]
        public string bed_bed { get; set; }

        public string bed_grd { get; set; }

        public string bed_dpt { get; set; }

        public string bed_unit { get; set; }

        public string bed_status { get; set; }

        /// <summary>
        /// 轉入註記
        /// </summary>
        public string bed_i_mark { get; set; }

        /// <summary>
        /// 轉出註記
        /// </summary>
        public string bed_o_mark { get; set; }

        public string bed_group1 { get; set; }

    }
}

