using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YAITS_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static YAITS_.Models.IssuesManager im = new YAITS_.Models.IssuesManager();
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return im.GetAllIssues();
        }

        // GET: api/<ValuesController>/5
        [HttpGet("{shortDescription}")]
        public string Get(string shortDescription)
        {
            return im.GetIssueById(shortDescription);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post(string shortDescription, string detailedDescription, int priority, bool status, string name, string lastName)
        {
            Models.Issues issue = new Models.Issues(shortDescription, detailedDescription, priority, status, name, lastName);
            im.AddIssue(issue);
        }

        // PUT: api/<ValuesController>/5
        [HttpPut]
        public void Update(string shortDescription, string detailedDescription, int priority, bool status, string name, string lastName)
        {
            Models.Issues updatedIssue = new Models.Issues(shortDescription, detailedDescription, priority, status, name, lastName);
            im.UpdateIssue(updatedIssue);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{shortDescription}")]
        public void Delete(string shortDescription)
        {
            im.DeleteIssue(shortDescription);
        }
    }
}
