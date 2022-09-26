import { Component } from 'react';

export class ListOfListings extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <ul class="lineSeparated">
                <hr class="line"> </hr>
                    {
                        this.props.listings
                            .map((listing, index) =>
                                <li class="mainListing" key={index}>

                                    <div class="listingInfo">
                                        <div class="listingCity">
                                            listing.city
                                        </div>
                                        <div class="listingDate">
                                            listing.date
                                        </div>
                                    </div>

                                    <div class="listingReview ">
                                        <div class="listingName" >listing.firstName listing.lastName </div>

                                        <div class="listingTexts">
                                            <div class="listingTextReview">
                                                listing.extraComment
                                            </div>
                                            <div class="listingMoreDetails">
                                                listing.phone, listing.email
                                            </div>
                                        </div>

                                        <div class="listingPrice">
                                            <span>
                                                listing.maxPrice €
                                            </span>
                                        </div>
                                    </div>
                                    <hr class="line"> </hr>
                                </li>
                            )
                    }

            </ul>

        );
    }


}
