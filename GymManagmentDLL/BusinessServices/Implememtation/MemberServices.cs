using AutoMapper;

namespace GymManagmentBLL.BusinessServices.Implememtation
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {

            if (IsEmailExist(createMember.Email) || IsPhoneExist(createMember.Phone))
                return false;

            #region manual mapping
            //var member = new Member
            //{
            //    Name = createMember.Name,
            //    Email = createMember.Email,
            //    Phone = createMember.Phone,
            //    Gender = createMember.Gender,
            //    DateOfBirth = createMember.DateOfBirth,
            //    Address = new Address
            //    {
            //        BuildingNumber = createMember.BuildingNumber,
            //        City = createMember.City,
            //        Street = createMember.Street,
            //    },


            //    HealthRecord = new HealthRecord
            //    {
            //        Height = createMember.HealthRecord.Height,
            //        Weight = createMember.HealthRecord.Weight,
            //        BloodType = createMember.HealthRecord.BloodTybe,
            //        Note = createMember.HealthRecord.Note,

            //    }

            //}; 
            #endregion



            var member = _mapper.Map<CreateMemberViewModel, Member>(createMember);

            member.HealthRecord = _mapper.Map<HealthRecordViewModel, HealthRecord>(createMember.HealthRecordViewModel);

            _unitOfWork.GetRepository<Member>().Add(member);
            return _unitOfWork.SaveChanges() > 0;


        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();

            if (members is null || !members.Any()) return [];


            #region manual mapping first way 
            //var listOfMembersViewModels = new List<MemberViewModel>();


            //foreach (var member in members) 
            //{
            //    var memberViewModel = new MemberViewModel
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Email = member.Email,
            //        Gender = member.Gender.ToString(),
            //    };
            //    listOfMembersViewModels.Add(memberViewModel);
            //}
            //return listOfMembersViewModels;
            #endregion

            #region MANUAL MAPPING
            //var memberViewModel = members.Select(M => new MemberViewModel
            //{
            //    Id = M.Id,
            //    Name = M.Name,
            //    Email = M.Email,
            //    Phone = M.Phone,
            //    Photo = M.Photo,
            //    Gender = M.Gender.ToString(),
            //});
            //return memberViewModel; 
            #endregion

            return _mapper.Map<IEnumerable<MemberViewModel>>(members);

        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;

            #region manual Mapping
            //var memberViewModel = new MemberViewModel
            //{
            //    Name = member.Name,
            //    Phone = member.Phone,
            //    Email = member.Email,
            //    Gender = member.Gender.ToString(),
            //    DateOfBirth = member.DateOfBirth.ToShortDateString(),
            //    Address = $"{member.Address.BuildingNumber}-{member.Address.Street}-{member.Address.City}",
            //    Photo = member.Photo,
            //    //MemberShipStartDate=member.Memberships.FirstOrDefault(X=> X.Status=="Active")?.CreatedAt.ToShortDateString(),

            //}; 
            #endregion

            var memberViewModel = _mapper.Map<MemberViewModel>(member);

            var membership = _unitOfWork.GetRepository<Membership>().GetAll(X => X.MemberId == memberId && X.Status == "Active").FirstOrDefault();
            if (membership is not null)
            {
                memberViewModel.MemberShipStartDate = membership.CreatedAt.ToShortDateString();
                memberViewModel.MemberShipEndDate = membership.EndDate.ToShortDateString();


                var plan = _unitOfWork.GetRepository<Plan>().GetById(membership.PlanId);

                memberViewModel.PlanName = plan?.Name;
            }
            return memberViewModel;
        }

        public MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return null;

            #region manual Mapping 
            //return new MemberToUpdateViewModel
            //{
            //    Name = member.Name,
            //    Photo = member.Photo,
            //    Email = member.Email,
            //    Phone = member.Phone,
            //    BuildingNumber = member.Address.BuildingNumber,
            //    Street = member.Address.Street,
            //    City = member.Address.City,
            //}; 
            #endregion

            return _mapper.Map<MemberToUpdateViewModel>(member);
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);

            if (memberHealthRecord is null) return null;

            #region manual mapping
            //return new HealthRecordViewModel
            //{
            //    Height = memberHealthRecord.Height,
            //    Weight = memberHealthRecord.Weight,
            //    BloodTybe = memberHealthRecord.BloodType,
            //    Note = memberHealthRecord.Note,
            //}; 
            #endregion


            return _mapper.Map<HealthRecordViewModel>(memberHealthRecord);

        }

        public bool RemoveMember(int memberId)
        {
            try
            {
                var memberRepository = _unitOfWork.GetRepository<Member>();
                var member = memberRepository.GetById(memberId);
                if (member is null)
                    return false;

                var memberSessionIds = _unitOfWork.GetRepository<MemberSession>()
                    .GetAll(X => X.MemberId == memberId)
                    .Select(X => X.SessionId);
                var hasFutureSessions = _unitOfWork.GetRepository<Session>().GetAll(
                    S => memberSessionIds.Contains(S.Id) && S.StartDate > DateTime.Now).Any();

                if (hasFutureSessions)
                    return false;

                var memberShipRepository = _unitOfWork.GetRepository<Membership>();
                var memberShips = memberShipRepository.GetAll(X => X.MemberId == memberId);


                if (memberShips.Any())
                {
                    foreach (var membership in memberShips)
                    {
                        memberShipRepository.Delete(membership);
                    }
                }
                memberRepository.Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {

                var memberRepository = _unitOfWork.GetRepository<Member>();
                //if (IsEmailExist(memberToUpdate.Email) || IsPhoneExist(memberToUpdate.Phone))
                //    return false;
                var emailExists = _unitOfWork.GetRepository<Member>()
                    .GetAll(X => X.Email == memberToUpdate.Email && X.Id != memberId)
                    .Any();
                var phoneExists = _unitOfWork.GetRepository<Member>()
                    .GetAll(X => X.Phone == memberToUpdate.Phone && X.Id != memberId)
                    .Any();
                var member = memberRepository.GetById(memberId);
                if (member is null)
                    return false;

                #region manual mapping

                //member.Email = memberToUpdate.Email;
                //member.Phone = memberToUpdate.Phone;
                ////member.Name = memberToUpdate.Name;
                //member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                //member.Address.Street = memberToUpdate.Street;
                //member.Address.City = memberToUpdate.City;

                //member.UpdatedAt = DateTime.Now;//مهمه  
                #endregion

                _mapper.Map(memberToUpdate, member);

                memberRepository.Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {

            return _unitOfWork.GetRepository<Member>().GetAll(X => X.Phone == phone).Any();
        }
    }
}
