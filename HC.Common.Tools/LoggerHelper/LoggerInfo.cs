using System;
using System.Collections.Generic;
using System.Text;

namespace HC.Common.Tools
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LoggerInfo
    {
        public DateTime Datetime { get; set; }
        public string Content { get; set; }
        public string IP { get; set; }
        public string LogColor { get; set; }
        public int Import { get; set; } = 0;
    }  
}
