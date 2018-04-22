using System;

namespace ENTITIES_POJO
{
    public class Sanction : BaseEntity
    {
        public int IdSanction { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public Terminal Terminal { get; set; }
        public Route Route { get; set; }
        public SanctionType Type { get; set; }

        public Sanction()
        {

        }
    }
}
