using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dal
{
    public class ExcersiceDBContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("server=.;initial catalog=excer01;integrated security=true");                 
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries();
            foreach (var item in entities)
            {                
                if(item.State == EntityState.Added || item.State== EntityState.Modified)
                {
                    var stringFields = item.Properties.Where(a => a.Metadata.ClrType == typeof(string));
                    foreach (var itemProb in stringFields)
                    {                        
                        itemProb.CurrentValue = itemProb.CurrentValue.ToString().Replace('ح', 'ت').Replace('ن', 'ل').Replace('ف', 'ت').Replace('ر', 'ل');                        
                    }
                }
            }
            return base.SaveChanges();
        }
       
    }

    public static class ExtMethod
    {
        public static void RemoveAll<T>(this DbSet<T> entity) where T:class
        {
            ExcersiceDBContext _Context = new ExcersiceDBContext();
            _Context.RemoveRange(entity);            
            _Context.SaveChanges();
            
        }
    }
}
