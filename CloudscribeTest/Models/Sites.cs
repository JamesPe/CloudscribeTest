using cloudscribe.Core.Models.Generic;
using cloudscribe.Web.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESDM.Models
{
    public partial class Site
    {
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string SiteType { get; set; }
    }


    public partial class SitesListViewModel
    {

        public SitesListViewModel()
        {
            SitesList = new PagedResult<SitesListViewModel>();
            Paging = new PaginationSettings();
        }


        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string SiteType { get; set; }

        public string Q { get; set; }

        public int SortMode { get; set; } = 0;

        public PagedResult<SitesListViewModel> SitesList { get; set; }

        public PaginationSettings Paging { get; set; }
        public string Query { get; set; } = string.Empty;
        public List<Site> Sites { get; set; } = null;
    }
}

