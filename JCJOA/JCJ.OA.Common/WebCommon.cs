﻿using JCJ.OA.Model;
using System;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace JCJ.OA.Common
{
    public class WebCommon
    {
        //public static void Show()
        //{
        //    HttpContext.Current.Response.Write("sss");
        //}

            /// <summary>
            /// 对字符串进行MD5运算
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
        public static string GetMd5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5Buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            md5.Clear();
            return sb.ToString();
        }

        /// <summary>
        /// 判断Cookie
        /// </summary>
        public static bool ValidateCookieInfo(UserInfo userInfo)
        {
            bool isSucess = false;
            HttpContext context = HttpContext.Current;
            if (userInfo != null)
            {
                if (context.Request.Cookies["cp2"] != null)
                {
                    string userPwd = context.Request.Cookies["cp2"].Value;
                    if (userInfo.UPwd == userPwd)
                    {
                         //context.Session["userInfo"] = userInfo;
                        string sessionId = Guid.NewGuid().ToString();//必须保证Memcache的key唯一
                        Common.MemcacheHelper.Set(sessionId, Common.SerializeHelper.SerializeToString(userInfo), DateTime.Now.AddMinutes(20));//向Memcache中添加登录用户数据.
                        context.Response.Cookies["sessionId"].Value = sessionId;
                        isSucess = true;
                        return isSucess;
                    }
                }
            }
            context.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
            context.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            return isSucess;
        }
        /// <summary>
        /// 时间差处理
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string GetTimespan(TimeSpan ts)
        {
            if (ts.TotalDays > 365)
            {
                return Math.Floor(ts.TotalDays / 365) + "年前";
            }
            else if (ts.TotalDays > 30)
            {
                return Math.Floor(ts.TotalDays / 30) + "月前";
            }
            else if (ts.TotalHours > 24)
            {
                return Math.Floor(ts.TotalDays) + "天前";
            }
            else if (ts.TotalHours > 1)
            {
                return Math.Floor(ts.TotalHours) + "小时前";
            }
            else if (ts.TotalMinutes > 1)
            {
                return Math.Floor(ts.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }

        /// <summary>
        /// 对字符串进行分词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> GetPanGuWord(string str)
        {
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            List<string> list = new List<string>();
            while ((token = tokenStream.Next()) != null)
            {
                list.Add(token.TermText());
            }
            return list;
        }

        /// <summary>
        /// 创建HTMLFormatter,参数为高亮单词的前后缀
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string CreateHightLight(string keywords, string Content)
        {
            Content = ClearTab(Content);//过滤HTML标签
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
             new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
            new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
            new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 50;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keywords, Content);

        }

        /// <summary>
        /// 过滤HTML标签
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        private static string ClearTab(string Content)
        {
            Regex regex = new Regex(@"<[^>]*>");//匹配所有标签
            return regex.Replace(Content, "");
        }
    }
}
