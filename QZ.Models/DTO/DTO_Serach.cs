using MongoDB.Bson.Serialization.Attributes;
using QZ.Models.DB;
using QZ.MongoDriver;
using System.Collections.Generic;

namespace QZ.Models.DTO
{
    /// <summary>
    /// 搜索结果表
    /// </summary>
    [BsonIgnoreExtraElements]
    [DbTable("weikes")]
    public class DTO_Serach
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string ID { get; set; }

        [BsonElement("channel_id")]
        public int ChannelId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [BsonElement("name")]
        public string Title { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [BsonElement("cover_url")]
        public string CoverUrl { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        [BsonElement("money")]
        public int OriginalPrice { get; set; }

        /// <summary>
        /// 是否付费
        /// </summary>
        [BsonElement("need_money")]
        public bool NeedMoney { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        [BsonElement("stats_info")]
        public StatsInfoDB StatsInfo { get; set; }

        /// <summary>
        /// 章节列表
        /// </summary>
        [BsonElement("lectures")]
        public List<LecturesDB> Lectures { get; set; }
    }
}
