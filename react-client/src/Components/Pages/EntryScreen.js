import React from 'react';
import { useNavigate } from 'react-router-dom';

export const EntryScreen = (props) => {
    let navigate = useNavigate();

    return (
        <div className="login-signup-btn-container">
            <button
                id="loginButton"
                onClick={() => { navigate(`/login`); }}
            >
                Login
            </button>
            <button
                id="signupButton"
                onClick={() => { navigate(`/signup`); }}
            >
                Signup
            </button>
        </div>
    );
}