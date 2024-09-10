using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity1.Models.ViewModel
{
	public class EditUserRoleViewModel // Id user,Select option roles,selected role اني اعرضله صفحة تحتوي على EditUserRole Action هاي سبب انشاءها : اني اربطها مع ال 
	{								   // View Model وهذا الشي غير متوفر , لذلك انا مجبر اعمل   
		public string Id { get; set; }


		public IEnumerable<SelectListItem> RolesList { get; set; } // View يعني بس بستفيد منها في  ,select option اعملي HTML هاي رسالة لل 
		public string? SelectedRoles { get; set; } // (?) مش رح تكون ماخذة قيمة, لذلك عملت view اول ما افتح ال
	
	}
}
