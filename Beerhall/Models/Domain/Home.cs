﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using TrustteamVersion4.Models.Extension;

namespace TrustTeamVersion4.Models.Domain
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Home
	{
		#region Properties
		private PropertyInfo[] _PropertyInfos = null;
		private DateTime _tempDate = DateTime.MaxValue;
		private DateTime _tempTime = DateTime.MaxValue;
		private List<string> _tempList = new List<string>();

		// Alle kolom namen
		// De JsonProperty tag zorgt ervoor dat al deze properties worden geserialized. Anders krijg je gewoon een lege string na serialization. 
		// De NoPrintAttribute tag zorgt ervoor dat deze property niet in de excel wordt weergegeven.
		[JsonProperty]
		[Display(Name = "Nummer ticket")]
		public int Number { get; set; }
		[JsonProperty]
		[NotMapped]
		[NoPrintAttribute]
		public bool LastMonth { get; set; }
		[JsonProperty]
		[Display(Name = "Maand")]
		public int Month { get; set; }
		[JsonProperty]
		[Display(Name = "Jaartal")]
		public int Year { get; set; }
		[JsonProperty]
		[Display(Name = "Nummer Organisatie")]
		public string OrganizationNumber { get; set; }
		[JsonProperty]
		[Display(Name = "Naam Organisatie")]
		public string InvoicCenterOrganization { get; set; }
		[JsonProperty]
		[Display(Name = "Groep naam")]
		public string GroupName { get; set; }
		[JsonProperty]
		[Display(Name = "Naam persoon")]
		public string PersonName { get; set; }
		[JsonProperty]
		[Display(Name = "Support Call Type")]
		public string SupportCallType { get; set; }
		[JsonProperty]
		[Display(Name = "Prioriteit")]
		public string SupportCallPriority { get; set; }
		[JsonProperty]
		[Display(Name = "Impact")]
		public string SupportCallImpact { get; set; }
		[JsonProperty]
		[Display(Name = "Urgentie")]
		public string SupportCallUrgency { get; set; }
		[JsonProperty]
		[Display(Name = "Status")]
		public string SupportCallStatus { get; set; }
		[JsonProperty]
		[Display(Name = "Categorie")]
		public string SupportCallCategory { get; set; }
		[JsonProperty]
		//Deze dates zijn nooit null. Is verplicht om in te vullen bij de tickets, dus hier zijn geen maatregelen nodig.
		public string SupportCallOpenDate { get; set; }
		[JsonProperty]
		[NotMapped]
		public DateTime SupportCallOpenDateEinde { get; set; }
		[JsonProperty]
		public string SupportCallOpenTime { get; set; }
		[JsonProperty]
		public string SupportCallClosedDate { get; set; }
		// De tag "NotMapped" is er omdat anders SQL een error geeft aangezien er geen gelijknamige kolom bestaat in de databank
		// Deze property is echter vereist als we met deze data willen werken (uitleg zie onderaan)
		[JsonProperty]
		[NoPrintAttribute]
		[NotMapped]
		public DateTime SupportCallClosedDateNotNull
		{

			get { return _tempDate; }
			set
			{
				if (value != null)
				{
					_tempDate = (DateTime)value;
				}
				else
				{
					_tempDate = DateTime.MaxValue;
				}
			}

		}
		[JsonProperty]
		[Display(Name = "Tijdstip sluiting")]
		public string SupportCallClosedTime { get; set; }
		// De tag "NotMapped" is er omdat anders SQL een error geeft aangezien er geen gelijknamige kolom bestaat in de databank
		// Deze property is echter vereist als we met deze data willen werken (uitleg zie onderaan)
		[JsonProperty]
		[NoPrintAttribute]
		[NotMapped]
		public DateTime SupportCallClosedTimeNotNull
		{

			get { return _tempTime; }
			set
			{
				if (value != null)
				{
					_tempTime = (DateTime)value;
				}
				else
				{
					_tempTime = DateTime.MaxValue;
				}
			}
		}
		[JsonProperty]
		[Display(Name = "Hours Till Closed")]
		public decimal? HoursTillClosed { get; set; }
		[JsonProperty]
		[Display(Name = "Hours in status open")]
		public decimal? HoursInStatusOpen { get; set; }
		[JsonProperty]
		[Display(Name = "hours invoice center")]
		public decimal? HoursInvoiceCenter { get; set; }
		[JsonProperty]
		[Display(Name = "Samenvatting")]
		public string InvoiceCallSummary { get; set; }
		[JsonProperty]
		[Display(Name = " Geopend door")]
		public string OpenedByUser { get; set; }
		[JsonProperty]
		[Display(Name = "Toegewezen gebruiker")]
		public string AssignedToUser { get; set; }
		[JsonProperty]
		[Display(Name = "Toegewijzen queue")]
		public string AssignedToQueue { get; set; }
		[JsonProperty]
		[Display(Name = "First even summary")]
		public string FirstEventSummary { get; set; }
		[JsonProperty]
		[Display(Name = "Clientele support call summary")]
		public string ClienteleSupportCallSummary { get; set; }
		[JsonProperty]
		[Display(Name = "Invoice organization name")]
		public string InvoiceOrganizationName { get; set; }
		[JsonProperty]
		[Display(Name = "Invoice Organization Number")]
		public int? InvoiceOrganizationNumber { get; set; }
		[JsonProperty]
		[Display(Name = "Status invoice")]
		public string InvoiceStatus { get; set; }
		[JsonProperty]
		[Display(Name = "Hours clientele worked on suppor call")]
		public decimal HoursClienteleWorkedOnSupportCall { get; set; }
		[JsonProperty]
		[NoPrintAttribute]
		[NotMapped]
		public double HoursTillClosedDouble { get; set; }
		[JsonProperty]
		[NoPrintAttribute]
		[NotMapped]
		public double HoursInStatusOpenDouble { get; set; }
		[JsonProperty]
		[NoPrintAttribute]
		[NotMapped]
		public double HoursInvoiceCenterDouble { get; set; }
		[JsonProperty]
		[NoPrintAttribute]
		[NotMapped]
		public bool FirstFilter { get; set; }
		#endregion
		#region ToString
		// Het aanpassen van de ToString zodanig deze alle properties returnt met steeds de waarde erbij in volgend formaat:
		// Property: value, Property: value,...
		//Indien dit wordt opgevraagd op een instantie waar alle properties null zijn dan zal er gewoon een / geretourneerd worden
		public override string ToString()
		{
			String result;
			_PropertyInfos = this.GetType().GetFilteredProperties();

			var sb = new StringBuilder();

			foreach (var info in _PropertyInfos)
			{
				var value = info.GetValue(this, null) ?? "null";
				if (!(value.Equals("null")) & !(value.Equals(true)) & !(value.Equals(false)) & !(value.Equals(0)) & !(value.Equals("0")) & !(value.Equals(Decimal.Zero)))
				{
					// Controle om te zorgen dat de DateTime niet sowieso wordt afgeprint omdat dit nooit null is maar wordt ingesteld op de 
					// min en max waarde
					if (!(value.Equals(DateTime.MaxValue) | value.Equals(DateTime.MinValue)))
					{
						if (value.ToString() == SupportCallOpenDate)
						{
							sb.Append("Begin periode: " + value.ToString() + ", ");
						}
						else if (value.ToString() == SupportCallOpenDateEinde.ToString())
						{
							if(value.ToString().Length > 10)
								sb.Append("Einde periode: " + value.ToString().Remove(10) + ", ");
							else
								sb.Append("Einde periode: " + value.ToString() + ", ");
						}
						else
						{
							sb.Append(info.Name + ": " + value.ToString() + ", ");
						}
					}
				}
			}
			// Het wegknippen van de laatste komma en spatie (indien er 1 of meer properties niet null waren
			if (sb.Length > 1)
			{
				result = sb.ToString().Remove(sb.Length - 2);
			}
			else
			{
				result = "";
			}
			return result;
		}
		#endregion
		#region Other methods
		// Het retournenen van alle properties als strings in een lijst
		public List<string> getProperties()
		{
			List<string> temp = new List<String>();
			var properties = this.GetType().GetProperties();
			foreach (var info in properties)
			{
				temp.Add(info.Name);
			}

			return temp;
		}
		//Opvragen van één property op basis van string
		public PropertyInfo getProperty(string prop)
		{
			try
			{
				IEnumerable<PropertyInfo> _temp = this.GetProperties().Where(x => x.Name.Equals(prop));
				return _temp.First();
			}
			catch (Exception e)
			{
				throw new Exception("Property not found : " + e.Message);
			}
		}
		// Deze methode is er puur om te zorgen dat we een property hebben waar we de closed data vinden waar die nooit null kan zijn. Ze wordt ingesteld op de
		// maximum waarde van DateTime in de plaats als ze null is, zo is ze wel nog steeds gemakkelijk te onderscheiden van de rest.
		// Door dat er in de databank vaak null staat in deze kollom kon dit niet gewoon opgelost worden met 1 property omdat we zowel van DateTime? moeten gebruik
		// maken (voor het inlezen van de data) als van DateTime (voor het gebruiken van de data).
		public void SetDateNotNull()
		{
			if (this.SupportCallClosedDate == null)
			{
				this.SupportCallClosedDateNotNull = DateTime.MaxValue;
				this.SupportCallClosedDate = "2999-01-01";
			}
			if (this.SupportCallClosedDate != null)
			{
				this.SupportCallClosedDateNotNull = DateTime.Parse(this.SupportCallClosedDate);
			}

			if (this.SupportCallClosedTime == null)
			{

				this.SupportCallClosedTimeNotNull = DateTime.MaxValue;
				this.SupportCallClosedTime = "2999-01-01";
			}
			if (this.SupportCallClosedTime != null)
			{
				this.SupportCallClosedTimeNotNull = DateTime.Parse(this.SupportCallClosedTime);
			}
		}
		// Zorgt ervoor dat alle double waarden niet null referentie zijn maar gewoon 0.
		public void SetDoublesNotNull()
		{
			if (this.Year == null)
				this.Year = 0;
			if (this.Month == null)
				this.Month = 0;
			if (this.Number == null)
				this.Number = 0;
			if (this.InvoiceOrganizationNumber == null)
				this.InvoiceOrganizationNumber = 0;
		}

		// Controle om te kijken of een instantie van dit object enkel nulls en de standaardwaarden voor de datums bevat. Maw, of dit object "leeg" is.
		public bool IsEmptyObject()
		{
			bool result = true;
			int check = 0;
			_PropertyInfos = this.GetType().GetProperties();
			// De lus overloopt alle properties, als er eentje hiervan niet gelijk is aan null of de standaardwaarde, dan zal check gelijk worden gesteld aan 1
			foreach (var prop in _PropertyInfos)
			{
				var value = prop.GetValue(this, null) ?? "null";
				if (!(value.Equals("null")) & !(value.Equals(false)) & !(value.ToString().Equals("0")))
				{
					if (!(value.Equals(DateTime.MaxValue) | value.Equals(DateTime.MinValue) | value.Equals(new List<int>())))
					{
						check = 1;
						break;
					}

				}
			}
			if (check == 1)
				result = false;

			return result;
		}
		//I'm tired of these m*therf*cking nullpointers in my m*therf*cking code!
		public void MakeDoublesActualDoubles()
		{
			if (this.HoursTillClosed.Equals("") | this.HoursTillClosed.Equals("NULL") | this.HoursTillClosed == null)
				HoursTillClosedDouble = 0.00;
			else
				HoursTillClosedDouble = (double)(HoursTillClosed);
			if (this.HoursInStatusOpen.Equals("") | this.HoursInStatusOpen.Equals("NULL") | this.HoursInStatusOpen == null)
				HoursInStatusOpenDouble = 0.00;
			else
				HoursInStatusOpenDouble = (double)HoursInStatusOpen;
			if (this.HoursInvoiceCenter.Equals("") | this.HoursInvoiceCenter.Equals("NULL") | this.HoursInvoiceCenter == null)
				HoursInvoiceCenterDouble = 0.00;
			else
				HoursInvoiceCenterDouble = (double)(HoursInvoiceCenter);
		}
		//making sure if a string is null it's converted into a string value "null"
		public void RemoveNull()
		{
			this.SetDateNotNull();
			this.SetDoublesNotNull();
			PropertyInfo[] _Properties = this.GetType().GetProperties();
			foreach (var prop in _Properties)
			{
				if (prop.GetValue(this) == null)
				{
					if(prop.PropertyType == typeof(String))
						prop.SetValue(this, "NULL");
					if (prop.PropertyType == typeof(Decimal?))
						prop.SetValue(this,(Decimal)0.0);
				}

			}// moet erna want string mag geen null meer zijn
			string hey = "stop";
			this.MakeDoublesActualDoubles();
		}
		// Controleren of de bool LastMonth true is, zoja, dan worden de correcte properties ingesteld op de juiste data.
		public void CheckAndSetLastMonth()
		{
			int substractMonth = -(1);
			if (this.LastMonth)
			{
				this.SupportCallOpenDate = DateTime.Now.AddMonths(substractMonth).ToString("yyyy-MM-dd");
				this.SupportCallOpenDateEinde = DateTime.Now;
			}
		}
		// Een methode voor alle properties op te vragen als PropertyInfo type
		public PropertyInfo[] GetProperties()
		{
			PropertyInfo[] value = this.GetType().GetProperties();

			return value;

		}
		// Een methode voor het knippen van de data naar gewenste strings.
		public Dictionary<string, string> CutDates()
		{
			this.SetDateNotNull();
			Dictionary<string, string> cutDate = new Dictionary<string, string> { };
			cutDate.Add("SupportCallOpenDate", this.SupportCallOpenDate);
			cutDate.Add("SupportCallOpenTime", this.SupportCallOpenTime);
			cutDate.Add("SupportCallClosedDate", this.SupportCallClosedDateNotNull.ToShortDateString());
			cutDate.Add("SupportCallClosedTime", this.SupportCallClosedTimeNotNull.ToShortTimeString());
			return cutDate;
		}
		#endregion
	}
}
