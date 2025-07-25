using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("gas102")]
    public class Gas102
    {
        [Key]
        public string gas102_01 { get; set; }

        public DateTime? gas102_02 { get; set; }

        public string gas102_03 { get; set; }

        public string gas102_04 { get; set; }

        public string gas102_05 { get; set; }

        public string gas102_06 { get; set; }

        public string gas102_08 { get; set; }

        public string gas102_09 { get; set; }

        public string gas102_10 { get; set; }

        public DateTime? gas102_11 { get; set; }

        public DateTime? gas102_12 { get; set; }

        public string gas102_13 { get; set; }

        public DateTime? gas102_14 { get; set; }

        public DateTime? gas102_15 { get; set; }

        public string gas102_21 { get; set; }

        public string gas102_22 { get; set; }

        public string gas102_23 { get; set; }

        public string gas102_24 { get; set; }

        public DateTime? gas102_25 { get; set; }

        public string gas102_31 { get; set; }

        public string gas102_98 { get; set; }

        public DateTime? gas102_99 { get; set; }

        public string PackNo { get; set; }

        public string del { get; set; }

        public string reason { get; set; }

        public string finish { get; set; }

        public string isUrgent { get; set; }

        public string isAppCase { get; set; }

        public short? transSeq { get; set; }

        public short? SeqEnd { get; set; }

        public string isReturn { get; set; }

        public string isMulti { get; set; }

        public string turnkeyNum { get; set; }

        public string gas102_dr { get; set; }

        public string gas102_drName { get; set; }

    }
}

