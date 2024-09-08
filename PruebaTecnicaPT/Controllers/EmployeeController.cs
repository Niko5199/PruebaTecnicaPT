using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PruebaTecnicaPT.Models;
using PruebaTecnicaPT.Models.Common;
using PruebaTecnicaPT.Models.Dtos;
using PruebaTecnicaPT.Repository.IRepository;
using System.Linq.Expressions;
using static PruebaTecnicaPT.Models.Response.EmployeesResponse;

namespace PruebaTecnicaPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllEmployees")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ResponseGeneric> GetEmployees(int pageNumber = 1, int pageSize = 10, string nameEmployee = "")
        {
            var response = new ResponseGeneric<EmployesListRespons>(false, "");
            try
            {
                var employeesTotal = _employeeRepository.GetTotalEmployees();
                var employeesList = _employeeRepository.GetEmployees(pageNumber, pageSize, nameEmployee);

                if (employeesList.Count > 0)
                {
                    var employeesListDto = new List<EmployeeDto>();
                    foreach (var employee in employeesList)
                    {
                        employeesListDto.Add(_mapper.Map<EmployeeDto>(employee));
                    }

                    response.Result = new EmployesListRespons
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalEmployees = employeesTotal,
                        TotalPages = (int)Math.Ceiling(employeesTotal / (double)pageSize),
                        Employees = employeesListDto,
                    };
                    response.Message = "Datos recuperados correctamente.";
                }
                else
                {
                    response.Result = new EmployesListRespons();
                    response.Message = "Aun no hay elementos.";
                }
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Message = e.Message;
            }
            return Ok(response);
        }

        [HttpGet("GetEmployee/{employeeId:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ResponseGeneric> GetEmployee(int employeeId)
        {
            var response = new ResponseGeneric<EmployeeDto>(false, "");
            try
            {
                var employeeItem = _employeeRepository.GetEmployee(employeeId);
                if (employeeItem == null)
                {
                    response.Message = "No se encontro el elemento proporcionado, intente por favor con otro";
                }
                else
                {
                    var employeeMapper = _mapper.Map<EmployeeDto>(employeeItem);
                    response.Result = employeeMapper;
                    response.Message = "Datos recuperados correctamente.";
                }
            }
            catch (Exception e)
            {
                response.Error = true;
                response.Message = e.Message;
            }

            return Ok(response);
        }

        [HttpPost("CreateEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ResponseGeneric> CreateEmployee([FromBody] CreatedEmployeeDto employeeDto)
        {
            var response = new ResponseGeneric(false, "");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (employeeDto == null)
                {
                    throw new Exception("Es necesario pasar todos los campos sin excepción, por favor intente de nuevo.");
                }

                if (_employeeRepository.ExistsEmployee(employeeDto.FirstName))
                {
                    throw new Exception("El empleado ya existe por favor intente con otro.");
                }

                var employee = _mapper.Map<Employee>(employeeDto);

                if (!_employeeRepository.CreateEmployee(employee))
                {
                    throw new Exception($"Ocurrio un error inesperado al registrar {employee.FirstName} {employee.LastName}, por favor intente de nuevo.");
                }

                return CreatedAtRoute("GetEmployee", new { employeeId = employee.EmployeeId }, employee);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Error = true;
            }

            return BadRequest(response);

        }

        [HttpPatch("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ResponseGeneric> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var response = new ResponseGeneric(false, "");
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (employeeDto == null || employeeDto.EmployeeId <= 0) {
                    throw new Exception("The EmployeeId or the object is incorrect please check");
                }

                if (!_employeeRepository.ExistsEmployee(employeeDto.EmployeeId))
                {
                    throw new Exception("The employee don't exists, please try with another");
                }

                var employee = _mapper.Map<Employee>(employeeDto);
                if (!_employeeRepository.UpdateEmployee(employee)) {
                    throw new Exception($"Somenthing is wrong to save the register {employee.FirstName} {employee.LastName}");
                }
                response.Message = "Datos actualizados correctamente";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Error = true;
            }
            return BadRequest(response);
        } 
        
        [HttpDelete("DeleteEmployee/{employeeId:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ResponseGeneric> DeleteEmployee(int employeeId)
        {
            var response = new ResponseGeneric(false, "");
            try
            {
                if (employeeId <= 0) {
                    throw new Exception("The employeeId is incorrect please check");
                }

                if (!_employeeRepository.ExistsEmployee(employeeId))
                {
                    throw new Exception("The employee don't exists, please try with another");
                }

                var employeeSelected = _employeeRepository.GetEmployee(employeeId);

                if (!_employeeRepository.DeleteEmployee(employeeSelected)) {
                    throw new Exception($"Ocurrio un problema al borrar el registro, intente de nuevo.");
                }
                response.Message = "Elemento eliminado correctamente.";
                return Ok(response);
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Error = true;
            }
            return BadRequest(response);
        }
    }
}
