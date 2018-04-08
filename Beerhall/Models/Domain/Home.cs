using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace TrustTeamVersion4.Models.Domain
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Home
	{
		private PropertyInfo[] _PropertyInfos = null;
		private DateTime _tempDate = DateTime.MaxValue;
		private DateTime _tempTime = DateTime.MaxValue;
		// Alle kollom namen
		// De JsonProperty tag zorgt ervoor dat al deze properties worden geserialized. Anders krijg je gewoon een lege string na serialization. 
		[JsonProperty]
		[Display(Name = "Maand")]
		public Double? Month { get; set; }
		[JsonProperty]
		[Display(Name = "Jaartal")]
		public Double? Year { get; set; }
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
		[Display(Name = "Nummer ticket")]
		public Double? Number { get; set; }
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
		public DateTime SupportCallOpenDate{ get; set;}
		[JsonProperty]
		[NotMapped]
		public DateTime SupportCallOpenDateEinde { get; set; }
		[JsonProperty]
		public DateTime SupportCallOpenTime{ get; set;}
		[JsonProperty]
		public DateTime? SupportCallClosedDate { get; set; }
		// De tag "NotMapped" is er omdat anders SQL een error geeft aangezien er geen gelijknamige kolom bestaat in de databank
		// Deze property is echter vereist als we met deze data willen werken (uitleg zie onderaan)
		[JsonProperty]
		[NotMapped]
		public DateTime SupportCallClosedDateNotNull {

			get { return _tempDate; }
			set { if (value != null)
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
		public DateTime? SupportCallClosedTime{get; set;}
		// De tag "NotMapped" is er omdat anders SQL een error geeft aangezien er geen gelijknamige kolom bestaat in de databank
		// Deze property is echter vereist als we met deze data willen werken (uitleg zie onderaan)
		[JsonProperty]
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
		public string HoursTillClosed { get; set; }
		[JsonProperty]
		[Display(Name = "Hours in status open")]
		public string HoursInStatusOpen { get; set; }
		[JsonProperty]
		[Display(Name = "hours invoice center")]
		public string HoursInvoiceCenter { get; set; }
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
		public Double? InvoiceOrganizationNumber { get; set; }
		[JsonProperty]
		[Display(Name = "Status invoice")]
		public string InvoiceStatus { get; set; }
		[JsonProperty]
		[Display(Name = "Hours clientele worked on suppor call")]
		public string HoursClienteleWorkedOnSupportCall { get; set; }

		// Het aanpassen van de ToString zodanig deze alle properties returnt met steeds de waarde erbij in volgend formaat:
		// Property: value, Property: value,...
		//Indien dit wordt opgevraagd op een instantie waar alle properties null zijn dan zal er gewoon een / geretourneerd worden
		public override string ToString()
		{
			String result;
			if (_PropertyInfos == null)
				_PropertyInfos = this.GetType().GetProperties();

			var sb = new StringBuilder();
			
			foreach (var info in _PropertyInfos)
			{
				var value = info.GetValue(this, null) ?? "null";
				if (!(value.Equals("null")))
				{
					// Controle om te zorgen dat de DateTime niet sowieso wordt afgeprint omdat dit nooit null is maar wordt ingesteld op de 
					// min en max waarde
					if (!(value.Equals(DateTime.MaxValue) | value.Equals(DateTime.MinValue)))
					{
						if (value.ToString() == SupportCallOpenDate.ToString())
						{
							sb.Append("Begin periode: " + value.ToString().Remove(10) + ", ");
						}
						else if (value.ToString() == SupportCallOpenDateEinde.ToString())
						{
							sb.Append("Einde periode: " + value.ToString().Remove(10) + ", ");
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
				result = "/";
			}
			return result;
		}
		// Het retournenen van alle attributen als strings in een lijst
		public SelectList getAttributen() {
			List<String> temp = new List<String>();
			var properties = this.GetType().GetProperties();
			foreach (var info in properties)
			{
				temp.Add(info.Name);
			}

			SelectList result = new SelectList(temp);
			return result;
		}
		// Deze methode is er puur om te zorgen dat we een property hebben waar we de closed data vinden waar die nooit null kan zijn. Ze wordt ingesteld op de
		// maximum waarde van DateTime in de plaats als ze null is, zo is ze wel nog steeds gemakkelijk te onderscheiden van de rest.
		// Door dat er in de databank vaak null staat in deze kollom kon dit niet gewoon opgelost worden met 1 property omdat we zowel van DateTime? moeten gebruik
		// maken (voor het inlezen van de data) als van DateTime (voor het gebruiken van de data).
		public void SetDateNotNull()
		{
			if (this.SupportCallClosedDate == null)
			{
				this.SupportCallClosedDateNotNull = DateTime.MinValue;
			}
			if(this.SupportCallClosedDate != null)
			{
				this.SupportCallClosedDateNotNull = (DateTime)this.SupportCallClosedDate;
			}

			if (this.SupportCallClosedTime == null)
			{

				this.SupportCallClosedTimeNotNull = DateTime.MaxValue;
			}
			if (this.SupportCallClosedTime != null)
			{
				this.SupportCallClosedTimeNotNull = (DateTime)this.SupportCallClosedTime;
			}
		}

	}
}
