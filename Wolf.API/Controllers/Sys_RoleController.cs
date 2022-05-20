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
    public class Sys_RoleController : ApiControllerBase<Sys_Role>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_RoleController> _logger;
        public Sys_RoleController(IServiceWrapper service, ILogger<Sys_RoleController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("CheckDupicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDupicateAttributes(Guid? id, string code)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDupicateAttributes params: (id = {0}, code = {1})", id, code));
                var result = await _service.Sys_Role.IsDupicateAttributesAsync(id, code);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDupicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpDelete("DeleteById/{Id}")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteById(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call DeleteById params: (id = {0})", Id));
                await _service.Sys_Role.DeleteById(Id);
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DeleteById : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
