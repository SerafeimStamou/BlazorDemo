using CRUD.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorUI.Pages
{
    public class PeopleBase : ComponentBase
    {
        public List<Person> people { get; set; }
        public DatabaseResult result { get; set; }

        [Inject]
        protected  IHttpClientFactory _clientFactory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:44334/api/Person");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<DatabaseResult>();
 
                if (result.Result is not null)
                {
                    people = JsonConvert.DeserializeObject<List<Person>>(result.Result.ToString());
                }
            }
            else
            {
                result.Message = $"Cannot connect to API: {response.ReasonPhrase}";
            }
        }
    }
}
