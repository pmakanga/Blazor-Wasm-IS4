using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = repository.Employee.GetAllEmployees(trackChanges: false);

                var employeesDto = mapper.Map<IEnumerable<EmployeeDto>>(employees);

                return Ok(employeesDto);
            }
            catch (Exception ex)
            {

                logger.LogError($"Something went wrong in the {nameof(GetEmployees)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
