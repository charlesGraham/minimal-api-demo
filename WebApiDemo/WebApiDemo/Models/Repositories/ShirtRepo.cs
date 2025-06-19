namespace WebApiDemo.Models.Repositories
{
    public static class ShirtRepo
    {
        private static List<Shirt> _shirts = new List<Shirt>
        {
            new Shirt
            {
                Id = 1,
                Color = "Red",
                Size = 10,
                Brand = "Nike",
                Gender = "Men",
                Price = 30.00,
            },
            new Shirt
            {
                Id = 2,
                Color = "Blue",
                Size = 12,
                Brand = "Adidas",
                Gender = "Women",
                Price = 25.00,
            },
            new Shirt
            {
                Id = 3,
                Color = "Green",
                Size = 14,
                Brand = "Puma",
                Gender = "Men",
                Price = 35.00,
            },
        };

        public static bool ShirtExists(int id)
        {
            return _shirts.Any(s => s.Id == id);
        }

        public static List<Shirt> GetShirts()
        {
            return _shirts;
        }

        public static Shirt GetShirt(int id)
        {
            var foundShirt = _shirts.FirstOrDefault(s => s.Id == id);

            if (foundShirt == null)
                throw new Exception("Shirt not found.");

            return foundShirt;
        }

        public static Shirt AddShirt(Shirt shirt)
        {
            if (ShirtExists(shirt.Id))
                throw new Exception("Shirt already exists.");

            shirt.Id = _shirts.Count + 1;
            _shirts.Add(shirt);

            return _shirts.Last();
        }

        public static Shirt UpdateShirt(Shirt shirt)
        {
            var existingShirt = _shirts.FirstOrDefault(s => s.Id == shirt.Id);

            if (existingShirt == null)
                throw new Exception("Shirt not found.");

            existingShirt.Color = shirt.Color;
            existingShirt.Size = shirt.Size;
            existingShirt.Brand = shirt.Brand;
            existingShirt.Gender = shirt.Gender;
            existingShirt.Price = shirt.Price;

            return existingShirt;
        }

        public static bool DeleteShirt(int id)
        {
            var existingShirt = _shirts.FirstOrDefault(s => s.Id == id);

            if (existingShirt == null)
                throw new Exception("Shirt not found.");

            return _shirts.Remove(existingShirt);
        }
    }
}
