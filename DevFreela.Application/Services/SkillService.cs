using AutoMapper;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Domain;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        public readonly IRepositoryManager _repositoryManager;

        public readonly IMapper _mapper;

        private bool disposed;

        public SkillService(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<SkillDTO> GetById(int id)
        {
            if (id == null)
                throw new Exception();

            var listSkill = await _repositoryManager.SkillRepository.GetAll(); 
            var user = listSkill.Where(x => x.Id == id).FirstOrDefault();

            var skillDTO = _mapper.Map<SkillDTO>(user);

            return skillDTO;
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            var listSkills = await _repositoryManager.SkillRepository.GetAll();
            var listSkillsDTO = _mapper.Map<List<SkillDTO>>(listSkills);

            return listSkillsDTO;
        }

        public async Task<CreateSkillDTO> Post(CreateSkillDTO createSkillDTO)
        {
            if (createSkillDTO == null)
                throw new ArgumentNullException(nameof(createSkillDTO));

            var skill = _mapper.Map<CreateSkillDTO, Skill>(createSkillDTO);

            _repositoryManager.SkillRepository.Add(skill);
            await _repositoryManager.Save();

            return _mapper.Map<Skill, CreateSkillDTO>(skill);
        }

        public async Task<bool> Put(UpdateSkillDTO updateSkillDTO, int id)
        {
            if (updateSkillDTO == null)
                throw new ArgumentNullException(nameof(updateSkillDTO));

            var skill = await _repositoryManager.SkillRepository.GetAll();
            var skillExists = skill.Where(x => x.Id == id).FirstOrDefault();

            if (skillExists == null)
                throw new ArgumentNullException($"Não existe: {skillExists}.");

            _mapper.Map(updateSkillDTO, skillExists);

            _repositoryManager.SkillRepository.Put(skillExists);

            await _repositoryManager.Save();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var skill = await _repositoryManager.SkillRepository.GetAll();
            var returnSkillRemove = skill.Where(x => x.Id == id).FirstOrDefault();

            if (returnSkillRemove == null)
                throw new ArgumentNullException($"Não existe: {id}.");

            await _repositoryManager.SkillRepository.Delete(returnSkillRemove.Id);

            await _repositoryManager.Save();

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _repositoryManager.Dispose();
                }

                disposed = true;
            }
        }

        ~SkillService()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

