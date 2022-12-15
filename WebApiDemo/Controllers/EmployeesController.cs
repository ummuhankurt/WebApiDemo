using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.DataAccess;
using WebApiDemo.Entities;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        public IEmployeeDal _employeeDal { get; set; }
        public EmployeesController(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _employeeDal.GetAll();
            return Ok(employees);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            try
            {
                var employe = _employeeDal.Get(e => e.EmployeeID == id);
                if (employe == null)
                {
                    return NotFound($"There is not employee with Id = { id}");
                }
                return Ok(employe);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
