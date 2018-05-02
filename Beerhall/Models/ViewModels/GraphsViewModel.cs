using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrustTeamVersion4.Data.Repositories;
using TrustTeamVersion4.Models.Domain;

namespace TrustteamVersion4.Models.ViewModels
{
	[Serializable]
	[JsonObject(MemberSerialization.Fields)]
	public class GraphsViewModel 
	{
		#region properties
		[JsonProperty]
		public List<string> impacts { get; set; }
		[JsonProperty]
		public List<string> urgenties { get; set; }
		[JsonProperty]
		public int[,] prioriteiten { get; set; }
		[JsonProperty]
		public string[] EfficiencyString { get; set; }
		[JsonProperty]
		public double[] EfficiencyDouble { get; set; }
		[JsonProperty]
		public string BedrijfFiltered { get; set; }
		[JsonProperty]
		public List<string> Categories { get; set; }
		[JsonProperty]
		public int[] NrPerCategory { get; set; }
		[JsonProperty]
		public List<string> uniqueGroups { get; set; }
		[JsonProperty]
		public MultiKeyDictionary<string, string, int> namesPerGroup { get; set; }
		[JsonProperty]
		public List<string> allNames { get; set; }
		[JsonProperty]
		private readonly Home _filter;
		[JsonProperty]
		private List<string> _RemoveNullImp;
		[JsonProperty]
		private List<string> _RemoveNullUrg;
		[JsonProperty]
		public bool PdfFormat { get; set; }
		#endregion
		public GraphsViewModel()
		{

		}
		public GraphsViewModel(IHomeRepository repo, IEnumerable<Home> filtered, Home filter)
		{
			#region Injectie
			this._filter = filter;
			#endregion
			try
			{
				workAsync(repo, filtered);
			}
			catch (Exception e)
			{
				throw new Exception("Er ging iets fout, mogelijks is uw sessie verlopen.");
			}
		}

		#region Methods
		#region PieChart
		private async Task<int> PieChart(IHomeRepository repo, IEnumerable<Home> filtered)
		{
			try
			{
				int counter = 0;
				string[,] efficiency = repo.GetEfficiency(filtered);
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

				BedrijfFiltered = _filter.InvoicCenterOrganization;


				EfficiencyString = tempString;
				EfficiencyDouble = tempNumber;
			}
			catch (Exception e)
			{

				throw new Exception(e.Message + "Your session has expired.");
			}
			return 1;
		}
		#endregion

		#region Aantal Per Categorie
		private  void AantalPerCategorie(IHomeRepository repo, IEnumerable<Home> filtered)
		{
			Categories = repo.getSupportCallCategories();
			NrPerCategory = repo.GetCategoryCount(filtered);
			Categories.Sort();
		}

		#endregion

		#region GroupTable
		private  void GroupTable(IHomeRepository repo, IEnumerable<Home> filtered)
		{
			uniqueGroups = repo.GetUniqueGroups(filtered);
			allNames = repo.GetUniquePersonNames(filtered);
			namesPerGroup = repo.GetPersonsPerGroup(filtered, uniqueGroups, allNames);
			
		}
		#endregion

		#region IncidentenTabel
		private async Task<int> IncidentenTabel(IHomeRepository repo, IEnumerable<Home> filtered)
		{
			// Het aanpassen van de NULL string naar Not Set omdat het mooier oogt in de tabel
			// Ophalen van alle impacts uit de repository/db
			this._RemoveNullImp = repo.getSupportCallImpacts();
			// de Index achterhalen van null
			int index = _RemoveNullImp.IndexOf("NULL");
			// Index is -1 als NULL niet voorkomt in de array, als dit niet zo is, dan zit hij er ergens in en dan vervangen we hem
			if (index != -1)
				_RemoveNullImp[index] = "Not Set";

			//Idem voor urgenties
			_RemoveNullUrg = repo.getSupportCallUrgencies();

			int index2 = _RemoveNullUrg.IndexOf("NULL");
			if (index2 != -1)
				_RemoveNullUrg[index2] = "Not Set";
			// Verzamelen van alle nodige data voor de tabel samen te steken 
			//(mogelijke impacts, mogelijke urgenties en de totale aantal per categorie)
			impacts = _RemoveNullImp;
			urgenties = _RemoveNullUrg;
			prioriteiten = repo.GetIncidentenTable(filtered);
			return 4;
		}
		#endregion

		#region IsNullObject
		public bool isNullObject()
		{
			int counter = 0;
			var properties = this.GetType().GetProperties();
			foreach (var p in properties)
			{
				var value = p.GetValue(this, null) ?? "null";
				if (value.Equals("null") | value.Equals(false))
					counter++;

			}
			if (counter == properties.Length)
				return true;
			else
				return false;
		}
		#endregion

		private async void workAsync(IHomeRepository repo, IEnumerable<Home> filtered)
		{
			Task<int> TaskOne = PieChart(repo, filtered);

			Task<int> TaskFour = IncidentenTabel(repo, filtered);

			AantalPerCategorie(repo, filtered);

			GroupTable(repo, filtered);

			int result = await TaskOne;
			int result2 = await TaskFour;

		}

		#endregion


	}
}

