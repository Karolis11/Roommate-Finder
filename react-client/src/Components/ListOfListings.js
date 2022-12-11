import { Component } from 'react';
import { useState } from 'react';
import "./ListStyles.css";
import axios from 'axios';

export class ListOfListings extends Component {

    constructor(props) {
        super(props);
    }

    state = {
        userEmail: ""
    }

    getUser = () => {
        axios.get('https://localhost:44332/user/token', {
            params: {
                token: localStorage.getItem("token"),
            }
        })
        .then((response) => {
            this.state.userEmail = response.data.email
        })
    }

    render() {
        return (
            <ul className="lineSeparated">
                <hr className="line" />
                    {
                        this.props.listings
                            .map((listing, index) =>
                                <li className="mainListing" key={index}>

                                    <div className="listingInfo">
                                        <div className="listingCity">
                                            {listing.city}
                                        </div>
                                        <div className="listingDate">
                                            {listing.date}
                                        </div>
                                        <button
                                            onClick={() => {this.getUser(); this.props.toggleEditListingView(listing, listing.email == this.state.userEmail);}}
                                        >Edit</button>
                                        <button
                                            onClick={() => {this.getUser(); this.props.toggleDeleteListingView(listing, listing.email == this.state.userEmail);}}
                                        >Delete</button>
                                        <button 
                                            onClick={() => {this.toggleConversation()}}
                                        >Replies</button>
                                    </div>

                                    <div className="listingReview ">
                                        <div className="listingName" >{listing.firstName} {listing.lastName} </div>

                                        <div className="listingTexts">
                                            <div className="listingTextReview">
                                                {listing.extraComment}
                                            </div>
                                            <div className="listingMoreDetails">
                                                Prefered number of roommates: {listing.roommateCount}
                                            </div>
                                            <div className="listingContacts">
                                                {listing.phone}, {listing.email}
                                            </div>
                                        </div>

                                        <div className="listingPrice">
                                            <span>
                                                {listing.maxPrice} &#8364;
                                            </span>
                                        </div>
                                    </div>
                                    <hr className="line" />
                                </li>
                            )
                    }
            </ul>
        );
    }
}
