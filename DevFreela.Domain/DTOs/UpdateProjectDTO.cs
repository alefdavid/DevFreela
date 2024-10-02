using DevFreela.Domain.Entities;

namespace DevFreela.Domain.DTOs
{
    public class UpdateProjectDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public User Client { get; set; }
        public int IdFreelancer { get; set; }
        public User Freelancer { get; set; }
        public User FreelanceProjects { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? StartedAt { get; set; }
    }
}
