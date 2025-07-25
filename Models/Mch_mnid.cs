using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mch_mnid")]
    public class Mch_mnid
    {
        [Key]
        public string chnid_id { get; set; }

        [Key]
        public string chnid_code { get; set; }

        public string chnid_trn { get; set; }

        public string chnid_name { get; set; }

        public string chnid_rec { get; set; }

    }
}

