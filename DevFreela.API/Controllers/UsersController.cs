using AutoMapper;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UsersController (IUserService userService, 
                                IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var returnById = await _userService.GetById(id);

                if (returnById == null)
                    return NotFound();

                return Ok(returnById);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var returnAll = await _userService.GetAll();

                if (returnAll == null)
                    return NotFound();

                return Ok(returnAll);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var lista = ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(item => item.Value.Errors.ToList(), (item, errors) => new { item, errors })
                        .Select(x => x.errors.ErrorMessage)
                        .ToList();

                    return BadRequest(lista);
                }

                var userCreateDTO = _mapper.Map<CreateUserDTO>(createUserDTO);

                var returnUser = await _userService.Post(userCreateDTO);

                if (returnUser == null)
                    return NotFound();

                return Ok(returnUser);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateUserDTO updateUserDTO, int id)
        {
            try
            {
                var searchUser = await _userService.GetById(id);

                if (searchUser == null)
                    return NotFound();

                var userUpdateDTO = _mapper.Map<UpdateUserDTO>(updateUserDTO);

                var returnUser = await _userService.Put(userUpdateDTO, id);

                if (returnUser == null)
                    return NotFound();

                return Ok(returnUser);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var returnUser = await _userService.Delete(id);

                if (returnUser == null)
                    return NotFound();

                return Ok(returnUser);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
