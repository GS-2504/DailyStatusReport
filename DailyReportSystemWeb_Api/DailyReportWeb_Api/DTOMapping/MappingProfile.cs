using AutoMapper;
using DailyReportWeb_Api.Model;
using DailyReportWeb_Api.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserTask,UserTaskDto>().ReverseMap();
        }
    }
}
