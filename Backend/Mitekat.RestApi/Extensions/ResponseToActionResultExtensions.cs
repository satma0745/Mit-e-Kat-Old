namespace Mitekat.RestApi.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
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

        public static Task<IActionResult> ToActionResult<TResult>(
            this Task<Response<TResult>> responseTask,
            Func<TResult, IActionResult> onSuccess) =>
            responseTask.ToActionResult(onSuccess, _ => new StatusCodeResult(StatusCodes.Status500InternalServerError));

        public static Task<IActionResult> ToActionResult<TResult>(this Task<Response<TResult>> responseTask) =>
            responseTask.ToActionResult(
                _ => new StatusCodeResult(StatusCodes.Status200OK),
                _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
            );
    }
}
