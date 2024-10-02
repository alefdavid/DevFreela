using AutoMapper;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService,
                                IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var returnById = await _projectService.GetById(id);

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
                var returnAll = await _projectService.GetAll();

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
        public async Task<IActionResult> Post([FromBody] CreateProjectDTO createProjectDTO)
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

                var projectCreateDTO = _mapper.Map<CreateProjectDTO>(createProjectDTO);

                var returnProject = await _projectService.Post(projectCreateDTO);

                if (returnProject == null)
                    return NotFound();

                return Ok(returnProject);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateProjectDTO updateProjectDTO, int id)
        {
            try
            {
                var searchProject = await _projectService.GetById(id);

                if (searchProject == null)
                    return NotFound();

                var projectUpdateDTO = _mapper.Map<UpdateProjectDTO>(updateProjectDTO);

                var returnProject = await _projectService.Put(projectUpdateDTO, id);

                if (returnProject == null)
                    return NotFound();

                return Ok(returnProject);
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
                var returnProject = await _projectService.Delete(id);

                if (returnProject == null)
                    return NotFound();

                return Ok(returnProject);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
