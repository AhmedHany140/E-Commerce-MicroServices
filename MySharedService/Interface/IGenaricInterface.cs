
using ecommerce.shared.Responses;
using System.Linq.Expressions;

namespace ecommerce.shared.Interface
{
	public interface IGenaricInterface<T> where T : class
	{
		Task<Response> CreateAsync(T entity);
		Task<Response> UpdateAsync(T entity);

		Task<Response> DeleteeAsync(T entity);
		Task<T> FindByIdeAsync(int id );

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByAsync(Expression<Func<T,bool>> expression);

	}
}
