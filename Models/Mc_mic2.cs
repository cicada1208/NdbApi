using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("mc_mic2")]
    public class Mc_mic2
    {
        [Key]
        public string hcic2_rno { get; set; }

        public int? hcic2_ptno { get; set; }

        public string hcic2_pat_idno { get; set; }

        public string hcic2_ip_id { get; set; }

        public string hcic2_ip_no { get; set; }

        public string hcic2_id2no { get; set; }

        public int? hcic2_sdate { get; set; }

        public int? hcic2_diadate { get; set; }

        public int? hcic2_repdate { get; set; }

        public string hcic2_status { get; set; }

        public string hcic2_data2 { get; set; }

        public string hcic2_data3 { get; set; }

        public string hcic2_data4 { get; set; }

        public string hcic2_data5 { get; set; }

        public string hcic2_data6 { get; set; }

        public string hcic2_data7 { get; set; }

        public string hcic2_data8 { get; set; }

    }
}

