import React, { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ChessModel, BoardModel, FieldModel, PieceModel } from "../models/Chess.type";
import { getGame, postDoMovement } from "../services/ChessService";
import { Guid } from "guid-typescript";
import ChessComponent from "../components/ChessGame/ChessComponent";
import { FieldPosition } from "../models/Chess.enum";
import { BoardDtoModel, ChessDtoModel, DoMovementDtoModel, FieldDtoModel, PieceDtoModel } from "../models/ChessDto.type";

function ChessGame() {
    const { id } = useParams();
    const [chessResposne, setChessResposne] = useState<ChessDtoModel>();
    const [chess, setChess] = useState<ChessModel>();
    const [selectedField, setSelectedField] = useState<FieldPosition>();

    const fetchData = async () => {
        const idGuid = Guid.parse(id || "");
        const response = await getGame(idGuid);
        const model = chessDtoToChess(response);

        setChessResposne(response);
        setChess(model);
    };

    const chessDtoToChess = (dto: ChessDtoModel) : ChessModel => {
        const model: ChessModel = {
            id: dto.id,
            playerTurn: dto.playerTurn,
            board: boardDtoToBoard(dto.board),
        };

        return model;
    }

    const boardDtoToBoard = (dto: BoardDtoModel): BoardModel => {
        const model: BoardModel = {
            fields: dto.fields.map((value) => fieldDtoToField(value))
        };

        return model;
    }

    const fieldDtoToField = (dto: FieldDtoModel): FieldModel => {
        const model: FieldModel = {
            position: dto.position,
            piece: pieceDtoToPiece(dto.piece),
            isSelected: false,
            isShowMovement: false
        };

        return model;
    }

    const pieceDtoToPiece = (dto: PieceDtoModel | undefined): PieceModel | undefined => {
        if (dto == null) {
            return undefined;
        }

        const model: PieceModel = {
            color: dto.color,
            piece: dto.piece
        };

        return model;
    }

    const MoveClick = (fieldPosition: FieldPosition) => {
        const piece = chessResposne?.board.fields.filter(x => x.position == fieldPosition)[0].piece;

        const isFirstSelection = selectedField == null;
        const isPlayerPiece = piece != null ? chessResposne?.playerTurn == piece.color : false;
        const isDoMove = !isFirstSelection && chessResposne?.poolMovements.find(x => x.fromPosition == selectedField && x.toPosition == fieldPosition);

        if (isFirstSelection && isPlayerPiece) {
            SelectedPiece(fieldPosition);
            return;
        }

        if (isDoMove) {
            DoMovement(fieldPosition);
            return;
        }

        if (!isFirstSelection && isPlayerPiece) {
            ClearFields();
            SelectedPiece(fieldPosition);

            return;
        }

        ClearFields();
    }

    const SelectedPiece = (fieldPosition: FieldPosition) => {
        setSelectedField(fieldPosition);
        const toMove = chessResposne?.poolMovements.filter(x => x.fromPosition == fieldPosition).map(x => x.toPosition);

        chess?.board.fields.map(m => {
            if (m.position == fieldPosition) {
                m.isSelected = true;
            }

            if (toMove?.some(x => x == m.position)) {
                m.isShowMovement = true;
            }
        });
    }

    const ClearFields = () =>
    {
        setSelectedField(undefined);
        chess?.board.fields.map(m => {
            m.isSelected = false;
            m.isShowMovement = false;
        });
    }

    const DoMovement = async (fieldPosition: FieldPosition) => {
        const idGuid = Guid.parse(id || "");

        const request: DoMovementDtoModel = {
            chessId: idGuid,
            fromPosition: selectedField || FieldPosition.Empty,
            toPosition: fieldPosition,
            promotionPieceType: undefined
        };

        await postDoMovement(request);
        await fetchData();
    }

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <div>
            {chess != null ? <ChessComponent chess={chess as ChessModel} moveClick={MoveClick} /> : <p>Empty</p>}
        </div>
    );
}

export default ChessGame;
