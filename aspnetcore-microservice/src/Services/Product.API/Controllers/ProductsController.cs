using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Entities;
using Product.API.Repositories;
using Shared.DTOs.Product;

namespace Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #region CURD

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _repository.GetProducts();
        var result = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetProduct([Required] long id)
    {
        var product = await _repository.GetProduct(id);
        var result = _mapper.Map<ProductDto>(product);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var productEntity = await _repository.GetProductByNo(productDto.No);
        if(productEntity != null)
            return BadRequest($"Product No: {productDto.No} is existed.");
        
        var product = _mapper.Map<CatalogProduct>(productDto);
        await _repository.CreateProduct(product);
        await _repository.SaveChangesAsync();
        var result = _mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateProduct([Required] long id, 
        [FromBody] UpdateProductDto updateProductDto)
    {
        var product = await _repository.GetProduct(id);
        if(product == null)
            return NotFound();
        
        var updateProduct = _mapper.Map(updateProductDto, product);
        await _repository.UpdateProduct(updateProduct);
        await _repository.SaveChangesAsync();
        var result = _mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteProduct([Required] long id)
    {
        var product = await _repository.GetProduct(id);
        if (product == null)
            return NotFound();

        await _repository.DeleteProduct(id);
        await _repository.SaveChangesAsync();
        return NoContent();
    }
    
    #endregion CURD
    
    #region Additional Resources

    [HttpGet("get-product-by-no/{productNo}")]
    public async Task<IActionResult> GetProductByNo([Required] string productNo)
    {
        var product = await _repository.GetProductByNo(productNo);
        // if(product == null)
        //     return NotFound();
        
        var result = _mapper.Map<ProductDto>(product);
        return Ok(result);
    }
    #endregion
}