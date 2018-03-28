using Microsoft.AspNetCore.Mvc;
using TrustTeamVersion4.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using TrustTeamVersion4.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrustTeamVersion4.Models;
using System;

namespace TrustTeamVersion4.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
		IEnumerable<Home> homes;
		IEnumerable<Home> homesFiltered;
		public HomeController(IHomeRepository homeRepository)
        {
			_homeRepository = homeRepository;
        }

        public IActionResult Index(IEnumerable<Home> homes)
        {
			if (homes == null || homes.Count() == 0)
			{
				this.homes = _homeRepository.GetAll().OrderBy(b => b.Number).ToList();
			}
			else { this.homes = homes; }
            ViewData["TotalTurnover"] = this.homes.Sum(h => h.Number);
			ViewData["TicketNumbers"] = new SelectList(_homeRepository.GetNumbers());
			ViewData["Years"] = new SelectList(_homeRepository.GetYear());
			ViewData["OrganizationNumbers"] = new SelectList(_homeRepository.GetOrganizationNumbers());
			ViewData["InvoiceOrg"] = new SelectList(_homeRepository.getInvoiceCenterOrganizations());
			ViewData["GroupNames"] = new SelectList(_homeRepository.getGroupNames());
			ViewData["PersonNames"] = new SelectList(_homeRepository.getPersonNames());
			ViewData["SupportCallTypes"] = new SelectList(_homeRepository.getSupportCallTypes());
			ViewData["SupportCallPriorities"] = new SelectList(_homeRepository.getSupportCallPriorities());
			ViewData["SupportCallImpacts"] = new SelectList(_homeRepository.getSupportCallImpacts());
			ViewData["SupportCallUrgencies"] = new SelectList(_homeRepository.getSupportCallUrgencies());
			ViewData["SupportCallstatusses"] = new SelectList(_homeRepository.getSupportCallStatusses());
			ViewData["SupportCallCategories"] = new SelectList(_homeRepository.getSupportCallCategories());
			ViewData["OpenedByUsers"] = new SelectList(_homeRepository.getOpenedByUsers());
			ViewData["AssignedToUsers"] = new SelectList(_homeRepository.getAssignedtoUsers());
			ViewData["AssignedToQueus"] = new SelectList(_homeRepository.getAssignedToQueus());
			ViewData["InvoiceStatusses"] = new SelectList(_homeRepository.getInvoiceStatusses());
			return View(this.homes);
        }

		public IActionResult Table(Home filter)
		{
			homesFiltered = _homeRepository.Filter(filter);
			
			return View(homesFiltered);
		}

		//public void IndexSortByNumber(double number) {
		//	number = 143869;
		//	IEnumerable<Home> homes = _homeRepository.GetBy(number).OrderBy(b => b.Number).ToList();

		//	Index(homes);
		//}

		//public IActionResult Edit(int id)
		//{
		//    Home home = _homeRepository.GetBy(id);
		//    ViewData["IsEdit"] = true;
		//    ViewData["Locations"] = GetLocationsAsSelectList();
		//    return View(new BrewerEditViewModel(brewer));
		//}

		//[HttpPost]
		//public IActionResult Edit(BrewerEditViewModel brewerEditViewModel, int id)
		//{
		//    Brewer brewer = null;
		//    try
		//    {
		//        brewer = _brewerRepository.GetBy(id);
		//        MapBrewerEditViewModelToBrewer(brewerEditViewModel, brewer);
		//        _brewerRepository.SaveChanges();
		//        TempData["message"] = $"You successfully updated brewer {brewer.Name}.";
		//    }
		//    catch
		//    {
		//        TempData["error"] = $"Sorry, something went wrong, brewer {brewer?.Name} was not updated...";
		//    }
		//    return RedirectToAction(nameof(Index));
		//}

		//public IActionResult Create()
		//{
		//    ViewData["IsEdit"] = false;
		//    ViewData["Locations"] = GetLocationsAsSelectList();
		//    return View(nameof(Edit), new BrewerEditViewModel(new Brewer()));
		//}

		//[HttpPost]
		//public IActionResult Create(BrewerEditViewModel brewerEditViewModel)
		//{
		//    Brewer brewer = new Brewer();
		//    try
		//    {
		//        MapBrewerEditViewModelToBrewer(brewerEditViewModel, brewer);
		//        _brewerRepository.Add(brewer);
		//        _brewerRepository.SaveChanges();
		//        TempData["message"] = $"You successfully added brewer {brewer.Name}.";
		//    }
		//    catch
		//    {
		//        TempData["error"] = "Sorry, something went wrong, the brewer was not added...";
		//    }
		//    return RedirectToAction(nameof(Index));
		//}

		//public IActionResult Delete(int id)
		//{
		//    ViewData[nameof(Brewer.Name)] = _brewerRepository.GetBy(id).Name;
		//    return View();
		//}

		//[HttpPost, ActionName("Delete")]
		//public IActionResult DeleteConfirmed(int id)
		//{
		//    Brewer brewer = null;
		//    try
		//    {
		//        brewer = _brewerRepository.GetBy(id);
		//        _brewerRepository.Delete(brewer);
		//        _brewerRepository.SaveChanges();
		//        TempData["message"] = $"You successfully deleted brewer {brewer.Name}.";
		//    }
		//    catch
		//    {
		//        TempData["error"] = $"Sorry, something went wrong, brewer {brewer?.Name} was not deleted…";
		//    }
		//    return RedirectToAction(nameof(Index));
		//}

		private SelectList GetHostsAsSelectList()
        {
            return new SelectList(
                            _homeRepository.GetAll().OrderBy(l => l.Number),
                            nameof(Home.InvoiceOrganizationName),
                            nameof(Home.OrganizationNumber));
        }

        //private void MapBrewerEditViewModelToBrewer(BrewerEditViewModel brewerEditViewModel, Brewer brewer)
        //{
        //    brewer.Name = brewerEditViewModel.Name;
        //    brewer.Street = brewerEditViewModel.Street;
        //    brewer.Location = brewerEditViewModel.PostalCode == null
        //        ? null
        //        : _locationRepository.GetBy(brewerEditViewModel.PostalCode);
        //    brewer.Turnover = brewerEditViewModel.Turnover;
        //}
    }
}
