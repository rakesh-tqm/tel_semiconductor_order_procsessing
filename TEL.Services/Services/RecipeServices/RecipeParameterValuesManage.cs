namespace TEL.Services
{
    public class RecipeParameterValuesManage
    {

        /// <summary>
        /// Get Recipe Recipe Parameter Values by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (RecipeParameterValues, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                                new(){  ParameterName="@Id", Value= id},
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.RecipeParameterValues.FromSqlRaw("EXECUTE [RecipeParameterValuesGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Recipe Recipe Parameter Values
        /// </summary>        
        /// <returns></returns>
        public static List<RecipeParameterValues> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.RecipeParameterValues.FromSqlRaw("EXECUTE [RecipeParameterValuesGetAll]").ToList();
        }

        /// <summary>
        /// Create or Edit Process Parameters Values
        /// </summary>
        /// <param name="_RecipeParameterValues"></param>
        /// <returns></returns>
        public static (bool, string) Set(RecipeParameterValues _RecipeParameterValues)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _RecipeParameterValues.Id },
                                new(){ ParameterName="@RecipeId", Value= _RecipeParameterValues.RecipeId },
                                new(){ ParameterName="@ProcessTypeId", Value= _RecipeParameterValues.ProcessAreaId },
                                new(){ ParameterName="@ProcessParameterId", Value= _RecipeParameterValues.ProcessParameterId },
                                new(){ ParameterName="@ParameterValueId", Value= _RecipeParameterValues.ProcessParameterValueId},
                                new(){ ParameterName="@Status", Value= _RecipeParameterValues.Status },
                                new(){ ParameterName="@Comments", Value= _RecipeParameterValues.Comments },
                                new(){ ParameterName="@ImportLogId", Value= _RecipeParameterValues.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _RecipeParameterValues.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _RecipeParameterValues.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _RecipeParameterValues.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _RecipeParameterValues.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [RecipeParameterValuesSet] @Id,
                                @RecipeId,
                                @ProcessTypeId,
                                @ProcessParameterId,
                                @ParameterValueId,
                                @Status,
                                @Comments,
                                @ImportLogId,
                                @CreatedBy,
                                @CreatedOn,
                                @ModifiedBy,
                                @ModifiedOn,
                                 @ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (result > 0, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Delete Recipe Recipe Parameter Values by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (string, string) Delete(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= id },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw("EXECUTE [RecipeParameterValuesDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}