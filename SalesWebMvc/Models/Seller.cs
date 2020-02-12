using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateBirthday { get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        // chave estrangeira - Padrão: Nome do Model + Id
        public int DepartmentId { get; set; }

        // relação de 1 para muitos - 1 venderdor tem n registro de vendas
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime dateBirthday, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            DateBirthday = dateBirthday;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).
                Sum(sr => sr.Amount);
        }






    }
}
