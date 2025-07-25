using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_tab")]
    public class Ch_tab
    {
        [Key]
        public string tab_ym { get; set; }

        [Key]
        public string tab_schcode { get; set; }

        [Key]
        public string tab_date { get; set; }

        /// <summary>
        /// �|�E����  [1]��|�E [2]�@��|�E [3] �@��|�ݫ�|  [4]�|���ȯZ  [5]ON  Call    [6]��|�ȯZ
        /// </summary>
        [Key]
        public string tab_id { get; set; }

        public string tab_dpt { get; set; }

        /// <summary>
        /// �|�E���A [1]�̤���覡 [2]���@�z���覡
        /// </summary>
        public string tab_type { get; set; }

        public string tab_unit { get; set; }

        public string tab_dr_no { get; set; }

        public string tab_dr_name { get; set; }

        public int? tab_time_beg { get; set; }

        public int? tab_time_end { get; set; }

        public string tab_cre_user { get; set; }

        public string tab_cre_date { get; set; }

        public string tab_upd_user { get; set; }

        public string tab_upd_date { get; set; }

        public string tab_memo { get; set; }

        public string tab_schname { get; set; }

        public string tab_ipdmark { get; set; }

        public string tab_updmark { get; set; }

        public string tab_filler { get; set; }

        public decimal? A4GLIdentity { get; set; }

    }
}

