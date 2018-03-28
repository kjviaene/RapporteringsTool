﻿using System.Collections.Generic;

namespace TrustTeamVersion4.Models.Domain
{
	public interface IHomeRepository
	{
		IEnumerable<Home> GetBy(double number);
		IEnumerable<Home> GetAll();
		List<double?> GetNumbers();
		List<double?> GetYear();
		List<string> GetOrganizationNumbers();
		List<string> getInvoiceCenterOrganizations();
		List<string> getGroupNames();
		List<string> getPersonNames();
		List<string> getSupportCallTypes();
		List<string> getSupportCallPriorities();
		List<string> getSupportCallImpacts();
		List<string> getSupportCallUrgencies();
		List<string> getSupportCallStatusses();
		List<string> getSupportCallCategories();
		List<string> getOpenedByUsers();
		List<string> getAssignedtoUsers();
		List<string> getAssignedToQueus();
		List<string> getInvoiceStatusses();
		List<Home> Filter(Home home);

	}
}
