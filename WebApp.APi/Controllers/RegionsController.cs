using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.APi.Data;
using WebApp.APi.Models.Domain;
using WebApp.APi.Models.DTO;

namespace WebApp.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        //GET ALL REGIONS
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get data from the database - domain models
            var regionsDomain = _dbContext.Regions.ToList();

            //Map Domain Models to DTO
            var regionsDto = new List<RegionDto>();

            //Convert each region domain model to a region DTO
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto() 
                {
                   Id= regionDomain.Id,
                   Code= regionDomain.Code,
                   Name= regionDomain.Name,
                   RegionImageUrl= regionDomain.RegionImageUrl
               });
            }
            return Ok(regionsDto);
        }

        //GET A SINGLE REGION BY ID
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Get region domain model from the database
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
          
                return NotFound();
            }

            //Convert Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            
            return Ok(regionDto);
        }
    }
}
