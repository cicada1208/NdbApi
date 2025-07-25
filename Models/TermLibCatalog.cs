using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_TermLibCatalog")]
    public class TermLibCatalog
    {
        [Key]
        public string termLibCatalogId { get; set; }

        public string label { get; set; }

        public bool? isLimit { get; set; }

        public bool? isActive { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

    }
}

