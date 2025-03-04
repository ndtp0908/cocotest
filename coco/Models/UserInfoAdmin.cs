namespace coco.Models
{
    public class UserInfoAdmin
    {
        public string UserId { get; set; }
        public string UserName { get; set; }  // Thêm nếu chưa có
        public string Name { get; set; }      // Thêm nếu chưa có
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateOnly? Birthday { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }  // Thêm nếu chưa có
    }
}
