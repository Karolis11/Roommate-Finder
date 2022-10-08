import { Component } from 'react';
import { CreateListingComponent } from '../Views/CreateListingComponent';
import { CreateListingButton } from '../Buttons/CreateListingButton';
import { ListOfListings } from '../ListOfListings';
import { FilterComponent } from '../Views/FilterComponent';
import axios from 'axios';

import './Filters.css';


export class LoggedInMain extends Component {

    constructor(props) {
        super(props);

        this.state = {
            listings: undefined,
            createListingView: false,
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

    componentDidMount() {

        this.getListings();
    }

    getListings = () => {
        axios({
            method: 'post',
            url: 'https://localhost:44332/listing/sort',
            data: {sort: 0}
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
                        <div className="listings-container">
                            <>
                                <CreateListingButton
                                    id="create-listing-button"
                                    text="New listing"
                                    class="btn"
                                    onclick={this.toggleCreateListingWrapper.bind(this)}
                                />
                                { this.state.listings && <ListOfListings listings={this.state.listings}/> }
                                
                            </>
                        </div>
                }
            </div>
            </>
        );
    }
}