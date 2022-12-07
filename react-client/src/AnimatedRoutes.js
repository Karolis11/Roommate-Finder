import './App.css';
import { EntryScreen } from './Components/Pages/EntryScreen';
import { Signup } from './Components/Pages/Signup';
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
    useLocation,
} from 'react-router-dom';
import { ProtectedRoute } from './Components/Routers/ProtectedRoute';
import { AnimatePresence } from 'framer-motion/dist/framer-motion';

function AnimatedRoutes (){
    const location = useLocation();

    return (
        <AnimatePresence>
            <Routes location={location} key={location.pathname}>
                <Route index element={<EntryScreen />} />
                <Route path="/signup" element={<Signup />} />
                <Route path="/login" element={<Login />} />
                <Route path="/dashboard" element={<ProtectedRoute><LoggedInMain /></ProtectedRoute>} />
            </Routes>
        </AnimatePresence>
    );
}

