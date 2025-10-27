namespace GymManagmentBLL.BusinessServices.Interfaces
{
    internal interface IMemberServices
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);

        MemberViewModel? GetMemberDetails(int memberId);

        HealthRecordViewModel? GetMemberHealthRecord(int memberId);
        MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberId);

        bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate);

        bool RemoveMember(int memberId);
    }
}
