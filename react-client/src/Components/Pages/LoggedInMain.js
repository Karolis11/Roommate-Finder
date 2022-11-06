import { Component } from 'react';
import { CreateListingComponent } from '../Views/CreateListingComponent';
import { EditListingComponent } from '../Views/EditListingComponent';
import { CreateListingButton } from '../Buttons/CreateListingButton';
import { ListOfListings } from '../ListOfListings';
import { FilterComponent } from '../Views/FilterComponent';
import axios from 'axios';
import Pusher from 'pusher-js';

import './Filters.css';


export class LoggedInMain extends Component {

    constructor(props) {
        super(props);

        this.state = {
            city: null,
            listings: undefined,
            createListingView: false,
            editListingView: false,
            editedListing: null,
        }

        this.toggleCreateListing = this.toggleCreateListing.bind(this);
    }

    updateListings = (listings) => {
        this.setState({listings: listings});
    }

    toggleCreateListing = (toggleBool) => {
        this.setState({ createListingView: toggleBool });
        this.getListings();
    }

    toggleCreateListingWrapper = () => {
        this.toggleCreateListing(true);
    }

    toggleEditListingView = (listing, toggleBool) => {
        this.setState({
            editedListing: listing,
            editListingView: toggleBool
        })
    }

    componentDidMount() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition((position) => {
                axios({
                    method: "get",
                    url: `https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=${position.coords.latitude}&longitude=${position.coords.longitude}&localityLanguage=default`
                })
                .then((response) => {
                    this.setState({city: response.data.city});
                    this.getListings(response.data.city);
                })
            });
        } else {
            this.getListings(null);
        }

        const pusher = new Pusher('ae5e63689c26a34fbdbf', {
            cluster: 'mt1'
        });
        const channel = pusher.subscribe('listing_feed');
        const that = this;
        channel.bind('feed_updated', function(data) {
            that.getListings(that.state.city);
        });
    }

    getListings = (city) => {
        axios({
            method: 'get',
            url: `https://localhost:44332/listing/sort?sort=0&city=${encodeURIComponent(city)}`
        }).then((response) => {
            this.setState({listings: response.data})
        })
    }

    render() {
        return (
            <>
            <FilterComponent updateListings={this.updateListings.bind(this)}/>
            <div className={`logged-in-main-container ${this.state.createListingView && ' create-listing-on'}`}>
                {
                    this.state.createListingView
                    ?
                        <CreateListingComponent toggleCreateListing={this.toggleCreateListing}/>
                    :
                        this.state.editListingView
                        ?
                            <EditListingComponent 
                                listing={this.state.editedListing} 
                                toggleEditListing={this.toggleEditListingView.bind(this)}
                            />
                        :
                            <div className="listings-container">
                                <>
                                    <CreateListingButton
                                        id="create-listing-button"
                                        text="New listing"
                                        class="btn"
                                        onclick={this.toggleCreateListingWrapper.bind(this)}
                                    />
                                    { 
                                        this.state.listings && 
                                        <ListOfListings 
                                            listings={this.state.listings}
                                            toggleEditListingView={this.toggleEditListingView.bind(this)}
                                            /> 
                                    }
                                    
                                </>
                            </div>
                }
            </div>
            </>
        );
    }
}