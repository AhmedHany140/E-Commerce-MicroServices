using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Application.Dtos
{
	public record  OrderDetails
		(
		  [Required] int OrderId,
		  [Required] int ProductId,
		  [Required] int Client,
		  [Required] string Name,
		  [Required,EmailAddress] string Email,
		  [Required] string Address,
		  [Required] string ProductName,
		  [Required] int PurchaseQuentity,
		  [Required] string PhoneNumber,
		  [Required,DataType(DataType.Currency)] decimal UnitPrice,
		  [Required, DataType(DataType.Currency)] decimal TotalPrice,
		  [Required] DateTime orderdate
		);
}
