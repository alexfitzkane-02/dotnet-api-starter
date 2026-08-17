namespace dotnet_api_starter.Models.Dto
{
    public class LoginResponseDto
    {
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
