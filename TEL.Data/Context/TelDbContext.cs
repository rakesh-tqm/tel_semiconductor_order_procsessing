using System.ComponentModel.DataAnnotations.Schema;
using TelGws.Data.Models;

namespace TelGws.Data.Database
{
    public class TelDbContext : DbContext
    {

        public TelDbContext(DbContextOptions<TelDbContext> options)
            : base(options)
        {
        }
        [NotMapped]
        public DbSet<BusinessUnit> BusinessUnit { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<MeasurementType> MeasurementType { get; set; }
        public DbSet<MeasurementTypeValue> MeasurementTypeValue { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<ProcessArea> ProcessArea { get; set; }
        public DbSet<ProcessParameter> ProcessParameter { get; set; }
        public DbSet<ProcessParameterValue> ProcessParameterValue { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<RecipeParameters> RecipeParameters { get; set; }
        public DbSet<RecipeParameterValues> RecipeParameterValues { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Model configuration code here
        }
    }
}
