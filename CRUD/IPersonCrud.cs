using CRUD.DTOs;
using CRUD.Models;
using System.Threading.Tasks;

namespace CRUD
{
    public interface IPersonCrud
    {
        Task<DatabaseResult> Create(PersonDto person);
        Task Delete(int id);
        Task<DatabaseResult> Read();
        Task<DatabaseResult> Update(PersonDto person);
    }
}