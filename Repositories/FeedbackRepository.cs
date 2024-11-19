using Microsoft.EntityFrameworkCore;
using SysInfo.Infrastructure.Data;
using SysInfo.Models;

namespace SysInfo.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.Client)
                .Include(f => f.Project)
                .ToListAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks
                .Include(f => f.Client)
                .Include(f => f.Project)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackByClientIdAsync(int clientId)
        {
            return await _context.Feedbacks
                .Include(f => f.Project)
                .Where(f => f.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackByProjectIdAsync(int projectId)
        {
            return await _context.Feedbacks
                .Include(f => f.Client)
                .Where(f => f.ProjectId == projectId)
                .ToListAsync();
        }
    }

}
