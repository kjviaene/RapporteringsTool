using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrustTeamVersion4.Models.Domain
{
	public class Home
	{
		// Alle kollom namen
		[Display(Name = "Maand")]
		public Double? Month { get; set; }
		[Display(Name = "Jaartal")]
		public Double? Year { get; set; }
		[Display(Name = "Nummer Organisatie")]
		public string OrganizationNumber { get; set; }
		[Display(Name = "Naam Organisatie")]
		public string InvoicCenterOrganization { get; set; }
		[Display(Name = "Groep naam")]
		public string GroupName { get; set; }
		[Display(Name = "Naam persoon")]
		public string PersonName { get; set; }
		[Display(Name = "Nummer ticket")]
		public Double? Number { get; set; }
		[Display(Name = "Support Call Type")]
		public string SupportCallType { get; set; }
		[Display(Name = "Prioriteit")]
		public string SupportCallPriority { get; set; }
		[Display(Name = "Impact")]
		public string SupportCallImpact { get; set; }
		[Display(Name = "Urgentie")]
		public string SupportCallUrgency { get; set; }
		[Display(Name = "Status")]
		public string SupportCallStatus { get; set; }
		[Display(Name = "Categorie")]
		public string SupportCallCategory { get; set; }
		[Display(Name = "Datum opening")]
		public DateTime? SupportCallOpenDate { get; set; }
		[Display(Name = "Tijdstip opening")]
		public DateTime? SupportCallOpenTime { get; set; }
		[Display(Name = "Datum sluiting")]
		public DateTime? SupportCallClosedDate { get; set; }
		[Display(Name = "Tijdstip sluiting")]
		public DateTime? SupportCallClosedTime { get; set; }
		[Display(Name = "Hours Till Closed")]
		public string HoursTillClosed { get; set; }
		[Display(Name = "Hours in status open")]
		public string HoursInStatusOpen { get; set; }
		[Display(Name = "hours invoice center")]
		public string HoursInvoiceCenter { get; set; }
		[Display(Name = "Samenvatting")]
		public string InvoiceCallSummary { get; set; }
		[Display(Name = " Geopend door")]
		public string OpenedByUser { get; set; }
		[Display(Name = "Toegewezen gebruiker")]
		public string AssignedToUser { get; set; }
		[Display(Name = "Toegewijzen queue")]
		public string AssignedToQueue { get; set; }
		[Display(Name = "First even summary")]
		public string FirstEventSummary { get; set; }
		[Display(Name = "Clientele support call summary")]
		public string ClienteleSupportCallSummary { get; set; }
		[Display(Name = "Invoice organization name")]
		public string InvoiceOrganizationName { get; set; }
		[Display(Name = "Invoice Organization Number")]
		public Double? InvoiceOrganizationNumber { get; set; }
		[Display(Name = "Status invoice")]
		public string InvoiceStatus { get; set; }
		[Display(Name = "Hours clientele worked on suppor call")]
		public string HoursClienteleWorkedOnSupportCall { get; set; }

		//// Voor elke tabel waarop gefiltert kan worden maken we een verzameling voor alle mogelijke selecties in op te slaan
		//public IEnumerable<SelectListItem> TicketNumbers { get; set; }
		//public IEnumerable<SelectListItem> Years { get; set; }
		//public IEnumerable<SelectListItem> OrganizationNumbers { get; set; }
		//public IEnumerable<SelectListItem> InvoiceOrg { get; set; }
		//public IEnumerable<SelectListItem> GroupNames { get; set; }
		//public IEnumerable<SelectListItem> PersonNames { get; set; }
		//public IEnumerable<SelectListItem> SupportCallTypes { get; set; }
		//public IEnumerable<SelectListItem> SupportCallPriorities { get; set; }
		//public IEnumerable<SelectListItem> SupportCallImpacts { get; set; }
		//public IEnumerable<SelectListItem> SupportCallUrgencies { get; set; }
		//public IEnumerable<SelectListItem> SupportCallstatusses { get; set; }
		//public IEnumerable<SelectListItem> OpenedByUsers { get; set; }
		//public IEnumerable<SelectListItem> AssignedToUsers { get; set; }
		//public IEnumerable<SelectListItem> AssignedToQueus { get; set; }
		//public IEnumerable<SelectListItem> InvoiceStatusses { get; set; }

		//// Hierin slaan we leesbare versie op van het attribuut
		//public string TicketNumbersName { get; set; }
		//public string YearsName { get; set; }
		//public string OrganizationNumbersName { get; set; }
		//public string InvoiceOrgName { get; set; }
		//public string GroupNamesName { get; set; }
		//public string PersonNamesName { get; set; }
		//public string SupportCallTypesName { get; set; }
		//public string SupportCallPrioritiesName { get; set; }
		//public string SupportCallImpactsName { get; set; }
		//public string SupportCallUrgenciesName { get; set; }
		//public string SupportCallstatussesName { get; set; }
		//public string OpenedByUsersName { get; set; }
		//public string AssignedToUsersName { get; set; }
		//public string AssignedToQueusName { get; set; }
		//public string InvoiceStatussesName { get; set; }

	}
}
