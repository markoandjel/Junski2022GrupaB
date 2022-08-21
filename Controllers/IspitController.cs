using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Template.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IspitController : ControllerBase
    {
        IspitDbContext Context { get; set; }

        public IspitController(IspitDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("PreuzmiProdavnice")]
        public async Task<ActionResult> PreuzmiProdavnice()
        {
            try
            {

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var rez = await Context.Prodavnice.Select(p=>new {
                Id=p.Id,
                Naziv=p.Naziv
            }).ToListAsync();
            return Ok(rez);
        }

        [HttpGet]
        [Route("PreuzmiMarke/{idProd}")]
        public async Task<ActionResult> PreuzmiMarke(int idProd)
        {
            try
            {
                var rez = Context.Spojevi.Where(p=>p.ProdavnicaSpoj.Id==idProd).Include(p=>p.AutomobilSpoj.Marka)
                .Select(p=>new 
                {
                    Id=p.AutomobilSpoj.Marka.Id, 
                    Naziv=p.AutomobilSpoj.Marka.Naziv
                }).Distinct();
                return Ok(await rez.ToListAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("PreuzmiModele/{idProd}/{idMarka}")]
        public async Task<ActionResult> PreuzmiModele(int idProd,int idMarka)
        {
            try
            {
                var rez = Context.Spojevi.Where(p=>p.ProdavnicaSpoj.Id==idProd && p.AutomobilSpoj.Marka.Id==idMarka)
                .Include(p=>p.AutomobilSpoj.Model).Select
                (p=>new 
                {
                    Id=p.AutomobilSpoj.Model.Id, 
                    Naziv=p.AutomobilSpoj.Model.Naziv
                }).Distinct();
                return Ok(await rez.ToListAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("PreuzmiBoje/{idProd}/{idMarka}/{idModel}")]
        public async Task<ActionResult> PreuzmiBoje(int idProd,int idMarka,int idModel)
        {
            try
            {
                var rez = Context.Spojevi.Where(p=>p.ProdavnicaSpoj.Id==idProd && p.AutomobilSpoj.Marka.Id==idMarka && p.AutomobilSpoj.Model.Id==idModel)
                .Include(p=>p.AutomobilSpoj.Boja).Select
                (p=>new 
                {
                    Id=p.AutomobilSpoj.Boja.Id, 
                    Naziv=p.AutomobilSpoj.Boja.Naziv
                }).Distinct();
                return Ok(await rez.ToListAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpGet]
        [Route("PreuzmiAutomobile/{idProd}/{idMarka}")]
        public async Task<ActionResult> PreuzmiAutomobile(int idProd,int idMarka,int idModel,int idBoja)
        {
            if(idProd<0)
            throw new Exception("Niste uneli potreban podatak: idProd");
            if(idMarka<0)
            throw new Exception("Niste uneli potreban podatak: idMarka");

            try
            {
                var rez = Context.Spojevi.Include(p=>p.AutomobilSpoj)
                .Where(p=>p.ProdavnicaSpoj.Id==idProd && p.AutomobilSpoj.Marka.Id==idMarka);

                if(idModel>0)
                rez=rez.Where(p=>p.AutomobilSpoj.Model.Id==idModel);

                if(idModel>0 && idBoja>0)
                rez=rez.Where(p=>p.AutomobilSpoj.Model.Id==idModel && p.AutomobilSpoj.Boja.Id==idBoja);

                return Ok(await rez.Select(p=>new{
                    Id=p.Id,
                    Marka=p.AutomobilSpoj.Marka.Naziv,
                    Model=p.AutomobilSpoj.Model.Naziv,
                    Boja=p.AutomobilSpoj.Boja.Naziv,
                    Kolicina=p.Kolicina,
                    Datum=p.Datum,
                    Cena=p.Cena
                }).ToListAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        [Route("KupiAutomobil/{idSpoj}")]
        public async Task<ActionResult> KupiAutomobil(int idSpoj)
        {
            try
            {
                var pom = Context.Spojevi.Where(p=>p.Id==idSpoj).FirstOrDefault();
                if(pom==null || pom.Kolicina<1)
                throw new Exception("Nesto nije uredu sa bazom");

                pom.Kolicina--;
                pom.Datum=DateTime.Now;

                Context.Spojevi.Update(pom);
                await Context.SaveChangesAsync();
                return Ok(pom.Datum);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
