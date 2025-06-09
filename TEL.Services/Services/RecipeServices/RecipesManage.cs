namespace TEL.Services
{
    public class RecipesManage
    {


        /// <summary>
        /// Get Recipe by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (Recipes, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.Recipes.FromSqlRaw("EXECUTE [RecipesGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Recipe 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Recipes> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.Recipes.FromSqlRaw("EXECUTE [RecipesGetAll]").ToList();
        }

        /// <summary>
        /// Create or Edit Recipe 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (bool, string) Set(Recipes _Recipes)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _Recipes.Id },
                                new(){ ParameterName="@VirtualRecipe", Value= _Recipes.VirtualRecipe },
                                new(){ ParameterName="@RecipeName", Value= _Recipes.RecipeName },
                                new(){ ParameterName="@Type", Value= _Recipes.Type },
                                new(){ ParameterName="@PdId", Value= _Recipes.PdId },
                                new(){ ParameterName="@SiteId", Value= _Recipes.SiteId },
                                new(){ ParameterName="@RecipeId", Value= _Recipes.RecipeId },
                                new(){ ParameterName="@Routeid", Value= _Recipes.Routeid },
                                new(){ ParameterName="@VirtualCost", Value= _Recipes.VirtualCost },
                                new(){ ParameterName="@ToolId", Value= _Recipes.ToolId },
                                new(){ ParameterName="@RecipeType", Value= _Recipes.RecipeType },
                                new(){ ParameterName="@RecipeDepdId", Value= _Recipes.RecipeDepdId },
                                new(){ ParameterName="@Description", Value= _Recipes.Description },
                                new(){ ParameterName="@RecipeNote", Value= _Recipes.RecipeNote },
                                new(){ ParameterName="@RecipeProcessAreaId", Value= _Recipes.RecipeProcessAreaId},
                                new(){ ParameterName="@Status", Value= _Recipes.Status },
                                new(){ ParameterName="@IsCompleted", Value= _Recipes.IsCompleted },
                                new(){ ParameterName="@IsMultilayer", Value= _Recipes.IsMultilayer },
                                new(){ ParameterName="@RecipeCount", Value= _Recipes.RecipeCount },
                                new(){ ParameterName="@SpecTempName", Value= _Recipes.SpecTempName },
                                new(){ ParameterName="@OperationNumber", Value= _Recipes.OperationNumber },
                                new(){ ParameterName="@ProcessStep", Value= _Recipes.ProcessStep },
                                new(){ ParameterName="@WorkOrder", Value= _Recipes.WorkOrder },
                                new(){ ParameterName="@Comments", Value= _Recipes.Comments },
                                new(){ ParameterName="@ImportLogId", Value= _Recipes.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _Recipes.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _Recipes.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _Recipes.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _Recipes.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [RecipesSet] @Id,
                                @VirtualRecipe,
                                @RecipeName,
                                @Type,
                                @PdId,
                                @SiteId,
                                @RecipeId,
                                @Routeid,
                                @VirtualCost,
                                @ToolId,
                                @RecipeType,
                                @RecipeDepdId,
                                @Description,
                                @RecipeNote,
                                @RecipeProcessAreaId,
                                @Status,
                                @IsCompleted,
                                @IsMultilayer,
                                @RecipeCount,
                                @SpecTempName,
                                @OperationNumber,
                                @ProcessStep,
                                @WorkOrder,
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
        /// Delete Recipe by Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [RecipesDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}