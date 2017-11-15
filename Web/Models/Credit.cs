using Microsoft.Extensions.Options;
using System;
namespace Web.Models
{
    public class Credit
    {
        public Credit()
        {
            
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
    }
}
