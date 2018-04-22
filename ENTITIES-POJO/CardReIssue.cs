namespace ENTITIES_POJO
{
    public class CardReIssue : BaseEntity
    {
        public int Type { get; set; }
        public Card Card { get; set; }

        public CardReIssue()
        {

        }
    }
}
