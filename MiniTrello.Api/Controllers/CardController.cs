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
    public class CardController : ApiController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private readonly IMappingEngine _mappingEngine;

        public CardController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [POST("/lanes/{Id}/cards/crearCard/{accessToken}")]
        public ResponseMessageModel CreateCard([FromBody] CrearCardModel model, string accesstoken,long Id)
        {

            var lane = _readOnlyRepository.GetById<Lane>(Id);

            Cards cards = _mappingEngine.Map<CrearCardModel, Cards>(model);
            Cards cardCreated = _writeOnlyRepository.Create(cards);

            if (cardCreated != null)
            {
                lane.AddCard(cardCreated);
                return new ResponseMessageModel { Message = "Se creado exitosamente la carta" };
            }

            return new ResponseMessageModel { Message = "No se ha creado exitosamente la columna" };


        }

        [GET("lanes/{Id}/cards/{accessToken}")]
        public List<CardModel> GetAllUserCards(long Id, string accessToken)
        {
            //validar accessToken

            var lane = _readOnlyRepository.GetById<Lane>(Id);
            var mappedCardModelList = _mappingEngine.Map<IEnumerable<Cards>, IEnumerable<
            CardModel>>(lane.Cards).ToList();
            return mappedCardModelList;

        } 

        [System.Web.Http.AcceptVerbs("PUT")]
        [PUT("card/changecardtitle/{accessToken}")]
        public CardModel ChangeCardTitle([FromBody] ChangeCardTitleModel model, string accessToken)
        {
            //validar seguridad

            var card = _readOnlyRepository.GetById<Cards>(model.CardId);
            card.Information = model.Title;


            var updatedCard = _writeOnlyRepository.Update(card);
            var cardModel = _mappingEngine.Map<Cards,CardModel>(updatedCard);
            return cardModel;

        }

        public CardModel Archive(string accessToken, [FromBody] CardArchiveModel model)
        {
            var card = _readOnlyRepository.GetById<Cards>(model.Id);
            var archivedCard = _writeOnlyRepository.Archive(card);
            return _mappingEngine.Map<Cards, CardModel>(archivedCard);
        }

        [GET("card/{accessToken}/{cardId}")]
        public CardModel GetById(string accessToken, long cardId)
        {
            var card = _readOnlyRepository.GetById<Cards>(cardId);
            return _mappingEngine.Map<Cards, CardModel>(card);
        }


    }

    public class CrearCardModel
    {
      
        public string Information { get; set; }

    }

    public class CardArchiveModel
    {
        public long Id { get; set; }

    }

    public class CardModel
    {
        public long Id { get; set; }
       
        public string Information { get; set; }

    }

    public class ChangeCardTitleModel
    {
        public int CardId { get; set; }
        public string Title { get; set; }
    }


}