using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBUI.Models.Controls;
using ENTITIES_POJO;
using COREAPI;
using System.Globalization;

namespace WEBUI.Models.Helpers
{
    public static class ControlExtensions
    {
        public static HtmlString CtrlTable(this HtmlHelper html, string viewName, string id, string title,
         string columnsTitle, string ColumnsDataName, string onSelectFunction)
        {
            var ctrl = new CtrlTableModel
            {
                ViewName = viewName,
                Id = id,
                Title = title,
                Columns = columnsTitle,
                ColumnsDataName = ColumnsDataName,
                FunctionName = onSelectFunction
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlInput(this HtmlHelper html, string id, string type, string label, string placeHolder = "", string columnDataName = "")
        {
            var ctrl = new CtrlInputModel
            {
                Id = id,
                InputType = type,
                LabelName = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName,
                ViewName = ""
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlButton(this HtmlHelper html, string viewName, string id, string label, string onClickFunction = "", string buttonType = "primary")
        {
            var ctrl = new CtrlButtonModel
            {
                ViewName = viewName,
                Id = id,
                Label = label,
                FunctionName = onClickFunction,
                ButtonType = buttonType
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlComboBox(this HtmlHelper html, string id, string label, string idList, string columnDataName = "", string viewname = "")
        {
            var comboBox = new CtrlComboBoxModel
            {
                Id = id,
                Label = label,
                ViewName = viewname,
                ColumnDataName = columnDataName,
                IdListRequired = idList
            };

            return new HtmlString(comboBox.GetHtml());
        }

        public static HtmlString CtrlCheckBoxList(this HtmlHelper html, string idList, string labelName, string columnDataName = "", string viewname = "")
        {
            ValueListManager Select = new ValueListManager();

            var buscarSelect = new ValueListSelect
            {
                IdList = idList
            };

            var options = Select.GetValuesList(buscarSelect);

            var checkList = new CtrlCheckBoxList
            {
                Id = idList,
                Label = labelName,
                List = options,
                ViewName = viewname,
                ColumnDataName = columnDataName
            };

            return new HtmlString(checkList.GetHtml());
        }

        public static HtmlString CtrlTextArea(this HtmlHelper html, string id, string label, int rows, string columnDataName = "")
        {
            var CtrlTextArea = new CtrlTextAreaModel
            {
                Id = id,
                LabelName = label,
                Rows = rows,
                ColumnDataName = columnDataName
            };
            return new HtmlString(CtrlTextArea.GetHtml());
        }

        public static HtmlString CtrlMap(this HtmlHelper html, string id, string name, string latitude = "9.9281", string longitude = "84.0907", string viewName = "")
        {
            var map = new CtrlMapModel
            {
                ViewName = viewName,
                Id = id,
                Name = name,
                Latitude = float.Parse(latitude, CultureInfo.InvariantCulture),
                Longitude = float.Parse(longitude, CultureInfo.InvariantCulture)
            };

            return new HtmlString(map.GetHtml());
        }

        public static HtmlString CtrlCredidCard(this HtmlHelper html, string id, string label, string idList, string columnDataName = "", string viewname = "")
        {
            var comboBox = new CtrlComboBoxModel
            {
                Id = id,
                Label = label,
                ViewName = viewname,
                ColumnDataName = columnDataName,
                IdListRequired = idList
            };

            return new HtmlString(comboBox.GetHtml());
        }
    }
}


