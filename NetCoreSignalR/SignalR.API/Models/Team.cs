using System.Collections.Generic;

namespace SignalR.API.Models
{
    /// <summary>
    /// Veri tabanı takım tablomuz
    /// </summary>
    public class Team
    {
        public Team()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// NEDEN VİRTUAL? 
        /// Lazy loading desteği olabilsin diye ve aynı zaman da entity framework ün property üzerinde
        /// değişiklikleri izleyebilsin diye.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
