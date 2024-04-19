using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB2, "ch_tab")]
    public class Ch_tab
    {
        [Key]
        public string tab_ym { get; set; }

        [Key]
        public string tab_schcode { get; set; }

        [Key]
        public string tab_date { get; set; }

        /// <summary>
        /// 會診種類  [1]急會診 [2]一般會診 [3] 一般會兼急會  [4]院內值班  [5]ON  Call    [6]住院值班
        /// </summary>
        [Key]
        public string tab_id { get; set; }

        public string tab_dpt { get; set; }

        /// <summary>
        /// 會診型態 [1]依日期方式 [2]依護理站方式
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

