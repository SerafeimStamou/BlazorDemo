using AutoMapper;
using CRUD.DTOs;
using CRUD.Models;
using DataAccess;
using System;
using System.Threading.Tasks;

namespace CRUD
{
    public class PersonCrud : IPersonCrud
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly IMapper _mapper;

        DatabaseResult result;

        public PersonCrud(IDatabaseAccess databaseAccess, IMapper mapper)
        {
            _databaseAccess = databaseAccess;
            _mapper = mapper;
            result = new();
        }

        public async Task<DatabaseResult> Create(PersonDto person)
        {
            string query = @"INSERT INTO People (FirstName,LastName,Email,DateOfBirth,Gender)
                             VALUES (@FirstName,@LastName,@Email,@DateOfBirth,@Gender)";

            try
            {
                result.Result = await _databaseAccess.ManipulateData(query, person);
                return result;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
                result.HasFatalErrors = true;
                return result;
            }
        }

        public async Task<DatabaseResult> Read()
        {
            string query = "SELECT * FROM People";

            try
            {
                result.Result = await _databaseAccess.ReadData<Person>(query, new());
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.HasFatalErrors = true;
                return result;
            }
        }

        public async Task<DatabaseResult> Update(PersonDto person)
        {
            try
            {
                var personFromDb = await _databaseAccess.FindById<Person>($"SELECT * FROM People WHERE Id={person.Id}");

               var newPerson =  _mapper.Map(person, personFromDb);

                string query = @$"UPDATE People SET FirstName=@FirstName, LastName=@LastName, Email=@Email,
                             DateOfBirth=@DateOfBirth, Gender=@Gender WHERE Id={newPerson.Id}";

                await _databaseAccess.ManipulateData(query, newPerson);

                result.Result = newPerson;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.HasFatalErrors = true;
                return result;
            }
        }

        public async Task<DatabaseResult> Delete(int id)
        {
            try
            {
                string query = $"DELETE FROM People WHERE Id={id}";

                await _databaseAccess.ManipulateData<Person>(query, new());

                result.Message = "Person deleted successfully";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.HasFatalErrors = true;
                return result;
            }
        }
    }
}
