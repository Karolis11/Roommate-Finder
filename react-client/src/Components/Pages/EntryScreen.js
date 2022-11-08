import React from 'react';
import { useNavigate } from 'react-router-dom';
import img1 from './Icons/RoommateLogo.png';
import './EntryScreen.css';

export const EntryScreen = (props) => {
    const navigate = useNavigate();

    return (
        <div className="login-signup-btn-container">
            <img className="entry-logo" src={img1}></img>
            <h2>Roommate Finder</h2>
            <button className="entry-btn"
                id="loginButton"
                onClick={() => { navigate(`/login`); }}
            >
                Login
            </button>
            <button className="entry-btn"
                id="signupButton"
                onClick={() => { navigate(`/signup`); }}
            >
                Signup
            </button>
        </div>
    );
}