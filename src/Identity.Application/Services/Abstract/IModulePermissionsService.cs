using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;

namespace Identity.Application.Services.Abstract
{
    public interface IModulePermissionsService
    {
        Task<ModuleAuthInfoDto?> FindOrDefault(string? path, string? type, int? id);
    }
}
