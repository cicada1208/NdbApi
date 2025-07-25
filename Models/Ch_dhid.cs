using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Lib.Attributes.Table("ch_dhid")]
    public class Ch_dhid
    {
        [Key]
        public string dhid_schcode { get; set; }

        public string dhid_schname { get; set; }

        /// <summary>
        /// 線別型態(D:科別 S:護理站 F:科別+護理站)
        /// </summary>
        public string dhid_type { get; set; }

        /// <summary>
        /// 醫師身份
        /// 總值:01
        /// 主治醫師:02
        /// 住院醫師:03
        /// 專科護理師:04
        /// </summary>
        public string dhid_dr_type { get; set; }

        public string dhid_unit { get; set; }

        public string dhid_fill { get; set; }

    }
}

