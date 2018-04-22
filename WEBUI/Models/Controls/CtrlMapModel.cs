using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.Models.Controls
{
    public class CtrlMapModel : CtrlBaseModel
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }

        public CtrlMapModel(){

        }  
    }
}