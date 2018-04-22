using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COREAPI;
using ENTITIES_POJO;

namespace WEBUI.Models.Controls
{
    public class CtrlComboBoxModel : CtrlBaseModel
    {
        public List<ValueListSelect> OptionList;
        public string Label { get; set; }
        public string ColumnDataName { get; set; }
        public string IdListRequired { get; set; }

        public string OptionValues
        {
            get
            {
                OptionList = new ValueListManager().GetValuesList(new ValueListSelect { IdList = IdListRequired });

                var selectOptions = " ";

                foreach (var option in OptionList)
                {
                    selectOptions += "<option value=" + '"' + option.Value + '"' + ">" + option.Description + "</option>";
                }

                return selectOptions;
            }
        }

        public CtrlComboBoxModel()
        {
            ViewName = "";

        }
    }
}