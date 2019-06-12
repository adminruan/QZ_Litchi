using MongoDB.Driver;
using QZ.Common.Enum;
using QZ.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QZ.Services
{
    public class BaseService
    {
        private readonly DbContext _dbContext = null;

        public BaseService()
        {
            this._dbContext = new DbContext();
        }

        public BaseService(string conn)
        {
            this._dbContext = new DbContext(conn);
        }

        /// <summary>
        /// 获取数据库文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IMongoCollection<T> GetMongoDb<T>() where T : class
        {
            return this._dbContext.GetCollection<T>();
        }

        /// <summary>
        /// 增添数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Create<T>(T entity) where T : class
        {
            this.GetMongoDb<T>().InsertOne(entity);
            return entity;
        }

        /// <summary>
        /// 表达式获取数据 -- 单条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T Find<T>(Expression<Func<T,bool>> expression) where T : class
        {
            return this.GetMongoDb<T>().Find(expression).FirstOrDefault();
        }

        public List<T> GetList<T>() where T : class
        {
            return this.GetMongoDb<T>().Find(x => true).ToList();
        }
    }
}
