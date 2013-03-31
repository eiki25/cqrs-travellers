using System;
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
	public class TravellerController : Controller
	{
		private readonly ICommandDispatcher _commandDispatcher;
		private readonly IRepository<Traveller> _travellerRepository;
		private readonly IRepository<Place> _placeRepository;

		public TravellerController(ICommandDispatcher commandDispatcher, IRepository<Traveller> travellerRepository, IRepository<Place> placeRepository)
		{
			_commandDispatcher = commandDispatcher;
			_travellerRepository = travellerRepository;
			_placeRepository = placeRepository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Search(string query = null)
		{
			var searchResult = _travellerRepository.Search(query)
				.Select(x => new TravellerModel
					             {
						             Id = x.Id,
						             Firstname = x.Firstname,
						             Lastname = x.Lastname,
						             Country = x.Country
					             });

			return View(new SearchTravellerModel { Query = query, Travellers = searchResult });
		}

		[HttpPost, ActionName("Search")]
		public ActionResult Search_POST(string query)
		{
			return RedirectToAction("Search", new {query});
		}

		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			var traveller = _travellerRepository.ById(id);

			if (traveller == null)
			{
				return new HttpNotFoundResult(string.Format("Traveller with id {0} not found.", id));
			}

			var model = new TravellerModel
				            {
					            Id = traveller.Id,
					            Firstname = traveller.Firstname,
					            Lastname = traveller.Lastname,
					            Country = traveller.Country,
					            IsReallyCool = traveller.IsReallyCool,
					            NumberOfVisits = traveller.NumberOfVisits,
					            TotalPoints = traveller.TotalPoints,
					            VisitedPlaces = traveller.Visits.Select(v => new VisitModel
						                                                         {
							                                                         PlaceId = v.PlaceId,
							                                                         PlaceName = v.Place.Name,
							                                                         Rating = v.Rating
						                                                         })
				            };

			return View(model);
		}

		[HttpPost]
		[Persistence]
		public ActionResult Edit(TravellerModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var traveller = _travellerRepository.ById(model.Id);

			if (traveller == null)
			{
				return new HttpNotFoundResult(string.Format("Traveller with id {0} not found.", model.Id));
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
		[Persistence]
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
			var traveller = _travellerRepository.ById(id);

			if (traveller == null)
			{
				return HttpNotFound("Traveller could not be found.");
			}

			var model = new VisitPlaceModel
				            {
					            TravellerId = id,
					            TravellerName = traveller.Firstname + " " + traveller.Lastname,
					            Places = _placeRepository.All()
						            .Select(x => new PlaceModel
							                         {
								                         Id = x.Id,
								                         Name = x.Name,
								                         Description = x.Description,
								                         Points = x.Points
							                         })
				            };

			return View(model);
		}

		[HttpPost]
		[Persistence]
		public ActionResult VisitPlace(VisitPlaceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var traveller = _travellerRepository.ById(model.TravellerId);
			var selectedPlace = _placeRepository.ById(model.SelectedPlaceId);
			_commandDispatcher.Send(new VisitPlace(model.TravellerId, selectedPlace.Id, selectedPlace.Points, model.Rating));

			this.FlashSuccess(string.Format("Traveller '{0} {1}' visited '{2}'", traveller.Firstname, traveller.Lastname, selectedPlace.Name));

			return RedirectToAction("Edit", new { id = traveller.Id });
		}
	}
}