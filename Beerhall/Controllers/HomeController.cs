
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
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
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using TrustteamVersion4.Models.ViewModels;
using System.Collections.ObjectModel;
//using Rotativa.NetCore.Options;

namespace TrustTeamVersion4.Controllers
{
	public class HomeController : Controller
	{
		#region Properties
		private readonly IHomeRepository _homeRepository;
		private IHostingEnvironment _hostingEnvironment;
		private static String chosenFilter;
		private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		private bool slaView = false;
		Home _home = new Home();
		Home _filter = new Home();
		TableViewModel _viewModel;
		[JsonProperty]
		IEnumerable<Home> _homesFiltered = Enumerable.Empty<Home>();
		IEnumerable<Home> _homesSorted = Enumerable.Empty<Home>();
		IEnumerable<Home> _EmptyEnumerable = Enumerable.Empty<Home>();
		Dictionary<string, List<object>> _possibleChoices;
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
			slaView = false;
			_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string,List<object>>>("_possibleChoices");
			if (_possibleChoices == null | _possibleChoices.Count() == 0)
			{
				_possibleChoices = _homeRepository.getPossibleChoices();
				HttpContext.Session.SetObject("_possibleChoices", _possibleChoices);
			}
			ViewData["Selections"] = _possibleChoices;
			return View("Index");
		}
		#endregion
		#region Table method
		// Wordt geladen als er gekozen is op wat men wil filteren plus ook als er gesorteerd wil worden eenmaal de tabel geladen is, de string sortOrder is optioneel
		// Indien er niets gekozen werd wordt de lijst gewoon ongesorteerd weergegeven
		public IActionResult Table(Home filter, string sortOrder)
		{

			slaView = false;
			_homesFiltered = (IEnumerable<Home>)HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			_viewModel = HttpContext.Session.GetObjectSingle<TableViewModel>("_viewModel");
			// Het doorgeven van de gekozen filter als string aan de view via een ViewBag (zodat dit kan weergegeven worden)
			ViewBag.filter = chosenFilter;

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
			if (_viewModel == null)
			{
				_viewModel = new TableViewModel(_homesFiltered, _filter);
				HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
			}
			else if (!(_homesFiltered.SequenceEqual(_viewModel.Data)))
			{
				_viewModel = new TableViewModel(_homesFiltered, _filter);
				HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
			}
			if (CheckInput(filter))
			{
				_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");

				ViewData["Selections"] = _possibleChoices;
				return View("Index");
			}
			return View(_viewModel);
		}
		#endregion
		#region Sorteren Partial
		public PartialViewResult SortPartial(string sorter)
		{
			_viewModel = this.CheckSlaInput();
			_viewModel.Data = _viewModel.SortBy(_viewModel.Data, sorter).ToList<Home>();
			if (slaView == true)
				HttpContext.Session.SetObject<TableViewModel>("_slafilter", _viewModel);
			else
				HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
			return PartialView("_TableView",_viewModel);
		}
		#endregion
		#region Closing columns
		public PartialViewResult CloseColumnPartial(string closed)
		{
			_viewModel = this.CheckSlaInput();
			_viewModel.ClosedColumns.Add(closed);
			if (slaView == true)

				HttpContext.Session.SetObject<TableViewModel>("_slafilter", _viewModel);
			else
				HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
			return PartialView("_TableView",_viewModel);
		}
		#endregion
		#region Graphs method
		public IActionResult Graphs(Home filter, GraphsViewModel model)
		{
			GraphsViewModel graphsView = model;
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
				_filter = HttpContext.Session.GetObjectSingle<Home>("filter"); ;
				_homesFiltered = HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			}// Als er geen filter in de session zat dan tonen we terug de index pagina met een melding
			if (CheckInput(_filter))
			{
				_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");

				ViewData["Selections"] = _possibleChoices;
				return View("Index");
			}
			#endregion
			// Het aanmaken van een ViewModel nu we zeker zijn dat  _filter niet null is.
			if (graphsView.isNullObject())
				try
				{
					graphsView = new GraphsViewModel(_homeRepository, _homesFiltered, _filter);
				}
				catch (Exception e)
				{
					_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");

					ViewData["Selections"] = _possibleChoices;
					ViewData["Error"] = e.Message;
					this.Index();
				}
			HttpContext.Session.SetObject<GraphsViewModel>("graphsViewModel",graphsView);
			// doorgeven ViewModel aan de View
			return View(graphsView);
		}
		#endregion
		#region Reset method
		public IActionResult  Reset()
		{
			_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");
			_homesFiltered = _EmptyEnumerable;
			HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
			ViewData["Selections"] = _possibleChoices;
			return View("Index");

		}

		#endregion
		#region SlaFilter method
		public ActionResult SlaFilter(string imp, string urg) {
			IEnumerable<Home> _SlaPriorities = Enumerable.Empty<Home>();
			TableViewModel _tempModel;
			Home _SlaFilter = new Home
			{
				SupportCallImpact = imp,
				SupportCallUrgency = urg
			};
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
			_tempModel = new TableViewModel(_SlaPriorities, _SlaFilter);
			HttpContext.Session.SetObject<TableViewModel>("_slafilter", _tempModel);
			return View("Table",_tempModel);
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
		private TableViewModel CheckSlaInput() {
			TableViewModel _temp = HttpContext.Session.GetObjectSingle<TableViewModel>("_slafilter");
			if (_temp != null)
			{
				slaView = true;
				return _temp;
			}
			else
				return HttpContext.Session.GetObjectSingle<TableViewModel>("_viewModel");
		}
		#endregion
		#region Print Pdf
		public IActionResult PrintPdf()
		{
			GraphsViewModel graphsView = new GraphsViewModel();
			try
			{
				_filter = HttpContext.Session.GetObjectSingle<Home>("filter"); ;
				_homesFiltered = HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
				graphsView = HttpContext.Session.GetObjectSingle<GraphsViewModel>("graphsViewModel");
				graphsView.PdfFormat = true;
			}
			catch (Exception e)
			{
				_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");

				ViewData["Selections"] = _possibleChoices;
				ViewData["Error"] = e.Message;
				this.Index();
			}

			return new Rotativa.AspNetCore.ViewAsPdf("Graphs", graphsView) { CustomSwitches = "--no-stop-slow-scripts --window-status ready" }; // --no-pdf-compression
		}
		#endregion

	}



}
