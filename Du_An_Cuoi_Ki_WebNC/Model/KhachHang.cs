using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace NguyenVanCong_2307.Models
{
    public class KhachHang
    {
        [Key]
        public int makh { get; set; }
        public string tenkhachhang { get; set; }
        public string diachi { get; set; } 
        public string sdt { get; set; } 
        public int trangthai { get; set; }
        public DateTime ngaythamgia { get; set; } 
    }
}
