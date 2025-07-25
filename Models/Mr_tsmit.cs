using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mr_tsmit")]
    public class Mr_tsmit
    {
        [Key]
        public string tsmit_key { get; set; }

        [Key]
        public int? tsmit_mod_dt { get; set; }

        [Key]
        public int? tsmit_mod_time { get; set; }

        [Key]
        public int? tsmit_seq { get; set; }

        public string tsmit_mod_date_v { get; set; }

        public int? tsmit_pat_no { get; set; }

        public string tsmit_ipd_no { get; set; }

        public string tsmit_cre_user { get; set; }

        public string tsmit_data { get; set; }

        public string tsmit_data1 { get; set; }

        public string tsmit_data2 { get; set; }

        [NotMapped]
        public string tsmit_data_grade_id { get; set; }

    }
}

