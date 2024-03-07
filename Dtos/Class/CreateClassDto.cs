using System.ComponentModel.DataAnnotations;

namespace ClassManagement2.Dtos.Class
{
    public class CreateClassDto
    {
        private string _name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên lớp không được bỏ trống")]
        [MaxLength(128, ErrorMessage = "Tên lớp không được dài quá 128 ký tự")]
        public string Name
        {
            get => _name;
            set => _name = value?.Trim();
        }
    }
}
