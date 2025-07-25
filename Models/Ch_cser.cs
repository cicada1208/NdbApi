using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_cser")]
    public class Ch_cser
    {
        [Key]
        public string cser_t_class { get; set; }

        [Key]
        public int? cser_ipd_dt { get; set; }

        [Key]
        public short? cser_ipd_seq { get; set; }

        [Key]
        public int? cser_t_dt { get; set; }

        public int? cser_t_dt_v { get; set; }

        public string cser_mark { get; set; }

        public string cser_print { get; set; }

        public int? cser_pat_no { get; set; }

        public string cser_pat_na { get; set; }

        public string cser_pat_sex { get; set; }

        public int? cser_pat_birthday { get; set; }

        public string cser_station { get; set; }

        public string cser_cost_center { get; set; }

        public string cser_bed_room { get; set; }

        public string cser_bed_no { get; set; }

        public string cser_disability { get; set; }

        public string cser_calm { get; set; }

        public string cser_mj_dr { get; set; }

        public string cser_ipd_idzs1 { get; set; }

        public string cser_dpt { get; set; }

        public int? cser_in_dt { get; set; }

        public int? cser_out_dt { get; set; }

        public decimal? cser_tall { get; set; }

        public decimal? cser_weight { get; set; }

        public string cser_pright { get; set; }

        public string cser_ut_des { get; set; }

        public string cser_t_user { get; set; }

        public string cser_t_udep { get; set; }

        public string cser_t_post { get; set; }

        public int? cser_t_time { get; set; }

        public string cser_r_user { get; set; }

        public int? cser_r_dt { get; set; }

        public int? cser_r_time { get; set; }

        public string cser_fill { get; set; }

        public decimal? A4GLIdentity { get; set; }

    }
}

