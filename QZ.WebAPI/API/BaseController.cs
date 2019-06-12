using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QZ.WebAPI.API
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 总页数
        /// </summary>
        protected int PageCount = 0;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int CurrentPage = 1;
        /// <summary>
        /// 分页大小
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 数据总数
        /// </summary>
        protected int SizeCount = 0;

        /// <summary>
        /// 设置请求响应内容
        /// </summary>
        /// <param name="str"></param>
        protected void Write(string content)
        {
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.WriteAsync(content);
        }

        #region Base64加密、解密
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string Base64Encode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string Base64Decode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

    }
}