import React from 'react';
import axios from 'axios';

export const DeleteListingComponent = (props) => {

    return (
        <>
            <div className="delete-listing-container">
                <form>
                    <div className="form-field-flex-text-comfirmation">Are you sure you want to delete the listing?</div>
                    <button onClick={ props.deleteListing(props.listing ) }>Delete</button>
                </form>
            </div>
        </>
    )
}
 
