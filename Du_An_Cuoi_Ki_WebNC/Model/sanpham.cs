using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NguyenVanCong_2307.Models
{
    public class sanpham
    {
        [Key]
        public int masp { get; set; }
        public string tensp { get; set; }
        public string hinhanh { get; set; }
        public int? xuatxu { get; set; }
        public string chipxuly {  get; set; }
        public int? dungluongpin { get; set; }
        public double? kichthuocman { get; set; }
        public int? hedieuhanh { get; set; }
        public int? phienbanhdh { get; set; }
        public string camerasau { get; set; }
        public string cameratruoc { get; set; }
        public int? thoigianbaohanh { get; set; }
        public int? thuonghieu { get; set; }
        public int? khuvuckho { get; set; }
        public int soluongton { get; set; }
        public byte trangthai { get; set; } = 1;

        [ForeignKey("thuonghieu")]
        public virtual ThuongHieus? ThuongHieus { get; set; }

        public ICollection<PhienBanSanPham> PhienBanSanPhams { get; set; }
    }
}
