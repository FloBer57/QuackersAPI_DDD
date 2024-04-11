namespace QuackersAPI_DDD.API.DTO.PersonDTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        // Ajouter d'autres champs au besoin, mais éviter les données sensibles comme le mot de passe
    }

}
