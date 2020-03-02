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
        Admin Controller 
		    Change Status of User (Active/Suspended/Disabled) 
    */
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly Services.IUserService _Service;
        private readonly ILogger<AdminController> _Logger;
        
        public AdminController(Services.IUserService service,  ILogger<AdminController> logger)
        {
            _Service = service ?? throw new System.ArgumentNullException("service");
            _Logger = logger;
        }

        
        #region update data
        [Authentication.APIKeyAuthorizationAttribute]
        [HttpPost("changestatus/{id:int}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody]string status)
        {
            try
            {
                if (String.IsNullOrEmpty(status) || Core.UserStatusEnum.FromString(status)== Core.UserStatusEnum.Unknown)
                {
                    return BadRequest(Extensions.DataConstants.InvalidEntity);
                }
                var result = await _Service.UpdateOneStatusAsync(id,status).ConfigureAwait(false);
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