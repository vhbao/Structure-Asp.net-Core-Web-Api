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
    public class Sys_OrganizationController : ApiControllerBase<Sys_Organization>
    {
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_CategoryController> _logger;
        public Sys_OrganizationController(IServiceWrapper service, ILogger<Sys_CategoryController> logger) :base(service, logger)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("CheckDupicateAttributes")]
        [AuthorizeFilter]
        public async Task<IActionResult> CheckDupicateAttributes(Guid? id, string code)
        {
            try
            {
                _logger.LogInformation(string.Format("Call CheckDupicateAttributes params: (id = {0}, code = {1})", id, code));
                var result = await _service.Sys_Organization.IsDupicateAttributesAsync(id, code);
                return ResponseMessage.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("CheckDupicateAttributes : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("Tree")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetTree()
        {
            try
            {
                _logger.LogInformation("Call GetTree");
                var treeOrgan = await _service.Sys_Organization.GetTreeAsync();
                return ResponseMessage.Success(treeOrgan);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetTree : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpGet("List/{ParentId}")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetByPerentId(Guid parentId)
        {
            try
            {
                _logger.LogInformation(string.Format("Call GetByPerentId params: (parentId = {0})", parentId));
                var items = await _service.Sys_Organization.GetByParentIdAsync(parentId);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetByPerentId : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
        [HttpDelete("DeleteById/{Id}")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteById(Guid Id)
        {
            try
            {
                _logger.LogInformation(string.Format("Call DeleteById params: (Id = {0})", Id));
                await _service.Sys_Organization.DeleteById(Id);
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
