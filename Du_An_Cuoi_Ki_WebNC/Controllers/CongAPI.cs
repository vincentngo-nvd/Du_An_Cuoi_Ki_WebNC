using Du_An_Cuoi_Ki_WebNC.DTO;
using Du_An_Cuoi_Ki_WebNC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanCong_2307.Models;
using System.Collections.Specialized;
namespace Du_An_Cuoi_Ki_WebNC.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class CongAPI : ControllerBase
    {
        private readonly Dbcontext _dbContext;
        public CongAPI(Dbcontext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPut("UpdateSanPham")]
        public async Task<ActionResult> UpdateSanPham(int masp, [FromBody] sanpham updatedSanPham)
        {
            if (_dbContext.sanpham == null)
            {
                return NotFound();
            }

            // Tìm sản phẩm theo ID
            var existingSanPham = await _dbContext.sanpham.FindAsync(masp);
            if (existingSanPham == null)
            {
                return NotFound(); // Trả về 404 nếu sản phẩm không tồn tại
            }

            // Cập nhật thông tin sản phẩm
            existingSanPham.tensp = updatedSanPham.tensp;
            existingSanPham.hinhanh = updatedSanPham.hinhanh;
            existingSanPham.xuatxu = updatedSanPham.xuatxu;
            existingSanPham.chipxuly = updatedSanPham.chipxuly;
            existingSanPham.dungluongpin = updatedSanPham.dungluongpin;
            existingSanPham.kichthuocman = updatedSanPham.kichthuocman;
            existingSanPham.hedieuhanh = updatedSanPham.hedieuhanh;
            existingSanPham.phienbanhdh = updatedSanPham.phienbanhdh;
            existingSanPham.camerasau = updatedSanPham.camerasau;
            existingSanPham.cameratruoc = updatedSanPham.cameratruoc;
            existingSanPham.thoigianbaohanh = updatedSanPham.thoigianbaohanh;
            existingSanPham.thuonghieu = updatedSanPham.thuonghieu; // Nếu có khóa ngoại
            existingSanPham.khuvuckho = updatedSanPham.khuvuckho;
            existingSanPham.soluongton = updatedSanPham.soluongton;
            existingSanPham.trangthai = updatedSanPham.trangthai;

            // Cập nhật vào cơ sở dữ liệu
            _dbContext.sanpham.Update(existingSanPham);
            await _dbContext.SaveChangesAsync(); // Lưu thay đổi

            return NoContent(); // Trả về 204 No Content
        }

            [HttpGet("ListSanPham")]
            public async Task<ActionResult<IEnumerable<object>>> MonAns() // Change return type to IEnumerable<object>
            {
                if (_dbContext.sanpham == null)
                {
                    return NotFound();
                }

                var sanPhamList = await _dbContext.sanpham
                    .Include(sp => sp.ThuongHieus) // Include the related ThuongHieu entity
                    .ToListAsync();

                var result = sanPhamList.Select(sp => new
                {
                    sp.masp, // Ensure property names match your model
                    sp.tensp,
                    sp.hinhanh,
                    sp.xuatxu,
                    sp.chipxuly,
                    sp.dungluongpin,
                    sp.kichthuocman,
                    sp.hedieuhanh,
                    sp.phienbanhdh,
                    sp.camerasau,
                    sp.cameratruoc,
                    sp.thoigianbaohanh,
                    ThuongHieus = sp.thuonghieu != null ? sp.ThuongHieus.tenthuonghieu : null, // Get the brand name
                    sp.khuvuckho,
                    sp.soluongton,
                    sp.trangthai
                }).ToList(); // Use ToList() here instead

                return Ok(result); // Return the result with Ok()
            }

        [HttpGet("ListSanPhamWeb")]
        public async Task<ActionResult<IEnumerable<object>>> ListSanPhamWeb()
        {
            if (_dbContext.PhienBanSanPhams == null || _dbContext.sanpham == null)
            {
                return NotFound();
            }

            var sanPhamWebList = await _dbContext.PhienBanSanPhams
                .Include(pb => pb.SanPham) // Bao gồm thông tin sản phẩm liên kết
                .Select(pb => new
                {
                    hinhanh = pb.SanPham.hinhanh, // Hình ảnh từ bảng sanpham
                    tensp = pb.SanPham.tensp, // Tên sản phẩm từ bảng sanpham
                    kichthuocman = pb.SanPham.kichthuocman, // Kích thước màn hình từ bảng sanpham
                    ram = pb.ram, // RAM từ bảng phienbansanpham
                    rom = pb.rom, // ROM từ bảng phienbansanpham
                    giaNhap = pb.giaNhap, // Giá nhập từ bảng phienbansanpham
                    giaXuat = pb.giaXuat // Giá xuất từ bảng phienbansanpham
                })
                .Distinct() // Loại bỏ các bản ghi trùng lặp
                .ToListAsync();

            return Ok(sanPhamWebList); // Trả về dữ liệu dưới dạng JSON
        }

        [HttpGet("ListSanPhamByBrand")]
        public async Task<ActionResult<IEnumerable<object>>> ListSanPhamByBrand(int brandId)
        {
            var sanPhamList = await _dbContext.PhienBanSanPhams
                .Include(pb => pb.SanPham)
                .ThenInclude(sp => sp.ThuongHieus)
                .Where(pb => pb.SanPham.ThuongHieus != null
                             && pb.SanPham.ThuongHieus.mathuonghieu == brandId)
                .Select(pb => new
                {
                    hinhanh = pb.SanPham.hinhanh,
                    tensp = pb.SanPham.tensp,
                    kichthuocman = pb.SanPham.kichthuocman,
                    ram = pb.ram,
                    rom = pb.rom,
                    giaNhap = pb.giaNhap,
                    giaXuat = pb.giaXuat
                })
                .Distinct()
                .ToListAsync();

            return Ok(sanPhamList);
        }

        [HttpGet("SearchSanPham")]
        public async Task<IActionResult> SearchSanPham(int id)
        {
            // Kiểm tra xem DbSet có tồn tại không
            if (_dbContext.sanpham == null)
            {
                return NotFound();
            }

            // Tìm sản phẩm theo msp
            var product = await _dbContext.sanpham
                .Include(sp => sp.ThuongHieus) 
                .FirstOrDefaultAsync(sp => sp.masp == id);

            if (product == null)
            {
                return NotFound(); // Hoặc bạn có thể trả về một thông báo tùy chỉnh
            }

            // Kiểm tra xem sản phẩm có tồn tại không
            var result = new
            {
                product.masp,
                product.tensp,
                product.hinhanh,
                product.xuatxu,
                product.chipxuly,
                product.dungluongpin,
                product.kichthuocman,
                product.hedieuhanh,
                product.phienbanhdh,
                product.camerasau,
                product.cameratruoc,
                product.thoigianbaohanh,
                ThuongHieus = product.thuonghieu != null ? product.ThuongHieus.tenthuonghieu : null,
                product.khuvuckho,
                product.soluongton,
                product.trangthai
            };
            return Ok(result); // Return the result with Ok()
        }

        [HttpPost("InsertSanPham")]
        public async Task<ActionResult<sanpham>> ThemsanPham([FromBody] sanpham sp)
        {
            // Kiểm tra dữ liệu đầu vào
            if (sp == null || string.IsNullOrEmpty(sp.tensp))
            {
                return BadRequest("Dữ liệu sản phẩm không hợp lệ.");
            }

            // Tạo một đối tượng mới để thêm vào DbContext
            var newSanPham = new sanpham
            {
                masp = sp.masp,
                tensp = sp.tensp,
                hinhanh = sp.hinhanh,
                xuatxu = sp.xuatxu,
                chipxuly = sp.chipxuly,
                dungluongpin = sp.dungluongpin,
                kichthuocman = sp.kichthuocman,
                hedieuhanh = sp.hedieuhanh,
                phienbanhdh = sp.phienbanhdh,
                camerasau = sp.camerasau,
                cameratruoc = sp.cameratruoc,
                thoigianbaohanh = sp.thoigianbaohanh,
                thuonghieu = sp.thuonghieu, // Nếu có
                khuvuckho = sp.khuvuckho,
                soluongton = sp.soluongton,
                trangthai = sp.trangthai
            };

            // Thêm sản phẩm vào DbContext
            _dbContext.sanpham.Add(newSanPham);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra khi lưu sản phẩm: " + message);
            }

            return CreatedAtAction(nameof(SearchSanPham), new { id = newSanPham.masp }, newSanPham);
        }
        [HttpDelete("DeleteSanPham")]
        public async Task<IActionResult> DeleteSP(int id)
        {
            if (_dbContext.sanpham == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.sanpham.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbContext.sanpham.Remove(brand);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("InsertThuongHieu")]
        public async Task<ActionResult<ThuongHieus>> PostBrand(ThuongHieus thuonghieu)
        {
            _dbContext.thuonghieu.Add(thuonghieu);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(SearchSanPham), new { id = thuonghieu.mathuonghieu }, thuonghieu);
        }
        [HttpGet("ListThuongHieu")]
        public async Task<ActionResult<IEnumerable<ThuongHieus>>> GetBrands()
        {
            if (_dbContext.thuonghieu == null)
            {
                return NotFound();
            }
            var brands = await _dbContext.thuonghieu
            .Select(th => th.tenthuonghieu) // Chọn trường tenthuonghieu
            .ToListAsync();

            return Ok(brands); // Trả về danh sách tên thương hiệu
        }
        [HttpDelete("DeleteAllSanPham")]
        public async Task<IActionResult> DeleteAllSanPham()
        {
            if (_dbContext.sanpham == null)
            {
                return NotFound("Không tìm thấy bảng sản phẩm.");
            }

            // Lấy tất cả thương hiệu
            var allSanPham = await _dbContext.sanpham.ToListAsync();

            // Kiểm tra xem có sản phẩm nào không
            if (allSanPham.Count == 0)
            {
                return NotFound("Không có sản phẩm nào để xóa.");
            }

            // Xóa tất cả thương hiệu
            _dbContext.sanpham.RemoveRange(allSanPham);

            try
            {
                await _dbContext.SaveChangesAsync();
                return Ok("Đã xóa tất cả thương hiệu.");
            }
            catch (DbUpdateException ex)  //lỗi về ràng buộc khóa
            {
                var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra khi xóa sản phẩm: " + message);
            }
        }
        //----------------------------------------------------------------

        //Đơn hàng

        //----------------------------------------------------------------
        [HttpGet("ListDonHang")]
        public async Task<ActionResult<IEnumerable<object>>> DonHang() // Change return type to IEnumerable<object>
        {
            if (_dbContext.phieuxuat == null)
            {
                return NotFound();
            }

            var sanPhamList = await _dbContext.phieuxuat
                .Include(sp => sp.KhachHang) // Include the related ThuongHieu entity
                .ToListAsync();

            var result = sanPhamList.Select(sp => new
            {
                sp.maphieuxuat, // Ensure property names match your model
                sp.thoigian,
                sp.tongtien,

                KhachHang = sp.KhachHang != null ? sp.KhachHang.tenkhachhang : null, // Get the brand name

                sp.trangthai
                
            }).ToList(); // Use ToList() here instead

            return Ok(result); // Return the result with Ok()
        }



        //Cập nhập tình trạng phiếu xuất
        [HttpPut("UpdateTinhTrangPhieuXuat")]
        public async Task<IActionResult> UpdateTinhTrangPhieuXuat(int id, [FromBody] UpdateTrangThaiRequest request)
        {
            // Kiểm tra nếu id hợp lệ
            var phieuXuat = await _dbContext.phieuxuat.FindAsync(id);
            if (phieuXuat == null)
            {
                return NotFound();
            }

            // Cập nhật trường trangthai
            phieuXuat.trangthai = request.TrangThai;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)  //lỗi về xử lí đồng bộ
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool BrandAvailable(int id)
        {
            return (_dbContext.phieuxuat?.Any(x => x.maphieuxuat == id)).GetValueOrDefault();
        }

        [HttpDelete("DeletePhieuXuat")]
        public async Task<IActionResult> DeletePhieuXuat(int id)
        {
            if (_dbContext.phieuxuat == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.phieuxuat.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbContext.phieuxuat.Remove(brand);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("SearchPhieuXuat")]
        public async Task<IActionResult> SearchPhieuXuat(int id)
        {
            // Kiểm tra xem DbSet có tồn tại không
            if (_dbContext.phieuxuat == null)
            {
                return NotFound();
            }

            // Tìm sản phẩm theo mpx
            var product = await _dbContext.phieuxuat
                .Include(px => px.KhachHang)
                .FirstOrDefaultAsync(px => px.maphieuxuat == id);

            if (product == null)
            {
                return NotFound(); // Hoặc bạn có thể trả về một thông báo tùy chỉnh
            }

            // Kiểm tra xem sản phẩm có tồn tại không
            var result = new
            {
                product.maphieuxuat, // Ensure property names match your model
                product.thoigian,
                product.tongtien,

                KhachHang = product.KhachHang != null ? product.KhachHang.tenkhachhang : null, // Get the brand name

                product.trangthai
            };
            return Ok(result); // Return the result with Ok()
        }


        [HttpGet("SearchPhieuXuatTheoNgay")]
        public async Task<IActionResult> SearchPhieuXuatTheoNgay(DateOnly tungay, DateOnly denngay)
        {
            // Kiểm tra xem DbSet có tồn tại không
            if (_dbContext.phieuxuat == null)
            {
                return NotFound();
            }

            // Chuyển đổi DateOnly thành DateTime
            var fromDateTime = tungay.ToDateTime(new TimeOnly(0, 0));
            var toDateTime = denngay.ToDateTime(new TimeOnly(23, 59, 59));

            // Tìm các phiếu xuất trong khoảng thời gian đã cho
            var results = await _dbContext.phieuxuat
                .Include(px => px.KhachHang)
                .Where(px => px.thoigian >= fromDateTime && px.thoigian <= toDateTime)
                .Select(px => new
                {
                    px.maphieuxuat,
                    px.thoigian,
                    px.tongtien,
                    KhachHang = px.KhachHang != null ? px.KhachHang.tenkhachhang : null,
                    px.trangthai
                })
                .ToListAsync();

            // Kiểm tra xem có kết quả không
            if (results == null || !results.Any())
            {
                return NotFound();
            }

            return Ok(results);
        }
    }

}
