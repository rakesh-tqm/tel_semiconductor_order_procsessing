namespace TEL.Services
{
    public class DepartmentManage
    {
        /// <summary>
        /// Get Department by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (Department, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                       new(){  ParameterName="@Id", Value= id},
                       new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.Department.FromSqlRaw("EXECUTE [DepartmentGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <returns></returns>
        public static List<Department> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
                return context.Department.FromSqlRaw("EXECUTE [DepartmentGetAll]").ToList();
        }

        /// <summary>
        /// Create or Edit Department
        /// </summary>
        /// <param name="_Department"></param>
        /// <returns></returns>
        public static (bool, string) Set(Department _Department)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                new(){ ParameterName="@Id", Value= _Department.Id },
                new(){ ParameterName="@Name", Value= _Department.Name },
                new(){ ParameterName="@AbbreviatedName", Value= _Department.AbbreviatedName },
                new(){ ParameterName="@OrganizationId", Value= _Department.OrganizationId },
                new(){ ParameterName="@ApproverEmailId", Value= _Department.ApproverEmailId },
                new(){ ParameterName="@ApproverUserId", Value= _Department.ApproverUserId },
                new(){ ParameterName="@SalesPersonEmail", Value= _Department.SalesPersonEmail },
                new(){ ParameterName="@SalesPersonId", Value= _Department.SalesPersonId },
                new(){ ParameterName="@ShipPersonEmail", Value= _Department.ShipPersonEmail },
                new(){ ParameterName="@PurchasePersonEmail", Value= _Department.PurchasePersonEmail },
                new(){ ParameterName="@PurchasePersonId", Value= _Department.PurchasePersonId },
                new(){ ParameterName="@ShipToAddress", Value= _Department.ShipToAddress },
                new(){ ParameterName="@ShipFromAddress", Value= _Department.ShipFromAddress },
                new(){ ParameterName="@Status", Value= _Department.Status },
                new(){ ParameterName="@LastOrderCount", Value= _Department.LastOrderCount },
                new(){ ParameterName="@SuccessorFlag", Value= _Department.SuccessorFlag },
                new(){ ParameterName="@CountChangeNote", Value= _Department.CountChangeNote },
                new(){ ParameterName="@DeptMergeUnmergeId", Value= _Department.DeptMergeUnmergeId },
                new(){ ParameterName="@UpdateNote", Value= _Department.UpdateNote },
                new(){ ParameterName="@ShowHistory", Value= _Department.ShowHistory },
                new(){ ParameterName="@HistoryNote", Value= _Department.HistoryNote },
                new(){ ParameterName="@ImportLogId", Value= _Department.ImportLogId },
                new(){ ParameterName="@CreatedBy", Value= _Department.CreatedBy },
                new(){ ParameterName="@CreatedOn", Value= _Department.CreatedOn },
                new(){ ParameterName="@ModifiedBy", Value= _Department.ModifiedBy },
                new(){ ParameterName="@ModifiedOn", Value= _Department.ModifiedOn },
                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
};
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [DepartmentSet] @Id,
                        @Name,
                        @AbbreviatedName,
                        @OrganizationId,
                        @ApproverEmailId,
                        @ApproverUserId,
                        @SalesPersonEmail,
                        @SalesPersonId,
                        @ShipPersonEmail,
                        @PurchasePersonEmail,
                        @PurchasePersonId,
                        @ShipToAddress,
                        @ShipFromAddress,
                        @Status,
                        @LastOrderCount,
                        @SuccessorFlag,
                        @CountChangeNote,
                        @DeptMergeUnmergeId,
                        @UpdateNote,
                        @ShowHistory,
                        @HistoryNote,
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
        /// Delete Department
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [DepartmentDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }
}