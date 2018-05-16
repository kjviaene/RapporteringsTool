using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TrustTeamVersion4.Models.Domain;

namespace TrustteamVersion4.Models.Extension
{

    public static class SessionExtensions
    {
		#region IEnumerable homes
		//Het schrijven van een object naar een sessie

		public static void SetObject<IEnumerable>(this ISession session, string key, IEnumerable<Home> value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));

		}
		
		//Het lezen van een object uit een sessie
		public static IEnumerable<Home> GetObject<IEnumerable>(this ISession session, string key)
		{
			var value = session.GetString(key);
			if (value == null)
			{ return Enumerable.Empty<Home>(); }
			return JsonConvert.DeserializeObject<IEnumerable<Home>>(value);
		}

		#endregion

		#region Home
		// Het schrjiven van een object van type T
		public static void SetObject<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));

		}
		//Het opvragen van één enkel object. Als dit object niet bestaat wordt een lege string gedeserialiseerd
		public static T GetObjectSingle<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			if (value == null)
			{ value = "";  }
			return JsonConvert.DeserializeObject<T>(value);
		}
		#endregion
		// Opragen van een dictionary die bestaat uit een string en een lijst die zelf nog objecten bevat.
		public static IDictionary<string,List<object>> GetObjectDict<IDictionary>(this ISession session, string key)
		{
			var value = session.GetString(key);
			if (value == null)
			{ return new Dictionary<string, List<object>>(); }
			return JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(value);
		}

	}
}
