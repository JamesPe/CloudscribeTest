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

        public async Task<IActionResult> SiteRecord(string code)
        {
            return View( _context.Sites.Where(x => x.SiteName == "Reed Hill SSSI").ToList())  ;
        }

        //public async Task<IActionResult> Source(string id)
        //{
        //    var sourceRecord = await queries.GetRecordXml("source", id);
        //    if (sourceRecord == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = new SourceViewModel();
        //    model.Main = DynamicXml.Load(sourceRecord.Xml);
        //    model.Docs = await GetLLWSData(id);

        //    return View(model);
        //}

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sites.ToListAsync());
        }
    }
}
