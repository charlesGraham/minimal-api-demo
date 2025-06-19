namespace WebApp.Models.Repositories
{
    public class ShirtRepo
    {
        List<Shirt> shirts = new()
        {
            new Shirt
            {
                Id = 1,
                Color = "Red",
                Size = 10,
                Brand = "Nike",
                Gender = "Men",
                Price = 100,
            },
            new Shirt
            {
                Id = 2,
                Color = "Blue",
                Size = 12,
                Brand = "Adidas",
                Gender = "Women",
                Price = 120,
            },
            new Shirt
            {
                Id = 3,
                Color = "Green",
                Size = 14,
                Brand = "Puma",
                Gender = "Men",
                Price = 140,
            },
        };

        public bool ShirtExists(int id)
        {
            return shirts.Any(s => s.Id == id);
        }

        public List<Shirt> GetShirts()
        {
            return shirts;
        }

        public Shirt GetShirt(int id)
        {
            if (!ShirtExists(id))
                throw new Exception("Shirt not found.");

            return shirts.FirstOrDefault(s => s.Id == id)
                ?? throw new Exception("Shirt not found.");
        }

        public Shirt AddShirt(Shirt shirt)
        {
            shirts.Add(shirt);
            return shirt;
        }

        public Shirt UpdateShirt(int id, Shirt shirt)
        {
            var existingShirt =
                shirts.FirstOrDefault(s => s.Id == id) ?? throw new Exception("Shirt not found.");

            existingShirt.Color = shirt.Color;
            existingShirt.Size = shirt.Size;
            existingShirt.Brand = shirt.Brand;
            existingShirt.Gender = shirt.Gender;
            existingShirt.Price = shirt.Price;

            return existingShirt;
        }

        public bool DeleteShirt(int id)
        {
            var existingShirt = shirts.FirstOrDefault(s => s.Id == id);

            if (existingShirt == null)
                throw new Exception("Shirt not found.");

            shirts.Remove(existingShirt);
            return true;
        }
    }
}
