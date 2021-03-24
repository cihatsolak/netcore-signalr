using System.Collections.Generic;

namespace SignalR.API.Models.ViewModel
{
    public class PlayerTeamViewModel
    {
        public PlayerTeamViewModel()
        {
            Users = new List<string>();
        }

        public string TeamName { get; set; }
        public IEnumerable<string> Users { get; set; }
    }
}
