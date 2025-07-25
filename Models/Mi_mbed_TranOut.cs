using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mi_mbed")]
    public class Mi_mbed_TranOut : Mi_mbed_Ext
    {
        private string _bed_pat_no;
        /// <summary>
        /// 該床病歷號
        /// </summary>
        [NotMapped]
        public override string bed_pat_no
        {
            get
            {
                if (_bed_pat_no == null)
                    _bed_pat_no = base.bed_pat_no;
                return _bed_pat_no;
            }
            set => _bed_pat_no = value;
        }

        /// <summary>
        /// 該床病人姓名
        /// </summary>
        [NotMapped]
        public string bed_pat_name { get; set; }

        /// <summary>
        /// 該床病人性別
        /// </summary>
        [NotMapped]
        public string bed_pat_sex { get; set; }

        /// <summary>
        /// 預出院日期
        /// </summary>
        [NotMapped]
        public string ipd_preout_dt { get; set; }

        /// <summary>
        /// 出準註記
        /// A:表出院準備連續性照護 B:實際預定出院 空白:未預定出院
        /// </summary>
        [NotMapped]
        public string ipd2_out_hos_knd { get; set; }

        /// <summary>
        /// 是否結帳
        /// </summary>
        [NotMapped]
        public string ipd_oc_end_yn { get; set; }

        /// <summary>
        /// 預出/出院/轉出狀態
        /// </summary>
        [NotMapped]
        public string bed_tranout_st { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [NotMapped]
        public string bed_note { get; set; }
    }
}
