using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.SiteSpecific
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessAreaController : TelBaseController
    {

        /// <summary>
        /// Get ProcessArea List
        /// </summary>
        /// <returns></returns>

        [HttpGet("getall")]
        public IActionResult ProcessAreaGetAll()
        {

            var result = ProcessAreaManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get ProcessArea by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult ProcessAreaGet(int id)
        {
            var (result, message) = ProcessAreaManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Edit ProcessArea
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult ProcessAreaSet(ProcessArea param)
        {
            var (result, error) = ProcessAreaManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete Process Area By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult ProcessAreaDelete(int id)
        {
            var (result, error) = ProcessAreaManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
