
using Models;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public interface IProductService
    {
        List<ProductModel> GetAllData();

        int InsertData(ProductModel model);

        int UpdateData(ProductModel model);

        int DeleteData(int id);
    }
}
