using HC.Common.Tools;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Common.Base.Unit
{
    /// <summary>
    /// 数据库配置节点
    /// </summary>
    public class DataBaseConfig
    {
        private static string _connectionWriteStr;
        private static List<string> _connectionReadStr = new List<string>();
        private SqlSugarClient _db;

        /// <summary>
        /// 链接数据库
        /// </summary>
        /// <param name="IsAutoCloseConnection">是否主动关闭数据库连接：设置为true无需使用using或者Close操作，默认false</param>
        /// <param name="OperationDBType">数据库的读写操作选择</param>
        private DataBaseConfig(bool IsAutoCloseConnection)
        {
            _connectionWriteStr = Appsettings.GetSettingNode(new string[] { "AppSettings", "MySql", "Write", "ConnectionString" });
            var connStr = Appsettings.GetSettingNode(new string[] { "AppSettings", "MySql", "Read", "ConnectionString" });
            if (!string.IsNullOrEmpty(connStr))
            {
                if (connStr.Contains('|'))
                {
                    _connectionReadStr.AddRange(connStr.Split('|'));
                }
                else
                    _connectionReadStr.Add(connStr);
            }

            if (string.IsNullOrEmpty(_connectionWriteStr))
                throw new ArgumentNullException("数据库连接字符串为空");

            List<SlaveConnectionConfig> connectionConfigs = null;
            if (_connectionReadStr != null && _connectionReadStr.Any())
            {
                connectionConfigs = new List<SlaveConnectionConfig>();
                foreach (var item in _connectionReadStr)
                {
                    connectionConfigs.Add(new SlaveConnectionConfig
                    {
                        ConnectionString = item,
                        HitRate = 10
                    });
                }
            }

            bool reval = false;
            var isSqlAop = Appsettings.GetSettingNode(new string[] { "AppSettings", "SqlAOP", "Enabled" });
            reval = !string.IsNullOrEmpty(isSqlAop) ? bool.Parse(isSqlAop) : false;
            //数据库读写分离（自动将写的操作连接到主库，读的操作连接到次库）
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                //主连接
                ConnectionString = _connectionWriteStr,
                DbType = DbType.MySql,
                IsAutoCloseConnection = IsAutoCloseConnection,
                IsShardSameThread = false,
                //从连接
                SlaveConnectionConfigs = connectionConfigs,
                //如果是非正常的数据类型ORM不支持可以自已添加扩展，其他功能详见官网设置
                ConfigureExternalServices = new ConfigureExternalServices() { },
                //用于一些全局设置
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = false  //为true表示可以自动删除二级缓存
                },
                AopEvents = new AopEvents
                {
                    OnLogExecuting = (sql, p) =>
                    {
                        if (reval)
                        {
                            Parallel.For(0, 1, e =>
                            {
                                //MiniProfiler.Current.CustomTiming("SQL：", GetParas(p) + "【SQL语句】：" + sql);
                                LoggerLock.OutPutLogger("SqlLog", new string[] { GetParas(p), "【SQL语句】：" + sql });
                            });
                        }
                    }
                }
            });

        }

        /// <summary>
        /// 获得一个 DataBaseConfig
        /// </summary>
        /// <param name="operationDBType">操作数据库的类型</param>
        /// <param name="blnIsAutoCloseConnection">是否自动关闭连接（如果为false，则使用接受时需要手动关闭Db）</param>
        /// <returns></returns>
        public static DataBaseConfig GetDbContext(bool blnIsAutoCloseConnection = true)
        {
            return new DataBaseConfig(blnIsAutoCloseConnection);
        }


        /// <summary>
        /// 数据连接对象       
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }

        private static string GetParas(SugarParameter[] pars)
        {
            string key = "【SQL参数】：";
            foreach (var param in pars)
            {
                key += $"{param.ParameterName}:{param.Value}\n";
            }
            return key;
        }

    }
}
