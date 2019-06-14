using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poc.API.Core.Dao;
using Poc.API.Core.Dao.Interfaces;
using Poc.API.Core.Dto;
using Poc.API.Core.Models;

namespace Poc.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        //readonly ILogger<ProduitController> logger;
        private readonly IProduitDao _produitDao;
        public ProduitsController(IProduitDao iProduitDao)
        {
            _produitDao = iProduitDao;
        }

        // GET: api/Produits
        [HttpGet]
        public IActionResult Get(int nb =5)
        {
            var list = _produitDao.GetList(nb);

            if (list != null)
                return Ok(list);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, list);
        }

        // GET: api/Produits/5
        [HttpGet("{id}")]
        public IActionResult Get(short id)
        {
            var produit = _produitDao.GetProduit(id);

            if (produit == null)
                return NotFound();
            else
                return Ok(produit);
        }

        // PUT: api/Produits/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] long id, [FromBody] ProduitDto produit)
        {
            if (produit != null)
            {
                try
                {
                    if (id != produit.Id)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        _produitDao.UpdateProduit(produit, id);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (produit==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
            }
            return NoContent();
        }

        // POST: api/Produits
        [HttpPost]
        public IActionResult Post([FromBody] ProduitDto produit)
        {
            if (_produitDao.CreateProduit(produit))
                return Ok();
            else
                return BadRequest();
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        public IActionResult Delete(short id)
        {
            var del = _produitDao.DeleteProduit(id);
            if (!del)
                return StatusCode(StatusCodes.Status500InternalServerError);
            else
                return Ok();
        }

        private ProduitDto ProduitExists(long id)
        {
            return _produitDao.GetProduit(id);
        }
    }
}
