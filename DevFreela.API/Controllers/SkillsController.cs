using AutoMapper;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillService _skillService;

        private readonly IMapper _mapper;

        public SkillsController(ISkillService skillService,
                                IMapper mapper)
        {
            _skillService = skillService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var returnById = await _skillService.GetById(id);

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
                var returnAll = await _skillService.GetAll();

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
        public async Task<IActionResult> Post([FromBody] CreateSkillDTO createSkillDTO)
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

                var skillCreateDTO = _mapper.Map<CreateSkillDTO>(createSkillDTO);

                var returnSkill = await _skillService.Post(skillCreateDTO);

                if (returnSkill == null)
                    return NotFound();

                return Ok(returnSkill);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateSkillDTO updateSkillDTO, int id)
        {
            try
            {
                var searchSkill = await _skillService.GetById(id);

                if (searchSkill == null)
                    return NotFound();

                var skillUpdateDTO = _mapper.Map<UpdateSkillDTO>(updateSkillDTO);

                var returnSkill = await _skillService.Put(skillUpdateDTO, id);

                if (returnSkill == null)
                    return NotFound();

                return Ok(returnSkill);
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
                var returnSkill = await _skillService.Delete(id);

                if (returnSkill == null)
                    return NotFound();

                return Ok(returnSkill);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
