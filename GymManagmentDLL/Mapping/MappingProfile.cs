using AutoMapper;
using GymManagementSystemBLL.View_Models.SessionVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.Trainer.Name))//destnation>dest اللى هيتعرض عليه  options>الاختيارات  options.MapFrom>منين هياخد البيانات  src>المصدر اللى هياخد منه البيانات
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.AvailableSlots, options => options.Ignore());


            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();//يعنى العكس برضو اقدر احول من ده الى ده ومن ده الى ده

        }
    }
} 
