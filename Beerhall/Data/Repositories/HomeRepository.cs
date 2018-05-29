using TrustTeamVersion4.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrustTeamVersion4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Reflection;
using System.Text;
using TrustteamVersion4.Models.Extension;
using TrustteamVersion4.Models.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace TrustTeamVersion4.Data.Repositories
{
	public class HomeRepository : IHomeRepository
	{
		#region Properties/variables
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Home> _homes2;
		private List<Home> _homesDB;
		public IEnumerable<Home> HomesFiltered;
		public IEnumerable<Home> _homes;
		public Dictionary<string, List<object>> IndexSelects;
		private readonly Home _home = new Home();
		#endregion
		#region Constructor
		public HomeRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;//_homes2 = dbContext.homes;
			//_homes = new List<Home>() { new Home() };
		}

		#endregion
		#region Initiele filter
		public IEnumerable<Home> InitialFilter(Home home)
		{
			home.CheckAndSetLastMonth();
			if (home.InvoicCenterOrganization.Equals("") | home.SupportCallOpenDate == null)
				throw new Exception("Ongeldige invoer");
			var bedrijf = home.InvoicCenterOrganization;
			string begin = home.SupportCallOpenDate;
			string eind = home.SupportCallOpenDateEinde.ToString("yyyy-MM-dd");
			if (eind == "0001-01-01")
				eind = "2999-01-01";
			IEnumerable<Home> result;
			result = _dbContext.homes.FromSql($"EXECUTE dbo.sp_opvragenRapportering {bedrijf},{begin},{eind}").ToList();
			return result;

		}
		#endregion
		#region DB filter
		// Vervanging van oude filter methode, deze haalt data steeds rechstreeks uit database mbv linq queries.
		public IEnumerable<Home> getFiltered(string[] property, List<string> filter,IEnumerable<Home> initial)
		{
			// Bij de eerste iteratie wordt de eerste initiele hoop data opgehaald, daarna wordt op deze reeds opgevraagde data verder gefilterd.
			// Het is eenvoudiger om over elke property te filteren dan ze allemaal te controleren in 1 linq query omdat we anders met null kwesties zitten
			// die we liever vermijden. 
			// De dates worden wel beide in 1 iteratie gecontroleerd omdat dit anders nog een extra if statement zou moeten zijn voor te kiezen tussen < en >
			List<PropertyInfo> prop = new List<PropertyInfo>();
			List<Home> result = new List<Home>();
			List<Home> resultTemp = new List<Home>();
			bool first = true;
			bool dateDone = false;
			int index = 0;
			int temp = 0;
			bool dateDoneThisLoop = false;
			foreach (var str in property)
			{
				prop.Add(_home.getProperty(str));
			}
			// Deze check is er om ervoor te zorgen dat er niet twee maal geïtereerd wordt voor zowel start als eind datum
			// De index is er zodanig we later de correct waarde kunnen verwijderen (line 106)
			if (prop.Contains(_home.getProperty("SupportCallOpenDate")) && prop.Contains(_home.getProperty("SupportCallOpenDateEinde")))
			{
				temp = prop.IndexOf(_home.getProperty("SupportCallOpenDateEinde"));
				prop.Remove(_home.getProperty("SupportCallOpenDateEinde"));
			}
			// overlopen elke property die een waarde heeft
			foreach (var pr in prop)
			{// Als het de eerste filter is dan moeten we de data nog uit de databank halen. (_homes2)
				if (first)
				{// itereren over databank
					foreach (var item in initial)
					{// als de filter geen datum is is de controle eenvoudig
						if (!(pr.Name.Contains("Date")))
						{// Enkel de bedrijfsnaam mag maar deels overeen komen om te matchen, de overige moeten volledig identiek zijn
							if ((pr.Name.Equals("InvoicCenterOrganization")))
							{
								if (pr.GetValue(item) != null && pr.GetValue(item).ToString().ToLower().Contains(filter[index].ToLower()))
								{
									result.Add(item);
								}
							}
							else
							{
								if (pr.GetValue(item) != null && pr.GetValue(item).ToString().ToLower().Trim().Equals(filter[index].ToLower().Trim()))
								{
									result.Add(item);
								}
							}
						}
						//Als de filter wel een datum is
						else
						{// Als er nog niet gefilterd werd op datum in deze iteratie. Anders zijn de gegevens al verwijderd (zie line 106)
							if (!(dateDone))
							{// Instellen start date op de meegegeven filter. Een start date is sowieso aanwezig, een end date echter niet
								DateTime start = DateTime.Parse(filter[index]);
								DateTime end;
								dateDoneThisLoop = true;
								// Als de filter geen element meer telt na de start datum of ->
								if (filter.Count - 1 > index)
								{ // -> de filter is geen datum, dan wordt dit niet uitgevoerd maar wel dit ->
									if (filter[index + 1].Contains("00:00:00"))
										end = DateTime.Parse(filter[index + 1]);
									else
										// -> het toewijzen van de max value zodanig alle data kleiner zijn dan dit
										end = DateTime.MaxValue;

								}
								else
									end = DateTime.MaxValue;

								DateTime date = DateTime.Parse(item.SupportCallOpenDate);
								DateTime dateEinde = (DateTime.Parse(item.SupportCallClosedDate) == null) ? DateTime.MaxValue : DateTime.Parse(item.SupportCallClosedDate);
								// Controleren of de datum niet null is / groter is dan start / kleiner is dan eind
								if (pr.GetValue(item) != null && DateTime.Compare(date, start) >= 0 && DateTime.Compare(date, end) <= 0)
								{
									result.Add(item);
								}
								else if (dateEinde >= start && dateEinde <= end)
								{
									resultTemp.Add(item);
								}
							}
						}
					}
					// Verwijderen van de datum gegevens in de filter zodanig het verhogen van de index met 1 nog klopt en bools aanpassen
					if (dateDoneThisLoop)
					{
						dateDone = true;
						if (temp != 0)
							filter.RemoveAt(temp);
					}
					first = false;
					index++;
				}
				else
				{// idem hierboven
					foreach (var obj in result)
					{
						if (!(pr.Name.Contains("Date")))
						{
							if ((pr.Name.Equals("InvoicCenterOrganization")))
							{
								if (pr.GetValue(obj) != null && pr.GetValue(obj).ToString().ToLower().Contains(filter[index].ToLower()))
								{
									resultTemp.Add(obj);
								}
							}
							else
							{
								if (pr.GetValue(obj) != null && pr.GetValue(obj).ToString().ToLower().Equals(filter[index].ToLower()))
								{
									resultTemp.Add(obj);
								}
							}
						}
						else
						{
							if (!(dateDone))
							{// Instellen start date op de meegegeven filter. Een start date is sowieso aanwezig, een end date echter niet
								DateTime start = DateTime.Parse(filter[index]);
								DateTime end;
								dateDoneThisLoop = true;
								// Als de filter geen element meer telt na de start datum of ->
								if (filter.Count - 1 > index)
								{ // -> de filter is geen datum, dan wordt dit niet uitgevoerd maar wel dit ->
									if (filter[index + 1].Contains("00:00:00"))
										end = DateTime.Parse(filter[index + 1]);
									else
										// -> het toewijzen van de max value zodanig alle data kleiner zijn dan dit
										end = DateTime.MaxValue;

								}
								else
									end = DateTime.MaxValue;

								DateTime dateBegin = DateTime.Parse(obj.SupportCallOpenDate);
								DateTime dateEinde = (obj.SupportCallClosedDate == null) ? DateTime.MaxValue : DateTime.Parse(obj.SupportCallClosedDate);
								// Controleren of de datum niet null is / groter is dan start / kleiner is dan eind
								if (pr.GetValue(obj) != null && DateTime.Compare(dateBegin, start) >= 0 && DateTime.Compare(dateBegin, end) <= 0)
								{
									resultTemp.Add(obj);
								}
								else if ( dateEinde >= start && dateEinde <= end )
								{
									resultTemp.Add(obj);
								}
							}
						}
					}
					if (dateDoneThisLoop)
					{
						dateDone = true;
						if (temp != 0)
							filter.RemoveAt(temp);
					}
					result = resultTemp;
					resultTemp = new List<Home>();
					index++;
				}
			}

			return result;
		}
		#endregion
		
		#region Get methodes voor de verschillen kolommen
		// Return alle unieke nummers die in de databank zitten
		public List<int> GetNumbers()
		{
			List<int> numbers = new List<int>();
			foreach (Home h in _homes)
			{
				if (!(h.Number == null | numbers.Exists(b => b == h.Number)))
				{
					numbers.Add(h.Number);
				}
			}
			numbers.Sort();
			return numbers;
		}
		// Return alle unieke jaartallen die in de databank zitten
		public List<int> GetYear()
		{
			List<int> years = new List<int>();
			foreach (Home h in _homes)
			{
				if (!(h.Year == null | years.Exists(b => b == h.Year)))
				{
					years.Add(h.Year);
				}
			}
			years.Sort();
			return years;
		}
		// Return alle unieke Organisatie nummers die in de databank zitten
		public List<string> GetOrganizationNumbers()
		{
			List<string> ONumbers = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.OrganizationNumber == null | ONumbers.Exists(b => b == h.OrganizationNumber)))
				{
					ONumbers.Add(h.OrganizationNumber);
				}
			}
			ONumbers.Sort();
			return ONumbers;
		}
		// Return alle unieke Organisatie namen die in de databank zitten
		public List<string> getInvoiceCenterOrganizations()
		{
			List<string> InvoiceOrg = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.InvoicCenterOrganization == null | InvoiceOrg.Exists(b => b == h.InvoicCenterOrganization)))
				{
					InvoiceOrg.Add(h.InvoicCenterOrganization);
				}
			}
			InvoiceOrg.Sort();
			return InvoiceOrg;
		}
		// Return alle unieke groep namen die in de databank zitten
		public List<string> getGroupNames()
		{
			List<string> groups = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.GroupName == null | groups.Exists(b => b == h.GroupName)))
				{
					groups.Add(h.GroupName);
				}
			}
			groups.Sort();
			return groups;
		}
		// Return alle unieke persoons namen die in de databank zitten
		public List<string> getPersonNames()
		{
			List<string> names = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.PersonName == null | names.Exists(b => b == h.PersonName)))
				{
					names.Add(h.PersonName);
				}
			}
			names.Sort();
			return names;
		}
		// Return alle unieke call types  die in de databank zitten
		public List<string> getSupportCallTypes()
		{
			List<string> types = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.SupportCallType == null | types.Exists(b => b == h.SupportCallType)))
				{
					types.Add(h.SupportCallType);
				}
			}
			types.Sort();
			return types;
		}
		// Return alle unieke call prioriteiten  die in de databank zitten
		public List<string> getSupportCallPriorities()
		{
			List<string> priorities = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.SupportCallPriority == null | priorities.Exists(b => b == h.SupportCallPriority)))
				{
					priorities.Add(h.SupportCallPriority);
				}
			}
			priorities.Sort();
			return priorities;
		}
		// Return alle unieke support call impacten die in de databank zitten
		public List<string> getSupportCallImpacts()
		{
			List<string> impacts = new List<string>() { "1 - Organization", "2 - Department", "3 - Employee", "4 - No impact", "Not Set" };

			impacts.Sort();
			return impacts;
		}
		// Return alle unieke urgenties die in de databank zitten
		public List<string> getSupportCallUrgencies()
		{
			List<string> urgencies = new List<string>() { "1 - Unable to Work", "2 - Critical Business Process Unavailable", "3 - Normal Business Process Unavailable", "4 - Incident, but Workaround Available", "5 - Service Request", "Not Set" };

			return urgencies;
		}
		// Return alle unieke statussen die in de databank zitten
		public List<string> getSupportCallStatusses(IEnumerable<Home> homes)
		{
			List<string> statusses = new List<string>();
			foreach (Home h in homes)
			{
				if (!(h.SupportCallStatus == null | statusses.Exists(b => b == h.SupportCallStatus)))
				{
					statusses.Add(h.SupportCallStatus);
				}
			}
			statusses.Sort();
			return statusses;
		}
		// Return alle unieke call categoriën die in de databank zitten
		public List<string> getSupportCallCategories(IEnumerable<Home> homes)
		{
			List<string> categories = new List<string>();
			foreach (Home h in homes)
			{
				if (!(h.SupportCallCategory == null | categories.Exists(b => b == h.SupportCallCategory)))
				{
					categories.Add(h.SupportCallCategory);
				}
			}
			categories.Sort();
			return categories;
		}
		// Return alle unieke users die een ticket openden die in de databank zitten
		public List<string> getOpenedByUsers()
		{
			List<string> users = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.OpenedByUser == null | users.Exists(b => b == h.OpenedByUser)))
				{
					users.Add(h.OpenedByUser);
				}
			}
			users.Sort();
			return users;
		}
		// Return alle unieke users die een ticker werden toegewezen die in de databank zitten
		public List<string> getAssignedtoUsers()
		{
			List<string> users = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.AssignedToUser == null | users.Exists(b => b == h.AssignedToUser)))
				{
					users.Add(h.AssignedToUser);
				}
			}
			users.Sort();
			return users;
		}
		// Return alle unieke queus die in de databank zitten
		public List<string> getAssignedToQueus()
		{
			List<string> queus = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.AssignedToQueue == null | queus.Exists(b => b == h.AssignedToQueue)))
				{
					queus.Add(h.AssignedToQueue);
				}
			}
			queus.Sort();
			return queus;
		}
		// Return alle unieke Statussen die in de databank zitten
		public List<string> getInvoiceStatusses()
		{
			List<string> statusses = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.InvoiceStatus == null | statusses.Exists(b => b == h.InvoiceStatus)))
				{
					statusses.Add(h.InvoiceStatus);
				}
			}
			statusses.Sort();
			return statusses;
		}
		#endregion

		#region Filter method
		// Filter de data op basis van een Home opbject dat werd meegegeven, als een property null is zal deze ook steeds toegevoegd worden.
		public List<Home> Filter(Home home)
		{
			List<Home> filteredHomes = new List<Home>();
			// als er geen geldig home object werd meegegeven wordt de if statement niet uitgevoerd en zal de volledige databank worden teruggeven,
			// niet gefilterd
			if (home != null)
			{
				// Als er gefilterd werd op de laatste maand dan moeten deze variabelen eerst ingesteld worden
				home.CheckAndSetLastMonth();
				//Het verzamelen van alle properties van het object waarop gefilterd wordt
				PropertyInfo[] _PropertyFilter = home.GetType().GetProperties();

				//Overlopen van elk object in de DB om te vergelijken met de filter
				foreach (Home h in _homes)
				{
					// De counter telt hoeveel overeenkomsten er zijn en gebaseerd hierop wordt het object al dan niet toegevoegd aan de verzameling van gefilterde 
					// objecten
					int counter = 0;
					PropertyInfo[] _PropertyAll = h.GetType().GetProperties();
					// Overlopen van alle properties van het object uit de databank
					foreach (var propAll in _PropertyAll)
					{
						//Het het ophalen van de waarde die vasthangt aan de property en als deze null referentie is omzetten naar een string "null"
						var valueAll = propAll.GetValue(h, null) ?? "null";

						// Overlopen van de properties van de filter en deze vergelijken met de property die we momenteel vasthebben van de filter
						foreach (var propFilter in _PropertyFilter)
						{
							var valueFilter = propFilter.GetValue(home, null) ?? "null";
							// Als de values het zelfde zijn of de value van de filter niet van het type DateTime is
							// er moet ook gekeken worden of het over dezelfde property gaat, dit omdat er soms in verschillende properties dezelfde waarda kan
							// zitten. In dit geval zou de counter te veel verhogen en kan dat verkeerde resultaten geven
							// en niet null is dan wordt de counter met eentje verhoogd
							// Ook de list variabele slaan we over, omdat deze standaard empty is maar dus wel niet null is
							if (valueAll.Equals(valueFilter) && !(valueFilter.GetType() == typeof(DateTime)) && !(valueFilter.Equals("null")) && propFilter.Name == propAll.Name && !(valueFilter.Equals(false)))
							{
								counter++;

							}
						}


					}
					// De controle voor de data zijn apart omdat we hier kijken voor <= en >= en dit is niet mogelijk als we algemeen elke property overlopen
					// Als de datum niet de minimum waarde heeft (standaard ingesteld) en tussen de ingestelde waarden ligt dan zal de counter ook verhogen
					if (!(DateTime.Parse(home.SupportCallOpenDate) == DateTime.MinValue) | !(home.SupportCallOpenDateEinde == DateTime.MinValue))
					{

						if (DateTime.Parse(home.SupportCallOpenDate) <= DateTime.Parse(h.SupportCallOpenDate))
							counter++;
						if (home.SupportCallOpenDateEinde >= DateTime.Parse(h.SupportCallOpenDate))
							counter++;
						if (home.LastMonth == true)
							counter++;
					}
					// Als de counter identiek is aan het aantal properties dat werd ingesteld in de filter dan wordt deze toegevoegd aan de lijst van
					// gefilterde objecten
					if (counter == GetAmountOfSetProperties(home))
						filteredHomes.Add(h);


				}
			}
			// Indien er één of meer objecten voldeden aan de waarden worden deze geretourneerd. Anders zal de volledige lijst worden
			// teruggegeven
			if (!(filteredHomes == null))
			{
				return filteredHomes;
			}
			else
			{
				return _homes.ToList();
			}
		}
		#endregion

		#region Other methods

		//Telt het aantal properties die een waarde toegewezen kregen
		// De value mag niet gelijk zijn aan false omdat anders boolean properties altijd worden geteld
		public int GetAmountOfSetProperties(Home home)
		{
			int amount = 0;
			PropertyInfo[] _Properties = home.GetType().GetProperties();

			foreach (var prop in _Properties)
			{
				var value = prop.GetValue(home, null) ?? "null";
				if (!(value.Equals("null") | value.Equals(DateTime.MinValue) | value.Equals(DateTime.MaxValue) | value.Equals(false)))
					amount++;
			}

			return amount;

		}

		public void SetHomesFiltered(IEnumerable<Home> input)
		{
			this.HomesFiltered = input;
		}

		// Returnen van alle objecten in de databank in lijst formaat
		public IEnumerable<Home> GetAll()
		{
			return _homes.ToList();
		}

		// Het veranderen van waarden die misschien een null referentie hebben naar een string die "NULL" zegt.
		public IEnumerable<Home> RemoveNull(IEnumerable<Home> homes)
		{
			foreach (var h in homes)
			{
				h.RemoveNull();
			}

			return homes;

		}



		#endregion

		#region Methods for the graphs page
		// De methode die de efficiency grafiek opstelt (taartdiagram)
		// De array is : [[Closed,12][Open,4]...]
		public string[,] GetEfficiency(IEnumerable<Home> homes)
		{ // We tellen het aantal tickets dat er zijn per soort status. Counter telt de hoeveelste status we aan het overlopen zijn
		  // Amount overloopt hoeveel tickets er zijn binnen deze status
			int counter = 0;
			int amount;
			IEnumerable<string> statusses = this.getSupportCallStatusses(homes);
			string[,] result = new string[statusses.Count(), 2];
			foreach (var status in statusses)
			{
				// Dit zou waarschijnlijk efficienter zijn met een LINQ querie. Maar momenteel geen prioriteit.
				amount = 0;
				foreach (var home in homes)
				{
					if (home.SupportCallStatus == status)
					{
						amount = amount + 1;
					}
				}
				// Enkel toevoegen aan de resultset als er wel degelijk tickets in deze categorie voorkwamen.
				if (amount > 0)
				{
					result[counter, 0] = status;
					result[counter, 1] = amount.ToString();
					counter = counter + 1;
				}
			}

			return result;
		}

		//Het verzamelen van het aantal issues die thuis horen in elke interventie categorie.
		public int[,] GetIncidentenTable(IEnumerable<Home> homes)
		{
			int amountImpact = this.getSupportCallImpacts().Count();
			int amountUrgency = this.getSupportCallUrgencies().Count();
			// Table :
			// Impact | IncidentNiv1   | IncidentNiv2   | IncidentNiv3   | IncidentNiv4   |...
			// Impact1|Interventiecat.1|Interventiecat.2|Interventiecat.3|Interventiecat.4|...              
			// Impact2|Interventiecat.2|Interventiecat.3|Interventiecat.4|Interventiecat.4|...
			// Impact3|Interventiecat.3|Interventiecat.4|Interventiecat.4|Interventiecat.5|...
			// Impact4|Interventiecat.x|Interventiecat.x|Interventiecat.x|Interventiecat.x|...
			// Dit komt dus overeen met de array, zo zullen table[0,0] ,table[0,*] en table [*,0] dus leeg blijven
			// IncidentNiv komt overeen met SupportCallUrgency, impact met SupportCallImpact en Interventiecat. met SupportCallPriority

			int[,] table = new int[amountImpact + 1, amountUrgency + 1];

			foreach (var h in homes)
			{
				h.RemoveNull();
				for (var i = 1; i <= amountImpact; i++)
				{
					if (h.SupportCallImpact.StartsWith(i.ToString()))
					{
						for (var j = 1; j <= amountUrgency; j++)
						{
							if (h.SupportCallUrgency.StartsWith(j.ToString()))
							{
								table[i, j]++;
							}
							if (h.SupportCallUrgency.StartsWith("N") & j == amountUrgency)
							{
								table[i, j]++;
							}
						}
					}
					if (h.SupportCallImpact.StartsWith("N") & i == amountImpact)
					{
						for (var j = 1; j <= amountUrgency; j++)
						{
							if (h.SupportCallUrgency.StartsWith(j.ToString()))
							{
								table[i, j]++;
							}
							if (h.SupportCallUrgency.StartsWith("N") & j == amountUrgency)
							{
								table[i, j]++;
							}
						}
					}
				}

			}

			return table;
		}
		// Telt de frequentie van elke categorie in een verzameling van objecten
		public int[] GetCategoryCount(IEnumerable<Home> homes, List<string> cat)
		{
			List<string> categories = cat;
			int amountOfCategories = cat.Count();
			int[] amounts = new int[amountOfCategories];

			categories.Sort();


			foreach (var h in homes)
			{
				foreach (var c in categories)
				{
					int index = categories.IndexOf(c);
					if (h.SupportCallCategory == c)
						amounts[index]++;
				}
			}
			return amounts;
		}
		// Hier wordt gebruik gemaakt van een nieuwe klasse 'MultiKeyDictionary'. Dit is een Dictionary die werkt met 2 keys.
		// Eerst geprobeerd met 3D Array en list<list<list>>> maar waren op een bepaald moment niet geschikt voor gebruik. Arrays 
		// gaven problemen door hun fixed length waardoor ze zeer veel white space hebben en de lists gaven problemen bij het itereren.
		// Hier is de eerste key telkens de groep en de tweede de persoon. De value is dan het aantal tickets dat geteld wordt bij die persoon.
		//
		public MultiKeyDictionary<string, string, int> GetPersonsPerGroup(IEnumerable<Home> homes, List<string> gr, List<string> na)
		{
			IEnumerable<Home> Filtered = homes;
			Filtered.ForEach(h => h.RemoveNull());
			List<string> groups = gr;
			groups.Add("");
			List<string> names = na;
			names.Add("");
			var result = new MultiKeyDictionary<string, string, int>();
			var newList = Filtered.GroupBy(x => new { x.GroupName, x.PersonName });

			foreach (var y in newList)
			{
				if (y.ToList().Count > 0)
					result.Add(y.Key.GroupName, y.Key.PersonName, y.ToList().Count);
			}


			return result;
		}
		// Geeft de unieke groepen terug voor de meegegeven verzameling homes
		public List<string> GetUniqueGroups(IEnumerable<Home> homes)
		{


			List<string> uniques = new List<string>();
			foreach (Home h in homes)
			{
				if (!(h.GroupName == null | uniques.Exists(b => b == h.GroupName)))
				{
					uniques.Add(h.GroupName);
				}
			}
			uniques.Sort();
			return uniques;

		}
		// Geeft de unieke PersonNames terug voor de meegegeven verzameling homes
		public List<string> GetUniquePersonNames(IEnumerable<Home> homes)
		{


			List<string> names = new List<string>();
			foreach (Home n in homes)
			{
				if (!(n.PersonName == null | names.Exists(b => b == n.PersonName)))
				{
					names.Add(n.PersonName);
				}
			}
			names.Sort();
			return names;

		}
		#endregion

	}
}

