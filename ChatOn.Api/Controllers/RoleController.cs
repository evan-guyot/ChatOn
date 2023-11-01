using ChatOn.Database;
using ChatOn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatOn.Api.Controllers
{
    public class RoleController : Controller
    {
        private readonly DatabaseContext _context;

        public RoleController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("roles"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IList<IRoleApiData>>> GetAll()
        {
            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpDelete("roles/{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            
            if (role == null)
            {
                return NotFound();
            }

            if(role.IsDeletable)
            {
                return Conflict("This role cannot be deleted");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
