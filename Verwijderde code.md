
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
Vroegere manier van incidenten tabel tellen
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
