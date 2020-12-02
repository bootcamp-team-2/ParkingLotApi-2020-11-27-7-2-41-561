using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingLotApiTest
{
    public class TestTool
    {
        public static StringContent SerializeRequestBody(object obj)
        {
            var httpContent = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, "application/json");
            return content;
        }

        public static async Task<T> DeserializeResponseBodyAsync<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}
