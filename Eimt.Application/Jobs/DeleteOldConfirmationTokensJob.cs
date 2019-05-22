using Eimt.Application.Interfaces;
using Eimt.Domain.DomainModels;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eimt.Application.Jobs
{
    public class DeleteOldConfirmationTokensJob : IJob
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteOldConfirmationTokensJob(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var repository = unitOfWork.CreateRepository<UserConfirmationToken, long>();
            var tokens = repository
                            .Where(x => x.CreateAt.AddMinutes(1) >= DateTime.Now);
            if (tokens.Any())
            {
                var transation = unitOfWork.CreateTransaction();
                try
                {

                    foreach (var token in tokens)
                    {
                        repository.Remove(token);
                    }
                    unitOfWork.Commit();
                    transation.Commit();
                }
                catch 
                {
                    transation.Rollback();
                }
                finally
                {
                    transation.Dispose();
                }
            }
            return Task.FromResult(0);
        }
    }
}
