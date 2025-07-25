using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mi_clrbed")]
    public class Mi_clrbed
    {
        [Key]
        public int? clrbed_cre_dt { get; set; }

        [Key]
        public int? clrbed_cre_time { get; set; }

        [Key]
        public string clrbed_bed { get; set; }

        public string clrbed_unit { get; set; }

        public int? clrbed_pat_no { get; set; }

        public string clrbed_ipd_no { get; set; }

        public short? clrbed_sort_odr { get; set; }

        public string clrbed_memo { get; set; }

        public string clrbed_cre_user { get; set; }

        public int? clrbed_clr_dt { get; set; }

        public int? clrbed_clr_time { get; set; }

        public string clrbed_clr_from { get; set; }

        public string clrbed_status { get; set; }

        public string clrbed_filler { get; set; }

    }
}

