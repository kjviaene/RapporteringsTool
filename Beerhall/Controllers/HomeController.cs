using Microsoft.AspNetCore.Mvc;
using TrustTeamVersion4.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TrustteamVersion4.Models.Extension;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.ComponentModel;
using System.Web.Mvc;
//using Rotativa.AspNetCore;
//using Rotativa.AspNetCore.Options;
using Rotativa.NetCore;
using DinkToPdf;
using TrustteamVersion4.Models.ViewModels;

namespace TrustTeamVersion4.Controllers
{
	public class HomeController : Controller
	{
		#region Properties
		private readonly IHomeRepository _homeRepository;
		private IHostingEnvironment _hostingEnvironment;
		private static String chosenFilter;
		private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		Home _home = new Home();
		Home _filter = new Home();
		[JsonProperty]
		IEnumerable<Home> _homesFiltered = Enumerable.Empty<Home>();
		IEnumerable<Home> _homesSorted = Enumerable.Empty<Home>();
		IEnumerable<Home> _EmptyEnumerable = Enumerable.Empty<Home>();
		
		#endregion
		#region Constructor
		public HomeController(IHomeRepository homeRepository, IHostingEnvironment env)
		{
			_homeRepository = homeRepository;
			_hostingEnvironment = env;
		}
		#endregion
		#region Index
		public IActionResult Index()
		{
			SetViewDataIndex();
			return View("Index");
		}
		#endregion
		#region Table method
		// Wordt geladen als er gekozen is op wat men wil filteren plus ook als er gesorteerd wil worden eenmaal de tabel geladen is, de string sortOrder is optioneel
		// Indien er niets gekozen werd wordt de lijst gewoon ongesorteerd weergegeven
		public IActionResult Table(Home filter, string sortOrder)
		{
			
			_homesFiltered = (IEnumerable<Home>)HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			// Het doorgeven van de gekozen filter als string aan de view via een ViewBag (zodat dit kan weergegeven worden)
			ViewBag.filter = chosenFilter;
			#region SortOrder
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
			
			#region Switch statement
			// Controleren of de gebruiker iets ingaf om te sorteren
			if (!(String.IsNullOrEmpty(sortOrder)))
			{

				// Overlopen van de mogelijkheden om te sorteren.
				switch (sortOrder)
				{
					case "numb_desc":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderByDescending(h => h.Number);
						break;
					case "number":
						_homesSorted = (IEnumerable<Home>)_homesFiltered.ToList().OrderBy(h => h.Number);
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
				#endregion
			

				// de view terug laden met de gesorteerde data
				return View(_homesSorted);
			}
			#endregion
			// Als de filter niet null is uitvoeren, maw enkel de eerste maal dat data wordt geladen of indien er opnieuw naar de index werd gegaan
			// om opnieuw te filteren.
			// Dit zal dus nooit activeren als men gewoon wenst te sorteren met reeds eerdere gefilterde data
			if (!(filter.IsEmptyObject()) | _homesFiltered == null)
			{
				// Het filteren van de data adhv de meegeven filter.
				_homesFiltered = _homeRepository.Filter(filter);

				// Het opslaan van de filter zodanig dit kan weergegeven worden boven de data
				chosenFilter = filter.ToString();

				// Het doorgeven van de gekozen filter als string aan de view via een ViewBag (zodat dit kan weergegeven worden)
				ViewBag.filter = chosenFilter;

				//Bijhouden van de filter
				HttpContext.Session.SetObject<Home>("filter", filter);

				// Bijhouden van gefilterde Home objecten
				HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
			}
			if (CheckInput(filter))
			{
				SetViewDataIndex();
				return View("Index");
			}
			// Dit zal nooit geactiveerd worden. Aangezien de filter of de sortOrder parameter altijd een waarde zullen hebben.
			// Het staat hier echter omdat de vorige twee returns in een if statement staan waardoor er een error word gegeven
			// waar wordt gezegd dat er niet altijd een return is. Dus returnen we hier gewoon homes (wat null is )
			return View(_homesFiltered);
		}
		#endregion
		#region Graphs method
		public IActionResult Graphs(Home filter)
		{
			
			// Als er een filter is opgegeven dan voeren we de volgende stappen uit
			if (!(filter.IsEmptyObject()))
			{
			#region toepassen filter en controle filter
				// Het filteren van de data adhv de meegeven filter.
				_homesFiltered = _homeRepository.Filter(filter);     
				// Het opslaan van de filter zodanig dit kan weergegeven worden boven de data. Dit moet na het filteren,
				//dit omdat anders de data als "laatste maand" geselecteerd werd nog niet correct zijn.
				chosenFilter = filter.ToString();
				// Het doorgeven van de gekozen filter als string aan de view via een ViewBag (zodat dit kan weergegeven worden)
				ViewBag.filter = chosenFilter;
				//Bijhouden van de filter
				HttpContext.Session.SetObject<Home>("filter", filter);
				// Bijhouden van gefilterde Home objecten
				HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
			}
			// Als er geen filter is meegegeven dan kijken we of er eentje in de session zit
			else
			{
				_filter = HttpContext.Session.GetObjectHome<Home>("filter"); ;
				_homesFiltered = HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			}// Als er geen filter in de session zat dan tonen we terug de index pagina met een melding
			if (CheckInput(_filter))
			{
				SetViewDataIndex();
				return View("Index");
			}
			#endregion
			// Het aanmaken van een ViewModel nu we zeker zijn dat  _filter niet null is.
			GraphsViewModel temp1 = new GraphsViewModel(_homeRepository, _homesFiltered, _filter);
			// doorgeven ViewModel aan de View
			return View(temp1);
		}
		#endregion
		#region Reset method
		public IActionResult  Reset()
		{
			_homesFiltered = _EmptyEnumerable;
			HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
			SetViewDataIndex();
			return View("Index");

		}

		#endregion
		#region SlaFilter method
		public ActionResult SlaFilter(string imp, string urg) {
			IEnumerable<Home> _SlaPriorities = Enumerable.Empty<Home>();
			if (imp == "Not Set")
				imp = "NULL";
			if (urg == "Not Set")
				urg = "NULL";
			if (CheckInputMany(_homesFiltered))
			{
				_homesFiltered = HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
				if (CheckInputMany(_homesFiltered))
				{
					_homesFiltered = _homeRepository.GetAll();
				}
				_homeRepository.RemoveNull(_homesFiltered);
				_SlaPriorities = _homesFiltered.Where(h => h.SupportCallImpact.Equals(imp) && h.SupportCallUrgency.Equals(urg));
			}
			else
			{

				_homeRepository.RemoveNull(_homesFiltered);
				_SlaPriorities = _homesFiltered.Where(h => h.SupportCallImpact.Equals(imp) && h.SupportCallUrgency.Equals(urg));
				
			}
			return View("Table",_SlaPriorities);
		}
		#endregion
		#region Export Excel
		public IActionResult Export()
		{
			_homesFiltered = (IEnumerable<Home>)HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			var dataTable = new DataTable("data");
			dataTable = _homesFiltered.ToDataTable<Home>();

			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Excel Test");
				worksheet.Cells["A1"].LoadFromDataTable(dataTable, PrintHeaders: true);
				for (var col = 1; col < dataTable.Columns.Count + 1; col++)
				{
					worksheet.Column(col).AutoFit();
				}
				return File(package.GetAsByteArray(), XlsxContentType, "report.xlsx");
			}
		}
		#endregion
		
		#region methods
		public void SetViewDataIndex() {
			// Creatie van de mogelijke keuzes waar men op kan filteren. Deze worden uit de databank gehaald, zo blijft de dropdown altijd 
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
		}

		public bool CheckInput(Home home)
		{
			if (_homesFiltered.Count() == 0)
			{
				string ErrorMessage = "Deze zoekopdracht leverde geen resultaten op of u gaf geen gegevens in om op te filteren.";
				ViewData["Error"] = ErrorMessage;
				return true;
			}
			else {
				return false;
			}
		}
		public bool CheckInputMany(IEnumerable<Home> homes)
		{
			if (homes.Count() == 0 )
			{
				string ErrorMessage = "Deze zoekopdracht leverde geen resultaten op of u gaf geen gegevens in om op te filteren.";
				ViewData["Error"] = ErrorMessage;
				return true;
			}
			else
			{
				return false;
			}
		}
		// Een methode die een array vult met kolom headers zoals in excel, het eerste lement is A, het laatste zal ZZ zijn.
		public string[] AlphabeticalColumns(int amount)
		{
			int counter = 0;
			int counter2 = 0;
			string pre = "";
			string[] value = new string[amount];
			string[] alphabet = new[] { "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
			for (int i = 0; i <= amount; i++)
			{
				value.Append(pre + alphabet[i-counter2]);
				if (i == alphabet.Count()-1)
				{
					pre = alphabet[counter];
					counter++;
					counter2 = counter2 + alphabet.Count();
				}
			}
			return alphabet;
		}
		#endregion

		public IActionResult test()
		{
			SetViewDataIndex();
			_filter = HttpContext.Session.GetObjectHome<Home>("filter"); ;
			_homesFiltered = HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			GraphsViewModel temp2 = new GraphsViewModel(_homeRepository,_homesFiltered,_filter);

			return new ViewAsPdf("Graphs",temp2);
		}
	}



}
