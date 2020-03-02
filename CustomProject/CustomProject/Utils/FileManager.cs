using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Utils
{
    public class FileManager
    {

        private static FileManager instance;

        public static FileManager Instance()
        {
            if (instance == null)
            {
                instance = new FileManager();
                LogProvider.Instance().AddLog("Init File Manager.");
                InitFolders();
            }
            lock (instance)
            {
                return instance;
            }
        }

        private static void InitFolders()
        {
            List<String> DirectorysPath = new List<string>()
            {

            }; 

            foreach(string dir in DirectorysPath)
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }

        }

        public void WriteToFile(string message, string path)
        {
            using (StreamWriter writetext = new StreamWriter(path))
            {
                writetext.WriteLine(message);
            }
        }

        public void WriteToFile(List<string> messages, string path)
        {
            using (StreamWriter writetext = new StreamWriter(path))
            {
                foreach (string message in messages)
                {
                    writetext.WriteLine(message);
                }
            }
        }

        public List<string> ReadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                LogProvider.Instance().AddError($"File {path} not exist.");
                return new List<string>();
            }
            List<string> messages = new List<string>();
            using (StreamReader readtext = new StreamReader(path))
            {
                string line;
                while ((line = readtext.ReadLine()) != null)
                {
                    messages.Add(line);
                }
            }
            return messages;
        }

        public string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                LogProvider.Instance().AddError($"File {path} not exist.");
                return String.Empty;
            }
            var message = File.ReadAllText(path);
            return message;
        }
    }
}
