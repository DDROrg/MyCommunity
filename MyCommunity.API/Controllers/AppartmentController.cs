using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MyCommunity.DAL;
using MyCommunity.DAL.DTO;
using MyCommunity.DAL.Models;
using MyCommunity.Utility.Extensions;

namespace MyCommunity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppartmentController : Controller
    {
        private readonly ILogger<AppartmentController> _logger;
        private readonly ICommunityRepository _repo;
        private readonly IMapper _mapper;

        public AppartmentController(ILogger<AppartmentController> logger,ICommunityRepository repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        
        // GET api/Appartment/get
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<AppartmentDTO>>> Get()
        {
            _logger.LogInformation($"Get Appartment API called.");
            var appartment = await _repo.GetAllAppartments();
            return new ObjectResult(_mapper.Map<IEnumerable<AppartmentDTO>>(appartment));
        }


        // GET api/Appartment/get/1
        [HttpGet("get/{id}")]
        public async Task<ActionResult<AppartmentDTO>> Get(string id)
        {
            var appartment = await _repo.GetAppartment(id);
            if (appartment == null)
            {
                return new NotFoundResult();
            }
            return new ObjectResult(_mapper.Map<AppartmentDTO>(appartment));
        }

        // POST api/Appartment/create
        [HttpPost("create")]
        public async Task<ActionResult<AppartmentDTO>> Post([FromBody] AppartmentDTO appartment)
        {
            // Validation
            bool isValid = true;
            if(isValid && string.IsNullOrWhiteSpace(appartment.name)){
                isValid = false;
            }

            if(isValid && appartment.address == null){
                isValid = false;
            }            

            if(!isValid){
                return new BadRequestObjectResult(appartment);
            }

            // Set default attribute
            Guid aptId = Guid.NewGuid();
            appartment.id = aptId.ToString();
            appartment.customerId = aptId.ToFriendlyId();
            appartment.status = "ACTIVE";
                
            var apt = _mapper.Map<Appartment>(appartment);
            await _repo.CreateAppartment(apt);
            return new OkObjectResult(_mapper.Map<AppartmentDTO>(apt));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
