import React, { useContext, useEffect, useState } from "react";
import { BoardModel, FieldModel } from "../../models/Chess.type";
import FieldComponent from "./FieldComponent";
import styles from "./Chess.module.css";
import { FieldPosition } from "../../models/Chess.enum";


type BoardComponentModel = {
    board: BoardModel;
    moveClick: (fieldPosition: FieldPosition) => void;
}

function BoardComponent(props: BoardComponentModel) {
    var field8 = FiltrField(props, "8");
    var field7 = FiltrField(props, "7");
    var field6 = FiltrField(props, "6");
    var field5 = FiltrField(props, "5");
    var field4 = FiltrField(props, "4");
    var field3 = FiltrField(props, "3");
    var field2 = FiltrField(props, "2");
    var field1 = FiltrField(props, "1");

    return (
        <div className={styles.board}>
            {MapToDiv(field8, props.moveClick )}
            {MapToDiv(field7, props.moveClick )}
            {MapToDiv(field6, props.moveClick )}
            {MapToDiv(field5, props.moveClick )}
            {MapToDiv(field4, props.moveClick )}
            {MapToDiv(field3, props.moveClick )}
            {MapToDiv(field2, props.moveClick )}
            {MapToDiv(field1, props.moveClick )}
        </div>
    );
}

function FiltrField(props: BoardComponentModel, match: string) {
    return props.board.fields.filter(f => f.position.toString().match(match)).sort();
}

function MapToDiv(fields: FieldModel[], moveClick: (fieldPosition: FieldPosition) => void) {
    return (
        <div className={styles.board_tr}>
            {fields.map((value) => <div className={styles.board_td}>
                <FieldComponent field={value} key={value.position} moveClick={moveClick} />
            </div>)}
        </div>
    );
}

export default BoardComponent;
