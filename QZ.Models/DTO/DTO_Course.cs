using MongoDB.Bson.Serialization.Attributes;
using QZ.Models.DB;
using QZ.MongoDriver;
using System;
using System.Collections.Generic;
using System.Text;

namespace QZ.Models.DTO
{
    /// <summary>
    /// 课程表
    /// </summary>
    [DbTable("weikes")]
    [BsonIgnoreExtraElements]   //忽略其它的字段
    public class DTO_Course
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 是否可以用
        /// </summary>
        [BsonElement("can_public_relay")]
        public bool IsRead { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [BsonElement("cover_url")]
        public string CoverUrl { get; set; }

        /// <summary>
        /// 课程名
        /// </summary>
        [BsonElement("name")]
        public string Title { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        [BsonElement("money")]
        public int OriginalPrice { get; set; }

        /// <summary>
        /// 是否需要付费
        /// </summary>
        [BsonElement("need_money")]
        public bool Needmoney { get; set; }

        /// <summary>
        /// 活动相关
        /// </summary>
        [BsonElement("promoprice")]
        public PromopriceDB Promoprice { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        [BsonElement("stats_info")]
        public StatsInfoDB StatsInfo { get; set; }
    }
}
