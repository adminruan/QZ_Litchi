using System;

namespace QZ.MongoDriver
{
    public class DbFunction
    {
        public static string GetDbTable(Type type)
        {
            DbTableAttribute attributes = (DbTableAttribute)type.GetCustomAttributes(typeof(DbTableAttribute), false)[0];

            return attributes != null ? attributes.Name : type.Name;
        }
    }
}
