using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_Bed")]
    public class Bed
    {
        [Key]
        public string bedId { get; set; }

        public string label { get; set; }

        public string bedHisId { get; set; }

        public string clinicalUnitId { get; set; }

        public string clinicalHisId { get; set; }

        public bool? isAdd { get; set; }

        public bool? isActive { get; set; }

        public string systemUserId { get; set; }

        public DateTime? systemDt { get; set; }

        [NotMapped]
        public string ptEncounterId { get; set; }

    }
}

