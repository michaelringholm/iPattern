using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace iPatternManager.Models
{
    public class WordIndexModel
    {
        public List<DTL.WordHeaderDTO> WordHeaders { get; set; }
		public bool ShowIgnoredWords { get; set; }
    }
}
