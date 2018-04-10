
Verwijderde if statement
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

Probeersel van grafiek plotten voor tijd data

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
