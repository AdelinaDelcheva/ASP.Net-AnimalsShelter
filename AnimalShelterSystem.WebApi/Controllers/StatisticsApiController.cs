
namespace AnimalShelterSystem.WebApi.Controllers
{
    using AnimalsShelterSystem.Services.Data.Interfaces;
    using AnimalsShelterSystem.Services.Data.Models.Statistics;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IAnimalService animalService;
        public StatisticsApiController(IAnimalService animalService)
        {
            this.animalService = animalService;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel =
                    await this.animalService.GetStatisticsAsync();

                return this.Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

    }
}
