import { eventWrapper } from '@testing-library/user-event/dist/utils';
import { Component } from 'react';
import { useState } from 'react';


export class CreateListingView extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount()
    {
        let userInput ={
            firstName: document.getElementById("firstName").value,
            lastName: document.getElementById("lastName").value,
            email: document.getElementById("email").value,
            city: document.getElementById("city").value,
            roommateCount: document.getElementById("roommateCount").value,
            maxPrice: document.getElementById("maxPrice").value,
        }
    }

    render() {
        return (
            <div className="create-listing-form">
                {
                    <div className="userInput">
                    <label>First Name: </label>
                    <input id="firstName" text="text"></input><br></br>
                    <label>Last name: </label>
                    <input id="lastName" text="text"></input><br></br>
                    <label>City: </label>
                    <input id="city" text="text"></input><br></br>
                    <label>Number of roommates: </label>
                    <input id="roommateCount" text="number"></input><br></br>
                    <label>E-mail: </label>
                    <input id="email" text="text"></input><br></br>
                    <label>Maximum price: </label>
                    <input id="maxPrice" text="number"></input><br></br>
                    </div>
                }
            </div>
        )
    }
}