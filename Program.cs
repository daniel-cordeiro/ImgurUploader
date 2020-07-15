using System;

namespace ImgurUploader
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Notifier notifier = new Notifier();
            notifier.ShowNotification("hello world");
        }
    }
}