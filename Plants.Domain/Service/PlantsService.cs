using Plants.Domain.Entities;
using Plants.Domain.IRepository;
using Plants.Domain.IService;
using Plants.Domain.Model;
using Plants.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plants.Domain.Service
{
    public class PlantsService : IPlantsService
    {
        private IPlantsRepository _plantsRepository;
        public PlantsService(IPlantsRepository plantsRepository)
        {
            _plantsRepository = plantsRepository;
        }
        public async Task<BaseResponse> Create(PlantRequest plantRequest)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {

                Plant plant = new Plant();
                plantRequest.CopyPropertiesTo(plant);
                await _plantsRepository.Add(plant);
                return baseResponse.ToSuccess<BaseResponse>();
            }
            catch (Exception ex)
            {

                return baseResponse.ToApiError<BaseResponse>();
            }



        }
        public async Task<PlantResponse> GetAllPlants()
        {
            var result = await _plantsRepository.GetAll();
            PlantResponse plantResponse = new PlantResponse();
            plantResponse.plants = new List<PlantRow>();
            foreach (var item in result)
            {
                PlantRow plantRow = new PlantRow();
                item.CopyPropertiesTo(plantRow);
                TimeSpan timeSpan = DateTime.Now - item.LastWateringDate;
                if (timeSpan.TotalHours >= 6)
                {
                    plantRow.plantsStatus = PlantsStatus.ReadyToWattering;
                    plantRow.statusName = PlantsStatus.ReadyToWattering.ToString();
                }
                else
                {
                    plantRow.plantsStatus = PlantsStatus.Watered;
                    plantRow.statusName = PlantsStatus.Watered.ToString();
                }

                plantResponse.plants.Add(plantRow);
            }
            plantResponse.ResultCount = plantResponse.plants.Count;
            plantResponse.ToSuccess<PlantResponse>();
            return plantResponse;
        }
        public async Task<BaseResponse> Update(PlantRequest plantRequest)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                Plant plant = new Plant();
                plant.UpdatedBy = plantRequest.UserName;
                plant.UpdatedDate = DateTime.Now;
                plantRequest.CopyPropertiesTo(plant);
                await _plantsRepository.Update(plant);
                return baseResponse.ToSuccess<BaseResponse>();

            }
            catch (Exception ex)
            {
                return baseResponse.ToApiError<BaseResponse>();
            }

        }

        public async Task<BaseResponse> StartWatering(PlantRequest plantRequest, CancellationToken cancellationToken)
        {
            BaseResponse baseResponse = new BaseResponse();

            Plant plant = await _plantsRepository.GetById(plantRequest.Id);
            if (plant != null)
            {
                TimeSpan timeSpan = DateTime.Now - plant.LastWateringDate;
                if (timeSpan.TotalHours < 6)
                    return baseResponse.ToToMuchWateringError<BaseResponse>();
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        Thread.Sleep(1000);
                    }
                    plant.LastWateringDate = DateTime.Now;
                    await _plantsRepository.Update(plant);
                    return baseResponse.ToSuccess<BaseResponse>();
                }

            }
            else
                return baseResponse.ToNotFoundError<BaseResponse>();
        }
    }
}


