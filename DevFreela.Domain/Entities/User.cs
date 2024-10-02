namespace DevFreela.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Project> FreelanceProjects { get; set; }

    }
}
