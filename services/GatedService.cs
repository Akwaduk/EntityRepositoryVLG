using System;
using System.Reflection.Metadata;
using BlazingScaffolds.Services.Gates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BlazingScaffolds.services
{
    public class GatedService<T> : BaseService<T> where T : BaseItem
    {
        private readonly IGateKeep<T> _gatekeeper;

        public GatedService(DbContext dbContext, IGateKeep<T> gatekeeper,
         bool EnableLogging, bool EnableVersioning) : 
            base(dbContext, EnableLogging, EnableVersioning)
        {
            _gatekeeper = gatekeeper ?? throw new ArgumentNullException(nameof(gatekeeper));
        }
    }
}
