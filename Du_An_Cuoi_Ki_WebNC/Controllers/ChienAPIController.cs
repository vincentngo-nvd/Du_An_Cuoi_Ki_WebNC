using Du_An_Cuoi_Ki_WebNC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Du_An_Cuoi_Ki_WebNC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChienAPIController : ControllerBase
    {
        private readonly Dbcontext _dbcontext;

        public ChienAPIController(Dbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<NhanvienModels>>> GetNhanVien()
        {
            if (_dbcontext.Nhanviens == null)
            {
                return NotFound();
            }
            return await _dbcontext.Nhanviens.ToListAsync();
        }


        [HttpGet("ListId")]
        public async Task<ActionResult<NhanvienModels>> GetNhanVienID(int id)
        {
            if (_dbcontext.Nhanviens == null)
            {
                return NotFound();
            }

            var brand = await _dbcontext.Nhanviens.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }
        [HttpPost("Insert")]
        public async Task<ActionResult<NhanvienModels>> PostNhanVien(NhanvienModels nhanvien)
        {
            _dbcontext.Nhanviens.Add(nhanvien);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNhanVienID), new { id = nhanvien.manv }, nhanvien);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> PutNhanVien(int id, NhanvienModels nhanvien)
        {
            if (id != nhanvien.manv)
            {
                return BadRequest();
            }
            _dbcontext.Entry(nhanvien).State = EntityState.Modified;

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienAvailable(id))
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
        private bool NhanVienAvailable(int id)
        {
            return (_dbcontext.Nhanviens?.Any(x => x.manv == id)).GetValueOrDefault();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            if (_dbcontext.Nhanviens == null)
            {
                return NotFound();
            }
            var brand = await _dbcontext.Nhanviens.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbcontext.Nhanviens.Remove(brand);
            await _dbcontext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("List_taikhoan")]
        public async Task<ActionResult<IEnumerable<TaikhoanKH>>> GetTaiKhoan()
        {
            if (_dbcontext.taikhoans == null)
            {
                return NotFound();
            }
            return await _dbcontext.taikhoans.ToListAsync();
        }
        private bool TaiKhoanAvailable(int id)
        {
            return (_dbcontext.taikhoans?.Any(x => x.makh == id)).GetValueOrDefault();
        }
        [HttpPut("Update_taikhoan")]
        public async Task<IActionResult> UpdateTaiKhoan([FromBody] TaikhoanKH request)
        {
            if (request == null || request.makh <= 0)
            {
                return BadRequest(new { message = "Mã tài khoản không hợp lệ." });
            }

            try
            {
                // Tìm tài khoản theo makh
                var taiKhoan = await _dbcontext.taikhoans.FindAsync(request.makh);

                if (taiKhoan == null)
                {
                    return NotFound(new { message = "Không tìm thấy tài khoản với ID được cung cấp." });
                }

                // Cập nhật tất cả các trường trong đối tượng TaikhoanKH
                taiKhoan.tendangnhap = request.tendangnhap;
                taiKhoan.matkhau = request.matkhau;
                taiKhoan.trangthai = request.trangthai;

                // Đánh dấu tất cả các thuộc tính đã thay đổi
                _dbcontext.Entry(taiKhoan).State = EntityState.Modified;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbcontext.SaveChangesAsync();

                return Ok(new { message = "Cập nhật tài khoản thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi cập nhật.", error = ex.Message });
            }
        }
    }
}

