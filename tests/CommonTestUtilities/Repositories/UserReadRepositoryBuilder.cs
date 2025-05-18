using CashFlow.Domain.Repositories.Users;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserReadRepositoryBuilder
    {
        Mock<IUserReadOnlyRepository> _repository;

        public UserReadRepositoryBuilder()
        {
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public void ExistsActiveUserWithEmail(string email)
        {
            _repository.Setup(config => config.ExistsActiveUserWithEmail(email)).ReturnsAsync(true);
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
