using Lib;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table("mi_mbed")]
    public class Mi_mbed_Ext : Mi_mbed
    {
        /// <summary>
        /// 該床病歷號
        /// </summary>
        [NotMapped]
        public virtual string bed_pat_no
        {
            get => bed_group1.SybSubStr(111, 8).ToInt() == 0 ? string.Empty : bed_group1.SybSubStr(111, 8).TrimEnd();
            set { }
        }

        /// <summary>
        /// 轉入註記：床號
        /// </summary>
        [NotMapped]
        public string bed_i_bed =>
            bed_group1.SybSubStr(61, 6).TrimEnd();

        /// <summary>
        /// 轉入註記：住院序號
        /// </summary>
        [NotMapped]
        public string bed_i_ipd_no =>
            bed_group1.SybSubStr(75, 11).TrimEnd();

        /// <summary>
        /// 轉入註記：病歷號
        /// </summary>
        [NotMapped]
        public string bed_i_pat_no =>
            bed_group1.SybSubStr(67, 8).ToInt() == 0 ? string.Empty : bed_group1.SybSubStr(67, 8).TrimEnd();

        /// <summary>
        /// 轉出註記：床號
        /// </summary>
        [NotMapped]
        public string bed_o_bed =>
            bed_group1.SybSubStr(86, 6).TrimEnd();

        /// <summary>
        /// 轉出註記：病歷號
        /// </summary>
        [NotMapped]
        public string bed_o_pat_no =>
            bed_group1.SybSubStr(92, 8).ToInt() == 0 ? string.Empty : bed_group1.SybSubStr(92, 8).TrimEnd();

        /// <summary>
        /// 不排床註記理由：代碼
        /// </summary>
        /// <remarks>
        /// 1.隔離
        /// 2.設備因素
        /// 3.己被預約
        /// 4.包床
        /// 5.其他
        /// 6.申報
        /// </remarks>
        [NotMapped]
        public string bed_reason =>
            bed_group1.SybSubStr(60, 1).TrimEnd();

        /// <summary>
        /// 不排床註記理由：名稱
        /// </summary>
        [NotMapped]
        public string bed_reason_name =>
            bed_reason switch
            {
                "1" => "隔離",
                "2" => "設備因素",
                "3" => "己被預約",
                "4" => "包床",
                "5" => "其他",
                "6" => "申報",
                _ => bed_reason
            };

        /// <summary>
        /// 不排床註記理由：其它說明
        /// </summary>
        [NotMapped]
        public string bed_reason_doc =>
            bed_group1.SybSubStr(155, 14).TrimEnd();
    }
}
