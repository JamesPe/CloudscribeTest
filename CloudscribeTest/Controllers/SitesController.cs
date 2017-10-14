using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESDM.Models;

namespace ESDM.Controllers
{
    public class SitesController : Controller
    {
        private readonly CMSiDataContext _context;

        public SitesController(CMSiDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sites.ToListAsync());
        }
    }
}
