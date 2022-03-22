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
    public class KorisnikController : ControllerBase
    {  
      public BankaContext Context {get; set;}
        public KorisnikController(BankaContext context)
        {
            Context = context;
        }
        [Route("Korsinici/{jmbg}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(Int64 jmbg)
        {
            var korisnik = await Context.Korisnici
    
                            .Where(p=>p.JMBG == jmbg).FirstOrDefaultAsync();

            return Ok(korisnik);
        }
        [Route("KorsiniciID/{id}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int id)
        {
            var korisnik = await Context.Korisnici
    
                            .Where(p=>p.ID == id).FirstOrDefaultAsync();

            return Ok(korisnik);
        }


        [Route("DodatiKorisnika/{ime}/{prezime}/{jmbg}/{datumRodjenja}/{adresa}")]
        [HttpPost]
        public async Task<ActionResult> DodajKorisnika(string ime, string prezime, Int64 jmbg, DateTime datumRodjenja, string adresa)
        {
            if(string.IsNullOrWhiteSpace(ime) && ime.Length >= 50)
            {
                return BadRequest("Error! Ime is not OK!");
            }
            
            if(string.IsNullOrWhiteSpace(prezime) && prezime.Length <= 50)
            {
                return BadRequest("Error! Prezime is not OK!");
            }
            if(adresa.Length >= 50 && string.IsNullOrWhiteSpace(adresa))
            {
                return BadRequest("Error! Adresa is not OK!");
            }

            try
            {
                var korisnik = new Korisnik();
                korisnik.Ime = ime;
                korisnik.Prezime = prezime;
                korisnik.JMBG = jmbg;
                korisnik.Adresa =adresa;
                korisnik.DatumRodjenja = datumRodjenja;
                Context.Korisnici.Add(korisnik);
                await Context.SaveChangesAsync();
                return Ok($"Korisnik je uspesno dodat! ID je:{korisnik.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [Route("PromeniKorisnika")]
        [HttpPut]
        public async Task<ActionResult> Promeni([FromBody]Korisnik korisnik)
        {
            if(korisnik.ID < 0)
            {
                return BadRequest("Pogresan ID!");
            }
            if(korisnik.Ime.Length >= 50 && string.IsNullOrWhiteSpace(korisnik.Ime))
            {
                return BadRequest("Error! Ime is not OK!");
            }
            if(korisnik.Prezime.Length >= 50 && string.IsNullOrWhiteSpace(korisnik.Prezime))
            {
                return BadRequest("Error! Prezime is not OK!");
            }
            
            if(korisnik.DatumRodjenja == null)
            {
                return BadRequest("Error! Dodaj datum rodjenja");
            }
            if(korisnik.Adresa.Length >= 50 && string.IsNullOrWhiteSpace(korisnik.Adresa))
            {
                return BadRequest("Error! Adresa is not OK!");
            }
             var korisnikZaPromenu = await Context.Korisnici.FindAsync(korisnik.ID);
            korisnikZaPromenu.Ime = korisnik.Ime;
            korisnikZaPromenu.Prezime = korisnik.Prezime;
            korisnikZaPromenu.Adresa = korisnik.Adresa;
            korisnikZaPromenu.DatumRodjenja = korisnik.DatumRodjenja;
            await Context.SaveChangesAsync();
            return Ok("Korisnik promenjen!");

        }


        [Route("IzbrisiKorisnika/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(Int64 id)
        {
            if(id < 0)
            {
                return BadRequest("Pogresan ID!");
            }
            try 
            {
                var korisnikZaBrisanje = await Context.Korisnici.FindAsync(id);
                string ime = korisnikZaBrisanje.Ime;
                string prezime = korisnikZaBrisanje.Prezime;
                Context.Korisnici.Remove(korisnikZaBrisanje);
                await Context.SaveChangesAsync();
                return Ok($"Korisnik {ime} {prezime} je izbrisan"); 
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
