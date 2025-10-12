using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ICategoryRepository
    {
        //GetAllCategories
        IEnumerable<Category> GetAllCategories();
        //GetCategoryById
        Category? GetCategoryById(int id);
        //AddCategory
        int Add(Category category);
        //UpdateCategory
        int Update(Category category);
        //DeleteCategory
        int Remove(int id);


    }
}
