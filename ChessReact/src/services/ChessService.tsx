import { Guid } from "guid-typescript";
import axios from "axios";
import { ChessDtoModel, DoMovementDtoModel } from "../models/ChessDto.type";

const instance = axios.create({
    baseURL: "https://localhost:7038/api/chess/",
    headers: {
        "Access-Control-Allow-Origin": "http://localhost:3000",
    }
});

export async function postCreateStandardGame(): Promise<Guid> {
    return await instance
        .post("create-standard-game")
        .then((res) => res.data as Promise<Guid>);
}

export async function getGame(id: Guid): Promise<ChessDtoModel> {
    return await instance
        .get("game?id=" + id.toString())
        .then((res) => res.data as Promise<ChessDtoModel>);
}

export async function postDoMovement(doMovement: DoMovementDtoModel) {
    var request: any = {
        chessId: doMovement.chessId.toString(),
        fromPosition: doMovement.fromPosition,
        toPosition: doMovement.toPosition,
        promotionPieceType: doMovement.promotionPieceType
    };

    await instance.post("do-movement", request);
}