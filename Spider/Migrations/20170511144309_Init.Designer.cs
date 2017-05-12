using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Spider;

namespace Spider.Migrations
{
    [DbContext(typeof(SpiderContext))]
    [Migration("20170511144309_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Spider.FoundEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Found");
                });

            modelBuilder.Entity("Spider.UrlEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Queue");
                });

            modelBuilder.Entity("Spider.VisitedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Visited");
                });
        }
    }
}
