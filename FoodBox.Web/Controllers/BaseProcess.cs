using FoodBox.Core.Services;
using FoodBox.Web.Models;
using FoodBox.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarkodDProject.Web.Controllers
{
	public abstract class BaseProcess<TEntity, TService> : Controller
		where TEntity : class
		where TService : IService<TEntity>
	{
		private readonly TService service;

		public BaseProcess(TService service)
		{
			this.service = service;
		}

		[HttpGet]
		public async virtual Task<IActionResult> Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var data = await service.GetAll();

			return new OkObjectResult(data);
		}

		[HttpGet]
		public async Task<IActionResult> GetById(Guid Id)
		{
			var data = await service.GetById(Id);

			return new OkObjectResult(data);
		}

		public virtual async Task<IActionResult> GetBasicJsonList()
		{
			DataTableFilterService<TEntity> filterService = new DataTableFilterService<TEntity>();

			var expression = filterService.FilterWithExpression(Request.HttpContext);
			var result = await service.GetAll(expression); // ToDataTableResponse(Request);
			return new OkObjectResult(result.ToList().ToDataTableResponse(Request.HttpContext));
		}

		[HttpPost]
		public async virtual Task<IActionResult> Create(TEntity entity)
		{
			var save = await service.Create(entity);
			return new OkObjectResult(true);
		}

		[HttpPost]
		public async Task<IActionResult> Update(TEntity entity, Guid Id)
		{
			await service.Update(entity);

			var entityRecord = await service.GetById(Id);

			return new OkObjectResult(true);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid Id)
		{
			var item = await service.GetById(Id);
			await service.Delete(item);
			return new OkObjectResult(true);
		}
	}
}