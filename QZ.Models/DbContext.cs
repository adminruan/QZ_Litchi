using MongoDB.Driver;
using QZ.Common.Enum;
using QZ.Common.Helper;
using QZ.MongoDriver;
using System;
using System.Collections.Concurrent;

namespace QZ.Models
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DbContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string ConnectionString { get; set; }

        private static ConcurrentDictionary<string, IMongoDatabase> _database = new ConcurrentDictionary<string, IMongoDatabase>();

        public DbContext() : this(Enum_Connection.ConnectionLitchi.ToString())
        {
        }

        /// <summary>
        /// 初始化连接字符串
        /// </summary>
        /// <param name="conn"></param>
        public DbContext(string conn)
        {
            if (string.IsNullOrWhiteSpace(conn))
            {
                throw new Exception("connectionString is empty");
            }
            ConnectionString = conn.Contains("mongodb://") ? conn : Helper_Config.Configuration.GetSection("MongodbConnectionString:" + conn).Value;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        public IMongoDatabase Database
        {
            get
            {
                return _database.GetOrAdd(ConnectionString, (conn) =>
                 {
                     MongoUrlBuilder url = new MongoUrlBuilder(conn)
                     {
                         ConnectTimeout = TimeSpan.FromSeconds(5),
                         SocketTimeout = TimeSpan.FromSeconds(30),
                         MaxConnectionIdleTime = TimeSpan.FromSeconds(10),    //连接的最大闲置时间
                         MaxConnectionLifeTime = TimeSpan.FromSeconds(60),    //连接的最大存在时间
                         WaitQueueTimeout = TimeSpan.FromSeconds(5),
                         WTimeout = TimeSpan.FromSeconds(5),
                         MaxConnectionPoolSize = 50,
                         WaitQueueMultiple = 5,
                         WaitQueueSize = 300
                     };
                     MongoClient client = new MongoClient(url.ToMongoUrl());

                     return client.GetDatabase(url.DatabaseName);
                 });
            }
        }

        /// <summary>
        /// 获取数据库文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            return Database.GetCollection<T>(DbFunction.GetDbTable(typeof(T)));
        }
    }
}
