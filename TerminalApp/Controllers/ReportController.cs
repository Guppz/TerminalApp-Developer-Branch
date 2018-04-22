using Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class ReportController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();
        [Route("api/Report/TotalRevenue")]
        [HttpGet]
        public HttpResponseMessage GetTotalRevenue()
        {
            try
            {
                var g = new GestorSsrs();

                var report = g.RunReport("Reports", "TotalRevenue", new Dictionary<string, string>());
                var bytesArray = Convert.FromBase64String(report);
                string path = @"C:\_temp\TotalRevenue.pdf";
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                File.WriteAllBytes(path, bytesArray);
                FileStream fileStream = File.OpenRead(path);
                result.Content = new StreamContent(fileStream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "TotalRevenue.pdf"
                    };

                return result;



            }
            catch (BusinessException bex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }


        }
        [Route("api/Report/TotalRevenueByTerminal")]
        [HttpGet]
        public HttpResponseMessage GetTotalRevenueByTerminal(int id)
        {
            try
            {
                var g = new GestorSsrs();
                var dic = new Dictionary<string, string>();
                dic.Add("P_FKIdTerminal", id.ToString());
                var report = g.RunReport("Reports", "TotalRevenueByTerminal", dic);
                var bytesArray = Convert.FromBase64String(report);
                string path = @"C:\_temp\TotalRevenue.pdf";
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                File.WriteAllBytes(path, bytesArray);
                FileStream fileStream = File.OpenRead(path);
                result.Content = new StreamContent(fileStream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "TotalRevenueByTerminal.pdf"
                    };

                return result;



            }
            catch (BusinessException bex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
