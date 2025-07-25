using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mh_mpat")]
    public class Mh_mpat
    {
        [Key]
        public int? pat_no { get; set; }

        public int? pat_srt { get; set; }

        public string pat_orig { get; set; }

        public string pat_idno { get; set; }

        public string pat_name { get; set; }

        public int? pat_birth_dt { get; set; }

        public string pat_ano_yn { get; set; }

        public string pat_data_1 { get; set; }

        public string pat_data_2 { get; set; }

        public string pat_data_3 { get; set; }

        public string pat_data_4 { get; set; }

        [NotMapped]
        public string pat_sex { get; set; }

        [NotMapped]
        public string pat_age { get; set; }

        [NotMapped]
        public string pat_no_m { get; set; }

        [NotMapped]
        public string pat_test_yn { get; set; }

    }
}

