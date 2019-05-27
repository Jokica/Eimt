using System;
using System.Collections.Generic;

namespace Eimt.Domain.DomainModels
{
    public class Sector
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public User Menager { get; set; }
        public long? MenagerId { get; set; }
        public List<User> Employees { get; set; }
        private Sector()
        {

        }
        public Sector(string Name, User Menager, List<User> Employees)
        {
            this.Name = Name;
            this.Menager = Menager ?? throw new ArgumentNullException(nameof(Menager));
            this.Employees = Employees ?? throw new ArgumentNullException(nameof(Employees));
        }
        public Sector(string Name, long MenagerId, List<User> Employees)
        {
            this.Name = Name;
            this.MenagerId = MenagerId;
            this.Employees = Employees ?? throw new ArgumentNullException(nameof(Employees));
        }
        public Sector(string Name, User Menager)
        {
            this.Name = Name;
            this.Menager = Menager;
            Employees = new List<User>();
        }
        public Sector(string Name)
        {
            this.Name = Name;
            Employees = new List<User>();
        }

    }
}
