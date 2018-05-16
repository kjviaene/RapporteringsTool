using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrustteamVersion4.Models.Extension
{
	//Simpelweg een attribuut dat ervoor zorgt dat deze niet worden afgedrukt in de excelfile
	// Dit zijn properties die werden toegevoegd voor bepaalde functies (zoals de bool LastMonth)
    public class NoPrintAttribute : Attribute
    {
    }
}
