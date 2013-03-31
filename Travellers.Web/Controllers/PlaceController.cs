using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Travellers.Core.Commands;
using Travellers.Core.Entities;
using Travellers.Core.Repositories;
using Travellers.Core.ViewModels;
using Travellers.Web.ActionFilters;
using Travellers.Web.Helpers;

namespace Travellers.Web.Controllers
{
	public class PlaceController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IRepository<Place> _placeRepository;

		public PlaceController(ICommandDispatcher commandDispatcher, IRepository<Place> placeRepository)
		{
			_commandDispatcher = commandDispatcher;
			_placeRepository = placeRepository;
		}

		public ActionResult Search(string query = null)
		{
			var searchResult = _placeRepository.Search(query)
				.Select(x => new PlaceModel
					             {
						             Id = x.Id,
						             Name = x.Name,
						             Description = x.Description,
						             Points = x.Points
					             });

			return View(new SearchPlaceModel { Query = query, Places = searchResult });
		}

		[HttpPost, ActionName("Search")]
		public ActionResult Search_POST(string query)
		{
			return RedirectToAction("Search", new { query });
		}

		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			var place = _placeRepository.ById(id);

			if (place == null)
			{
				return new HttpNotFoundResult(string.Format("Place with id {0} not found.", id));
			}

			var model = new PlaceModel
			{
				Id = place.Id,
				Name = place.Name,
				Description = place.Description,
				Points = place.Points
			};

			return View(model);
		}

		[HttpPost]
		[Persistence]
		public ActionResult Edit(PlaceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var place = _placeRepository.ById(model.Id);

			if (place == null)
			{
				return new HttpNotFoundResult(string.Format("Place with id {0} not found.", model.Id));
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
		[Persistence]
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