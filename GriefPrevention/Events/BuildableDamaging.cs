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
        private readonly IConfiguration _Configuration;
        private readonly bool _enable_grief_prevention;

        public BuildableDamaging(IConfiguration configuration)
        {
            _Configuration = configuration;
            _enable_grief_prevention = _Configuration.GetValue<bool>("enable_grief_prevention");
        }

        public async Task HandleEventAsync(object sender, UnturnedBuildableDamagingEvent @event)
        {
            if (!_enable_grief_prevention)
            {
                return;
            }
            if (@event.Buildable is UnturnedBarricadeBuildable barricadeBuildable && barricadeBuildable.BarricadeDrop.interactable is InteractableFarm plant)
            {
                return;
            }

            @event.IsCancelled = true;
        }
    }
}
