﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompressPicture
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DataModelEntities : DbContext
    {
        public DataModelEntities()
            : base("name=DataModelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Data_TempCopy> Data_TempCopy { get; set; }
        public virtual DbSet<Web_ItemLibrary> Web_ItemLibrary { get; set; }
        public virtual DbSet<Web_UserBuildingCircle> Web_UserBuildingCircle { get; set; }
    }
}
