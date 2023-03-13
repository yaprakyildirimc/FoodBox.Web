namespace FoodBox.Web.Models
{
	public class ResultData<TEntity>
	{
		public IList<TEntity> data { get; set; }
		public string draw { get; set; }
		public int recordsTotal { get; set; }
		public int recordsFiltered { get; set; }
	}
}