namespace Mitekat.Core.Features.Auth.RegisterNewUser
{
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Features.Shared.Responses;

    public class RegisterNewUserRequest : RequestBase<BlankResult>
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Username { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Password { get; private set; }
    }
}
