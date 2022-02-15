using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clientes.Querys.GetAllClientes
{
    public class GetAllClientesQuery : IRequest<PagedResponse<List<ClienteDto>>>
    {

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }




        public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, PagedResponse<List<ClienteDto>>>
        {

            private readonly IRepositoryAsync<Cliente> _repositoryAsync;

            private readonly IDistributedCache _distributedCache;

            private readonly IMapper _mapper;



            public GetAllClientesQueryHandler(IRepositoryAsync<Cliente> repositoryAsync, IMapper mapper, IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;

            }





            public async Task<PagedResponse<List<ClienteDto>>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
            {
                //Se arma una llave o key con los parametros de la petición
                var cacheKey = $"listadoClientes_{request.PageSize}_{request.PageNumber}_{request.Nombre}_{request.Apellidos}";
                string serializedEstadoClientes;
                var listadoClientes = new List<Cliente>();
                var redisListadoClientes = await _distributedCache.GetAsync(cacheKey);//Se busca si la llave ya esta en el cache

                //Si ya estaba
                if(redisListadoClientes != null)
                {

                    //Se códifica de bites a string JSON
                    serializedEstadoClientes = Encoding.UTF8.GetString(redisListadoClientes);

                    //Se deserializa para obtener la Lista de Clientes guardada en el cache
                    listadoClientes = JsonConvert.DeserializeObject<List<Cliente>>(serializedEstadoClientes);

                }
                else
                {
                    //Si no estaba guardad la key en el cache aun

                    //Extraemos de bd con las especificaciones necesarias
                    listadoClientes = await _repositoryAsync.ListAsync(new PagedClientesSpecifications(request.PageSize, request.PageNumber, request.Nombre, request.Apellidos));

                    //Convertimos a JSON
                    serializedEstadoClientes = JsonConvert.SerializeObject(listadoClientes);

                    //Convertimos a bites para pasarlo a Redis
                    redisListadoClientes = Encoding.UTF8.GetBytes(serializedEstadoClientes);

                    //Configuramos los tiempos que durará la cache
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))//Tiempo de vencimiento del objeto 
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));//Si no se solicita despues de 2 minutos se elimina

                    //Guardamos en cache
                    await _distributedCache.SetAsync(cacheKey,redisListadoClientes,options);


                }



                //Se mapean a una Lista de Dtos de tipo Cliente

                var clienteDto = _mapper.Map<List<ClienteDto>>(listadoClientes);

                                                          //Se añade la lista de Clientes a la Respuesta
                                                                    //Se indica el numero de pagina que se solicito
                                                                                         //Indica el numero de registros por pagina
                return new PagedResponse<List<ClienteDto>>(clienteDto,request.PageNumber,request.PageSize);


            }
        } 
    }
}
