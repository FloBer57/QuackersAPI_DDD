using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO
{
    public class PersonDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilPicturePath { get; set; }
        public string Description { get; set; } = "Je suis nouveau sur Quackers!";
        public bool IsTemporaryPassword { get; set; }
        public string TokenResetPassword { get; set; }
        public int PersonJobTitle_Id { get; set; } = 1;
        public int PersonStatut_Id { get; set; } = 1;
        public int PersonRole_Id { get; set; } = 1;


        public PersonDTO(Person person)
        {
            Id = person.Person_Id;
            FirstName = person.Person_FirstName;
            LastName = person.Person_LastName;
            PhoneNumber = person.Person_PhoneNumber;
            Email = person.Person_Email;
            Password = person.Person_Password;
            ProfilPicturePath = person.Person_ProfilPicturePath;
            Description = person.Person_Description;
            IsTemporaryPassword = person.Person_IsTemporaryPassword;
            TokenResetPassword = person.Person_TokenResetPassword;
            PersonJobTitle_Id = person.PersonJobTitle_Id;
            PersonStatut_Id = person.PersonStatut_Id;
            PersonRole_Id = person.PersonRole_Id;
        }
    }
}