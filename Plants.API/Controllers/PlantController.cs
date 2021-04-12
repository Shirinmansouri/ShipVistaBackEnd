using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plants.Domain;
using Plants.Domain.IService;
using Microsoft.AspNetCore.Authorization;
using Plants.Domain.Model;
using System.Threading;

namespace Plants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : Controller
    {
        private IPlantsService _plantsService;
        public PlantController(IPlantsService plantsService)
        {
            _plantsService = plantsService;
        }
        [HttpPost("Create")]
        [Authorize]
        public async Task<BaseResponse> Create([FromBody] PlantRequest  plantRequest)
        {
            BaseResponse  baseResponse= await _plantsService.Create(plantRequest);
            return baseResponse;

        }
        [HttpPost("Update")]
        [Authorize]
        public async Task<BaseResponse> Update([FromBody] PlantRequest plantRequest)
        {
            BaseResponse baseResponse = await _plantsService.Create(plantRequest);
            return baseResponse;

        }
        [HttpGet("GetAllPlants")]
        [Authorize]
        public async Task<PlantResponse> GetAllPlants()
        {
            PlantResponse plantResponse = await _plantsService.GetAllPlants();
            return plantResponse;

        }
        [HttpPost("StartWatering")]
        [Authorize]
        public async Task<BaseResponse> StartWatering([FromBody] PlantRequest plantRequest, CancellationToken cancelToken)
        {
            return await _plantsService.StartWatering(plantRequest, cancelToken);


        }
    }
}
