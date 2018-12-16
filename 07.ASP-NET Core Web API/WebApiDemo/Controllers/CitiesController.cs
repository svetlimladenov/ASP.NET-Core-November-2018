using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;

namespace WebApiDemo.Controllers
{
    public class CitiesController : ApiController
    {
        private Random random;
        private readonly MyDbContext context;

        public CitiesController(MyDbContext context)
        {
            this.random = new Random();
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CityInfo>> Get()
        {
            return context.Cities.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CityInfo> Get(string id)
        {
            var city = this.context.Cities.FirstOrDefault(c => c.Name == id);
            if (city == null)
            {
                return BadRequest();
            }

            return city;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<CityInfo> Post([FromBody] CityInfo cityInfo)
        {
            this.context.Cities.Add(cityInfo);
            this.context.SaveChanges();

            return this.CreatedAtAction(nameof(Get), new {id = cityInfo.Id}, cityInfo);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
