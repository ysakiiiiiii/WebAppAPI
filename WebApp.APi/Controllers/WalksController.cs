using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.APi.Data;
using WebApp.APi.Models.Domain;
using WebApp.APi.Models.DTO;
using WebApp.APi.Repositories;

namespace WebApp.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            //Call Repository to save the walk
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        //Get:/api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
                                                [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //Get all walks from the repository
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy, isAscending ?? true, pageNumber, pageSize);
            //Map Domain Model to DTO
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
            return Ok(walksDto);
        }
    }
}
