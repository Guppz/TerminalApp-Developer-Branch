using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace COREAPI
{
    public class ComplaintManager
    {
        private ComplaintCrud CrudFactory;

        public ComplaintManager()
        {
            CrudFactory = new ComplaintCrud();
        }

        public void Create(Complaint complaint)
        {
            try
            {
                ValidateFields(complaint);
                CrudFactory.Create(complaint);
                SendNotificationToTerminal(complaint);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Update(Complaint complaint)
        {
            try
            {
                ValidateFields(complaint);
                CrudFactory.Update(complaint);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Delete(Complaint complaint)
        {
            CrudFactory.Delete(complaint);
        }

        public Complaint RetrieveById(Complaint complaint)
        {
            Complaint NewComplaint = CrudFactory.Retrieve<Complaint>(complaint);
            ValidateObject(NewComplaint);
            BuildObjects(NewComplaint);

            return NewComplaint;
        }

        public void ValidateObject(Complaint complaint)
        {
            try
            {
                if (complaint == null)
                {
                    throw new BusinessException(5);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void UpdateComplaintsSettings(double number, int idParam, string name)
        {
            try
            {
                if (number > 0)
                {
                    CrudFactory.UpdateComplaintsSettings(number, idParam, name);
                }
                else
                {
                    throw new BusinessException(2);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public List<Complaint> RetrieveAll()
        {
            List<Complaint> LstComplaints = null;

            try
            {
                LstComplaints = CrudFactory.RetrieveAll<Complaint>();
                GenerateList(LstComplaints);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstComplaints;
        }

        public List<Complaint> RetrieveComplaintsByTerminal(Terminal terminal)
        {
            List<Complaint> LstComplaints = null;

            try
            {
                LstComplaints = CrudFactory.RetrieveComplaintsByTerminal<Complaint>(terminal);
                GenerateList(LstComplaints);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstComplaints;
        }

        public List<Complaint> RetrieveComplaintsByCompany(Company company)
        {
            List<Complaint> LstComplaints = null;

            try
            {
                LstComplaints = CrudFactory.RetrieveComplaintsByCompany<Complaint>(company);
                GenerateList(LstComplaints);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstComplaints;
        }

        public List<Complaint> RetrieveComplaintsByDateRange(DateTime beginDate, DateTime endDate)
        {
            List<Complaint> LstComplaints = null;

            try
            {
                LstComplaints = CrudFactory.RetrieveComplaintsByDateRange<Complaint>(beginDate, endDate);
                GenerateList(LstComplaints);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstComplaints;
        }

        public List<Complaint> RetrieveComplaintsByDateRangeAndCompany(DateTime beginDate, DateTime endDate, int idCompany)
        {
            List<Complaint> LstComplaints = null;

            try
            {
                LstComplaints = CrudFactory.RetrieveComplaintsByDateRangeAndCompany<Complaint>(beginDate, endDate, idCompany);
                GenerateList(LstComplaints);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstComplaints;
        }

        public List<Complaint> RetrieveComplaintsByDateRangeAndTerminal(int idTerminal, DateTime beginDate, DateTime endDate)
        {
            List<Complaint> LstComplaints = null;

            try
            {
                LstComplaints = CrudFactory.RetrieveComplaintsByDateRangeAndTerminal<Complaint>(idTerminal, beginDate, endDate);
                GenerateList(LstComplaints);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstComplaints;
        }

        public void ValidateFields(Complaint complaint)
        {
            PropertyInfo[] Props = complaint.GetType().GetProperties();

            foreach (PropertyInfo p in Props)
            {
                object Valor = p.GetValue(complaint, null);

                if (Valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
            }
        }

        public void GenerateList(List<Complaint> lstComplaints)
        {
            foreach (Complaint complaint in lstComplaints)
            {
                BuildObjects(complaint);
            }
        }

        public void BuildObjects(Complaint complaint)
        {
            complaint.Terminal = new TerminalCrud().Retrieve<Terminal>(complaint.Terminal);
            complaint.User = new UserCrud().Retrieve<User>(complaint.User);
            complaint.Driver = new DriverCrud().Retrieve<Driver>(complaint.Driver);
            complaint.Bus = new BusCrud().Retrieve<Bus>(complaint.Bus);
            complaint.Company = new CompanyCrud().Retrieve<Company>(complaint.Company);
        }

        public void SendNotificationToTerminal(Complaint complaint)
        {
            BuildObjects(complaint);
            int IsComplaintslimit = CrudFactory.VerifyComplaintsLimit(complaint.Company);
            string EmailBody = CreateBodyEmail(complaint, IsComplaintslimit);
            List<SystemParam> IssuerEmailInformation = new SystemParamManager().RetrieveIssuerEmailInfo();      

            MailAddress from = new MailAddress(IssuerEmailInformation[0].Value, "Terminal App");
            MailAddress to = new MailAddress(IssuerEmailInformation[0].Value, "Terminal");
            MailMessage Mail = new MailMessage(from, to);
            SmtpClient Client = new SmtpClient();
            Client.Port = 587;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Host = "smtp.gmail.com";
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential(IssuerEmailInformation[0].Value, IssuerEmailInformation[1].Value);
            Mail.Subject = "Queja enviada por usuario";
            Mail.Body = EmailBody;
            Mail.IsBodyHtml = true;
            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Client.Send(Mail);
        }

        private string CreateBodyEmail(Complaint complaint, int isComplaintsLimit)
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string Body = string.Empty;

            basePath = basePath.Replace("TerminalApp", "WEBUI").Replace("WEBUI-", "TerminalApp-");

            string folderPath = "Models\\ControlsHtml\\ComplaintSentTemplate.html";
            string path = basePath + folderPath;

            Body = System.IO.File.ReadAllText(path);

            Body = Body.Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));
            Body = Body.Replace("{Company}", complaint.Company.Name);

            if(isComplaintsLimit == 1)
            {
                Body = Body.Replace("{ComplaintsLimit}", "La compañía ha alcanzado el máximo de quejas, es necesario aplicar una multa");
            }
            else
            {
                Body = Body.Replace("{ComplaintsLimit}", "");
            }

            return Body;
        }
    }
}
