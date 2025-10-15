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

        public MemberServices(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            var EmailExist = _memberRepository.GetAll(X => X.Email == createMember.Email).Any();
            var PhoneExist = _memberRepository.GetAll(X => X.Phone == createMember.Phone).Any();

            if (EmailExist||PhoneExist )
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
                    Height=createMember.HealthRecord.Height,
                    Weight=createMember.HealthRecord.Weight,
                    BloodType=createMember.HealthRecord.BloodTybe,
                    Note = createMember.HealthRecord.Note,

                }

            };
            return _memberRepository.Add(member)>0;
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

    }
}
