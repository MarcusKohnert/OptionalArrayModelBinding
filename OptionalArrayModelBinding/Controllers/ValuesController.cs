using Microsoft.AspNetCore.Mvc;

namespace OptionalArrayModelBinding.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get(CustomIdentifier[] ids)
        {
            if (this.ModelState.IsValid == false) return this.BadRequest();

            return this.Ok(ids);
        }
    }
}
