using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TelGws.Services.Models;


namespace TelGws.API.Infrastructures
{
    public class TelBaseController : Controller
    {        

        /// <summary>
        /// Gets ajax response with data null
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message)
        {

            return new ApiResponse { Status = status, Message = message, Data = null };
        }

        /// <summary>
        /// Get ajax response with data
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResponse GetAjaxResponse(bool status, string message, object data)
        {
            return new ApiResponse { Status = status, Message = message, Data = data };
        }

        /// <summary>
        /// Internal Server error with ajax response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult InternalServerError(bool status, string message)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new ApiResponse { Status = status, Message = message, Data = null });
        }

        /// <summary>
        /// Bad request with Ajax response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult BadRequest(bool status, string message)
        {
            return BadRequest(new ApiResponse { Status = status, Message = message, Data = null });
        }

        /// <summary>
        /// Ok with Ajax response
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok(bool status, string message)
        {
            return Ok(new ApiResponse { Status = status, Message = message, Data = null });
        }

        /// <summary>
        /// Ok with Ajax response and data
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok(bool status, string message, object data)
        {
            return BadRequest(new ApiResponse { Status = status, Message = message, Data = data });
        }
        
    }
}
