using JCJ.OA.Model.Enum;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace JCJ.OA.WebUI.Models
{
    public sealed class IndexManager
    {
        private readonly static IndexManager indexManager = new IndexManager();
        private IndexManager()
        {
        }
        public static IndexManager GetInstance()
        {
            return indexManager;
        }
        /// <summary>
        /// 将信息写到队列中
        /// </summary>
        /// <param name="searchContent"></param>
        public void AddEqueue(SearchContent searchContent)
        {
            searchContent.LuceneActionType = LuceneActionType.Add;
            string str = Common.SerializeHelper.SerializeToString(searchContent);
            Common.RedisHelper.Enqueue("addLucene", str);
        }
        public void DeleteEqueue(SearchContent searchContent)
        {
            searchContent.LuceneActionType = LuceneActionType.Delete;
        }
        /// <summary>
        /// 开启线程.
        /// </summary>
        public void MyThread()
        {
            Thread myThread = new Thread(StartThread);
            myThread.IsBackground = true;
            myThread.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        private void StartThread()
        {

            while (true)
            {
                if (Common.RedisHelper.GetEqueueCount("addLucene") > 0)
                {
                    //try
                    // {
                    CreateSearchIndex();
                    //}
                    //  catch (Exception ex)
                    // {

                    // }
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }

        }


        public void CreateSearchIndex()
        {

            string indexPath = @"C:\lucenedir";//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
            bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
            if (isUpdate)
            {
                //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。
            //判断队列中是否有数据（注意：为什么写循环）
            while (Common.RedisHelper.GetEqueueCount("addLucene") > 0)
            {

                string str = Common.RedisHelper.Dequeue("addLucene");
                SearchContent model = Common.SerializeHelper.DeserializeToObject<SearchContent>(str);
                //注意：这里只根据ID删除是有问题的。有可能删除删除别的信息。
                writer.DeleteDocuments(new Term("Id", model.Id.ToString()));//删除

                if (model.LuceneActionType == LuceneActionType.Delete)
                {
                    continue;
                }


                Document document = new Document();//表示一篇文档。
                                                   //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("Id", model.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                document.Add(new Field("AddDate", model.AddDate.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

                document.Add(new Field("Flag", model.Flag.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("Title", model.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("Content", model.Content, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                writer.AddDocument(document);
            }


            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }
    }
}