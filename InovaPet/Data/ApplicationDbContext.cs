using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InovaPet.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<Pet> Pet { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Matheus\\Documents\\InovaPet.mdf;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(14);

                entity.Property(e => e.Endereço)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasMaxLength(14);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.Property(e => e.Cor)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Peso)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Porte)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Raca)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pet)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_Cliente");
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.HasKey(e => e.IdServico);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Servico)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servico_Cliente");

                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Servico)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servico_Funcionario");

                entity.HasOne(d => d.IdPetNavigation)
                    .WithMany(p => p.Servico)
                    .HasForeignKey(d => d.IdPet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servico_Pet");
            });
        }
    }
}
