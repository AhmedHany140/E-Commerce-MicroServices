using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs
{
	public record ProductDto
		(
		int Id,
		[Required]string Name,
		[Required,Range(1,int.MaxValue)] int Quentity,
		[Required,DataType(DataType.Currency)] decimal Price
		);

}
