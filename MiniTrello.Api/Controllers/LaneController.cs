using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class LaneController : ApiController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private readonly IMappingEngine _mappingEngine;

        public LaneController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [POST("/boards/{Id}/lanes/crearLane/{accessToken}")]
        public ResponseMessageModel CreateLane([FromBody] CrearLaneModel model, string accesstoken,long Id)
        {

            var board = _readOnlyRepository.GetById<Board>(Id);

            Lane lanes = _mappingEngine.Map<CrearLaneModel, Lane>(model);
            Lane lanesCreated = _writeOnlyRepository.Create(lanes);

            if (lanesCreated != null)
            {
                board.AddLanes(lanesCreated);
                return new ResponseMessageModel { Message = "Se creado exitosamente la columna" };
            }

            return new ResponseMessageModel { Message = "No se ha creado exitosamente la columna" };


        }

        [GET("boards/{Id}/lanes/{accessToken}")]
        public List<LaneModel> GetAllUserLanes(long Id, string accessToken)
        {
            //validar accessToken

            var board = _readOnlyRepository.GetById<Board>(Id);
            var mappedLaneModelList = _mappingEngine.Map<IEnumerable<Lane>, IEnumerable<
            LaneModel>>(board.Lanes).ToList();
            return mappedLaneModelList;

        } 

        [System.Web.Http.AcceptVerbs("PUT")]
        [PUT("lane/changelanetitle/{accessToken}")]
        public LaneModel ChangeLaneTitle([FromBody] ChangeLaneTitleModel model, string accessToken)
        {
            //validar seguridad

            var lane = _readOnlyRepository.GetById<Lane>(model.LaneId);
            lane.Name = model.Title;


            var updatedLane = _writeOnlyRepository.Update(lane);
            var laneModel = _mappingEngine.Map<Lane, LaneModel>(updatedLane);
            return laneModel;

        }

        public LaneModel Archive(string accessToken, [FromBody] LaneArchiveModel model)
        {
            var lane = _readOnlyRepository.GetById<Lane>(model.Id);
            var archivedLane = _writeOnlyRepository.Archive(lane);
            return _mappingEngine.Map<Lane, LaneModel>(archivedLane);
        }

        [GET("lane/{accessToken}/{laneId}")]
        public LaneModel GetById(string accessToken, long LaneId)
        {
            var lane = _readOnlyRepository.GetById<Lane>(LaneId);
            return _mappingEngine.Map<Lane, LaneModel>(lane);
        }


    }

    public class CrearLaneModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class LaneArchiveModel
    {
        public long Id { get; set; }

    }

    public class LaneModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class ChangeLaneTitleModel
    {
        public int LaneId { get; set; }
        public string Title { get; set; }
    }


}