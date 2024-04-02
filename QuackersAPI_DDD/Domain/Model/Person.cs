using QuackersAPI_DDD.Domain.Utilitie;

namespace QuackersAPI_DDD.Domain.Model
{
    public class Person
    {
        public int Person_Id { get; set; }
        public string Person_Password { get; set; } = PasswordGenerator.GeneratePassword();
        public string Person_Email { get; set; }
        public string Person_PhoneNumber { get; set; }
        public string Person_FirstName { get; set; }
        public string Person_LastName { get; set; }
        public DateTime Person_CreatedTimeUser { get; set; } = DateTime.Now;
        public string Person_ProfilPicturePath { get; set; } = "/path/to/defaultImage";
        public string Person_Description { get; set; } = "Je suis nouveau sur Quackers!";
        public bool Person_IsTemporaryPassword { get; set; } = true;
        public string Person_TokenResetPassword { get; set; } = SecurityService.GenerateToken();
        public int PersonJobTitle_Id { get; set; } = 1;
        public int PersonStatut_Id { get; set; } = 1;
        public int PersonRole_Id { get; set; } = 1;


        public ICollection<Channel> Channels { get; set; }
        public virtual PersonJobTitle PersonJobTitle { get; set; }
        public virtual PersonStatut PersonStatut { get; set; }
        public virtual PersonRole PersonRole { get; set; } 

    }
}
