using Health_Joy_Mobile_Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Joy_Backend_Mobile.Applications.UserOperations.GetUsers
{
    public class GetUsersQuery
    {
        private readonly AppDbContext _context;

        public GetUsersQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching users: {ex}");
                return new StatusCodeResult(500);
            }
        }
    }

    //ihtiyaca göre modifiye edebiliriz.
    public class UserViewModel
    {
        public string FullName { get; set; }
    }
}
