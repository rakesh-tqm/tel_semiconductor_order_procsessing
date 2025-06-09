using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.SiteSpecific
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementTypeValueController : TelBaseController
    {
        /// <summary>
        /// Get All Measurement Types Values
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult MeasurementTypeValueGetAll()
        {

            var result = MeasurementTypeValueManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get All Measurement Types Values by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult MeasurementTypeValueGet(int id)
        {
            var (result, message) = MeasurementTypeValueManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Edit Measurement Types Values
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult MeasurementTypeValueSet(MeasurementTypeValue param)
        {
            var (result, error) = MeasurementTypeValueManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete Measurement Types Values by Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult MeasurementTypeValueDelete(int id)
        {
            var (result, error) = MeasurementTypeValueManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
