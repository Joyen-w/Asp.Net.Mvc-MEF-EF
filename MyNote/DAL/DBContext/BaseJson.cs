using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.DBContext
{
    public class BaseJson
    {
        [JsonIgnore, ScaffoldColumn(false)]
        public string Serialized
        {
            get
            {
                return JsonConvert.SerializeObject(this);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                    jsonSerializerSettings.ObjectCreationHandling= ObjectCreationHandling.Reuse;
                    JsonSerializerSettings jsonSerializerSettings2 = jsonSerializerSettings;
                    JsonConvert.PopulateObject(value, this, jsonSerializerSettings2);
                }
            }
        }
    }
}
