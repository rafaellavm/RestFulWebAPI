using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    //RoutePrefix: Determina a rota de acesso para o controller, exemplo: http://localhost:porta/api/usuario/metodo.
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private static List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

        //AcceptVerbs: Determina o tipo do método http que o método do Controller vai responder.
        //Route: Determina o nome para acesso ao método, no nosso exemplo coloquei o mesmo nome do método do Controller, mas você pode definir qualquer outro nome para acesso.
        [AcceptVerbs("POST")]
        [Route("CadastrarUsuario")]
        public HttpResponseMessage CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                if(usuario.Codigo != 0)
                {
                    listaUsuarios.Add(usuario);
                    return Request.CreateResponse(HttpStatusCode.OK, listaUsuarios.ToArray());
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "O código do usuário não pode ser 0 nem vazio");
                }
               
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [AcceptVerbs("PUT")]
        [Route("AlterarUsuario")]
        public string AlterarUsuario(UsuarioModel usuario)
        {
            listaUsuarios.Where(n => n.Codigo == usuario.Codigo)
                         .Select(s =>
                         {
                             s.Codigo = usuario.Codigo;
                             s.Login = usuario.Login;
                             s.Nome = usuario.Nome;

                             return s;

                         }).ToList();

            return "Usuário alterado com sucesso!";
        }

        [AcceptVerbs("DELETE")]
        [Route("ExcluirUsuario/{codigo}")]
        public string ExcluirUsuario(int codigo)
        {
            UsuarioModel usuario = listaUsuarios.Where(n => n.Codigo == codigo)
                                                .Select(n => n)
                                                .First();

            listaUsuarios.Remove(usuario);

            return "Registro excluido com sucesso!";
        }

        [AcceptVerbs("GET")]
        [Route("ConsultarUsuarios")]
        public List<UsuarioModel> ConsultarUsuarios()
        {
            return listaUsuarios;
        }
    }


}
