using CashFlow.Domain.Entities;
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

        public UserReadRepositoryBuilder GetUserByEmail(User user)
        {
            _repository.Setup(config => config.GetUserByEmail(user.Email)).ReturnsAsync(user);
            return this;
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
