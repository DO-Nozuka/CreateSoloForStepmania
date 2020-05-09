using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CreateSoloForStepmania.Utilities
{
    public static class FileController
    {
        public static string GetExecutionPath()
        {
            Assembly myAssembly = Assembly.GetEntryAssembly();
            string path = myAssembly.Location;

            return Path.GetDirectoryName(path);
        }

        public static string GetFilePathFromOFD(string filter)
        {
            string result = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = GetExecutionPath();
                openFileDialog.Filter = filter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    result = openFileDialog.FileName;
                }
            }

            return result;
        }

        public static string GetFileContent(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return "";
            }
        }
    }
}
