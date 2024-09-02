using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DPPAPP.Models;

public partial class ParpolContext : DbContext
{
    public ParpolContext()
    {
    }

    public ParpolContext(DbContextOptions<ParpolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asamblea> Asambleas { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Convencione> Convenciones { get; set; }

    public virtual DbSet<Estatuto> Estatutos { get; set; }

    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public virtual DbSet<Imagen> Imagens { get; set; }

    public virtual DbSet<MiembroDesc> MiembroDescs { get; set; }

    public virtual DbSet<MiembrosDeOrganizacion> MiembrosDeOrganizacions { get; set; }

    public virtual DbSet<MiembrosDeOrganizacionView> MiembrosDeOrganizacionViews { get; set; }

    public virtual DbSet<OrganizacionPolitica> OrganizacionPoliticas { get; set; }

    public virtual DbSet<PartHistImg> PartHistImgs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TituloAbreviado> TituloAbreviados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asamblea>(entity =>
        {
            entity.HasKey(e => e.IdAsamblea).HasName("PK__ASAMBLEA__2D80C20A4C1631AC");

            entity.ToTable("ASAMBLEAS");

            entity.Property(e => e.IdAsamblea).HasColumnName("ID_ASAMBLEA");
            entity.Property(e => e.Asambleas)
                .IsRequired()
                .HasColumnName("ASAMBLEAS");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdOrganizacion).HasColumnName("ID_ORGANIZACION");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.CargoId).HasName("PK__CARGO__2585FE49C83032F9");

            entity.ToTable("CARGO");

            entity.Property(e => e.CargoId).HasColumnName("CARGO_ID");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
        });

        modelBuilder.Entity<Convencione>(entity =>
        {
            entity.HasKey(e => e.IdConvencion).HasName("PK__CONVENCI__A161E2C34CF79813");

            entity.ToTable("CONVENCIONES");

            entity.Property(e => e.IdConvencion).HasColumnName("ID_CONVENCION");
            entity.Property(e => e.Convencion)
                .IsRequired()
                .HasColumnName("CONVENCION");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdOrganizacion).HasColumnName("ID_ORGANIZACION");
        });

        modelBuilder.Entity<Estatuto>(entity =>
        {
            entity.HasKey(e => e.IdEstatuto).HasName("PK__ESTATUTO__C3A2A47A2129F755");

            entity.ToTable("ESTATUTOS");

            entity.Property(e => e.IdEstatuto).HasColumnName("ID_ESTATUTO");
            entity.Property(e => e.Estatuto1)
                .IsRequired()
                .HasColumnName("ESTATUTO");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.IdOrganizacion).HasColumnName("ID_ORGANIZACION");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdHistorialToken).HasName("PK__historia__03DC48A57782CEFE");

            entity.ToTable("historialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Token)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__historial__idUsu__69C6B1F5");
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__IMAGEN__586F31A06D345848");

            entity.ToTable("IMAGEN");

            entity.Property(e => e.IdImagen).HasColumnName("ID_IMAGEN");
            entity.Property(e => e.Imgen)
                .IsRequired()
                .HasColumnName("IMGEN");
        });

        modelBuilder.Entity<MiembroDesc>(entity =>
        {
            entity.HasKey(e => e.IdMiembro).HasName("PK__MIEMBRO___40AFC4DE048622DB");

            entity.ToTable("MIEMBRO_DESC");

            entity.Property(e => e.IdMiembro).HasColumnName("ID_MIEMBRO");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CargoId).HasColumnName("CARGO_ID");
            entity.Property(e => e.Cedula)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CEDULA");
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Edad).HasColumnName("EDAD");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ACTUALIZACION");
            entity.Property(e => e.FechaDesignacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_DESIGNACION");
            entity.Property(e => e.FechaInsercion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_INSERCION");
            entity.Property(e => e.Genero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GENERO");
            entity.Property(e => e.Imagen).HasColumnName("IMAGEN");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUNICIPIO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Provincia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROVINCIA");
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.Tituloadesc).HasColumnName("TITULOADESC");

            entity.HasOne(d => d.Cargo).WithMany(p => p.MiembroDescs)
                .HasForeignKey(d => d.CargoId)
                .HasConstraintName("FK__MIEMBRO_D__CARGO__5070F446");

            entity.HasOne(d => d.ImagenNavigation).WithMany(p => p.MiembroDescs)
                .HasForeignKey(d => d.Imagen)
                .HasConstraintName("FK__MIEMBRO_D__IMAGE__477199F1");

            entity.HasOne(d => d.TituloadescNavigation).WithMany(p => p.MiembroDescs)
                .HasForeignKey(d => d.Tituloadesc)
                .HasConstraintName("FK__MIEMBRO_D__TITUL__531856C7");
        });

        modelBuilder.Entity<MiembrosDeOrganizacion>(entity =>
        {
            entity.HasKey(e => e.IdMiembro).HasName("PK__MIEMBROS__40AFC4DEF018AD70");

            entity.ToTable("MIEMBROS_DE_ORGANIZACION");

            entity.Property(e => e.IdMiembro)
                .ValueGeneratedNever()
                .HasColumnName("ID_MIEMBRO");
            entity.Property(e => e.MiembroDesc).HasColumnName("MIEMBRO_DESC");
            entity.Property(e => e.OrganizacionId).HasColumnName("ORGANIZACION_ID");

            entity.HasOne(d => d.MiembroDescNavigation).WithMany(p => p.MiembrosDeOrganizacions)
                .HasForeignKey(d => d.MiembroDesc)
                .HasConstraintName("FK__MIEMBROS___MIEMB__6FE99F9F");

            entity.HasOne(d => d.Organizacion).WithMany(p => p.MiembrosDeOrganizacions)
                .HasForeignKey(d => d.OrganizacionId)
                .HasConstraintName("FK__MIEMBROS___ORGAN__70DDC3D8");
        });

        modelBuilder.Entity<MiembrosDeOrganizacionView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("miembrosDeOrganizacionView");

            entity.Property(e => e.Acronimo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("ACRONIMO");
            entity.Property(e => e.ApellidoMiembro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO_MIEMBRO");
            entity.Property(e => e.Cargo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CARGO");
            entity.Property(e => e.CargoId).HasColumnName("CARGO_ID");
            entity.Property(e => e.Cedula)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CEDULA");
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.DireccionDeSede)
                .IsRequired()
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("DIRECCION_DE_SEDE");
            entity.Property(e => e.Edad).HasColumnName("EDAD");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.EmailOrganizacion)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL_ORGANIZACION");
            entity.Property(e => e.FechaDesignacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_DESIGNACION");
            entity.Property(e => e.Genero)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GENERO");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdMiembro).HasColumnName("ID_MIEMBRO");
            entity.Property(e => e.Imagen).HasColumnName("IMAGEN");
            entity.Property(e => e.Imgen).HasColumnName("IMGEN");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Logo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LOGO");
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MUNICIPIO");
            entity.Property(e => e.MunicipioDeSede)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNICIPIO_DE_SEDE");
            entity.Property(e => e.NombreMiembro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_MIEMBRO");
            entity.Property(e => e.NombreOrganizacion)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_ORGANIZACION");
            entity.Property(e => e.Provincia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PROVINCIA");
            entity.Property(e => e.ProvinciaDeSede)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROVINCIA_DE_SEDE");
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
            entity.Property(e => e.TelefonoDeSede)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("TELEFONO_DE_SEDE");
            entity.Property(e => e.TituloaDesc)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("TITULOA_DESC");
            entity.Property(e => e.Tituloadesc1).HasColumnName("TITULOADESC");
        });

        modelBuilder.Entity<OrganizacionPolitica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ORGANIZA__3214EC279169431A");

            entity.ToTable("ORGANIZACION_POLITICA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acronimo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("ACRONIMO");
            entity.Property(e => e.AnoReconocimiento)
                .HasColumnType("datetime")
                .HasColumnName("ANO_RECONOCIMIENTO");
            entity.Property(e => e.Asambleas).HasColumnName("ASAMBLEAS");
            entity.Property(e => e.Convenciones).HasColumnName("CONVENCIONES");
            entity.Property(e => e.DireccionDeSede)
                .IsRequired()
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("DIRECCION_DE_SEDE");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Escano).HasColumnName("ESCANO");
            entity.Property(e => e.Estatutos).HasColumnName("ESTATUTOS");
            entity.Property(e => e.Logo)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("LOGO");
            entity.Property(e => e.MunicipioDeSede)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MUNICIPIO_DE_SEDE");
            entity.Property(e => e.NoActa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NO_ACTA");
            entity.Property(e => e.NoResolucion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NO_RESOLUCION");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Position).HasColumnName("POSITION");
            entity.Property(e => e.ProvinciaDeSede)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PROVINCIA_DE_SEDE");
            entity.Property(e => e.TelefonoDeSede)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("TELEFONO_DE_SEDE");
            entity.Property(e => e.Tipo)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TIPO");
            entity.Property(e => e.Website)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("WEBSITE");

            entity.HasOne(d => d.AsambleasNavigation).WithMany(p => p.OrganizacionPoliticas)
                .HasForeignKey(d => d.Asambleas)
                .HasConstraintName("FK__ORGANIZAC__ASAMB__28ED12D1");

            entity.HasOne(d => d.ConvencionesNavigation).WithMany(p => p.OrganizacionPoliticas)
                .HasForeignKey(d => d.Convenciones)
                .HasConstraintName("FK__ORGANIZAC__CONVE__27F8EE98");

            entity.HasOne(d => d.EstatutosNavigation).WithMany(p => p.OrganizacionPoliticas)
                .HasForeignKey(d => d.Estatutos)
                .HasConstraintName("FK__ORGANIZAC__ESTAT__2704CA5F");
        });

        modelBuilder.Entity<PartHistImg>(entity =>
        {
            entity.HasKey(e => e.IdHisImg).HasName("PK__PART_HIS__761AD7AD135D516A");

            entity.ToTable("PART_HIST_IMG");

            entity.Property(e => e.IdHisImg).HasColumnName("ID_HIS_IMG");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.HisImg).HasColumnName("HIS_IMG");
            entity.Property(e => e.OrgId).HasColumnName("ORG_ID");

            entity.HasOne(d => d.Org).WithMany(p => p.PartHistImgs)
                .HasForeignKey(d => d.OrgId)
                .HasConstraintName("FK__PART_HIST__ORG_I__0E04126B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__ROLES__3D978A358804881B");

            entity.ToTable("ROLES");

            entity.Property(e => e.UserRole)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TituloAbreviado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TITULO_A__3214EC27ECAAFECA");

            entity.ToTable("TITULO_ABREVIADO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TituloaDesc)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("TITULOA_DESC");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsiario).HasName("PK__USUARIO__D6DADF42D9704DEB");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Clave)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.UserRole)
                .HasConstraintName("FK__USUARIO__UserRol__7DCDAAA2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
