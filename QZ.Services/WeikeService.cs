using MongoDB.Driver;
using MongoDB.Driver.Linq;
using QZ.IServices;
using QZ.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QZ.Models.DB;
using QZ.Common.Helper;

namespace QZ.Services
{
    public class WeikeService : BaseService, IWeikeService
    {
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc">是否降序</param>
        /// <returns></returns>
        public List<DTO_Course> GetCourseList(int pageIndex, int pageSize, bool isDesc = true)
        {
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return null;
            }
            if (isDesc)
            {
                //降序分页
                return base.GetMongoDb<DTO_Course>().Find(x => true).SortByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Limit(pageIndex * pageSize).ToList();
            }
            else
            {
                //升序分页
                return base.GetMongoDb<DTO_Course>().Find(x => true).SortBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Limit(pageIndex * pageSize).ToList();
            }
        }

        /// <summary>
        /// 根据ID获取课程-章节列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTO_ChapterDetail GetCourseDetailByID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            Expression<Func<DTO_ChapterDetail, bool>> expression = x => x.ID.Equals(id);
            return base.Find(expression);
        }

        /// <summary>
        /// 搜索（标题）
        /// </summary>
        /// <param name="context">搜索内容</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<DTO_Serach> SerachCourseInfo(string context)
        {
            List<DTO_Serach> dataList = new List<DTO_Serach>();
            if (string.IsNullOrWhiteSpace(context))
            {
                return dataList;
            }

            //标题
            var data = base.GetMongoDb<DTO_Serach>().Find(p => p.Title.Contains(context)).ToListAsync();
            dataList = data.Result;

            return dataList;
        }

        /// <summary>
        /// 搜索（课程）
        /// </summary>
        /// <param name="context">搜索内容</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public List<LecturesDB> SearchChapterInfo(string context)
        {
            List<LecturesDB> dataList = new List<LecturesDB>();

            try
            {
                //可以用的
                //匹配包含搜索关键字的数据
                var data = base.GetMongoDb<DTO_Serach>().AsQueryable()
                       .SelectMany(p => p.Lectures, (p, s) =>
                       new LecturesDB
                       {
                           ParentId = p.ID,
                           Audition = s.Audition,
                           ChapterCoverUrl = s.ChapterCoverUrl,
                           LectureMode = s.LectureMode,
                           Price = s.Price,
                           Popular = s.Popular,
                           SubTitle = s.SubTitle,
                           Title = s.Title
                       }).Where(p => p.Title.Contains(context)).ToListAsync();

                dataList = data.Result;
            }
            catch (Exception ex)
            {
            }

            return dataList;
        }
    }
}
