import React, { useContext, useEffect, useState } from "react";
import { ColorType } from "../../models/Chess.enum";
import styles from "./Chess.module.css";

type PlayerTurnComponentModel = {
    playerTurn: ColorType;
}

function PlayerTurnComponent(props: PlayerTurnComponentModel) {
    const isWhitePlayer = props.playerTurn == "White";
    const divStyle = isWhitePlayer ? styles.player_white : styles.player_black;

    return (
        <div className={styles.player +' '+ divStyle}>
            <p>{props.playerTurn}</p>
        </div>
    );
}

export default PlayerTurnComponent;
