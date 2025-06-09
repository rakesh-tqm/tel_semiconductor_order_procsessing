namespace TEL.Services
{
    public class MeasurementTypeManage
    {

        /// <summary>
        /// Get Measurement Type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (MeasurementType, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                                   new(){  ParameterName="@Id", Value= id},
                                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.MeasurementType.FromSqlRaw("EXECUTE [MeasurementTypeGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Measurement Types
        /// </summary>
        /// <returns></returns>
        public static List<MeasurementType> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.MeasurementType.FromSqlRaw("EXECUTE [MeasurementTypeGetAll]").ToList();
        }

        /// <summary>
        /// Create Or Edit Measurement Type
        /// </summary>
        /// <param name="_MeasurementType"></param>
        /// <returns></returns>
        public static (bool, string) Set(MeasurementType _MeasurementType)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _MeasurementType.Id },
                                new(){ ParameterName="@Name", Value= _MeasurementType.Name },
                                new(){ ParameterName="@Status", Value= _MeasurementType.Status },
                                new(){ ParameterName="@Comments", Value= _MeasurementType.Comments },
                                new(){ ParameterName="@ImportLogId", Value= _MeasurementType.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _MeasurementType.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _MeasurementType.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _MeasurementType.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _MeasurementType.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [MeasurementTypeSet]
                                @Id,
                                @Name,
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
        /// Delete Measurement By Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [MeasurementTypeDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}