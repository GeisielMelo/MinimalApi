namespace WebApiDemo.Models.Repositories
{
    public static class ShirtRepository
    {
            private static List<Shirt> shirts = new List<Shirt>() {
            new Shirt { Id = 1, Brand = "Rike", Color = "Blue", Gender = "Men", Price = 30, Size = 15},
            new Shirt { Id = 2, Brand = "Nuke", Color = "Blue", Gender = "Men", Price = 30, Size = 10},
            new Shirt { Id = 3, Brand = "utke", Color = "Blue", Gender = "Women", Price = 30, Size = 8},
            new Shirt { Id = 4, Brand = "meke", Color = "Blue", Gender = "Women", Price = 30, Size = 11},
            new Shirt { Id = 5, Brand = "hibke", Color = "Blue", Gender = "Men", Price = 30, Size = 13},
            new Shirt { Id = 6, Brand = "deke", Color = "Blue", Gender = "Men", Price = 30, Size = 12}
        };

        public static List<Shirt> GetShirts() {
            return shirts;
        }

        public static bool ShirtExists(int id) {
            return shirts.Any(x => x.Id == id);
        }

        public static Shirt? GetShirtById(int id) {
            return shirts.FirstOrDefault(x => x.Id == id);
        }
    }
}