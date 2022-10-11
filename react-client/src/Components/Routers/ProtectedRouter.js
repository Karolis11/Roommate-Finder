import { useAuth } from 'react'
import {
    Routes,
    Route,
    NavLink,
    Navigate,
    useNavigate,
} from 'react-router-dom';

export const ProtectedRoute = (props, { children }) => {


    if (!props.loggedIn) {
        return <Navigate to="/login" replace />;
    }

    return children;
};