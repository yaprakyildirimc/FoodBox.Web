using System.Linq.Dynamic.Core;

namespace FoodBox.Web.Models
{
	public static class ObjectExtensions
	{
		public static ResultData<TEntity> ToDataTableResponse<TEntity>(this IList<TEntity> _data, HttpContext request)
		{
			int totalRows = _data.Count;
			int start = Convert.ToInt32(request.Request.Form["start"]);
			int length = Convert.ToInt32(request.Request.Form["length"]);
			string sortColumnName = request.Request.Form["columns[" + request.Request.Form["order[0][column]"] + "][name]"];
			string sortDirection = request.Request.Form["order[0][dir]"];
			int rowsAferFiltering = _data.Count;

			if (sortColumnName.Length > 0 && sortDirection.Length > 0)
				_data = _data.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();

			_data = _data.Skip(start).Take(length).ToList();

			return new ResultData<TEntity>()
			{
				data = _data,
				draw = request.Request.Form["draw"],
				recordsTotal = totalRows,
				recordsFiltered = rowsAferFiltering
			};
		}
	}
}