

using System.Data;
using WebMyChart.Model;
using WebMyChart.Repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMyChart.Manage
{
    public class ProductionManage 
    {

        private readonly ProductionRepo _ProductionRepo;
        public ProductionManage(ProductionRepo EstimatesRepo, IConfiguration configuration)
        {
            _ProductionRepo = EstimatesRepo;
        }
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
        public string Test()
        {
            return _ProductionRepo.test();
            //return "hello word aaa";
        }
        public List<ProductionClass> GetData()
        {
            List<ProductionClass> Data_List = new List<ProductionClass>();
            Data_List = ConvertToList<ProductionClass>(_ProductionRepo.GetData());
            return Data_List;
        }

        public string DeleteData(int ID)
        {
            if (_ProductionRepo.CheckData(ID))
            {
                _ProductionRepo.DeleteData(ID);
                return $"Success Delete Production ID {ID}";
            }
            return $"No Production ID {ID}";
        }

        public string UpdateData(ProductionClass data)
        {
            if (_ProductionRepo.CheckData(data.ID))
            {
                _ProductionRepo.UpdateData(data);
                return $"Success Update Production ID {data.ID}";
            }
            return $"No Production ID {data.ID}";
        }

        public ProductionClass InsertData(ProductionClass data)
        {
            if (!_ProductionRepo.CheckDup(data))
            {
               _ProductionRepo.InsertData(data);
                return data;
            }
            return data;
        }
    }
}
