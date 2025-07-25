using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mg_mnid")]
    public class Mg_mnid
    {
        [Key]
        public string nid_id { get; set; }

        [Key]
        public string nid_code { get; set; }

        public string nid_trn { get; set; }

        public string nid_name { get; set; }

        public string nid_rec { get; set; }

        [NotMapped]
        public string UserId { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string Dimission { get; set; }

    }
}
