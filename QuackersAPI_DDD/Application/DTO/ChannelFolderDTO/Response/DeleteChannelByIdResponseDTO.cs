namespace QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response
{
    public class DeleteChannelByIdResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public DeleteChannelByIdResponseDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
