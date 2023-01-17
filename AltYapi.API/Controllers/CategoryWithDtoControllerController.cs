﻿using AltYapi.Core.Dtos;
using AltYapi.Core.Models;
using AltYapi.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AltYapi.API.Controllers
{

    public class CategoryWithDtoControllerController : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> _categoryServiceWithDto;

        public CategoryWithDtoControllerController(IServiceWithDto<Category, CategoryDto> service)
        {
            _categoryServiceWithDto = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryServiceWithDto.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto category)
        {
            return CreateActionResult(await _categoryServiceWithDto.AddAsync(category));
        }
    }
}
