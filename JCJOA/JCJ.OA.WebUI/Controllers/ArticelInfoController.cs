using JCJ.OA.BLL;
using JCJ.OA.Common;
using JCJ.OA.Model;
using JCJ.OA.Model.Search;
using JCJ.OA.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JCJ.OA.WebUI.Controllers
{
    public class ArticelInfoController : BaseController
    {
        // GET: ArticelInfo
        IBLL.IArticelClassService ArticelClassService { get; set; }
        IBLL.IArticelService ArticelService { get; set; }
        IBLL.ISensitiveWordService SensitiveWordService { get; set; }
        IBLL.IArticelCommentService ArticelCommentService { get; set; }
        public ActionResult Index()
        {
            ViewBag.classInfo = LoadArticelClassList();
            return View();
        }

        /// <summary>
        /// 加载新闻类别
        /// </summary>
        /// <returns></returns>
        private string LoadArticelClassList() {
            var articelParentClassList = ArticelClassService.LoadEntities(a => a.ParentId == 0);
            StringBuilder sb = new StringBuilder();
            foreach (var articelParentClass in articelParentClassList)
            {
                sb.Append("<option value='" + articelParentClass.ID + "'>" + "--" + articelParentClass.ClassName + "</option>");
                var articelChildClassList = ArticelClassService.LoadEntities(a => a.ParentId == articelParentClass.ID);
                foreach (var childClass in articelChildClassList)
                {
                    sb.Append("<option value=" + childClass.ID + ">" + "----" + childClass.ClassName + "</option>");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 搜索文章新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArticelInfo() {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;//当前页码。
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;//每页显示记录数。
            string fromDateTime = Request["formDatepicker"];
            string toDatepicker = Request["toDatepicker"];
            string articelClassId = Request["articelClassId"];
            string txtArticeTitle = Request["txtArticeTitle"];
            string txtArticelAuthor = Request["txtArticelAuthor"];
            int totalCount = 0;
            ArticelSearch articelSearch = new ArticelSearch()
            {
                ArticelAuthor = txtArticelAuthor,
                ArticelClassId = articelClassId,
                FormDatepicker = fromDateTime,
                ToDatepicker = toDatepicker,
                ArticelTitle = txtArticeTitle,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount

            };
            var articelList = ArticelService.LoadSearchEntities(articelSearch);
            var temp = from a in articelList
                       select new { ID = a.ID, Title = a.Title, Author = a.Author, AddDate = a.AddDate, Origin = a.Origin, ClassName = from c in a.ArticelClass select c.ClassName };

            return Json(new { rows = temp, total = articelSearch.TotalCount }, JsonRequestBehavior.AllowGet);
            
        }
        /// <summary>
        /// 展示添加新闻的表单
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAddInfo()
        {
            ViewBag.classInfo = LoadArticelClassList();
            return View();
        }
        /// <summary>
        /// 完成文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult FileUp()
        {
            HttpPostedFileBase file = Request.Files["Filedata"];
            if (file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName);
                if (fileExt == ".jpg")
                {
                    string dir = "/ImageUp/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                    string newfileName = Guid.NewGuid().ToString().Substring(0, 8);
                    string fullDir = dir + newfileName + fileExt;
                    string flag = Request["flag"];
                    if (!string.IsNullOrEmpty(flag))//表示添加水印
                    {
                        using (Image img = Image.FromStream(file.InputStream))
                        {
                            using (Bitmap map = new Bitmap(img.Width, img.Height))
                            {
                                using (Graphics g = Graphics.FromImage(map))
                                {
                                    g.DrawImage(img, 0, 0, map.Width, map.Height);
                                    g.DrawString("传智播客", new Font("微软雅黑", 14.0f, FontStyle.Bold), Brushes.Red, new PointF(map.Width - 60, map.Height - 30));
                                    map.Save(Request.MapPath(fullDir), System.Drawing.Imaging.ImageFormat.Jpeg);
                                    return Content("ok:" + fullDir);

                                }
                            }
                        }
                    }
                    else//表示不添加水印
                    {
                        file.SaveAs(Request.MapPath(fullDir));
                        return Content("ok:" + fullDir);
                    }
                }
                else
                {
                    return Content("no:格式错误!!");
                }
            }
            else
            {
                return Content("no:请选择上传的文件");
            }
        }

        /// <summary>
        /// 完成文章新闻添加
        /// </summary>
        /// <param name="articelInfo"></param>
        /// <returns></returns>
        [ValidateInput(false)]//不对发送给该方法的HTML内容作安全检查
        [HttpPost]
        public ActionResult AddArticelInfo(Articel articelInfo)
        {
            articelInfo.AddDate = DateTime.Now;
            articelInfo.DelFlag = 0;
            articelInfo.ModifyDate = DateTime.Now;
            int cid = int.Parse(Request["ArticelClassInfo"]);
            ArticelService.AddEntity(cid, articelInfo);
            //完成内容保存以后，生成静态页面
            CreateHtmlPage(articelInfo, "add");
            //将新添加的文章写到队列中并且存储到Lucene.Net中
            SearchContent searchContent = new SearchContent();
            searchContent.Content = articelInfo.ArticleContent;
            searchContent.Id = articelInfo.ID;
            searchContent.Flag = 0;
            searchContent.AddDate = articelInfo.AddDate;
            searchContent.Title = articelInfo.Title;
            IndexManager.GetInstance().AddEqueue(searchContent);
            return Content("ok");
        }
        /// <summary>
        /// 展示要编辑的新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var articelInfo = ArticelService.LoadEntities(a => a.ID == id).FirstOrDefault();
            ViewBag.ArticelInfo = articelInfo;
            return View();
        }
        /// <summary>
        /// 获取根类别信息，填充Tree
        /// </summary>
        /// <returns></returns>
        public ActionResult GetArticelClass()
        {
            var articelClassList = ArticelClassService.LoadEntities(a => a.ParentId == 0);
            var temp = from a in articelClassList
                       select new { id = a.ID, text = a.ClassName };
            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查找子类别，并且将文章所属的类别前面的复选框选中
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowChildClass()
        {
            int classId = int.Parse(Request["classId"]);//父类别编号
            int articelId = int.Parse(Request["articelId"]);//文章编号
                                                            //找出父类下的子类
            var childClassList = ArticelClassService.LoadEntities(a => a.ParentId == classId);
            //获取文章的类别。
            var articelInfo = ArticelService.LoadEntities(a => a.ID == articelId).FirstOrDefault();
            var articelClassList = from a in articelInfo.ArticelClass
                                   select a.ID;
            List<ArticelClassModel> list = new List<ArticelClassModel>();
            foreach (var childClass in childClassList)
            {
                if (articelClassList.Contains(childClass.ID))
                {
                    ArticelClassModel articelClassModel = new ArticelClassModel();
                    articelClassModel.Checked = true;
                    articelClassModel.Id = childClass.ID;
                    articelClassModel.Text = childClass.ClassName;
                    list.Add(articelClassModel);
                }
                else
                {
                    ArticelClassModel articelClassModel = new ArticelClassModel();
                    articelClassModel.Id = childClass.ID;
                    articelClassModel.Text = childClass.ClassName;
                    list.Add(articelClassModel);
                }
            }
            var temp = from a in list
                       select new { id = a.Id, text = a.Text, @checked = a.Checked };
            return Json(temp, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 完成文章类别的修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditArticelClass()
        {
            string classId = Request["classId"];
            string[] classIds = classId.Split(',');
            int articelId = int.Parse(Request["articelId"]);
            List<int> list = new List<int>();
            foreach (string cid in classIds)
            {
                list.Add(Convert.ToInt32(cid));
            }
            return Content(ArticelService.EditArticelClass(articelId, list) ? "ok" : "no");
        }
        /// <summary>
        /// 完成新闻的更新.
        /// </summary>
        /// <param name="articelInfo"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult EditArticelInfo(Articel articelInfo)
        {
            var articel = ArticelService.LoadEntities(a => a.ID == articelInfo.ID).FirstOrDefault();
            articel.ArticleContent = articelInfo.ArticleContent;
            articel.Author = articelInfo.Author;
            articel.FullTitle = articelInfo.FullTitle;
            articel.ModifyDate = DateTime.Now;
            articel.Intro = articelInfo.Intro;
            articel.KeyWords = articelInfo.KeyWords;
            articel.Origin = articelInfo.Origin;
            articel.PhotoUrl = articelInfo.PhotoUrl;
            articel.Title = articelInfo.Title;
            articel.TitleFontColor = articelInfo.TitleFontColor;
            articel.TitleFontType = articelInfo.TitleFontType;
            articel.TitleType = articelInfo.TitleType;
            if (ArticelService.EditEntity(articel))
            {
                CreateHtmlPage(articel, "edit");
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        /// <summary>
        /// 生成静态页
        /// </summary>
        /// <param name="articelInfo"></param>
        /// <param name="flag"></param>
        public void CreateHtmlPage(Articel articelInfo, string flag)
        {
            string html = NVelocityHelper.RenderTemplate("ArticelTemplateInfo", articelInfo, "/ArticelTemplate/");
            string dir = "/ArticelHtml/" + articelInfo.AddDate.Year + "/" + articelInfo.AddDate.Month + "/" + articelInfo.AddDate.Day + "/";
            if (flag == "add")
            {
                //dir = "/ArticelHtml/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
            }
            //else
            //{
            //    dir = "/ArticelHtml/" + articelInfo.AddDate.Year + "/" + articelInfo.AddDate.Month + "/" + articelInfo.AddDate.Day + "/";
            //}

            string fullDir = dir + articelInfo.ID + ".html";
            System.IO.File.WriteAllText(Request.MapPath(fullDir), html, System.Text.Encoding.UTF8);

        }
        /// <summary>
        /// 加载相关的新闻.
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadLikeNews()
        {
            //根据找相同类别的新闻。
            int articelId = int.Parse(Request["articelId"]);
            var articelInfo = ArticelService.LoadEntities(a => a.ID == articelId).FirstOrDefault();
            var articelList = (from a in articelInfo.ArticelClass
                               from b in a.Articel
                               orderby b.ID descending
                               where b.ID != articelId
                               select b).Skip<Articel>(0).Take<Articel>(4);
            var temp = from a in articelList
                       select new { Id = a.ID, Title = a.Title, AddDate = a.AddDate };
            //  return Json(temp,JsonRequestBehavior.AllowGet);
            return Content(Common.SerializeHelper.SerializeToString(temp));

        }

        /// <summary>
        /// 一站静态化
        /// </summary>
        /// <returns></returns>
        public ActionResult SetAllArticelPage()
        {
            var articelList = ArticelService.LoadEntities(a => a.DelFlag == 0);
            foreach (var articelInfo in articelList)
            {
                CreateHtmlPage(articelInfo, "add");
            }
            return Content("ok");
        }
        /// <summary>
        /// 完成评论的添加
        /// </summary>
        /// <returns></returns>
        public ActionResult AddArticelComment()
        {
            int articelId = int.Parse(Request["articelId"]);
            string msg = Request["msg"];
            if (SensitiveWordService.FilterFobidWord(msg))
            {
                return Content("no:评论中含有禁用词!!");
            }
            else if (SensitiveWordService.FilterModWord(msg))
            {
                ArticelComment articelComment = new ArticelComment();
                articelComment.AddDate = DateTime.Now;
                articelComment.IsPass = 0;
                articelComment.Msg = msg;
                articelComment.ArticelID = articelId;
                ArticelCommentService.AddEntity(articelComment);
                return Content("no:评论待审查!!");
            }
            else
            {
                ArticelComment articelComment = new ArticelComment();
                articelComment.AddDate = DateTime.Now;
                articelComment.IsPass = 1;
                articelComment.Msg = msg;
                articelComment.ArticelID = articelId;
                ArticelCommentService.AddEntity(articelComment);
                return Content("ok:评论成功");
            }
        }
        /// <summary>
        /// 加载评论
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadArticelComment()
        {
            int articelId = int.Parse(Request["articelId"]);
            var commentList = ArticelCommentService.LoadEntities(a => a.ArticelID == articelId && a.IsPass == 1).ToList();
            List<ArticelCommentViewModel> newList = new List<ArticelCommentViewModel>();
            foreach (var articelComment in commentList)
            {
                ArticelCommentViewModel articelCommentModel = new ArticelCommentViewModel();
                TimeSpan ts = DateTime.Now - articelComment.AddDate;
                articelCommentModel.AddDate = Common.WebCommon.GetTimespan(ts);
                articelCommentModel.Msg = articelComment.Msg;
                newList.Add(articelCommentModel);
            }

            return Content(Common.SerializeHelper.SerializeToString(newList));
        }
        /// <summary>
        /// 设置文章的各种状态（推荐,置顶，滚动等等）
        /// </summary>
        /// <returns></returns>
        public ActionResult SetArticelState()
        {
            int articelId = int.Parse(Request["articelId"]);
            int flag = int.Parse(Request["flag"]);
            var articelInfo = ArticelService.LoadEntities(a => a.ID == articelId).FirstOrDefault();
            switch (flag)
            {
                case 1:
                    articelInfo.Recommend = 1;
                    break;
                case 2:
                    articelInfo.Popular = 1;
                    break;
                case 3:
                    articelInfo.Strip = 1;
                    break;
                case 4:
                    articelInfo.IsTop = 1;
                    break;
                case 5:
                    articelInfo.Rolls = 1;
                    break;

            }
            return Content(ArticelService.EditEntity(articelInfo) ? "ok" : "no");
        }
    }
}