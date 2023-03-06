using Ceo.Core.Dtos;
using Ceo.Core.Models;
using Ceo.Core.Models.ModelsMongo;
using Ceo.Core.Repositories.RepositoriesMongo;
using Ceo.Core.Services;
using Ceo.Service.Services;
using Autofac.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ceo.API.Controllers.MongoOrnekler
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
