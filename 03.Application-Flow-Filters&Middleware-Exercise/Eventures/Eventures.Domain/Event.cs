using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventures.Domain
{
    public class Event
    {
        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int TotalTickets { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }
    }
}
