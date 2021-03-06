﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TrustteamVersion4.Models.Extension
{
	public static class DataExtension
	{
		// Dit is een extension method. Het fungeert als een extra methode op een bepaald Type (hier IEnumerable)
		public static DataTable ToDataTable<T>(this IEnumerable<T> items)
		{
			// Verzamelen van alle properties van het meegegeven type (en het aanmaken van een DataTable). Hier gebruiken we onze eigen
			// gedefinieerde methode van GetProperties zodanig de properties die niet in de database voorkomen ook niet in de excel
			// worden weergegeven (zoals Boolean LastMonth)
			DataTable table = new DataTable(typeof(T).Name);
			PropertyInfo[] props = typeof(T).GetFilteredProperties();

			// Toevoegen van de properties aan de kolommen van de DataTable
			foreach (var prop in props)
			{
				Type propType = prop.PropertyType;

				// Als een property Nullable is, dan voegen we de "underlying type" toe, hetzelfde type dus maar niet nullable
				if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					propType = new NullableConverter(propType).UnderlyingType;
				if (propType != typeof(bool) | propType.Name.Equals("SupportCallClosedDateNotNull") | propType.Name.Equals("SupportCallClosedTimeNotNull"))
					table.Columns.Add(prop.Name, propType);
			}

			// Toevoegen van de elk object zijn values aan de rijen
			foreach (var item in items)
			{
				var values = new object[props.Length];
				for (var i = 0; i < props.Length; i++)
					if (props[i] != typeof(bool) | !(props[i].Name.Equals("SupportCallClosedDateNotNull")) | !(props[i].Name.Equals("SupportCallClosedTimeNotNull")))
					{
						values[i] = props[i].GetValue(item, null);						
					}
				table.Rows.Add(values);
			}

			return table;
		}

		// Methode als extension op PropertyInfo, deze laat de properties met Attribute "NoPrintAttribute" links liggen
		public static PropertyInfo[] GetFilteredProperties(this Type type)
		{
			return type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(NoPrintAttribute), true).Length == 0).ToArray();
		}

		// Maken van een foreach methode voor IEnumerables
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T item in source)
				action(item);
		}
	}
}

// source for Everything but the "GetFilteredProperties" :
// https://github.com/JanKallman/EPPlus
// Source for IEnumerable
// https://stackoverflow.com/questions/1883920/call-a-function-for-each-value-in-a-generic-c-sharp-collection
