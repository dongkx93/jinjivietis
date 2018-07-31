using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Common
{
    public class HelperCommon
    {
        public static List<SelectListItem> convertToListItem(Dictionary<string , string> dictionarys)
        {
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach(KeyValuePair<string , string> entry in dictionarys)
            {
                selectListItem.Add(new SelectListItem { Text = entry.Key , Value = entry.Value});
            }

            return selectListItem;
        }

        public static string hashPassword(string password)
        {
            string hasedPassword = "";
            using (var sha256 = new SHA256Managed())
            {
                var hasedByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                hasedPassword = BitConverter.ToString(hasedByte).Replace("-" ,"").ToLower();
            }
            return hasedPassword;
        }

        public static void setSelected(List<SelectListItem> selectListItem , string selectedKey)
        {
            foreach(var item in selectListItem)
            {
                if(item.Value == selectedKey)
                {
                    item.Selected = true;
                }
            }
        }

        public static string saveAvatarFile(HttpPostedFileBase avatarFile , string id)
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(ConstantCommon.EMPLOYEEROOTPATH, id));
            string filePath = String.Format("{0}\\{1}{2}" , directoryInfo.FullName , id , ".png");
               // Path.Combine(DirectoryInfo.FullName + "\\" + id + ".png");

            avatarFile.SaveAs(filePath);
            string databaseFilePath = filePath.Substring(filePath.IndexOf(ConstantCommon.EMPLOYEEFOLDERPATH));
            // "\\Content\\EmployeeInformation\\"
            return databaseFilePath;
        }

        public static Dictionary<string , string> getPersonalDocuments(string employeeId)
        {
            Dictionary<string , string> documents = new Dictionary<string, string>();
            var directoryPath = HttpContext.Current.Server.MapPath("~/Content/EmployeeInformation/" + "/" + employeeId + "/" + employeeId);
            
            
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                                where e.employeeId == employeeId
                                select e).First();
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                query.personalDocumentPath = directoryPath.Substring(directoryPath.IndexOf("Content\\EmployeeInformation\\"));
            }
                documents.Add("name", query.name);
                db.SaveChanges();
            }
                    
            
            string[] files = Directory.GetFiles(directoryPath);

            foreach(string file in files)
            {
                FileInfo f = new FileInfo(file);
                string pathLink = f.FullName.Substring(f.FullName.IndexOf("\\Content\\EmployeeInformation\\"));
                documents.Add(f.Name, pathLink);
            }
            return documents;
        }

        public static bool addPersonalDocument(HttpPostedFileBase postFile,string employeeId)
        {
            using (sys_employeeEntities db = new sys_employeeEntities())
            {
                var query = (from e in db.employee
                             where e.employeeId == employeeId
                             select e).First();
                var directoryPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), query.personalDocumentPath);
                var fullFilePath = String.Format("{0}\\{1}" , directoryPath , postFile.FileName);

                postFile.SaveAs(fullFilePath);
            }

            return true;
        }

        public static bool deletePeronalDocument(string id , string filename)
        {
            var filePath = string.Format(@"{0}\{0}\{1}", id , filename);
            var folderPath = HttpContext.Current.Server.MapPath("~/Content/EmployeeInformation");
            var fullFilePath = Path.Combine(folderPath , filePath);
            File.Delete(fullFilePath);
            return true;
        }

        public static List<FileStream> extractZipfileToFileStream(MemoryStream ms , string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            List<FileStream> fileStreams = new List<FileStream>();
            ZipArchive archive = new ZipArchive(ms);
            foreach(ZipArchiveEntry entry in archive.Entries)
            {
                Stream unzippedEntryStream = entry.Open();
                string fileName = entry.Name;
                string filePath = Path.Combine(folderPath, fileName);
                FileStream fs = new FileStream(filePath,FileMode.OpenOrCreate);
                unzippedEntryStream.CopyTo(fs);
                fileStreams.Add(fs);
            }
            return fileStreams;
        }

        public static FileStream saveSalaryDetail(string salaryMonth , HttpPostedFileBase salaryPdfFile) {
            string salaryDirectory = salaryMonth.Substring(0, 7).Replace("/", "");
            DirectoryInfo salaryDirectoryInfo = Directory.CreateDirectory(Path.Combine(ConstantCommon.SALARYFOLDERROOTPATH, salaryDirectory));
            string salaryFilePath = String.Format("{0}\\{1}" , salaryDirectoryInfo.FullName , salaryPdfFile.FileName);
            FileStream fs = new FileStream(salaryFilePath , FileMode.OpenOrCreate);
            salaryPdfFile.InputStream.CopyTo(fs);
            return fs;
        }
    }
}