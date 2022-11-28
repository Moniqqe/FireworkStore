using System;

namespace Interview.FireworkStore.Core.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int FireworkId { get; set; }
        public DateTime Created { get; set; }
        public int Quantity { get; set; }
        public Guid GroupId { get; set; }
    }
}
