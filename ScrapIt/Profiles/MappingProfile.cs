using AutoMapper;
using ScrapIt.DAL.Contracts.Entities;
using ScrapIt.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapIt.Web.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskCreateDto, TaskEntity>();
            CreateMap<TaskEntity, TaskCreateDto >();
            CreateMap<TaskDto, TaskEntity>();
            CreateMap<TaskEntity, TaskDto>();

            CreateMap<CarCreateDto, CarEntity>();
            CreateMap<CarEntity, CarCreateDto >();
        }
    }
}
