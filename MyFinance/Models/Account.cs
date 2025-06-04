using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

    }
}
