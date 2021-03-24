using System.ComponentModel.DataAnnotations;

namespace SignalR.API.Models
{
    /// <summary>
    /// Veri tabanı user tablomuz
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Team Team { get; set; }
    }
}
