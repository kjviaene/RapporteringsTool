using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustTeamVersion4.Data.Repositories;
using TrustTeamVersion4.Models.Domain;

namespace TrustteamVersion4.Models.ViewModels
{
	public class GraphsViewModel
	{
		public List<string> impacts { get; set; }
		public List<string> urgenties { get; set; }
		public int[,] prioriteiten { get; set; }
		public string[] EfficiencyString { get; set; }
		public double[] EfficiencyDouble { get; set; }
		public string BedrijfFiltered { get; set; }
		public List<string> Categories { get; set; }
		public int[] NrPerCategory { get; set; }
		public List<string> uniqueGroups { get; set; }
		public MultiKeyDictionary<string, string, int> namesPerGroup { get; set;}
		public List<string> allNames { get; set; }
		private readonly IHomeRepository  _homeRepository;
		private readonly IEnumerable<Home> _homesFiltered;
		private readonly Home _filter;
		private List<string> _RemoveNullImp;
		private List<string> _RemoveNullUrg;

		public GraphsViewModel(IHomeRepository repo, IEnumerable<Home> filtered, Home filter)
		{
			#region Injectie
			this._homeRepository = repo;
			this._homesFiltered = filtered;
			this._filter = filter;
			#endregion

			#region PieChart1
			int counter = 0;
			string[,] efficiency = _homeRepository.GetEfficiency(_homesFiltered);
			for (var g = 0; g < efficiency.Length / 2; g++)
			{
				if (!(efficiency[g, 0] == null))
				{
					counter++;
				}
			}

			string[] tempString = new string[counter];
			double[] tempNumber = new double[counter];
			for (var k = 0; k < counter; k++)
			{
				tempString[k] = efficiency[k, 0];
				tempNumber[k] = Int32.Parse(efficiency[k, 1]);
			}

			BedrijfFiltered = filter.InvoicCenterOrganization;
			EfficiencyString = tempString;
			EfficiencyDouble = tempNumber;
			#endregion

			#region Aantal per categorie
			Categories = _homeRepository.getSupportCallCategories();
			NrPerCategory = _homeRepository.GetCategoryCount(_homesFiltered);
			Categories.Sort();
			#endregion

			#region GroupTable
			uniqueGroups = _homeRepository.GetUniqueGroups(_homesFiltered);
			namesPerGroup = _homeRepository.GetPersonsPerGroup(_homesFiltered);
			allNames = _homeRepository.GetUniquePersonNames(_homesFiltered);
			#endregion

			#region IncidentenTabel
			// Het aanpassen van de NULL string naar Not Set omdat het mooier oogt in de tabel
			// Ophalen van alle impacts uit de repository/db
			this._RemoveNullImp = _homeRepository.getSupportCallImpacts();
			// de Index achterhalen van null
			int index = _RemoveNullImp.IndexOf("NULL");
			// Index is -1 als NULL niet voorkomt in de array, als dit niet zo is, dan zit hij er ergens in en dan vervangen we hem
			if (index != -1)
				_RemoveNullImp[index] = "Not Set";

			//Idem voor urgenties
			 _RemoveNullUrg = _homeRepository.getSupportCallUrgencies();

			int index2 = _RemoveNullUrg.IndexOf("NULL");
			if (index2 != -1)
				_RemoveNullUrg[index2] = "Not Set";
			// Verzamelen van alle nodige data voor de tabel samen te steken 
			//(mogelijke impacts, mogelijke urgenties en de totale aantal per categorie)
			impacts = _RemoveNullImp;
			urgenties = _RemoveNullUrg;
			prioriteiten = _homeRepository.GetIncidentenTable(_homesFiltered);
			#endregion
		}




	}
}


//#region PieChart1
//int counter = 0;
//string[,] efficiency = _homeRepository.GetEfficiency(_homesFiltered);
//			for (var g = 0; g<efficiency.Length / 2; g++)
//			{
//				if (!(efficiency[g, 0] == null))
//				{
//					counter++;
//				}
//			}

//			string[] tempString = new string[counter];
//double[] tempNumber = new double[counter];
//			for (var k = 0; k<counter; k++)
//			{
//				tempString[k] = efficiency[k, 0];
//				tempNumber[k] = Int32.Parse(efficiency[k, 1]);
//			}
//			ViewData["Bedrijf"] = filter.InvoicCenterOrganization;
//			ViewData["EfficiencyString"] = tempString;
//			ViewData["EfficiencyDouble"] = tempNumber;
//			#endregion
//			#region IncidentenTabel
//			// Het aanpassen van de NULL string naar Not Set omdat het mooier oogt in de tabel
//			List<string> removeNullImp = _homeRepository.getSupportCallImpacts();
//int index = removeNullImp.IndexOf("NULL");
//			if (index != -1)
//				removeNullImp[index] = "Not Set";
//			List<string> removeNullUrg = _homeRepository.getSupportCallUrgencies();
//int index2 = removeNullUrg.IndexOf("NULL");
//			if (index2 != -1)
//				removeNullUrg[index2] = "Not Set";
//			// Verzamelen van alle nodige data voor de tabel samen te steken (mogelijke impacts, mogelijke urgenties en de totale aantal per categorie

//			GraphsViewModel temp1 = new GraphsViewModel(_homeRepository, _homesFiltered);
////ViewData["Impacts"] = removeNullImp;
//ViewBag.Impacts = removeNullImp;
//			ViewData["Urgenties"] = removeNullUrg;
//			ViewData["Prioriteiten"] = _homeRepository.GetIncidentenTable(_homesFiltered);
//			#endregion
//			#region Aantal per categorie
//			List<string> categories = _homeRepository.getSupportCallCategories();
//int[] amounts = _homeRepository.GetCategoryCount(_homesFiltered);

//categories.Sort();


//			ViewData["Categories"] = categories;
//			ViewData["NrPerCategory"] = amounts;

//			#endregion
//			#region GroupTable
//			ViewData["uniqueGroups"] = _homeRepository.GetUniqueGroups(_homesFiltered);
//			ViewData["namesPerGroup"] = _homeRepository.GetPersonsPerGroup(_homesFiltered);
//			ViewData["allNames"] = _homeRepository.GetUniquePersonNames(_homesFiltered);
//			#endregion
//			return View(temp1);
