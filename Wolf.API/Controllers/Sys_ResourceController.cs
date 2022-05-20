using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wolf.API.Infrastructure.Authorization;
using Wolf.API.Service;
using Wolf.API.Controllers;
using Wolf.API.Model;
using Wolf.Core.Constant;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Controllers
{
    public class Sys_ResourceController : ApiControllerBase<Sys_Resource>
    {        
        private readonly IServiceWrapper _service;
        private readonly ILogger<Sys_CategoryController> _logger;
        public Sys_ResourceController(IServiceWrapper service, ILogger<Sys_CategoryController> logger) :base(service, logger)
        {
            _service = service;            
            _logger = logger;
        }
        [HttpPost("InitFunc")]
        [AuthorizeFilter]
        public async Task<IActionResult> InitFunction()
        {
            try
            {
                _logger.LogInformation("Call InitFunction");
                var items = await _service.Sys_Resource.InitFunctionAsync();
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("InitFunction : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpPost("InitMenu")]
        [AuthorizeFilter]
        public async Task<IActionResult> InitMenu(List<Menu> menus)
        {
            try
            {
                _logger.LogInformation(string.Format("Call InitMenu params: (menus = {0})", menus != null && menus.Count > 0 ? string.Join(", ", menus) : ""));
                var items = await _service.Sys_Resource.InitMenuAsync(menus);
                return ResponseMessage.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("InitMenu : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("FuncTree")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetFunctionTree()
        {
            try
            {
                _logger.LogInformation("Call GetFunctionTree");
                var treeOrgan = await _service.Sys_Resource.GetFunctionTreeAsync();
                return ResponseMessage.Success(treeOrgan);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetFunctionTree : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpGet("MenuTree")]
        [AuthorizeFilter]
        public async Task<IActionResult> GetMenuTree()
        {
            try
            {
                _logger.LogInformation("Call MenuTree");
                var treeOrgan = await _service.Sys_Resource.GetMenuTreeAsync();
                return ResponseMessage.Success(treeOrgan);
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("GetMenuTree : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpDelete("All/Menu")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteAllMenu()
        {
            try
            {
                _logger.LogInformation("Call DeleteAllMenu");
                await _service.Sys_Resource.DeleteAllMenu();
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DeleteAllMenu : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }

        [HttpDelete("All/Func")]
        [AuthorizeFilter]
        public async Task<IActionResult> DeleteAllFunction()
        {
            try
            {
                _logger.LogInformation("Call DeleteAllFunction");
                await _service.Sys_Resource.DeleteAllFunction();
                return ResponseMessage.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("DeleteAllFunction : {0}", ex.Message));
                return ResponseMessage.Error(ex.Message);
            }
        }
    }
}
