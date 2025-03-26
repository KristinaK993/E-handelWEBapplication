namespace E_handelWEBapplication.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }    // 1 - 5
        public string Comment { get; set; }
    }
}
