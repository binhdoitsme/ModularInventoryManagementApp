using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using ModularInventoryManagement.Data.Configuration;
using ModularInventoryManagement.Data.Models;

namespace ModularInventoryManagement.Data.Database
{
    public partial class InventoryManagementContext : DbContext
    {
        private static readonly string ConnectionString;
        private static readonly InventoryManagementContext Instance;

        static InventoryManagementContext()
        {
            ConnectionString = Configurator.GetConfiguration()
                                    .GetConnectionString("InventoryManagement.Development");
            Instance = new InventoryManagementContext();
        }

        public static InventoryManagementContext GetInstance()
        {
            return Instance;
        }

        public InventoryManagementContext()
        {
        }

        public InventoryManagementContext(DbContextOptions<InventoryManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Batch> Batch { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeContact> EmployeeContact { get; set; }
        public virtual DbSet<EmployeeJobTitle> EmployeeJobTitle { get; set; }
        public virtual DbSet<JobTitle> JobTitle { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<OrderLine> Orderline { get; set; }
        public virtual DbSet<PaymentByCard> PaymentByCard { get; set; }
        public virtual DbSet<PaymentInCash> PaymentInCash { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductVariant> ProductVariant { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SupplierContract> SupplierContract { get; set; }
        public virtual DbSet<SupplierShipment> SupplierShipment { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Workshift> Workshift { get; set; }
        public virtual DbSet<WorkshiftAssignment> WorkshiftAssignment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(ConnectionString)
                                .EnableDetailedErrors()
                                .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity => {
                entity.HasKey(e => new { e.EmployeeId, e.WorkshiftAssignmentId })
                    .HasName("PRIMARY");

                entity.ToTable("attendance");

                entity.HasIndex(e => e.WorkshiftAssignmentId)
                    .HasName("FKAttendance97219");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WorkshiftAssignmentId)
                    .HasColumnName("workshift_assignment_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CheckTime).HasColumnName("check_time");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKAttendance96294");

                entity.HasOne(d => d.WorkshiftAssignment)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.WorkshiftAssignmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKAttendance97219");
            });

            modelBuilder.Entity<Batch>(entity => {
                entity.ToTable("batch");

                entity.HasIndex(e => e.ProductVariantId)
                    .HasName("FKBatch488689");

                entity.HasIndex(e => e.ShipmentId)
                    .HasName("FKBatch208364");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImportPrice)
                    .HasColumnName("import_price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductVariantId)
                    .HasColumnName("product_variant_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShipmentId)
                    .HasColumnName("shipment_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKBatch488689");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Batch)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKBatch208364");
            });

            modelBuilder.Entity<Category>(entity => {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(127)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity => {
                entity.ToTable("employee");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeContact>(entity => {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PRIMARY");

                entity.ToTable("employee_contact");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.EmployeeContact)
                    .HasForeignKey<EmployeeContact>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKEmployee_C604284");
            });

            modelBuilder.Entity<EmployeeJobTitle>(entity => {
                entity.HasKey(e => new { e.EmployeeId, e.JobTitleId })
                    .HasName("PRIMARY");

                entity.ToTable("employee_job_title");

                entity.HasIndex(e => e.JobTitleId)
                    .HasName("FKEmployee_J111215");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.JobTitleId)
                    .HasColumnName("job_title_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeJobTitle)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKEmployee_J143782");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.EmployeeJobTitle)
                    .HasForeignKey(d => d.JobTitleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKEmployee_J111215");
            });

            modelBuilder.Entity<JobTitle>(entity => {
                entity.ToTable("job_title");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LatePenaltyPerMin)
                    .HasColumnName("late_penalty_per_min")
                    .HasColumnType("decimal(19,0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(96)
                    .IsUnicode(false);

                entity.Property(e => e.SalaryPerHour)
                    .HasColumnName("salary_per_hour")
                    .HasColumnType("decimal(19,0)");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.ToTable("order");

                entity.HasIndex(e => e.CashierId)
                    .HasName("FKOrder635284");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CashierId)
                    .HasColumnName("cashier_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(19,0)");

                entity.HasOne(d => d.Cashier)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CashierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrder635284");
            });

            modelBuilder.Entity<OrderPayment>(entity => {
                entity.HasKey(e => new { e.OrderId, e.PaymentMethodId })
                    .HasName("PRIMARY");

                entity.ToTable("order_payment");

                entity.HasIndex(e => e.PaymentMethodId)
                    .HasName("FKOrder_Paym585467");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaymentMethodId)
                    .HasColumnName("payment_method_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPayment)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrder_Paym366149");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.OrderPayment)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrder_Paym585467");
            });

            modelBuilder.Entity<OrderLine>(entity => {
                entity.HasKey(e => new { e.OrderId, e.BatchId })
                    .HasName("PRIMARY");

                entity.ToTable("order_line");

                entity.HasIndex(e => e.BatchId)
                    .HasName("FKOrderLine22235");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BatchId)
                    .HasColumnName("batch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LineSum)
                    .HasColumnName("line_sum")
                    .HasColumnType("decimal");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Orderline)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrderLine22235");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderline)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKOrderLine56869");
            });

            modelBuilder.Entity<PaymentByCard>(entity => {
                entity.ToTable("payment_by_card");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceNo)
                    .HasColumnName("reference_no")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PaymentByCard)
                    .HasForeignKey<PaymentByCard>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKPayment_By479349");
            });

            modelBuilder.Entity<PaymentInCash>(entity => {
                entity.ToTable("payment_in_cash");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaidAmount)
                    .HasColumnName("paid_amount")
                    .HasColumnType("decimal");

                entity.Property(e => e.Returned)
                    .HasColumnName("returned")
                    .HasColumnType("decimal");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PaymentInCash)
                    .HasForeignKey<PaymentInCash>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKPayment_In160098");
            });

            modelBuilder.Entity<PaymentMethod>(entity => {
                entity.ToTable("payment_method");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Permission>(entity => {
                entity.ToTable("permission");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasColumnName("module")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.View)
                    .IsRequired()
                    .HasColumnName("view")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity => {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("FKProduct850050");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasColumnName("unit")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKProduct850050");
            });

            modelBuilder.Entity<ProductVariant>(entity => {
                entity.ToTable("product_variant");

                entity.HasIndex(e => e.ProductId)
                    .HasName("FKProduct_Va954638");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("FKProduct_Va683858");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ListPrice)
                    .HasColumnName("list_price")
                    .HasColumnType("decimal(19,0)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVariant)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKProduct_Va954638");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductVariant)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKProduct_Va683858");
            });

            modelBuilder.Entity<Role>(entity => {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolePermission>(entity => {
                entity.HasKey(e => new { e.RoleId, e.PermissionId })
                    .HasName("PRIMARY");

                entity.ToTable("role_permission");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("FKRole_Permi855251");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKRole_Permi855251");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKRole_Permi314838");
            });

            modelBuilder.Entity<Supplier>(entity => {
                entity.ToTable("supplier");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasColumnName("website")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupplierContract>(entity => {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PRIMARY");

                entity.ToTable("supplier_contract");

                entity.HasIndex(e => e.ContractMaker)
                    .HasName("FKSupplier_C539802");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContractMaker)
                    .HasColumnName("contract_maker")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EarlyEnded)
                    .HasColumnName("early_ended")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.ContractMakerNavigation)
                    .WithMany(p => p.SupplierContract)
                    .HasForeignKey(d => d.ContractMaker)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKSupplier_C539802");

                entity.HasOne(d => d.Supplier)
                    .WithOne(p => p.SupplierContract)
                    .HasForeignKey<SupplierContract>(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKSupplier_C625466");
            });

            modelBuilder.Entity<SupplierShipment>(entity => {
                entity.ToTable("supplier_shipment");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("FKSupplier_S244021");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImportTime).HasColumnName("import_time");

                entity.Property(e => e.SupplierId)
                    .HasColumnName("supplier_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierShipment)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKSupplier_S244021");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("user");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("FKUser117288");

                entity.HasIndex(e => e.RoleId)
                    .HasName("FKUser87814");

                entity.HasIndex(e => e.Username)
                    .HasName("username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created).HasColumnName("created");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LastLogin).HasColumnName("last_login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FKUser117288");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKUser87814");
            });

            modelBuilder.Entity<Workshift>(entity => {
                entity.ToTable("workshift");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DayOfWeek)
                    .HasColumnName("day_of_week")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.SalaryPerHour)
                    .HasColumnName("salary_per_hour")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartTime).HasColumnName("start_time");
            });

            modelBuilder.Entity<WorkshiftAssignment>(entity => {
                entity.ToTable("workshift_assignment");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("FKWorkshift_618183");

                entity.HasIndex(e => e.WorkshiftId)
                    .HasName("FKWorkshift_546973");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Multiplier)
                    .HasColumnName("multiplier")
                    .HasColumnType("decimal(19,0)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.WorkshiftId)
                    .HasColumnName("workshift_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkshiftAssignment)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKWorkshift_618183");

                entity.HasOne(d => d.Workshift)
                    .WithMany(p => p.WorkshiftAssignment)
                    .HasForeignKey(d => d.WorkshiftId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FKWorkshift_546973");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
