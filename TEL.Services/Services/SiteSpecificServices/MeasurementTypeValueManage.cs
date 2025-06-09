namespace TEL.Services
{
    public class MeasurementTypeValueManage
    {

        /// <summary>
        /// Get Measurement Type Value by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (MeasurementTypeValue, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                            };
                var data = context.MeasurementTypeValue.FromSqlRaw("EXECUTE [MeasurementTypeValueGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Measurement Values
        /// </summary>
        /// <returns></returns>
        public static List<MeasurementTypeValue> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.MeasurementTypeValue.FromSqlRaw("EXECUTE [MeasurementTypeValueGetAll]").ToList();
        }

        /// <summary>
        /// Create or Edit Measurement Type Value
        /// </summary>
        /// <param name="_MeasurementTypeValue"></param>
        /// <returns></returns>
        public static (bool, string) Set(MeasurementTypeValue _MeasurementTypeValue)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _MeasurementTypeValue.Id },
                                new(){ ParameterName="@MeasurementTypeId", Value= _MeasurementTypeValue.MeasurementTypeId },
                                new(){ ParameterName="@MeasurementUnit", Value= _MeasurementTypeValue.MeasurementUnit },
                                new(){ ParameterName="@Status", Value= _MeasurementTypeValue.Status },
                                new(){ ParameterName="@Comments", Value= _MeasurementTypeValue.Comments },
                                new(){ ParameterName="@ImportLogId", Value= _MeasurementTypeValue.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _MeasurementTypeValue.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _MeasurementTypeValue.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _MeasurementTypeValue.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _MeasurementTypeValue.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [MeasurementTypeValueSet] @Id,
                                    @MeasurementTypeId,
                                    @MeasurementUnit,
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
        /// Delete Measurement Type Value by Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [MeasurementTypeValueDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}