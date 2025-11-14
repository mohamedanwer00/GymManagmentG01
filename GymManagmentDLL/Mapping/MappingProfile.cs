

using GymManagmentBLL.BusinessServices.View_Models.BookingVM;
using GymManagmentBLL.BusinessServices.View_Models.MembershipVM;
using GymManagmentBLL.BusinessServices.View_Models.SessionVM;

namespace GymManagmentBLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            MapSession();
            MapMember();
            MapPlan();
            MapTrainer();
            MapMembership();
            MapBooking();
        }

        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
              .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.Trainer.Name))//destnation>dest اللى هيتعرض عليه  options>الاختيارات  options.MapFrom>منين هياخد البيانات  src>المصدر اللى هياخد منه البيانات
              .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))//dest الmember اللى جاى من الداتا بيز
              .ForMember(dest => dest.AvailableSlots, options => options.Ignore());


            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();//يعنى العكس برضو اقدر احول من ده الى ده ومن ده الى ده

            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.CategoryName));

        }

        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, options => options.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));



            CreateMap<HealthRecordViewModel, HealthRecord>()

                .ReverseMap();


            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.DateOfBirth, options => options.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"));



            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, options => options.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, options => options.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City));


            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, options => options.Ignore())
                .ForMember(dest => dest.Photo, options => options.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });



        }


        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<PlanToUpdateViewModel, Plan>()
                .ForMember(dest => dest.Name, options => options.Ignore())
                .ForMember(dest => dest.UpdatedAt, options => options.MapFrom(src => DateTime.Now));

        }


        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()

              .ForMember(dest => dest.Address, options => options.MapFrom(src => new Address
              {
                  BuildingNumber = src.BuildingNumber,
                  Street = src.Street,
                  City = src.City
              })
              );

            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest => dest.Specialities, options => options.MapFrom(src => src.Specialties.ToString()))
                .ForMember(dest => dest.DateOfBirth, options => options.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, options => options.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, options => options.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, options => options.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, options => options.MapFrom(src => src.Address.City));



            CreateMap<TrainerToUpdateViewModel, Trainer>()

                //.ForMember(dest => dest.Specialties, options => options.MapFrom(src => src.Specialities.ToString()))
                .ForMember(dest => dest.Name, options => options.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });


        }

        private void MapMembership()
        {
            CreateMap<Membership, MembershipViewModel>()
                .ForMember(dest => dest.MemberName, memberOptions: opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.PlanName, memberOptions: opt => opt.MapFrom(src => src.Plan.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<CreateMembershipViewModel, Membership>();
            

            CreateMap<Member, MemberSelectListViewModel>();
            CreateMap<Plan, PlanSelectListViewModel>();
        }

        private void MapBooking()
        {
            CreateMap<MemberSession, MemberForSessionViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.CreatedAt.ToString()));

            CreateMap<CreateBookingViewModel, MemberSession>();

        }

    }
}
