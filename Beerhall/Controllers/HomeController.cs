using Microsoft.AspNetCore.Mvc;
using TrustTeamVersion4.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TrustteamVersion4.Models.Extension;
using Newtonsoft.Json;


namespace TrustTeamVersion4.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHomeRepository _homeRepository;
		private static String chosenFilter;
		Home home = new Home();
		IEnumerable<Home> homes;
		[JsonProperty]
		IEnumerable<Home> _homesFiltered;
		IEnumerable<Home> _homesSorted;
		
		public HomeController(IHomeRepository homeRepository)
		{
			_homeRepository = homeRepository;
		}

		public IActionResult Index()
		{ // Creatie van de mogelijke keuzes waar men op kan filteren. Deze worden uit de databank gehaald, zo blijft de dropdown altijd 
			// up to date met de gegevens in de databank
			//De view haalt deze op uit de ViewData's
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
			return View();
		}

		// Wordt geladen als er gekozen is op wat men wil filteren plus ook als er gesorteerd wil worden eenmaal de tabel geladen is, de string sortOrder is optioneel
		// Indien er niets gekozen werd wordt de lijst gewoon ongesorteerd weergegeven
		public IActionResult Table(Home filter, string sortOrder)
		{ _homesFiltered =	(IEnumerable<Home>) HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			// ViewData met daarin alle kollom namen als strings
			ViewData["ColumnNames"] = home.getAttributen();
			// een Viewbag met de gekozen filters in string
			ViewBag.filter = chosenFilter;
			// Overlopen van alle mogelijke filters. Als sortOrder al een string bevat wil dit zeggen dat er al gesorteerd is. Als de gebruiker
			// er dus nogmaals op geklikt heeft wil dit zeggen dat hij ze van beneden naar boven wil zien (= desc). 
			// dan zal de waarde dus zo aangepast worden.
			ViewBag.Number = sortOrder == "number" ? "numb_desc" : "number";
			ViewBag.Month = sortOrder == "Month" ? "Month_desc" : "Month";
			ViewBag.Year = sortOrder == "Year" ? "Year_desc" : "Year";
			ViewBag.OrganizationNumber = sortOrder == "OrganizationNumber" ? "OrganizationNumber_desc" : "OrganizationNumber";
			ViewBag.InvoicCenterOrganization = sortOrder == "InvoicCenterOrganization" ? "InvoicCenterOrganization_desc" : "InvoicCenterOrganization";
			ViewBag.GroupName = sortOrder == "GroupName" ? "GroupName_desc" : "GroupName";
			ViewBag.PersonName = sortOrder == "PersonName" ? "PersonName_desc" : "PersonName";
			ViewBag.SupportCallType = sortOrder == "SupportCallType" ? "SupportCallType_desc" : "SupportCallType";
			ViewBag.SupportCallPriority = sortOrder == "SupportCallPriority" ? "SupportCallPriority_desc" : "SupportCallPriority";
			ViewBag.SupportCallImpact = sortOrder == "SupportCallImpact" ? "SupportCallImpact_desc" : "SupportCallImpact";
			ViewBag.SupportCallUrgency = sortOrder == "SupportCallUrgency" ? "SupportCallUrgency_desc" : "SupportCallUrgency";
			ViewBag.SupportCallStatus = sortOrder == "SupportCallStatus" ? "SupportCallStatus_desc" : "SupportCallStatus";
			ViewBag.SupportCallCategory = sortOrder == "SupportCallCategory" ? "SupportCallCategory_desc" : "SupportCallCategory";
			ViewBag.SupportCallOpenDate = sortOrder == "SupportCallOpenDate" ? "SupportCallOpenDate_desc" : "SupportCallOpenDate";
			ViewBag.SupportCallOpenTime = sortOrder == "SupportCallOpenTime" ? "SupportCallOpenTime_desc" : "SupportCallOpenTime";
			ViewBag.SupportCallClosedDate = sortOrder == "SupportCallClosedDate" ? "SupportCallClosedDate_desc" : "SupportCallClosedDate";
			ViewBag.SupportCallClosedTime = sortOrder == "SupportCallClosedTime" ? "SupportCallClosedTime_desc" : "SupportCallClosedTime";
			ViewBag.HoursTillClosed = sortOrder == "HoursTillClosed" ? "HoursTillClosed_desc" : "HoursTillClosed";
			ViewBag.HoursInStatusOpen = sortOrder == "HoursInStatusOpen" ? "HoursInStatusOpen_desc" : "HoursInStatusOpen";
			ViewBag.HoursInvoiceCenter = sortOrder == "HoursInvoiceCenter" ? "HoursInvoiceCenter_desc" : "HoursInvoiceCenter";
			ViewBag.InvoiceCallSummary = sortOrder == "InvoiceCallSummary" ? "InvoiceCallSummary_desc" : "InvoiceCallSummary";
			ViewBag.OpenedByUser = sortOrder == "OpenedByUser" ? "OpenedByUser_desc" : "OpenedByUser";
			ViewBag.AssignedToUser = sortOrder == "AssignedToUser" ? "AssignedToUser_desc" : "AssignedToUser";
			ViewBag.AssignedToQueue = sortOrder == "AssignedToQueue" ? "AssignedToQueue_desc" : "AssignedToQueue";
			ViewBag.FirstEventSummary = sortOrder == "FirstEventSummary" ? "FirstEventSummary_desc" : "FirstEventSummary";
			ViewBag.ClienteleSupportCallSummary = sortOrder == "ClienteleSupportCallSummary" ? "ClienteleSupportCallSummary_desc" : "ClienteleSupportCallSummary";
			ViewBag.InvoiceOrganizationName = sortOrder == "InvoiceOrganizationName" ? "InvoiceOrganizationName_desc" : "InvoiceOrganizationName";
			ViewBag.InvoiceOrganizationNumber = sortOrder == "InvoiceOrganizationNumber" ? "InvoiceOrganizationNumber_desc" : "InvoiceOrganizationNumber";
			ViewBag.InvoiceStatus = sortOrder == "InvoiceStatus" ? "InvoiceStatus_desc" : "InvoiceStatus";
			ViewBag.HoursClienteleWorkedOnSupportCall = sortOrder == "HoursClienteleWorkedOnSupportCall" ? "HoursClienteleWorkedOnSupportCall_desc" : "HoursClienteleWorkedOnSupportCall";

			// Controleren of de gebruiker iets ingaf om te sorteren
			if (!(String.IsNullOrEmpty(sortOrder)))
			{

			
				// Overlopen van de mogelijkheden om te sorteren.
				switch (sortOrder)
				{
					case "numb_desc":
						_homesSorted = (IEnumerable<Home>) _homesFiltered.ToList().OrderByDescending(h => h.Number);
						break;
					case "number":
						_homesSorted = (IEnumerable<Home>) _homesFiltered.ToList().OrderBy(h => h.Number);
						break;
					case "Month_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.Month);
						break;
					case "Month":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.Month);
						break;
					case "Year_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.Year);
						break;
					case "Year":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.Year);
						break;
					case "OrganizationNumber_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.OrganizationNumber);
						break;
					case "OrganizationNumber":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.OrganizationNumber);
						break;
					case "InvoicCenterOrganization_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.InvoicCenterOrganization);
						break;
					case "InvoicCenterOrganization":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.InvoicCenterOrganization);
						break;
					case "GroupName_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.GroupName);
						break;
					case "GroupName":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.GroupName);
						break;
					case "PersonName_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.PersonName);
						break;
					case "PersonName":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.PersonName);
						break;
					case "SupportCallType_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallType);
						break;
					case "SupportCallType":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallType);
						break;
					case "SupportCallPriority_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallPriority);
						break;
					case "SupportCallPriority":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallPriority);
						break;
					case "SupportCallImpact_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallImpact);
						break;
					case "SupportCallImpact":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallImpact);
						break;
					case "SupportCallUrgency_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallUrgency);
						break;
					case "SupportCallUrgency":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallUrgency);
						break;
					case "SupportCallStatus_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallStatus);
						break;
					case "SupportCallStatus":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallStatus);
						break;
					case "SupportCallCategory_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallCategory);
						break;
					case "SupportCallCategory":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallCategory);
						break;
					case "SupportCallOpenDate_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallOpenDate);
						break;
					case "SupportCallOpenDate":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallOpenDate);
						break;
					case "SupportCallOpenTime_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallOpenTime);
						break;
					case "SupportCallOpenTime":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallOpenTime);
						break;
					case "SupportCallClosedDate_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallClosedDate);
						break;
					case "SupportCallClosedDate":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallClosedDate);
						break;
					case "SupportCallClosedTime_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.SupportCallClosedTime);
						break;
					case "SupportCallClosedTime":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.SupportCallClosedTime);
						break;
					case "HoursTillClosed_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.HoursTillClosed);
						break;
					case "HoursTillClosed":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.HoursTillClosed);
						break;
					case "HoursInStatusOpen_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.HoursInStatusOpen);
						break;
					case "HoursInStatusOpen":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.HoursInStatusOpen);
						break;
					case "HoursInvoiceCenter_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.HoursInvoiceCenter);
						break;
					case "HoursInvoiceCenter":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.HoursInvoiceCenter);
						break;
					case "InvoiceCallSummary_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.InvoiceCallSummary);
						break;
					case "InvoiceCallSummary":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.InvoiceCallSummary);
						break;
					case "OpenedByUser_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.OpenedByUser);
						break;
					case "OpenedByUser":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.OpenedByUser);
						break;
					case "AssignedToUser_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.AssignedToUser);
						break;
					case "AssignedToUser":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.AssignedToUser);
						break;
					case "AssignedToQueue_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.AssignedToQueue);
						break;
					case "AssignedToQueue":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.AssignedToQueue);
						break;
					case "FirstEventSummary_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.FirstEventSummary);
						break;
					case "FirstEventSummary":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.FirstEventSummary);
						break;
					case "ClienteleSupportCallSummary_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.ClienteleSupportCallSummary);
						break;
					case "ClienteleSupportCallSummary":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.ClienteleSupportCallSummary);
						break;
					case "InvoiceOrganizationName_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.InvoiceOrganizationName);
						break;
					case "InvoiceOrganizationName":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.InvoiceOrganizationName);
						break;
					case "InvoiceOrganizationNumber_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.InvoiceOrganizationNumber);
						break;
					case "InvoiceOrganizationNumber":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.InvoiceOrganizationNumber);
						break;
					case "InvoiceStatus_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.InvoiceStatus);
						break;
					case "InvoiceStatus":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.InvoiceStatus);
						break;
					case "HoursClienteleWorkedOnSupportCall_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.HoursClienteleWorkedOnSupportCall);
						break;
					case "HoursClienteleWorkedOnSupportCall":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.HoursClienteleWorkedOnSupportCall);
						break;
				}
				
				// de view terug laden met de gesorteerde data
				return View(_homesSorted);
			}
			// Als de filter niet null is uitvoeren, maw enkel de eerste maal dat data wordt geladen of indien er opnieuw naar de index werd gegaan
			// om opnieuw te filteren.
			// Dit zal dus nooit activeren als men gewoon wenst te sorteren met reeds eerdere gefilterde data
			if (filter != null)
			{
				// Het opslaan van de filter zodanig dit kan weergegeven worden boven de data
				chosenFilter = filter.ToString();
				// Het doorgeven van de gekozen filter als string aan de view via een ViewBag (zodat dit kan weergegeven worden)
				ViewBag.filter = chosenFilter;
				// Het filteren van de data adhv de meegeven filter.
				_homesFiltered = _homeRepository.Filter(filter);

				HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered",_homesFiltered);
				
				// laden van de view met de gefilterde data
				return View(_homesFiltered);
			}
			// Dit zal nooit geactiveerd worden. Aangezien de filter of de sortOrder parameter altijd een waarde zullen hebben.
			// Het staat hier echter omdat de vorige twee returns in een if statement staan waardoor er een error word gegeven
			// waar wordt gezegd dat er niet altijd een return is. Dus returnen we hier gewoon homes (wat null is )
			return View(this.homes);
		}
		public IActionResult Graphs(Home filter)
		{
			Home test = new Home();
			test.InvoicCenterOrganization = "A'Domo";
			ViewData["Bedrijf"] = test.InvoicCenterOrganization;
			ViewData["Data"] = _homeRepository.GetEfficiency(_homeRepository.Filter(test));
			return View(_homesFiltered);
		}


	}
}
