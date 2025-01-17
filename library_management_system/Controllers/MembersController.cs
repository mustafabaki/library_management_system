using library_management_system.Models;
using library_management_system.Services.Members;
using library_management_system.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();
            return Ok(members);
        }

        [HttpGet]
        public async Task<IActionResult> GetMemberById(Guid id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);

        }

        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            var memberWithHashedPassword = Utilities.HashPassword(member);
            await _memberService.AddMemberAsync(member);
            return CreatedAtAction(nameof(AddMember), new { id = member.Id }, new { member.Id, member.Name, member.Email, member.Role });

        }

        [HttpPut]
        public async Task<IActionResult> UpdateMember(Guid id, [FromBody] Member member)
        {
            var existingMember = await _memberService.GetMemberByIdAsync(id);
            _memberService.UpdateMember(member, existingMember);
            return NoContent();
        }
    }
}