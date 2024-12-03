namespace NguyenVanCong_2307.Models
{
    public class DungLuongRom
    {
        public int maDungLuongRom { get; set; }

        public int kichThuocRom { get; set; }

        public int trangThai { get; set; }

        public ICollection<PhienBanSanPham> PhienBanSanPhams { get; set; }
    }
}
