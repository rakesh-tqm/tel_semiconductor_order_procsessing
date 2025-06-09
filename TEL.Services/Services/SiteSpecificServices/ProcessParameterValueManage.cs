namespace TEL.Services
{
    public class ProcessParameterValueManage
    {

        /// <summary>
        /// Get Process Parameter Values
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (ProcessParameterValue, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                       new(){  ParameterName="@Id", Value= id},
                       new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.ProcessParameterValue.FromSqlRaw("EXECUTE [ProcessParameterValueGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Process Parameter Values
        /// </summary>
        /// <returns></returns>
        public static List<ProcessParameterValue> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.ProcessParameterValue.FromSqlRaw("EXECUTE [ProcessParameterValueGetAll]").ToList();
        }

        /// <summary>
        /// Create or Edit Process Parameter Values
        /// </summary>
        /// <param name="_ProcessParameterValue"></param>
        /// <returns></returns>
        public static (bool, string) Set(ProcessParameterValue _ProcessParameterValue)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                    new(){ ParameterName="@Id", Value= _ProcessParameterValue.Id },
                                    new(){ ParameterName="@ProcessParameterId", Value= _ProcessParameterValue.ProcessParameterId },
                                    new(){ ParameterName="@ParameterValue", Value= _ProcessParameterValue.ParameterValue },
                                    new(){ ParameterName="@MeasurementUnit", Value= _ProcessParameterValue.MeasurementUnit },
                                    new(){ ParameterName="@CommonMeasurement", Value= _ProcessParameterValue.CommonMeasurement },
                                    new(){ ParameterName="@Description", Value= _ProcessParameterValue.Description },
                                    new(){ ParameterName="@Status", Value= _ProcessParameterValue.Status },
                                    new(){ ParameterName="@Comments", Value= _ProcessParameterValue.Comments },
                                    new(){ ParameterName="@ImportLogId", Value= _ProcessParameterValue.ImportLogId },
                                    new(){ ParameterName="@CreatedBy", Value= _ProcessParameterValue.CreatedBy },
                                    new(){ ParameterName="@CreatedOn", Value= _ProcessParameterValue.CreatedOn },
                                    new(){ ParameterName="@ModifiedBy", Value= _ProcessParameterValue.ModifiedBy },
                                    new(){ ParameterName="@ModifiedOn", Value= _ProcessParameterValue.ModifiedOn },
                                    new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                    };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [ProcessParameterValueSet] @Id,
                                            @ProcessParameterId,
                                            @ParameterValue,
                                            @MeasurementUnit,
                                            @CommonMeasurement,
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
        /// Delete Process Parameter By Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [ProcessParameterValueDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }

}