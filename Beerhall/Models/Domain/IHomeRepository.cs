using System.Collections.Generic;
using System.Linq;

namespace TrustTeamVersion4.Models.Domain
{
	public interface IHomeRepository
	{
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
		string[,] GetEfficiency(IEnumerable<Home> homes);
		int GetAmountOfSetProperties(Home home);
		int[,] GetIncidentenTable(IEnumerable<Home> homes);
		IEnumerable<Home> RemoveNull(IEnumerable<Home> homes);
		int[] GetCategoryCount(IEnumerable<Home> homes);
		MultiKeyDictionary<string, string, int> GetPersonsPerGroup(IEnumerable<Home> homes);
		List<string> GetUniqueGroups(IEnumerable<Home> homes);
		List<string> GetUniquePersonNames(IEnumerable<Home> homes);
	}
}
