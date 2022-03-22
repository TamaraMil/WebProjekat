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
    public class RacunController : ControllerBase
    {
        public BankaContext Context { get; set; }

        public RacunController(BankaContext context)
        {
            Context = context;
        }

        [Route("Racuni")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            var racuni = await Context.Racuni
                        .Include(p=>p.vrstaRacuna)
                        .Include(p=>p.korisnik)
                            .ToListAsync();
            return Ok(racuni);
        }
        [Route("Racuni/{idKorisnika}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(int idKorisnika)
        {
            var racuni = await Context.Racuni
                        .Include(p=>p.vrstaRacuna)
                        .Include(p=>p.korisnik).Where(p=>p.korisnik.ID == idKorisnika)
                        .ToListAsync();
            return Ok(
                racuni.Select(
                    p=>new {
                        id = p.ID,
                        broj = p.Broj,
                        valuta = p.Valuta,
                        stanje = p.Stanje,
                        datumOtvaranja = p.DatumOtvaranja,
                        datumZatvaranja = p.DatumZatvaranja
                    }
                ).ToList()
                );

        }
        [Route("Racuni/{idVrsteRacuna}/{idKorisnika}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPremaVrsti(int idVrsteRacuna, int idKorisnika)
        {
            var racuni = await Context.Racuni
                        .Include(p=>p.vrstaRacuna).Where(p=>p.vrstaRacuna.id == idVrsteRacuna)
                        .Include(p=>p.korisnik).Where(p=>p.korisnik.ID == idKorisnika)
                        .ToListAsync();
            return Ok(
                racuni.Select(
                    p=>
                    new 
                    {
                        id = p.ID,
                        broj = p.Broj,
                        valuta = p.Valuta,
                        stanje = p.Stanje,
                        datumOtvaranja = p.DatumOtvaranja,
                        datumZatvaranja = p.DatumZatvaranja,
                        vrstaRacunaID = p.vrstaRacuna.id

                    }).ToList()
                );

        }
        
        
        [Route("DodatiRacun/{idKorisnika}/{brojRacuna}/{valuta}/{datum}/{vrstaRacunaid}")]
        [HttpPost]
        public async Task<ActionResult> DodajRacun(Int64 brojRacuna, string valuta, string datum, int idKorisnika,int vrstaRacunaid)
        {

            if (valuta.Length >= 30 && String.IsNullOrWhiteSpace(valuta))
            {
                return BadRequest("Valuta nije okej!");
            }
            try
            {   
                var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
                var vrstaRacuna = await Context.VrsteRacuna.Where(p=>p.id == vrstaRacunaid).FirstOrDefaultAsync();
                var racun = new Racun();
                racun.Broj = brojRacuna;
                racun.DatumOtvaranja = new DateTime();
                racun.Valuta = valuta;
                racun.Stanje = 0.0;
                racun.korisnik = korisnik;
                racun.vrstaRacuna = vrstaRacuna;
                Context.Racuni.Add(racun);
                await Context.SaveChangesAsync();
                return Ok($"Racun {racun.Broj} je dodat.");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [Route("PromenitiRacun")]
        [HttpPut]
        public async Task<ActionResult> PromeniRacun([FromBody]Racun racun)
        {
            if(racun.ID < 0)
            {
                return BadRequest("ID nije u redu");
            }
            if(racun.Broj == null)
            {
                return BadRequest("Broj racuna nije u redu!");
            }
            if(racun.Stanje == null)
            {
                return BadRequest("Stanje nije u redu");
            }
            if(racun.Valuta.Length >= 30 && String.IsNullOrWhiteSpace(racun.Valuta))
            {
                return BadRequest("Valute nije u redu");
            }
            try
            {
                Context.Racuni.Update(racun);
                await Context.SaveChangesAsync();
                return Ok($"Racun {racun.Broj} je promenjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiRacun/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiRacun(int id)
        {
            if(id < 0)
            {
                return BadRequest("ID nije u redu");
            }
            try
            {
                var racunZaBrijanje = await Context.Racuni.FindAsync(id);
                Int64? broj = racunZaBrijanje.Broj;
                Context.Racuni.Remove(racunZaBrijanje);
                await Context.SaveChangesAsync();
                return Ok($"Racun {broj} je izbrisan");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}