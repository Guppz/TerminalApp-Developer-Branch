using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WEBUI.Models.Controls
{
    public class CtrlBaseModel
    {
        public string Id { get; set; }
        public string ViewName { get; set; }

        private string ReadFileText()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = "Models\\ControlsHtml\\";
            string fileName = this.GetType().Name + ".html";

            string path = basePath + folderPath + fileName;

            string text = System.IO.File.ReadAllText(path);

            return text;
        }

        public string GetHtml()
        {
            var html = ReadFileText();

            foreach (var prop in this.GetType().GetProperties())
            {
                var value = prop.GetValue(this, null).ToString();

                var tag = string.Format("-#{0}-", prop.Name);
                html = html.Replace(tag, value);
            }
            return html;
        }
    }
}