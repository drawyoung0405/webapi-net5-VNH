using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MyApiApp.Data;
using MyApiApp.Models;
using System.Linq;

namespace MyApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDbContext _contextAccessor;
        public LoaiController(MyDbContext contextAccessor)
        {
         _contextAccessor = contextAccessor;
        }

        // Lay tata ca cac loai
        [HttpGet]
        public IActionResult GetAllLoai()
        {

            var dsLoai = _contextAccessor.Loais.ToList();
            return Ok(dsLoai); 
        }

        [HttpGet("Id")]
        public IActionResult GetLoaiById(int Id) {
            var loai = _contextAccessor.Loais.FirstOrDefault(lo => lo.MaLoai == Id);
            if(loai!= null)
            {
                return Ok(loai);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public IActionResult CreateNew(LoaiVM loaiVM)
        {
            try {
                var loai = new Loai
                {
                    TenLoai = loaiVM.TenLoai
                };
                _contextAccessor.Add(loai);
                _contextAccessor.SaveChanges();
                return Ok(loai);
            }
            catch
            {
                return BadRequest(); 
            }
        }

        [HttpPut("Id")]
        public async Task<IActionResult> EditLoai(int Id, Loai loai
            )
        {
            if(Id != loai.MaLoai)
            {
                return BadRequest();
            }
            else
            {
                _contextAccessor.Entry(loai).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
               
                    await _contextAccessor.SaveChangesAsync();

            }
            return NoContent();

        }


    }
}
