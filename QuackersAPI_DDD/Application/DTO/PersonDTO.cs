namespace QuackersAPI_DDD.Application.DTO
{
    public class PersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonDTO(string firstname, string lastName)
        {
            FirstName = firstname;
            LastName = lastName;
        }
    }
}