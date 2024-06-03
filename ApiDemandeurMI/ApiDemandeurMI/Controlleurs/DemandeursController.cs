using ApiDemandeurMI.Dtos;
using ApiDemandeurMI.Models;
using ApiDemandeurMI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiDemandeurMI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandeursController : ControllerBase
    {
        private readonly DemandeursService _DemandeursService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;


        public DemandeursController(DemandeursService service, IMapper mapper, IConfiguration config)
        {   
            _DemandeursService = service;
            _mapper = mapper;
            _config = config;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandeursDto>>> Get()
        {
            var listeDemandeurs = await _DemandeursService.GetAsync();
            var demandeurs = _mapper.Map<IEnumerable<DemandeursDto>>(listeDemandeurs);
            return Ok(demandeurs);
        }

        [HttpGet("{id}", Name = "GetDemandeurById")]
        public async Task<ActionResult<Demandeur>> Get(int id)
        {
            var DemandeurToFind = await _DemandeursService.GetAsync(id);

            if (DemandeurToFind != null)
            {
                return Ok(DemandeurToFind);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Demandeur>> CreateDemandeur(Demandeur entity)
        {
            entity.Password = PasswordHashService.GenerateHash(entity.Password);
            await _DemandeursService.CreateAsync(entity);

            return CreatedAtRoute("GetSalleById", new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDemandeur(int id, Demandeur entity)
        {
            var demandeurToUpdate = await _DemandeursService.GetAsync(id);

            if (demandeurToUpdate == null)
            {
                return NotFound();
            }

            await _DemandeursService.UpdateAsync(id, entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemandeur(int id)
        {
            var book = await _DemandeursService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _DemandeursService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(AuthLogin authlogin)
        {
            var demandeurConnect = _DemandeursService.Authenticate(authlogin.Login, authlogin.Password);
            if (demandeurConnect != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , demandeurConnect.Login),
                };
                var token = _DemandeursService.GererateToken(_config["Jwt:Key"], claims);
                return Ok(token);
            }
            return Unauthorized();
        }

    }
    

}
