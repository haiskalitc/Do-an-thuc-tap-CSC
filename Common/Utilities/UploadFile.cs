using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class UploadFile
    {
        public static string DonViDirectory = "Data/LogoDonVi/";
        public static string KhaoSatDirectory = "Data/LogoKhaoSat/";
        public static string getFullFilePath(string directory, string fileName, out string savedFileName)
        {
            string fileID = Utility.ConvertToUnixTimestamp(DateTime.UtcNow,true).ToString();
            savedFileName = Path.GetFileName(fileID + fileName);
            string pathCombine = Path.Combine(
                  AppDomain.CurrentDomain.BaseDirectory,directory);
            pathCombine = Path.Combine(pathCombine, savedFileName);
            return pathCombine;
        }
    }
}
