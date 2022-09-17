import { useRef, useEffect } from 'react';

export const CreateListingButton = (props) => {

    return (
        <button
            id={props.id}
            className={props.class}
        >
            {props.text}
        </button>
    );
}