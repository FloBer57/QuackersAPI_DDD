namespace QuackersAPI_DDD.Domain.Utilitie
{
    public class NameGenerator
    {
        public static string GenerateUniqueAttachmentName(string originalName)
        {
            string extension = Path.GetExtension(originalName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalName);
            string uniqueName = $"{fileNameWithoutExtension}_{Guid.NewGuid()}{extension}";
            return uniqueName;
        }
    }
}
