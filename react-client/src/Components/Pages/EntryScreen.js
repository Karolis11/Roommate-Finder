import React from 'react';
import { useNavigate } from 'react-router-dom';
import img1 from './Icons/RoommateLogo.png';
import './EntryScreen.css';
import { motion } from "framer-motion";

export const EntryScreen = (props) => {
    const navigate = useNavigate();

    return (
        <motion.div
            className="entryscreen"
            initial={{ x: 300, opacity: 0 }}
            animate={{ x: 0, opacity: 1 }}
            exit={{ x: 300, opacity: 0 }}
            transition={{
                type: "spring",
                stiffness: 260,
                damping: 20,
            }}
        ><div className="login-signup-btn-container">
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
        </div></motion.div>
    );
}