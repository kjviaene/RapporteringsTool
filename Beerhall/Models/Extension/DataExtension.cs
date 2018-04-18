using System;
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
			// Verzamelen van alle properties van het meegegeven type (en het aanmaken van een DataTable)     
			DataTable table = new DataTable(typeof(T).Name);
			PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			// Add the properties as columns to the datatable
			foreach (var prop in props)
			{
				Type propType = prop.PropertyType;

				// Is it a nullable type? Get the underlying type 
				if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
					propType = new NullableConverter(propType).UnderlyingType;
				if (propType != typeof(bool) | propType.Name.Equals("SupportCallClosedDateNotNull") | propType.Name.Equals("SupportCallClosedTimeNotNull"))
					table.Columns.Add(prop.Name, propType);
			}

			// Add the property values per T as rows to the datatable
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

	}
}
