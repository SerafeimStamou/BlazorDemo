using Blazored.Toast.Services;
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
        public List<Person> People { get; set; } = new();
        public DatabaseResult Result { get; set; } = new();
        public string Message { get; set; }
        public bool HasFatalErrors { get; set; }

        [Inject]
        protected  IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        protected IToastService _toastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
                HttpRequestMessage request = new(HttpMethod.Get, "https://localhost:44334/api/Person");

                HttpClient client = _clientFactory.CreateClient();

                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DatabaseResult>();

                    if (result.Result is not null)
                    {
                        var resultList = result.Result.ToString();
                        People = JsonConvert.DeserializeObject<List<Person>>(resultList);
                    }
                    else
                    {
                       Message = result.Message;
                       HasFatalErrors = result.HasFatalErrors;
                    }
                }
                else
                {
                    Message = $"Cannot connect to API: {response.ReasonPhrase}";
                }
        }

        protected void ErrorMessage()
        {
            _toastService.ShowError(Message);
        }
    }
}
