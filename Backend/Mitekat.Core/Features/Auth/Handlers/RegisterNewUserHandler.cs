namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class RegisterNewUserHandler : IRequestHandler<RegisterNewUserRequest, Response<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHashingHelper _passwordHashingHelper;

        public RegisterNewUserHandler(IUnitOfWork unitOfWork, IPasswordHashingHelper passwordHashingHelper)
        {
            _unitOfWork = unitOfWork;
            _passwordHashingHelper = passwordHashingHelper;
        }
        
        public async Task<Response<Unit>> Handle(RegisterNewUserRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHashingHelper.HashPassword(request.Password);
            var user = new UserEntity(request.Username, hashedPassword);
            
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
            
            return Response<Unit>.Success(Unit.Value);
        }
    }
}
