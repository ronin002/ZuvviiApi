namespace ZuvviiAPI.Dtos
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();
        }
        public int Status { get; set; }
        public string Error { get; set; }
        public List<string> Errors { get; set; }
    }
}
