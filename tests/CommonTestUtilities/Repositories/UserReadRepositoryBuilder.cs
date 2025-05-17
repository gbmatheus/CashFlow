using CashFlow.Domain.Repositories.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class UserReadRepositoryBuilder
    {
        new Mock<IUserReadOnlyRepository> _repository;

        public UserReadRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
