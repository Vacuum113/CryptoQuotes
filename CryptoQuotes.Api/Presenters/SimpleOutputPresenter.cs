using System.Threading.Tasks;
using Api.Helpers;
using Api.Presenters.Output;
using Application.Abstractions.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presenters
{
    public class SimpleOutputPresenter: ISimpleOutputPort
    {
        public ActionResult Result { get; private set; }

        public Task Output(object output)
        {
            Result = output != null ? new JsonActionResult(ActionOutput.SuccessData(output)) : new JsonActionResult(ActionOutput.Success);

            return Task.CompletedTask;
        }

        public Task Error(string error)
        {
            Result = JsonActionResult.BadRequest(ActionOutput.Failure(ErrorCode.GenericError, error));
            return Task.CompletedTask;
        }

        public Task AccessDenied(string output)
        {
            Result = JsonActionResult.Forbidden(ActionOutput.Failure(ErrorCode.Forbidden, output));

            return Task.CompletedTask;
        }
        
        public Task NotUnauthorized()
        {
            Result = JsonActionResult.Unauthorized(ActionOutput.Failure(ErrorCode.Unauthorized, "User not authorized"));

            return Task.CompletedTask;
        }
    }
}