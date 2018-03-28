using System;
using TrustTeamVersion4.Models.Domain;

namespace TrustTeamVersion4.Models.ViewModels
{
	public class HomeFilterViewModel
	{
		public Double? Month { get; set; }
		public Double? Year { get; set; }
		public string OrganizationNumber { get; set; }
		public string InvoicCenterOrganization { get; set; }
		public string GroupName { get; set; }
		public string PersonName { get; set; }
		public Double? Number { get; set; }
		public string SupportCallType { get; set; }
		public string SupportCallPriority { get; set; }
		public string SupportCallImpact { get; set; }
		public string SupportCallUrgency { get; set; }
		public string SupportCallStatus { get; set; }
		public string SupportCallCategory { get; set; }
		public DateTime? SupportCallOpenDate { get; set; }
		public DateTime? SupportCallOpenTime { get; set; }
		public DateTime? SupportCallClosedDate { get; set; }
		public DateTime? SupportCallClosedTime { get; set; }
		public string HoursTillClosed { get; set; }
		public string HoursInStatusOpen { get; set; }
		public string HoursInvoiceCenter { get; set; }
		public string InvoiceCallSummary { get; set; }
		public string OpenedByUser { get; set; }
		public string AssignedToUser { get; set; }
		public string AssignedToQueue { get; set; }
		public string FirstEventSummary { get; set; }
		public string ClienteleSupportCallSummary { get; set; }
		public string InvoiceOrganizationName { get; set; }
		public Double? InvoiceOrganizationNumber { get; set; }
		public string InvoiceStatus { get; set; }
		public string HoursClienteleWorkedOnSupportCall { get; set; }


		public HomeFilterViewModel()
		{
		}
		
		public HomeFilterViewModel(Home home) : this()
		{
			Month = home.Month;
			Year = home.Year;
			OrganizationNumber = home.OrganizationNumber;
			InvoicCenterOrganization = home.InvoicCenterOrganization;
			GroupName = home.GroupName;
			PersonName = home.PersonName;
			Number = home.Number;
			SupportCallType = home.SupportCallType;
			SupportCallPriority = home.SupportCallPriority;
			SupportCallImpact = home.SupportCallImpact;
			SupportCallUrgency = home.SupportCallUrgency;
			SupportCallStatus = home.SupportCallStatus;
			SupportCallCategory = home.SupportCallCategory;
			SupportCallOpenDate = home.SupportCallOpenDate;
			SupportCallOpenTime = home.SupportCallOpenTime;
			SupportCallClosedDate = home.SupportCallClosedDate;
			SupportCallClosedTime = home.SupportCallClosedTime;
			HoursTillClosed = home.HoursTillClosed;
			HoursInStatusOpen = home.HoursInStatusOpen;
			HoursInvoiceCenter = home.HoursInvoiceCenter;
			InvoiceCallSummary = home.InvoiceCallSummary;
			OpenedByUser = home.OpenedByUser;
			AssignedToUser = home.AssignedToUser;
			AssignedToQueue = home.AssignedToQueue;
			FirstEventSummary = home.FirstEventSummary;
			ClienteleSupportCallSummary = home.ClienteleSupportCallSummary;
			InvoiceOrganizationName = home.InvoiceOrganizationName;
			InvoiceOrganizationNumber = home.InvoiceOrganizationNumber;
			InvoiceStatus = home.InvoiceStatus;
			HoursClienteleWorkedOnSupportCall = home.HoursClienteleWorkedOnSupportCall;

		}
	}
}
