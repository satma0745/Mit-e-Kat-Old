namespace Mitekat.Core.Features.Auth.UpdateUser
{
    using System.Threading.Tasks;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class UpdateUserHandler : RequestHandlerBase<UpdateUserRequest, BlankResult>
    {
        private readonly IPasswordHashingHelper _passwordHashingHelper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IPasswordHashingHelper passwordHashingHelper)
        {
            _unitOfWork = unitOfWork;
            _passwordHashingHelper = passwordHashingHelper;
        }

        protected override async Task<Response<BlankResult>> HandleAsync(UpdateUserRequest request)
        {
            if (request.Requester.Role == UserRole.User && request.Id != request.Requester.Id)
            {
                return Failure(Error.AccessViolation);
            }

            var user = await _unitOfWork.Users.FindAsync(request.Id);
            if (user is null)
            {
                return Failure(Error.NotFound);
            }

            if (await _unitOfWork.Users.UsernameTakenAsync(request.Username, request.Id))
            {
                return Failure(Error.Conflict);
            }

            var newPassword = _passwordHashingHelper.HashPassword(request.Password);
            user.Patch(request.Username, newPassword);

            await _unitOfWork.SaveChangesAsync();

            return Success();
        }
    }
}
