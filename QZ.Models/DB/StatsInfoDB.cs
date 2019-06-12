using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QZ.Models.DB
{
    /// <summary>
    /// stats_info
    /// </summary>
    [BsonIgnoreExtraElements]
    public class StatsInfoDB
    {
        /// <summary>
        /// 人气
        /// </summary>
        [BsonElement("popular")]
        public int Popularity { get; set; }

        /// <summary>
        /// 讲座期数（课时）
        /// </summary>
        [BsonElement("lecture_count")]
        public int LectureCount { get; set; }
    }
}
