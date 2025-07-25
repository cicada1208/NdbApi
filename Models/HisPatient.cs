using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ni_HisPatient")]
    public class HisPatient
    {
        [Key]
        public string PatNo { get; set; }

        public string PatName { get; set; }

        public string PatBirth { get; set; }

        public string PatGender { get; set; }

        public string PatIdNo { get; set; }

        public string PatAdd { get; set; }

        public string PatTel1 { get; set; }

        public string PatTel2 { get; set; }

        public string PatTel3 { get; set; }

        public string PatContactName { get; set; }

        public string PatContactRs { get; set; }

        public string PatContactTel { get; set; }

        public string PatMergeIntoPatNo { get; set; }

        public string PatBloodType { get; set; }

        public string PatBloodRh { get; set; }

        public string PatSuicide { get; set; }

        public string UUserNO { get; set; }

        public string UDateTime { get; set; }

    }
}

