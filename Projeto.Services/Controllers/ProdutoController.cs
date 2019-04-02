using AutoMapper;
using Projeto.Entidades;
using Projeto.Infra.Data.Contracts;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto.Services.Controllers
{
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        //construtor para injeção de dependencia..
        public ProdutoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost] //requisição POST
        [Route("cadastrar")] //URL: /api/produto/cadastrar
        public HttpResponseMessage Post(ProdutoCadastroModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Produto p = Mapper.Map<Produto>(model);
                    unitOfWork.ProdutoRepository.Insert(p);
                    //requisição bem-sucedida (HTTP 200 - Ok)
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Produto cadastrado com sucesso");
                }
                else
                {
                    //retornando status HTTP 403
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

        [HttpPut] //requisição PUT
        [Route("atualizar")] //URL: /api/produto/atualizar
        public HttpResponseMessage Put(ProdutoEdicaoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Produto p = Mapper.Map<Produto>(model);
                    unitOfWork.ProdutoRepository.Update(p);
                    //requisição bem-sucedida (HTTP 200 - Ok)
                    return Request.CreateResponse(HttpStatusCode.OK,
                    "Produto atualizado com sucesso");
                }
                else
                {
                    //retornando status HTTP 403
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
        [Route("excluir")] //URL: /api/produto/excluir?id={0}
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Produto p = unitOfWork.ProdutoRepository.FindById(id);

                unitOfWork.ProdutoRepository.Delete(p);

                //requisição bem-sucedida (HTTP 200 - Ok)
                return Request.CreateResponse(HttpStatusCode.OK,
                "Produto excluído com sucesso");
            }
            catch (Exception e)
            {
                //requisição inválida (HTTP 400 - BadRequest)
                return Request.CreateResponse
                (HttpStatusCode.BadRequest, e.Message);
            }
        }
        [HttpGet] //requisição GET
        [Route("obtertodos")] //URL: /api/produto/obtertodos
        public HttpResponseMessage GetAll()
        {
            try
            {
                List<ProdutoConsultaModel> lista = new List<ProdutoConsultaModel>();

                foreach (Produto p in unitOfWork.ProdutoRepository.FindAll())
                {
                    lista.Add(Mapper.Map<ProdutoConsultaModel>(p));
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
        [Route("obterporid")] //URL: /api/produto/obterporid?id={0}
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                Produto p = unitOfWork.ProdutoRepository.FindById(id);

                ProdutoConsultaModel model = Mapper.Map<ProdutoConsultaModel>(p);

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
