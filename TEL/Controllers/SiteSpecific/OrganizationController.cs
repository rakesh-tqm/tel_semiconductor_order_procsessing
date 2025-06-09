using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.SiteSpecific
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : TelBaseController
    {
        /// <summary>
        /// Get All Organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult OrganizationGetAll()
        {

            var result = OrganizationManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }


        /// <summary>
        /// Get Organizations By Id
        /// </summary>
        /// <returns></returns>

        [HttpGet("get")]
        public IActionResult OrganizationGet(int id)
        {
            var (result, message) = OrganizationManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Edit Orderganization 
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult OrganizationSet(Organization param)
        {
            var (result, error) = OrganizationManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }
        /// <summary>
        /// Delete Organizations By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult OrganizationDelete(int id)
        {
            var (result, error) = OrganizationManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
