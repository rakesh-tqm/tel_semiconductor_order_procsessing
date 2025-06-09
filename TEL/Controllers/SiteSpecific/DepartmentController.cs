using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.SiteSpecific
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : TelBaseController
    {
        /// <summary>
        /// Get All Department List
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult DepartmentGetAll()
        {

            var result = DepartmentManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get All Department by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult DepartmentGet(int id)
        {
            var (result, message) = DepartmentManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Update Department
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult DepartmentSet(Department param)
        {
            var (result, error) = DepartmentManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete Department By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult DepartmentDelete(int id)
        {
            var (result, error) = DepartmentManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
