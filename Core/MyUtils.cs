using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BF1RecordQuery.Core
{
    class MyUtils
    {
        public static string WindowTitle = "[BFBAN] 战地1战绩查询工具 v";

        /// <summary>
        /// 检查更新地址
        /// </summary>
        public static string CheckUpdateAdress = @"https://cdn.staticaly.com/gh/BFBAN/BF1RecordQuery/main/Server/version.txt";

        /// <summary>
        /// 下载更新地址
        /// </summary>
        public static string DownloadUpdateAddress = @"https://download.fastgit.org/BFBAN/BF1RecordQuery/releases/download/Latest/BF1RecordQuery.exe";

        /// <summary>
        /// 程序当前版本号，如：1.2.3.4
        /// </summary>
        public static string LocalVersionInfo = Application.ResourceAssembly.GetName().Version.ToString();

        /// <summary>
        /// 获取当前文件目录，不加文件名及后缀，例如：X:\xxx\xxx\ (.exe文件所在的目录+"\")
        /// </summary>
        public static string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string HttpDownloadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, bArr.Length);

            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, bArr.Length);
            }

            stream.Close();
            responseStream.Close();

            return path;
        }

        /// <summary>
        /// 执行CMD指令
        /// </summary>
        /// <param name="cmd"></param>
        public static void CMD_Code(string cmd)
        {
            Process CmdProcess = new Process();
            CmdProcess.StartInfo.FileName = "cmd.exe";
            CmdProcess.StartInfo.CreateNoWindow = true;                     // 不创建新窗口
            CmdProcess.StartInfo.UseShellExecute = false;                   // 不启用shell启动进程  
            CmdProcess.StartInfo.RedirectStandardInput = true;              // 重定向输入    
            CmdProcess.StartInfo.RedirectStandardOutput = true;             // 重定向标准输出    
            CmdProcess.StartInfo.RedirectStandardError = true;              // 重定向错误输出  
            CmdProcess.StartInfo.Arguments = "/c " + cmd;                   // "/C" 表示执行完命令后马上退出  
            CmdProcess.Start();                                             // 执行   
            CmdProcess.WaitForExit();                                       // 等待程序执行完退出进程  
            CmdProcess.Close();                                             // 结束  
        }
    }
}
