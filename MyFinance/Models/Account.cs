using System;

namespace MyFinance.Models;

public class Account
{
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Balance { get; set; }
}
