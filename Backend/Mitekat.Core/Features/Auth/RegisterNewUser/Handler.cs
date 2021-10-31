namespace Mitekat.Core.Features.Auth.RegisterNewUser
{
    using System.Threading.Tasks;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class RegisterNewUserHandler : RequestHandlerBase<RegisterNewUserRequest, BlankResult>
    {
        private readonly IPasswordHashingHelper _passwordHashingHelper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewUserHandler(IUnitOfWork unitOfWork, IPasswordHashingHelper passwordHashingHelper)
        {
            _unitOfWork = unitOfWork;
            _passwordHashingHelper = passwordHashingHelper;
        }

        protected override async Task<Response<BlankResult>> HandleAsync(RegisterNewUserRequest request)
        {
            var hashedPassword = _passwordHashingHelper.HashPassword(request.Password);
            var user = new UserEntity(request.Username, hashedPassword);

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return Success();
        }
    }
}
