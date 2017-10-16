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

namespace ESDM.Controllers
{
    public class SitesController : Controller
    {
        private const int DefaultPageSize = 10;
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

        
    }
}
