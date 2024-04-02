namespace QuackersAPI_DDD.Domain.Model
{
    public class PersonRole
    {
        public int PersonRole_Id { get; set; }
        public string PersonRole_Name { get; set; }

        public ICollection<Person> Persons { get; set; }
    }
}
