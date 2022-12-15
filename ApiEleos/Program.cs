﻿using ApiEleos.Models;
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
            //if (response.IsSuccessful)
            //{
            //    string userJson = response.Content;
            //    Documents docs = JsonConvert.DeserializeObject<Documents>(userJson.ToString());
            //    //Aqui obtengo los valores
            //    int identidicador = docs.document_identifier;
            //    string obtDocs = docs.download_url;

            //}
            //else
            //{
            //    ejecutarApi();
            //}


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
            string c1 = "jy193UAhUHJsAKHV4rD904PBAWCC0wAA&url=";
            string c2 = "&usg=AFQjCNH-c6dVemIxU_GaSYgoGPNXWVztIA";
            string urls = "https://axle-production.s3-external-1.amazonaws.com/tiffs/15742722-f59e-4695-950d-88e04b98c314.tif?AWSAccessKeyId=AKIAYKFM34B2KNHRC742&Signature=G%2FqtJqROMQIocTxP8DF3Nq1LGtA%3D&Expires=1671128308&response-content-disposition=attachment%3B%20filename%3D%22ORD_ABI_1232824_UNK_101233549.tif%22";
            //string url = "https://file-examples.com/wp-content/uploads/2017/10/file_example_TIFF_1MB.tiff";
            string cadena = c1 + urls + c2;
            if (cadena.Contains("&url=") && cadena.Contains("&usg="))
            {
                var subCadena = cadena.Split(new string[] { "&url=", "&usg=" }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.StartsWith("http")).FirstOrDefault();
                string url = Uri.UnescapeDataString(subCadena);
                string furl = url.Split(';').Last();
                string qc = furl.Split('=').Last();
                string filenamef = qc.Replace("\"", "");
                using (WebClient webClient = new WebClient())
                {

                    var fImage = @"C:\Administración\ApiEleos\Images\" + filenamef;

                    webClient.DownloadFile(new Uri(urls), fImage);
                    string titulo = fImage;
                    string segmento = "1223";
                    string mensaje = "Prueba de envio de imagenes";
                    facLabControler.enviarNotificacion(segmento, titulo.ToString(), mensaje);



                }

            }


            //string url = "https://cdn-icons-png.flaticon.com/512/72/72648.png";
           

           
        }
        static void Main(string[] args)
        {
            try
            {
                //barrap();
                download();
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
