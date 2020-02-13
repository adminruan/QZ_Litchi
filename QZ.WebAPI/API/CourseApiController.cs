using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using QZ.Common.Helper;
using QZ.IServices;
using QZ.Models.DB;
using QZ.Models.DTO;

namespace QZ.WebAPI.API
{
    [Route("api/[controller]/[action]")]
    public class CourseApiController : BaseController
    {
        private readonly IWeikeService _iWeikeService;

        public CourseApiController(IWeikeService weikeService)
        {
            this._iWeikeService = weikeService;
        }

        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        [HttpGet]
        public void CourseList(int pageIndex, int pageSize, bool? lowPrice, bool? isDesc)
        {
            if (lowPrice == null)
                lowPrice = false;
            if (isDesc == null)
                isDesc = false;
            CurrentPage = pageIndex < 1 ? 1 : pageIndex;
            PageSize = pageSize < 1 ? 20 : pageSize;
            Expression<Func<DTO_Course, bool>> where = p => p.IsRead == true && p.Title != "无";
            if (lowPrice.Value)
            {
                where = p => p.IsRead == true && p.Title != "无" && p.OriginalPrice < 1000 && p.OriginalPrice >= 500;
            }
            var data = _iWeikeService.GetData(where);
            SizeCount = (int)data.CountDocuments();
            var dataList = data.Skip((CurrentPage - 1) * PageSize).Limit(PageSize).ToList();
            //List<DTO_Course> dataList = _iWeikeService.GetCourseList(CurrentPage, PageSize);
            if (dataList == null)
            {
                Write("{\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",\"S\":\"" + Base64Encode("0") + "\",\"msg\":\"" + Base64Encode("暂无数据") + "\"}");
                return;
            }
            string jsonStr = "{\"msg\":\"\",\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",\"S\":\"" + Base64Encode("1") + "\",";
            jsonStr += "\"currentPage\":\"" + Base64Encode(CurrentPage.ToString()) + "\",\"totalPage\":\"" + Base64Encode(SizeCount.GetPageCount(PageSize).ToString()) + "\",\"dataList\":[";
            foreach (DTO_Course item in dataList)
            {
                jsonStr += "{\"ID\":\"" + Base64Encode(item.Id) + "\",";
                jsonStr += "\"CoverUrl\":\"" + Base64Encode(item.CoverUrl) + "\",";
                jsonStr += "\"Title\":\"" + Base64Encode(item.Title) + "\",";
                jsonStr += "\"Periods\":\"" + Base64Encode(item.StatsInfo.LectureCount.ToString()) + "\",";//已更新多少期数
                jsonStr += "\"Needmoney\":\"" + Base64Encode(item.Needmoney.ToString()) + "\",";
                jsonStr += "\"OriginalPrice\":\"" + Base64Encode((item.OriginalPrice * 0.01).ToString("0.00")) + "\",";
                jsonStr += "\"CurrentPrice\":\"" + Base64Encode(item.Promoprice != null ? (item.Promoprice.Money * 0.01).ToString("0.00") : (item.OriginalPrice * 0.01).ToString("0.00")) + "\",";
                jsonStr += "\"PurchasingNum\":\"" + Base64Encode(item.StatsInfo.Popularity.ToString()) + "\"},";
            }
            jsonStr = jsonStr.TrimEnd(',');
            jsonStr += "]}";
            Write(jsonStr);
            return;
        }

        /// <summary>
        /// 根据编号获取课程详情
        /// </summary>
        /// <param name="id">课程ID</param>
        [HttpGet]
        public void CourseDetails(string ID)
        {
            if (string.IsNullOrWhiteSpace(ID))
            {
                Write("{\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",\"S\":\"" + Base64Encode("0") + "\",\"msg\":\"" + Base64Encode("参数不能为空") + "\"}");
                return;
            }
            DTO_ChapterDetail data = _iWeikeService.GetCourseDetailByID(ID);
            if (data == null)
            {
                Write("{\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",\"S\":\"" + Base64Encode("0") + "\",\"msg\":\"" + Base64Encode("未找到数据") + "\"}");
                return;
            }
            string jsonStr = "{\"msg\":\"\",\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",\"S\":\"" + Base64Encode("1") + "\",";
            jsonStr += "\"ID\":\"" + Base64Encode(data.ID) + "\",";
            jsonStr += "\"Title\":\"" + Base64Encode(data.Title) + "\",";
            jsonStr += "\"NeedMoney\":\"" + Base64Encode(data.NeedMoney.ToString()) + "\",";
            jsonStr += "\"CoverUrl\":\"" + Base64Encode(data.CoverUrl) + "\",";
            jsonStr += "\"Description\":\"" + Base64Encode(data.Description) + "\",";
            jsonStr += "\"OriginalPrice\":\"" + Base64Encode((data.OriginalPrice * 0.01).ToString("0.00")) + "\",";
            jsonStr += "\"CurrentPrice\":\"" + Base64Encode(data.Promoprice != null ? (data.Promoprice.Money * 0.01).ToString("0.00") : "") + "\",";
            jsonStr += "\"Deadline\":\"" + Base64Encode(data.Promoprice != null ? data.Promoprice.Deadline : "") + "\",";
            jsonStr += "\"ChapterList\":[";
            if (data.Lectures != null)
            {
                foreach (LecturesDB item in data.Lectures)
                {
                    jsonStr += "{\"Title\":\"" + Base64Encode(item.Title) + "\",";
                    jsonStr += "\"ChapterCoverUrl\":\"" + Base64Encode(item.ChapterCoverUrl) + "\",";
                    jsonStr += "\"LectureMode\":\"" + Base64Encode(item.LectureMode) + "\",";
                    jsonStr += "\"IsAudition\":\"" + Base64Encode(item.Audition == "open_lecture" ? "true" : "false") + "\"";
                    jsonStr += "},";
                }
                jsonStr = jsonStr.TrimEnd(',');
            }
            jsonStr += "]}";
            Write(jsonStr);
            return;
        }

        /// <summary>
        /// 搜索课程
        /// </summary>
        /// <param name="context">搜索内容</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示行数</param>
        [HttpGet]
        public void SerachCourse(string context, int pageIndex, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                Write("{\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",\"S\":\"" + Base64Encode("0") + "\",\"msg\":\"" + Base64Encode("参数不能为空") + "\"}");
                return;
            }

            CurrentPage = pageIndex <= 0 ? 1 : pageIndex;
            PageSize = pageSize <= 0 ? 15 : pageSize;

            List<DTO_Serach> courseList = _iWeikeService.SerachCourseInfo(context/*, CurrentPage, PageSize, ref PageCount, ref SizeCount*/);//搜索课程


            string jsonStr = "{\"msg\":\"\",\"SIP\":\"" + Base64Encode(Helper_IP.GetServiceIP()) + "\",\"Date\":\"" + Base64Encode(DateTime.Now.ToString()) + "\",";
            jsonStr += "\"S\":\"" + Base64Encode("1") + "\",";
            if (courseList != null && courseList.Count > 0)//课程
            {
                foreach (var item in courseList)
                {
                    jsonStr += "\"CourseList\":[{";
                    foreach (DTO_Serach serach in courseList)
                    {
                        jsonStr += "\"ID\":\"" + base.Base64Encode(serach.ID) + "\",";
                        jsonStr += "\"Title\":\"" + base.Base64Encode(serach.Title) + "\",";
                        jsonStr += "\"CoverUrl\":\"" + base.Base64Encode(serach.CoverUrl) + "\",";
                        jsonStr += "\"LectureCount\":\"" + base.Base64Encode(serach.StatsInfo.LectureCount.ToString()) + "\",";
                        jsonStr += "\"Popularity\":\"" + base.Base64Encode(serach.StatsInfo.Popularity.ToString()) + "\",";
                        if (serach.NeedMoney)
                        {
                            jsonStr += "\"OriginalPrice\":\"" + base.Base64Encode(serach.OriginalPrice.ToString("0.00")) + "\",";
                            jsonStr += "\"CurrentPrice\":\"" + base.Base64Encode(serach.Lectures != null ? serach.Lectures.Last().Price.ToString("0.00") : "0.00") + "\",";
                        }
                        else
                        {
                            jsonStr += "\"OriginalPrice\":\"" + base.Base64Encode("0.00") + "\",";
                            jsonStr += "\"CurrentPrice\":\"" + base.Base64Encode("0.00") + "\",";
                        }
                    }
                    jsonStr = jsonStr.TrimEnd(',') + "}],";
                }
            }
            else
            {
                jsonStr += "\"CourseList\":[],";
            }

            List<LecturesDB> chapterList = _iWeikeService.SearchChapterInfo(context/*, CurrentPage, PageSize, ref PageCount, ref SizeCount*/);//搜索章节
            //jsonStr += "\"LPageCount\":\"" + Base64Encode(PageCount.ToString()) + "\",\"LSizeCount\":\"" + Base64Encode(SizeCount.ToString()) + "\",";
            if (chapterList != null && chapterList.Count > 0)//章节
            {
                jsonStr += "\"ChapterList\":[{";
                foreach (LecturesDB item in chapterList)
                {
                    jsonStr += "\"ID\":\"" + base.Base64Encode(item.ParentId) + "\",";
                    jsonStr += "\"ChapterName\":\"" + base.Base64Encode(item.Title) + "\",";
                    jsonStr += "\"LectureMode\":\"" + base.Base64Encode(item.LectureMode) + "\",";
                    jsonStr += "\"CoverUrl\":\"" + base.Base64Encode(item.ChapterCoverUrl) + "\",";
                    jsonStr += "\"Popular\":\"" + base.Base64Encode(item.Popular.ToString()) + "\",";
                }
                jsonStr = jsonStr.TrimEnd(',') + "}]";
            }
            else
            {
                jsonStr += "\"ChapterList\":[]";
            }
            Write(jsonStr + "}");
            return;
        }

    }
}
