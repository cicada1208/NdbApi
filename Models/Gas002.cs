using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("gas002")]
    public class Gas002
    {
        [Key]
        public string gas002_00 { get; set; }

        [Key]
        public string gas002_01 { get; set; }

        public string gas002_02 { get; set; }

        public string gas002_11 { get; set; }

        public string gas002_12 { get; set; }

        public string gas002_13 { get; set; }

        public string gas002_14 { get; set; }

        public string gas002_94 { get; set; }

        public DateTime? gas002_95 { get; set; }

        public string gas002_98 { get; set; }

        public DateTime? gas002_99 { get; set; }

    }
}

