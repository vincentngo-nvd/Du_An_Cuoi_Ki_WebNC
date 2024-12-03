using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace NguyenVanCong_2307.Models
{
    public class PhieuXuat
    {
        [Key]
        public int maphieuxuat { get; set; }
        public DateTime thoigian { get; set; }
        public long tongtien { get; set; }
        public int makh { get; set; } 
        public int trangthai { get; set; }
        public int? manv { get; set; }

        [ForeignKey("makh")]
        public virtual KhachHang? KhachHang { get; set; }
    }
}
