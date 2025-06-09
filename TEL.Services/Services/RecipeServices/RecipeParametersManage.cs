namespace TEL.Services
{
    public class RecipeParametersManage
    {
        /// <summary>
        /// Get Recipe Parameters by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (RecipeParameters, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.RecipeParameters.FromSqlRaw("EXECUTE [RecipeParametersGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }
        /// <summary>
        /// Get All Recipe Parameters 
        /// </summary>        
        /// <returns></returns>
        public static List<RecipeParameters> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.RecipeParameters.FromSqlRaw("EXECUTE [RecipeParametersGetAll]").ToList();
        }

        /// <summary>
        /// Create Or Update Recipe Parameters 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (bool, string) Set(RecipeParameters _RecipeParameters)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _RecipeParameters.Id },
                                new(){ ParameterName="@RecipeId", Value= _RecipeParameters.RecipeId },
                                new(){ ParameterName="@ProcessTypeId", Value= _RecipeParameters.ProcessAreaId },
                                new(){ ParameterName="@ProcessParameterId", Value= _RecipeParameters.ProcessParameterId },
                                new(){ ParameterName="@Status", Value= _RecipeParameters.Status },
                                new(){ ParameterName="@ImportLogId", Value= _RecipeParameters.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _RecipeParameters.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _RecipeParameters.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _RecipeParameters.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _RecipeParameters.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [RecipeParametersSet] @Id,
                                    @RecipeId,
                                    @ProcessTypeId,
                                    @ProcessParameterId,
                                    @Status,
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
        /// Delete Recipe Parameters by Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [RecipeParametersDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}