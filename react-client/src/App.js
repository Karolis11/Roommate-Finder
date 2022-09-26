import './App.css';
import { EntryScreen } from './Components/Views/EntryScreen';
import { Signup } from './Components/Views/Signup';
import { Component } from 'react';
import { LoggedInMain } from './Components/Views/LoggedInMain';
import { Login } from './Components/Views/Login';


class App extends Component {
    createListingButtonId = "create-listing-button";
    createListingButton;

    constructor(props) {
        super(props)
        this.state = {
            signupScreen: false,
            loggedIn: false,
            loginScreen: false
        }
    }

    toggleSignUp = (toggleBool) => {
        this.setState({signupScreen: toggleBool});
    }

    toggleLogin = (toggleBool) => {
        this.setState({loginScreen: toggleBool});
    }

    toggleLoggedIn = (toggleBool) => {
        this.setState({loggedIn: toggleBool})
    }

    render() {
        return (
            <>
            {
                this.state.loggedIn
                ?
                    <LoggedInMain />
                :
                    this.state.signupScreen
                    ?
                        <Signup toggleSignUp={this.toggleSignUp.bind(this)}/>
                    :   
                        this.state.loginScreen
                        ?
                        <Login toggleLogin={this.toggleLogin.bind(this)}
                        Login toggleLoggedIn={this.toggleLoggedIn.bind(this)}/>
                        :
                        <EntryScreen toggleLogin={this.toggleLogin.bind(this)}
                        EntryScreen toggleSignUp={this.toggleSignUp.bind(this)} />

            }
                
            </>
        );
    } 

    
}

export default App;
