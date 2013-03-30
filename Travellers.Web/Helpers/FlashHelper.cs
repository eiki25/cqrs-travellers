using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Travellers.Web.Helpers
{
	public static class FlashHelpers
	{
		public static void FlashInfo(this Controller controller, string message)
		{
			controller.TempData["info"] = message;
		}
		public static void FlashSuccess(this Controller controller, string message)
		{
			controller.TempData["success"] = message;
		}
		public static void FlashWarning(this Controller controller, string message)
		{
			controller.TempData["warning"] = message;
		}
		public static void FlashError(this Controller controller, string message)
		{
			controller.TempData["error"] = message;
		}

		public static string Flash(this HtmlHelper helper)
		{
			var message = "";
			var className = "";

			if (helper.ViewContext.TempData["info"] != null)
			{
				message = helper.ViewContext.TempData["info"].ToString();
				className = "alert-info";
			}
			else if (helper.ViewContext.TempData["success"] != null)
			{
				message = helper.ViewContext.TempData["success"].ToString();
				className = "alert-success";
			}
			else if (helper.ViewContext.TempData["warning"] != null)
			{
				message = helper.ViewContext.TempData["warning"].ToString();
				className = "";
			}
			else if (helper.ViewContext.TempData["error"] != null)
			{
				message = helper.ViewContext.TempData["error"].ToString();
				className = "alert-error";
			}

			var sb = new StringBuilder();

			if (!String.IsNullOrEmpty(message))
			{
				sb.AppendLine("<script type=\"text/javascript\">");
				sb.AppendLine("$(document).ready(function() {");
				sb.AppendFormat("$('#flash').toggleClass('{0}').html('<a class=\"close\" data-dismiss=\"alert\" href=\"#\">&times;</a>{1}').slideDown('fast');", HttpUtility.HtmlEncode(className), HttpUtility.HtmlEncode(message));
				sb.AppendLine("});");
				sb.AppendLine("</script>");
			}

			return sb.ToString();
		}
	}
}