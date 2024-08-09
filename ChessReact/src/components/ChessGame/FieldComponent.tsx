import React, { useContext, useEffect, useState } from "react";
import { FieldModel } from "../../models/Chess.type";
import PieceComponent from "./PieceComponent";
import styles from "./Chess.module.css";
import { FieldPosition } from "../../models/Chess.enum";

type FieldComponentModel = {
    field: FieldModel;
    moveClick: (fieldPosition: FieldPosition) => void;
}

function FieldComponent(props: FieldComponentModel) {

    //var position = Object.values(FieldPosition).indexOf(props.field.position.toString());

    const moveClick = () => props.moveClick(props.field.position);

    var position = props.field.position.toString();
    const darkStylePairOne: string[] = ['A', 'C', 'E', 'G', '1', '3', '5', '7'];
    const darkStylePairTwo: string[] = ['B', 'D', 'F', 'H', '2', '4', '6', '8'];

    var colorStyle = (darkStylePairOne.includes(position[0]) && darkStylePairOne.includes(position[1]))
        || (darkStylePairTwo.includes(position[0]) && darkStylePairTwo.includes(position[1]))
        ? styles.field_color_dark : styles.field_color_light;

    var selectedStyle = props.field.isSelected ? styles.field_selected : "";

    var moveStyle = props.field.isShowMovement ? styles.field_circle : styles.field_circle_hide;
    var enemyPieceMoveStyle = props.field.piece != null ? styles.field_circle_red : "";

    var divStyle = styles.field + ' ' + colorStyle + ' ' + selectedStyle;
    var spanStyle = moveStyle + ' ' + enemyPieceMoveStyle;

    return (
        <div className={divStyle} onClick={moveClick}>
            <p>{props.field.position}</p>
            <span className={spanStyle} />
            <PieceComponent piece={props.field.piece} />
        </div>
    );
}

export default FieldComponent;
