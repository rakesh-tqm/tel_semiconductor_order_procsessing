using System.Data;
using TEL.Services.Utility;
namespace TEL.Services
{

    public class BusinessUnitManage
    {
        /// <summary>
        /// Get Business Unit by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static (BusinessUnit, string) Get(int id)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]
                {
                   new(){  ParameterName="@Id", Value= id},
                   new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                };
                var data = context.BusinessUnit.FromSqlRaw("EXECUTE [BusinessUnitGet] @Id, @ErrorMessage OUTPUT", parameters).ToList().FirstOrDefault();
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (data, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Get All Business units
        /// </summary>
        /// <returns></returns>
        public static List<BusinessUnit> GetAll()
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var test = context.BusinessUnit.FromSqlRaw("EXECUTE [BusinessUnitGetAll]").ToList();

                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                DataTable dt = converter.ToDataTable(test);
                var dataTable = new DataTable();
                dataTable.TableName = "DC";
                DataTable dtr = converter.ToDataTable(test);
                dtr.ExportToExcel(@"d:\temp\buexported.xls", true);
                return new List<BusinessUnit>();
            }
        }

        /// <summary>
        /// Create or edit Business Unit
        /// </summary>
        /// <param name="_BusinessUnit"></param>
        /// <returns></returns>
        public static (bool, string) Set(BusinessUnit _BusinessUnit)
        {
            using (var context = SiteHelper.CreateDbContext())
            {
                var parameters = new SqlParameter[]{
                                new(){ ParameterName="@Id", Value= _BusinessUnit.Id },
                                new(){ ParameterName="@Name", Value= _BusinessUnit.Name },
                                new(){ ParameterName="@OrganizationId", Value= _BusinessUnit.OrganizationId },
                                new(){ ParameterName="@Status", Value= _BusinessUnit.Status },
                                new(){ ParameterName="@CreatedBy", Value= _BusinessUnit.CreatedBy },
                                new(){ ParameterName="@CreatedOn", Value= _BusinessUnit.CreatedOn },
                                new(){ ParameterName="@ModifiedBy", Value= _BusinessUnit.ModifiedBy },
                                new(){ ParameterName="@ModifiedOn", Value= _BusinessUnit.ModifiedOn },
                                new(){ ParameterName="@ImportLogId", Value= _BusinessUnit.ImportLogId },
                                new(){ ParameterName="@Comments", Value= _BusinessUnit.Comments },
                                new(){ ParameterName="@ErrorMessage",Direction=System.Data.ParameterDirection.Output,Size=-1,SqlDbType=System.Data.SqlDbType.VarChar}
                                };
                var result = context.Database.ExecuteSqlRaw(@"EXECUTE [BusinessUnitSet] @Id,
                                @Name,
                                @OrganizationId,
                                @Status,
                                @CreatedBy,
                                @CreatedOn,
                                @ModifiedBy,
                                @ModifiedOn,
                                @ImportLogId,
                                @Comments,
                                 @ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                var errorMessage = message.Value;
                return (result > 0, Convert.ToString(errorMessage));
            }
        }

        /// <summary>
        /// Delete Business Unit By Id
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
                var result = context.Database.ExecuteSqlRaw("EXECUTE [BusinessUnitDelete] @Id,@ErrorMessage OUTPUT", parameters);
                var message = parameters.First(param => param.ParameterName == "@ErrorMessage");
                if (Convert.ToString(message.Value) != string.Empty)
                    return (string.Empty, (string)message.Value);
                else
                    return (string.Empty, null);
            }
        }
    }

}