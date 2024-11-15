namespace CommerceAPI_IOON.CommerceAPI.DTOs
{
    public class RegisterRequestDto
    {
        public string CommerceName { get; set; }
        public string CommerceAddress { get; set; }
        public string RUC {  get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
