using DPPAPP.DTO;
using DPPAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DPPAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class parpolController : ControllerBase
    {
        private readonly ParpolContext _dbcontext;

        public parpolController(ParpolContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        //[Authorize]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Listar()
        {
            List<OrganizacionPolitica> lista = _dbcontext.OrganizacionPoliticas
                .Include(x => x.MiembrosDeOrganizacions)
                .ThenInclude(x => x.MiembroDescNavigation)
                .Include(x=> x.PartHistImgs)
                .OrderBy(x => x.Position)
                .ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        [Route("listoneorg/{id:int}")]
        public async Task<IActionResult> listOneOrg(int id)
        {
            OrganizacionPolitica org = _dbcontext.OrganizacionPoliticas
                .Include(x => x.MiembrosDeOrganizacions)
                .ThenInclude(x => x.MiembroDescNavigation)
                .FirstOrDefault(x => x.Id == id);

            return StatusCode(StatusCodes.Status200OK, org);
        }

        [HttpGet]
        [Route("listtitle")]
        public async Task<IActionResult> ListTitle()
        {

            List<TituloAbreviado> titulo = _dbcontext.TituloAbreviados.OrderBy(x => x.Id).ToList();

            return StatusCode(StatusCodes.Status200OK, titulo);
        }

        [HttpGet]
        [Route("listmemberforoneorg/{acronimo?}")]
        public async Task<IActionResult> Listarmiem2ForOneOrg(string acronimo = null)
        {
            List<MiembrosDeOrganizacionView> lista = _dbcontext.MiembrosDeOrganizacionViews
                .Where(x => x.Acronimo == acronimo && x.Isactive == 1)
                .OrderBy(x => x.IdMiembro)
                .ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        [Route("listPadron/{muni?}")]
        public async Task<IActionResult> GetPadron(string muni = null)
        {
            if (string.IsNullOrWhiteSpace(muni))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No se recibio el municipio");
            }
            else
            {
                List<OrganizacionPolitica> padron = await _dbcontext.OrganizacionPoliticas
               .Include(x => x.MiembrosDeOrganizacions)
               .ThenInclude(x => x.MiembroDescNavigation)
               .ThenInclude(x => x.ImagenNavigation)
               .OrderBy(x => x.Position)
               .ToListAsync();

                List<List<OrganizacionPolitica>> organizacionesAgrupadas = new List<List<OrganizacionPolitica>>();
                List<OrganizacionPolitica> organizacionesTemp = new List<OrganizacionPolitica>();

                foreach (var organizacion in padron)
                {
                    organizacion.MiembrosDeOrganizacions = organizacion.MiembrosDeOrganizacions
                       .Where(miembro => miembro.MiembroDescNavigation != null
                       && miembro.MiembroDescNavigation.Isactive == 1
                       && miembro.MiembroDescNavigation.Municipio == muni)
                       .OrderBy(x => x.MiembroDescNavigation.CargoId)
                       .ToList();

                    organizacionesTemp.Add(organizacion);

                    if (organizacionesTemp.Count == 2)
                    {
                        organizacionesAgrupadas.Add(new List<OrganizacionPolitica>(organizacionesTemp));
                        organizacionesTemp.Clear();
                    }
                }
                return StatusCode(StatusCodes.Status200OK, organizacionesAgrupadas);
            }
        }

        [HttpGet]
        [Route("listcargo")]
        public async Task<IActionResult> ListCargo()
        {
            List<Cargo> lista = _dbcontext.Cargos.OrderBy(x => x.CargoId).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        [Route("listmember")]
        public async Task<IActionResult> Listarmiem2()
        {
            List<MiembrosDeOrganizacionView> lista = _dbcontext.MiembrosDeOrganizacionViews.OrderBy(x => x.IdMiembro).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        [Route("listestatutos/{id:int}")]
        public async Task<IActionResult> getEstatutos(int id)
        {
            List<Estatuto> estatutos = _dbcontext.Estatutos.Where(x => x.IdOrganizacion == id).OrderByDescending(x => x.IdEstatuto).ToList();

            return StatusCode(StatusCodes.Status200OK, estatutos);
        }

        [HttpGet]
        [Route("listasambleas/{id:int}")]
        public async Task<IActionResult> getAsambleas(int id)
        {
            List<Asamblea> asambleas = _dbcontext.Asambleas.Where(x => x.IdOrganizacion == id).OrderByDescending(x => x.IdAsamblea).ToList();

            return StatusCode(StatusCodes.Status200OK, asambleas);
        }

        [HttpGet]
        [Route("listconvenciones/{id:int}")]
        public async Task<IActionResult> getConvencione(int id)
        {
            List<Convencione> convenciones = _dbcontext.Convenciones.Where(x => x.IdOrganizacion == id).OrderByDescending(x => x.IdConvencion).ToList();

            return StatusCode(StatusCodes.Status200OK, convenciones);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] OrganizacionPolitica request)
        {
            await _dbcontext.OrganizacionPoliticas.AddAsync(request);
            await _dbcontext.SaveChangesAsync();
            int idOrg = request.Id;
            return StatusCode(StatusCodes.Status200OK, idOrg);
        }

        [HttpPost]
        [Route("createmember")]
        public async Task<IActionResult> CreateMember([FromBody] CreateMember request)
        {
            SqlParameter[] parameters =
             {
            new SqlParameter ("@Organizacion_id",request.Organizacion_id),
            new SqlParameter ("@Nombre", request.Nombre),
            new SqlParameter ("@Apellido", request.Apellido),
            new SqlParameter ("@Edad", request.Edad),
            new SqlParameter ("@Direccion", request.Direccion),
            new SqlParameter ("@Telefono", request.Telefono),
            new SqlParameter ("@Cargo_Id", request.Cargo_Id),
            new SqlParameter ("@Provincia",request.Provincia),
            new SqlParameter ("@Municipio", request.Municipio),
            new SqlParameter ("@Fecha_designacion", request.Fecha_designacion),
            new SqlParameter ("@Genero",request.Genero),
            new SqlParameter ("@Tituloadesc", request.Tituloadesc),
            new SqlParameter ("@Cedula", request.Cedula),
            new SqlParameter ("@Email", request.Email),
            new SqlParameter ("@Imagen", request.imagen)
            };
            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync
                ("EXEC CrearMiembro @Organizacion_id, @Nombre, @Apellido, @Edad, @Direccion, @Telefono, @Cargo_Id, @Provincia, @Municipio, @Fecha_designacion, @Genero, @Tituloadesc, @Cedula, @Email, @Imagen", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return StatusCode(StatusCodes.Status200OK, "Created" + parameters);
        }

        [HttpPost]
        [Route("postimagethen")]
        public async Task<IActionResult> postImageThen(ImageForUserDTO request)
        {

            Console.WriteLine(request);
            if (request == null || string.IsNullOrEmpty(request.Imgen))
            {
                return StatusCode(StatusCodes.Status200OK, 1);
            }
            else
            {
                byte[] imageBytes = Convert.FromBase64String(request.Imgen);
                Console.WriteLine($"bytes document:  {imageBytes.Length}");

                Imagen image = new()
                {
                    Imgen = imageBytes,
                };

                await _dbcontext.Imagens.AddAsync(image);
                await _dbcontext.SaveChangesAsync();

                int imageId = image.IdImagen;

                return StatusCode(StatusCodes.Status200OK, imageId);

            }
        }

        [HttpPut]
        [Route("updateimagethen/{id:int}")]
        public async Task<IActionResult> updateImageThen(int id, ImageForUserDTO request)
        {
            try
            {
                Imagen imagen = await _dbcontext.Imagens.FindAsync(id);

                if (request == null || string.IsNullOrEmpty(request.Imgen))
                {
                    return BadRequest("Invalid input");
                }

                byte[] imageBytes = Convert.FromBase64String(request.Imgen);
                Console.WriteLine(imageBytes);
                Imagen image = new()
                {
                    Imgen = imageBytes
                };

                imagen.Imgen = image.Imgen;

                _dbcontext.Imagens.Update(imagen);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "updated");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            OrganizacionPolitica organizacionPolitica = _dbcontext.OrganizacionPoliticas.FirstOrDefault(o => o.Id == id);
            PartHistImg partHistImg = _dbcontext.PartHistImgs.FirstOrDefault(x => x.OrgId == id);
            if (organizacionPolitica == null)
            {
                return NotFound();
            }

            _dbcontext.PartHistImgs.Remove(partHistImg);
            _dbcontext.OrganizacionPoliticas.Remove(organizacionPolitica);
            await _dbcontext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "Deleted");
        }

        [HttpGet]
        [Route("statistic1")]
        public async Task<IActionResult> Statistic1()
        {
            var menctt = _dbcontext.MiembroDescs.Count(m => m.Genero == "M");
            var womctt = _dbcontext.MiembroDescs.Count(m => m.Genero == "F");

            var result = new
            {
                CttMens = menctt,
                CttWomen = womctt
            };

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("statistic2/{province?}")]
        public async Task<IActionResult> Statistic2(string province = null)
        {
            try
            {
                var mencttPerProvince = _dbcontext.MiembroDescs.Where(x => x.Provincia == province).Count(m => m.Genero == "M");
                var womcttPerProvince = _dbcontext.MiembroDescs.Where(x => x.Provincia == province).Count(m => m.Genero == "F");

                var result = new
                {
                    CttMens = mencttPerProvince,
                    CttWom = womcttPerProvince
                };

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet]
        [Route("statistic1forparty/{party?}")]
        public async Task<IActionResult> statistic1ForParty(string party = null)
        {
            var menctt = _dbcontext.MiembrosDeOrganizacionViews.Where(mp => mp.Acronimo == party).Count(mp => mp.Genero == "M");
            var womctt = _dbcontext.MiembrosDeOrganizacionViews.Where(mp => mp.Acronimo == party).Count(mp => mp.Genero == "F");

            var result = new
            {
                cttmen = menctt,
                cttwom = womctt
            };
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut]
        [Route("updatecargo/{id:int}")]
        public async Task<IActionResult> updateCargo(int id, [FromBody] Cargo updatedCargo)
        {
            Cargo cargo = await _dbcontext.Cargos.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Descripcion = updatedCargo.Descripcion;
            _dbcontext.Cargos.Update(cargo);
            await _dbcontext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "updated");
        }

        [HttpPost]
        [Route("postestatuto")]
        public async Task<IActionResult> postEstatuto(EstatutoDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Estatuto1))
            {
                return BadRequest("Invalid input");
            }
            try
            {
                byte[] imageBytes = Convert.FromBase64String(request.Estatuto1);
                Console.WriteLine($"Bytes Length: {imageBytes.Length}");

                Estatuto estatuto = new()
                {
                    Estatuto1 = imageBytes,
                    IdOrganizacion = request.IdOrganizacion,
                    Fecha = request.Fecha
                };

                await _dbcontext.Estatutos.AddAsync(estatuto);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "estatuto created");
            }
            catch
            {
                return BadRequest("Image data is not properly base64 encoded.");
            }
        }

        [HttpPost]
        [Route("postasamblea")]
        public async Task<IActionResult> postasamblea(AsambleaDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Asambleas))
            {
                return BadRequest("invalid asamblea");
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(request.Asambleas);
                Console.WriteLine($"bytes document:  {imageBytes.Length}");

                Asamblea asamblea = new()
                {
                    Asambleas = imageBytes,
                    Fecha = request.Fecha,
                    IdOrganizacion = request.IdOrganizacion
                };

                await _dbcontext.Asambleas.AddAsync(asamblea);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "asamblea created");
            }
            catch
            {
                return BadRequest("Image data not valid");
            }

        }

        [HttpPost]
        [Route("postconvencion")]

        public async Task<IActionResult> postConvencion(ConvencionDTO request)
        {

            if (request == null || string.IsNullOrEmpty(request.Convencion))
            {
                return BadRequest("invalid Convencion");
            }
            byte[] iamgeBytes = Convert.FromBase64String(request.Convencion);
            Console.WriteLine($"bytes document:  {iamgeBytes.Length}");

            Convencione convencion = new()
            {
                Convencion = iamgeBytes,
                Fecha = request.Fecha,
                IdOrganizacion = request.IdOrganizacion
            };

            await _dbcontext.Convenciones.AddAsync(convencion);
            await _dbcontext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "estatuto created");
        }

        //[Authorize]
        [HttpPost]
        [Route("postparthistimg")]
        public async Task<IActionResult> PostPartHistImg(PartHistImgDTO partHistImgDTO) 
        {
            if (partHistImgDTO.HisImg == null || string.IsNullOrEmpty(partHistImgDTO.HisImg))
            {
                return BadRequest("No image to post");
            }

            byte[] imageBytes = Convert.FromBase64String(partHistImgDTO.HisImg);

            PartHistImg partHistImg = new()
            {
                HisImg = imageBytes,
                Fecha = partHistImgDTO.Fecha,
                OrgId = partHistImgDTO.OrgId,
            };

            await _dbcontext.PartHistImgs.AddAsync(partHistImg);
            await _dbcontext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "History Img Inserted");
        }

        [HttpPost]
        [Route("addingimgfororg")]
        public async Task<IActionResult> AddingImgForOrg(PartHistImgDTO partHistImgDTO)
        {
            if (partHistImgDTO.HisImg == null || string.IsNullOrEmpty(partHistImgDTO.HisImg))
            {
                return BadRequest("No image to post");
            }

            byte[] imageBytes = Convert.FromBase64String(partHistImgDTO.HisImg);

            PartHistImg partHistImg = new()
            {
                HisImg = imageBytes,
                Fecha = partHistImgDTO.Fecha,
                OrgId = partHistImgDTO.OrgId,
            };

            await _dbcontext.PartHistImgs.AddAsync(partHistImg);
            await _dbcontext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "History Img Inserted");
        }


        [HttpGet]
        [Route("getparthisimg/{id:int}")]
        public async Task<IActionResult> GetPartHistImg(int id)
        {
            List<PartHistImg> partHistImgs = await _dbcontext.PartHistImgs.Where(x => x.OrgId == id).OrderByDescending(x => x.Fecha).ToListAsync();

            return StatusCode(StatusCodes.Status200OK, partHistImgs);
        }


        [HttpPut]
        [Route("updateorg/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrganizacionPolitica updatedOrganizacionpolitica)
        {
            OrganizacionPolitica organizacionPolitica = await _dbcontext.OrganizacionPoliticas.FindAsync(id);

            if (organizacionPolitica == null)
            {
                return NotFound();
            }

            organizacionPolitica.Tipo = updatedOrganizacionpolitica.Tipo;
            organizacionPolitica.Nombre = updatedOrganizacionpolitica.Nombre;
            organizacionPolitica.Acronimo = updatedOrganizacionpolitica.Acronimo;
            organizacionPolitica.DireccionDeSede = updatedOrganizacionpolitica.DireccionDeSede;
            organizacionPolitica.MunicipioDeSede = updatedOrganizacionpolitica.MunicipioDeSede;
            organizacionPolitica.ProvinciaDeSede = updatedOrganizacionpolitica.ProvinciaDeSede;
            organizacionPolitica.TelefonoDeSede = updatedOrganizacionpolitica.TelefonoDeSede;
            organizacionPolitica.Email = updatedOrganizacionpolitica.Email;
            organizacionPolitica.Logo = updatedOrganizacionpolitica.Logo;
            organizacionPolitica.Website = updatedOrganizacionpolitica.Website;
            organizacionPolitica.AnoReconocimiento = updatedOrganizacionpolitica.AnoReconocimiento;
            organizacionPolitica.NoActa = updatedOrganizacionpolitica.NoActa;
            organizacionPolitica.NoResolucion = updatedOrganizacionpolitica.NoResolucion;

            _dbcontext.OrganizacionPoliticas.Update(organizacionPolitica);

            await _dbcontext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "updated");
        }


        [HttpPut]
        [Route("updatemiembro/{id:int}")]
        public async Task<IActionResult> UpdateMiembro(int id, [FromBody] Updatemember updatedMiembro)
        {
            if (updatedMiembro == null)
            {
                return BadRequest("Datos de miembro no válidos");
            }
            SqlParameter[] parameters =
            {
            new SqlParameter("@IdMiembro", id),
            new SqlParameter("@NombreMiembro", updatedMiembro.Nombremiembro),
            new SqlParameter("@ApellidoMiembro", updatedMiembro.ApellidoMiembro),
            new SqlParameter("@Edad", updatedMiembro.Edad),
            new SqlParameter("@Direccion", updatedMiembro.Direccion),
            new SqlParameter("@Telefono", updatedMiembro.Telefono),
            new SqlParameter("@Provincia", updatedMiembro.Provincia),
            new SqlParameter("@Municipio", updatedMiembro.Municipio),
            new SqlParameter("@OrganizacionId", updatedMiembro.OrganizacionId),
            new SqlParameter("@CargoId", updatedMiembro.Cargo_Id),
            new SqlParameter("@Fecha_designacion", updatedMiembro.Fecha_designacion),
            new SqlParameter("@Genero", updatedMiembro.Genero),
            new SqlParameter("@Tituloadesc", updatedMiembro.Tituloadesc),
            new SqlParameter("@Cedula", updatedMiembro.Cedula),
            new SqlParameter("@Email", updatedMiembro.Email),
            };
            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync
                    ("EXEC ActualizarMiembro @IdMiembro, @NombreMiembro, @ApellidoMiembro, @Edad, @Direccion, @Telefono, @Provincia, @Municipio, @OrganizacionId, @CargoId, @Fecha_designacion, @Genero, @Tituloadesc, @Cedula, @Email", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return StatusCode(StatusCodes.Status200OK, "updated");
        }

        [HttpDelete]
        [Route("deletemember/{id:int}")]

        public async Task<IActionResult> DeleteMember(int id)
        {
            MiembroDesc miembroDesc = await _dbcontext.MiembroDescs.FindAsync(id);

            if (miembroDesc == null)
            {
                return NotFound();
            }

            SqlParameter[] parameters = {
              new SqlParameter("@IdMiembro", id)
            };

            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync("EXEC DeleteMenber @IdMiembro", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return StatusCode(StatusCodes.Status200OK, "Deleted");
        }
    }
}