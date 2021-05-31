using AutoMapper;
using CRUD.DTOs;
using CRUD.Models;

namespace CRUD.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDto, Person>();
        }
    }
}
