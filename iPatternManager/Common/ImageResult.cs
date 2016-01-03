using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Web;

namespace Common
{
	public class ImageResult : ActionResult
	{
		public MemoryStream SourceStream { get; set; }
		public string ContentType { get; set; }

		public ImageResult(MemoryStream sourceStream, string contentType)
		{
			SourceStream = sourceStream;
			ContentType = contentType;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var res = context.HttpContext.Response;
			res.Clear();

			res.Cache.SetCacheability(HttpCacheability.NoCache);
			res.ContentType = ContentType;

			if (SourceStream != null)
			{
				SourceStream.WriteTo(res.OutputStream);
			}
		}
	}



}
