using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonRoleDTO
{
    public class CreatePersonRoleDTO
    {
        [Required(ErrorMessage = "Channel type name is required.")]
        [StringLength(50, ErrorMessage = "Channel type name must be at most 50 characters long.")]
        public string PersonRole_Name { get; set; }
    }
}
