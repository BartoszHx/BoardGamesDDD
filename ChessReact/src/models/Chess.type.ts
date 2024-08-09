import { Guid } from "guid-typescript";
import { ColorType, FieldPosition, PieceType } from "./Chess.enum";

export type ChessModel = {
    id: Guid;
    board: BoardModel;
    //poolMovements: PoolMovementModel[];
    playerTurn: ColorType
};

export type BoardModel = {
    fields: FieldModel[];
};

export type FieldModel = {
    position: FieldPosition;
    piece?: PieceModel;
    isSelected: boolean;
    isShowMovement: boolean;
}

export type PieceModel = {
    piece: PieceType;
    color: ColorType;
}

export type LoadGameModel = {
    id: Guid;
}