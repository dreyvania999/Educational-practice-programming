﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UPM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Abonent> Abonent { get; set; }
        public virtual DbSet<BildingTypes> BildingTypes { get; set; }
        public virtual DbSet<Citys> Citys { get; set; }
        public virtual DbSet<ConectService> ConectService { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<EquIpInstall> EquIpInstall { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentInPoint> EquipmentInPoint { get; set; }
        public virtual DbSet<FactAdress> FactAdress { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<IP> IP { get; set; }
        public virtual DbSet<ListPhone> ListPhone { get; set; }
        public virtual DbSet<Mac> Mac { get; set; }
        public virtual DbSet<Moduls> Moduls { get; set; }
        public virtual DbSet<ModulsForRole> ModulsForRole { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<PortType> PortType { get; set; }
        public virtual DbSet<PositionInform> PositionInform { get; set; }
        public virtual DbSet<ProblemType> ProblemType { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<ResonTerminate> ResonTerminate { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<ServiceView> ServiceView { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffInform> StaffInform { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Street> Street { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypeContract> TypeContract { get; set; }
        public virtual DbSet<TypeEquipment> TypeEquipment { get; set; }
        public virtual DbSet<ViewTypeService> ViewTypeService { get; set; }
        public virtual DbSet<MacAndIP> MacAndIP { get; set; }
    }
}
