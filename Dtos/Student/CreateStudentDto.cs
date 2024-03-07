using System.ComponentModel.DataAnnotations;

namespace ClassManagement2.Dtos.Student
{
    public class CreateStudentDto
    {
        private string _code;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mã sinh viên không được bỏ trống")]
        [MaxLength(8, ErrorMessage = "Mã sinh viên tối đa 8 ký tự")]
        public string Code
        {
            get => _code;
            set => _code = value?.Trim();
        }

        private string _name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên sinh viên không được bỏ trống")]
        [MaxLength(128, ErrorMessage = "Tên sinh viên không được dài quá 128 ký tự")]
        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
        }

        private string _email;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email sinh viên không được bỏ trống")]
        [MaxLength(128, ErrorMessage = "Email sinh viên không được dài quá 128 ký tự")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim();
        }

        [Required(ErrorMessage = "Tuổi sinh viên không được bỏ trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Tuổi sinh viên phải lớn hơn 0")]
        public int Age { get; set; }
    }
}
