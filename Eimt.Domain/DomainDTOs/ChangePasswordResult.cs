namespace Eimt.Domain.DomainDTOs
{
    public class ChangePasswordResult
    {
        public string Message { get;  set; }
        public bool Success => string.IsNullOrEmpty(Message);
    }
}