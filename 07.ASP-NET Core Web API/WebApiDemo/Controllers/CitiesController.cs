using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET api/cities/
        [HttpGet]
        public ActionResult<IEnumerable<CityInfo>> Get()
        {
            return context.Cities.ToList();
        }

        // GET api/cities/Sofia
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

        // POST api/cities
        [HttpPost]
        public ActionResult<CityInfo> Post(CityInfo cityInfo)
        {
            this.context.Cities.Add(cityInfo);
            this.context.SaveChanges();

            return this.CreatedAtAction(nameof(Get), new {id = cityInfo.Name}, cityInfo);
        }

        // PUT api/cities/Sofia
        [HttpPut("{id}")]
        public ActionResult<CityInfo> Put(string id, CityInfo cityInfo)
        {
            var dbId = this.context.Cities.Where(x => x.Name == id).Select(x => x.Id).FirstOrDefault();

            if (dbId == string.Empty)
            {
                return NotFound();
            }

            cityInfo.Id = dbId;
            this.context.Entry(cityInfo).State = EntityState.Modified;
            this.context.SaveChanges();

            return cityInfo;
        }

        // DELETE api/cities/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
