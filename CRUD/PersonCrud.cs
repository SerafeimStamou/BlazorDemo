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
        DatabaseResult result;

        public PersonCrud(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
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

                Person newPerson = new()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    DateOfBirth = person.DateOfBirth,
                    Gender = person.Gender
                };

                string query = @$"UPDATE People SET FirstName=@FirstName, LastName=@LastName, Email=@Email,
                             DateOfBirth=@DateOfBirth, Gender=@Gender WHERE Id={person.Id}";

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
                var person = await _databaseAccess.FindById<Person>($"SELECT * FROM People WHERE Id={id}");

                string query = $"DELETE FROM People WHERE Id={person.Id}";

                await _databaseAccess.ManipulateData(query, person);

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
