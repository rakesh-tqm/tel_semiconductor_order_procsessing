namespace TEL.Services
{
    public class ProcessFlowManage
    {

        /// <summary>
        /// Get Process Flow by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (ProcessFlow, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.ProcessFlows.FromSqlRaw("EXECUTE [ProcessFlowGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Process Flow
        /// </summary>
        /// <returns></returns>
        public static List<ProcessFlow> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.ProcessFlows.FromSqlRaw("EXECUTE [ProcessFlowGetAll]").ToList();
        }

        /// <summary>
        /// Create Or Edit Process Flow
        /// </summary>
        /// <param name="_ProcessFlow"></param>
        /// <returns></returns>
        public static (bool, string) Set(ProcessFlow _ProcessFlow)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                            new(){ ParameterName="@Id", Value= _ProcessFlow.Id },
                            new(){ ParameterName="@SiteId", Value= _ProcessFlow.SiteId },
                            new(){ ParameterName="@Name", Value= _ProcessFlow.Name },
                            new(){ ParameterName="@Document", Value= _ProcessFlow.Document },
                            new(){ ParameterName="@Image", Value= _ProcessFlow.Image },
                            new(){ ParameterName="@Info", Value= _ProcessFlow.Info },
                            new(){ ParameterName="@Status", Value= _ProcessFlow.Status },
                            new(){ ParameterName="@CreatedBy", Value= _ProcessFlow.CreatedBy },
                            new(){ ParameterName="@CreatedOn", Value= _ProcessFlow.CreatedOn },
                            new(){ ParameterName="@ModifiedBy", Value= _ProcessFlow.ModifiedBy },
                            new(){ ParameterName="@ModifiedOn", Value= _ProcessFlow.ModifiedOn },
                            new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                            };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [ProcessFlowSet] @Id,
                                        @SiteId,
                                        @Name,
                                        @Document,
                                        @Image,
                                        @Info,
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
        /// Delete Process Flow by Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [ProcessFlowDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}