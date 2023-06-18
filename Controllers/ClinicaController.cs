﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clinica.Modelos;
using Clinica.SqlTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Nest;
using static Nest.JoinField;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlClient;
using System.Linq;
using Serilog;

namespace Clinica.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        public readonly dbapiContext _dbcontext;
        private readonly string cadenaSQL;
        private readonly ILogger<ClinicaController> _logger;

        public ClinicaController(dbapiContext _context, IConfiguration config, ILogger<ClinicaController> logger)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
            _dbcontext = _context;
            _logger = logger;

        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Patient> Lista = new List<Patient>();
            try
            {
                Lista = _dbcontext.Patients.Where(p => p.Activo == true).ToList();
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpGet]
        [Route("ListaTodos")]
        public IActionResult ListaTodos()
        {
            List<Patient> Lista = new List<Patient>();
            try
            {
                Lista = _dbcontext.Patients.ToList();
                return Ok(Lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }
        [HttpGet]
        [Route("Listas")]
        public IActionResult Listas()
        {
            List<IdtherapistIdtherapy> Lista = new List<IdtherapistIdtherapy>();
            try
            {
                Lista = _dbcontext.IdtherapistIdtherapies.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpGet]
        [Route("ListaUsers")]
        public IActionResult ListaUsers()
        {
            List<User> Lista = new List<User>();
            try
            {
                Lista = _dbcontext.Users.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpPost]
        [Route("GuardarPaciente")]
        public IActionResult Guardar([FromBody] Patient objeto)
        {
            try
            {
                _dbcontext.Patients.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("ContabilidadReportes")]
        public IActionResult ContabilidadReportes([FromBody] Inversion objeto)
        {
            try
            {
                _dbcontext.Inversions.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarPaciente")]
        public IActionResult EditarPaciente([FromBody] Patient objeto)
        {

            Patient oProducto = _dbcontext.Patients.Find(objeto.IdPatients);

            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }

            try
            {

                oProducto.Name = objeto.Name is null ? oProducto.Name : objeto.Name;
                oProducto.Sex = objeto.Sex is null ? oProducto.Sex : objeto.Sex;
                oProducto.ParentsName = objeto.ParentsName is null ? oProducto.ParentsName : objeto.ParentsName;
                oProducto.ParentOrGuardianPhoneNumber = objeto.ParentOrGuardianPhoneNumber is null ? oProducto.ParentOrGuardianPhoneNumber : objeto.ParentOrGuardianPhoneNumber;
                oProducto.NumberMothers = objeto.NumberMothers is null ? oProducto.NumberMothers : objeto.NumberMothers;
                oProducto.DateOfBirth = objeto.DateOfBirth is null ? oProducto.DateOfBirth : objeto.DateOfBirth;
                oProducto.Activo = objeto.Activo is null ? oProducto.Activo : objeto.Activo;
                oProducto.Age = objeto.Age is null ? oProducto.Age : objeto.Age;
                oProducto.EducationalInstitution = objeto.EducationalInstitution is null ? oProducto.EducationalInstitution : objeto.EducationalInstitution;
                oProducto.Course = objeto.Course is null ? oProducto.Course : objeto.Course;
                oProducto.WhoRefers = objeto.WhoRefers is null ? oProducto.WhoRefers : objeto.WhoRefers;
                oProducto.FamilySettings = objeto.FamilySettings is null ? oProducto.FamilySettings : objeto.FamilySettings;
                oProducto.TherapiesOrServiceYouWillReceiveAtTheCenter = objeto.TherapiesOrServiceYouWillReceiveAtTheCenter is null ? oProducto.TherapiesOrServiceYouWillReceiveAtTheCenter : objeto.TherapiesOrServiceYouWillReceiveAtTheCenter;
                oProducto.Diagnosis = objeto.Diagnosis is null ? oProducto.Diagnosis : objeto.Diagnosis;
                oProducto.Recommendations = objeto.Recommendations is null ? oProducto.Recommendations : objeto.Recommendations;
                oProducto.FamilyMembersConcerns = objeto.FamilyMembersConcerns is null ? oProducto.FamilyMembersConcerns : objeto.FamilyMembersConcerns;
                oProducto.SpecificMedicalCondition = objeto.SpecificMedicalCondition is null ? oProducto.SpecificMedicalCondition : objeto.SpecificMedicalCondition;
                oProducto.Other = objeto.Other is null ? oProducto.Other : objeto.Other;

                _dbcontext.Patients.Update(oProducto);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("CrearTerapia")]
        public IActionResult CrearTerapia([FromBody] Therapy objeto)
        {
            try
            {
                _dbcontext.Therapies.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("EditarTerapia")]
        public IActionResult EditarTerapia([FromBody] Therapy objeto)
        {
            Therapy oProducto = _dbcontext.Therapies.Find(objeto.IdTherapy);
            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            try
            {
                oProducto.Label = objeto.Label is null ? oProducto.Label : objeto.Label;
                oProducto.Description = objeto.Description is null ? oProducto.Description : objeto.Description;
                oProducto.Price = objeto.Price is null ? oProducto.Price : objeto.Price;
                oProducto.Porcentaje = objeto.Porcentaje is null ? oProducto.Porcentaje : objeto.Porcentaje;
                _dbcontext.Therapies.Update(oProducto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarAdmin")]
        public IActionResult EditarAdmin([FromBody] SqlTables.User objeto)
        {
            User oProducto = _dbcontext.Users.Find(objeto.IdUser);
            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            try
            {
                oProducto.Names = objeto.Names is null ? oProducto.Names : objeto.Names;
                oProducto.Email = objeto.Email is null ? oProducto.Email : objeto.Email;
                _dbcontext.Users.Update(oProducto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("Asistencias")]
        public IActionResult Asistencias([FromBody] AsistenciaViewModels objAttendance)
        {


             foreach(var fecha in objAttendance.FechaInicio)
                {

                Attendance asistencia = new Attendance()
                {
                    IdPatients = objAttendance.IdPatients,
                    IdTerapeuta = objAttendance.IdTerapeuta,
                    IdTherapy = objAttendance.IdTherapy,
                    FechaInicio = fecha,
                    TipoAsistencias = objAttendance.TipoAsistencias,
                    Remarks = objAttendance.Remarks,

                };         

                _dbcontext.Attendances.Add(asistencia);
                _dbcontext.SaveChanges();
              }

        
            return Ok("Juan");
        }



        [HttpPost]
        [Route("fechas")]
        public IActionResult Fechas([FromBody] ListaFecha objeto)
        {



            return Ok();

        }



        [HttpGet]
        [Route("Calendario")]
        public IActionResult Calendario()
        {
            List<Attendance> Lista = new List<Attendance>();
            try
            {
                Lista = _dbcontext.Attendances.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }


        [HttpGet]
        [Route("Consultorios")]
        public IActionResult Consultorios()
        {
            List<SqlTables.Consultorio> Lista = new List<SqlTables.Consultorio>();
            try
            {
                Lista = _dbcontext.Consultorios.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        
        }

        [HttpGet]
        [Route("terapeuta")]
        public IActionResult terapeuta()
        {
            List<User> Lista = new List<User>();
            try
            {
                var usuarios = _dbcontext.Users.Where(u => u.IdRol == 2).ToList();
                return StatusCode(StatusCodes.Status200OK, new { usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { Lista });
            }
        }

        [HttpPost]
        [Route("Fecha")]
        public IActionResult Fecha(Attendance IdAsistencias)
        {
            var evento = _dbcontext.Attendances.FirstOrDefault(e => e.IdAsistencias == IdAsistencias.IdAsistencias);
            if (evento == null)
            {
                return NotFound();
            }
            _dbcontext.Attendances.Remove(evento);
            _dbcontext.SaveChanges();
            return Ok();
        }

        //  <----------------------------- filtrar Moscu -------------------------> 

        [HttpGet]
        [Route("Moscu")]
        public IActionResult Moscu()
        {
            List<Therapy> Lista = new List<Therapy>();
            try
            {
                Lista = _dbcontext.Therapies.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Lista });
            }
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public IActionResult CrearUsuario([FromBody] User objeto)
        {
            try
            {
                _dbcontext.Users.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("CrearConsultorio")]
        public IActionResult CrearConsultorio([FromBody] SqlTables.Consultorio objeto)
        {
            try
            {
                _dbcontext.Consultorios.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public IActionResult EliminarUsuario([FromBody] User objeto)
        {
            try
            {
                var usuarioEncontrado = _dbcontext.Users.FirstOrDefault(u => u.IdUser == objeto.IdUser);
                _dbcontext.Remove(usuarioEncontrado);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("EliminarConsultorio")]
        public IActionResult EliminarConsultorio([FromBody] SqlTables.Consultorio objeto)
        {
            try
            {
                var usuarioEncontrado = _dbcontext.Consultorios.FirstOrDefault(u => u.IdConsultorio == objeto.IdConsultorio);
                _dbcontext.Remove(usuarioEncontrado);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        [Route("GuardarUsers")]
        public IActionResult GuardarUsers([FromBody] User objeto)
        {

            User oProducto = _dbcontext.Users.Find(objeto.IdUser);

            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            oProducto.Names = objeto.Names is null ? oProducto.Names : objeto.Names;
            oProducto.Apellido = objeto.Apellido is null ? oProducto.Apellido : objeto.Apellido;
            oProducto.Telefono = objeto.Telefono is null ? oProducto.Telefono : objeto.Telefono;
            oProducto.Direccion = objeto.Direccion is null ? oProducto.Direccion : objeto.Direccion;
            oProducto.Email = objeto.Email is null ? oProducto.Email : objeto.Email;
            oProducto.Password = objeto.Password is null ? oProducto.Password : objeto.Password;
            oProducto.IdRol = objeto.IdRol is null ? oProducto.IdRol : objeto.IdRol;

            _dbcontext.Users.Update(oProducto);
            _dbcontext.SaveChanges();
            return Ok();

        }

        [HttpPost]
        [Route("EditarConsultorio")]
        public IActionResult EditarConsultorio([FromBody] SqlTables.Consultorio objeto)
        {

            SqlTables.Consultorio oProducto = _dbcontext.Consultorios.Find(objeto.IdConsultorio);

            if (oProducto == null)
            {
                return BadRequest("producto no encontrado");
            }
            oProducto.Nombre = objeto.Nombre is null ? oProducto.Nombre : objeto.Nombre;
            oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
          

            _dbcontext.Consultorios.Update(oProducto);
            _dbcontext.SaveChanges();
            return Ok();

        }


        [HttpGet]
        [Route("ListaTerapia")]
        public IActionResult ListaTerapia()
        {
            List<Probar> viewModal = new List<Probar>();
            try
            {
                var oLista = _dbcontext.Therapies.ToList();
                foreach (var cita in oLista)
                {
                    Probar nuevoObjetao = new Probar();
                    nuevoObjetao.NombreTerapia = cita;
                    viewModal.Add(nuevoObjetao);
                }

                return Ok(viewModal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, viewModal });
            }
        }

        [HttpPost]
        [Route("GetEvaluacionByTerapeuta")]
        public async Task<IActionResult> GetEvaluacionByTerapeuta(IdtherapistIdtherapy terapeutaId)
        {
            List<Probar> viewModal = new List<Probar>();
            var evaluaciones = await _dbcontext.IdtherapistIdtherapies.Where(e => e.Idterapeuta == terapeutaId.Idterapeuta).ToListAsync();

            if (evaluaciones != null)
            {
                foreach (var cita in evaluaciones)
                {
                    var idsTerapia = cita.Idtherapia;
                    var terapia = await FiltrarTerapia(idsTerapia);

                    Probar nuevoObjeto = new Probar();
                    nuevoObjeto.NombreTerapia = terapia;
                    viewModal.Add(nuevoObjeto);
                }
            }

            return Ok(viewModal);
        }

        [HttpPost]
        [Route("BuscarPacientePorTerapeuta")]
        public async Task<IActionResult> BuscarPacientePorTerapeuta(IdtherapistIdtherapy terapeutaId)
        {
            try
            {
                List<Probar> viewModal = new List<Probar>();
                var evaluaciones = await _dbcontext.Evaluations
                .Where(e => e.IdTerapeuta == terapeutaId.Idterapeuta)
                .Select(e => e.IdPatients) 
                .Distinct()
                .ToListAsync();

                            if (evaluaciones != null)
                            {
                               foreach (var cita in evaluaciones)
                               {
                                       var resPaciente = await Filtrar(cita);
                                      Probar nuevoObjeto = new Probar();
                                      nuevoObjeto.NombrePaciente = resPaciente;
                                     viewModal.Add(nuevoObjeto);
                               }
                            }

                    return Ok(viewModal);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

   


        [HttpPost]
        [Route("FiltrarGastos")]
        public async Task<IActionResult> FiltrarGastos(Inversion obj)
        {
            string mensaje = string.Empty;
            List<Inversion> viewModal = new List<Inversion>();
            var gastos = _dbcontext.Inversions.Where(x => x.DateOfInvestment >= obj.DateOfInvestment && x.DateOfInvestment < obj.EndDate).ToList();

            if (gastos.Count != 0)
            {
                foreach (var cita in gastos)
                {
                    Inversion inversion = new Inversion();
                    inversion.Descripcion = cita.Descripcion;
                    inversion.Nombre = cita.Nombre;
                    inversion.Amount = cita.Amount;
                    inversion.DateOfInvestment = cita.DateOfInvestment;
                    viewModal.Add(inversion);
                }
                return Ok(viewModal);
            }
            mensaje = "No hubo Inversión para esta fecha";
            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje });
        }



        //aquiiiii ------------------>\

       

        [HttpPost]
        [Route("GastosGanancia")]
        public async Task<IActionResult> GastosGanancia(Attendance obj)
        {

            Attendance fechaInicio = new Attendance();

            List<Probar> viewModal = new List<Probar>();
            var attendanceList = _dbcontext.Attendances.Where(x => x.FechaInicio >= obj.FechaInicio && x.FechaInicio < obj.FechaFinal).ToList();
            foreach (var cita in attendanceList)
            {
                var idsTerapia = cita.IdTherapy;
                var fechas = cita.FechaInicio;
                var terapia = await FiltrarTerapia(idsTerapia);

                fechaInicio.FechaInicio = fechas;

                Probar nuevoObjeto = new Probar();
                nuevoObjeto.FechaInicio = fechaInicio;

                nuevoObjeto.NombreTerapia = terapia;

                viewModal.Add(nuevoObjeto);
            }
            return StatusCode(StatusCodes.Status200OK, new { viewModal });
        }

        [HttpPost]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar(Attendance obj)
        {

            try
            {
                List<Probar> viewModal = new List<Probar>();
                var attendanceList = _dbcontext.Attendances.Where(x => x.IdTerapeuta == obj.IdTerapeuta && x.FechaInicio >= obj.FechaInicio && x.FechaInicio < obj.FechaFinal).ToList();
                foreach (var cita in attendanceList)
                {
                    var ids = cita.IdPatients;
                    var idsTerapia = cita.IdTherapy;
                    var idTerapeuta = cita.IdTerapeuta;
                    var paciente = await Filtrar(ids);
                    var terapia = await FiltrarTerapia(idsTerapia);
                    var terapeuta = await FiltrarTerapeuta(idTerapeuta);
                    Probar nuevoObjeto = new Probar();
                    nuevoObjeto.NombrePaciente = paciente;
                    nuevoObjeto.NombreTerapia = terapia;
                    nuevoObjeto.NombreTerapeuta = terapeuta;
                    viewModal.Add(nuevoObjeto);
                }
                return Ok(viewModal);
            }
            catch (Exception ex)
            {
                return Ok("error");
            }

        }
        private async Task<Patient> Filtrar(int? ids)
        {
            var paciente = await _dbcontext.Patients.FindAsync(ids);
            return paciente;
        }
        private async Task<Therapy> FiltrarTerapia(int? idTerapia)
        {
            var Tera = await _dbcontext.Therapies.FindAsync(idTerapia);
            return Tera;
        }

        private async Task<User> FiltrarTerapeuta(int? idTerapeuta)
        {
            var Terapeuta = await _dbcontext.Users.FindAsync(idTerapeuta);
            return Terapeuta;
        }

        [HttpPost]
        [Route("TraerUsuario")]
        public IActionResult TraerUsuario([FromBody] User usuario)
        {
            try
            {
                var users = _dbcontext.Users.FirstOrDefault(u => u.IdUser == usuario.IdUser);
                return StatusCode(StatusCodes.Status200OK, new { users });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPost]
        [Route("AgregarEvento")]
        public IActionResult AgregarEvento([FromBody] Attendance agenda)
        {
            _dbcontext.Attendances.Add(agenda);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("EliminarPaciente")]
        public IActionResult EliminarPaciente([FromBody] Patient IdPaciente)
        {
            var paciente = _dbcontext.Patients.FirstOrDefault(u => u.IdPatients == IdPaciente.IdPatients);
            if (paciente == null)
            {
                NoContent();
            }
            _dbcontext.Remove(paciente);
            _dbcontext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("EliminarTerapia")]
        public IActionResult EliminarTerapia([FromBody] Therapy terapia)
        {
            var terapiaEncontrada = _dbcontext.Therapies.FirstOrDefault(u => u.IdTherapy == terapia.IdTherapy);
            if (terapiaEncontrada == null)
            {
                NoContent();
            }
            _dbcontext.Remove(terapiaEncontrada);
            _dbcontext.SaveChanges();
            return Ok();
        }




        [HttpPost]
        [Route("CrearAbono")]
        public IActionResult CrearAbono([FromBody] Abono objeto)
        {
            try
            {
                _dbcontext.Abonos.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }




        [HttpPost]
        [Route("Probar")]
        public object Probar([FromBody] Buscar obj)
        {
            List<Buscar> lista = new List<Buscar>();

            try
            {
                using (var dbContext = _dbcontext)
                {
                    var result = from a in dbContext.Attendances
                                 join t in dbContext.Therapies on a.IdTherapy equals t.IdTherapy
                                 where a.FechaInicio >= obj.FechaInicio && a.FechaInicio <= obj.FechaFinal
                                 select new Buscar
                                 {
                                     Price = t.Price,
                                     Label = t.Label,
                                     FechaInicio = a.FechaInicio,
                                     FechaFinal = a.FechaFinal
                                 };

                    lista = result.ToList();
                }
            }
            catch (Exception ex)
            {
                lista = new List<Buscar>();
            }
            return lista;
        }


        [HttpPost]
        [Route("AbonoTerapias")]
        public IActionResult AbonoTerapias([FromBody] AbonosTerapia objeto)
        {
            try
            {
                _dbcontext.AbonosTerapias.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }




        [HttpPost]
        [Route("EliminarCita")]
        public IActionResult EliminarCita([FromBody] Buscar obj)
        {

            var resRecu = _dbcontext.Recurrencia.Where(u => u.IdEvaluation == obj.IdEvaluation).ToList();



            if (resRecu == null)
            {
                NoContent();
            }

            foreach (var numero in resRecu)
            {
                _dbcontext.Recurrencia.Remove(numero);
                _dbcontext.SaveChanges();

            }


            var resEva = _dbcontext.Evaluations.FirstOrDefault(u => u.Id == obj.IdEvaluation);

            if (resEva == null)
            {
                NoContent();
            }
            _dbcontext.Remove(resEva);
            _dbcontext.SaveChanges();
            
            return Ok();
        }
        [HttpPost]
        [Route("Post")]
        public IActionResult Post(ListaEnteros obj)
        {

            foreach (var numero in obj.teras)
            {
                _dbcontext.IdtherapistIdtherapies.AddRange(new IdtherapistIdtherapy { Idtherapia = numero, Idterapeuta = obj.id });
            }
            _dbcontext.SaveChanges();
            return Ok();
        }


        [HttpPost]
        [Route("EditarCitas")]
        public IActionResult EditarCitas([FromBody] Citas objeto )
        {
            try
            {
         
                Evaluation oProducto =  _dbcontext.Evaluations.Find(objeto.Id);

                if (oProducto == null)
                {
                    return BadRequest("producto no encontrado");
                }

                oProducto.IdPatients = objeto.IdPatients is null ? oProducto.IdPatients : objeto.IdPatients;
                oProducto.IdTherapy = objeto.IdTherapy is null ? oProducto.IdTherapy : objeto.IdTherapy;
                oProducto.Price = objeto.Price is null ? oProducto.Price : objeto.Price;
                oProducto.IdTerapeuta = objeto.IdTerapeuta is null ? oProducto.IdTerapeuta : objeto.IdTerapeuta;
                oProducto.Visitas = objeto.Visitas is null ? oProducto.Visitas : objeto.Visitas;
                oProducto.IdConsultorio = objeto.IdConsultorio is null ? oProducto.IdConsultorio : objeto.IdConsultorio;

                _dbcontext.Evaluations.Update(oProducto);
                  _dbcontext.SaveChanges();
           
                return Ok();

            }catch(Exception ex)
            {
                return BadRequest();
            }


        }

     

   

        [HttpGet]
        [Route("Citas")]
        public async Task<object> Citas()
        {
       
            string mensaje = string.Empty;
            List<Evaluation> viewModal = new List<Evaluation>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            List<Buscar> recu = new List<Buscar>();
            List<Buscar> InfoProcesada = new List<Buscar>();

            try
            {

    
                using (var dbContext = _dbcontext)
                {
                    var result = from r in dbContext.Recurrencia
                                 
                                 select new Buscar
                                 {
                                     FechaInicio = r.FechaInicio,
                                     Repetir = r.Repetir,
                                     Frecuencia = r.Frecuencia,
                                     Dias = r.Dias,
                                     IdEvaluation = r.IdEvaluation,
                                     IdRecurrencia = r.IdRecurrencia
                                 };

                    recu = await result.ToListAsync();

                    List<Recurrencia> prose = new List<Recurrencia>()
                    {
                        new Recurrencia
                        {
                            Repetir = 4,
                            Frecuencia = "mensual",
                            Dias = "jueves",
                            IdEvaluation  = 2

                        },
                           new Recurrencia
                        {
                            Repetir = 4,
                            Frecuencia = "mensual",
                            Dias = "jueves",
                            IdEvaluation  = 2

                        },
                              new Recurrencia
                        {
                            Repetir = 4,
                            Frecuencia = "mensual",
                            Dias = "jueves",
                            IdEvaluation  = 3

                        }
                    };
                    foreach (var pro in recu)
                    {
                        var idEva = pro.IdEvaluation;
                        Buscar recuProcesada = InfoProcesada.FirstOrDefault(f => f.IdEvaluation == idEva);

                        if (recuProcesada == null)
                        {
                            InfoProcesada.Add(pro); // Agregar 'pro' en lugar de 'recuProcesada'
                        }
                    }


                    foreach (var listado in InfoProcesada)
                    {
                   
                        var idEva = listado.IdEvaluation;


                        var resultEva = from e in dbContext.Evaluations
                                        join c in dbContext.Consultorios on e.IdConsultorio equals c.IdConsultorio
                                        join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                        join p in dbContext.Patients on e.IdPatients equals p.IdPatients
                                        join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                        where e.Id == idEva && p.Activo == true
                                        select new Modelos.UserEvaluacion
                                        {
                                            IdEvaluacion = idEva,
                                            Terapeuta = new User
                                            {
                                                IdUser = u.IdUser,
                                                Names = u.Names,
                                                Apellido = u.Apellido
                                            },
                                            Terapia = new Therapy
                                            {
                                                IdTherapy = t.IdTherapy,
                                                Label = t.Label
                                            },

                                            Paciente = new Patient
                                            {   IdPatients = p.IdPatients,
                                                Name = p.Name,
                                                Activo = p.Activo
                                            },
                                            FechaInicio = listado.FechaInicio,
                                            Price = e.Price,

                                            Consultorio = new Modelos.Consultorio
                                            {
                                                IdConsultorio = c.IdConsultorio,
                                                Nombre = c.Nombre,
                                            
                                            },

                                            Repetir = listado.Repetir,
                                            Frecuencia = listado.Frecuencia,
                                            Dias = listado.Dias,

                                            Recurrencia = new SqlTables.Recurrencium
                                            {
                                                IdRecurrencia = (int)listado.IdRecurrencia
                                            }
                                        };

                        olista.AddRange(resultEva.Distinct().ToList());


                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("el error en las citas es :" , ex);
                return ex;
            }
            return olista;
        }


        [HttpPost]
        [Route("FiltrarConsultorios")]
        public object FiltrarConsultorios(Buscar obj)
        {
            string mensaje = string.Empty;
            List<Evaluation> viewModal = new List<Evaluation>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            List<Buscar> recu = new List<Buscar>();


            using (var dbContext = _dbcontext)
            {
                var result = from r in dbContext.Recurrencia
                             where r.FechaInicio >= obj.FechaInicio && r.FechaInicio <= obj.FechaFinal
                             select new Buscar
                             {
                                 FechaInicio = r.FechaInicio,
                                 IdEvaluation = r.IdEvaluation
                             };

                recu = result.ToList();

                if(obj.IdConsultorio == 0)
                {
                    foreach (var listado in recu)
                    {
                        var idEva = listado.IdEvaluation;


                        var resultEva = from e in dbContext.Evaluations
                                        join c in dbContext.Consultorios on e.IdConsultorio equals c.IdConsultorio
                                        join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                        join p in dbContext.Patients on e.IdPatients equals p.IdPatients
                                        join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                        where e.Id == idEva  && p.Activo == true
                                        select new Modelos.UserEvaluacion
                                        {
                                            IdEvaluacion = idEva,

                                            Terapeuta = new User
                                            {
                                                Names = u.Names,
                                                Apellido = u.Apellido
                                            },
                                            Terapia = new Therapy
                                            {
                                                Label = t.Label
                                            },

                                            Paciente = new Patient
                                            {
                                                Name = p.Name
                                            },
                                            FechaInicio = listado.FechaInicio,

                                            Consultorio = new Modelos.Consultorio
                                            {
                                                Nombre = c.Nombre
                                            },
                                        };

                        olista.AddRange(resultEva.ToList());
                    }
                    return olista;
                }

                foreach (var listado in recu)
                {
                    var idEva = listado.IdEvaluation;


                    var resultEva = from e in dbContext.Evaluations
                                    join c in dbContext.Consultorios on e.IdConsultorio equals c.IdConsultorio
                                    join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                    join p in dbContext.Patients on e.IdPatients equals p.IdPatients
                                    join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                    where e.Id == idEva && e.IdConsultorio == obj.IdConsultorio && p.Activo == true
                                    select new Modelos.UserEvaluacion
                                    {
                                        IdEvaluacion = idEva,

                                        Terapeuta = new User
                                        {
                                            Names = u.Names,
                                            Apellido = u.Apellido
                                        },
                                        Terapia = new Therapy
                                        {
                                            Label = t.Label
                                        },
                                      
                                        Paciente = new Patient
                                        {
                                            Name = p.Name
                                        },
                                        FechaInicio = listado.FechaInicio,

                                           Consultorio = new Modelos.Consultorio
                                           {
                                               Nombre = c.Nombre
                                           },
                                    };

                    olista.AddRange(resultEva.ToList());
                }

            }

            return olista;
        }

        [Route("ListaEvaluacion")]
        public object ListaEvaluacion([FromBody] Buscar obj)
        {
             List<Buscar> lista = new List<Buscar>();
             List<UserEvaluacion> olista = new List<UserEvaluacion>();
            List<UserEvaluacion> Terapeutas = new List<UserEvaluacion>();
            try
            {
                using (var dbContext = _dbcontext)
                {
                    var result = from r in dbContext.Recurrencia
                                 where r.FechaInicio >= obj.FechaInicio && r.FechaInicio <= obj.FechaFinal
                                 select new Buscar
                                 {
                                     FechaInicio = r.FechaInicio,
                                     IdEvaluation = r.IdEvaluation
                                 };

                    lista = result.ToList();

                    foreach (var listado in lista)
                    {
                        var idEva = listado.IdEvaluation;


                        var resultEva = from e in dbContext.Evaluations
                                        join t in dbContext.Therapies on e.IdTherapy equals t.IdTherapy
                                        join u in dbContext.Users on e.IdTerapeuta equals u.IdUser
                                        where e.Id == idEva && e.IdTerapeuta == obj.IdTerapeuta
                                        select new Modelos.UserEvaluacion
                                        {

                                            Terapeuta = new User
                                            {   IdUser = u.IdUser,
                                                Names = u.Names
                                            },
                                            Terapia = new Therapy
                                            {
                                                Label = t.Label,
                                                Price = t.Price
                                            },
                                            FechaInicio = listado.FechaInicio           
                                        };

                        olista.AddRange(resultEva.ToList());                 
                    }

                    foreach (var item in olista)
                    {
                        int idTerapeuta = item.Terapeuta.IdUser;

                        bool existeTerapeuta = Terapeutas.Any(item => item.Terapeuta.IdUser == idTerapeuta);
                        if (existeTerapeuta)
                        {
                            Modelos.UserEvaluacion tera = Terapeutas.FirstOrDefault(u => u.Terapeuta.IdUser == idTerapeuta);
                        }
                        else
                        {
                           
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Buscar>();
            }
            return olista;
        }

        
        [Route("ListaEvaluacions")]
        public object ListaEvaluacions([FromBody] Buscar obj)
        {
            List<Buscar> lista = new List<Buscar>();
            List<UserEvaluacion> olista = new List<UserEvaluacion>();
            try
            {
                using (var dbContext = _dbcontext)
                {
                    var result = from r in dbContext.Recurrencia
                                 where r.FechaInicio >= obj.FechaInicio && r.FechaInicio <= obj.FechaFinal
                                 select new Buscar
                                 {
                                     FechaInicio = r.FechaInicio,
                                     IdEvaluation = r.IdEvaluation
                                 };

                    lista = result.ToList();


                    foreach (var listado in lista)
                    {
                        var idEva = listado.IdEvaluation;


                        var resultEva = from e in dbContext.Evaluations
                                        join a in dbContext.Attendances on new {e.IdTerapeuta} equals new { a.IdTerapeuta  }
                                        where e.Id == idEva && e.IdTerapeuta == obj.IdTerapeuta
                                        select new
                                        {
                                            Evaluation = e,
                                            Attendance = a
                                        };


                        foreach (var todos in resultEva)
                        {
                        }

                        var resultPatients = from r in resultEva
                                             join p in dbContext.Patients on r.Attendance.IdPatients equals p.IdPatients
                                           join t in dbContext.Therapies on r.Attendance.IdTherapy equals t.IdTherapy
                                           join u in dbContext.Users on r.Attendance.IdTerapeuta equals u.IdUser
                                             select new Modelos.UserEvaluacion
                                             {
                                               
                                                 Paciente = p,
                                                 Terapia = t,
                                                 Terapeuta = u,
                                                 FechaInicio = r.Attendance.FechaInicio,
                                                 Price = r.Evaluation.Price
                                             };

                        olista.AddRange(resultPatients.ToList());
                    }



                }
            }
            catch (Exception ex)
            {
                lista = new List<Buscar>();
            }
            return olista;
        }

    }
}



