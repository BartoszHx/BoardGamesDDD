import { type } from "@testing-library/user-event/dist/type";
import { Guid } from "guid-typescript";
import React from "react";
import { FieldErrors, UseFormRegister } from "react-hook-form";
import { LoadGameModel } from "../../models/Chess.type";
import styles from "./LoadGame.module.css";

type LoadGameComponentModel = {
    register: UseFormRegister<LoadGameModel>
    errors: FieldErrors<LoadGameModel>
}

function LoadGameComponent(props: LoadGameComponentModel) {
    return (
        <div className={styles.load_game}>
            <div className={styles.full_div}>
                <h1>Load Game</h1>
            </div>
            <div className={styles.full_div}>
                <div className={styles.label_div}><p>Game ID:</p></div>
                <div className={styles.input_div}>
                    <input {...props.register("id", { required: true, validate: (value) => Guid.isGuid(value) })} />
                </div>
                <div className={styles.error_div}>
                    {props.errors.id?.type == "required" && <p>Is required</p>}
                    {props.errors.id?.type == "validate" && <p>Not Guid type</p>}
                </div>
            </div>
            <div className={styles.full_div}>
                <input type="submit" value="Load" />
            </div>
        </div>
    );
}

export default LoadGameComponent;