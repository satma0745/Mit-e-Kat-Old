namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System;
    using System.Text.Json.Serialization;
    using FluentValidation;
    using Mitekat.Core.Features.Auth.UpdateUser;
    using Mitekat.Core.Features.Shared.Requests;

    public class UpdateUserRequestDto
    {
        [JsonPropertyName("username")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Password { get; set; }
        
        public UpdateUserRequest ToRequest(Guid userId, IRequester requester) =>
            new(userId, Username, Password, requester);
    }

    internal class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserRequestDtoValidator()
        {
            RuleFor(dto => dto.Username)
                .MinimumLength(6)
                .MaximumLength(20)
                .NotEmpty();
            
            RuleFor(dto => dto.Password)
                .MinimumLength(6)
                .MaximumLength(20)
                .NotEmpty();
        }
    }
}
