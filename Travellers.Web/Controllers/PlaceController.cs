using System;
using System.Web.Mvc;
using Travellers.Core.Commands;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;
using Travellers.Web.Helpers;

namespace Travellers.Web.Controllers
{
	public class PlaceController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryService _queryService;

		public PlaceController(ICommandDispatcher commandDispatcher, IQueryService queryService)
		{
			_commandDispatcher = commandDispatcher;
			_queryService = queryService;
		}

		public ActionResult Search(string query = null)
		{
			var model = _queryService.ExecuteQuery(new PlacesBySearch { SearchString = query });
			return View(model);
		}

		[HttpPost, ActionName("Search")]
		public ActionResult Search_POST(string query)
		{
			return RedirectToAction("Search", new { query });
		}

		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			var model = _queryService.ExecuteQuery(new PlaceById { Id = id });

			if(model == null)
			{
				return new HttpNotFoundResult(string.Format("Place with id {0} not found.", id));
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(PlaceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_commandDispatcher.Send(new ChangePlaceName(model.Id, model.Name));
			_commandDispatcher.Send(new ChangePlaceDescription(model.Id, model.Description));
			_commandDispatcher.Send(new ChangePlacePoints(model.Id, model.Points));

			this.FlashSuccess(string.Format("Place '{0}' updated", model.Name));

			return RedirectToAction("Edit", new { id = model.Id });
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(new PlaceModel());
		}

		[HttpPost]
		public ActionResult Create(PlaceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var command = new CreatePlace(Guid.NewGuid(), model.Name, model.Description, model.Points);
			_commandDispatcher.Send(command);

			this.FlashSuccess(string.Format("Place '{0}' created", model.Name));

			return RedirectToAction("Edit", new { id = command.PlaceId });
		}
	}
}