using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_PtStay")]
    public class PtStay
    {
        [Key]
        public string ptStayId { get; set; }

        public string ptEncounterId { get; set; }

        public string clinicalUnitId { get; set; }

        public string bedId { get; set; }

        public DateTime? beginDt { get; set; }

        public DateTime? endDt { get; set; }

        public DateTime? systemDt { get; set; }

        public string systemUserId { get; set; }

    }
}

