using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class User : BaseEntity
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Identification { get; set;  }
        public DateTime  BirthDate { get; set; }
        public int idConvenio { get; set; }
        public string BirthDateString {
            get {
                var dateString = BirthDate.ToShortDateString();
                return dateString;
            }
            set {
            }
        }
        
        public UserPassword Password { get; set; }
        public List<Role> Roleslist { get; set; }
        public List<View> ViewList { get; set; }
        public Terminal UserTerminal { get; set; }
        public int Status { get; set; }
        public string StatusString { get {
                string ss;
                if (Status == 1)
                {
                    ss = "Activado";
                }
                else {
                    ss = "Desactivado";
                }
                return ss;

            } }
        public User()
        {

        }
    }
}
