﻿using DeliveryBackend.Model;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryBackend.Service
{
    public class AddressManager
    {
        public  Address Get(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://127.0.0.1:8080/server/api/addresses/{id}");
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                var list = JsonConvert.DeserializeObject<Address>(result);
                return list;
            }
        }

        public bool Create(Address nw)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8080/server/api/addresses/");
            request.Method = "POST";
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(nw);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
            }
            return false;

        }

        public bool Remove(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://127.0.0.1:8080/server/api/addresses/{id}");
            request.Method = "DELETE";
            request.ContentType = "application/json";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if(response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Update(int id, Address nw)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://127.0.0.1:8080/server/api/addresses/{id}");
            request.Method = "PUT";
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(nw);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
