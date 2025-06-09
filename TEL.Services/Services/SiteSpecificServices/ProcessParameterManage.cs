namespace TEL.Services
{
    public class ProcessParameterManage
    {

        /// <summary>
        /// Get Process Parameter By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (ProcessParameter, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.ProcessParameter.FromSqlRaw("EXECUTE [ProcessParameterGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Process Parameter
        /// </summary>
        /// <returns></returns>
        public static List<ProcessParameter> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.ProcessParameter.FromSqlRaw("EXECUTE [ProcessParameterGetAll]").ToList();
        }

        /// <summary>
        /// Create Or Edit Process Parameter
        /// </summary>
        /// <param name="_ProcessParameter"></param>
        /// <returns></returns>
        public static (bool, string) Set(ProcessParameter _ProcessParameter)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _ProcessParameter.Id },
                                new(){ ParameterName="@ProcessAreaId", Value= _ProcessParameter.ProcessAreaId },
                                new(){ ParameterName="@MeasurementTypeId", Value= _ProcessParameter.MeasurementTypeId },
                                new(){ ParameterName="@ParameterName", Value= _ProcessParameter.ParameterName },
                                new(){ ParameterName="@IncludeInRecipe", Value= _ProcessParameter.IncludeInRecipe },
                                new(){ ParameterName="@Description", Value= _ProcessParameter.Description },
                                new(){ ParameterName="@Status", Value= _ProcessParameter.Status },
                                new(){ ParameterName="@Comments", Value= _ProcessParameter.Comments },
                                new(){ ParameterName="@ImportLogId", Value= _ProcessParameter.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _ProcessParameter.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _ProcessParameter.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _ProcessParameter.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _ProcessParameter.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [ProcessParameterSet] @Id,
                                                            @ProcessAreaId,
                                                            @MeasurementTypeId,
                                                            @ParameterName,
                                                            @IncludeInRecipe,
                                                            @Description,
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
        /// Delete Process Parameter
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [ProcessParameterDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}