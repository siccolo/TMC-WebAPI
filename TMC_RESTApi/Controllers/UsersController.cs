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
    Users Controller 
		    Get all Users 
		    Get an individual user by their identifier, including their subscriptions 
            
            Get all Active Users 
            Get all Suspended Users 
            Get all Disabled Users 

            Add a User 
            Update a User (User.Status cannot be updated via this endpoint) 
            Delete a User 
        Admin Controller 
		    Change Status of User (Active/Suspended/Disabled) 
    */
    [Route("api/[controller]")]
    [ApiController]
    [Authentication.APIKeyAuthorizationAttribute]
    public class UsersController : ControllerBase
    {
        private readonly Services.IUserService _Service;
        private readonly ILogger<UsersController> _Logger;
        public UsersController(Services.IUserService service, ILogger<UsersController> logger)
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

        #region GET filtered data
        [HttpGet]
        [Route("active")]
        public async Task<IActionResult> AllActive()
        {
            try
            {
                Func<Models.User, bool> criteria = u => u.Status.Value==Core.UserStatusEnum.Activated.Value;
                var result = await _Service.FindAsync(criteria).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        
        [HttpGet]
        [Route("suspended")]
        public async Task<IActionResult> AllSuspended()
        {
            try
            {
                Func<Models.User, bool> criteria = u => u.Status.Value == Core.UserStatusEnum.Suspended.Value;
                var result = await _Service.FindAsync(criteria).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        
        [HttpGet]
        [Route("disabled")]
        public async Task<IActionResult> AllDisabled()
        {
            try
            {
                Func<Models.User, bool> criteria = u => u.Status.Value == Core.UserStatusEnum.Disabled.Value;
                var result = await _Service.FindAsync(criteria).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        #endregion

        #region POST add data
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Models.UserDataCreateRequest datarequest)
        {
            try
            {
                if (datarequest == null || !ModelState.IsValid)
                {
                    return BadRequest(Extensions.DataConstants.InvalidEntity);
                }
                Models.User user = datarequest.ToModel();
                var result = await _Service.AddOneAsync(user).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        #endregion

        #region PUT update data
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]Models.UserDataUpdateRequest datarequest)
        {
            try
            {
                if (datarequest == null || !ModelState.IsValid)
                {
                    return BadRequest(Extensions.DataConstants.InvalidEntity);
                }
                //Update a User (User.Status cannot be updated via this endpoint) 
                Models.User givenUser = datarequest.ToModel();
                var result = await _Service.UpdateOneAsync(givenUser).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        #endregion

        #region DELETE delete data
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _Service.DeleteOneAsync(id).ConfigureAwait(false);
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