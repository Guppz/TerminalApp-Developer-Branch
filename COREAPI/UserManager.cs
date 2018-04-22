using DATAACCESS.CRUD;
using ENTITIES_POJO;
using System.Collections.Generic;
using System;
using System.Net.Mail;
using System.Net;
using Exceptions;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace COREAPI
{
    public class UserManager
    {
        private UserCrud UCrud;
        private RoleCrud RCrud;
        private UserPasswordCrud PasswordCrud;
        private ViewCrud VCrud;
        private TerminalCrud TCrud;

        public UserManager()
        {
            UCrud = new UserCrud();
            RCrud = new RoleCrud();
            PasswordCrud = new UserPasswordCrud();
            VCrud = new ViewCrud();
            TCrud = new TerminalCrud();
        }

        public void Create(User user)
        {
            try
            {
                var userExist = UCrud.UserExist<User>(user);
                if (userExist != null)
                {
                    throw new BusinessException(6);
                }

                UserPassword up = new UserPassword
                {
                    Password = Membership.GeneratePassword(12, 1),
                    DateExpiry = GetExpicyDate(-3)
                };
                user.Password = up;

                foreach (Role RolxU in user.Roleslist)
                {
                    if (RolxU.IdRole == 1 || RolxU.IdRole == 36)
                    {
                        user.UserTerminal = new Terminal();
                        user.UserTerminal.IdTerminal = -1;
                    }

                }
                user = UCrud.CreateUser(user);
                foreach (Role RolxU in user.Roleslist)
                {

                    RCrud.CreateRolexUser(RolxU, user);
                }
                PasswordCrud.Create(user);

                SentEmail(user, "Su usuario ha sido creado exitosamente!");

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(User user)
        {
            UCrud.Delete(user);
        }

        public List<User> RetrieveAll()
        {
            List<User> lstUser = UCrud.RetrieveAll<User>();
            foreach (User u in lstUser)
            {
                u.Roleslist = RCrud.RetrieveRolesByUser<Role>(u);
                if (u.UserTerminal.IdTerminal != -1)
                {
                    u.UserTerminal = TCrud.Retrieve<Terminal>(u.UserTerminal);
                }

            }

            return lstUser;
        }

        public void Update(User user)
        {
            List<Role> list = new List<Role>();
            list = user.Roleslist;
            RCrud.DelteRolexUser(user);
            foreach (Role RolxU in user.Roleslist)
            {
                if (RolxU.IdRole == 1 || RolxU.IdRole == 36)
                {
                    user.UserTerminal = new Terminal();
                    user.UserTerminal.IdTerminal = -1;
                }

            }
            UCrud.Update(user);

            foreach (Role RolxU in list)
            {

                RCrud.CreateRolexUser(RolxU, user);
            }
        }

        public User RetrieveById(int idUser)
        {
            throw new System.NotImplementedException();
        }

        public List<User> RetrieveByTerminal(User user)
        {

            List<User> lstUser = UCrud.RetrieveAllByTerminal<User>(user);
            foreach (User u in lstUser)
            {
                u.Roleslist = RCrud.RetrieveRolesByUser<Role>(u);
                if (u.UserTerminal.IdTerminal != -1)
                {
                    u.UserTerminal = TCrud.Retrieve<Terminal>(u.UserTerminal);
                }

            }

            return lstUser;
        }

        public void RecoverPassword(User user)
        {
            int minusDays = -1;
            try
            {
                var userExist = UCrud.UserExist<User>(user);
                if (userExist == null)
                {
                    throw new BusinessException(15);
                }
                List<UserPassword> PassList = PasswordCrud.RetrieveAllPasswordsByUser<UserPassword>(userExist);
                userExist.Password = PasswordCrud.Retrieve<UserPassword>(userExist);
                userExist.Password.Password = Membership.GeneratePassword(12, 1);
                userExist.Password.DateExpiry = userExist.Password.DateExpiry.AddDays(1);

                if (PassList.Count < 6)
                {

                    PasswordCrud.Create(userExist);
                }
                else
                {

                    foreach (UserPassword Pass in PassList)
                    {
                        if (userExist.Password.DateExpiry > Pass.DateExpiry)
                        {
                            userExist.Password.DateExpiry = Pass.DateExpiry;
                        }
                    }
                    PasswordCrud.Update(userExist);
                }
                PassList = PasswordCrud.RetrieveAllPasswordsByUser<UserPassword>(userExist);
                user.IdUser = userExist.IdUser;
                foreach (UserPassword Pass in PassList)
                {
                    user.Password = Pass;
                    PasswordCrud.ResetPasswords(user, minusDays--);
                }
                SentEmail(userExist, "Se ha reconfigurado su contraseña");
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void ModifyPassword(User user, string newPassword)
        {
            try
            {
                var Us = UCrud.UserExist<User>(user);
                if (Us == null)
                {
                    throw new BusinessException(11);
                }

                Us.Password = PasswordCrud.Retrieve<UserPassword>(Us);

                if (!ValidatePassword(user.Password.Password, Us.Password.Password))
                {
                    throw new BusinessException(11);
                }

                List<UserPassword> PassList = PasswordCrud.RetrieveAllPasswordsByUser<UserPassword>(Us);

                foreach (UserPassword Passw in PassList)
                {

                    if (ValidatePassword(newPassword, Passw.Password))
                    {
                        throw new BusinessException(14);
                    }
                }

                Us.Password.Password = newPassword;

                if (PassList.Count < 6)
                {
                    Us.Password.DateExpiry = GetExpicyDate(3);
                    PasswordCrud.Create(Us);
                }
                else
                {
                    PasswordCrud.Update(Us);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public User LoginUser(User user)
        {
            try
            {
                var Us = UCrud.UserExist<User>(user);
                if (Us == null)
                {
                    throw new BusinessException(11);
                }

                var Pass = PasswordCrud.Retrieve<UserPassword>(Us);

                if (!ValidatePassword(user.Password.Password, Pass.Password))
                {
                    throw new BusinessException(11);
                }

                if (Pass.DateExpiry < DateTime.Now)
                {
                    throw new BusinessException(12);
                }

                Us.Roleslist = RCrud.RetrieveRolesByUser<Role>(Us);
                Us.ViewList = VCrud.RetrieveViewsByUser<View>(Us);

                foreach (Role RolxU in Us.Roleslist)
                {
                    if (RolxU.IdRole == 1 || RolxU.IdRole == 36)
                    {
                        Us.UserTerminal = new Terminal();
                        Us.UserTerminal.IdTerminal = -1;
                    }

                }
                if (Us.UserTerminal.IdTerminal != -1)
                {
                    Us.UserTerminal = TCrud.Retrieve<Terminal>(Us.UserTerminal);
                }

                return Us;
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return null;
        }

        public void ValidatePasswordFormat(string password)
        {
            throw new System.NotImplementedException();
        }

        public bool ValidatePassword(string typedPassword, string dbPassword)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                if (VerifyMd5Hash(md5Hash, typedPassword, dbPassword))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ValidateFields(User user)
        {
            throw new System.NotImplementedException();
        }

        public void ValidateNonExistingUser(User user)
        {
            throw new System.NotImplementedException();
        }

        private DateTime GetExpicyDate(int amount)
        {
            var d = DateTime.Now;

            d = d.AddMonths(amount);
            return d;
        }


        public void SentEmail(User user, string subject)
        {
            string emailBody = CreateBodyEmail(user, subject);
            List<SystemParam> EmailInformation = new SystemParamCrud().RetrieveIssuerEmailInfo<SystemParam>();

            MailAddress from = new MailAddress(EmailInformation[0].Value, "Terminal App");
            MailAddress to = new MailAddress(user.Email, user.Name);
            MailMessage Mail = new MailMessage(from, to);
            SmtpClient Client = new SmtpClient();
            Client.Port = 587;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Host = "smtp.gmail.com";
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential(EmailInformation[0].Value, EmailInformation[1].Value);
            Mail.Subject = subject;
            Mail.Body = emailBody;
            Mail.IsBodyHtml = true;
            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Client.Send(Mail);
        }

        private string CreateBodyEmail(User user, string subject)
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string Body;
            basePath = basePath.Replace("TerminalApp", "WEBUI").Replace("WEBUI-", "TerminalApp-");
            if (subject == "Su usuario ha sido creado exitosamente!")
            {
                string folderPath = "Models\\ControlsHtml\\EmailTemplate.html";
                string path = basePath + folderPath;

                Body = System.IO.File.ReadAllText(path);

                Body = Body.Replace("{UserName}", user.Name);
                Body = Body.Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));
                Body = Body.Replace("{Password}", user.Password.Password);
            }
            else
            {
                string folderPath = "Models\\ControlsHtml\\ModifyPasswordTemplate.html";
                string path = basePath + folderPath;

                Body = System.IO.File.ReadAllText(path);

                Body = Body.Replace("{UserName}", user.Name);
                Body = Body.Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));
                Body = Body.Replace("{Password}", user.Password.Password);
                Body = Body.Replace("{Email}", user.Email);
            }
            return Body;
        }
        public int CreateAgreementUser(User user)
        {
            try
            {
                CardManager cardsCrud = new CardManager();
                Card card = new Card();
                var exist = 0;
                card.Terminal = user.UserTerminal;
                Agreement agg = new Agreement();
                agg.IdAgreement = user.IdUser;
                var userExist = UCrud.UserExist<User>(user);
                if (userExist != null)
                {
                    var count = 0;
                    count++;
                    return exist = 1;
                }
                else
                {
                    UserPassword up = new UserPassword
                    {
                        Password = Membership.GeneratePassword(12, 1),
                        DateExpiry = GetExpicyDate(-3)
                    };
                    user.Password = up;

                    foreach (Role RolxU in user.Roleslist)
                    {
                        if (RolxU.IdRole == 1 || RolxU.IdRole == 36)
                        {
                            user.UserTerminal = new Terminal();
                            user.UserTerminal.IdTerminal = -1;
                        }

                    }
                    var newUser = UCrud.CreateUser(user);
                    foreach (Role RolxU in newUser.Roleslist)
                    {
                        RCrud.CreateRolexUser(RolxU, newUser);
                    }
                    DateTime date = new DateTime();
                    CardType cy = new CardType();
                    card.DaysForNotification = 10;
                    card.Balance = 3000;
                    card.agreement = agg;
                    if (user.idConvenio == 1)
                    {
                        cy.IdCardType = 3;
                    }
                    if (user.idConvenio == 2)
                    {
                        cy.IdCardType = 2;
                    }
                    card.ExpiryDate = date;
                    card.CrType = cy;
                    card.User = newUser;
                    PasswordCrud.Create(user);
                    SentEmail(user, "Su usuario ha sido creado exitosamente!");
                    cardsCrud.Create(card);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
                return 0;
            }
        }
    }
}
