using Bernhoeft.Entitys;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bernhoeft.Utils
{

    public class JsonProperty
    { 
        public string Property { get; set; }
        public JsonValueKind JsonValueKind { get; set; }

        public string DecodedProperty { get; set; }

        public JsonProperty(string property, JsonValueKind jsonValueKind)
        {
            Property = property;
            this.JsonValueKind = jsonValueKind;
        }
    }


    public static class Helper
    {
        public static string ConnectionString { get; set; } = "Server=localhost;Port=5433;User ID=postgres;Password=;Database=Bernhoeft;Integrated Security=true;Pooling=true;";




        public static string GetUrlParameter(HttpContext context, string parameter)
        {
            return context.Request.Query[parameter].Count > 0 ? context.Request.Query[parameter][0] : "";
        }

        public static User ParserUserFromJson(Stream body)
        {
            User user = new User();
            List<JsonProperty> properties = new List<JsonProperty>();
            properties.Add(new JsonProperty("name", JsonValueKind.String));
            properties.Add(new JsonProperty("email", JsonValueKind.String));
            properties.Add(new JsonProperty("username", JsonValueKind.String));
            properties.Add(new JsonProperty("password", JsonValueKind.String));

            properties = ParserBody(body, properties).Result;

            user.Name = properties[0].DecodedProperty;
            user.Email = properties[1].DecodedProperty;
            user.Username = properties[2].DecodedProperty;
            user.Password = properties[3].DecodedProperty;

            return user;
        }
        private async static Task<List<JsonProperty>> ParserBody(Stream body, List<JsonProperty> properties)
        {
            User user = new User();
            using TextReader reader = new StreamReader(body);
            string json = await reader.ReadToEndAsync();
            JsonElement bodyAsJson = JsonSerializer.Deserialize<JsonElement>(json.ToLower());

            foreach (var propertyJSON in properties)
            {
                if (bodyAsJson.TryGetProperty(propertyJSON.Property, out JsonElement property) && property.ValueKind == propertyJSON.JsonValueKind)
                {
                    propertyJSON.DecodedProperty = property.GetString();
                    
                }
            }
            return properties;
        }

    }
}
