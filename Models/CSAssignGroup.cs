using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CSAssignGroup
    {
        public string csassignMainId { get; set; }

        public string loginId { get; set; }

        public string userName { get; set; }

        public string proTitleLevelLabel { get; set; }

        /// <summary>
        /// 班別Id
        /// </summary>
        public string itemId { get; set; }

        /// <summary>
        /// 班別
        /// </summary>
        public string itemName { get; set; }

        /// <summary>
        /// 組別Id
        /// </summary>
        public string csteamId { get; set; }

        /// <summary>
        /// 組別
        /// </summary>
        public string teamName { get; set; }

        public string timeInterval { get; set; }

        /// <summary>
        /// 照護床號
        /// </summary>
        /// <remarks>逗號分隔</remarks>
        public string beds { get; set; }

        /// <summary>
        /// 編組(點班)
        /// </summary>
        /// <remarks>逗號分隔</remarks>
        public string checkItems { get; set; }

    }
}
