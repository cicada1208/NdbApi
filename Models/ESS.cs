using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("ni_ESS")]
    public class ESS
    {
        [Key]
        public string REC_NO { get; set; }

        public string ptEncounterID { get; set; }

        public string DOC_CODE { get; set; }

        public string PT_NO { get; set; }

        public string REC_DTM { get; set; }

        public string DT { get; set; }

        public string Physical { get; set; }

        public string Family { get; set; }

        public string Emotional { get; set; }

        public string Practical { get; set; }

        public string Spiritual { get; set; }

        public string REC_STATUS { get; set; }

        public string InstructorId { get; set; }

        public string Instructor_Name { get; set; }

        public string MD_MAN { get; set; }

        public string MD_NAME { get; set; }

        public string MD_PC { get; set; }

        public string MD_Version { get; set; }

        public string MD_DT { get; set; }

        public string MD_TIME { get; set; }

        /// <summary>
        /// Âà¤¶¸ê°T
        /// </summary>
        [NotMapped]
        public string Info { get; set; }

    }
}

