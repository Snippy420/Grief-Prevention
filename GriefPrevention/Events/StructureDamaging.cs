using Microsoft.Extensions.Configuration;
using OpenMod.API.Eventing;
using OpenMod.API.Permissions;
using OpenMod.API.Persistence;
using OpenMod.API.Users;
using OpenMod.Unturned.Building.Events;
using OpenMod.Unturned.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GriefPrevention.Events
{
    public class StructureDamaging : IEventListener<UnturnedStructureDamagingEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly bool m_enable_grief_prevention;

        public StructureDamaging(IConfiguration configuration)
        {
            m_Configuration = configuration;

            m_enable_grief_prevention = m_Configuration.GetValue<bool>("enable_grief_prevention");
        }

        public async Task HandleEventAsync(object sender, UnturnedStructureDamagingEvent @event)
        {
            if (m_enable_grief_prevention)
            {
                @event.IsCancelled = true;
            }
        }
    }
}
