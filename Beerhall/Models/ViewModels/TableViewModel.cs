using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrustteamVersion4.Models.ViewModels
{
    public class TableViewModel
    {
		#region SortProperties
		public string _sortNumber {

			get {
				return _sortNumber;
			}

			set {
				if (_sortNumber == "number")
					_sortNumber = "numb_desc";
				else
				_sortNumber = value;
			}

		}
		public string _sortMonth
		{

			get
			{
				return _sortMonth;
			}
			set
			{
				if (_sortMonth == "Month")
					_sortMonth = "Month_desc";
				else
					_sortMonth = "Month";
			}

		}
		public string _sortYear
		{

			get
			{
				return _sortYear;
			}
			set
			{
				if (_sortYear == "Year")
					_sortYear = "Year_desc";
				else
					_sortYear = "Year";
			}

		}
		public string _sortOrganizationNumber
		{

			get
			{
				return _sortOrganizationNumber;
			}

			set
			{
				_sortOrganizationNumber = value == "OrganizationNumber" ? "OrganizationNumber_desc" : "OrganizationNumber";
			}

		}
		public string _sortInvoicCenterOrganization
		{

			get
			{
				return _sortInvoicCenterOrganization;
			}

			set
			{
				_sortInvoicCenterOrganization = value == "InvoicCenterOrganization" ? "InvoicCenterOrganization_desc" : "InvoicCenterOrganization";
			}

		}
		public string _sortGroupName
		{

			get
			{
				return _sortGroupName;
			}

			set
			{
				_sortGroupName = value == "GroupName" ? "GroupName_desc" : "GroupName";
			}

		}
		public string _sortSupportCallType
		{

			get
			{
				return _sortSupportCallType;
			}

			set
			{
				_sortSupportCallType = value == "SupportCallType" ? "SupportCallType_desc" : "SupportCallType";
			}

		}
		public string _sortPersonName
		{

			get
			{
				return _sortPersonName;
			}

			set
			{
				_sortPersonName = value == "PersonName" ? "PersonName_desc" : "PersonName";
			}

		}

		public string _sortSupportCallPriority
		{

			get
			{
				return _sortSupportCallPriority;
			}

			set
			{
				_sortSupportCallPriority = value == "SupportCallPriority" ? "SupportCallPriority_desc" : "SupportCallPriority";
			}

		}
		public string _sortSupportCallImpact
		{

			get
			{
				return _sortSupportCallImpact;
			}

			set
			{
				_sortSupportCallImpact = value == "SupportCallImpact" ? "SupportCallImpact_desc" : "SupportCallImpact";
			}

		}
		public string _sortSupportCallUrgency
		{

			get
			{
				return _sortSupportCallUrgency;
			}

			set
			{
				_sortSupportCallUrgency = value == "SupportCallUrgency" ? "SupportCallUrgency_desc" : "SupportCallUrgency";
			}

		}
		public string _sortSupportCallStatus
		{

			get
			{
				return _sortSupportCallStatus;
			}

			set
			{
				_sortSupportCallStatus = value == "SupportCallStatus" ? "SupportCallStatus_desc" : "SupportCallStatus";
			}

		}
		public string _sortSupportCallCategory
		{

			get
			{
				return _sortSupportCallCategory;
			}

			set
			{
				_sortSupportCallCategory = value == "SupportCallCategory" ? "SupportCallCategory_desc" : "SupportCallCategory";
			}

		}
		public string _sortSupportCallOpenDate
		{

			get
			{
				return _sortSupportCallOpenDate;
			}

			set
			{
				_sortSupportCallOpenDate = value == "SupportCallOpenDate" ? "SupportCallOpenDate_desc" : "SupportCallOpenDate";
			}

		}
		public string _sortSupportCallOpenTime
		{

			get
			{
				return _sortSupportCallOpenTime;
			}

			set
			{
				_sortSupportCallOpenTime = value == "SupportCallOpenTime" ? "SupportCallOpenTime_desc" : "SupportCallOpenTime";
			}

		}
		public string _sortSupportCallClosedDate
		{

			get
			{
				return _sortSupportCallClosedDate;
			}

			set
			{
				_sortSupportCallClosedDate = value == "SupportCallClosedDate" ? "SupportCallClosedDate_desc" : "SupportCallClosedDate";
			}

		}
		public string _sortSupportCallClosedTime
		{

			get
			{
				return _sortSupportCallClosedTime;
			}

			set
			{
				_sortSupportCallClosedTime = value == "SupportCallClosedTime" ? "SupportCallClosedTime_desc" : "SupportCallClosedTime";
			}

		}
		public string _sortHoursTillClosed
		{

			get
			{
				return _sortHoursTillClosed;
			}

			set
			{
				_sortHoursTillClosed = value == "HoursTillClosed" ? "HoursTillClosed_desc" : "HoursTillClosed";
			}

		}
		public string _sortHoursInStatusOpen
		{

			get
			{
				return _sortHoursInStatusOpen;
			}

			set
			{
				_sortHoursInStatusOpen = value == "HoursInStatusOpen" ? "HoursInStatusOpen_desc" : "HoursInStatusOpen";
			}

		}
		public string _sortHoursInvoiceCenter
		{

			get
			{
				return _sortHoursInvoiceCenter;
			}

			set
			{
				_sortHoursInvoiceCenter = value == "HoursInvoiceCenter" ? "HoursInvoiceCenter_desc" : "HoursInvoiceCenter";
			}

		}
		public string _sortInvoiceCallSummary
		{

			get
			{
				return _sortInvoiceCallSummary;
			}

			set
			{
				_sortInvoiceCallSummary = value == "InvoiceCallSummary" ? "InvoiceCallSummary_desc" : "InvoiceCallSummary";
			}

		}
		public string _sortOpenedByUser
		{

			get
			{
				return _sortOpenedByUser;
			}

			set
			{
				_sortOpenedByUser = value == "OpenedByUser" ? "OpenedByUser_desc" : "OpenedByUser";
			}

		}
		public string _sortAssignedToUser
		{

			get
			{
				return _sortAssignedToUser;
			}

			set
			{
				_sortAssignedToUser = value == "AssignedToUser" ? "AssignedToUser_desc" : "AssignedToUser";
			}

		}
		public string _sortAssignedToQueue
		{

			get
			{
				return _sortAssignedToQueue;
			}

			set
			{
				_sortAssignedToQueue = value == "AssignedToQueue" ? "AssignedToQueue_desc" : "AssignedToQueue";
			}

		}
		public string _sortFirstEventSummary
		{

			get
			{
				return _sortFirstEventSummary;
			}

			set
			{
				_sortFirstEventSummary = value == "FirstEventSummary" ? "FirstEventSummary_desc" : "FirstEventSummary";
			}

		}
		public string _sortClienteleSupportCallSummary
		{

			get
			{
				return _sortClienteleSupportCallSummary;
			}

			set
			{
				_sortClienteleSupportCallSummary = value == "ClienteleSupportCallSummary" ? "ClienteleSupportCallSummary_desc" : "ClienteleSupportCallSummary";
			}

		}
		public string _sortInvoiceOrganizationName
		{

			get
			{
				return _sortInvoiceOrganizationName;
			}

			set
			{
				_sortInvoiceOrganizationName = value == "InvoiceOrganizationName" ? "InvoiceOrganizationName_desc" : "InvoiceOrganizationName";
			}

		}
		public string _sortInvoiceOrganizationNumber
		{

			get
			{
				return _sortInvoiceOrganizationNumber;
			}

			set
			{
				_sortInvoiceOrganizationNumber = value == "InvoiceOrganizationNumber" ? "InvoiceOrganizationNumber_desc" : "InvoiceOrganizationNumber";
			}

		}
		public string _sortInvoiceStatus
		{

			get
			{
				return _sortInvoiceStatus;
			}

			set
			{
				_sortInvoiceStatus = value == "InvoiceStatus" ? "InvoiceStatus_desc" : "InvoiceStatus";
			}

		}
		public string _sortHoursClienteleWorkedOnSupportCall
		{

			get
			{
				return _sortHoursClienteleWorkedOnSupportCall;
			}

			set
			{
				_sortHoursClienteleWorkedOnSupportCall = value ==  "HoursClienteleWorkedOnSupportCall" ? "HoursClienteleWorkedOnSupportCall_desc" : "HoursClienteleWorkedOnSupportCall";
			}

		}
		#endregion

	}
}
