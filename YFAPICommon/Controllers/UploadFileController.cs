using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YFAPICommon.Controllers
{
    public class FileBase64Input
    {
        public string base64 { set; get; }
    }

    public class UploadFileOutput
    {
        public string url { set; get; }
        public int error { set; get; }
        public string msg { set; get; }
    }

    public class UploadFileController : ApiController
    {
        private static string serverPath = System.Configuration.ConfigurationSettings.AppSettings["serverPath"];
        private static string localPath = System.Configuration.ConfigurationSettings.AppSettings["localPath"];

        [HttpPost]
        public UploadFileOutput UploadWithStream()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            UploadFileOutput returnNode = new UploadFileOutput();

            string date = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                System.Web.HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型     

                    //string fileFilt = ".gif|.jpg|.bmp|.jpeg|.png";
                    //if (fileFilt.IndexOf(fileExt) <= -1)
                    //{
                    //    return "1";
                    //}

                    //判断文件大小     
                    int length = hpf.ContentLength;
                    if (length > 16240000)
                    {
                        returnNode.error = 2;
                        returnNode.msg = "文件大小超出限制";
                        return returnNode;
                    }

                    if (localPath.Trim().Length == 0)
                    {
                        localPath = System.Web.HttpContext.Current.Server.MapPath("/");
                    }

                    DateTime dt = DateTime.Now;
                    string folderPath = dt.Year + "\\" + dt.Month + "\\" + dt.Day + "\\";
                    string serverFolderPath = "/" + dt.Year + "/" + dt.Month + "/" + dt.Day + "/";
                    if (Directory.Exists(localPath + folderPath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(localPath + folderPath);
                    }

                    string fileName = System.Guid.NewGuid().ToString() + Path.GetExtension(hpf.FileName);

                    string filePath = localPath + folderPath + fileName;
                    hpf.SaveAs(filePath);
                    returnNode.error = 0;
                    returnNode.msg = "上传成功";
                    returnNode.url = serverPath + serverFolderPath + fileName;
                    return returnNode;
                }
            }
            returnNode.error = 3;
            returnNode.msg = "没有获取到文件";
            return returnNode;
        }

        [HttpPost]
        public UploadFileOutput UploadWithBase64(FileBase64Input input)
        {
            UploadFileOutput returnNode = new UploadFileOutput();
            try
            {
                if (input==null||string.IsNullOrWhiteSpace(input.base64))
                {
                    returnNode.error = 3;
                    returnNode.msg = "没有获取到文件";
                    return returnNode;
                }

                string[] array = input.base64.Split(',');
                if (array.Length == 2)
                    input.base64 = array[1];

                byte[] fileBytes = Convert.FromBase64String(input.base64);
                if (fileBytes == null)
                {
                    returnNode.error = 3;
                    returnNode.msg = "Base64解析错误";
                    return returnNode;
                }


                //判断文件大小     
                int length = fileBytes.Length;
                if (length > 16240000)
                {
                    returnNode.error = 2;
                    returnNode.msg = "文件大小超出限制";
                    return returnNode;
                }

                if (localPath.Trim().Length == 0)
                {
                    localPath = System.Web.HttpContext.Current.Server.MapPath("/");
                }

                DateTime dt = DateTime.Now;
                string folderPath = dt.Year + "\\" + dt.Month + "\\" + dt.Day + "\\";
                string serverFolderPath = "/" + dt.Year + "/" + dt.Month + "/" + dt.Day + "/";
                if (Directory.Exists(localPath + folderPath) == false)//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(localPath + folderPath);
                }

                string fileType = ".jpg";
                string fileName = System.Guid.NewGuid().ToString() + fileType;

                string filePath = localPath + folderPath + fileName;
                FileStream fs = new FileStream(filePath, FileMode.Create);
                //将byte数组写入文件中
                fs.Write(fileBytes, 0, fileBytes.Length);
                //所有流类型都要关闭流，否则会出现内存泄露问题
                fs.Close();


                returnNode.error = 0;
                returnNode.msg = "上传成功";
                returnNode.url = serverPath + serverFolderPath + fileName;
                return returnNode;
            }
            catch (Exception ex)
            {
                returnNode.error = 1;
                returnNode.msg = ex.Message;//文件上传失败   
            }
            return returnNode;
        }
    }
}
