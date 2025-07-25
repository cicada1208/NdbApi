using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ch_rel2")]
    public class Ch_rel2
    {
        [Key]
        public int? chrel2_pat_no { get; set; }

        [Key]
        public string chrel2_itm_cd { get; set; }

        [Key]
        public string chrel2_dpt_no { get; set; }

        [Key]
        public string chrel2_cls_cd { get; set; }

        [Key]
        public int? chrel2_pr_seq { get; set; }

        [Key]
        public int? chrel2_ip_date { get; set; }

        public string chrel2_ctm_value { get; set; }

        public int? chrel2_rp_date { get; set; }

        public int? chrel2_rp_time { get; set; }

        public int? chrel2_rp_date_v { get; set; }

        public int? chrel2_rp_time_v { get; set; }

        public int? chrel2_ck_date { get; set; }

        public int? chrel2_ck_time { get; set; }

        public int? chrel2_ck_date_v { get; set; }

        public int? chrel2_ck_time_v { get; set; }

        public int? chrel2_ac_date { get; set; }

        public int? chrel2_ac_time { get; set; }

        public string chrel2_ip_no { get; set; }

        public string chrel2_ip_man { get; set; }

        public string chrel2_filler { get; set; }

        public decimal? A4GLIdentity { get; set; }

        [NotMapped]
        public string chrel2_ctm_unit { get; set; }

    }
}

