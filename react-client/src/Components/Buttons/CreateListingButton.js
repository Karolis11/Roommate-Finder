import { useRef, useEffect } from 'react';

export const CreateListingButton = (props) => {

    return (
        <button
            id={props.id}
            className={props.class}
            onClick={props.onclick}
        >
            {props.text}
        </button>
    );
}