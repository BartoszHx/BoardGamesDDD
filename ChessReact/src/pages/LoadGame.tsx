import React from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { LoadGameModel } from "../models/Chess.type";
import LoadGameComponent from "../components/LoadGame/LoadGameComponent";

function LoadGame() {
    const { register, handleSubmit, formState: {errors} } = useForm<LoadGameModel>();
    const navigate = useNavigate();

    const onSubmit: SubmitHandler<LoadGameModel> = (data) => {
        navigate("/chess/" + data.id.toString());
    };

    return (
        <form onSubmit={handleSubmit(onSubmit) }>
            <div>
                <LoadGameComponent register={register} errors= {errors } />
            </div>
        </form>
    );
}

export default LoadGame;