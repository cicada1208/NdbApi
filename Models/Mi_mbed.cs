using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mbed")]
    public class Mi_mbed 
    {
        [Key]
        public string bed_bed { get; set; }

        public string bed_grd { get; set; }

        public string bed_dpt { get; set; }

        public string bed_unit { get; set; }

        public string bed_status { get; set; }

        /// <summary>
        /// ��J���O
        /// </summary>
        public string bed_i_mark { get; set; }

        /// <summary>
        /// ��X���O
        /// </summary>
        public string bed_o_mark { get; set; }

        public string bed_group1 { get; set; }

    }
}
