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
                racun.DatumOtvaranja = Convert.ToDateTime(datum);
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

        [Route("PromenitiRacun/{brojRacuna}/{valuta}/{stanje}/{datumOtvaranja}")]
        [HttpPut]
        public async Task<ActionResult> PromeniRacun(int brojRacuna,string valuta, double stanje, string datumOtvaranja )
        {
            if(brojRacuna < 0)
            {
                return BadRequest("ID nije u redu");
            }
            if(valuta.Length >= 30 && String.IsNullOrWhiteSpace(valuta))
            {
                return BadRequest("Valute nije u redu");
            }
            try
            {
                var racun = await Context.Racuni.Where(p=>p.Broj==Convert.ToInt64(brojRacuna)).FirstOrDefaultAsync();
                racun.Stanje = stanje;
                racun.DatumOtvaranja = Convert.ToDateTime(datumOtvaranja);
                racun.Valuta = valuta;
                await Context.SaveChangesAsync();
                return Ok($"Racun {racun.Broj} je promenjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromenitiRacun/{brojRacuna}/{datumZatvaranja}")]
        [HttpPut]
        public async Task<ActionResult> PromeniRacun(int brojRacuna ,string datumZatvaranja )
        {
            if(brojRacuna < 0)
            {
                return BadRequest("ID nije u redu");
            }
            try
            {
                var racun = await Context.Racuni.Where(p=>p.Broj==Convert.ToInt64(brojRacuna)).FirstOrDefaultAsync();
                racun.DatumZatvaranja = Convert.ToDateTime(datumZatvaranja);
                await Context.SaveChangesAsync();
                return Ok($"Racun {racun.Broj} je promenjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiRacun/{brojRacuna}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiRacun(int brojRacuna)
        {
            if(brojRacuna < 0)
            {
                return BadRequest("ID nije u redu");
            }
            try
            {
                var racunZaBrijanje = await Context.Racuni.Where(p=>p.Broj == brojRacuna).FirstOrDefaultAsync();
                Context.Racuni.Remove(racunZaBrijanje);
                await Context.SaveChangesAsync();
                return Ok($"Racun {brojRacuna} je izbrisan");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}