using System;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Api.DTOs;

public class AccountDTO
{
        public AccountDTO()
        {
        }

        public AccountDTO(int id, string name, decimal balance)
        {
                Id = id;
                Name = name;
                Balance = balance;
        }

        [Required(ErrorMessage = "O id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "999999999999.99", ErrorMessage = "O saldo não pode ser negativo.", ParseLimitsInInvariantCulture = true)]
        public decimal Balance { get; set; }
}
