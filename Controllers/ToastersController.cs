using ToasterApi.Models;
using ToasterApi.Services;
using ToasterApi.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ToasterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToastersController : ControllerBase
    {
        private readonly ToasterService _toasterService;

        public ToastersController(ToasterService toasterService)
        {
            _toasterService = toasterService;
        }

        // Get All Route, endpoint is api/toasters

        [HttpGet]
        public ActionResult<List<Toaster>> Get()
        {

            _toasterService.Get();
            var toaster = _toasterService.Get();

            EventHandler.HandleFrontEndRequest("kazoo");

            return toaster;
        }

        // Get One Route, endpoint is api/toasters/{id}

        [HttpGet("{id:length(24)}", Name = "GetToaster")]
        public ActionResult<Toaster> Get(string id)
        {
            var toaster = _toasterService.Get(id);

            if (toaster == null)
            {
                return NotFound();
            }

            return toaster;
        }

        // Post Route, endpoint is api/toasters/new

        [HttpPost("new")]
        public ActionResult<Toaster> Create(Toaster toaster)
        {
            _toasterService.Create(toaster);

            return CreatedAtRoute("GetToaster", new { id = toaster.Id.ToString() }, toaster);
        }

        // Update Route, endpoint is api/toasters/{id}

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Toaster toasterIn)
        {
            var toaster = _toasterService.Get(id);

            if (toaster == null)
            {
                return NotFound();
            }

            var oldToaster = _toasterService.Get(id);
            
            _toasterService.Update(id, toasterIn);

            // NOTE: it is logically possible for the toaster state to be updated in the DB
            // and still have the logging fail. This is something we can handle in the next iteration.
            EventHandler.HandleToasterStateChange(oldToaster, toasterIn);
            return NoContent();
        }

        // Delete Route, endpoint is api/toasters/{id}

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var toaster = _toasterService.Get(id);

            if (toaster == null)
            {
                return NotFound();
            }

            _toasterService.Remove(toaster.Id);

            return NoContent();
        }
    }
}