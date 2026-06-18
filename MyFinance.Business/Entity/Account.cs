using System.ComponentModel.DataAnnotations;

namespace MyFinance.Business.Entity
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Balance { get; set; }

        public bool IsBalanceValid(decimal balance)
        {
            return balance >= 0;
        }
    }
}
