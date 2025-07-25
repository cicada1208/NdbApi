using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_Allergy")]
    public class Allergy
    {
        [Key]
        public string ptEncounterID { get; set; }

        public string PT_NO { get; set; }

        [Key]
        public string AllergyType { get; set; }

        [Key]
        public string PRSNO { get; set; }

        public string PRSNAME { get; set; }

        public string MD_MAN { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        [NotMapped]
        public string AllergyTypeName { get; set; }

    }
}

