using Bogus;
using E_handelWEBapplication.Models;


namespace E_handelWEBapplication.Services
{
    public class FakeDataService
    {
        private readonly EHandelWebappContext _context;

        // Max antal records i varje tabell
        private const int MaxCategories = 3;
        private const int MaxProducts = 10;
        private const int MaxReviews = 30;

        public FakeDataService(EHandelWebappContext context)
        {
            _context = context;
        }

        public void SeedFakeData()
        {
            // === categorys ===
            if (_context.Categories.Count() < MaxCategories)
            {
                var missingCategories = MaxCategories - _context.Categories.Count();

                var categories = new List<Category>
                {
                    new Category { Name = "Electronics" },
                    new Category { Name = "Books" },
                    new Category { Name = "Home Appliances" }
                };

                _context.Categories.AddRange(categories.Take(missingCategories));
                _context.SaveChanges();
            }

            // === products ===
            if (_context.Products.Count() < MaxProducts)
            {
                var missingProducts = MaxProducts - _context.Products.Count();

                var categoryList = _context.Categories.ToList();

                var productFaker = new Faker<Product>()
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(10, 1000)))
                    .RuleFor(p => p.CategoryId, f => f.PickRandom(categoryList).Id);

                var products = productFaker.Generate(missingProducts);

                _context.Products.AddRange(products);
                _context.SaveChanges();
            }

            // === Reviews ===
            if (_context.Reviews.Count() < MaxReviews)
            {
                var missingReviews = MaxReviews - _context.Reviews.Count();

                var productList = _context.Products.ToList();

                var reviewFaker = new Faker<Review>()
                    .RuleFor(r => r.ProductId, f => f.PickRandom(productList).Id)
                    .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                    .RuleFor(r => r.Comment, f => f.Lorem.Sentence());

                var reviews = reviewFaker.Generate(missingReviews);

                _context.Reviews.AddRange(reviews);
                _context.SaveChanges();
            }
        }
    }
}
