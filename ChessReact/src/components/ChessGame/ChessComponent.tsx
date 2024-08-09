import React, { useContext, useEffect, useState } from "react";
import { ChessModel } from "../../models/Chess.type";
import { Guid } from "guid-typescript";
import PlayerTurnComponent from "./PlayerTurnComponent";
import BoardComponent from "./BoardComponent";
import styles from "./Chess.module.css";
import { FieldPosition } from "../../models/Chess.enum";

type ChessComponentModel = {
    chess: ChessModel;
    moveClick: (fieldPosition: FieldPosition) => void;
}

function ChessComponent(props: ChessComponentModel) {
    const id = props.chess.id.toString();

    return (
        <div className={styles.chess}>
            <p>{id}</p>
            <PlayerTurnComponent playerTurn={props.chess.playerTurn} />
            <BoardComponent board={props.chess.board} moveClick={props.moveClick} />
        </div>
    );
}

export default ChessComponent;
