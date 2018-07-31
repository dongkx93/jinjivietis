using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Common
{
    public class StringCommon
    {
        public static string extractFileNameWithoutExtension(string filePath , int start)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            return fileNameWithoutExtension.Substring(start);
        }

        public static string extractFileNameWithoutExtension(string filePath, int start , int length)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            return fileNameWithoutExtension.Substring(start, length);
        }
    }
}