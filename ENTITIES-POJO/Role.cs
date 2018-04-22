using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Role : BaseEntity
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public string statusActivate {

            get {
                var Statustring = Status.ToString();

                if (Status == 0)
                {

                    Statustring = "Desactivado";

                }
                else  if(Status == 1)
                {
                    Statustring = "Activado";


                }

                return Statustring;
            }


        }

        public List<View> ViewsPerRole { get; set; }
        public Role()
        {

        }
    }
}
