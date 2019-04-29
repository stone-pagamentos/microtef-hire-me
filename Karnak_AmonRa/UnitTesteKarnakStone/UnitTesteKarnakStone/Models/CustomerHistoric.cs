using System;

namespace UnitTesteKarnakStone.Models
{
    public class CustomerHistoric
    {
        public Guid? Id { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}
