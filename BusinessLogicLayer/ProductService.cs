
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class ProductService : IProductService
    {
        public List<ProductModel> GetAllData()
        {
            return CRUD.GetAllData();
        }

        public int InsertData(ProductModel model)
        {
            return CRUD.InsertData(model);
        }

        public int UpdateData(ProductModel model)
        {
            return CRUD.UpdateData(model);
        }

        public int DeleteData(int id)
        {
            return CRUD.DeleteData(id);
        }
    }
}
