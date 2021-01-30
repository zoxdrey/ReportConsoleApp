using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReportConsoleApp
{
    class Reporter
    {
        string path = @"reports";
        string errorFileName = "Error_";
        string errorText = "Report error";
        string timeoutFileName = "Timeout_";
        string timeoutText = "Timeout error";
        public void ReportSuccess(byte[] Data, int Id)
        {
            writeFile(Data, Id);
        }
        public void ReportError(int Id)
        {
            writeFile(errorText, Id, errorFileName);
        }

        public void ReportTimeout(int Id)
        {
            writeFile(timeoutText, Id, timeoutFileName);
        }

        private void CreateDirectory(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

        }

        private void writeFile(byte[] Data, int Id)
        {
            CreateDirectory(path);
            using FileStream fstream = new FileStream($"{path}/Report_[{Id}].txt", FileMode.OpenOrCreate);
            fstream.Write(Data, 0, Data.Length);
        }

        private void writeFile(string Data, int Id, string fileName)
        {
            CreateDirectory(path);
            byte[] byteData = System.Text.Encoding.Default.GetBytes(Data);
            using FileStream fstream = new FileStream($"{path}/{fileName}_[{Id}].txt", FileMode.OpenOrCreate);
            fstream.Write(byteData, 0, byteData.Length);
        }
    }
}
