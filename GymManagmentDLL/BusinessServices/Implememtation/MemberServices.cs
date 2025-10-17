using GymManagmentBLL.BusinessServices.Interfaces;
using GymManagmentBLL.BusinessServices.View_Models;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Implememtation
{
    internal class MemberServices : IMemberServices
    {
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<Membership> _memberShipRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;

        public MemberServices(IGenericRepository<Member> memberRepository,
            IGenericRepository<Membership> memberShipRepository,
            IPlanRepository planRepository,
            IGenericRepository<HealthRecord> healthRecordRepository)
        {
            _memberRepository = memberRepository;
            _memberShipRepository = memberShipRepository;
            _planRepository = planRepository;
            _healthRecordRepository = healthRecordRepository;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            var EmailExist = _memberRepository.GetAll(X => X.Email == createMember.Email).Any();
            var PhoneExist = _memberRepository.GetAll(X => X.Phone == createMember.Phone).Any();

            if (EmailExist || PhoneExist)
                return false;

            var member = new Member
            {
                Name = createMember.Name,
                Email = createMember.Email,
                Phone = createMember.Phone,
                Gender = createMember.Gender,
                DateOfBirth = createMember.DateOfBirth,
                Address = new Address
                {
                    BuildingNumber = createMember.BuildingNumber,
                    City = createMember.City,
                    Street = createMember.Street,
                },


                HealthRecord = new HealthRecord
                {
                    Height = createMember.HealthRecord.Height,
                    Weight = createMember.HealthRecord.Weight,
                    BloodType = createMember.HealthRecord.BloodTybe,
                    Note = createMember.HealthRecord.Note,

                }

            };
            return _memberRepository.Add(member) > 0;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll();

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

            var memberViewModel = members.Select(M => new MemberViewModel
            {
                Id = M.Id,
                Name = M.Name,
                Email = M.Email,
                Phone = M.Phone,
                Photo = M.Photo,
                Gender = M.Gender.ToString(),
            });
            return memberViewModel;

        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _memberRepository.GetById(memberId);
            if (member is null) return null;
            var memberViewModel = new MemberViewModel
            {
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber}-{member.Address.Street}-{member.Address.City}",
                Photo = member.Photo,
                //MemberShipStartDate=member.Memberships.FirstOrDefault(X=> X.Status=="Active")?.CreatedAt.ToShortDateString(),

            };
            var membership = _memberShipRepository.GetAll(X => X.MemberId == memberId && X.Status == "Active").FirstOrDefault();
            if (membership is not null)
            {
                memberViewModel.MemberShipStartDate = membership.CreatedAt.ToShortDateString();
                memberViewModel.MemberShipEndDate = membership.EndDate.ToShortDateString();


                var plan = _planRepository.GetPlanById(membership.PlanId);

                memberViewModel.PlanName = plan?.Name;
            }
            return memberViewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecord(int memberId)
        {
            var memberHealthRecord = _healthRecordRepository.GetById(memberId);

            if (memberHealthRecord is null) return null;

            return new HealthRecordViewModel
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodTybe = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note,
            };

        }
    }
}
