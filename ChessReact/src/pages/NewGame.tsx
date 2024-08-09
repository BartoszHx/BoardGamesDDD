import { Guid } from "guid-typescript";
import React from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import NewGameComponent from "../components/NewGame/NewGameComponent";
import { postCreateStandardGame } from "../services/ChessService";

function NewGame() {
    const { handleSubmit } = useForm();
    const navigate = useNavigate();

    const onSubmit = () => {
        const createData = async () => {
            const gameId = await postCreateStandardGame();
            navigate("/chess/" + gameId);
        };

        createData();
    };

    return (
        <form onSubmit={handleSubmit(onSubmit) }>
            <div>
                <NewGameComponent/>
            </div>
        </form>
    );
}

export default NewGame;