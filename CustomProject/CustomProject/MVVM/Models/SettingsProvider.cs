using CustomProject.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.MVVM.Models
{
    public class SettingsProvider
    {
        private static SettingsProvider instance;


        private SettingsProvider()
        { 
            
        }

        public static SettingsProvider Instance()
        {
            if (instance == null)
                instance = new SettingsProvider();
            return instance;
        }


        public static void LoadFromFile()
        { 
            instance = JsonConvert.DeserializeObject<SettingsProvider>(FileManager.ReadFile("Setting.json"));
        }

        public static void SaveToFile()
        {
            string output = JsonConvert.SerializeObject(instance);
            FileManager.WriteToFile(output, "Setting.json");
        }

    }
}
