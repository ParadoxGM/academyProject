using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

namespace ServiceIndustryWebSolo.Manager
{

    public class JsonManager<T>
    {
        private string _pathToFile;
        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(List<T>));

        public List<T> ReadFile(string path, string filename)
        {
            _pathToFile = $@"{path}{filename}";

            using (FileStream streamReader = new FileStream(_pathToFile, FileMode.Open))
            {
                return (List<T>)jsonSerializer.ReadObject(streamReader);
            }
        }

        public void SaveToFile(List<T> list, string path, string filename)
        {
            _pathToFile = $@"{path}{filename}.json";
            //using (FileStream fs = new FileStream(_pathToFile, FileMode.OpenOrCreate))
            //{
            //    jsonSerializer.WriteObject(fs, list);
            ////}

            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            using (StreamWriter streamReader = new StreamWriter(_pathToFile))
            {
                streamReader.Write(JsonConvert.SerializeObject(list, microsoftDateFormatSettings));
            }


            //JsonSerializer serializer = new JsonSerializer();
            ////serializer.Converters.Add(new JavaScriptDateTimeConverter());
            //serializer.NullValueHandling = NullValueHandling.Ignore;
            //using (StreamWriter sw = new StreamWriter(_pathToFile))
            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    serializer.Serialize(writer, list);
            //    // {"ExpiryDate":new Date(1230375600000),"Price":0}
            //}
        }
    }
}