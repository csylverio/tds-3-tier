using System.ComponentModel.DataAnnotations;

namespace MyFinance.Business.Entity
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Balance { get; set; }

        public bool IsBalanceValid(double balance)
        {
            return Balance >= 0;
        }
    }
}
