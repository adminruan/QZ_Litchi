using QZ.Models.DB;
using QZ.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;

namespace QZ.IServices
{
    public interface IWeikeService : IBaseService
    {
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页行数</param>
        /// /// <param name="isDesc">是否降序</param>
        List<DTO_Course> GetCourseList(int pageIndex, int pageSize, bool isDesc = true);

        /// <summary>
        /// 获取课程列表-使用时查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        /*IOrderedMongoQueryable<DTO_Course>*/
        IOrderedFindFluent<DTO_Course, DTO_Course> GetData(Expression<Func<DTO_Course, bool>> where, bool isDesc = true);

        /// <summary>
        /// 根据编号获取课程详情
        /// </summary>
        /// <param name="id">需要查询的ID</param>
        DTO_ChapterDetail GetCourseDetailByID(string id);

        /// <summary>
        /// 搜索（标题）
        /// </summary>
        /// <param name="context">搜索的内容</param>
        /// <param name="index">当前页</param>
        /// <param name="size">显示行数</param>
        /// <returns></returns>
        List<DTO_Serach> SerachCourseInfo(string context);

        /// <summary>
        /// 搜索（课程）
        /// </summary>
        /// <param name="context">搜索内容</param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<LecturesDB> SearchChapterInfo(string context);
    }
}
