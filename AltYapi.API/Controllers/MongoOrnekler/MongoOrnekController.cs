using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Models.ModelsMongo;
using AltYapi.Core.Repositories.RepositoriesMongo;
using AltYapi.Core.Services;
using AltYapi.Service.Services;
using Autofac.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AltYapi.API.Controllers.MongoOrnekler
{
    public class MongoOrnekController : CustomBaseController
    {
        private readonly IPersonServicesWithDto _service;


        public MongoOrnekController(IPersonServicesWithDto service)
        {
            _service = service;
        }

        [HttpPost("registerPerson")]
        public async Task<IActionResult> AddPerson(CreatePersonDto person)
        {
            return CreateActionResult(await _service.AddAsync(person));
        }

        [HttpGet("getPeopleData")]
        public async Task<IActionResult> GetPeopleData()
        {
            return CreateActionResult(await _service.GetAllAsync());
        }
    }
}
