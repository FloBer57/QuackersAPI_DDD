namespace QuackersAPI_DDD.API.DTO.PersonDTO
{
    public class UpdatePersonDTO
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? ProfilPicturePath { get; set; } = "Path/To/Default/Image";
        // Exclure les champs qui ne devraient pas être mis à jour directement comme le mot de passe
    }

}
