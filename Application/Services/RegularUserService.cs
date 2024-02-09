using Application.Services.Interfaces;
using Domain.Domain;
using Domain.Repository;

namespace Application.Services
{
    public class RegularUserService : IRegularUserService
    {
        private readonly IRegularUserRepo _regularUserRepo;

        public RegularUserService(IRegularUserRepo regularUserRepo)
        {
            _regularUserRepo = regularUserRepo;
        }

        public async Task CheckFields(string userName, CancellationToken cancellationToken = default)
            => await RegularUser.CheckFields(userName, cancellationToken);

        public async Task<RegularUser> CreateRegularUser(string regularUserId, string username, CancellationToken cancellationToken = default)
        {
            RegularUser regularUser = new()
            {
                Id = regularUserId,
                Username = username
            };
            return await _regularUserRepo.Add(regularUser, cancellationToken);
        }

        public Task<RegularUser> FindRegularUserById(string regularUserId, CancellationToken cancellationToken = default)
            => _regularUserRepo.GetById(regularUserId, cancellationToken);

        public Task<RegularUser> FindRegularUserByUsername(string username, CancellationToken cancellationToken = default)
            => _regularUserRepo.GetByUsername(username, cancellationToken);
    }
}
