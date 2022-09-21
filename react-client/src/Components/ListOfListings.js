import { Component } from 'react';

export class ListOfListings extends Component {

    constructor(props){
        super(props);
    }

    render()
    {
        return (
            <ul>
                {
                    this.props.listings
                        .map((listing, index) =>
                            <li key={index}>
                                {JSON.stringify(listing)}
                            </li>
                        )
                }
            </ul>
            
        );
    }


}
