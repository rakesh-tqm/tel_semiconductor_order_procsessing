namespace TEL.Services
{
    public class RecipeDependentIdManage
    {

        /// <summary>
        /// Get Recipie Dependent by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (RecipeDependentId, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                               new(){  ParameterName="@Id", Value= id},
                               new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.RecipeDependentIds.FromSqlRaw("EXECUTE [RecipeDependentIdGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Recipie Dependent 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<RecipeDependentId> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.RecipeDependentIds.FromSqlRaw("EXECUTE [RecipeDependentIdGetAll]").ToList();
        }

        /// <summary>
        /// Create or Edit Recipe Dependent Id
        /// </summary>
        /// <param name="_RecipeDependentId"></param>
        /// <returns></returns>
        public static (bool, string) Set(RecipeDependentId _RecipeDependentId)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _RecipeDependentId.Id },
                                new(){ ParameterName="@RecipeId", Value= _RecipeDependentId.RecipeId },
                                new(){ ParameterName="@DependentId", Value= _RecipeDependentId.DependentId },
                                new(){ ParameterName="@Position", Value= _RecipeDependentId.Position },
                                new(){ ParameterName="@Status", Value= _RecipeDependentId.Status },
                                new(){ ParameterName="@CreatedBy", Value= _RecipeDependentId.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _RecipeDependentId.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _RecipeDependentId.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _RecipeDependentId.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [RecipeDependentIdSet] @Id,
                                        @RecipeId,
                                        @DependentId,
                                        @Position,
                                        @Status,
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
        /// Delete Recipe Dependent by Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [RecipeDependentIdDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}