using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SistemaNotas.Entidades.Usuarios;
using SistemaNotas.Datos.Mapping.Usuarios;
using SistemaNotas.Entidades.Administrar;
using SistemaNotas.Datos.Mapping.Administrar;
using SistemaNotas.Datos.Mapping.Docente;
using SistemaNotas.Entidades.Docente;
using SistemaNotas.Entidades.Estudiante;
using SistemaNotas.Datos.Mapping.Estudiante;

namespace SistemaNotas.Datos
{
    public class DbContextSistemaNotas : DbContext
    {
        public DbSet<Rol> roles { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Anioescolar> aniosescolares { get; set; }
        public DbSet<Periodoescolar> periodoescolares { get; set; }
        public DbSet<Curso> cursos { get; set; }
        public DbSet<Area> areas { get; set; }
        public DbSet<Materia> materias { get; set; }
        public DbSet<Cursoxmateriaxdocente> cursoxmateriaxdocentes { get; set; }
        public DbSet<Aniocursado> aniocursados { get; set; }
        public DbSet<Calificacion> calificaciones { get; set; }
        public DbSet<Director> directores { get; set; }

        public DbContextSistemaNotas(DbContextOptions<DbContextSistemaNotas> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AnioescolarMap());
            modelBuilder.ApplyConfiguration(new PeriodoescolarMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new AreaMap());
            modelBuilder.ApplyConfiguration(new MateriaMap());
            modelBuilder.ApplyConfiguration(new CursoxmateriaxdocenteMap());
            modelBuilder.ApplyConfiguration(new AniocursadoMap());
            modelBuilder.ApplyConfiguration(new CalificacionMap());
            modelBuilder.ApplyConfiguration(new DirectorMap());
        }
    }
}
