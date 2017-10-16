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
            //return View(GetLists(1, 10));
        }

        //public async Task<PagedResult<SitesListViewModel>> GetLists(
        //    int pageNumber,
        //    int pageSize,
        //    CancellationToken cancellationToken = default(CancellationToken)
        //    )
        //{
        //    cancellationToken.ThrowIfCancellationRequested();

        //    int offset = (pageSize * pageNumber) - pageSize;

        //    var query = _context.SitesListViewModel.OrderBy(x => x.SiteName)
        //        .Select(p => p)
        //        .Skip(offset)
        //        .Take(pageSize)
        //        ;

        //    var result = new PagedResult<SitesListViewModel>();
        //    result.Data = await query.AsNoTracking().ToListAsync(cancellationToken);
        //    result.TotalItems = await _context.SitesListViewModel.CountAsync();
        //    result.PageNumber = pageNumber;
        //    result.PageSize = pageSize;

        //    return result;

        //}

        public IActionResult SiteList(
            int? pageNumber,
            int? pageSize,
            string query = "A")
        {
            var itemsPerPage = pageSize.HasValue ? pageSize.Value : DefaultPageSize;
            var currentPageNum = pageNumber.HasValue ? pageNumber.Value : 1;
            var offset = (itemsPerPage * currentPageNum) - itemsPerPage;
            var model = new SitesListViewModel();

            //var filtered = this.allSites.Where(p =>
            //    p.SiteName.StartsWith(query)
            //    );

            var filtered = _context.Sites.Where(s => s.SiteName.StartsWith(query));

            //model.Sites = filtered
            //    .Skip(offset)
            //    .Take(itemsPerPage)
            //    .ToList();

            model.Sites = filtered
            .ToList();

            model.Paging.CurrentPage = currentPageNum;
            model.Paging.ItemsPerPage = itemsPerPage;
            model.Paging.TotalItems = filtered.ToList().Count;
            model.Query = query; //TODO: sanitize

            return View(model);


        }


        //public async Task<IActionResult> Index(GlobalSearchViewModel model)
        //{
        //    ViewData["Title"] = "Search";
        //    if (Request.QueryString.HasValue)
        //    {
        //        var data = await queries.PagedSearch(
        //            model.PageNumber,
        //            model.PageSize,
        //            model.CleanedSearchString,
        //            model.ExactMatch,
        //            String.Empty
        //            );

        //        var totalRows = 0;
        //        if (data.Count > 0)
        //        {
        //            totalRows = data[0].TotalRecords;
        //        }

        //        model.TotalRows = totalRows;
        //        model.SearchResults = data;

        //        model.MapJsonUrl = Url.RouteUrl("MonumentSearch",
        //        new
        //        {
        //            ps = scriptSettings.MaxMapMarkers,
        //            q = model.SearchString,
        //            em = model.ExactMatch
        //        });

        //    }

        //    return View(model);

        //}
    }
}
