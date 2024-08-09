using AutoMapper;
using App = ChessGame.Application.Models;
using Dom = ChessGame.Domain.Dtos;

namespace ChessGame.Application.Profiles
{
    public class ChessDtoProfile : Profile
    {
        public ChessDtoProfile()
        {
            CreateMap<Dom.PieceDto, App.PieceDto>();
            CreateMap<Dom.FieldDto, App.FieldDto>();
            CreateMap<Dom.PoolMovementDto, App.PoolMovementDto>();
            CreateMap<Dom.BoardDto, App.BoardDto>();
            CreateMap<Dom.ChessDto, App.ChessDto>();
        }
    }
}
