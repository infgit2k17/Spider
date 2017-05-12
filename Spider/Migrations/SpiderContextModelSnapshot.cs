using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Spider.DAL;

namespace Spider.Migrations
{
    [DbContext(typeof(SpiderContext))]
    partial class SpiderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Spider.DAL.FoundEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("Found");
                });

            modelBuilder.Entity("Spider.DAL.UrlEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.ToTable("Queue");
                });

            modelBuilder.Entity("Spider.DAL.VisitedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Value")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Visited");
                });
        }
    }
}
