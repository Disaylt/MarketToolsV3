using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.UpdateModels;

namespace UserNotifications.Applications.Services.Abstract
{
    public interface IUpdateEntityService<in T> where T : BaseUpdateModel
    {
        public Task UpdateManyAsync(T data);
    }
}
