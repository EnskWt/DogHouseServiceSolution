using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Core.Domain.Models
{
    /// <summary>
    /// Represents a dog domain object and database table.
    /// </summary>
    public class Dog
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;

        public int TailLength { get; set; } = 0;

        public int Weight { get; set; } = 0;
    }
}
