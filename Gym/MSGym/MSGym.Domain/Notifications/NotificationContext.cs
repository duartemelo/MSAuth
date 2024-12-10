namespace MSGym.Domain.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;
        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public bool HasNotifications => _notifications.Count != 0;
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string error, string message)
        {
            _notifications.Add(new Notification(error, message));
        }
    }
}
