using AutoMapper;
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
                .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.Trainer.Name))//destnation>dest
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.AvailableSlots, options => options.Ignore());

        }
    }
}
