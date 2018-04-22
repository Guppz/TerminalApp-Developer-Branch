using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.Models.Controls
{
    public class CtrlInputModel : CtrlBaseModel
    {
        public string LabelName { get; set; }
        public string InputType { get; set; }
        public string PlaceHolder { get; set; }
        public string ColumnDataName { get; set; }

        public CtrlInputModel()
        {

        }
    }
}