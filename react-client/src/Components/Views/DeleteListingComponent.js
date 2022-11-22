import React from 'react';
import axios from 'axios';

export const DeleteListingComponent = () => {
    return (
        <>
            <div className="delete-listing-container">
                <form>
                    <div className="form-field-flex-text-comfirmation">Are you sure you want to delete the listing?</div>
                    <button type="submit">Delete</button>
                </form>
            </div>
        </>
    )
}
 
