using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_FocusRecord")]
    public class FocusRecord
    {
        [Key]
        public string focusRecordId { get; set; }

        public string nursingRecordId { get; set; }

        public string ptEncounterId { get; set; }

        public string focusItemId { get; set; }

        public string focusName { get; set; }

        public short? focusStatus { get; set; }

        public string planRecordId { get; set; }

        public bool? isActive { get; set; }

        public short? focusSeq { get; set; }

        public DateTime? recordDate { get; set; }

        public DateTime? changeDate { get; set; }

        public DateTime? systemDt { get; set; }

        public string systemUserId { get; set; }

        [NotMapped]
        public string focusNo { get; set; }

    }
}

