namespace TEL.Services
{
    public class ProcessFlowDetailManage
    {

        /// <summary>
        /// Get Process Flow Details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (ProcessFlowDetail, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.ProcessFlowDetails.FromSqlRaw("EXECUTE [ProcessFlowDetailGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All ProcessFlow Details
        /// </summary>
        /// <returns></returns>
        public static List<ProcessFlowDetail> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.ProcessFlowDetails.FromSqlRaw("EXECUTE [ProcessFlowDetailGetAll]").ToList();
        }

        /// <summary>
        /// Create Or Edit Process FLow Details
        /// </summary>
        /// <param name="_ProcessFlowDetail"></param>
        /// <returns></returns>
        public static (bool, string) Set(ProcessFlowDetail _ProcessFlowDetail)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _ProcessFlowDetail.Id },
                                new(){ ParameterName="@ProcessFlowId", Value= _ProcessFlowDetail.ProcessFlowId },
                                new(){ ParameterName="@recipeId", Value= _ProcessFlowDetail.recipeId },
                                new(){ ParameterName="@toolId", Value= _ProcessFlowDetail.toolId },
                                new(){ ParameterName="@toolCost", Value= _ProcessFlowDetail.toolCost },
                                new(){ ParameterName="@CreatedBy", Value= _ProcessFlowDetail.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _ProcessFlowDetail.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _ProcessFlowDetail.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _ProcessFlowDetail.ModifiedOn },
                                new(){ ParameterName="@Status", Value= _ProcessFlowDetail.Status },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [ProcessFlowDetailSet] @Id,
                                        @ProcessFlowId,
                                        @recipeId,
                                        @toolId,
                                        @toolCost,
                                        @CreatedBy,
                                        @CreatedOn,
                                        @ModifiedBy,
                                        @ModifiedOn,
                                        @Status,
                                         @ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (result > 0, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Delete Process Flow Details by Id 
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [ProcessFlowDetailDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}