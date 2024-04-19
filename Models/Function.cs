using Params;
using System.Collections.Generic;

namespace Models
{
    public class Function 
    {
        /// <summary>
        /// 唯一識別名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 功能類型
        /// </summary>
        public FunctionParam.FuncType Type { get; set; }

        /// <summary>
        /// 內容位址
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 內容實例
        /// </summary>
        public object ContentInstance { get; set; }

        /// <summary>
        /// 群組子功能
        /// </summary>
        public List<Function> Functions { get; set; }
    }
}
