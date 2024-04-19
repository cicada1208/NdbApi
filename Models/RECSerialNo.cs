using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_RECSerialNo")]
    public class RECSerialNo 
    {
        [Key]
        public string SYSID { get; set; }

        [Key]
        public string SDATE { get; set; }

        public string NUM { get; set; }

    }
}
