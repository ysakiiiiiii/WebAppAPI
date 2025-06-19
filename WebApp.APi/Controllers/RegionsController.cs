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
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
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

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Convert Dto to Domain Model
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to Create Region
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            });
        }

        //Update a region
        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }
    }
}
