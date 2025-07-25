using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_CSAssignDetail")]
    public class CSAssignDetail
    {
        [Key]
        public string csassignDetailId { get; set; }

        public string csassignMainId { get; set; }

        public string assignType { get; set; }

        public string typeId { get; set; }

        public DateTime? validDate { get; set; }

        public DateTime? delDate { get; set; }

        public string systemUserId { get; set; }

    }
}

