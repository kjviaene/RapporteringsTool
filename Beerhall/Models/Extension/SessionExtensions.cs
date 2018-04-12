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

	}
}
