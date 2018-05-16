using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrustTeamVersion4.Models.Domain
{// Een gewone Dictionary heeft 1 Key gelinkt met 1 value, hier zorgen we ervoor dat 1 value altijd 2 keys heeft.
	// Er zijn andere mogelijkheden om dit te doen maar dit leek mij de meest eenvoudige. Dit is in principe gewoon steeds een Dictionary met een Dictionary
	// als value. Maar om dit steeds uit te typen is het eenvoudiger van hiervoor een extra klasse aan te maken.
	// De code schreef ik niet zelf, de source wordt onderaan deze klasse vermeld
	// De klasse is gebaseerd op een Dictionary die een normale key heeft en een Dictionary als value
	public class MultiKeyDictionary<K1, K2, V> : Dictionary<K1, Dictionary<K2, V>>
	{

		public V this[K1 key1, K2 key2]
		{
			get
			{// 1 van de twee keys is niet aanwezig? Exception!
				if (!ContainsKey(key1) || !this[key1].ContainsKey(key2))
					throw new ArgumentOutOfRangeException();
				return base[key1][key2];
			}
			set
			{// Instellen van een value. Als key1 nog niet bestaat, dan creëren we een nieuwe dictionary met als key de meegegeven key
				if (!ContainsKey(key1))
					this[key1] = new Dictionary<K2, V>();
				this[key1][key2] = value;
			}
		}
		// Idem set methode van V
		public void Add(K1 key1, K2 key2, V value)
		{
			if (!ContainsKey(key1))
				this[key1] = new Dictionary<K2, V>();
			this[key1][key2] = value;
		}
		// Controleren of deze tweek eys in combinatie voorkomen in deze dictionary
		public bool ContainsKey(K1 key1, K2 key2)
		{
			return base.ContainsKey(key1) && this[key1].ContainsKey(key2);
		}
		// selecteer alle values (dus eerst de value = eerste dictionary en dan de values van al die dictionaries)
		public new IEnumerable<V> Values
		{
			get
			{
				return from baseDict in base.Values
					   from baseKey in baseDict.Keys
					   select baseDict[baseKey];
			}
		}

	}

}

// source: https://stackoverflow.com/questions/1171812/multi-key-dictionary-in-c
