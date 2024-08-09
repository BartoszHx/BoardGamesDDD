import React, { useContext, useEffect, useState } from "react";
import { PieceModel } from "../../models/Chess.type";
import KingWhite from "../../assets/pieces/king-white.png";
import BishopWhite from "../../assets/pieces/bishop-white.png";
import KnightWhite from "../../assets/pieces/knight-white.png";
import PawnWhite from "../../assets/pieces/pawn-white.png";
import QueenWhite from "../../assets/pieces/queen-white.png";
import RockWhite from "../../assets/pieces/rock-white.png";
import KingBlack from "../../assets/pieces/king-black.png";
import BishopBlack from "../../assets/pieces/bishop-black.png";
import KnightBlack from "../../assets/pieces/knight-black.png";
import PawnBlack from "../../assets/pieces/pawn-black.png";
import QueenBlack from "../../assets/pieces/queen-black.png";
import RockBlack from "../../assets/pieces/rock-black.png";
import styles from "./Chess.module.css";

type PieceComponentModel = {
    piece?: PieceModel;
}

function PieceComponent(props: PieceComponentModel) {
    let isPiece = props.piece != null;

    return (
        <div className={styles.piece}>
            {isPiece ? SetPiece(props.piece) : null}
        </div>
    );
}

function SetPiece(piece?: PieceModel) {
    var image = SetPieceImage(piece);

    return (
        <img src={image} alt={piece?.piece + " | " + piece?.color} title={piece?.piece} />
    );
}

function SetPieceImage(piece?: PieceModel): any {
    const color = piece?.color;

    switch (piece?.piece) {
        case "King": return color == "White" ? KingWhite : KingBlack;
        case "Bishop": return color == "White" ? BishopWhite : BishopBlack;
        case "Knight": return color == "White" ? KnightWhite : KnightBlack;
        case "Pawn": return color == "White" ? PawnWhite : PawnBlack;
        case "Queen": return color == "White" ? QueenWhite : QueenBlack;
        case "Rock": return color == "White" ? RockWhite : RockBlack;
        default: return undefined;
    }
}

export default PieceComponent;
