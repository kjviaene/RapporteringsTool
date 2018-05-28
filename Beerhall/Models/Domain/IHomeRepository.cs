using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TrustTeamVersion4.Models.Domain
{
	public interface IHomeRepository
	{// Uitleg voor alle methodes is te vinden in HomeRepository
		IEnumerable<Home> InitialFilter(Home home);
		IEnumerable<Home> getFiltered(string[] property, List<string> filter, IEnumerable<Home> initial);
		IEnumerable<Home> GetAll();
		List<int> GetNumbers();
		List<int> GetYear();
		List<string> GetOrganizationNumbers();
		List<string> getInvoiceCenterOrganizations();
		List<string> getGroupNames();
		List<string> getPersonNames();
		List<string> getSupportCallTypes();
		List<string> getSupportCallPriorities();
		List<string> getSupportCallImpacts();
		List<string> getSupportCallUrgencies();
		List<string> getSupportCallStatusses(IEnumerable<Home> homes);
		List<string> getSupportCallCategories(IEnumerable<Home> homes);
		List<string> getOpenedByUsers();
		List<string> getAssignedtoUsers();
		List<string> getAssignedToQueus();
		List<string> getInvoiceStatusses();
		List<Home> Filter(Home home);
		string[,] GetEfficiency(IEnumerable<Home> homes);
		int GetAmountOfSetProperties(Home home);
		int[,] GetIncidentenTable(IEnumerable<Home> homes);
		IEnumerable<Home> RemoveNull(IEnumerable<Home> homes);
		int[] GetCategoryCount(IEnumerable<Home> homes, List<string> cat);
		MultiKeyDictionary<string, string, int> GetPersonsPerGroup(IEnumerable<Home> homes, List<string> gr, List<string> na);
		List<string> GetUniqueGroups(IEnumerable<Home> homes);
		List<string> GetUniquePersonNames(IEnumerable<Home> homes);
	}
}
