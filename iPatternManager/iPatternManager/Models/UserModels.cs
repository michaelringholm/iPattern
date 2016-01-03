using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DTL;
using System.Web.Mvc;

namespace iPatternManager.Models
{
    public class UserIndexModel
    {
        public List<UserDTO> Users { get; set; }
        //public IEnumerable<String> Areanames { get; set; }
        public Dictionary<string, List<SelectListItem>> AreaNameList;
        public List<AreaDTO> Areas { get; set; }
        public UserDTO SelectedUser { get; set; }
        //public List<SelectListItem> AreaNames { get; set; }
        //public SelectList AreaNames { get; set; }
        
    }

    public class UserEditModel
    {
        [Required]
        [DisplayName("User ID")]
        public String UserID { get; set; }

        //[Required]
        //[DisplayName("User name")]
        //public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Administrator")]
        public Boolean IsAdministrator { get; set; }
    }
}
