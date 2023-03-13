using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBox.Core.Models
{
    public class EntitiyBase
    {
        public EntitiyBase()
        {
            CreatedDate = DateTime.Now;
            Id = Guid.NewGuid();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Delete { get; set; } = false;
    }
}
