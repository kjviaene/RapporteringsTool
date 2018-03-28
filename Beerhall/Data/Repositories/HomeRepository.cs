using TrustTeamVersion4.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrustTeamVersion4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrustTeamVersion4.Data.Repositories
{
	public class HomeRepository : IHomeRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly DbSet<Home> _homes;


		public HomeRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			_homes = dbContext.homes;
		}

		public IEnumerable<Home> GetBy(double number)
		{
			return _homes.Where(b => b.Number == number);
		}

		public IEnumerable<Home> GetAll()
		{
			// return _homes.Include(b => b.InvoiceStatus).Include(b => b.Beers).ToList();
			return _homes.ToList();
		}


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
		public List<Home> Filter(Home home)
		{
			List<Home> filteredHomes = null;
			foreach (Home h in _homes)
			{
				if (!(home.Month.HasValue) | home.Month.Equals(h.Month))
				{
					if (!(home.Number.HasValue) | home.Number.Equals(h.Number))
					{
						if (!(home.Year.HasValue) | home.Year.Equals(h.Year))
						{
							if (string.IsNullOrEmpty(home.OrganizationNumber) | home.OrganizationNumber.Equals(h.OrganizationNumber))
							{
								if (string.IsNullOrEmpty(home.GroupName) | home.GroupName.Equals(h.GroupName))
								{
									if (string.IsNullOrEmpty(home.PersonName) | home.PersonName.Equals(h.PersonName))
									{
										if (string.IsNullOrEmpty(home.SupportCallType) | home.SupportCallType.Equals(h.SupportCallType))
										{
											if (string.IsNullOrEmpty(home.SupportCallPriority) | home.SupportCallPriority.Equals(h.SupportCallPriority))
											{
												if (string.IsNullOrEmpty(home.SupportCallImpact) | home.SupportCallImpact.Equals(h.SupportCallImpact))
												{
													if (string.IsNullOrEmpty(home.SupportCallUrgency) | home.SupportCallUrgency.Equals(h.SupportCallUrgency))
													{
														if (string.IsNullOrEmpty(home.SupportCallStatus) | home.SupportCallStatus.Equals(h.SupportCallStatus))
														{
															if (string.IsNullOrEmpty(home.OpenedByUser) | home.OpenedByUser.Equals(h.OpenedByUser))
															{
																if (string.IsNullOrEmpty(home.AssignedToUser) | home.AssignedToUser.Equals(h.AssignedToUser))
																{
																	if (string.IsNullOrEmpty(home.AssignedToQueue) | home.AssignedToQueue.Equals(h.AssignedToQueue))
																	{
																		if (string.IsNullOrEmpty(home.InvoiceStatus) | home.InvoiceStatus.Equals(h.InvoiceStatus))
																		{
																			filteredHomes.Add(h);
																		}
																	}
																}
															}
														}

													}
												}
											}
										}
									}
								}
							}
						}
					}

				}
			}
			if (!(filteredHomes == null) | filteredHomes.Any())
			{
				return filteredHomes;
			}
			else
			{
				return _homes.ToList();
			}
		}
	}
}

