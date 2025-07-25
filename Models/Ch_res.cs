using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_res")]
    public class Ch_res
    {
        /// <summary>
        /// 來源病歷號
        /// </summary>
        [Key]
        public int? chres_pat_no { get; set; }

        [Key]
        public string chres_mj_dr { get; set; }

        [Key]
        public int? chres_sign_dt { get; set; }

        public int? chres_date { get; set; }

        public int? chres_date_v { get; set; }

        public string chres_del_mark { get; set; }

        public string chres_dpt { get; set; }

        public string chres_sex { get; set; }

        public int? chres_sign_time { get; set; }

        public string chres_sign_user { get; set; }

        public short? chres_ser_lv { get; set; }

        public short? chres_priority { get; set; }

        public int? chres_op_date { get; set; }

        public string chres_idzs_1 { get; set; }

        public string chres_idzs_2 { get; set; }

        public string chres_idzs_3 { get; set; }

        public string chres_idzs_4 { get; set; }

        public string chres_idzs_5 { get; set; }

        public short? chres_bed_lv { get; set; }

        public string chres_phone_1 { get; set; }

        public string chres_phone_2 { get; set; }

        public string chres_phone_3 { get; set; }

        public string chres_phone_4 { get; set; }

        public string chres_phone_5 { get; set; }

        public string chres_relation_1 { get; set; }

        public string chres_relation_2 { get; set; }

        public string chres_relation_3 { get; set; }

        public string chres_relation_4 { get; set; }

        public string chres_relation_5 { get; set; }

        public string chres_bed_message_1 { get; set; }

        public string chres_bed_message_2 { get; set; }

        public string chres_bed_message_3 { get; set; }

        public string chres_bed_message_4 { get; set; }

        public string chres_pat_message_1 { get; set; }

        public string chres_pat_message_2 { get; set; }

        public string chres_pat_message_3 { get; set; }

        public string chres_pat_message_4 { get; set; }

        public string chres_team_message_1 { get; set; }

        public string chres_team_message_2 { get; set; }

        public string chres_team_message_3 { get; set; }

        public string chres_team_message_4 { get; set; }

        public string chres_no_mobile { get; set; }

        public string chres_bed_day { get; set; }

        public string chres_ctl_room { get; set; }

        public string chres_ctl_no { get; set; }

        public string chres_dr_room { get; set; }

        public string chres_dr_no { get; set; }

        /// <summary>
        /// 需求床位等級
        /// </summary>
        public string chres_grd_1 { get; set; }

        public string chres_grd_2 { get; set; }

        public string chres_grd_3 { get; set; }

        public int? chres_ctop_date { get; set; }

        public int? chres_ctop_time { get; set; }

        public int? chres_ptoc_date { get; set; }

        public int? chres_ptoc_time { get; set; }

        public int? chres_cton_date { get; set; }

        public int? chres_cton_time { get; set; }

        public int? chres_wait_date { get; set; }

        public int? chres_wait_time { get; set; }

        /// <summary>
        /// 預約床位
        /// </summary>
        public string chres_wait_bed { get; set; }

        public int? chres_acc_case_date { get; set; }

        public int? chres_acc_case_time { get; set; }

        public int? chres_trn_bed_date { get; set; }

        public int? chres_trn_bed_time { get; set; }

        /// <summary>
        /// 來源住院序號
        /// </summary>
        public decimal? chres_ipd_no { get; set; }

        public string chres_bed_sw { get; set; }

        public string chres_sur_yn { get; set; }

        public string chres_own_yn { get; set; }

        public string chres_own_sw { get; set; }

        public string chres_own_part { get; set; }

        public string chres_iso_sw { get; set; }

        public string chres_filler { get; set; }

    }
}

