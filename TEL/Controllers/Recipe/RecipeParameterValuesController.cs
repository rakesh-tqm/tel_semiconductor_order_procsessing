using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.Recipe
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeParameterValuesController : TelBaseController
    {
        /// <summary>
        /// Get All Recipe Parameter Values
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult RecipeParameterValuesGet()
        {

            var result = RecipeParameterValuesManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Get Recipe Parameters values by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult RecipeParameterValuesGet(int id)
        {
            var (result, message) = RecipeParameterValuesManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Create or Manage Recipe Parameter Values 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult RecipeParameterValuesSet(RecipeParameterValues param)
        {
            var (result, error) = RecipeParameterValuesManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Delete Recipe Parameter by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult RecipeParameterValuesDelete(int id)
        {
            var (result, error) = RecipeParameterValuesManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
