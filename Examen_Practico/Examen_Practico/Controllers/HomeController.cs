using Examen_Practico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Examen_Practico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact(integrante integrante)
        {
            Examen_ParcialEntities3 sd = new Examen_ParcialEntities3();
            List<integrante> listaintegrante = sd.integrantes.ToList();
            List<Asignatura> listaasignatura = sd.Asignaturas.ToList();
            List<cursa> listacursa = sd.cursas.ToList();
            List<profesor> listaProfesor = sd.profesors.ToList();
            List<imparte> listaImparte = sd.impartes.ToList();

            if (integrante.id == 0 ) {
                return Redirect("Asignaturas");
            }
            else { 
            ViewData["AsignaturasCursadas"] = from cur in listacursa
                                              join i in listaintegrante on cur.id_estudiante equals i.id into table1
                                              from i in table1.DefaultIfEmpty() where i.id == integrante.id
                                              join asign in listaasignatura on cur.id_materia equals asign.id into table2
                                              from asign in table2.DefaultIfEmpty()
                                              join imp in listaImparte on asign.id equals imp.id_materia into table3
                                              from imp in table3.DefaultIfEmpty()
                                              join prof in listaProfesor on imp.id_profesor equals prof.id into table4
                                              from prof in table4.DefaultIfEmpty()


                                              select new AsignaturasCursadas { listaAsignaturas = asign, listaIntegrantes = i, listaCursa = cur, listaImparte = imp, listaProfesores=prof };

            return View( ViewData["AsignaturasCursadas"]);
        }
        }




        [Authorize]
        public ActionResult Asignaturas()
        {

            Examen_ParcialEntities3 myEntity = new Examen_ParcialEntities3();
            var ListaEstudiantes = myEntity.integrantes.ToList();
            SelectList list = new SelectList(ListaEstudiantes, "ID", "Nombre");
            ViewBag.Integrantes = list;
            return View();
        }
        [Authorize]
        public ActionResult Agenda()
        {
            return View();
        }
        [Authorize]
        public ActionResult Lector()
        {
            return View();
        }
    }
}