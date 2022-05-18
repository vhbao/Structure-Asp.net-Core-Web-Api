using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wolf.API.Infrastructure.Authorization;
using Wolf.API.Service;
using Wolf.API.Controllers;
using Wolf.API.Model;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Controllers
{
    public class Sys_CategoryController : ApiControllerBase<Sys_Category>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_CategoryController> _logger;
        public Sys_CategoryController(IServiceWrapper repository, ILogger<Sys_CategoryController> logger) :base(repository, logger)
        {
            _logger = logger;
            _service = repository;
        }
        [HttpGet("{page}/{pageSize}/{totalLimitItems}/{type}")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetListPagedByType(int page = 1, int pageSize = 10, int totalLimitItems = 500, int type = 0)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetListPagedByType params: (page = {0}, pageSize = {1}, totalLimitItems = {2}, type = {3})", page, pageSize, totalLimitItems, type));
                string search = $"type = {type}";
                var items = await _service.Sys_Category.GetPagedAsync(page, pageSize, totalLimitItems, search);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetListPagedByType : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("CheckDupicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDupicateAttributes(Guid? id, string code, int type)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDupicateAttributes params: (id = {0}, code = {1}, type = {2})", id, code, type));
                var result = await _service.Sys_Category.IsDupicateAttributesAsync(id, code, type);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDupicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
