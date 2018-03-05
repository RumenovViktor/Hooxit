namespace Hooxit.Presentation.Read
{
    public class CommandExecutionResult<TReturnValue>
    {
        public CommandExecutionResult(bool isValid, string message)
        {
            this.IsValid = isValid;
            this.Message = message;
        }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public TReturnValue ReturnValue { get; set; }
    }
}
