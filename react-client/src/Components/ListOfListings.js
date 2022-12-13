import { Component } from 'react';
import { useState } from 'react';
import "./ListStyles.css";
import axios from 'axios';

export class ListOfListings extends Component {

    constructor(props) {
        super(props);

        this.state = {
            userEmail: "",
            userId: -1,
            repliesView: undefined,
            replyInputValue: "",
            replies: undefined,
        }
    }

    handleChange = (e) => {
        this.setState({replyInputValue: e.target.value});
    }

    handleEnter = (e, id) => {
        if (this.state.replyInputValue == "")
            return;

        if (e.key === "Enter") {
            axios({
                url: 'https://localhost:44332/reply',
                method: 'post',
                data: {
                    ListingId: id,
                    UserId: this.props.currentUser.id,
                    Message: this.state.replyInputValue
                }
            }).then((response) => {
                this.getReplies(id);
                this.setState({replyInputValue: ""});
            });
        }
    }

    getReplies = (id) => {
        axios.get('https://localhost:44332/reply', {
            params: {
                id: id
            }
        }).then(response => {
            this.setState({replies: response.data});
        })
    }

    toggleReplies = (id) => {
        this.setState({repliesView: id});
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
                                            onClick={() => {this.props.toggleEditListingView(listing, listing.email == this.props.currentUser.email);}}
                                        >Edit</button>
                                        <button
                                            onClick={() => { this.props.toggleDeleteListingView(listing, listing.email == this.props.currentUser.email);}}
                                        >Delete</button>
                                        <br/>
                                        <button 
                                            onClick={() => {this.toggleReplies(listing.id); this.getReplies(listing.id)}}
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

                                        {
                                            this.state.repliesView == listing.id &&
                                            <>
                                            {
                                                this.state.replies &&
                                                this.state.replies.map((reply) => {
                                                    return (
                                                        <p key={reply.id}><strong>{reply.user.firstName} {reply.user.lastName}</strong>: {reply.message}</p>
                                                    );
                                                })
                                            }
                                            <input
                                                type="text"
                                                onChange={this.handleChange}
                                                onKeyDown={(e) => {this.handleEnter(e, listing.id)}}
                                                value={this.state.replyInputValue}
                                                placeholder="Reply to this listing"
                                            />
                                            </>
                                        }
                                        
                                    </div>
                                    <hr className="line" />
                                </li>
                            )
                    }
            </ul>
        );
    }
}
