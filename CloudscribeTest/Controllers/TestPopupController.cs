using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CloudscribeTest.Controllers
{
    public class TestPopupController : Controller


    {

        public IActionResult TestPopup()        {
            return PartialView();
        }
    }
}