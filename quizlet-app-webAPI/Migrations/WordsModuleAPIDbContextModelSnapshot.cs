// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using quizlet_app_webAPI.Data;

#nullable disable

namespace quizlet_app_webAPI.Migrations
{
    [DbContext(typeof(WordsModuleAPIDbContext))]
    partial class WordsModuleAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("quizlet_app_webAPI.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("quizlet_app_webAPI.Models.Word", b =>
                {
                    b.Property<Guid>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Meaning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Transcription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WordImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WordsModuleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WordId");

                    b.HasIndex("WordsModuleId");

                    b.ToTable("Words", (string)null);
                });

            modelBuilder.Entity("quizlet_app_webAPI.Models.WordsModule", b =>
                {
                    b.Property<Guid>("WordsModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WordsCount")
                        .HasColumnType("int");

                    b.HasKey("WordsModuleId");

                    b.HasIndex("UserId");

                    b.ToTable("WordsModules", (string)null);
                });

            modelBuilder.Entity("quizlet_app_webAPI.Models.Word", b =>
                {
                    b.HasOne("quizlet_app_webAPI.Models.WordsModule", "WordsModule")
                        .WithMany("Words")
                        .HasForeignKey("WordsModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WordsModule");
                });

            modelBuilder.Entity("quizlet_app_webAPI.Models.WordsModule", b =>
                {
                    b.HasOne("quizlet_app_webAPI.Models.ApplicationUser", "User")
                        .WithMany("WordsModules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("quizlet_app_webAPI.Models.ApplicationUser", b =>
                {
                    b.Navigation("WordsModules");
                });

            modelBuilder.Entity("quizlet_app_webAPI.Models.WordsModule", b =>
                {
                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}
