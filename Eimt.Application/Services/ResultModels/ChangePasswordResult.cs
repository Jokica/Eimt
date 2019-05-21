using Eimt.Domain.DomainDTOs;

namespace Eimt.Application.Services.Impl
{
    public class ChangePasswordResult
    {
        public ChangePasswordResult()
        {

        }
        public ChangePasswordResult(Domain.DomainDTOs.ChangePasswordResult result)
        {
            this.Message = result.Message;
        }
        public string Message { get;  set; }
        public bool Success => string.IsNullOrEmpty(Message);
    }
}