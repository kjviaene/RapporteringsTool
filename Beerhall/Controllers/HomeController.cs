
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
using System.Globalization;
using System.Data.SqlTypes;
//using Rotativa.NetCore.Options;

namespace TrustTeamVersion4.Controllers
{
	public class HomeController : Controller
	{
		#region Properties/variables
		private readonly IHomeRepository _homeRepository;
		private IHostingEnvironment _hostingEnvironment;
		private static String chosenFilter;
		private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		private bool slaView = false;
		Home _home = new Home();
		Home _filter = new Home();
		TableViewModel _viewModel;
		GraphsViewModel graphsView;
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
			Home returnView;
			//slaView op vals zetten zodanig de juiste data wordt getoond als er gesorteerd/verborgen wordt.
			slaView = false;
			// Dit dient voor het tonen van de correcte textfields als de user wil filteren op reeds gefilterde data.
			returnView = HttpContext.Session.GetObjectSingle<Home>("filter");
			if (returnView == null)
				returnView = _home;
			return View("Index", returnView);
		}
		#endregion
		#region Table method
		// Wordt geladen als er gekozen is op wat men wil filteren plus ook als er gesorteerd wil worden eenmaal de tabel geladen is, de string sortOrder is optioneel
		// Indien er niets gekozen werd wordt de lijst gewoon ongesorteerd weergegeven
		public IActionResult Table(Home filter, string sortOrder)
		{
			string tempString = "";
			slaView = false;
			_homesFiltered = (IEnumerable<Home>)HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			_viewModel = HttpContext.Session.GetObjectSingle<TableViewModel>("_viewModel");
			try
			{
				// Als de filter niet null is uitvoeren, maw enkel de eerste maal dat data wordt geladen of indien er opnieuw naar de index werd gegaan
				// om opnieuw te filteren.
				// Dit zal dus nooit activeren als men bv wenst te sorteren met reeds eerdere gefilterde data
				if (!(filter.IsEmptyObject()) | _homesFiltered == null)
				{
					Home filterTemp = HttpContext.Session.GetObjectSingle<Home>("filter");
					filterTemp = filterTemp == null ? new Home() : filterTemp;
					if (filterTemp.FirstFilter)
						filter.FirstFilter = true;
					if (filter.FirstFilter == false)
					{
						filter.CheckAndSetLastMonth();
						if (filter.InvoicCenterOrganization == null | (filter.SupportCallOpenDate == null && filter.LastMonth == false))
						{
							ViewData["Error"] = "Gelieve zowel een bedrijfsnaam als een start datum in te vullen bij uw eerste selectie.";
							return View("Index", _home);
						}
						else if (DateTime.Parse(filter.SupportCallOpenDate) < DateTime.Today.AddYears(-1))
						{
							ViewData["Error"] = "Gelieve een datum te selecteren die minder dan een jaar geleden is.";
							return View("Index", _home);
						}
						_homesFiltered = _homeRepository.InitialFilter(filter);
						filter.FirstFilter = true;
						//Bijhouden van de filter
						HttpContext.Session.SetObject<Home>("filter", filter);

						// Bijhouden van gefilterde Home objecten
						HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);

						_viewModel = new TableViewModel(_homesFiltered, filter);
						if (_viewModel.Data.Count > 0)
						{
							_viewModel.FilterString = GetCorrectFilterString(_viewModel, graphsView, filter.ToString());
							HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
							return View(_viewModel);
						}
						else
						{
							filter.FirstFilter = false;
							HttpContext.Session.SetObject<Home>("filter", filter);
						}
					}

					// Het filteren van de data adhv de meegeven filter.
					//_homesFiltered = _homeRepository.Filter(filter);
					List<List<string>> setProps = this.GetSetPropertiesAsString(filter);
					_homesFiltered = _homeRepository.getFiltered(setProps[0].ToArray(), setProps[1], _homesFiltered);
					// Het opslaan van de filter zodanig dit kan weergegeven worden boven de data
					//chosenFilter = filter.ToString();

					// Het doorgeven van de gekozen filter als string aan de view via een ViewBag (zodat dit kan weergegeven worden)
					//ViewBag.filter = chosenFilter;
					if (_homesFiltered.Count() > 0)
					{

						//Bijhouden van de filter
						HttpContext.Session.SetObject<Home>("filter", filter);

						// Bijhouden van gefilterde Home objecten
						HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
					}
				}
				// Controleren of er al een viewModel bestaat, indien dit niet zo is maken we een nieuw aan gebaseeerd op de gefilterde data.
				if (_viewModel == null)
				{
					_viewModel = new TableViewModel(_homesFiltered, _filter);
					HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
				}
				// Controleren of de data in het viewmodel gelijk is aan de net gefilterde data. Anders werden er soms verkeerde resultaten getoond. Dit bijvoorbeeld
				// Als er verkeerde gegevens werden ingegeven, dan werd de table gewoon getoond met de gegevens van de vorige search ipv een foutmelding te geven
				else if (!(_homesFiltered.SequenceEqual(_viewModel.Data)))
				{
					_viewModel = new TableViewModel(_homesFiltered, filter);
					HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
				}
				//Kijken of de search resultaten opgaf en een gepaste reactie geven indien dit niet het geval was.
				if (CheckInput(filter))
				{
					_filter = HttpContext.Session.GetObjectSingle<Home>("filter");
					if (_filter == null)
					{
						_filter = new Home();
					}
					return View("Index", _filter);
				}
			}
			catch (Exception e)
			{
				ViewData["Error"] = "Er werden geen resultaten gevonden voor deze filter." + e.Message;
				RedirectToActionPermanent("Index");
			}
			if (_viewModel == null)
				_viewModel = new TableViewModel(_EmptyEnumerable, _home);

			graphsView = HttpContext.Session.GetObjectSingle<GraphsViewModel>("graphsView");
			if (filter == null && _home == null)
			{
				tempString = "";
			}
			else if (filter == null && _home != null)
			{
				tempString = _home.ToString();
			}
			else
			{
				tempString = filter.ToString();
			}
			_viewModel.FilterString = GetCorrectFilterString(_viewModel, graphsView, tempString);
			HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
			return View(_viewModel);
		}
		#endregion
		#region Sorteren Partial

		// De methode die zorgt voor het sorteren van de tabel. 
		//Deze retourneert een PartialView (_TableView.cshtml) en het Javascript tableSorter.js zorgt ervoor dat dit correct wordt weergegeven in de omringende view
		// De sorter parameter bevat de naam van een property (zie de view)
		public PartialViewResult SortPartial(string sorter)
		{
			try
			{
				// De _viewModel variabele vullen met de correct gegevens, indien iemand vanuit de SLA tabel komt moet deze data dus correct zijn
				// Anders wordt bij sorteren verkeerde data teruggegeven
				//Eenmaal de data is geladen wordt deze gesorteerd en dan weggeschreven naar de session. slaView is true eenmaal de CheckSlaInput de _slafilter teruggaf.
				//Als de _viewModel werd teruggegeven blijft deze false en zo wordt dus steeds de juiste variabele opgeslaan.
				_viewModel = this.CheckSlaInput();
				_viewModel.Data = _viewModel.SortBy(_viewModel.Data, sorter).ToList<Home>();
				if (slaView == true)
					HttpContext.Session.SetObject<TableViewModel>("_slafilter", _viewModel);
				else
					HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);
			}
			// Indien de sessie verloopt wordt hier een exception gevangen en worden de correct variabelen gezet.
			catch (Exception e)
			{
				ViewData["Selections"] = _possibleChoices;
				ViewData["Error"] = "Er ging iets mis, mogelijks is uw sessie verlopen" + e.Message;
				this.Index();
			}
			if (_viewModel == null)
				_viewModel = new TableViewModel(_EmptyEnumerable, _home);
			return PartialView("_TableView", _viewModel);
		}
		#endregion
		#region Closing columns
		// Zeer gelijk lopend aan de SortPartial.
		// De closed parameter wordt ook meegegeven uit de view en bevat ook de naam van een property
		public PartialViewResult CloseColumnPartial(string closed)
		{
			try
			{
				// Om te onthouden welke kolommen er gesloten zijn wordt gebruik gemaakt van de ClosedColumns variabele in TableViewModel
				_viewModel = this.CheckSlaInput();
				_viewModel.ClosedColumns.Add(closed);
				// controleren of we een SLAview tonen of niet
				if (slaView == true)

					HttpContext.Session.SetObject<TableViewModel>("_slafilter", _viewModel);
				else
					HttpContext.Session.SetObject<TableViewModel>("_viewModel", _viewModel);

			}
			catch (Exception e)
			{
				ViewData["Selections"] = _possibleChoices;
				ViewData["Error"] = "Er ging iets mis, mogelijks is uw sessie verlopen" + e.Message;
				this.Index();
			}
			if (_viewModel == null)
				_viewModel = new TableViewModel(_EmptyEnumerable, _home);
			return PartialView("_TableView", _viewModel);
		}
		#endregion
		#region Graphs method
		// Wordt geladen als er gekozen is op wat men wil filteren plus ook als er gesorteerd wil worden eenmaal de tabel geladen is, de string sortOrder is optioneel
		// Indien er niets gekozen werd wordt de lijst gewoon ongesorteerd weergegeven
		public IActionResult Graphs(Home filter)
		{

			_homesFiltered = (IEnumerable<Home>)HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			graphsView = HttpContext.Session.GetObjectSingle<GraphsViewModel>("graphsView");
			_viewModel = HttpContext.Session.GetObjectSingle<TableViewModel>("_viewModel");
			string tempString = "";

			//if (_viewModel != null & graphsView != null)
			//	graphsView.filterAsString = _viewModel.FilterString;
			try
			{
				// Als de filter niet null is uitvoeren, maw enkel de eerste maal dat data wordt geladen of indien er opnieuw naar de index werd gegaan
				// om opnieuw te filteren.
				// Dit zal dus nooit activeren als men bv wenst te sorteren met reeds eerdere gefilterde data
				if (!(filter.IsEmptyObject()) | _homesFiltered == null)
				{
					Home filterTemp = HttpContext.Session.GetObjectSingle<Home>("filter");
					filterTemp = filterTemp == null ? new Home() : filterTemp;
					if (filterTemp.FirstFilter)
						filter.FirstFilter = true;
					if (filter.FirstFilter == false)
					{
						filter.CheckAndSetLastMonth();
						if (filter.InvoicCenterOrganization == null | (filter.SupportCallOpenDate == null && filter.LastMonth == false))
						{
							ViewData["Error"] = "Gelieve zowel een bedrijfsnaam als een start datum in te vullen bij uw eerste selectie.";
							return View("Index", _home);
						}
						else if (DateTime.Parse(filter.SupportCallOpenDate) < DateTime.Today.AddYears(-1))
						{
							ViewData["Error"] = "Gelieve een datum te selecteren die minder dan een jaar geleden is.";
							return View("Index", _home);
						}
						_homesFiltered = _homeRepository.InitialFilter(filter);
						filter.FirstFilter = true;
						//Bijhouden van de filter
						HttpContext.Session.SetObject<Home>("filter", filter);

						// Bijhouden van gefilterde Home objecten
						HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);

						graphsView = new GraphsViewModel(_homeRepository, _homesFiltered, filter);
						if (_homesFiltered.Count() > 0)
						{
							graphsView.filterAsString = GetCorrectFilterString(_viewModel, graphsView, filter.ToString());
							HttpContext.Session.SetObject<GraphsViewModel>("graphsView", graphsView);
							return View(graphsView);
						}
						else
						{
							filter.FirstFilter = false;
							HttpContext.Session.SetObject<Home>("filter", filter);
						}
					}

					// Het filteren van de data adhv de meegeven filter.
					//_homesFiltered = _homeRepository.Filter(filter);
					List<List<string>> setProps = this.GetSetPropertiesAsString(filter);
					_homesFiltered = _homeRepository.getFiltered(setProps[0].ToArray(), setProps[1], _homesFiltered);


					if (_homesFiltered.Count() > 0)
					{

						//Bijhouden van de filter
						HttpContext.Session.SetObject<Home>("filter", filter);

						// Bijhouden van gefilterde Home objecten
						HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
						// Aanmaken nieuw viewmodel
						graphsView = new GraphsViewModel(_homeRepository, _homesFiltered, filter);
						HttpContext.Session.SetObject<GraphsViewModel>("graphsview", graphsView);
					}
				}
				// Controleren of er al een viewModel bestaat, indien dit niet zo is maken we een nieuw aan gebaseeerd op de gefilterde data.
				if (graphsView == null)
				{
					filter = HttpContext.Session.GetObjectSingle<Home>("filter");
					graphsView = new GraphsViewModel(_homeRepository, _homesFiltered, filter);
					HttpContext.Session.SetObject<GraphsViewModel>("graphsView", graphsView);
				}
				//Kijken of de search resultaten opgaf en een gepaste reactie geven indien dit niet het geval was.
				if (CheckInput(filter))
				{
					_filter = HttpContext.Session.GetObjectSingle<Home>("filter");
					if (_filter == null)
					{
						_filter = new Home();
					}
					return View("Index", _filter);
				}
			}
			catch (Exception e)
			{
				ViewData["Error"] = "Er werden geen resultaten gevonden voor deze filter." + e.Message;
				RedirectToActionPermanent("Index");
			}
			if (graphsView == null)
				graphsView = new GraphsViewModel(_homeRepository, _EmptyEnumerable, _home);
			_home = HttpContext.Session.GetObjectSingle<Home>("filter");
			if (filter == null && _home == null)
			{
				tempString = "";
			}
			else if (filter == null && _home != null)
			{
				tempString = _home.ToString();
			}
			else
			{
				tempString = filter.ToString();
			}
			graphsView.filterAsString = GetCorrectFilterString(_viewModel, graphsView, tempString);
			HttpContext.Session.SetObject<GraphsViewModel>("graphsView", graphsView);
			return View(graphsView);
		}
		#endregion
		#region Reset method
		// De reset functie zorgt niet alleen voor het resetten van de dropdowns maar ook voor het leeg maken van de Session. Als er niet gereset wordt,
		// kan de gebruiker steeds terugkeren naar graphs en table en de vorige data terug zien (tot hij opnieuw een filter heeft geselecteerd uiteraard)
		public IActionResult Reset()
		{
			//Ophalen van de mogelijke keuzes en leegmaken van de Session.
			_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");
			_homesFiltered = _EmptyEnumerable;
			HttpContext.Session.SetObject<IEnumerable<Home>>("_homesFiltered", _homesFiltered);
			HttpContext.Session.SetObject<Home>("filter", _home);
			HttpContext.Session.SetObject<TableViewModel>("_viewModel", new TableViewModel());
			HttpContext.Session.SetObject<GraphsViewModel>("graphsView", new GraphsViewModel());
			ViewData["Selections"] = _possibleChoices;
			return View("Index", _home);

		}

		#endregion
		#region SlaFilter method
		// De methode die er voor zorgt dat men specifieke tickets kan bekijken uit de tabel. Maar dat de origineel gefilterde data ook nog beschikbaar
		// blijft. Na bijvoorbeeld de pagina te vernieuwen.
		// De twee parameters bepalen welke SLA er geselecteerd werd.
		public ActionResult SlaFilter(string imp, string urg)
		{
			IEnumerable<Home> _SlaPriorities = Enumerable.Empty<Home>();
			TableViewModel _tempModel;
			Home _SlaFilter = new Home
			{
				SupportCallImpact = imp,
				SupportCallUrgency = urg
			};
			// string Not Set terug veranderen NULL zodat deze matched met de werkelijke waarde van NULL objecten
			if (imp == "Not Set")
				imp = "NULL";
			if (urg == "Not Set")
				urg = "NULL";
			ViewBag.filter = _SlaFilter.ToString();
			try
			{
				//Kijken of _homesfiltered null is, indien ja, dan uit sessie halen, is het nog steeds null, dan halen we alle objecten op

				_homesFiltered = HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
				if (CheckInputMany(_homesFiltered))
				{
					_homesFiltered = _homeRepository.GetAll();
				}
				// Verwijderen van alle mogelijke null referenties in de dataset
				_homeRepository.RemoveNull(_homesFiltered);
				//Het filteren van de opgehaalde data op basis van de impact en urgenties die we meekregen.
				_SlaPriorities = _homesFiltered.Where(h => h.SupportCallImpact.Equals(imp) && h.SupportCallUrgency.Equals(urg));

				//Aanmaken van een TableViewModel voor weergave en opslaan in Session. De reden waarom staat hierboven uitgelegd bij sorteer en close methoden.
				_tempModel = new TableViewModel(_SlaPriorities, _SlaFilter);
				HttpContext.Session.SetObject<TableViewModel>("_slafilter", _tempModel);
				return View("Table", _tempModel);
			}
			catch (Exception e)
			{
				ViewData["Selections"] = _possibleChoices;
				ViewData["Error"] = "Er ging iets mis, mogelijks is uw sessie verlopen" + e.Message;
				return View("Index");
			}
		}
		#endregion
		#region Export Excel
		// Methode die wordt opgeropen bij het klikken van het excel icoon
		public IActionResult Export()
		{
			// Het omzetten vna de IEnumerable van de gefilterde data naar een DataTable
			_homesFiltered = (IEnumerable<Home>)HttpContext.Session.GetObject<IEnumerable<Home>>("_homesFiltered");
			var dataTable = new DataTable("data");
			dataTable = _homesFiltered.ToDataTable<Home>();

			//Het aanmaken van een Excelbestand en daarin de data toevoegen
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Sheet1");
				worksheet.Cells["A1"].LoadFromDataTable(dataTable, PrintHeaders: true);
				for (var col = 1; col < dataTable.Columns.Count + 1; col++)
				{
					worksheet.Column(col).AutoFit();
				}
				//het returnen van de File naar de View
				return File(package.GetAsByteArray(), XlsxContentType, "report.xlsx");
			}
		}
		#endregion
		#region methods

		public string GetCorrectFilterString(TableViewModel table, GraphsViewModel graph, string filter)
		{
			if (table == null)
				table = new TableViewModel();
			if (graph == null)
				graph = new GraphsViewModel();
			string result = "";
			string tableString = table.FilterString == null ? "" : table.FilterString;
			string graphString = graph.filterAsString == null ? "" : graph.filterAsString;
			string filterString = filter == null ? "" : filter;
			
			if (((tableString.Equals(filterString)) != (graphString.Equals(filterString))))
			{
				if (tableString.Equals(filterString))
				{ result = graphString + " " + filterString; }
				else if (graphString.Equals(filterString))
				{ result = tableString + " " + filterString; }
			}
			else if((!(tableString.Contains(filterString)) && !(graphString.Contains(filterString))))
			{
				if (tableString.Count() > graphString.Count())
					result = result + tableString + " " + filterString;
				else if (graphString.Count() > tableString.Count())
					result = result + graphString + " " + filterString;
				else
					result = result + tableString + " " + filterString; //beide zijn even lang en bevatten F nog niet
			}
			else if (tableString.Count() > graphString.Count())
				result = tableString;
			else if (graphString.Count() > tableString.Count())
				result = graphString;
			else
				result = tableString; // table en graph bevatten zelfde waarde
			return result.Trim();


		}
		// Geeft de ingestelde properties van een object terug als string value. Dit in combinatie met de naam van de property ook als string
		public List<List<string>> GetSetPropertiesAsString(Home home)
		{
			PropertyInfo[] _temp = home.GetProperties();
			List<string> props = new List<string>();
			List<string> values = new List<string>();
			foreach (var pr in _temp)
			{
				if (pr.GetValue(home) != null)
				{// Als de property een gewone string is keert de string gewoon terug
					if (pr.PropertyType == typeof(string) && !(pr.GetValue(home).Equals("")))
					{
						props.Add(pr.Name);
						values.Add(pr.GetValue(home).ToString());
					}
					//  Bij een double of double? wordt de waarde ook omgezet naar string
					// aangezien sommige doubles null kunnen zijn en sommige nooit null zijn is een extra check vereist
					if (pr.PropertyType == typeof(double) | pr.PropertyType == typeof(double?) | pr.PropertyType == typeof(int) | pr.PropertyType == typeof(decimal) && (!(pr.GetValue(home).Equals(0.0)) && !(pr.GetValue(home).Equals(0)) && !(pr.GetValue(home).Equals(Decimal.Zero))))
					{
						props.Add(pr.Name);
						values.Add(pr.GetValue(home).ToString());
					}
					// De Underlyingsystemtype omdat er anders errors worden getoond als hier een nullable wordt vergeleken met bool.
					// De enigste bool is LastMonth en als deze op waar staat worden dus de nodige aanpassingen doorgevoerd
					if (pr.PropertyType.UnderlyingSystemType == typeof(bool) && !(pr.GetValue(home).Equals(false)))
					{
						home.CheckAndSetLastMonth();
					}
					// Als we bij de dates zijn moeten deze correct worden weergegeven waarvoor dit proces. De dates mogen echter niet
					// gelijk zijn aan de max of min values van DateTime
					if (pr.PropertyType == typeof(DateTime) | pr.PropertyType == typeof(DateTime?) && !(pr.GetValue(home).Equals(DateTime.MaxValue)) && !(pr.GetValue(home).Equals(DateTime.MinValue)))
					{
						props.Add(pr.Name);
						string temp = pr.GetValue(home).ToString();
						DateTime tempD = DateTime.Parse(temp);
						values.Add(tempD.ToString("yyyy-MM-dd HH:mm:ss"));
					}
				}
			}
			return new List<List<string>>() { props, values };
		}
		// Kijken of er resultaten werden gevonden op basis van de filter
		public bool CheckInput(Home home)
		{
			if (_homesFiltered.Count() == 0)
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
		// Kijken of de meegegeven IEnumerable instanties bevat of niet
		public bool CheckInputMany(IEnumerable<Home> homes)
		{
			if (homes.Count() == 0)
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
			string[] alphabet = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
			for (int i = 0; i <= amount; i++)
			{
				value.Append(pre + alphabet[i - counter2]);
				if (i == alphabet.Count() - 1)
				{
					pre = alphabet[counter];
					counter++;
					counter2 = counter2 + alphabet.Count();
				}
			}
			return alphabet;
		}
		// controleren of er een SLA wordt bekenen (of het in de sessie zit), Indien dit het geval is zullen we deze weergeven en slaView op true zetten
		// Is deze echter null, dan wil dat zeggen dat alle gefilterde data moet weergegeven worden en wordt dus de standaard _viewModel weergegeven.
		private TableViewModel CheckSlaInput()
		{
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
		//Het afprinten van een pdf gebeurd door deze methode
		public IActionResult PrintPdf()
		{
			GraphsViewModel graphsView = new GraphsViewModel();
			// Als de sessie verlopen is en de gebruiker wenst een pdf bekijken dan wordt een nullpointer gegeven, vandaar de error catching
			try
			{// Ophalen nodige data uit Sessie en de bool PdfFormat op true plaatsen zodanig bepaalde zaken weg gelaten worden in de pdf (header,footer,...)
				graphsView = HttpContext.Session.GetObjectSingle<GraphsViewModel>("graphsView");
				graphsView.PdfFormat = true;
			}
			catch (Exception e)
			{// Indien de sessie is verlopen wordt de gebruiker terug gestuurd naar de index pagina met een gepaste melding
				_possibleChoices = (Dictionary<string, List<object>>)HttpContext.Session.GetObjectDict<Dictionary<string, List<object>>>("_possibleChoices");

				ViewData["Selections"] = _possibleChoices;
				ViewData["Error"] = e.Message;
				this.Index();
			}
			// De pdf wordt gerenderd adhv Rotativa (een implementatie tool voor wkhtmltopdf. Sinds .net core2.0 doet wkhtmltopdf echter soms wat raar
			// De functie zal soms werken en soms niet, een duidelijke oplossing hiervoor is er nog niet.
			// CustomSwitches zijn opties die worden meegegeven aan Rotativa. No stop slow scripts zorgt ervoor dat scripts niet worden onderbroken
			// windows-status ready zorgt ervoor dat er gewacht wordt tot de Window een status heeft gelijk aan ready
			// dit wordt geset in de javascript in de view.
			return new Rotativa.AspNetCore.ViewAsPdf("Graphs", graphsView) { CustomSwitches = "--no-stop-slow-scripts --window-status ready" }; // --no-pdf-compression
		}
		#endregion

	}



}
