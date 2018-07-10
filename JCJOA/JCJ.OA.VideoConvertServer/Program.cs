using JCJ.OA.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCJ.OA.VideoConvertServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread myThread = new Thread(StartConvert);
            myThread.IsBackground = true;
            myThread.Start();
            Console.WriteLine("视频转换服务已经开启.............");
            Console.ReadKey();
        }

        private static void StartConvert(object obj)
        {
            int count = Common.RedisHelper.GetEqueueCount("videFileInfo");
            if (count > 0)
            {
                string str = Common.RedisHelper.Dequeue("videoFileInfo"); //出队
                VideoFileModel videoFile = Common.SerializeHelper.DeserializeToObject<VideoFileModel>(str);
                ConvertToVideo(videoFile);
            }
            else {
                Thread.Sleep(3000);
            }
        }
        /// <summary>
        /// 找到视频文件，并且进行转换
        /// </summary>
        /// <param name="videoFile"></param>
        private static void ConvertToVideo(VideoFileModel videoFile)
        {
            string srcFileName = @"d:\" + videoFile.VideoFileName + videoFile.videoFileExt;//视频存在转码服务器的D盘中。
            string destFile = @"d:\" + videoFile.VideoFileName + ".flv";//全部转换为Flv格式
            //string destImgFile = @"d:\" + videoFile.VideoFileName + ".jpg";
            Process p = new Process();
            //设置进程启动信息属性StartInfo，这是ProcessStartInfo类，包括了一些属性和方法：
            p.StartInfo.FileName = @"D:\传智讲课\0718就业班\OA\第十三天\资料\\ffmpeg.exe";        //程序名
            p.StartInfo.UseShellExecute = false;
            //-y选项的意思是当输出文件存在的时候自动覆盖输出文件，不提示“y/n”这样才能自动化
            p.StartInfo.Arguments = "-i " + srcFileName + " -y -ab 56 -ar 22050 -b 800 -r 29.97 -s 420x340 " + destFile;    //执行参数
                                                                                                                            //  p.StartInfo.Arguments = "-i " + srcFileName + " -y -f image2  -ss 53 -t 0.001 -s  600x500 " + destImgFile;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中
            p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.Start();
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程

            //将转换成功的flv视频回传给WEB服务器。
            WebClient webClient = new WebClient();
            //webClient.UploadData("http://")
        }

        private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
