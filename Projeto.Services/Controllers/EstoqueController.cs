using Projeto.Entidades;
using Projeto.Infra.Data.Contracts;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;

namespace Projeto.Services.Controllers
{
    [RoutePrefix("api/estoque")]
    public class EstoqueController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public EstoqueController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpPost] //requisição POST
        [Route("cadastrar")] //URL: /api/estoque/cadastrar
        public HttpResponseMessage Post(EstoqueCadastroModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Estoque e = Mapper.Map<Estoque>(model);

                    e.Nome = model.Nome;

                    unitOfWork.EstoqueRepository.Insert(e);

                    //requisição bem-sucedida (HTTP 200 - Ok)
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Estoque cadastrado com sucesso");
                }
                else
                {
                    //Erro HTTP 403 (Requisição proibida)
                    return Request.CreateResponse (HttpStatusCode.Forbidden, ModelState);
                }
            }
            catch (Exception e)
            {
                //requisição inválida (HTTP 400 - BadRequest)
                return Request.CreateResponse
                (HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut] //requisição PUT
        [Route("atualizar")] //URL: /api/estoque/atualizar
        public HttpResponseMessage Put(EstoqueEdicaoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Estoque e = Mapper.Map<Estoque>(model);
                    e.IdEstoque = model.IdEstoque;
                    e.Nome = model.Nome;

                    unitOfWork.EstoqueRepository.Update(e);

                    //requisição bem-sucedida (HTTP 200 - Ok)
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Estoque atualizado com sucesso");
                }
                else
                {
                    return Request.CreateResponse
                (HttpStatusCode.Forbidden, ModelState);
                }
            }
            catch (Exception e)
            {
                //requisição inválida (HTTP 400 - BadRequest)
                return Request.CreateResponse
                (HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete] //requisição DELETE
        [Route("excluir")] //URL: /api/estoque/excluir?id={0}
        public HttpResponseMessage Delete(int id)
        {
            try
            {

                Estoque e = unitOfWork.EstoqueRepository.FindById(id);
                unitOfWork.EstoqueRepository.Delete(e);

                //TODO..
                //requisição bem-sucedida (HTTP 200 - Ok)
                return Request.CreateResponse(HttpStatusCode.OK,
                "Estoque excluído com sucesso");
                

            }
            catch (Exception e)
            {
                //requisição inválida (HTTP 400 - BadRequest)
                return Request.CreateResponse
                (HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet] //requisição GET
        [Route("obtertodos")] //URL: /api/estoque/obtertodos
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<EstoqueConsultaModel> lista = new List<EstoqueConsultaModel>();

                foreach (Estoque e in unitOfWork.EstoqueRepository.FindAll())
                {

                    lista.Add(Mapper.Map<EstoqueConsultaModel>(e));
                }
                //requisição bem-sucedida (HTTP 200 - Ok)
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch (Exception e)
            {
                //requisição inválida (HTTP 400 - BadRequest)
                return Request.CreateResponse
                (HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet] //requisição GET
        [Route("obterporid")] //URL: /api/estoque/obterporid?id={0}
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                //buscando 1 estoque pelo id..
                Estoque e = unitOfWork.EstoqueRepository.FindById(id);

                EstoqueConsultaModel model = Mapper.Map<EstoqueConsultaModel>(e);

                //requisição bem-sucedida (HTTP 200 - Ok)
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch (Exception e)
            {
                //requisição inválida (HTTP 400 - BadRequest)
                return Request.CreateResponse
                (HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
