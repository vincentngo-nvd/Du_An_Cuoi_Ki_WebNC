namespace NguyenVanCong_2307.Models
{
    public class DungLuongRam
    {
        public int maDungLuongRam { get; set; }

        public int kichThuocRam { get; set; }

        public int trangThai { get; set; }

        public ICollection<PhienBanSanPham> PhienBanSanPhams { get; set; }
    }
}
