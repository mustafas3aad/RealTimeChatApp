﻿using Microsoft.EntityFrameworkCore;
using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Models;
using System.Security.Claims;

namespace Real_Time_Chat_Application.Helpers
{

    

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor,ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        
        public string UserId
        {
            get
            {
               
                var id = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

                
                if (string.IsNullOrEmpty(id))
                {
                    id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                return id;
            }
        }

       
        public async Task<AppUser> GetUser()
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == UserId);
            return user;
        }
    }
}
