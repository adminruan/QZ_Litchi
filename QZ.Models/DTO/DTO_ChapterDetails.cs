using MongoDB.Bson.Serialization.Attributes;
using QZ.Models.DB;
using QZ.MongoDriver;
using System.Collections.Generic;

namespace QZ.Models.DTO
{
    /// <summary>
    /// 课程详情
    /// </summary>
    [BsonIgnoreExtraElements]
    [DbTable("weikes")]
    public class DTO_ChapterDetail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [BsonElement("cover_url")]
        public string CoverUrl { get; set; }

        /// <summary>
        /// 课程简介
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// 章节列表
        /// </summary>
        [BsonElement("lectures")]
        public List<LecturesDB> Lectures { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        [BsonElement("money")]
        public int OriginalPrice { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [BsonElement("name")]
        public string Title { get; set; }

        /// <summary>
        /// 是否付费
        /// </summary>
        [BsonElement("need_money")]
        public bool NeedMoney { get; set; }

        /// <summary>
        /// 活动相关信息
        /// </summary>
        [BsonElement("promoprice")]
        public PromopriceDB Promoprice { get; set; }
    }
}
