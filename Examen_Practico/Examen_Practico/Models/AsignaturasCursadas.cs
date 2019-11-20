using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen_Practico.Models
{
    public class AsignaturasCursadas
    {
        public integrante listaIntegrantes { get; set; }
        public Asignatura listaAsignaturas { get; set; }
        public cursa listaCursa { get; set; }
        public profesor listaProfesores { get; set; }
        public imparte listaImparte { get; set; }
    }
}