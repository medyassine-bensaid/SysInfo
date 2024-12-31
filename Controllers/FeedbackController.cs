//using Microsoft.AspNetCore.Mvc;
//using SysInfo.Models;
//using SysInfo.Repositories;

//namespace SysInfo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FeedbackController : ControllerBase
//    {
//        private readonly IFeedbackRepository _feedbackRepository;

//        public FeedbackController(IFeedbackRepository feedbackRepository)
//        {
//            _feedbackRepository = feedbackRepository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacks()
//        {
//            var feedbacks = await _feedbackRepository.GetAllFeedbacksAsync();
//            return Ok(feedbacks);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
//        {
//            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
//            if (feedback == null)
//            {
//                return NotFound($"Feedback with ID {id} not found.");
//            }
//            return Ok(feedback);
//        }

//        [HttpPost]
//        public async Task<ActionResult> AddFeedback([FromBody] Feedback feedback)
//        {
//            await _feedbackRepository.AddFeedbackAsync(feedback);
//            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedback.Id }, feedback);
//        }
//    }

//}
