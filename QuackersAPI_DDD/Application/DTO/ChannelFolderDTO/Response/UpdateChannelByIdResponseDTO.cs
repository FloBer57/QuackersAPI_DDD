namespace QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response
{
    public class UpdateChannelByIdResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public UpdateChannelByIdResponseDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
