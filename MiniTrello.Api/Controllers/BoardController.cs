using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class BoardController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public BoardController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }
        [POST("/organizations/{Id}/boards/crearBoard/{accessToken}")]
        public ResponseMessageModel CreateBoard([FromBody] CrearBoardModel model,long Id, string accessToken)
        {

            //validation accessToken
             var organization = _readOnlyRepository.GetById<Organization>(Id);

             Board board = _mappingEngine.Map<CrearBoardModel, Board>(model);
            Board boardCreated = _writeOnlyRepository.Create(board);

            if (boardCreated != null)
            {
                organization.AddBoard(boardCreated);
                return new ResponseMessageModel { Message = "Se ha creado el board Exitosamente" };
            }

            return new ResponseMessageModel { Message = "No se ha creado el board" };
        }
        [PUT("boards/addmember/{accessToken}")]
        public BoardModel AddMember([FromBody] AddMemberBoardModel model, string accessToken)
        {
            //validar seguridad
            
            var memberToAdd = _readOnlyRepository.GetById<Account>(model.MemberId);
            var board = _readOnlyRepository.GetById<Board>(model.BoardId);
            
            board.AddMember(memberToAdd);
            var updatedBoard = _writeOnlyRepository.Update(board);
            var boardModel = _mappingEngine.Map<Board, BoardModel>(updatedBoard);
            return boardModel;
        }



        [GET("organizations/{Id}/boards/{accessToken}")]
        public List<BoardModel> GetAllUserBoards(long Id,string accessToken)
        {
            //validar accessToken

           var organization = _readOnlyRepository.GetById<Organization>(Id);
            var mappedBoardModelList = _mappingEngine.Map<IEnumerable<Board>, IEnumerable<
            BoardModel>>(organization.Boards).ToList();
            return mappedBoardModelList;

        } 
    }

    public class AddMemberBoardModel
    {
        public long MemberId { get; set; }
        public long BoardId { get; set; }
    }

    public class BoardModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CrearBoardModel
    {
        
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
