namespace Api.Presenters.Output
{
    public class ActionOutput
    {
        public bool Succeeded { get; }

        public ErrorCode? ErrorCode { get; }
        public object Data { get; }
        public string ErrorMessage { get; }

        public static ActionOutput Success => new ActionOutput(true);
        public static ActionOutput SuccessData(object data) => new ActionOutput(true, null, null, data);

        public static ActionOutput Failure(ErrorCode errorCode, string errorMessage) 
            => new ActionOutput(false, errorCode, errorMessage);

        private ActionOutput(bool succeeded, ErrorCode? errorCode = null, string errorMessage = null, object data = null)
        {
            Succeeded = succeeded;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}
