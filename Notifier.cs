using System.Runtime.InteropServices;

namespace ImgurUploader
{
    public class Notifier
    {
        private NotificationStrategy _notifier;

        public Notifier()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                _notifier = new NotificationLinux();
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                _notifier = new NotificationWindows();
        }
        public Notifier(NotificationStrategy notifier)
        {
            _notifier = notifier;
        }

        public void ShowNotification(string text)
        {
            _notifier.ShowNotification(text);
        }
    }
}