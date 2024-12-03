using System.ComponentModel.DataAnnotations;

namespace NguyenVanCong_2307.Models
{
    public class ThuongHieus
    {
        [Key]
        public int mathuonghieu { get; set; }
        public string tenthuonghieu { get; set; }
        public byte trangthai { get; set; } = 1;

        // Thuộc tính điều hướng ngược để lấy danh sách sản phẩm liên quan
        public virtual ICollection<sanpham> SanPham { get; set; }
    }
}
