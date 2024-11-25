using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosTech.Entidades;

namespace PosTech.Repository.Configurations
{
    public class RegiaoConfiguration
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.ToTable("Regiao");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever();
            builder.Property(p => p.Descricao).HasColumnType("VARCHAR(1000)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME");
            builder.Property(p => p.DataUltimaAlteracao).HasColumnType("DATETIME");
        }
    }
}
