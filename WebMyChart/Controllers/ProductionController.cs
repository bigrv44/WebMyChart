using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMyChart.Manage;
using WebMyChart.Model;

namespace WebMyChart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionController : ControllerBase
    {
        //private EstimateManage item = new EstimateManage();
        private readonly ProductionManage _ProductionManage;

        public ProductionController(ProductionManage EstimateManage)
        {
            _ProductionManage = EstimateManage;
        }
        [HttpGet("test")]
        public string GetTest()
        {
            return _ProductionManage.Test();
        }
        [HttpGet("estimate")]
        public IEnumerable<ProductionClass> GetData()
        {
            return _ProductionManage.GetData();
        }
        [HttpPost("insert")]
        public ProductionClass InsertData(ProductionClass data)
        {
            ProductionClass res = _ProductionManage.InsertData(data);
            return res;
        }
        [HttpPost("update")]
        public string UpdateData(ProductionClass data)
        {
            string res = _ProductionManage.UpdateData(data);
            return res;
        }
        [HttpPost("delete")]
        public string DeleteData([FromQuery] int ID)
        {
            string res = _ProductionManage.DeleteData(ID);
            return res;
        }
    }
}