using System;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Api.DTOs;

public class CreateAccountDTO
{
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public required string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O saldo não pode ser negativo.")]
        public double Balance { get; set; }
}
