using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESDM.Models;
using System.Threading;
using cloudscribe.Pagination.Models;
using cloudscribe.Core.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ESDM.Controllers
{
    public class SitesController : Controller
    {
        private const int DefaultPageSize = 20;
        private List<Site> allSites = new List<Site>();



        private readonly CMSiDataContext _context;

        public SitesController(CMSiDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SiteRecord(string code)
        {
            return View( _context.Sites.Where(x => x.SiteName == "Reed Hill SSSI").ToList())  ;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sites.ToListAsync());
        }


        [Authorize(Policy = "CanListSites")]
        public IActionResult SiteList(
            int? pageNumber,
            int? pageSize,
            string query = "A")
        {
            var itemsPerPage = pageSize.HasValue ? pageSize.Value : DefaultPageSize;
            var currentPageNum = pageNumber.HasValue ? pageNumber.Value : 1;
            var offset = (itemsPerPage * currentPageNum) - itemsPerPage;
            var model = new SitesListViewModel();

            var filtered = _context.Sites.Where(s => s.SiteName.StartsWith(query));

            model.Sites = filtered
            .OrderBy(o => o.SiteName)
            .Skip(offset)
            .Take(itemsPerPage)
            .ToList();

            model.Paging.CurrentPage = currentPageNum;
            model.Paging.ItemsPerPage = itemsPerPage;
            model.Paging.TotalItems = filtered.ToList().Count;
            model.Query = query; //TODO: sanitize

            return View(model);
        }

        // GET: vwSiteSearches/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Site = await _context.Sites.SingleOrDefaultAsync(m => m.SiteCode == id);
            if (Site == null)
            {
                return NotFound();
            }
            return View(Site);
        }

        // POST: vwSiteSearches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SiteCode,SiteName,SiteType")] Site Site)
        {
            if (id.ToUpper() != Site.SiteCode.ToUpper())
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(SiteList));
            }
            return View(Site);
        }

        private bool SiteExists(string id)
        {
            return _context.Sites.Any(e => e.SiteCode == id);
        }

    }
}
