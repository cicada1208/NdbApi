using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_TermLib")]
    public class TermLib 
    {
        [Key]
        public string termLibId { get; set; }

        public string termLibCatalogId { get; set; }

        public string label { get; set; }

        public string longLabel { get; set; }

        public bool? isActive { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

        public string value { get; set; }

    }
}

