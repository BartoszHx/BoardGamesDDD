import React from "react";
import styles from "./NewGame.module.css";

function NewGameComponent() {

    const halfDivLeft = styles.half_div + ' ' + styles.text_right;
    const halfDivRight = styles.half_div + ' ' + styles.text_left;

    return (
        <div className={styles.new_game}>
            <div className={styles.full_div}>
                <h1>New Game</h1>
            </div>
            <div className={styles.full_div }>
                <div className={halfDivLeft}><p>Game type:</p></div>
                <div className={halfDivRight}><p>Standard</p></div>
            </div>
            <div className={styles.full_div}>
                <div className={halfDivLeft}><p>Time:</p></div>
                <div className={halfDivRight}><p>None</p></div>
            </div>
            <div className={styles.full_div}>
                <div className={halfDivLeft}><p>Match type:</p></div>
                <div className={halfDivRight}><p>Local</p></div>
            </div>
            <div className={styles.full_div}>
                <input type="submit" value="Create" />
            </div>
        </div>
    );
}

export default NewGameComponent;