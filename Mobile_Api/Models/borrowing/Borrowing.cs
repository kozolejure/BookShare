namespace Web_Api.Models.borrowing
{
    public class Borrowing
    {

        public string BookId { get; set; }
        public string UserId { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
