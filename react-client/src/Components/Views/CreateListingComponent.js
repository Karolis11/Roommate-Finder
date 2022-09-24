import { Component } from 'react';
import axios from 'axios';


export class CreateListingComponent extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount()
    {
        document.getElementById("submitBtn").addEventListener('click', () => {
            let userInput ={
                firstName: document.getElementById("firstName").value,
                lastName: document.getElementById("lastName").value,
                email: document.getElementById("email").value,
                city: document.getElementById("city").value,
                roommateCount: document.getElementById("roommateCount").value,
                maxPrice: document.getElementById("maxPrice").value,
            }

            axios({
                method: 'post',
                url: 'https://localhost:44332/listing',
                data: userInput
            }).then((response) => {
                this.props.toggleCreateListing(false);
            })
        });
    }

    render() {
        return (
            <div className="create-listing-form">
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
                <button id="submitBtn">Submit</button>
            </div>
        )
    }
}