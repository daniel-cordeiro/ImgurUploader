using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImgurUploader
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Notifier notifier = new Notifier();

            List<string> supportedFormats = new List<string>
                {".jpg", ".jpeg", ".png", ".gif", ".mp4", ".mpeg", ".avi", ".webm"};
            
            if (args.Length != 1)
            {
                notifier.ShowNotification("Apenas é suportado um ficheiro.");
                Environment.Exit(0);
            }
            
            
            string imgPath = args[0];
            
            if (!File.Exists(imgPath))
            {
                notifier.ShowNotification("O ficheiro não existe.");
                Environment.Exit(0);
            }

            if (!supportedFormats.Contains(Path.GetExtension(imgPath).ToLower()))
            {
                notifier.ShowNotification("O ficheiro especificado não é suportado.");
                Environment.Exit(0);
            }
            
            string fileName = Path.GetFileName(imgPath);
            
            byte[] imgData = File.ReadAllBytes(imgPath); 
            
            var client = new RestClient("https://api.imgur.com/3/image");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID {{your-client-id}}");
            request.AlwaysMultipartFormData = true;
            request.AddFileBytes("image", imgData, fileName, "multipart/form-data");
            IRestResponse response = client.Execute(request);

            JObject obj = JObject.Parse(response.Content);

            bool success = bool.Parse(obj["success"].ToString());

            if (success)
            {
                string link = obj["data"]["link"].ToString();
                notifier.ShowNotification($"Foi efectuado o upload do ficheiro {fileName} com sucesso.\n{link}");
            }
            else
            {
                notifier.ShowNotification("Ocorreu um erro ao carregar o ficheiro.");
            }

        }
    }
}