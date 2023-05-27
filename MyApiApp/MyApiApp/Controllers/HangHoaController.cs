using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetHangHoaByID(string id)
        {
            try
            { 
            var hangHoa = hangHoas.SingleOrDefault(hh => hh.Id == Guid.Parse(id));
            if(hangHoa == null)
            {
                return NotFound();
            }
            return Ok(hangHoa);
            }
            catch
            {
                return BadRequest();
            }
        }
              [HttpPost]
        public IActionResult CreateNew(HangHoaVM hangHoaVM) {
            var hanghoa = new HangHoa
            {
                Id = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };
            hangHoas.Add(hanghoa);
            return Ok(new
            {
                Success = true,
                Data = hanghoa
            });
        }
        [HttpPut("{id}")]
        public IActionResult EditHangHoa(string id, HangHoa hangHoaEdit) {
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.Id == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }

                if(id != hangHoa.Id.ToString())
                {
                    return BadRequest();
                }
            //Update
                hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hangHoa.DonGia = hangHoaEdit.DonGia;
                return Ok();

            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("id")]
        public IActionResult DeleteHangHoaById(string id)
        {
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.Id == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }

                //Delete
              hangHoas.Remove(hangHoa);
                return Ok();

            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
