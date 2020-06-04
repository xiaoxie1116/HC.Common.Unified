using HC.Common.Tools;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace HY.Common.Base.Unit
{
    /// <summary>
    /// 数据库配置节点
    /// </summary>
    public class DataBaseConfig
    {
        private static string _connectionWriteStr;
        private static string[] _connectionReadStr;
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
                    _connectionReadStr = connStr.Split('|');
                }
                else
                    _connectionReadStr[0] = connStr;
            }

            if (string.IsNullOrEmpty(_connectionWriteStr))
                throw new ArgumentNullException("数据库连接字符串为空");

            List<SlaveConnectionConfig> connectionConfigs = null;
            if (_connectionReadStr.Length > 0)
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


        /// <summary>
        /// 加密随机数生成器 生成随机种子
        /// 该算法解决当计算机运算速度足够快的时候，系统来不及计算下一个随机数，最终可能产生一长串相同的数值问题
        /// </summary>
        /// <returns></returns>
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider r = new System.Security.Cryptography.RNGCryptoServiceProvider();
            r.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);

        }
    }
}
