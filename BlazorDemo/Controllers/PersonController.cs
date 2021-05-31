using CRUD;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonCrud _personCrud;

        public PersonController(IPersonCrud personCrud)
        {
            _personCrud = personCrud;
        }

        [HttpGet]
        public async Task<DatabaseResult> Get()
        {
            var result = await _personCrud.Read();
            return result;
        }
    }
}
