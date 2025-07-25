using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_prs")]
    public class Ch_prs
    {
        [Key]
        public string chprs_mst_id { get; set; }

        public string chprs_ins_id { get; set; }

        public string chprs_stk_cnt { get; set; }

        public string chprs_id_name { get; set; }

        public string chprs_id_name2 { get; set; }

        public string chprs_id_name3 { get; set; }

        public string chprs_brf_id { get; set; }

        public string chprs_fee_knd { get; set; }

        public string chprs_way_id { get; set; }

        public string chprs_typ_id { get; set; }

        public string chprs_alt2_1 { get; set; }

        public string chprs_data1 { get; set; }

        public string chprs_data2 { get; set; }

        public string chprs_data3 { get; set; }

        public string chprs_data4 { get; set; }

        public string chprs_data5 { get; set; }

        public string chprs_data6 { get; set; }

        public string chprs_data7 { get; set; }

        public string chprs_data8 { get; set; }

        public string chprs_data9 { get; set; }

        public string chprs_data10 { get; set; }

        public string chprs_data11 { get; set; }

    }
}

