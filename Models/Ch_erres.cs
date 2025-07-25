using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_erres")]
    public class Ch_erres
    {
        /// <summary>
        /// 來源病歷號
        /// </summary>
        [Key]
        public int? erres_pat_no { get; set; }

        [Key]
        public string erres_ro_no { get; set; }

        /// <summary>
        /// 來源住院序號
        /// </summary>
        public string erres_ipd_no { get; set; }

        public string erres_status { get; set; }

        public string erres_sex { get; set; }

        public int? erres_reg_date { get; set; }

        public int? erres_reg_time { get; set; }

        public int? erres_reg_date_v { get; set; }

        public int? erres_reg_time_v { get; set; }

        public string erres_pre_dpt { get; set; }

        public string erres_pre_dr { get; set; }

        public string erres_pre_dr2 { get; set; }

        public string erres_pre_dr3 { get; set; }

        public string erres_rd_dr { get; set; }

        public string erres_idzs_1 { get; set; }

        public string erres_idzs_2 { get; set; }

        public string erres_idzs_3 { get; set; }

        public string erres_idzs_4 { get; set; }

        public string erres_idzs_5 { get; set; }

        public string erres_isolation_yn { get; set; }

        public string erres_isolation_type { get; set; }

        public string erres_isolation_message_1 { get; set; }

        public string erres_isolation_message_2 { get; set; }

        public string erres_isolation_message_3 { get; set; }

        public string erres_isolation_message_4 { get; set; }

        public string erres_isolation_message_5 { get; set; }

        public string erres_bed_type { get; set; }

        public string erres_bed_type_condition { get; set; }

        public int? erres_pre_date { get; set; }

        public int? erres_pre_time { get; set; }

        public int? erres_pre_date_v { get; set; }

        /// <summary>
        /// 預約床位
        /// </summary>
        public string erres_given_bed { get; set; }

        public string erres_grd_data_1 { get; set; }

        public string erres_grd_data_2 { get; set; }

        public string erres_grd_data_3 { get; set; }

        public int? erres_res_date { get; set; }

        public int? erres_res_time { get; set; }

        public string erres_res_user { get; set; }

        /// <summary>
        /// 需求床位等級
        /// </summary>
        public string erres_res_lv { get; set; }

        public int? erres_xxx_date { get; set; }

        public int? erres_xxx_time { get; set; }

        public string erres_xxx_user { get; set; }

        public string erres_vip_level { get; set; }

        public string erres_vip_message_1 { get; set; }

        public string erres_vip_message_2 { get; set; }

        public string erres_vip_message_3 { get; set; }

        public string erres_vip_message_4 { get; set; }

        public string erres_vip_message_5 { get; set; }

        public string erres_message_1 { get; set; }

        public string erres_message_2 { get; set; }

        public string erres_message_3 { get; set; }

        public string erres_message_4 { get; set; }

        public string erres_message_5 { get; set; }

        public int? erres_cre_date { get; set; }

        public int? erres_cre_time { get; set; }

        public string erres_cre_user { get; set; }

        public int? erres_mod_date { get; set; }

        public int? erres_mod_time { get; set; }

        public string erres_mod_user { get; set; }

        public string erres_mod_status { get; set; }

        public string erres_filler { get; set; }

    }
}

