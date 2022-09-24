import { Component } from 'react';
import { CreateListingComponent } from './CreateListingComponent';
import { CreateListingButton } from '../Buttons/CreateListingButton';
import { ListOfListings } from '../ListOfListings';
import axios from 'axios';

export class LoggedInMain extends Component {
    constructor(props) {
        super(props);

        this.state = {
            listings: undefined,
            createListingView: false,
        }

        this.createListingButton = document.createElement('button');

        this.toggleCreateListing = this.toggleCreateListing.bind(this);
    }

    toggleCreateListing = (toggleBool) => {
        this.setState({ createListingView: toggleBool });
        this.getListings();
    }

    componentDidMount() {
        this.createListingButton = document.getElementById(this.createListingButtonId);

        this.createListingButton.addEventListener('click', () => {
            this.toggleCreateListing(true);
        })

        this.getListings();
    }

    getListings = () => {
        axios({
            method: 'get',
            url: 'https://localhost:44332/listing',
            data: {}
        }).then((response) => {
            this.setState({listings: response.data})
        })
    }

    render() {
        return (
            <>
            {
                !this.state.createListingView
                ?
                    <div className="listings-container">
                        <>
                            <CreateListingButton
                                id="create-listing-button"
                                text="New listing"
                                class="btn"
                            />
                            {
                                this.state.listings
                                ?
                                    <ListOfListings listings={this.state.listings}/>
                                :
                                    null
                            }
                            
                        </>
                    </div>
                :
                    <CreateListingComponent toggleCreateListing={this.toggleCreateListing}/>
            }
            </>
        );
    }
}