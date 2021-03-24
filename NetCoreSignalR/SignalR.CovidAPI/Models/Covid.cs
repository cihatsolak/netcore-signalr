using SignalR.CovidAPI.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SignalR.CovidAPI.Models
{
    public class Covid
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Şehir
        /// </summary>
        public City City { get; set; }

        /// <summary>
        /// Vaka Sayısı
        /// </summary>
        public int NumberOfCases { get; set; }

        /// <summary>
        /// Covid Tarihi
        /// </summary>
        public DateTime CovidDate { get; set; }
    }
}
