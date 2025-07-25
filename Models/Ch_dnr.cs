using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ch_dnr")]
    public class Ch_dnr
    {
        [Key]
        public string dnr_pat_idno { get; set; }

        [Key]
        public string dnr_tbl_type { get; set; }

        public int? dnr_pat_no { get; set; }

        public string dnr_tbl { get; set; }

        public string dnr_filler { get; set; }

        public decimal? A4GLIdentity { get; set; }

        [NotMapped]
        public string no143 { get; set; }

        [NotMapped]
        public string no144 { get; set; }

        [NotMapped]
        public string no145 { get; set; }

        [NotMapped]
        public string no146 { get; set; }

        [NotMapped]
        public string no147 { get; set; }

        [NotMapped]
        public string no148 { get; set; }

        [NotMapped]
        public string no149 { get; set; }

        [NotMapped]
        public string no150 { get; set; }

        [NotMapped]
        public string dnr_type { get; set; }

        [NotMapped]
        public string dnr_info { get; set; }

        [NotMapped]
        public string dnr_sign_dt { get; set; }

        private List<string> _dnr_chk_item;
        [NotMapped]
        public List<string> dnr_chk_item
        {
            get => _dnr_chk_item ??= new List<string>();
            set => _dnr_chk_item = value;
        }

    }
}

