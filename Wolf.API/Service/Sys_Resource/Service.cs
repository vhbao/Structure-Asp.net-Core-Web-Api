using Microsoft.EntityFrameworkCore;
using Wolf.API.Infrastructure;
using Wolf.Core.Core;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Wolf.Core.Helpers;

namespace Wolf.API.Service.Sys_Resource
{
    public class Service:RepositoryBase<Model.Sys_Resource>, Sys_Resource.IService
    {
        private readonly DomainDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserProvider _userProvider;
        public Service(DomainDbContext dbContext, IDateTimeProvider dateTimeProvider, IUserProvider userService):base(dbContext, dateTimeProvider, userService)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userProvider = userService;
        }
        public async Task<List<ViewModel.Sys_Resource.ResourceTree>> GetMenuTreeAsync()
        {
            List<ViewModel.Sys_Resource.ResourceTree> items = await _dbContext.Sys_Resources.Where(o => o.Type == Core.Enums.ResourceType.Menu || o.Type == Core.Enums.ResourceType.SubMenu).Select(o =>
                new ViewModel.Sys_Resource.ResourceTree() { Id = o.Id.ToString(), Code = o.Code, Name = o.Name, ParentId = o.ParentId.ToString() }).ToListAsync();
            List<ViewModel.Sys_Resource.ResourceTree> trees = TreeHelpers<ViewModel.Sys_Resource.ResourceTree>.ListToTrees(items);
            return trees;
        }
        public async Task<List<ViewModel.Sys_Resource.ResourceTree>> GetFunctionTreeAsync()
        {
            List<ViewModel.Sys_Resource.ResourceTree> items = await _dbContext.Sys_Resources.Where(o => o.Type == Core.Enums.ResourceType.Function).Select(o =>
                new ViewModel.Sys_Resource.ResourceTree() { Id = o.Id.ToString(), Code = o.Code, Name = o.Name, ParentId = o.ParentId.ToString() }).ToListAsync();
            List<ViewModel.Sys_Resource.ResourceTree> trees = TreeHelpers<ViewModel.Sys_Resource.ResourceTree>.ListToTrees(items);
            return trees;
        }
        public async Task<List<Model.Sys_Resource>> InitFunctionAsync()
        {            
            Model.Sys_Resource resourceParent;
            Model.Sys_Resource resourceChild;
            List<Model.Sys_Resource> resources = new List<Model.Sys_Resource>();
            IEnumerable<string> actions = ReflectionUtil.GetActionsWithController();
            IEnumerable<string> controllers = ReflectionUtil.GetControllers();
            for(int i = 0;i < controllers.Count();i++)
            {
                resourceParent = new Model.Sys_Resource();
                resourceParent.Id = Guid.NewGuid();
                resourceParent.Code = controllers.ElementAt(i);
                resourceParent.Name = controllers.ElementAt(i);
                resourceParent.Type = Core.Enums.ResourceType.Function;
                resourceParent.ParentId = Guid.Empty;
                resourceParent.CreatedBy = _userProvider.LoginName;
                resourceParent.CreatedDateTime = _dateTimeProvider.OffsetNow;
                resources.Add(resourceParent);
                for (int j = 0;j < actions.Count();j++)
                {                    
                    if (actions.ElementAt(j).Contains(controllers.ElementAt(i)))
                    {
                        string[] splitAction = actions.ElementAt(j).Split(".");
                        resourceChild = new Model.Sys_Resource();
                        resourceChild.Id = Guid.NewGuid();
                        resourceChild.Code = splitAction[1];
                        resourceChild.Name = splitAction[1];
                        resourceChild.Type = Core.Enums.ResourceType.Function;
                        resourceChild.ParentId = resourceParent.Id;
                        resourceChild.CreatedBy = _userProvider.LoginName;
                        resourceChild.CreatedDateTime = _dateTimeProvider.OffsetNow;
                        resources.Add(resourceChild);
                    }
                }
            }
            await _dbContext.Sys_Resources.AddRangeAsync(resources);
            await UnitOfWork.SaveAsync();
            return resources;
        }
        public async Task<List<Model.Sys_Resource>> InitMenuAsync(List<Menu> menu)
        {
            if (menu == null)            
                return null;            
            List<Model.Sys_Resource> resources = new List<Model.Sys_Resource>();
            Model.Sys_Resource resourceParent;
            Model.Sys_Resource resourceChildBig;
            Model.Sys_Resource resourceChildSmall;
            for (int i = 0; i < menu.Count; i++)
            {
                resourceParent = new Model.Sys_Resource();
                resourceParent.Id = Guid.NewGuid();
                resourceParent.Code = menu[i].Name;
                resourceParent.Name = menu[i].Meta.Title;
                resourceParent.Type = Core.Enums.ResourceType.Menu;
                resourceParent.ParentId = Guid.Empty;
                resourceParent.CreatedBy = _userProvider.LoginName;
                resourceParent.CreatedDateTime = _dateTimeProvider.OffsetNow;
                resources.Add(resourceParent);
                var childrensBig = menu[i].Children;
                if (childrensBig != null)
                {
                    for (int j = 0; j < childrensBig.Length; j++)
                    {
                        resourceChildBig = new Model.Sys_Resource();
                        resourceChildBig.Id = Guid.NewGuid();
                        resourceChildBig.Code = childrensBig[j].Name;
                        resourceChildBig.Name = childrensBig[j].Meta.Title;
                        resourceChildBig.Type = Core.Enums.ResourceType.SubMenu;
                        resourceChildBig.ParentId = resourceParent.Id;
                        resourceChildBig.CreatedBy = _userProvider.LoginName;
                        resourceChildBig.CreatedDateTime = _dateTimeProvider.OffsetNow;
                        resources.Add(resourceChildBig);
                        var childrensSmall = childrensBig[j].Children;
                        if(childrensSmall != null)
                        {
                            resourceChildSmall = new Model.Sys_Resource();
                            resourceChildSmall.Id = Guid.NewGuid();
                            resourceChildSmall.Code = childrensSmall[j].Name;
                            resourceChildSmall.Name = childrensSmall[j].Meta.Title;
                            resourceChildSmall.Type = Core.Enums.ResourceType.SubMenu;
                            resourceChildSmall.ParentId = resourceChildBig.Id;
                            resourceChildSmall.CreatedBy = _userProvider.LoginName;
                            resourceChildSmall.CreatedDateTime = _dateTimeProvider.OffsetNow;
                            resources.Add(resourceChildSmall);
                        }
                    }
                }
            }
            await _dbContext.Sys_Resources.AddRangeAsync(resources);
            await UnitOfWork.SaveAsync();
            return resources;
        }
        public async Task DeleteAllMenu()
        {
            _dbContext.Sys_Resources.RemoveRange(_dbContext.Sys_Resources.Where(o => o.Type == Core.Enums.ResourceType.Menu || o.Type == Core.Enums.ResourceType.SubMenu).ToList());
            await UnitOfWork.SaveAsync();
        }
        public async Task DeleteAllFunction()
        {
            _dbContext.Sys_Resources.RemoveRange(_dbContext.Sys_Resources.Where(o => o.Type == Core.Enums.ResourceType.Function));
            await UnitOfWork.SaveAsync();
        }
    }
}
