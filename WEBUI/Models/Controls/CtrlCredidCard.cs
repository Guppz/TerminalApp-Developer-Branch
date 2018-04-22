using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COREAPI;
using ENTITIES_POJO;

namespace WEBUI.Models.Controls
{
    public class CtrlCredidCard : CtrlBaseModel
    {
        public List<CredidCard> OptionList;
        public string Label { get; set; }
        public string ColumnDataName { get; set; }
        public int IdListRequired { get; set; }

        public string OptionValues
        {
            get
            {
                User user = new User();
                user.IdUser = IdListRequired;
                CredidCard cc = new CredidCard();
                cc.User = user;
                OptionList = new CredidCardManager().RetrieveAll(cc);

                var selectOptions = " ";

                foreach (var option in OptionList)
                {
                    selectOptions += "<option value=" + '"' + option.credidCardNum + '"' + ">" + option.credidCardNum + "</option>";
                }

                return selectOptions;
            }
        }

        public CtrlCredidCard()
        {
            ViewName = "";

        }
    }
}