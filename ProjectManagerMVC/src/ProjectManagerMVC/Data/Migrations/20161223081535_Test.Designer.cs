using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManagerMVC.Data;

namespace ProjectManagerMVC.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161223081535_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Business", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("ID");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Business_ID");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("ID");

                    b.HasIndex("Business_ID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Maintainance_Task", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ApprovedComplete");

                    b.Property<DateTime>("ApprovedDate");

                    b.Property<int?>("Business_ID");

                    b.Property<DateTime>("CompletionDate");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("Deadline");

                    b.Property<int?>("Department_ID");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<bool>("IsPriority");

                    b.Property<int?>("Maintask_ID");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<decimal>("Price");

                    b.Property<int?>("Staff_ID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("Status_ID");

                    b.HasKey("ID");

                    b.HasIndex("Business_ID");

                    b.HasIndex("Department_ID");

                    b.HasIndex("Maintask_ID");

                    b.HasIndex("Staff_ID");

                    b.HasIndex("Status_ID");

                    b.ToTable("Maintainance_Task");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Staff", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Department_ID");

                    b.HasKey("ID");

                    b.HasIndex("Department_ID");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Status", b =>
                {
                    b.Property<int>("Status_ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Status_Name")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Status_ID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjectManagerMVC.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjectManagerMVC.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectManagerMVC.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Department", b =>
                {
                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Business", "Business")
                        .WithMany()
                        .HasForeignKey("Business_ID");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Maintainance_Task", b =>
                {
                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Business", "Business")
                        .WithMany()
                        .HasForeignKey("Business_ID");

                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_ID");

                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Maintainance_Task", "Maintask")
                        .WithMany()
                        .HasForeignKey("Maintask_ID");

                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("Staff_ID");

                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Status", "Status")
                        .WithMany()
                        .HasForeignKey("Status_ID");
                });

            modelBuilder.Entity("ProjectManagerMVC.Models.TaskManagerViewModels.Staff", b =>
                {
                    b.HasOne("ProjectManagerMVC.Models.TaskManagerViewModels.Department", "Department")
                        .WithMany()
                        .HasForeignKey("Department_ID");
                });
        }
    }
}
