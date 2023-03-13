using System.ComponentModel.DataAnnotations;

namespace FoodBox.Web.Models
{
	public class UserSingInUpViewModel
	{
		[Required(ErrorMessage = "Lütfen Kullanıcı Aınızı Giriniz")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
		public string Password { get; set; }
	}
}