using Microsoft.AspNetCore.Http;
using Wolf.Core.Interfaces;
using Wolf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wolf.API.Service.Sys_File
{
    public interface IService: IRepositoryBase<Model.Sys_File>
    {
        Task<List<FileResult>> Upload(List<IFormFile> files, string objectId, string objectType, string savedPath);
        Task<List<FileResult>> GetByObjectId(string objectId, string objectType);
    }
}
