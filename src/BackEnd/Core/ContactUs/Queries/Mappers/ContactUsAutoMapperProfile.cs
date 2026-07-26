using AutoMapper;
using BackEnd.Core.ContactUs.Queries.DTOs;
using BackEnd.Core.User.Queries.DTOs;
using BackEnd.Core.User.Queries.GetId;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.ContactUs.Queries.Mappers
{
    public class ContactUsAutoMapperProfile : Profile
    {
        public ContactUsAutoMapperProfile()
        {
            CreateMap<BackEnd.Data.Entities.ContactUs.ContactUs, ContactUsDto>();
        }
    }
}
