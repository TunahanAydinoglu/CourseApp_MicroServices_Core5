namespace FreeCourse.IdentityServer.Models.Request
{
    public class RegisterRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
    }
}