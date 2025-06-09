using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.SiteSpecific
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUnitController : TelBaseController
    {
        /// <summary>
        /// Get All Business Units
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult BusinessUnitsGetAll()
        {

            var result = BusinessUnitManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get All Business Units by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult BusinessUnitsGet(int id)
        {
            var (result, message) = BusinessUnitManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Edit Business Units
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult BusinessUnitsSet(BusinessUnit param)
        {
            var (result, error) = BusinessUnitManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }



        /// <summary>
        /// Delete Business Units By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult BusinessUnitsDelete(int id)
        {
            var (result, error) = BusinessUnitManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}

