using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosTech.Entidades;

namespace PosTech.Repository.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contato");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(p => p.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Telefone).HasColumnType("VARCHAR(20)").IsRequired();
            builder.Property(p => p.DataCriacao).HasColumnType("DATETIME");
            builder.Property(p => p.DataUltimaAlteracao).HasColumnType("DATETIME");
        }
    }
}
