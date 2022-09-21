import './App.css';
import { CreateListingView } from './Components/Views/CreateListingView';
import { CreateListingButton } from './Components/Buttons/CreateListingButton';
import { ListOfListings } from './Components/ListOfListings';
import { Component } from 'react';
import axios from 'axios';

class App extends Component {
    createListingButtonId = "create-listing-button";
    createListingButton;

    constructor(props) {
        super(props)
        this.state = {
            listings: undefined,
            createListingView: false,
        }

        this.createListingButton = document.createElement('button');

        this.toggleCreateListing = this.toggleCreateListing.bind(this);
    }

    toggleCreateListing = (toggleBool) => {
        this.setState({ createListingView: toggleBool });
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
            var strs = response.data;
            console.log(strs);
            var arrobjs = strs.map(str => {
                if (str === "") {
                    return null;
                }
                return JSON.parse(str, true)
            });
            this.setState({listings: arrobjs})
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
                        <CreateListingView toggleCreateListing={this.toggleCreateListing}/>
                }
                
            </>
        );
    } 
}

export default App;
