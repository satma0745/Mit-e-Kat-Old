namespace Mitekat.RestApi.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mitekat.Core.Features.Shared.Responses;

    internal static class ResponseToActionResultExtensions
    {
        public static async Task<IActionResult> ToActionResult<TResult>(
            this Task<Response<TResult>> responseTask,
            Func<TResult, IActionResult> onSuccess,
            Func<Error, IActionResult> onFailure) =>
            await responseTask switch
            {
                (true, var result, _) => onSuccess(result),
                (false, _, var error) => onFailure(error)
            };
    }
}
