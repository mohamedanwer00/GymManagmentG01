using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using System.Text.Json;

namespace GymManagmentDAL.Data.SeedData
{
    public static class GymDbContextSeeding
    {
        public static bool SeedDate(GymDbContext dbContext)
        {
            try
            {
                var hasPlans = dbContext.Plans.Any();
                var hasCategories = dbContext.Categories.Any();

                if (hasPlans && hasCategories)
                    return false;

                if (!hasPlans)
                {
                    var plans = LoadDataFromJson<Plan>("Plans.json");

                    if (plans.Any())
                    {
                        dbContext.AddRange(plans);
                        //dbContext.SaveChanges();
                    }

                }

                if (!hasCategories)
                {
                    var categories = LoadDataFromJson<Category>("Categories.json");
                    if (categories.Any())
                    {
                        dbContext.AddRange(categories);
                        //dbContext.SaveChanges();
                    }
                }

                return dbContext.SaveChanges() > 0;
            }
            catch (Exception)
            {
                Console.WriteLine("Seeding Failed");

                return false;
            }

        }

        private static List<T> LoadDataFromJson<T>(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);

            if (!File.Exists(filePath))
                return [];

            var jsonData = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? [];
        }

    }
}

