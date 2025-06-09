using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.SiteSpecific
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementTypeController : TelBaseController
    {

        /// <summary>
        /// Get All Measurement Types
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult MeasurementTypeGetAll()
        {

            var result = MeasurementTypeManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get All Measurement Types by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult MeasurementTypeGet(int id)
        {
            var (result, message) = MeasurementTypeManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create Or Edit Measurement Types
        /// </summary>
        /// <returns></returns>

        [HttpPost("set")]
        public IActionResult MeasurementTypeSet(MeasurementType param)
        {
            var (result, error) = MeasurementTypeManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete  Measurement Types By Id
        /// </summary>
        /// <returns></returns>

        [HttpDelete("delete")]
        public IActionResult MeasurementTypeDelete(int id)
        {
            var (result, error) = MeasurementTypeManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
