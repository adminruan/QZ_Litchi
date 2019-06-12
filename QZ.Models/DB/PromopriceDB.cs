using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace QZ.Models.DB
{
    /// <summary>
    /// 活动相关信息 (promoprice)
    /// </summary>
    [BsonIgnoreExtraElements]    
    public class PromopriceDB
    {
        /// <summary>
        /// 活动截止日期
        /// </summary>
        [BsonElement("deadline")]
        public string Deadline { get; set; }

        /// <summary>
        /// 现价
        /// </summary>
        [BsonElement("money")]
        public int Money { get; set; }
    }
}
