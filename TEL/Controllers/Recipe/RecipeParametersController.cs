using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.Recipe
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeParametersController : TelBaseController
    {
        /// <summary>
        /// Get All Recipe Parameters
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult RecipeParametersGet()
        {

            var result = RecipeParametersManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get All Recipe Parameters by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("get")]
        public IActionResult RecipeParametersGet(int id)
        {
            var (result, message) = RecipeParametersManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Edit Recipe Parameters
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult RecipeParametersSet(RecipeParameters param)
        {
            var (result, error) = RecipeParametersManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete Recipe Paramter by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("delete")]
        public IActionResult RecipeParametersDelete(int id)
        {
            var (result, error) = RecipeParametersManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
