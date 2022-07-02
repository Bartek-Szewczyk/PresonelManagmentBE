using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresonelManagmentBE.Interface;

namespace PresonelManagmentBE.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepo _repository;

        public DashboardController(IDashboardRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            var result =  _repository.GetDashboard();
            return Ok(result);
        }
    }
}