using library_management_system.Models;

namespace library_management_system.Services.Members
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(Guid id);
        Task AddMemberAsync(Member member);
        void UpdateMember(Member member, Member existingMember);
        void DeleteMemberAsync(Guid id);

    }
}
