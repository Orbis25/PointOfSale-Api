using AutoMapper;
using Model.Models;
using Model.ModelsMappings;
using Model.ViewModels.Sale;
using Model.ViewModels.User;

namespace PointOfSale.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.User.Name))
                .ForMember(x => x.LastName , y =>  y.MapFrom(src => src.User.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(x => x.User.Email))
                .ReverseMap();
            CreateMap<Sale, SaleVM>().ReverseMap();
            CreateMap<Employee, UserEmployeeVm>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.User.Name))
                .ForMember(x => x.LastName , y => y.MapFrom(src => src.User.LastName))
                .ForMember(x => x.Email, y => y.MapFrom(src => src.User.Email))
                .ReverseMap();
            CreateMap<Sale, SaleDto>()
                .ForMember(x => x.FullNameClient, y => y.MapFrom(src => $"{src.Client.Name} {src.Client.LastName}"))
                .ForMember(x => x.FullNameEmployee, y => y.MapFrom(src => $"{src.Employee.User.Name} {src.Employee.User.LastName}"))
                .ReverseMap();

        }
    }
}
