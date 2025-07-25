using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mt_mchk")]
    public class Mt_mchk
    {
        [Key]
        public string htchk_dpt_cd { get; set; }

        [Key]
        public string htchk_cls_cd { get; set; }

        [Key]
        public int? htchk_pr_seq { get; set; }

        [Key]
        public int? htchk_ip_date { get; set; }

        public string htchk_p_key { get; set; }

        public int? htchk_ip_date2 { get; set; }

        public string htchk_chk_no2 { get; set; }

        public int? htchk_pt_no { get; set; }

        public string htchk_pt_id { get; set; }

        public string htchk_pt_data { get; set; }

        public string htchk_ip_hcid { get; set; }

        public string htchk_ip_gpid { get; set; }

        public string htchk_ip_gpsw { get; set; }

        public string htchk_ip_no { get; set; }

        public string htchk_ip_data { get; set; }

        public string htchk_wk_sw { get; set; }

        public string htchk_data_ac { get; set; }

        public string htchk_data_rp { get; set; }

        public string htchk_data_oth { get; set; }

    }
}

