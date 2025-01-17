using library_management_system.Models;
using library_management_system.Repository;
using library_management_system.Utils;

namespace library_management_system.Services.Members
{
    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberService;

        public MemberService(IRepository<Member> memberService)
        {
            _memberService = memberService;
        }

        public async Task AddMemberAsync(Member member)
        {
            await _memberService.AddAsync(member);
        }

        public void DeleteMemberAsync(Guid id)
        {
            var member = _memberService.GetByIdAsync(id).Result;
            _memberService.Delete(member);

        }

        public Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return _memberService.GetAllAsync();
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            return await _memberService.GetByIdAsync(id);
        }

        public async void UpdateMember(Member member, Member existingRecord)
        {
            
            if (existingRecord == null)
            {
                return;
            }

            if (existingRecord.Name != member.Name && member.Name != null)
            {
                existingRecord.Name = member.Name;
            }

            if (existingRecord.Email!= member.Email && member.Email != null)
            {
                existingRecord.Email = member.Email;
            }

            if (member.Password != null)
            {
                existingRecord.Password = Utilities.HashPassword(member).Password;
            }

            if (existingRecord.Role != member.Role && member.Role != null) 
            {
                existingRecord.Role = member.Role;
            }
            _memberService.Update(existingRecord);
        }
    }
}
