﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CookingBakery.Model;
using CookingBakery.Models;
using CookingBakery.Modules.EventLikeModule.Interface;
using CookingBakery.Modules.EventModule.Interface;
using CookingBakery.Modules.UserEventModule.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CookingBakery.Pages.Home
{
    public class Details : PageModel
    {
        private readonly CookingBakery.Models.CookingBakeryContext _context;

        public Details(CookingBakery.Models.CookingBakeryContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }
        public IEnumerable<PostDetail> PostDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts
             .Include(x => x.Category).FirstOrDefaultAsync(m => m.PostId == id);

            PostDetails = await _context.PostDetails.Include(x => x.Product).Where(x => x.PostId.Equals(id)).ToListAsync();

            if (Post == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}