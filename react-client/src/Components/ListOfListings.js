import { Component } from 'react';
import axios from 'axios';

export class ListOfListings extends Component {

    constructor(props){
        super(props);
        this.state = {
            listings: []
        }
    }

    componentDidMount()
    {
        axios({
            method: 'get',
            url: 'https://localhost:44332/listing',
            data: {}
        }).then((response) => {
            var strs = response.data;
            var arrobjs = strs.map(str => JSON.parse(str));
            this.state.listings = arrobjs.map(arrobj => ({ ...arrobj }));
            this.render();
        })
        
    }

    render()
    {
        return (
            <ul>
                {
                    this.state.listings
                        .map((listing, index) =>
                            <li key={index}>
                                {JSON.stringify(listing), console.log(listing) }
                            </li>
                        )
                }
            </ul>
            
        );
    }


}
