using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustTeamVersion4.Data.Repositories;
using TrustTeamVersion4.Models.Domain;

namespace TrustteamVersion4.Models.ViewModels
{
	public class IndexViewModel
	{
		public SelectList TicketNumbers { get; set; }
		public SelectList Years { get; set; }
		public SelectList OrganizationNumbers { get; set; }
		public SelectList InvoiceOrg { get; set; }
		public SelectList GroupNames { get; set; }
		public SelectList PersonNames { get; set; }
		public SelectList SupportCallTypes { get; set; }
		public SelectList SupportCallPriorities { get; set; }
		public SelectList SupportCallImpacts { get; set; }
		public SelectList SupportCallUrgencies { get; set; }
		public SelectList SupportCallstatusses { get; set; }
		public SelectList SupportCallCategories { get; set; }
		public SelectList OpenedByUsers { get; set; }
		public SelectList AssignedToUsers { get; set; }
		public SelectList AssignedToQueus { get; set; }
		public SelectList InvoiceStatusses { get; set; }
		MultiSelectList ColumnsToHide { get; set; }
		public Home _home = new Home();

		public IndexViewModel(IHomeRepository repo)
		{       // Creatie van de mogelijke keuzes waar men op kan filteren. Deze worden uit de databank gehaald, zo blijft de dropdown altijd 
				// up to date met de gegevens in de databank
				//De view haalt deze op uit de ViewData's
			this.TicketNumbers = new SelectList(repo.GetNumbers());
			this.Years = new SelectList(repo.GetYear());
			this.OrganizationNumbers = new SelectList(repo.GetOrganizationNumbers());
			this.InvoiceOrg = new SelectList(repo.getInvoiceCenterOrganizations());
			this.GroupNames = new SelectList(repo.getGroupNames());
			this.PersonNames = new SelectList(repo.getPersonNames());
			this.SupportCallTypes = new SelectList(repo.getSupportCallTypes());
			this.SupportCallPriorities = new SelectList(repo.getSupportCallPriorities());
			this.SupportCallImpacts = new SelectList(repo.getSupportCallImpacts());
			this.SupportCallUrgencies = new SelectList(repo.getSupportCallUrgencies());
			this.SupportCallstatusses = new SelectList(repo.getSupportCallStatusses());
			this.SupportCallCategories = new SelectList(repo.getSupportCallCategories());
			this.OpenedByUsers = new SelectList(repo.getOpenedByUsers());
			this.AssignedToUsers = new SelectList(repo.getAssignedtoUsers());
			this.AssignedToQueus = new SelectList(repo.getAssignedToQueus());
			this.InvoiceStatusses = new SelectList(repo.getInvoiceStatusses());
			this.ColumnsToHide = new MultiSelectList(_home.getProperties());
		}
	}
}
