using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Du_An_Cuoi_Ki_WebNC.Model

{
    [Table("nhanvien")]
    public class NhanvienModels
    {
        [Key]
        public int manv { get; set; }
        public string hoten { get; set; }
        public int gioitinh { get; set; }
        public DateOnly ngaysinh { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public int trangthai { get; set; }
    }
}
