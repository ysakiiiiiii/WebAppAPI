using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            //Get data from the database - domain models
            var regionsDomain = await _dbContext.Regions.ToListAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get region domain model from the database
            var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
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

        //Create Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Convert Dto to Domain Model
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to Create Region
            await  _dbContext.Regions.AddAsync(regionDomainModel);
            await _dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

             _dbContext.Regions.Remove(regionDomainModel);
            await _dbContext.SaveChangesAsync();

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
