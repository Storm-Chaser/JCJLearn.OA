﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JCJ.OA.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ItcastCmsEntities : DbContext
    {
        public ItcastCmsEntities()
            : base("name=ItcastCmsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<ActionInfo> ActionInfo { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<Articel> Articel { get; set; }
        public virtual DbSet<ArticelClass> ArticelClass { get; set; }
        public virtual DbSet<ArticelComment> ArticelComment { get; set; }
        public virtual DbSet<SensitiveWord> SensitiveWord { get; set; }
        public virtual DbSet<PhotoClass> PhotoClass { get; set; }
        public virtual DbSet<PhotoInfo> PhotoInfo { get; set; }
        public virtual DbSet<VideoClass> VideoClass { get; set; }
        public virtual DbSet<VideoFileInfo> VideoFileInfo { get; set; }
    }
}
