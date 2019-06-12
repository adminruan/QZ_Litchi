using System;
using System.Collections.Generic;
using System.Text;

namespace QZ.MongoDriver
{
    /// <summary>
    /// 数据库中对应的表名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DbTableAttribute : Attribute
    {
        public DbTableAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
