using System.ComponentModel.DataAnnotations;

namespace Identity1.Models.ViewModel
{
	public class RoleViewModel // ما بضيف عليها اشي Role لانه بالعادة ال  , User لالها, زي ما عملنا لل ApplicationRole ما رح اعمل
	{
		[Required]
		public string RoleName { get; set; }
	}
}
