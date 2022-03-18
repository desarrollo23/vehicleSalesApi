using System;
namespace VehicleSales.Model.Base.Model
{
    public class Entity
    {
        public Entity()
        {
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
