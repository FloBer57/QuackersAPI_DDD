namespace QuackersAPI_DDD.Application.DTO.Request
{
    public class GetAllPersonsRequestDTO
    {
        public string SearchTerm { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
