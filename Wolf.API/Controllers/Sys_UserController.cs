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
using Newtonsoft.Json;

namespace Wolf.API.Controllers
{
    public class Sys_UserController : ApiControllerBase<Sys_User>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_CategoryController> _logger;
        public Sys_UserController(IServiceWrapper service, ILogger<Sys_CategoryController> logger) :base(service, logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("CheckDupicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDupicateAttributes(Guid? id, string loginName)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDupicateAttributes params: (id = {0}, loginName = {1})", id, loginName));
                var result = await _service.Sys_User.IsDupicateAttributesAsync(id, loginName);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDupicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("{id}")]
        [AuthorizeFilter]
        public override async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetById params: (id = {0})", id));
                var item = await _service.Sys_User.GetDetailByIdAsync(id);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetById : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("List/{organId}")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetByOrganId(Guid organId)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetByOrganId params: (organId = {0})", organId));
                var items = await _service.Sys_User.GetByOrganIdAsync(organId);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetByOrganId : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPost("{organId}/{roleId}")]
        [AuthorizeFilter]
        public async Task<IActionResult> CreateWithOrganAndRole([FromBody] Sys_User model, Guid organId, Guid roleId)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CreateWithOrganAndRole params: (organId = {0}, roleId = {1}) and body: ({2})", organId, roleId, JsonConvert.SerializeObject(model)));
                var item = await _service.Sys_User.CreateAsync(model, organId, roleId);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CreateWithOrganAndRole : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpPut("{organId}/{roleId}")]
        [AuthorizeFilter]
        public async Task<IActionResult> UpdateWithOrganAndRole([FromBody] Sys_User model, Guid organId, Guid roleId)
        {
            try
            {
                _logger.LogInformation(string.Format("Call UpdateWithOrganAndRole params: (organId = {0}, roleId = {1}) and body: ({2})", organId, roleId, JsonConvert.SerializeObject(model)));
                var item = await _service.Sys_User.UpdateAsync(model, organId, roleId);
                return ResponseMessage.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("UpdateWithOrganAndRole : {0}", ex.Message));
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
                await _service.Sys_User.DeleteById(Id);
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
