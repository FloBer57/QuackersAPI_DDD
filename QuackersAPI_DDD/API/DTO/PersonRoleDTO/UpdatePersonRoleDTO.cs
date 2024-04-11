using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonRoleDTO
{
    public class UpdatePersonRoleDTO
    {
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50, ErrorMessage = "Role name must be at most 50 characters long.")]
        public string PersonRole_Name { get; set; }
    }
}
