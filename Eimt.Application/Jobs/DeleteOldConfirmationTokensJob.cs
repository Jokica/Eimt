using Eimt.Application.Interfaces;
using Eimt.Domain.DomainModels;
using Eimt.Persistence;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eimt.Application.Jobs
{
    public class DeleteOldConfirmationTokensJob : IJob
    {
        private readonly EiMTDbContext dbContext;

        public DeleteOldConfirmationTokensJob(DbContextOptions<EiMTDbContext> options)
        {
            this.dbContext =new EiMTDbContext(options);
        }
        public Task Execute(IJobExecutionContext context)
        {
            DbFunctions dfunc = null;
            var users = dbContext.Users
                            .Include(x=>x.Token)
                            .Where(x => SqlServerDbFunctionsExtensions.DateDiffDay(dfunc,x.Token.CreateAt, DateTime.Now) > 1)
                            .ToList();
            if (users.Any())
            {
                var transation = dbContext.Database.BeginTransaction();
                try
                {

                    foreach (var token in users)
                    {
                        dbContext.Users.Remove(token);
                    }
                    dbContext.SaveChanges();
                    transation.Commit();
                }
                catch
                {
                    transation.Rollback();
                }
                finally
                {
                    transation.Dispose();
                    dbContext.Dispose();
                }
            }
            return Task.FromResult(0);
        }
    }
}
