using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mch_mgop")]
    public class Mch_mgop
    {
        [Key]
        public int? mgop_ym { get; set; }

        [Key]
        public string mgop_dr_no { get; set; }

        public string mgop_gop_type_1 { get; set; }

        public string mgop_gop_type_2 { get; set; }

        public string mgop_gop_type_3 { get; set; }

        public string mgop_gop_type_4 { get; set; }

        public string mgop_gop_type_5 { get; set; }

        public string mgop_gop_type_6 { get; set; }

        public string mgop_gop_type_7 { get; set; }

        public string mgop_gop_type_8 { get; set; }

        public string mgop_gop_type_9 { get; set; }

        public string mgop_gop_type_10 { get; set; }

        public string mgop_gop_no_1 { get; set; }

        public string mgop_gop_no_2 { get; set; }

        public string mgop_gop_no_3 { get; set; }

        public string mgop_gop_no_4 { get; set; }

        public string mgop_gop_no_5 { get; set; }

        public string mgop_gop_no_6 { get; set; }

        public string mgop_gop_no_7 { get; set; }

        public string mgop_gop_no_8 { get; set; }

        public string mgop_gop_no_9 { get; set; }

        public string mgop_gop_no_10 { get; set; }

        public string mgop_mb_no_1 { get; set; }

        public string mgop_mb_no_2 { get; set; }

        public string mgop_mb_no_3 { get; set; }

        public string mgop_mb_no_4 { get; set; }

        public string mgop_mb_no_5 { get; set; }

        public string mgop_mb_no_6 { get; set; }

        public string mgop_mb_no_7 { get; set; }

        public string mgop_mb_no_8 { get; set; }

        public string mgop_mb_no_9 { get; set; }

        public string mgop_mb_no_10 { get; set; }

        public string mgop_filler { get; set; }

        public decimal? A4GLIdentity { get; set; }

    }
}

