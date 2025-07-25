using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("gas103")]
    public class Gas103
    {
        [Key]
        public string gas103_01 { get; set; }

        [Key]
        public byte? gas103_02 { get; set; }

        public string gas103_03 { get; set; }

        public DateTime? gas103_04 { get; set; }

        public DateTime? gas103_05 { get; set; }

        public string gas103_06 { get; set; }

        public DateTime? gas103_07 { get; set; }

        public string transNo { get; set; }

    }
}

