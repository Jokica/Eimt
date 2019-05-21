namespace Eimt.Application.Interfaces
{
    public class IdentityCreatedResult
    {
        public bool Success => !string.IsNullOrEmpty(Token);
        public string Token { get;  set; }
    }
}