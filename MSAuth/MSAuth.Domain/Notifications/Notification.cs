namespace MSAuth.Domain.Notifications
{
    public class Notification
    {
        public string Error { get; }
        public string Message { get; }

        public Notification(string error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}
