using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_ClinicalUnit")]
    public class ClinicalUnit 
    {
        [Key]
        public string clinicalUnitId { get; set; }

        public string label { get; set; }

        public string longLabel { get; set; }

        public string clinicalHisId { get; set; }

        public string cuTypeId { get; set; }

        public bool? isActive { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

    }
}

