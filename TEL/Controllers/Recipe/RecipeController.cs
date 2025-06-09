using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;

namespace TelGws.API.Controllers.Recipe
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : TelBaseController
    {
        /// <summary>
        /// Recipe Units Get
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult RecipeUnitsGet()
        {
            var result = RecipesManage.GetAll();

            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Recipe Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult RecipeGet(int id)
        {
            var (result, message) = RecipesManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Recipe Set
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult RecipeSet(Recipes param)
        {
            var (result, error) = RecipesManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }

        /// <summary>
        /// Recipe Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult RecipeDelete(int id)
        {
            var (result, error) = RecipesManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, result ?? string.Empty));
        }
    }
}
