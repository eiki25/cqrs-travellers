using System;
using System.Web.Mvc;
using Travellers.Core.Commands;
using Travellers.Core.Queries;
using Travellers.Core.ViewModels;
using Travellers.Web.Helpers;

namespace Travellers.Web.Controllers
{
	public class TravellerController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IQueryService _queryService;

		public TravellerController(ICommandDispatcher commandDispatcher, IQueryService queryService)
		{
			_commandDispatcher = commandDispatcher;
			_queryService = queryService;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Search(string query = null)
		{
			var model = _queryService.ExecuteQuery(new TravellersBySearch { SearchString = query });
			return View(model);
		}

		[HttpPost, ActionName("Search")]
		public ActionResult Search_POST(string query)
		{
			return RedirectToAction("Search", new {query});
		}

		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			var model = _queryService.ExecuteQuery(new TravellerById { Id = id });

			if (model == null)
			{
				return new HttpNotFoundResult(string.Format("Traveller with id {0} not found.", id));
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(TravellerModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_commandDispatcher.Send(new ChangeTravellerName(model.Id, model.Firstname, model.Lastname));
			_commandDispatcher.Send(new ChangeTravellerCountry(model.Id, model.Country));

			this.FlashSuccess(string.Format("Traveller '{0} {1}' updated", model.Firstname, model.Lastname));

			return RedirectToAction("Edit", new {id = model.Id});
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View(new TravellerModel());
		}

		[HttpPost]
		public ActionResult Create(TravellerModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var command = new CreateTraveller(Guid.NewGuid(), model.Firstname, model.Lastname, model.Country);
			_commandDispatcher.Send(command);

			this.FlashSuccess(string.Format("Traveller '{0} {1}' created", model.Firstname, model.Lastname));

			return RedirectToAction("Edit", new { id = command.TravellerId });
		}

		[HttpGet]
		public ActionResult VisitPlace(Guid id)
		{
			var model = _queryService.ExecuteQuery(new VisitPlaceByTravellerId { TravellerId = id });

			if (model == null)
			{
				return HttpNotFound("Traveller could not be found.");
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult VisitPlace(VisitPlaceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var selectedPlace = _queryService.ExecuteQuery(new PlaceById { Id = model.SelectedPlaceId });

			_commandDispatcher.Send(new VisitPlace(model.TravellerId, selectedPlace.Id, selectedPlace.Points, model.Rating));

			this.FlashSuccess(string.Format("Traveller '{0}' visited '{1}'", model.TravellerName, selectedPlace.Name));

			return RedirectToAction("Edit", new { id = model.TravellerId });
		}
	}
}