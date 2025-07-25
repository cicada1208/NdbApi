using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ch_tor")]
    public class Ch_tor
    {
        [Key]
        public int? chtor_server_dt { get; set; }

        [Key]
        public int? chtor_server_ti { get; set; }

        [Key]
        public int? chtor_pat_no { get; set; }

        [Key]
        public string chtor_item { get; set; }

        [Key]
        public string chtor_from_sys_1 { get; set; }

        [Key]
        public string chtor_from_sys_2 { get; set; }

        [Key]
        public decimal? chtor_ipd_no { get; set; }

        [Key]
        public short? chtor_ipd_seq { get; set; }

        [Key]
        public string chtor_filler1 { get; set; }

        public string chtor_del_mark { get; set; }

        public int? chtor_cre_dt { get; set; }

        public int? chtor_cre_ti { get; set; }

        public int? chtor_cre_dt_v { get; set; }

        public int? chtor_cre_ti_v { get; set; }

        public string chtor_value_type { get; set; }

        public string chtor_value_str { get; set; }

        public decimal? chtor_value_num { get; set; }

        public string chtor_unit { get; set; }

        public string chtor_memo { get; set; }

        public string chtor_cre_user { get; set; }

        public string chtor_cre_mj_dr { get; set; }

        public string chtor_upd_user { get; set; }

        public string chtor_upd_mj_dr { get; set; }

        public string chtor_filler2 { get; set; }

        [NotMapped]
        public string ptEncounterId { get; set; }

    }
}

