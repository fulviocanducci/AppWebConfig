using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Produces("application/json")]
    [Route("api/credits")]
    public class CreditsController : Controller
    {

        public IRepositoryCredit Repository { get; }

        public CreditsController(IRepositoryCredit repository)
        {
            Repository = repository;
        }

        // GET: api/Credits
        [HttpGet]
        public IEnumerable<Credit> Get()
        {
            return Repository.List();
        }

        // GET: api/Credits/5
        [HttpGet("{id}", Name = "Get")]
        public Credit Get(int id)
        {
            return Repository.Find(id);
        }
        
        // POST: api/Credits
        [HttpPost]
        public ObjectResult Post([FromBody]Credit value)
        {
            return Ok(Repository.Insert(value));
        }
        
        // PUT: api/Credits/5
        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromBody]Credit value)
        {
            return Ok(Repository.Edit(value));
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ObjectResult Delete(int id)
        {
            return Ok(Repository.Delete(id));
        }
    }
}
