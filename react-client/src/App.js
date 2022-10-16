import './App.css';
import { EntryScreen } from './Components/Pages/EntryScreen';
import { Signup } from './Components/Pages/Signup';
import { Component } from 'react';
import { LoggedInMain } from './Components/Pages/LoggedInMain';
import { Login } from './Components/Pages/Login';
import {
    Routes,
    Route,
    Outlet,
    Link,
    NavLink,
    useParams,
    useNavigate,
    useSearchParams,
} from 'react-router-dom';
import { ProtectedRoute } from './Components/Routers/ProtectedRoute';

class App extends Component {
    createListingButtonId = "create-listing-button";
    createListingButton;

    constructor(props) {
        super(props)
        this.state = {
            loggedIn: false
        }
    }
    
    toggleLoggedIn = (toggleBool) => {
        this.setState({loggedIn: toggleBool})
    }

    render() {
        return (
            <Routes>
                <Route index element={<EntryScreen />} />
                <Route path="/signup" element={<Signup />} />
                <Route path="/login" element={<Login />} />
                <Route path="/dashboard" element={<ProtectedRoute><LoggedInMain /></ProtectedRoute>} />
            </Routes>
        );
    }
}

export default App;
