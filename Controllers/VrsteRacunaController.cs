using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Banka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VrstaRacunaController : ControllerBase
    {  
      public BankaContext Context {get; set;}
        public VrstaRacunaController(BankaContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("PreuzmiVrsteRacuna")]
         public async Task<ActionResult> Preuzmi()
        {
            var vrste = await Context.VrsteRacuna.ToListAsync();
            return Ok(vrste);
        }

        [HttpPost]
        [Route("DodajVrstuRacuna")]
        public async Task<ActionResult> DodajTransakciju([FromBody] VrsteRacuna vrsta)
        {
            if (vrsta.vrstaNaziv.Length > 50 && String.IsNullOrWhiteSpace(vrsta.vrstaNaziv))
            {
                return BadRequest("Nije dobar naziv vrste racuna");
            }
            try
            {
                Context.VrsteRacuna.Add(vrsta);
                await Context.SaveChangesAsync();
                return Ok("Vrsta racuna je okej snimljena");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}