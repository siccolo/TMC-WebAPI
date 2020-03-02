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
    Subscription Controller 
		User Subscribe to a Service 
		User Unsubscribe from a Service 
		Get Active Subscriptions for user
    */
    [Route("api/[controller]")]
    [ApiController]
    [Authentication.APIKeyAuthorizationAttribute]
    public class SubscriptionController : ControllerBase
    {
        private readonly Services.IUserSubscriptionService _Service;
        private readonly ILogger<SubscriptionController> _Logger;
        
        public SubscriptionController(Services.IUserSubscriptionService service,  ILogger<SubscriptionController> logger)
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
        [Route("{key}")]
        public async Task<IActionResult> One(string key)
        {
            try
            {
                var result = await _Service.SelectOneAsync(key).ConfigureAwait(false);
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
        [Route("active/{userId:int}")]
        public async Task<IActionResult> AllActiveUserSubscriptions(int userId)
        {
            try
            {
                Func<Models.UserSubscription, bool> criteria = s => s.UserId == userId && s.Status.Value == Core.SubscriptionEnum.Activated.Value;
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

        #region - Subscribe
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Models.UserSubscriptionDataRequest datarequest)
        {
            try
            {
                if (datarequest == null || !ModelState.IsValid)
                {
                    return BadRequest(Extensions.DataConstants.InvalidEntity);
                }
                var userSubscription = datarequest.ToModel();
                var result = await _Service.AddOneAsync(userSubscription).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var r = new Result.Result<Boolean>(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, r);
            }
        }
        #endregion

        #region - Unsubscribe
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]Models.UserSubscriptionDataRequest datarequest)
        {
            try
            {
                if (datarequest == null || !ModelState.IsValid)
                {
                    return BadRequest(Extensions.DataConstants.InvalidEntity);
                }
                var userSubscription = datarequest.ToModel();
                var result = await _Service.DeleteOneAsync(userSubscription.Key).ConfigureAwait(false);
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