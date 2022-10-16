import { useAuth } from 'react'
import {
    Routes,
    Route,
    NavLink,
    Navigate,
    useNavigate,
} from 'react-router-dom';

export const ProtectedRoute = ({ children }) => {
    const token = localStorage.getItem('token');
    if (!token) {
        return <Navigate to="/login" replace />;
    }

    return children;
};

export default ProtectedRoute;