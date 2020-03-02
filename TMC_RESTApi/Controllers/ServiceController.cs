using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Controllers
{
    /*
    Services Controller 
		Lists an arbitrary list of services (id, name, price) 
    */
    [Route("api/[controller]")]
    [ApiController]
    [Authentication.APIKeyAuthorizationAttribute]
    public class ServiceController : ControllerBase
    {
        private readonly Services.IProductService _Service;
        private readonly ILogger<ServiceController> _Logger;
        
        public ServiceController(Services.IProductService service,  ILogger<ServiceController> logger)
        {
            _Service = service ?? throw new System.ArgumentNullException("service");
            _Logger = logger;
        }

        #region GET data
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var result = await _Service.SelectAsync().ConfigureAwait(false);
                return Ok(result);
            }
            catch(Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> One(int id)
        {
            try
            {
                var result = await _Service.SelectOneAsync(id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        #endregion

    }
}