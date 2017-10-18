using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESDM.Models;
using Microsoft.EntityFrameworkCore;
using ESDM.Controllers;

namespace CloudscribeTest.Controllers
{
    public class EditPopupController : Controller
    {

        private readonly CMSiDataContext _context;

        public EditPopupController(CMSiDataContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> EditPopup([Bind("SiteCode,SiteName,SiteType")] Site Site)
        {
            //if (id.ToUpper() != Site.SiteCode.ToUpper())
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Site);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteExists(Site.SiteCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SitesController.SiteList));
            }
            return View(Site);
        }

        private bool SiteExists(string id)
        {
            return _context.Sites.Any(e => e.SiteCode == id);
        }
    }
}