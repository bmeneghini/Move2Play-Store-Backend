using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class CemigAutorizadorContext : DbContext
    {
        public CemigAutorizadorContext()
        {
        }

        public CemigAutorizadorContext(DbContextOptions<CemigAutorizadorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Atendimento> Atendimento { get; set; }
        public virtual DbSet<Ocupacao> Ocupacao { get; set; }
        public virtual DbSet<OcupacaoProfissional> OcupacaoProfissional { get; set; }
        public virtual DbSet<Prestador> Prestador { get; set; }
        public virtual DbSet<Procedimento> Procedimento { get; set; }
        public virtual DbSet<ProcedimentosAtendimento> ProcedimentosAtendimento { get; set; }
        public virtual DbSet<ProfissionalExecutante> ProfissionalExecutante { get; set; }
        public virtual DbSet<TipoAtendimento> TipoAtendimento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendimento>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Cns)
                    .HasColumnName("CNS")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Dataatendimento)
                    .HasColumnName("DATAATENDIMENTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dataautorizacao)
                    .HasColumnName("DATAAUTORIZACAO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idprestador).HasColumnName("IDPRESTADOR");

                entity.Property(e => e.Indicacaoclinica)
                    .HasColumnName("INDICACAOCLINICA")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nomebeneficiario)
                    .HasColumnName("NOMEBENEFICIARIO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nomesolicitante)
                    .HasColumnName("NOMESOLICITANTE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ocupacaosolicitante)
                    .HasColumnName("OCUPACAOSOLICITANTE")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Registrosolicitante)
                    .HasColumnName("REGISTROSOLICITANTE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Rn).HasColumnName("RN");

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdprestadorNavigation)
                    .WithMany(p => p.Atendimento)
                    .HasForeignKey(d => d.Idprestador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ATENDIME_REFERENCE_PRESTADO");
            });

            modelBuilder.Entity<Ocupacao>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("DESCRICAO")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OcupacaoProfissional>(entity =>
            {
                entity.HasKey(e => new { e.Idocupacao, e.Idprofissional });

                entity.Property(e => e.Idocupacao).HasColumnName("IDOCUPACAO");

                entity.Property(e => e.Idprofissional).HasColumnName("IDPROFISSIONAL");

                entity.HasOne(d => d.IdocupacaoNavigation)
                    .WithMany(p => p.OcupacaoProfissional)
                    .HasForeignKey(d => d.Idocupacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OCUPACAO_REFERENCE_OCUPACAO");

                entity.HasOne(d => d.IdprofissionalNavigation)
                    .WithMany(p => p.OcupacaoProfissional)
                    .HasForeignKey(d => d.Idprofissional)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OCUPACAO_REFERENCE_PROFISSI");
            });

            modelBuilder.Entity<Prestador>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cnes)
                    .HasColumnName("CNES")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.CpfCnpj)
                    .IsRequired()
                    .HasColumnName("CPF_CNPJ")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Procedimento>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Codigo).HasColumnName("CODIGO");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("DESCRICAO")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idtipoatendimento).HasColumnName("IDTIPOATENDIMENTO");

                entity.Property(e => e.Tabela).HasColumnName("TABELA");

                entity.HasOne(d => d.IdtipoatendimentoNavigation)
                    .WithMany(p => p.Procedimento)
                    .HasForeignKey(d => d.Idtipoatendimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROCEDIM_REFERENCE_TIPOATEN");
            });

            modelBuilder.Entity<ProcedimentosAtendimento>(entity =>
            {
                entity.HasKey(e => new { e.Idprocedimento, e.Idatendimento });

                entity.Property(e => e.Idprocedimento).HasColumnName("IDPROCEDIMENTO");

                entity.Property(e => e.Idatendimento).HasColumnName("IDATENDIMENTO");

                entity.Property(e => e.Quantidade).HasColumnName("QUANTIDADE");

                entity.HasOne(d => d.IdatendimentoNavigation)
                    .WithMany(p => p.ProcedimentosAtendimento)
                    .HasForeignKey(d => d.Idatendimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROCEDIM_REFERENCE_ATENDIME");

                entity.HasOne(d => d.IdprocedimentoNavigation)
                    .WithMany(p => p.ProcedimentosAtendimento)
                    .HasForeignKey(d => d.Idprocedimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROCEDIM_REFERENCE_PROCEDIM");
            });

            modelBuilder.Entity<ProfissionalExecutante>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Conselho)
                    .IsRequired()
                    .HasColumnName("CONSELHO")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("ESTADO")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Idprestador).HasColumnName("IDPRESTADOR");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("NOME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Numeroconselho)
                    .IsRequired()
                    .HasColumnName("NUMEROCONSELHO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdprestadorNavigation)
                    .WithMany(p => p.ProfissionalExecutante)
                    .HasForeignKey(d => d.Idprestador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROFISSI_REFERENCE_PRESTADO");
            });

            modelBuilder.Entity<TipoAtendimento>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnName("CODIGO")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("DESCRICAO")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
