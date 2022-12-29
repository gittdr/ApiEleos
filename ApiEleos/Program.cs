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
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiEleos
{
    public class Program
    {
        public static FacLabControler facLabControler = new FacLabControler();
        System.Net.Mail.MailMessage Email;
        

        

        public string error = "";







        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="FROM">Procedencia</param>
        /// <param name="Para">Mail al cual se enviara</param>
        /// <param name="Mensaje">Mensaje del mail</param>
        /// <param name="Asunto">Asunto del mail</param>
        /// <param name="ArchivoPedido_">Archivo a adjuntar, no es obligatorio</param>
        static void Main(string[] args)
        {
            try
            {
            //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://10.176.167.171");
            //    request.Method = WebRequestMethods.Ftp.UploadFile;

            //    // This example assumes the FTP site uses anonymous logon.
            //    request.Credentials = new NetworkCredential("pages", "single");

            //    // Copy the contents of the file to the request stream.
            //    using (FileStream fileStream = File.Open(@"C:\Administración\ApiEleos\Images\ORD_SAY_1235299_UNK_101680205.tif", FileMode.Open, FileAccess.Read))
            //    {
            //        using (Stream requestStream = request.GetRequestStream())
            //        {
            //            await fileStream.CopyToAsync(requestStream);
            //            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            //            {
            //                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            //            }
            //        }
            //    }
                //enviarImages();
                barrap();
                //download(1, "https://axle-production.s3-external-1.amazonaws.com/tiffs/6733268c-d1f3-4cd7-bbc0-63dec108677f.tif?AWSAccessKeyId=AKIAYKFM34B2KOCLX77Y&Signature=YETypPZXTiHRiwcpDXpL%2FJtb8kM%3D&Expires=1671647905&response-content-disposition=attachment%3B%20filename%3D%22ORD_PE%C3%91_1233023_UNK_101549738.tif%22", 1212);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }
            
        }
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

            //var client = new RestClient("https://platform.driveaxleapp.com/api/v1/documents/queued/next");
            //client.Timeout = 12433453;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("Authorization", "key eleos_wnSrbpIEqnP5ACV79ELChxXfqiGhyENAofmrWXG0EOLW9nSQsPujrw00");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Accept", "application/json");
            //var body = @"";
            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.ResponseUri.AbsoluteUri);
            //if (response.IsSuccessful)
            //    string statusR = response.StatusCode.ToString();
            //if (statusR == "422")
            //{
            //    var userJson = response.Content;
            //    Documents docs = JsonConvert.DeserializeObject<Documents>(userJson.ToString());
            //    //Aqui obtengo los valores
            //    string obtDocs = response.ResponseUri.AbsoluteUri;
            //    string identificador = obtDocs.Split('/').Last();
            //    int ident = Int32.Parse(identificador);


            //    DetailsDoc(obtDocs);
            //}
            //else
            //{
            //    ejecutarApi();
            //}


            HttpClient httpClient = new HttpClient();
            var apiKey = "eleos_wnSrbpIEqnP5ACV79ELChxXfqiGhyENAofmrWXG0EOLW9nSQsPujrw00";
            var reasonPhrase = "";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "=" + apiKey);
            //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=" + apiKey);
            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.30.0");
            httpClient.DefaultRequestHeaders.Add("Accep-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("ContentType", "Application/json");

            //ESTE ES EL LINK DE LAS IMAGENES
            Uri uri = new Uri("https://platform.driveaxleapp.com/api/v1/documents/queued/next");
            //Uri uri = new Uri("https://platform.driveaxleapp.com/api/v1/documents/queued/162047494");
            //Uri uri = new Uri("https://platform.driveaxleapp.com/api/v1/screens");
            HttpResponseMessage response = httpClient.GetAsync(uri).Result;
            //response = httpClient.GetAsync(uri).Result;
            reasonPhrase = response.ReasonPhrase;
            string statusR = response.StatusCode.ToString();
            if (statusR == "422")
            //if (response.IsSuccessStatusCode)
            {
                //string userJson = response.Content.ReadAsStringAsync().Result;
                //Documents docs = JsonConvert.DeserializeObject<Documents>(userJson.ToString());
                //Aqui obtengo los valores
                //int identificador = docs.document_identifier;
                //string obtDocs = docs.download_url;
                string obtDocs = response.RequestMessage.RequestUri.AbsoluteUri;
                DetailsDoc(obtDocs);
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
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }
        }
        static void DetailsDoc(string url)
        {
            var client = new RestClient(url);
            client.Timeout = 12433453;
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Key eleos_wnSrbpIEqnP5ACV79ELChxXfqiGhyENAofmrWXG0EOLW9nSQsPujrw00");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.UseDefaultCredentials = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.AddHeader("User-Agent", "Other");
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.ResponseUri.AbsoluteUri);
            //if (response.IsSuccessful)
            string statusR = response.StatusCode.ToString();
            if (response.IsSuccessful)
            {
                var userJson = response.Content;
                Documents docs = JsonConvert.DeserializeObject<Documents>(userJson.ToString());
                //Aqui obtengo los valores
                int identificador = docs.document_identifier;
                string downloadI = docs.download_url;
                int load_number = docs.load_number;
                
                download(identificador, downloadI, load_number);
            }
            else
            {
                ejecutarApi();
            }
        }
        static void download(int identificador,string obtDocs, int load_number)
        {
            try
            {
                string c1 = "jy193UAhUHJsAKHV4rD904PBAWCC0wAA&url=";
                string c2 = "&usg=AFQjCNH-c6dVemIxU_GaSYgoGPNXWVztIA";
                //string urls = "https://axle-production.s3-external-1.amazonaws.com/tiffs/1bc98c4e-ef3d-4514-abf0-30a14bbf27a8.tif?AWSAccessKeyId=AKIAYKFM34B2KNHRC742&Signature=DBsw8XHkUb36RW1E8XcrfSw9kdg%3D&Expires=1671138538&response-content-disposition=attachment%3B%20filename%3D%22ORD_BAJ_1233069_UNK_101246898.tif%22";
                //string url = "https://file-examples.com/wp-content/uploads/2017/10/file_example_TIFF_1MB.tiff";
                string cadena = c1 + obtDocs + c2;
                if (cadena.Contains("&url=") && cadena.Contains("&usg="))
                {
                    var subCadena = cadena.Split(new string[] { "&url=", "&usg=" }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.StartsWith("http")).FirstOrDefault();
                    string url = Uri.UnescapeDataString(subCadena);
                    string furl = url.Split(';').Last();
                    string qc = furl.Split('=').Last();
                    string filenamef = qc.Replace("\"", "");
                    //string filenamesin = filenamef.Replace("Ñ","N");
                    using (WebClient webClient = new WebClient())
                    {

                        //var fImage = @"C:\Administración\ApiEleos\Images\" + filenamef;
                        var fImage = @"\\10.223.208.41\Users\Administrator\Documents\ImagesEleos\" + filenamef;
                        //C: \Users\Administrator\Documents\ImagesEleos
                        try
                        {
                            webClient.DownloadFile(new Uri(obtDocs), fImage);
                            string titulo = fImage;
                            string segmento = load_number.ToString() + " - " + filenamef;
                            string mensaje = "Prueba de envio de imagenes";
                            //facLabControler.registrarEvidencias(segmento, obtDocs, filenamef);
                            
                                //facLabControler.enviarNotificacion(segmento, titulo.ToString(), mensaje);
                                marcarImagen(identificador);
                               
                            
                            //deleteArchivo(filenamef);
                            //ejecutarApi();
                        }
                        catch (Exception e)
                        {
                            string segmento = load_number.ToString();
                            string errores = filenamef + "-" + "No se puedo descargar la imagen";
                            //facLabControler.registrarEvidencias(segmento, obtDocs, errores);
                            ejecutarApi();

                        }
                        

                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                ejecutarApi();

            }
        }
        
        static void marcarImagen(int identificador)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var apiKey = "eleos_wnSrbpIEqnP5ACV79ELChxXfqiGhyENAofmrWXG0EOLW9nSQsPujrw00";
                var reasonPhrase = "";
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Key=" + apiKey);
                httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("ContentType", "Application/json");
                //ESTE ES EL LINK DE LAS IMAGENES
                Uri uri = new Uri("https://platform.driveaxleapp.com/api/v1/documents/queued/" + identificador);
                HttpResponseMessage response = null;
                response = httpClient.DeleteAsync(uri).Result;
                reasonPhrase = response.ReasonPhrase;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Marcado exitoso: " + identificador);
                    //ejecutarApi();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                //ejecutarApi();

            }
        }
        static void deleteArchivo(string filenamef)
        {
            try
            {
                // Se obtienen todos los archivos y directorios dentro del directorio indicado.
                //string startFolder = @"C:\Administración\ApiEleos\Images\";
                string startFolder = @"\\10.223.208.41\Users\Administrator\Documents\ImagesEleos\";
                // Take a snapshot of the file system.  
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

                // This method assumes that the application has discovery permissions  
                // for all folders under the specified path.  
                IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                //Create the query  
                IEnumerable<System.IO.FileInfo> fileQuery =
                    from file in fileList
                    where file.Name == filenamef
                    orderby file.Name
                    select file;

                //Execute the query. This might write out a lot of files!  
                foreach (System.IO.FileInfo fi in fileQuery)
                {
                    Console.WriteLine(fi.Name);
                    fi.Delete();
                }

                //di24.Delete();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                ejecutarApi();

            }

        }
      
}
    }

