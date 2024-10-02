using AutoMapper;
using DevFreela.Application.Interfaces.Services;
using DevFreela.Domain;
using DevFreela.Domain.DTOs;
using DevFreela.Domain.Entities;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        public readonly IRepositoryManager _repositoryManager;

        public readonly IMapper _mapper;

        private bool disposed;

        public ProjectService(IRepositoryManager repositoryManager,
            IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }
        public async Task<ProjectDTO> GetById(int id)
        {
            if (id == null)
                throw new Exception();

            var listProject = await _repositoryManager.ProjectRepository.GetAll();
            var project = listProject.Where(x => x.Id == id).FirstOrDefault();

            var projectDTO = _mapper.Map<ProjectDTO>(project);

            return projectDTO;
        }

        public async Task<List<ProjectDTO>> GetAll()
        {
            var listProjects = await _repositoryManager.ProjectRepository.GetAll();
            var listProjectsDTO = _mapper.Map<List<ProjectDTO>>(listProjects);

            return listProjectsDTO;
        }

        public async Task<CreateProjectDTO> Post(CreateProjectDTO createProjectDTO)
        {
            if (createProjectDTO == null)
                throw new ArgumentNullException(nameof(createProjectDTO));

            var project = _mapper.Map<CreateProjectDTO, Project>(createProjectDTO);

            _repositoryManager.ProjectRepository.Add(project);
            await _repositoryManager.Save();

            return _mapper.Map<Project, CreateProjectDTO>(project);
        }

        public async Task<bool> Put(UpdateProjectDTO updateProjectDTO, int id)
        {
            if (updateProjectDTO == null)
                throw new ArgumentNullException(nameof(updateProjectDTO));

            var project = await _repositoryManager.ProjectRepository.GetAll();
            var projectExists = project.Where(x => x.Id == id).FirstOrDefault();

            if (projectExists == null)
                throw new ArgumentNullException($"Não existe: {projectExists}.");

            _mapper.Map(updateProjectDTO, projectExists);

            _repositoryManager.ProjectRepository.Put(projectExists);

            await _repositoryManager.Save();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var project = await _repositoryManager.ProjectRepository.GetAll();
            var returnProjectRemove = project.Where(x => x.Id == id).FirstOrDefault();

            if (returnProjectRemove == null)
                throw new ArgumentNullException($"Não existe: {id}.");

            await _repositoryManager.ProjectRepository.Delete(returnProjectRemove.Id);

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

        ~ProjectService()
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

