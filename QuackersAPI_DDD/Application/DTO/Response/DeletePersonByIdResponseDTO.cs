namespace QuackersAPI_DDD.Application.DTO.Response
{
    public class DeletePersonByIdResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public DeletePersonByIdResponseDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
