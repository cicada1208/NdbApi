using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ch_chgbed")]
    public class Ch_chgbed
    {
        /// <summary>
        /// 來源病歷號
        /// </summary>
        [Key]
        public int? chgbed_pat_no { get; set; }

        [Key]
        public string chgbed_ipd_no { get; set; }

        [Key]
        public int? chgbed_exe_date { get; set; }

        [Key]
        public string chgbed_ward { get; set; }

        public decimal? chgbed_seq_num { get; set; }

        public string chgbed_user { get; set; }

        public int? chgbed_exe_time { get; set; }

        public string chgbed_exe_status { get; set; }

        public string chgbed_original_bed { get; set; }

        public string chgbed_grd_data_1 { get; set; }

        public string chgbed_grd_data_2 { get; set; }

        public string chgbed_grd_data_3 { get; set; }

        /// <summary>
        /// 預約床位(前四碼)
        /// </summary>
        public string chgbed_bed_room { get; set; }

        /// <summary>
        /// 預約床位(後兩碼)
        /// </summary>
        public string chgbed_bed_no { get; set; }

        public string chgbed_mod_user { get; set; }

        public int? chgbed_mod_date { get; set; }

        public int? chgbed_mod_time { get; set; }

        public string chgbed_mod_status { get; set; }

        public string chgbed_message_1 { get; set; }

        public string chgbed_message_2 { get; set; }

        public string chgbed_message_3 { get; set; }

        public string chgbed_message_4 { get; set; }

        public string chgbed_message_5 { get; set; }

        public string chgbed_message_6 { get; set; }

        public string chgbed_message_7 { get; set; }

        public string chgbed_message_8 { get; set; }

        public string chgbed_filler { get; set; }

        /// <summary>
        /// 需求床位等級
        /// </summary>
        [NotMapped]
        public string chgbed_bed_cls { get; set; }

    }
}

