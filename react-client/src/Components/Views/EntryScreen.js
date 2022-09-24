import React from 'react';

export const EntryScreen = (props) => {
    return (
        <div className="login-signup-btn-container">
            <button 
                id="loginButton"
            >
                Login
            </button>
            <button 
                id="signupButton"
                onClick={() => { props.toggleSignUp(true); }}
            >
                Signup
            </button>
        </div>
    );
}