﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HY.Common.Base.DTO
{
    public class PageModel<T> where T : class
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount { get; set; } = 20;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int dataCount { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> data { get; set; }

    }
}
