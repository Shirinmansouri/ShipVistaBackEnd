using Microsoft.AspNetCore.Http;
using Plants.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plants.Domain.IService
{
    public interface IPlantsService
    {
        Task<BaseResponse> Create(PlantRequest plantRequest);
        Task<PlantResponse> GetAllPlants();
        Task<BaseResponse> Update(PlantRequest plantRequest);
        Task<BaseResponse> StartWatering(PlantRequest plantRequest, CancellationToken token);
    }
}
