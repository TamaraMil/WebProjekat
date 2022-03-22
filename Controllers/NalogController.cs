using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Banka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NalogController : ControllerBase
    {
        public BankaContext Context { get; set; }

        public NalogController(BankaContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("Login/{emailPassword}")]
        public async Task<ActionResult> Preuzmi(string  emailPassword)
        {
            try
            {
                var bytes = Convert.FromBase64String(emailPassword);
                string[] niz = Encoding.UTF8.GetString(bytes).Split(":");

                var nalog = await Context.Nalozi.Include(p=>p.korisnik).Where(p => p.email == niz[0] && p.Lozinka == niz[1]).FirstOrDefaultAsync();

                if (nalog == null)
                {
                    return BadRequest("Pogresna lozinka ili email");
                }
                return Ok(
                    new{
                    admin = nalog.admin,
                    korisnikID = nalog.korisnik == null ? -1 : nalog.korisnik.ID
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("DodajNalog")]
        public async Task<ActionResult> Dodaj([FromBody]Nalog nalog)
        {
            if(nalog.email.Length >= 70 && String.IsNullOrWhiteSpace(nalog.email))
            {
                return BadRequest("Email nije u redu!");
            }
            if(nalog.Lozinka.Length >= 30 && String.IsNullOrWhiteSpace(nalog.Lozinka))
            {
                return BadRequest("Lozinka nije u redu!");
            }
            if(nalog.admin == null)
            {
                return BadRequest("Admin nije u redu!");
            }
            try
            {
                Context.Nalozi.Add(nalog);
                await Context.SaveChangesAsync();
                return Ok($"Nalog {nalog.email} je dodat!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("IzmeniLozinka/{emailPassword}")]
        public async Task<ActionResult> Izmneni(string emailPassword)
        {
            try
            {
            var bytes = Convert.FromBase64String(emailPassword);
            string[] niz = Encoding.UTF8.GetString(bytes).Split(":");

            var nalog = await Context.Nalozi.Where(p=>p.email == niz[0]).FirstOrDefaultAsync();



            if(niz[0].Length >= 70 && String.IsNullOrWhiteSpace(niz[0]))
            {
                return BadRequest("Email nije u redu!");
            }
            if(niz[1].Length >= 30 && String.IsNullOrWhiteSpace(niz[1]))
            {
                return BadRequest("Lozinka nije u redu!");
            }
                nalog.Lozinka = niz[1];
                await Context.SaveChangesAsync();
                return Ok($"Lozinka {nalog.email} je izmenjena");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("IzbrisiNalog/{id}")]
        public async Task<ActionResult> IzbrisiNalog(int id)
        {
            if(id < 0)
            {
                return BadRequest("Id nije dobar!");
            }
            try
            {
                var nalogZaBrisanje = await Context.Nalozi.FindAsync(id);
                string emailNaloga = nalogZaBrisanje.email;
                Context.Nalozi.Remove(nalogZaBrisanje);
                await Context.SaveChangesAsync();
                return Ok($"Nalog {emailNaloga} je izbrisan");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }



    }
}