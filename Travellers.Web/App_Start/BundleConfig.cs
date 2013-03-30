using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Travellers.Web.App_Start.BundleConfig), "RegisterBundles")]

namespace Travellers.Web.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap*"));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

			BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
			BundleTable.Bundles.Add(new StyleBundle("~/Content/site").Include("~/Content/site.css"));
		}
	}
}
