using System;

namespace ENTITIES_POJO
{
    public class Complaint : BaseEntity
    {
        public int IdComplaint { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public User User { get; set; }
        public Terminal Terminal { get; set; }
        public Bus Bus { get; set; }
        public Driver Driver { get; set; }
        public Company Company { get; set; }

        public Complaint()
        {

        }
    }
}
