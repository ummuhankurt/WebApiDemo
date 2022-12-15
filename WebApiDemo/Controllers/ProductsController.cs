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
    public class ProductsController : ControllerBase
    {
        IProductDal _productDal;
        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productDal.GetAll();
            return Ok(products);
        }

        [HttpGet("GetProductDetails")]
        public IActionResult GetProductDetails()
        {
            try
            {
                var result = _productDal.GetProductWithDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpGet("getbystr")]
        public IActionResult GetByString(string str)
        {
            try
            {
                var products = _productDal.GetAll(p => p.ProductName.ToLower().Contains(str));
                if (products == null)
                {
                    return NotFound("There is no product");  
                }
                return Ok(products);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            try
            {
                _productDal.Add(product);
                return Ok("Product added");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
           
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok($"Product updated : {product.ProductName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        [HttpDelete]
        public IActionResult Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
                return Ok($"Product deleted : {product.ProductName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }
    }
}
