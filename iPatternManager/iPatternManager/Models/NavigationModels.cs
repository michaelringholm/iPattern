using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPatternManager.Models
{
	public class BreadCrumbModel
	{
		public List<SelectListItem> Items { get; set; }
    }


    public class PagingModel
    {
        public List<SelectListItem> Items { get; set; }
    }
}
