using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_icumemo")]
    public class Mi_icumemo
    {
        [Key]
        public int? icumemo_pat_no { get; set; }

        [Key]
        public int? icumemo_date { get; set; }

        [Key]
        public int? icumemo_time { get; set; }

        public string icumemo_ipd_no { get; set; }

        public int? icumemo_date_v { get; set; }

        public int? icumemo_time_v { get; set; }

        public string icumemo_bed { get; set; }

        public string icumemo_bdl_ipd { get; set; }

        public int? icumemo_bdl_date { get; set; }

        public int? icumemo_bdl_time { get; set; }

        public string icumemo_out_time { get; set; }

        public string icumemo_memo { get; set; }

        public string icumemo_cre_user { get; set; }

        public string icumemo_cre_line { get; set; }

        public string icumemo_filler { get; set; }

    }
}

