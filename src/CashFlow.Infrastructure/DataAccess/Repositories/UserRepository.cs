using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly CashFlowDBContext _dBContext;

        public UserRepository(CashFlowDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<bool> ExistsActiveUserWithEmail(string email)
        {
            var user = await _dBContext.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

            if(user is null)
                return false;

            return true;
        }

        public async Task Add(User user)
        {
            await _dBContext.AddAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dBContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
