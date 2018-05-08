
## Verwijderde if statement voor het filteren op properties
```
//if (!(home.Month.HasValue) | home.Month.Equals(h.Month))
//{
//	if (!(home.Number.HasValue) | home.Number.Equals(h.Number))
//	{
//		if (!(home.Year.HasValue) | home.Year.Equals(h.Year))
//		{
//			if (home.OrganizationNumber == null | home.OrganizationNumber == h.OrganizationNumber)
//			{
//				if (home.InvoicCenterOrganization == null | home.InvoicCenterOrganization == h.InvoicCenterOrganization)
//				{
//					if (home.GroupName == null | home.GroupName == h.GroupName)
//					{
//						if (home.PersonName == null | home.PersonName == h.PersonName)
//						{
//							if (home.SupportCallType == null | home.SupportCallType == h.SupportCallType)
//							{
//								if (home.SupportCallPriority == null | home.SupportCallPriority == h.SupportCallPriority)
//								{
//									if (home.SupportCallImpact == null | home.SupportCallImpact == h.SupportCallImpact)
//									{
//										if (home.SupportCallUrgency == null | home.SupportCallUrgency == h.SupportCallUrgency)
//										{
//											if (home.SupportCallStatus == null | home.SupportCallStatus == h.SupportCallStatus)
//											{
//												if (home.OpenedByUser == null | home.OpenedByUser == h.OpenedByUser)
//												{
//													if (home.AssignedToUser == null | home.AssignedToUser == h.AssignedToUser)
//													{
//														if (home.AssignedToQueue == null | home.AssignedToQueue == h.AssignedToQueue)
//														{
//															if (home.InvoiceStatus == null | home.InvoiceStatus == h.InvoiceStatus)
//															{
//																if (home.SupportCallOpenDate == DateTime.MinValue | home.SupportCallOpenDate <= h.SupportCallOpenDate)
//																{
//																	if (home.SupportCallOpenDateEinde == DateTime.MinValue | home.SupportCallOpenDateEinde >= h.SupportCallOpenDate)
//																	{
//																		filteredHomes.Add(h);
//																	}

//																}

//															}
//														}
//													}
//												}
//											}

//										}
//									}
//								}
//							}
//						}
//					}
//				}
//			}

//		}
//	}

//}
```

## Probeersel van grafiek plotten voor tijd data

```
function drawHoursworked() {
		var data = new google.visualization.DataTable();
		data.addColumn("number"," Hours");
		@{
			foreach (IGrouping<string, string> urgentie in hoursWorked)
			{
				@:data.addColumn("number",@urgentie.Key);
			}
				foreach (IGrouping<string, string> urgentie in hoursWorked)
				{
					foreach (string getal in urgentie)
					{
						@:data.addRows([[]]);
					}
				}
			}
      ```
## Vroegere manier van incidenten tabel tellen
```
//	if (h.SupportCallImpact.StartsWith("1"))
			//	{
			//		if (h.SupportCallUrgency.StartsWith("1"))
			//		{
			//			table[1, 1]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("2"))
			//		{
			//			table[1, 2]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("3"))
			//		{
			//			table[1, 3]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("4"))
			//		{
			//			table[1, 4]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("5"))
			//		{
			//			table[1, 5]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("N"))
			//		{
			//			table[1, 6]++;
			//		}
			//	}
			//	if (h.SupportCallImpact.StartsWith("2"))
			//	{
			//		if (h.SupportCallUrgency.StartsWith("1"))
			//		{
			//			table[2, 1]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("2"))
			//		{
			//			table[2, 2]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("3"))
			//		{
			//			table[2, 3]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("4"))
			//		{
			//			table[2, 4]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("5"))
			//		{
			//			table[2, 5]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("N"))
			//		{
			//			table[2, 6]++;
			//		}
			//	}
			//	if (h.SupportCallImpact.StartsWith("3"))
			//	{
			//		if (h.SupportCallUrgency.StartsWith("1"))
			//		{
			//			table[3, 1]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("2"))
			//		{
			//			table[3, 2]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("3"))
			//		{
			//			table[3, 3]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("4"))
			//		{
			//			table[3, 4]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("5"))
			//		{
			//			table[3, 5]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("N"))
			//		{
			//			table[3, 6]++;
			//		}
			//	}
			//	if (h.SupportCallImpact.StartsWith("4"))
			//	{
			//		if (h.SupportCallUrgency.StartsWith("1"))
			//		{
			//			table[4, 1]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("2"))
			//		{
			//			table[4, 2]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("3"))
			//		{
			//			table[4, 3]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("4"))
			//		{
			//			table[4, 4]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("5"))
			//		{
			//			table[4, 5]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("N"))
			//		{
			//			table[4, 6]++;
			//		}
			//	}
			//	if (h.SupportCallImpact.Equals("NULL"))
			//	{
			//		if (h.SupportCallUrgency.StartsWith("1"))
			//		{
			//			table[5, 1]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("2"))
			//		{
			//			table[5, 2]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("3"))
			//		{
			//			table[5, 3]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("4"))
			//		{
			//			table[5, 4]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("5"))
			//		{
			//			table[5, 5]++;
			//		}
			//		if (h.SupportCallUrgency.StartsWith("N"))
			//		{
			//			table[5, 6]++;
			//		}
			//	}
```
## Probeersel voor het printen van de tabel in excel
```

			//ExcelPackage pck = new ExcelPackage();
			////Add a new ExcelSheet
			//ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Rapport");
			//List<string> propsString = _home.getProperties();
			//PropertyInfo[] props = _home.GetProperties();
			//string[] columnHeaders = this.AlphabeticalColumns(propsString.Count());
			//int counter = 0;
			//IEnumerable<Home> data = _homesFiltered;
			//foreach (var pr in propsString)
			//{
			//	ws.Cells[columnHeaders[counter] + "1"].Value = pr.ToString();
			//	counter++;
			//}
			//int rowStart = 2;
			//foreach (var item in data)
			//{
			//	int columnCounter = 1;
			//	foreach (var pr in props)
			//	{
			//		ws.Cells[string.Format("{0}{1}", columnHeaders[columnCounter], rowStart)].Value = pr.GetValue(item);
			//		columnCounter++;
			//	}
			//	rowStart++;
			//}
			//ws.Cells["A:ZZ"].AutoFitColumns();
			//Response.Clear();
			//Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			// Response.AddHeader("content-disposition","attachment: filename=" + "ExcelReport.xlsx");
			//Response.BinaryWrite(pck.GetAsByteArray());
			//Response.end();
```

## Oude manier van sorteren, vervangen door partial view.
```
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
```
## Oude tabel creatie (vervangen door TableViewModel)
```
@*<table class="table table-striped table-condensed table-bordered table-responsive">
		<tr>
			<!-- - De klasse tbh_*_hide is voor de mogelijkheid tot het verbergen van elke kolom
			 - De klasse btn-table is voor de knop die dient om een kolom te verbergen. Deze krijgt wat andere css omdat
			   de standaard css niet gepast was in deze context.
			 - Het id btnHide* is telkens voor het verbergen van de kolom waarin de button zich bevind
			 - De Actionlinks dienen voor het sorteren van deze kolom. Dit roept terug de Table methode op uit de controller en geeft
			   als parameter een nieuwe instatie terug van de correcte viewBag
			- Het automatiseren van dit is wat uitdagend door het feit dat bij elke header een andere Viewbag instantie wordt gecreërd. Itereren
			  door een verzameling en voor elk item een aparte instantie van een verschillend object aan kunnen maken is absoluut niet eenvoudig en
			  zou waarschijnlijk meer werk kosten dan dat het opstellen van deze manuele lijst kost.-->
			<th>N°</th>
			<th class="tbh_Number_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide1"><b>X</b></button>@Html.ActionLink("Number", "Table", new { sortOrder = ViewBag.Number })</th>
			<th class="tbh_Month_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide2"><b>X</b></button>@Html.ActionLink("Month", "Table", new { sortOrder = ViewBag.Month })</th>
			<th class="tbh_Year_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide3"><b>X</b></button>@Html.ActionLink("Year", "Table", new { sortOrder = ViewBag.Year })</th>
			<th class="tbh_OrganizationNumber_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide4"><b>X</b></button>@Html.ActionLink("OrganizationNumber", "Table", new { sortOrder = ViewBag.OrganizationNumber })</th>
			<th class="tbh_InvoicCenterOrganization_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide5"><b>X</b></button>@Html.ActionLink("InvoicCenterOrganization", "Table", new { sortOrder = ViewBag.InvoicCenterOrganization })</th>
			<th class="tbh_GroupName_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide6"><b>X</b></button>@Html.ActionLink("GroupName", "Table", new { sortOrder = ViewBag.GroupName })</th>
			<th class="tbh_PersonName_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide7"><b>X</b></button>@Html.ActionLink("PersonName", "Table", new { sortOrder = ViewBag.PersonName })</th>
			<th class="tbh_SupportCallType_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide8"><b>X</b></button>@Html.ActionLink("SupportCallType", "Table", new { sortOrder = ViewBag.SupportCallType })</th>
			<th class="tbh_SupportCallPriority_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide9"><b>X</b></button>@Html.ActionLink("SupportCallPriority", "Table", new { sortOrder = ViewBag.SupportCallPriority })</th>
			<th class="tbh_SupportCallImpact_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide10"><b>X</b></button>@Html.ActionLink("SupportCallImpact", "Table", new { sortOrder = ViewBag.SupportCallImpact })</th>
			<th class="tbh_SupportCallUrgency_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide11"><b>X</b></button>@Html.ActionLink("SupportCallUrgency", "Table", new { sortOrder = ViewBag.SupportCallUrgency })</th>
			<th class="tbh_SupportCallStatus_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide12"><b>X</b></button>@Html.ActionLink("SupportCallStatus", "Table", new { sortOrder = ViewBag.SupportCallStatus })</th>
			<th class="tbh_SupportCallCategory_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide13"><b>X</b></button>@Html.ActionLink("SupportCallCategory", "Table", new { sortOrder = ViewBag.SupportCallCategory })</th>
			<th class="tbh_SupportCallOpenDate_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide14"><b>X</b></button>@Html.ActionLink("SupportCallOpenDate", "Table", new { sortOrder = ViewBag.SupportCallOpenDate })</th>
			<th class="tbh_SupportCallOpenTime_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide15"><b>X</b></button>@Html.ActionLink("SupportCallOpenTime", "Table", new { sortOrder = ViewBag.SupportCallOpenTime })</th>
			<th class="tbh_SupportCallClosedDateNotNull_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide16"><b>X</b></button>@Html.ActionLink("SupportCallClosedDate", "Table", new { sortOrder = ViewBag.SupportCallClosedDate })</th>
			<th class="tbh_SupportCallClosedTimeNotNull_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide17"><b>X</b></button>@Html.ActionLink("SupportCallClosedTime", "Table", new { sortOrder = ViewBag.SupportCallClosedTime })</th>
			<th class="tbh_HoursTillClosed_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide18"><b>X</b></button>@Html.ActionLink("HoursTillClosed", "Table", new { sortOrder = ViewBag.HoursTillClosed })</th>
			<th class="tbh_HoursInStatusOpen_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide19"><b>X</b></button>@Html.ActionLink("HoursInStatusOpen", "Table", new { sortOrder = ViewBag.HoursInStatusOpen })</th>
			<th class="tbh_HoursInvoiceCenter_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide20"><b>X</b></button>@Html.ActionLink("HoursInvoiceCenter", "Table", new { sortOrder = ViewBag.HoursInvoiceCenter })</th>
			<th class="tbh_InvoiceCallSummary_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide21"><b>X</b></button>@Html.ActionLink("InvoiceCallSummary", "Table", new { sortOrder = ViewBag.InvoiceCallSummary })</th>
			<th class="tbh_OpenedByUser_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide22"><b>X</b></button>@Html.ActionLink("OpenedByUser", "Table", new { sortOrder = ViewBag.OpenedByUser })</th>
			<th class="tbh_AssignedToUser_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide23"><b>X</b></button>@Html.ActionLink("AssignedToUser", "Table", new { sortOrder = ViewBag.AssignedToUser })</th>
			<th class="tbh_26_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide26"><b>X</b></button>@Html.ActionLink("ClienteleSupportCallSummary", "Table", new { sortOrder = ViewBag.ClienteleSupportCallSummary })</th>
			<th class="tbh_InvoiceOrganizationName_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide27"><b>X</b></button>@Html.ActionLink("InvoiceOrganizationName", "Table", new { sortOrder = ViewBag.InvoiceOrganizationName })</th>
			<th class="tbh_InvoiceOrganizationNumber_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide28"><b>X</b></button>@Html.ActionLink("InvoiceOrganizationNumber", "Table", new { sortOrder = ViewBag.InvoiceOrganizationNumber })</th>
			<th class="tbh_InvoiceStatus_hide"><button class="btn  btn-xs btn-info btn-table" id="btnHide29"><b>X</b></button>@Html.ActionLink("InvoiceStatus", "Table", new { sortOrder = ViewBag.InvoiceStatus })</th>
		</tr>
		<!-- Het overlopen van alle Home objecten die werden meegeven in het model en het printen hiervan in de tabel-->
		 int counter = 1;
			foreach (var item in Model.Data)
			{
				<tr>
					<td>@counter</td>
					<td class="tbh_Number_hide">@item.Number</td>
					<td class="tbh_Month_hide">@item.Month</td>
					<td class="tbh_Year_hide">@item.Year</td>
					<td class="tbh_OrganizationNumber_hide">@item.OrganizationNumber</td>
					<td class="tbh_InvoicCenterOrganization_hide">@item.InvoicCenterOrganization</td>
					<td class="tbh_GroupName_hide">@item.GroupName</td>
					<td class="tbh_PersonName_hide">@item.PersonName</td>
					<td class="tbh_SupportCallType_hide">@item.SupportCallType</td>
					<td class="tbh_SupportCallPriority_hide">@item.SupportCallPriority</td>
					<td class="tbh_SupportCallImpact_hide">@item.SupportCallImpact</td>
					<td class="tbh_SupportCallUrgency_hide">@item.SupportCallUrgency</td>
					<td class="tbh_SupportCallStatus_hide">@item.SupportCallStatus</td>
					<td class="tbh_SupportCallCategory_hide">@item.SupportCallCategory</td>
					<td class="tbh_SupportCallOpenDate_hide">@item.SupportCallOpenDate.ToShortDateString()</td>
					<td class="tbh_SupportCallOpenTime_hide">@item.SupportCallOpenTime.ToShortTimeString()</td>
					<td class="tbh_SupportCallClosedDateNotNull_hide">@item.SupportCallClosedDateNotNull.ToShortDateString()</td>
					<td class="tbh_SupportCallClosedTimeNotNull_hide">@item.SupportCallClosedTimeNotNull.ToShortTimeString()</td>
					<td class="tbh_HoursTillClosed_hide">@item.HoursTillClosed</td>
					<td class="tbh_HoursInStatusOpen_hide">@item.HoursInStatusOpen</td>
					<td class="tbh_HoursInvoiceCenter_hide">@item.HoursInvoiceCenter</td>
					<td class="tbh_InvoiceCallSummary_hide">@item.InvoiceCallSummary</td>
					<td class="tbh_OpenedByUser_hide">@item.OpenedByUser</td>
					<td class="tbh_AssignedToUser_hide">@item.AssignedToUser</td>
					<td class="tbh_AssignedToQueue_hide">@item.AssignedToQueue</td>
					<td class="tbh_InvoiceOrganizationName_hide">@item.InvoiceOrganizationName</td>
					<td class="tbh_InvoiceOrganizationNumber_hide">@item.InvoiceOrganizationNumber</td>
					<td class="tbh_InvoiceStatus_hide">@item.InvoiceStatus</td>
				</tr>

				counter = counter + 1;

			}

	</table>*@

```
## Oude Javascript voor het sluiten van een kolom bij het klikken op een button
```
<script type="text/javascript">

	$(document).ready(function () {

		$("#btnHide1").click(function () {
			$("td.tbh_Number_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_Number_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide2").click(function () {
			$("td.tbh_Month_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_Month_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide3").click(function () {
			$("td.tbh_Year_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_Year_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide4").click(function () {
			$("td.tbh_OrganizationNumber_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_OrganizationNumber_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide5").click(function () {
			$("td.tbh_InvoicCenterOrganization_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_InvoicCenterOrganization_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide6").click(function () {
			$("td.tbh_GroupName_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_GroupName_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide7").click(function () {
			$("td.tbh_PersonName_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_PersonName_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide8").click(function () {
			$("td.tbh_SupportCallType_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallType_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide9").click(function () {
			$("td.tbh_SupportCallPriority_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallPriority_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide10").click(function () {
			$("td.tbh_SupportCallImpact_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallImpact_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide11").click(function () {
			$("td.tbh_SupportCallUrgency_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallUrgency_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide12").click(function () {
			$("td.tbh_SupportCallStatus_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallStatus_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide13").click(function () {
			$("td.tbh_SupportCallCategory_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallCategory_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide14").click(function () {
			$("td.tbh_SupportCallOpenDate_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallOpenDate_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide15").click(function () {
			$("td.tbh_SupportCallOpenTime_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallOpenTime_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide16").click(function () {
			$("td.tbh_SupportCallClosedDateNotNull_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallClosedDateNotNull_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide17").click(function () {
			$("td.tbh_SupportCallClosedTimeNotNull_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_SupportCallClosedTimeNotNull_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide18").click(function () {
			$("td.tbh_HoursTillClosed_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_HoursTillClosed_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide19").click(function () {
			$("td.tbh_HoursInStatusOpen_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_HoursInStatusOpen_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide20").click(function () {
			$("td.tbh_HoursInvoiceCenter_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_HoursInvoiceCenter_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide21").click(function () {
			$("td.tbh_InvoiceCallSummary_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_InvoiceCallSummary_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide22").click(function () {
			$("td.tbh_OpenedByUser_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_OpenedByUser_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide23").click(function () {
			$("td.tbh_AssignedToUser_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_AssignedToUser_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide24").click(function () {
			$("td.tbh_AssignedToQueue_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_AssignedToQueue_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide27").click(function () {
			$("td.tbh_InvoiceOrganizationName_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_InvoiceOrganizationName_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide28").click(function () {
			$("td.tbh_InvoiceOrganizationNumber_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_InvoiceOrganizationNumber_hide").addClass(function (n) {
				return "hidden";
			});
		});
		$("#btnHide29").click(function () {
			$("td.tbh_InvoiceStatus_hide").addClass(function (n) {
				return "hidden";
			});
			$("th.tbh_InvoiceStatus_hide").addClass(function (n) {
				return "hidden";
			});
		});
	});
</script>
```
## Oude manier van data doorspelen aan index voor het opstellen van selectLists
```
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
  ViewData["ColumnsToHide"] = new MultiSelectList(_home.getProperties());
}
```
