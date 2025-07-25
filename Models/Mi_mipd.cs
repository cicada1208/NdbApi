using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mi_mipd")]
    public class Mi_mipd
    {
        [Key]
        public string ipd_no { get; set; }

        public int? ipd_dt { get; set; }

        public int? ipd_pat_no { get; set; }

        public string ipd_name { get; set; }

        public string ipd_bed { get; set; }

        public int? ipd_out_dt { get; set; }

        public string ipd_alt { get; set; }

        public string ipd_no_v { get; set; }

        public int? ipd_dt_v { get; set; }

        public string ipd_del_mark { get; set; }

        public string ipd_mj_dr1 { get; set; }

        public int? ipd_apy_enddt { get; set; }

        public string ipd_group1 { get; set; }

        public string ipd_group2 { get; set; }

    }
}

