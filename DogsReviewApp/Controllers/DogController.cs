using AutoMapper;
using DogsReviewApp.Dto;
using DogsReviewApp.Interfaces;
using DogsReviewApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PokemonReviewApp.Models;

namespace DogsReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public DogController(IDogRepository dogRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dog>))]
        public IActionResult GetDogs()
        {
            var dogs = _mapper.Map<List<DogDto>>(_dogRepository.GetDogs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dogs);
        }

        [HttpGet("{dogId}")]
        [ProducesResponseType(200, Type = typeof(Dog))]
        [ProducesResponseType(400)]
        public IActionResult GetDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();

            var dog = _mapper.Map<DogDto>(_dogRepository.GetDog(dogId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dog);
        }

        [HttpGet("{dogId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetDogRating(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();

            var rating = _dogRepository.GetDogRating(dogId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDog([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] DogDto dogCreate)
        {
            if (dogCreate == null)
                return BadRequest(ModelState);

            var dogs = _dogRepository.GetDogs()
                .Where(c => c.Name.Trim().ToUpper() == dogCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (dogs != null)
            {
                ModelState.AddModelError("", "Dog is already exist!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var dogMap = _mapper.Map<DogDto>(dogCreate);
            var dogMap = _mapper.Map<Dog>(dogCreate);

            if (!_dogRepository.CreateDog(ownerId, categoryId, dogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDog([FromQuery]int? ownerId, [FromQuery]int? categoryId, int dogId, [FromBody] DogDto updateDog)
        {
            if (updateDog == null)
                return BadRequest(ModelState);

            //if (dogId != updateDog.Id)
            //    return BadRequest(ModelState);

            //if (!_dogRepository.DogExists(dogId))
            //    return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var dogMap = _mapper.Map<Dog>(updateDog);

            if (!_dogRepository.UpdateDog(ownerId, categoryId, dogId, dogMap))
            {
                ModelState.AddModelError("", "Something went wrong to updating dog!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{dogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDog(int dogId)
        {
            if (!_dogRepository.DogExists(dogId))
                return NotFound();

            var reviewsToDelete = _reviewRepository.GetReviewsOfADog(dogId);

            var dogToDelete = _dogRepository.GetDog(dogId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong to delete reviews!");
            }

            if (!_dogRepository.DeleteDog(dogToDelete))
            {
                ModelState.AddModelError("", "Something went wrong to delete dog!");
            }

            return NoContent();
        }
    }
}
