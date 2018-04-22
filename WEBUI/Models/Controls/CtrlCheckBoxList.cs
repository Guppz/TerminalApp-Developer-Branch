using ENTITIES_POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.Models.Controls
{
    public class CtrlCheckBoxList : CtrlBaseModel
    {
        public List<ValueListSelect> List;
        public string Label { get; set; }
        public string ColumnDataName { get; set; }

        public string CheckValues
        {
            get
            {
                var selectOptions = "";
                foreach (var option in List)
                {
                    selectOptions += "<div class=" + '"' + "form-check" + '"' + ">" +
                         "<input type=" + '"' + "checkbox" + '"' + "class=" + '"' + "form-check-input" + '"' + " value=" + '"' + option.Value + '"' + ">" +
                        "<label class=" + '"' + "form-check-label" + '"' + ">" + option.Description + "</label>" +

                  "</div> ";
                }

                return selectOptions;
            }
        }
    }
}