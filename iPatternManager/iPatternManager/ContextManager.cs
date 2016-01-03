using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Security;
using DTL;
using DAL;
using System.Web.Mvc;

namespace iPatternManager
{
    public class ContextManager
    {
        /* TODO - Keep in Session instead - MRS */        
        public static ContextManager Current
        {
            get
            {
                if (Membership.GetUser() == null)
                {
                    HttpContext.Current.Response.Redirect("/Account/LogOn");
                    return null;
                }
                else
                {
					Object cmObj = HttpContext.Current.Session["ContextManager"];
					ContextManager current;

					if (cmObj == null)
					{
						current = new ContextManager();

						current.Areas = AreaDAC.GetAreas(Membership.GetUser().ProviderUserKey);
						current.SelectedAreaID = UserDAC.GetDefaultAreaId(Membership.GetUser().ProviderUserKey);

						if (!current.SelectedAreaID.HasValue)
							throw new Exception("User must have a default area"); // HttpContext.Current.Response.Redirect("/Shared/Error");


						current.AreaListItems = new List<SelectListItem>();
						foreach (AreaDTO area in current.Areas)
						{
							bool selected = false;
							if (area.ID.Value == current.SelectedAreaID.Value)
								selected = true;

							current.AreaListItems.Add(new SelectListItem { Selected = selected, Text = area.Title, Value = area.ID.Value.ToString() });
						}

						//_current.AreaID = Convert.ToInt32(ConfigurationManager.AppSettings["areaID"]);

						HttpContext.Current.Session["ContextManager"] = current;
					}
					else
						current = (ContextManager)cmObj;

                    return current;
                }
            }
        }

        public List<AreaDTO> Areas { get; set; }
        public List<SelectListItem> AreaListItems { get; set; }

        private Nullable<Int32> _selectedAreaID;
        public Nullable<Int32> SelectedAreaID
        {
            get
            {
				if (_selectedAreaID.HasValue)
					return _selectedAreaID;
				else
					throw new Exception("No area selected");
            }

            set
            {
                _selectedAreaID = value;

                if (AreaListItems != null && AreaListItems.Count > 0 && value != null)
                {
                    foreach (SelectListItem item in AreaListItems)
                    {
                        item.Selected = item.Value == value.Value.ToString();
                    }
                }
            }
        }

        internal static void Reset()
        {
			HttpContext.Current.Session["ContextManager"] = null;
        }
    }
}
