using System.Linq.Expressions;

namespace ZM.Application.Common.Extensions;
public static class LinqExtensions
{
	public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
		=> condition ? query.Where(predicate) : query;
}
