using ApiEleos.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiEleos
{
    public class Program
    {
        public static FacLabControler facLabControler = new FacLabControler();
        static void barrap()
        {

            //var client = new RestClient("https://platform.driveaxleapp.com/api/v1/documents/queued/next");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Key eleos_wnSrbpIEqnP5ACV79ELChxXfqiGhyENAofmrWXG0EOLW9nSQsPujrw00");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept", "application/json");
            //var body = @"";
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);


            HttpClient httpClient = new HttpClient();
            var apiKey = "eleos_wnSrbpIEqnP5ACV79ELChxXfqiGhyENAofmrWXG0EOLW9nSQsPujrw00";
            var reasonPhrase = "";
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Key=" + apiKey);
            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("ContentType", "Application/json");
            //ESTE ES EL LINK DE LAS IMAGENES
            Uri uri = new Uri("https://platform.driveaxleapp.com/api/v1/documents/queued/next");
            //Uri uri = new Uri("https://platform.driveaxleapp.com/api/v1/screens");
            HttpResponseMessage response = null;
            response = httpClient.GetAsync(uri).Result;
            reasonPhrase = response.ReasonPhrase;

            if (response.IsSuccessStatusCode)
            {
                string userJson = response.Content.ReadAsStringAsync().Result;
                Documents docs = JsonConvert.DeserializeObject<Documents>(userJson.ToString());
                //Aqui obtengo los valores
                int identidicador = docs.document_identifier;
                string obtDocs = docs.download_url;
                
            }
            else
            {
                ejecutarApi();
            }
            

            


        }
        static void ejecutarApi()
        {
            try
            {
                barrap();
            }
            catch (ExternalException e)
            {

                Console.WriteLine(e.Message);

            }
        }
        static void download()
        {
            string urls = "https://filesamples.com/samples/image/tiff/sample_640%C3%97426.tiff";
            string url = "https://file-examples.com/wp-content/uploads/2017/10/file_example_TIFF_1MB.tiff";
            string fname = urls.Split('/').Last();
           

            //string url = "https://cdn-icons-png.flaticon.com/512/72/72648.png";
            using (WebClient webClient = new WebClient())
            {
               
                var fImage = @"C:\Administración\ApiEleos\Images\" + fname;
                
                webClient.DownloadFile(new Uri(urls), fImage);
                string titulo = fImage;
                string segmento = "1223";
                string mensaje = "Prueba de envio de imagenes";
                facLabControler.enviarNotificacion(segmento, titulo.ToString(), mensaje);

                

            }

           
        }
        static void Main(string[] args)
        {
            try
            {
                barrap();
                //download();
            }
            catch (ExternalException e)
            {

                Console.WriteLine(e.Message);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
