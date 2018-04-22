using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.Models.Controls
{
    public class CtrlTextAreaModel : CtrlBaseModel
    {
        public string LabelName { get; set; }
        public int Rows { get; set; }
        public string ColumnDataName { get; set; }

        public CtrlTextAreaModel()
        {
            ViewName = "";
        }
    }
}