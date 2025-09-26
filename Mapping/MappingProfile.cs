using AutoMapper;
using Unibouw.DTOs;
using Unibouw.Models;

namespace Unibouw.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkItems, WorkItemDTO>().ReverseMap();
            CreateMap<SubContractor, SubContractorDTO>().ReverseMap();
        }
    }
}
