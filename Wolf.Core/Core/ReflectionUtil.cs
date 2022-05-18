using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wolf.Core.Core
{
    public class ReflectionUtil
    {
        public static IEnumerable<string> GetControllers()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var controllers = asm.GetTypes()
                .Where(o => typeof(ControllerBase).IsAssignableFrom(o.BaseType) && o.Name.Contains("Controller") && !o.IsAbstract)
                .Select(o => o.Name);            
            return controllers;
        }
        public static IEnumerable<string> GetActionsWithController()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var actions = asm.GetTypes()
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                .Where(action => action.DeclaringType.ToString().Contains("Controllers.")
                && !action.ReflectedType.Name.Contains("ApiControllerBase"))
                .Select(o => o.ReflectedType.Name + "." + o.Name);
            return actions;        
        }
    }
}
