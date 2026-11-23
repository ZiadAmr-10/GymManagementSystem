using AutoMapper;
using GymManagementSystem.BLL.Services.Sessions.ViewModels;
using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.BLL.Services.Sessions.Mapping
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>();

            CreateMap<Session, SessionDetailsViewModel>()
                 .ForMember(dest=>dest.TrainerName ,opt=>opt.MapFrom(src=>src.Trainer.Name))
                 .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.Category.CategoryName));
                 

        }
    }
}
