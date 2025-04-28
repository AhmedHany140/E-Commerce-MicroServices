using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.shared.Responses
{
	public record Response(bool flag=false,string message=null!);
	
}
