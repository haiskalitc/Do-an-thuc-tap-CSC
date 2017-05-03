using System.ComponentModel.DataAnnotations;

namespace SystemWebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [RegularExpression("[a-zA-Z0-9]*", ErrorMessage = "Tên đăng nhập không được chứa các ký tự đặc biệt")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        public string Password { get; set; }
        public bool Result { get; set; }
    }
}