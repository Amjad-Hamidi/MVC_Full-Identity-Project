namespace Identity1.Models.ViewModel
{
    public class UsersViewModel // UsersViewModel بدي اعرض بس ال 4 , ما بدي اعرض كلهن فبعمل 
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City {  get; set; }



        public IEnumerable<string> Roles { get; set; } // Roles اله مجموعة User كل   // IEnumerable بس رح نقرا ما رح نعدل , اذا نستخدم
    
        
        public string Id { get; set; } // Id لاني محتاج ابعث ال EditUserRole Action هاي ضفناها بالاخر عشان اقدر اعمل 
    }
}
