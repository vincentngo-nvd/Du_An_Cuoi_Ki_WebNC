namespace NguyenVanCong_2307.Models
{
    public class CTSanPham
    {
        public int maImei { get; set; }

        public int maPhienBanSanPham { get; set; }

        public int maPhieuNhap { get; set; }

        public int maPhieuXuat { get; set; }

        public int tinhTrang { get; set; }

        public PhienBanSanPham PhienBanSanPham { get; set; }
    }
}
