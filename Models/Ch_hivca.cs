using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_hivca")]
    public class Ch_hivca
    {
        [Key]
        public string hivca_cn_sys { get; set; }

        [Key]
        public int? hivca_cn_dt { get; set; }

        [Key]
        public int? hivca_cn_pat { get; set; }

        public string hivca_vpn_caseno { get; set; }

        public string hivca_careman_id { get; set; }

        public string hivca_careman_na { get; set; }

        public string hivca_status { get; set; }

        public string hivca_stage { get; set; }

        public int? hivca_birthday { get; set; }

        public string hivca_pat_name { get; set; }

        public string hivca_id { get; set; }

        public string hivca_sex { get; set; }

        public int? hivca_open_dt { get; set; }

        public string hivca_open_dr { get; set; }

        public string hivca_country { get; set; }

        public string hivca_edu { get; set; }

        public string hivca_job { get; set; }

        public string hivca_marry { get; set; }

        public short? hivca_child { get; set; }

        public string hivca_religion { get; set; }

        public string hivca_contact1 { get; set; }

        public string hivca_contact2 { get; set; }

        public string hivca_contact3 { get; set; }

        public string hivca_addr_no_1 { get; set; }

        public string hivca_addr_1 { get; set; }

        public string hivca_addr_no_2 { get; set; }

        public string hivca_addr_2 { get; set; }

        public int? hivca_end_dt { get; set; }

        public string hivca_end_rec { get; set; }

        public int? hivca_labe_dt { get; set; }

        public string hivca_cd4 { get; set; }

        public string hivca_hiv { get; set; }

        public string hivca_family { get; set; }

        public string hivca_ca_yn { get; set; }

        public string hivca_md { get; set; }

        public int? hivca_md_dt { get; set; }

        public string hivca_danger_1 { get; set; }

        public string hivca_danger_2 { get; set; }

        public string hivca_danger_3 { get; set; }

        public string hivca_danger_4 { get; set; }

        public string hivca_danger_5 { get; set; }

        public string hivca_fill { get; set; }

    }
}

