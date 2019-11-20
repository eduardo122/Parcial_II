using Examen_Practico.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;


namespace Examen_Practico.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Inicio() {

            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = (DataTable)Session["tmpdata"];
            }
            catch (Exception ex)
            {

            }

            return View(dt);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;

                    IExcelDataReader reader = null;


                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    int fieldcount = reader.FieldCount;
                    int rowcount = reader.RowCount;
                    DataTable dt = new DataTable();
                    DataRow row;
                    DataTable dt_ = new DataTable();
                    try
                    {
                        dt_ = reader.AsDataSet().Tables[0];
                        for (int i = 0; i < dt_.Columns.Count; i++)
                        {
                            dt.Columns.Add(dt_.Rows[0][i].ToString());
                        }
                        int rowcounter = 0;
                        for (int row_ = 1; row_ < dt_.Rows.Count; row_++)
                        {
                            row = dt.NewRow();

                            for (int col = 0; col < dt_.Columns.Count; col++)
                            {
                                row[col] = dt_.Rows[row_][col].ToString();
                                rowcounter++;
                            }
                            dt.Rows.Add(row);
                        }

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("File", "Unable to Upload file!");
                        return View();
                    }

                    DataSet result = new DataSet();
                    result.Tables.Add(dt);
                    reader.Close();
                    reader.Dispose();
                    DataTable tmp = result.Tables[0];
                    Session["tmpdata"] = tmp;  //store datatable into session
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
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


        public ActionResult Datatable()
        {
            DataTable dt = new DataTable();

            try
            {
                dt = (DataTable)Session["tmpdata"];
            }
            catch (Exception ex)
            {

            }

            return View(dt);

        }

       
    }
}