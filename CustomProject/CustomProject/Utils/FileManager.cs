using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Utils
{
    public static class FileManager
    { 

        public static void WriteToFile(string message, string path)
        {
            using (StreamWriter writetext = new StreamWriter(path))
            {
                writetext.WriteLine(message);
            }
        }

        public static void WriteToFile(List<string> messages, string path)
        {
            using (StreamWriter writetext = new StreamWriter(path))
            {
                foreach(string message in messages)
                {
                    writetext.WriteLine(message);
                }
            }
        }

        public static List<string> ReadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                LogProvider.AddError($"File {path} not exist.");
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

        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                LogProvider.AddError($"File {path} not exist.");
                return String.Empty;
            }
            var message = File.ReadAllText(path);
            return message;
        }
    }
}
