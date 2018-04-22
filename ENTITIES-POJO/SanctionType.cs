namespace ENTITIES_POJO
{
    public class SanctionType : BaseEntity
    {
        public int IdType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public int Status { get; set; }

        public SanctionType()
        {

        }
    }
}
