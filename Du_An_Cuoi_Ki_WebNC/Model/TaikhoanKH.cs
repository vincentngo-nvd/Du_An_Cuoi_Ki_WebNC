using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Du_An_Cuoi_Ki_WebNC.Model
{
    [Table("taikhoankhachhang")]
    public class TaikhoanKH
    {
        [Key]
        public int makh { get; set; }
        public string matkhau { get; set; }
        public string tendangnhap { get; set; }
        public int trangthai { get; set; }
    }
}
