using Microsoft.AspNetCore.Mvc;
using TelGws.API.Infrastructures;
namespace TelGws.API.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingLanguageController : TelBaseController
    {
        /// <summary>
        /// Get All Setting Language
        /// </summary>
        /// <returns></returns>

        [HttpGet("getall")]
        public IActionResult SettingLanguageGet()
        {

            var result = SettingLanguageManage.GetAll();
            if (result == null)
                return Json(GetAjaxResponse(false, "No record found."));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }
        /// <summary>
        /// Get All Setting Language By Id
        /// </summary>
        /// <returns></returns>
        [HttpPost("getbyvariable")]
        public IActionResult SettingLanguageGetByVariable([FromBody] string[] variables)
        {
            var (result, message) = SettingLanguageManage.Get(variables);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));
            else if (result.Count > 0)
                return Json(GetAjaxResponse(true, string.Empty, result));

            return Json(GetAjaxResponse(true, "No data found", result));
        }
        /// <summary>
        /// Download Setting Language
        /// </summary>
        /// <returns></returns>
        [HttpGet("download")]
        public FileResult SettingLanguageDownload()
        {
            var (folderName, fileName) = SettingLanguageManage.Download();

            string file = Path.Combine(folderName, fileName);

            // DeleteFileStream will override dispose and delete file when filestream dispose
            return File(new DeleteFileStream(file, FileMode.Open), "application/octet-stream", fileName);
        }

        /// <summary>
        /// Import Setting Language
        /// </summary>
        /// <returns></returns>
        [HttpPost("importFile")]
        public string SaveFile(IFormFile file)
        {
            var folderPath = AppSettingsHepler.GetFilePath(AppSettingConfig.GetValue("FileImportPath"));
            if (file.Length == 0)
            {
                throw new BadHttpRequestException("File is empty.");
            }

            var extension = Path.GetExtension(file.FileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            var fileName = $"{Guid.NewGuid()}.{extension}";
            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);
            stream.Dispose();

            var (result, error) = SettingLanguageManage.ImportFile(fileName);

            return filePath;
        }


        /// <summary>
        /// Import Setting Language File
        /// </summary>
        /// <returns></returns>
        [HttpPost("import")]
        public IActionResult SettingLanguageImportFile([FromBody] string fileName)
        {
            var (result, error) = SettingLanguageManage.ImportFile(fileName);

            if (!result)
                return Json(GetAjaxResponse(true, error));

            return Json(GetAjaxResponse(true, string.Empty));

        }


        /// <summary>
        /// Get Setting Language by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult SettingLanguageGet(int id)
        {
            var (result, message) = SettingLanguageManage.Get(id);

            if (!string.IsNullOrEmpty(message))
                return Json(GetAjaxResponse(false, message));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }


        /// <summary>
        /// Create or edit Setting Language
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public IActionResult SettingLanguageSet(SettingLanguage param)
        {
            var (result, error) = SettingLanguageManage.Set(param);
            if (!string.IsNullOrEmpty(error))
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty, result));
        }


        /// <summary>
        /// Delete Setting Language
        /// </summary>
        /// <returns></returns>

        [HttpDelete("delete")]
        public IActionResult RecipeDelete(int id)
        {
            var (result, error) = SettingLanguageManage.Delete(id);
            if (error != null)
                return Json(GetAjaxResponse(false, error));

            return Json(GetAjaxResponse(true, string.Empty));
        }

    }
}
