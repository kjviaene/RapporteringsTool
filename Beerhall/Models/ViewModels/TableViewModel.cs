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
		[JsonProperty]
		public string FilterString { get; set; }
		#endregion
		#region Variabels
		// Properties die niet dienen getoond worden in de tabel
		private List<string> _notPrintedProps = new List<string> {"HoursInStatusOpenDouble", "HoursInvoiceCenterDouble","HoursTillClosedDouble", "LastMonth", "SupportCallOpenDateEinde", "SupportCallClosedDateNotNull", "SupportCallClosedTimeNotNull", "AssignedToQueue", "FirstEventSummary", "HoursClienteleWorkedOnSupportCall", "ClosedTabs", "FirstFilter" };
		private PropertyInfo _propHolder;
		private IEnumerable<Home> _sortChecker = Enumerable.Empty<Home>();
		private List<string> _avoidNullClosedColumns = new List<string>();
		private Home _emptyHome = new Home();
		#endregion

		#region Constructors
		public TableViewModel()
		{
			this.Properties = _emptyHome.GetProperties();
			this.PropertiesAsString = _emptyHome.getProperties();
		}
		// injecteren van data. IEnumerable omzetten naar List
		// de try catch zorgt ervoor dat indien er een null object werd meegegeven als filter, de data van een emptyhome wordt genomen.
		public TableViewModel(IEnumerable<Home> data, Home filter)
		{
			this.Data = data.ToList<Home>();
			try
			{
				this.Properties = filter.GetProperties();
				this.PropertiesAsString = filter.getProperties();
				this.FilterString = filter.ToString();
			}
			catch (Exception e)
			{

				this.Properties = _emptyHome.GetProperties();
				this.PropertiesAsString = _emptyHome.getProperties();
				this.FilterString = "";
			}
		}
		#endregion
		// Moet het meegegeven object getoond worden in de tabel?
		public bool IsShown(string prop)
		{
			if (_notPrintedProps.Contains(prop) | ClosedColumns.Contains(prop))
				return false;
			else
				return true;
		}
		//Sorteer de objecten op basis van de meegegeven string
		public IEnumerable<Home> SortBy(IEnumerable<Home> objects, string sorter)
		{
			_sortChecker = objects;
			foreach (var s in _sortChecker)
			{
				s.RemoveNull();
			}
			// de juiste property wordt gekozen op basis van de ontvangen string, het is een string omdat in onze view met een string dient gewerkt te worden
			// dus kan geen property worden meegegeven. (Of dit kan maar we houden die logica liever hier dan in de view/controller)
			foreach (var prop in Properties)
			{
				string name = prop.Name;
				if (name.Equals(sorter))
				{
					this._propHolder = prop;
					break;
				}
			}// Als de tijden moeten apart gesorteerd worden omdat deze achter de schermen volledige DateTime objecten zijn.
			// Dus als hier op wordt gefilterd, dan wordt gefilterd op de datum en de tijd, terwijl de datum meestal verkeerd is
			// en ons enkel de tijd interreseert
			if(!(_propHolder.Name.Equals("SupportCallOpenTime") | _propHolder.Name.Equals("SupportCallClosedTime") | _propHolder.Name.Equals("HoursTillClosed") | _propHolder.Name.Equals("HoursInStatusOpen") | _propHolder.Name.Equals("HoursInvoiceCenter") | _propHolder.Name.Equals("SupportCallOpenDate") | _propHolder.Name.Equals("SupportCallClosedDate")))
			_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => _propHolder.GetValue(h));
			else if (_propHolder.Name.Equals("SupportCallClosedDate"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => DateTime.Parse(h.SupportCallClosedDate));
			else if (_propHolder.Name.Equals("SupportCallOpenDate"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => DateTime.Parse(h.SupportCallOpenDate));
			else if (_propHolder.Name.Equals("SupportCallOpenTime"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => DateTime.Parse(h.SupportCallOpenTime.Split(" ")[1]));
			else if (_propHolder.Name.Equals("SupportCallClosedTime"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => h.SupportCallClosedTimeNotNull.TimeOfDay);
			else if (_propHolder.Name.Equals("HoursTillClosed"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => h.HoursTillClosedDouble);
			else if (_propHolder.Name.Equals("HoursInStatusOpen"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => h.HoursInStatusOpenDouble);
			else if (_propHolder.Name.Equals("HoursInvoiceCenter"))
				_sortChecker = (IEnumerable<Home>)_sortChecker.OrderBy(h => h.HoursInvoiceCenterDouble);
			// Als het al eens eerder gesorteerd id moet het omgekeerd gesorteerd worden.
			if (_sortChecker.SequenceEqual(objects))
			{
				if (!(_propHolder.Name.Equals("SupportCallOpenTime") | _propHolder.Name.Equals("SupportCallClosedTime") | _propHolder.Name.Equals("HoursTillClosed") | _propHolder.Name.Equals("HoursInStatusOpen") | _propHolder.Name.Equals("HoursInvoiceCenter") | _propHolder.Name.Equals("SupportCallOpenDate") | _propHolder.Name.Equals("SupportCallClosedDate")))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => _propHolder.GetValue(h));
				else if (_propHolder.Name.Equals("SupportCallClosedDate"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => DateTime.Parse(h.SupportCallClosedDate));
				else if (_propHolder.Name.Equals("SupportCallOpenDate"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => DateTime.Parse(h.SupportCallOpenDate));
				else if (_propHolder.Name.Equals("SupportCallOpenTime"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => h.SupportCallOpenTime.Split(" ")[1]);
				else if (_propHolder.Name.Equals("SupportCallClosedTime"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => h.SupportCallClosedTimeNotNull.TimeOfDay);
				else if (_propHolder.Name.Equals("HoursTillClosed"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => h.HoursTillClosedDouble);
				else if (_propHolder.Name.Equals("HoursInStatusOpen"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => h.HoursInStatusOpenDouble);
				else if (_propHolder.Name.Equals("HoursInvoiceCenter"))
					_sortChecker = (IEnumerable<Home>)_sortChecker.OrderByDescending(h => h.HoursInvoiceCenterDouble);

				return _sortChecker;
			}
			else
				return _sortChecker;
		}






	}
}
