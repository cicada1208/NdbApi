using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_FocusItem")]
    public class FocusItem
    {
        [Key]
        public string focusItemId { get; set; }

        public string focusNo { get; set; }

        public string focusName { get; set; }

        public bool? isUnusual { get; set; }

        public string announceItem { get; set; }

        public bool? isActive { get; set; }

        public DateTime? systemDt { get; set; }

        public string systemUserId { get; set; }

    }
}

