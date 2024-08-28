﻿using Newtonsoft.Json;

namespace DigaoRunnerApp.Services
{
    public class Customization
    {

        public static Customization Instance { get; set; }

        public string Font = "Consolas";
        public float Size = 12;
        public Color ColorNormal = Color.LimeGreen;
        public Color ColorError = Color.Crimson;
        public Color ColorBack = Color.FromArgb(30, 30, 30);
        public bool WordWrap = true;

        private static string GetFile()
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DigaoRunner");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            return Path.Combine(folder, "config.json");
        }

        public static void Load()
        {
            var file = GetFile();
            if (File.Exists(file))
            {
                var jsonData = File.ReadAllText(file);
                Instance = JsonConvert.DeserializeObject<Customization>(jsonData);
            }
            else
            {
                Instance = new Customization();
            }
        }

        public void LoadVisual()
        {
            LogService.Form.EdLog.Font = new Font(Font, Size);
            LogService.Form.EdLog.WordWrap = WordWrap;
            LogService.Form.EdLog.BackColor = ColorBack;
        }

        public void Save()
        {
            File.WriteAllText(GetFile(), JsonConvert.SerializeObject(this));
        }

    }
}
