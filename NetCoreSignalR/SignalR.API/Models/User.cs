using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("TeamId")]
        public virtual int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
