using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.Recipe
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeDependentIdController : TelBaseController
    {
        /// <summary>
        /// Get Recipe Dependent Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult RecipeDependentIdGet()
        {

            var result = RecipeDependentIdManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get Recipe Dependent By Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult RecipeDependentIdGet(int id)
        {
            var (result, message) = RecipeDependentIdManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Update Recipe Dependent Id
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult RecipeDependentIdSet(RecipeDependentId param)
        {
            var (result, error) = RecipeDependentIdManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete from Recipe Dependent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult RecipeDependentIdDelete(int id)
        {
            var (result, error) = RecipeDependentIdManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
