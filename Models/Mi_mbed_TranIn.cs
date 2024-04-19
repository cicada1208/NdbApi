﻿using Params;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mbed")]
    public class Mi_mbed_TranIn : Mi_mbed_Ext
    {
        /// <summary>
        /// 轉入註記：病人姓名
        /// </summary>
        [NotMapped]
        public string bed_i_pat_name { get; set; }

        /// <summary>
        /// 轉入註記：病人性別
        /// </summary>
        [NotMapped]
        public string bed_i_pat_sex { get; set; }

        /// <summary>
        /// 轉入註記：病人來源
        /// </summary>
        [NotMapped]
        public string bed_i_pat_from { get; set; }

        /// <summary>
        /// 轉入註記：病人需求床位等級
        /// </summary>
        [NotMapped]
        public string bed_i_need_bed_cls { get; set; }
    }
}
