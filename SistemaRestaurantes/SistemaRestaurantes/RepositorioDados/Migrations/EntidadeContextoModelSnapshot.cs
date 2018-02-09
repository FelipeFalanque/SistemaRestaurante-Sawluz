using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using RepositorioDados;

namespace RepositorioDados.Migrations
{
    [DbContext(typeof(EntidadeContexto))]
    partial class EntidadeContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositorioDados.Entidades.Prato", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<int>("RestauranteID");

                    b.Property<double>("Valor");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("RepositorioDados.Entidades.Restaurante", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("RepositorioDados.Entidades.Prato", b =>
                {
                    b.HasOne("RepositorioDados.Entidades.Restaurante")
                        .WithMany()
                        .HasForeignKey("RestauranteID");
                });
        }
    }
}
