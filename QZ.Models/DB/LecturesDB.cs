using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QZ.Models.DB
{
    /// <summary>
    /// 章节表 (lectures)
    /// </summary>     
    [BsonIgnoreExtraElements]
    public class LecturesDB
    {
        public string ParentId { get; set; }

        /// <summary>
        /// 价格（活动）
        /// </summary>
        [BsonElement("money")]
        public int Price { get; set; }

        /// <summary>
        /// 章节标题
        /// </summary>
        [BsonElement("name")]
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [BsonElement("subtitle")]
        public string SubTitle { get; set; }  

        /// <summary>
        /// 章节封面
        /// </summary>
        [BsonElement("cover_url")]
        public string ChapterCoverUrl { get; set; }

        /// <summary>
        /// 是否免费试听（pay_channel、open_lecture）
        /// </summary>
        [BsonElement("lecture_type")]
        public string Audition { get; set; }

        /// <summary>
        /// 上课方式
        /// </summary>
        [BsonElement("lecture_mode")]
        public string LectureMode { get; set; }

        /// <summary>
        /// 人气
        /// </summary>
        [BsonElement("popular")]
        public int Popular { get; set; }
    }
}
