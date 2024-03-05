using Microsoft.Extensions.Configuration;
using OpenMod.API.Eventing;
using OpenMod.API.Permissions;
using OpenMod.API.Persistence;
using OpenMod.API.Users;
using OpenMod.Core.Users;
using OpenMod.Unturned.Building;
using OpenMod.Unturned.Building.Events;
using OpenMod.Unturned.Users;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GriefPrevention.Events
{
    public class BuildableDamaging : IEventListener<UnturnedBuildableDamagingEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly bool m_enable_grief_prevention;
        private readonly UnturnedUserDirectory m_UnturnedUserDirectory;

        public BuildableDamaging(IConfiguration configuration, UnturnedUserDirectory unturnedUserDirectory)
        {
            m_Configuration = configuration;

            m_enable_grief_prevention = m_Configuration.GetValue<bool>("enable_grief_prevention");
            m_UnturnedUserDirectory = unturnedUserDirectory;
        }

        public async Task HandleEventAsync(object sender, UnturnedBuildableDamagingEvent @event)
        {
            if (m_enable_grief_prevention)
            {
                if (@event.Buildable is UnturnedBarricadeBuildable barricadeBuildable && barricadeBuildable.BarricadeDrop.interactable is InteractableFarm plant)
                {
                    return;
                }
                else
                {
                    @event.IsCancelled = true;
                }
            }
        }
    }
}
