namespace TEL.Services
{
    public class ProcessAreaManage
    {

        /// <summary>
        /// Get Process Area by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (ProcessArea, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                                   new(){  ParameterName="@Id", Value= id},
                                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.ProcessArea.FromSqlRaw("EXECUTE [ProcessTypeGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Process Area
        /// </summary>
        /// <returns></returns>
        public static List<ProcessArea> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.ProcessArea.FromSqlRaw("EXECUTE [ProcessTypeGetAll]").ToList();
        }

        /// <summary>
        /// Create Or Edit Process Area
        /// </summary>
        /// <param name="_ProcessType"></param>
        /// <returns></returns>
        public static (bool, string) Set(ProcessArea _ProcessType)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _ProcessType.Id },
                                new(){ ParameterName="@SiteId", Value= _ProcessType.SiteId },
                                new(){ ParameterName="@Type", Value= _ProcessType.Type },
                                new(){ ParameterName="@Cost", Value= _ProcessType.Cost },
                                new(){ ParameterName="@Description", Value= _ProcessType.Description },
                                new(){ ParameterName="@Status", Value= _ProcessType.Status },
                                new(){ ParameterName="@Comments", Value= _ProcessType.Comments },
                                new(){ ParameterName="@ImportLogId", Value= _ProcessType.ImportLogId },
                                new(){ ParameterName="@CreatedBy", Value= _ProcessType.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _ProcessType.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _ProcessType.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _ProcessType.ModifiedOn },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [ProcessTypeSet] @Id,
                                        @SiteId,
                                        @Type,
                                        @Cost,
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
        /// Delete Process Area by Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [ProcessTypeDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}