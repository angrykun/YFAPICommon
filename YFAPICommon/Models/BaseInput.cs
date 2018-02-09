using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YFAPICommon.Models
{
    public class BaseInput
    {
        public BaseInput()
        {
            pageMaxCount = 10;
            startIndex = 0;
        }
        /// <summary>
        /// 开始下标
        /// </summary>
        public int startIndex { get; set; }
        /// <summary>
        /// 单页最大条数 
        /// </summary>
        public int pageMaxCount { get; set; }
    }
}