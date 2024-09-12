namespace PB201Initial.Entities
{
    public class Group : BaseEntity
    {
        public string GroupNo { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
