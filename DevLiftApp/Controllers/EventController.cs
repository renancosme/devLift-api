using Business;
using Microsoft.AspNetCore.Mvc;
using Business.Entities;
using DevLiftApp.Model;

namespace DevLiftApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Event")]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Events
        /// </summary>
        /// <param name="pageIndex">The initial page number</param>
        /// <param name="pageSize">The total of events to return</param>
        /// <returns>A list of Events</returns>
        /// <response code="200">Success on getting data</response>
        /// <response code="400">Invalid pageIndex and/or pageSize</response>
        [HttpGet("{pageIndex}/{pageSize}")]
        [ProducesResponseType(typeof(EventPageResult), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var eventCount = _unitOfWork.Events.GetTotal();
            var eventPageResult = _unitOfWork.Events.GetEvents(pageIndex, pageSize);
            return Ok(new EventPageResult(eventCount, eventPageResult));
        }

        /// <summary>
        /// Get an specific Event 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An specific Event</returns>        
        /// <response code="200">Success on getting data</response>
        /// <response code="404">Resource not found</response>
        [HttpGet("{id}", Name = "GetEvent")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            Event eventModel = _unitOfWork.Events.Get(id);

            if (eventModel == null)
            {
                return NotFound();
            }

            return Ok(eventModel);
        }

        /// <summary>
        /// Creates an Event
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly-created Event</returns>
        /// <response code="201">Returns the newly-created Event</response>
        /// <response code="400">If the event is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(Event), 201)]
        [ProducesResponseType(400)]
        public IActionResult PostEvent([FromBody] Event item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _unitOfWork.Events.Add(item);
            _unitOfWork.Complete();

            return CreatedAtRoute("GetEvent", new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates a specific Event
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <param name="item">Event</param>
        /// <returns>No content</returns>
        /// <response code="204">Success on update</response>
        /// <response code="400">If item null or id different</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Event), 204)]
        [ProducesResponseType(400)]
        public IActionResult Update(int id, [FromBody]Event item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var eventToUpdate = _unitOfWork.Events.Get(id);
            eventToUpdate.Name = item.Name;
            eventToUpdate.When = item.When;
            _unitOfWork.Complete();

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific Event
        /// </summary>
        /// <param name="id">Event id</param>
        /// <returns>No content</returns>
        /// <response code="204">Success on delete</response>
        /// <response code="404">Resource not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            var eventToDelete = _unitOfWork.Events.Get(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            _unitOfWork.Events.Remove(eventToDelete);
            _unitOfWork.Complete();

            return new NoContentResult();
        }
    }
}