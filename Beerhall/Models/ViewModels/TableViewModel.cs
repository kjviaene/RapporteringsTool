using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TrustTeamVersion4.Models.Domain;

namespace TrustteamVersion4.Models.ViewModels
{
	[Serializable()]
	[JsonObject(MemberSerialization.OptIn)]
	public class TableViewModel
	{
		#region Properties
		[JsonProperty]
		public List<Home> Data { get; set; }
		public PropertyInfo[] Properties { get; set; }
		[JsonProperty]
		public List<string> PropertiesAsString { get; set; }
		[JsonProperty]
		public List<string> ClosedColumns
		{

			get { return _avoidNullClosedColumns; }
			set
			{
				if (value != null)
				{
					_avoidNullClosedColumns = (List<string>)value;
				}
				else
				{
					_avoidNullClosedColumns = new List<string>();
				}
			}

		}
		#endregion
		#region Variabels
		private List<string> _notPrintedProps = new List<string> { "LastMonth", "SupportCallOpenDateEinde", "SupportCallClosedDateNotNull", "SupportCallClosedTimeNotNull", "AssignedToQueue", "FirstEventSummary", "HoursClienteleWorkedOnSupportCall", "ClosedTabs" };
		private PropertyInfo _propHolder;
		private IEnumerable<Home> _sortChecker = Enumerable.Empty<Home>();
		private List<string> _avoidNullClosedColumns = new List<string>();
		private Home _emptyHome = new Home();
		#endregion


		public TableViewModel(IEnumerable<Home> data, Home filter)
		{
			this.Data = data.ToList<Home>();
			try
			{
				this.Properties = filter.GetProperties();
				this.PropertiesAsString = filter.getProperties();
			}
			catch (Exception e)
			{

				this.Properties = _emptyHome.GetProperties();
				this.PropertiesAsString = _emptyHome.getProperties();
			}
		}


		public bool IsShown(string prop)
		{
			if (_notPrintedProps.Contains(prop) | ClosedColumns.Contains(prop))
				return false;
			else
				return true;
		}

		public IEnumerable<Home> SortBy(IEnumerable<Home> objects, string sorter)
		{
			foreach (var prop in Properties)
			{
				string name = prop.Name;
				if (name.Equals(sorter))
				{
					this._propHolder = prop;
					break;
				}
			}
			if(!(_propHolder.Name.Equals("SupportCallOpenTime") | _propHolder.Name.Equals("SupportCallClosedTime") ))
			_sortChecker = (IEnumerable<Home>)objects.OrderBy(h => _propHolder.GetValue(h));
			else if (_propHolder.Name.Equals("SupportCallOpenTime"))
				_sortChecker = (IEnumerable<Home>)objects.OrderBy(h => h.SupportCallOpenTime.TimeOfDay);
			else if (_propHolder.Name.Equals("SupportCallClosedTime"))
				_sortChecker = (IEnumerable<Home>)objects.OrderBy(h => h.SupportCallOpenTime.TimeOfDay);


			if (_sortChecker.SequenceEqual(objects))
			{
				_sortChecker = objects.OrderByDescending(h => _propHolder.GetValue(h));
				return _sortChecker;
			}
			else
				return _sortChecker;
		}






	}
}
