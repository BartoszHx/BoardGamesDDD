import { Guid } from "guid-typescript";
import { ColorType, FieldPosition, MovementType, PieceType } from "./Chess.enum";

export type ChessDtoModel = {
    id: Guid;
    board: BoardDtoModel;
    poolMovements: PoolMovementDtoModel[];
    playerTurn: ColorType
};

export type BoardDtoModel = {
    fields: FieldDtoModel[];
};

export type FieldDtoModel = {
    position: FieldPosition;
    piece?: PieceDtoModel;
}

export type PieceDtoModel = {
    piece: PieceType;
    color: ColorType;
}

export type PoolMovementDtoModel = {
    fromPosition: FieldPosition;
    toPosition: FieldPosition;
    movement: MovementType;
}

export type DoMovementDtoModel = {
    chessId: Guid;
    fromPosition: FieldPosition;
    toPosition: FieldPosition;
    promotionPieceType?: PieceDtoModel;
}
