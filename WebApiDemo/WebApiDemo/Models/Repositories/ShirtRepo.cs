using WebApiDemo.Data;

namespace WebApiDemo.Models.Repositories
{
    public class ShirtRepo
    {
        private readonly ApplicationDbContext _db;

        public ShirtRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool ShirtExists(int id)
        {
            return _db.Shirts.Any(s => s.Id == id);
        }

        public List<Shirt> GetShirts()
        {
            return _db.Shirts.ToList();
        }

        public Shirt GetShirt(int id)
        {
            if (!ShirtExists(id))
                throw new Exception("Shirt not found.");

            return _db.Shirts.FirstOrDefault(s => s.Id == id)
                ?? throw new Exception("Shirt not found.");
        }

        public Shirt AddShirt(Shirt shirt)
        {
            _db.Shirts.Add(shirt);
            _db.SaveChanges();
            return shirt;
        }

        public Shirt UpdateShirt(int id, Shirt shirt)
        {
            var existingShirt =
                _db.Shirts.FirstOrDefault(s => s.Id == id)
                ?? throw new Exception("Shirt not found.");

            existingShirt.Color = shirt.Color;
            existingShirt.Size = shirt.Size;
            existingShirt.Brand = shirt.Brand;
            existingShirt.Gender = shirt.Gender;
            existingShirt.Price = shirt.Price;

            _db.SaveChanges();

            return existingShirt;
        }

        public bool DeleteShirt(int id)
        {
            var existingShirt = _db.Shirts.FirstOrDefault(s => s.Id == id);

            if (existingShirt == null)
                throw new Exception("Shirt not found.");

            _db.Shirts.Remove(existingShirt);
            return _db.SaveChanges() > 0;
        }
    }
}
