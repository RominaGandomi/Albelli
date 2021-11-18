using Albelli.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Albelli.Data
{
    public class AlbelliDbContext :DbContext
    {

        private static IHttpContextAccessor _httpContextAccessor;
        public static HttpContext CurrentHttpContext => _httpContextAccessor.HttpContext;

        public AlbelliDbContext(DbContextOptions<AlbelliDbContext> options) : base(options) { }

        public AlbelliDbContext(DbContextOptions<AlbelliDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options) { _httpContextAccessor = httpContextAccessor; }

        #region Configure Db

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        
        #endregion   

        #region Configure FluentApi

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order>(ConfigureOrder);
            builder.Entity<OrderItem>(ConfigureOrderItem);
        }
        
        public void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.Items);
        }
        public void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(x => x.Order);
            builder.HasOne(x => x.ProductType);
        }
      
        #endregion

        public virtual void Save()
        {
            base.SaveChanges();
        }
    }
}
