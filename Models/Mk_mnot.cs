using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mk_mnot")]
    public class Mk_mnot
    {
        [Key]
        public int? not_no_zd { get; set; }

        [Key]
        public int? not_no_dt { get; set; }

        [Key]
        public short? not_no_nm { get; set; }

        public string not_no_v { get; set; }

        public int? not_dz_no { get; set; }

        public string not_serg_sta { get; set; }

        public int? not_preop_dt { get; set; }

        public short? not_preop_stm { get; set; }

        public string not_prerm_ctr { get; set; }

        public string not_prerm_no { get; set; }

        public string not_rel_dpt { get; set; }

        public string not_mdtr { get; set; }

        public int? not_or_dt { get; set; }

        public short? not_or_stm { get; set; }

        public string not_or_ctr { get; set; }

        public string not_or_no { get; set; }

        public int? not_add_dt { get; set; }

        public short? not_add_tm { get; set; }

        public string not_come_cd { get; set; }

        public string not_preop_turn { get; set; }

        public string not_group_1 { get; set; }

        public string not_group_2 { get; set; }

        public string not_group_3 { get; set; }

        public string not_group_4 { get; set; }

        public string not_group_5 { get; set; }

        public string not_filler1 { get; set; }

    }
}

