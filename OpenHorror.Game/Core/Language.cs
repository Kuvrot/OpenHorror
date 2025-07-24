using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using System.Text.Json;
using System.IO;

namespace OpenHorror.Core
{
    public class Language : SyncScript
    {

        #region singleton
        public static Language Instance { get; set;}
         public override void Start()
         {
            Instance = this;
            LoadLanguage("language.json");
         }
        #endregion

        private Dictionary<string, string> texts;
        private bool languageFileFound = false;

        private void LoadLanguage(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                texts = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                languageFileFound = true;
            }
            catch (Exception e)
            {
                languageFileFound = false;
            }
            
        }

        public string Translate(string key)
        {
            if (texts.ContainsKey(key) && languageFileFound)
                return texts[key];
            return key;
        }

        public override void Update()
        {
        }
    }
}
