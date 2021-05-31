using CRUD;
using CRUD.DTOs;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
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

        [HttpPost]
        public async Task<DatabaseResult> Post(PersonDto person)
        {
            var result = await _personCrud.Create(person);
            return result;
        }

        [HttpPut]
        public async Task<DatabaseResult> Put(PersonDto person)
        {
            var result = await _personCrud.Update(person);
            return result;
        }

        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<DatabaseResult> Delete(int id)
        {
            var result = await _personCrud.Delete(id);
            return result;
        }
    }
}
