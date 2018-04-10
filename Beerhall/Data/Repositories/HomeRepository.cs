﻿using TrustTeamVersion4.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrustTeamVersion4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Reflection;

namespace TrustTeamVersion4.Data.Repositories
{
	public class HomeRepository : IHomeRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Home> _homes;
		public IEnumerable<Home> HomesFiltered;

		public void SetHomesFiltered(IEnumerable<Home> input)
		{
			this.HomesFiltered = input;
		}


		public HomeRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_homes = dbContext.homes;
			foreach (var home in _homes)
			{
				home.SetDateNotNull();
			}
		}
		// Returnen van alle objecten in de databank in lijst formaat
		public IEnumerable<Home> GetAll()
		{
			return _homes.ToList();
		}

		// Return alle unieke nummers die in de databank zitten
		public List<double?> GetNumbers()
		{
			List<double?> numbers = new List<double?>();
			foreach (Home h in _homes)
			{
				if (!(h.Number == null | numbers.Exists(b => b == h.Number)))
				{
					numbers.Add(h.Number.Value);
				}
			}
			numbers.Sort();
			return numbers;
		}
		// Return alle unieke jaartallen die in de databank zitten
		public List<double?> GetYear()
		{
			List<double?> years = new List<double?>();
			foreach (Home h in _homes)
			{
				if (!(h.Year == null | years.Exists(b => b == h.Year)))
				{
					years.Add(h.Year.Value);
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
			List<string> impacts = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.SupportCallImpact == null | impacts.Exists(b => b == h.SupportCallImpact)))
				{
					impacts.Add(h.SupportCallImpact);
				}
			}
			impacts.Sort();
			return impacts;
		}
		// Return alle unieke urgenties die in de databank zitten
		public List<string> getSupportCallUrgencies()
		{
			List<string> urgencies = new List<string>();
			foreach (Home h in _homes)
			{
				if (!(h.SupportCallUrgency == null | urgencies.Exists(b => b == h.SupportCallUrgency)))
				{
					urgencies.Add(h.SupportCallUrgency);
				}
			}
			urgencies.Sort();
			return urgencies;
		}
		// Return alle unieke statussen die in de databank zitten
		public List<string> getSupportCallStatusses()
		{
			List<string> statusses = new List<string>();
			foreach (Home h in _homes)
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
		public List<string> getSupportCallCategories()
		{
			List<string> categories = new List<string>();
			foreach (Home h in _homes)
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
		// Filter de data op basis van een Home opbject dat werd meegegeven, als een property null is zal deze ook steeds toegevoegd worden.
		public List<Home> Filter(Home home)
		{
			List<Home> filteredHomes = new List<Home>();
			// als er geen geldig home object werd meegegeven wordt de if statement niet uitgevoerd en zal de volledige databank worden teruggeven,
			// niet gefilterd
			if (home != null)
			{
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
							// en niet null is dan wordt de counter met eentje verhoogd
							if (valueAll.Equals(valueFilter) && !(valueFilter.GetType() == typeof(DateTime)) && !(valueFilter.Equals("null")))
							{
								counter++;

							}
						}


					}
					// De controle voor de data zijn apart omdat we hier kijken voor <= en >= en dit is niet mogelijk als we algemeen elke property overlopen
					// Als de datum niet de minimum waarde heeft (standaard ingesteld) en tussen de ingestelde waarden ligt dan zal de counter ook verhogen
					if (!(home.SupportCallOpenDate == DateTime.MinValue) | !(home.SupportCallOpenDateEinde == DateTime.MinValue) )

						if (home.SupportCallOpenDate <= h.SupportCallOpenDate && home.SupportCallOpenDateEinde >= h.SupportCallOpenDate)

							counter++;
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

		//Telt het aantal properties die een waarde toegewezen kregen
		public int GetAmountOfSetProperties(Home home)
		{
			int amount = 0;
			PropertyInfo[] _Properties = home.GetType().GetProperties();

			foreach (var prop in _Properties)
			{
				var value = prop.GetValue(home, null) ?? "null";
				if (!(value.Equals("null") | value.Equals(DateTime.MinValue) | value.Equals(DateTime.MaxValue)))
					amount++;
			}

			return amount;

		}

		public string[,] GetEfficiency(IEnumerable<Home> homes)
		{
			int counter = 0;
			int amount;
			IEnumerable<string> statusses = this.getSupportCallStatusses();
			string[,] result = new string[statusses.Count(), 2];
			foreach (var status in statusses)
			{
				amount = 0;
				foreach (var home in homes)
				{
					if (home.SupportCallStatus == status)
					{
						amount = amount + 1;
					}
				}
				if (amount > 0)
				{
					result[counter, 0] = status;
					result[counter, 1] = amount.ToString();
					counter = counter + 1;
				}
			}

			return result;
		}

		public IEnumerable<IGrouping<string, string>> GetHoursWorkedOnUrgency(IEnumerable<Home> homes)
		{
			foreach (var home in homes)
			{
				if (home.SupportCallUrgency.Equals("null"))
				{
					home.SupportCallUrgency = "None Assigned";
				}
			}
			homes.OrderBy(h => Double.Parse(h.HoursInvoiceCenter));
			IEnumerable<IGrouping<string, string>> grouped = homes.GroupBy(h => h.SupportCallUrgency, h => h.HoursInvoiceCenter);


			return grouped;
		}
	}
}

