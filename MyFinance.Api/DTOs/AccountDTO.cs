using System;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Api.DTOs;

public class AccountDTO
{
        public AccountDTO()
        {
        }

        public AccountDTO(int id, string name, double balance)
        {
                Id = id;
                Name = name;
                Balance = balance;
        }

        [Required(ErrorMessage = "O id é obrigatório.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O saldo não pode ser negativo.")]
        public double Balance { get; set; }
}