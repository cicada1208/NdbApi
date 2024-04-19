using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_PtEncHis")]
    public class PtEncHis
    {
        [Key]
        public string ptEncHisId { get; set; }

        public string ptEncounterId { get; set; }

        public string hisIpdNo { get; set; }

        public DateTime? ipdDt { get; set; }

        public bool? isActive { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

    }
}

