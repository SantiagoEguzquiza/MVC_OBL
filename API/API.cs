using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class API_COT
    {
        public string GetCotizacion()
        {

            UriBuilder builder = new UriBuilder("http://apilayer.net/api/live?access_key=5543f38918295a979d09e585efff2f4d&currencies=UYU&source=USD&format=1");

            HttpClient client = new HttpClient();
            var result = client.GetAsync(builder.Uri).Result;

            using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
            {
                return sr.ReadToEnd();
            }
        }

    }



}
