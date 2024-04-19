using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB2, "ch_dhid")]
    public class Ch_dhid
    {
        [Key]
        public string dhid_schcode { get; set; }

        public string dhid_schname { get; set; }

        /// <summary>
        /// �u�O���A(D:��O S:�@�z�� F:��O+�@�z��)
        /// </summary>
        public string dhid_type { get; set; }

        /// <summary>
        /// ��v����
        /// �`��:01
        /// �D�v��v:02
        /// ���|��v:03
        /// �M���@�z�v:04
        /// </summary>
        public string dhid_dr_type { get; set; }

        public string dhid_unit { get; set; }

        public string dhid_fill { get; set; }

    }
}
