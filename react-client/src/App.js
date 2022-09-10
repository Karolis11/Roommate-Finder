import './App.css';
import { TestButton } from './Components/TestButton';
import { Component } from 'react';
import axios from 'axios';

class App extends Component {
    buttonId = "test-server-button";
    buttonElement;

    constructor(props) {
        super(props)
        this.state = {
            response: undefined
        }
        this.buttonElement = document.createElement('button');
    }

    componentDidMount() {
        this.buttonElement = document.getElementById(this.buttonId);

        this.buttonElement.addEventListener('click', () => {
            axios({
                method: 'get',
                url: '/servertest',
                data: {}
            }).then((response) => {
                this.setState({ response: response });
            })
        })
    }

    render() {
        console.log('test');
        return (
            <>
                <TestButton />
                <p>{this.state.response}</p>
            </>
        );
    } 
}

export default App;
