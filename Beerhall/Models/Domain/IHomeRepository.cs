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
		IEnumerable<IGrouping<string, string>> GetHoursWorkedOnUrgency(IEnumerable<Home> homes);
	}
}
