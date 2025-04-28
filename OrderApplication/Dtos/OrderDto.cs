using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Dtos
{
	public record  OrderDto(
		int Id,
		[Required,Range(1,int.MaxValue)] int Productid,
		[Required,Range(1,int.MaxValue)] int Clientid,
		[Required,Range(1,int.MaxValue)] int Quentity,
		DateTime orderdate
		
	);
}

