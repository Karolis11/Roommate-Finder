import React from 'react';
import './ButtonStyles.css';


export class LogOutButton extends React.Component {

   constructor(props) {
    super(props);
    this.state = {
      disabled: !localStorage.getItem("token"),
    };

    this.handleClick = this.handleClick.bind(this);
  }

  handleClick(event) {
    event.preventDefault();
    localStorage.setItem("token", "");
    this.setState({ disabled: true });
    window.location.reload(false);
  }

  render() {
    return (
      <button
        className= "log-out-button"
        onClick={this.handleClick}
        disabled={this.state.disabled}
      >
        Log out
      </button>
    );
  }
}
export default LogOutButton;