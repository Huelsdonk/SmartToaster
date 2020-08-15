using ToasterApi.Models;
using ToasterApi.Services;
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

        [HttpGet]
        public ActionResult<List<Toaster>> Get() =>
            _toasterService.Get();

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

        [HttpPost]
        public ActionResult<Toaster> Create(Toaster toaster)
        {
            _toasterService.Create(toaster);

            return CreatedAtRoute("GetToaster", new { id = toaster.Id.ToString() }, toaster);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Toaster toasterIn)
        {
            var toaster = _toasterService.Get(id);

            if (toaster == null)
            {
                return NotFound();
            }

            _toasterService.Update(id, toasterIn);

            return NoContent();
        }

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