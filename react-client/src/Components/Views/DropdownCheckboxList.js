import {Component} from 'react';
import {Button, Checkbox} from '@mui/material';

export class DropdownCheckboxList extends Component {
    constructor(props) {
        super(props);
        this.state = {
          isListVisible: false
        };
      }
    
      handleChange(event) {
        this.setState({
          selectedOptions: event.target.value
        });
      }
    
      handleClick() {
        this.props.setParentClass(!this.state.isListVisible);
        this.setState(prevState => ({
          isListVisible: !prevState.isListVisible
        }));
      }
      
      render() {
        return (
            <>
            <Button variant="outlined" onClick={this.handleClick.bind(this)} style={{position: "relative"}}>Cities</Button>
            <div 
                className={`city-checkboxes${this.state.isListVisible ? ' visible': ''}`}
            >
                {this.state.isListVisible && (
                    this.props.options.map(option => (
                        <span key={option}>
                        <Checkbox 
                            checked={this.props.selectedOptions.includes(option)} 
                            value={option}
                            onChange={(e) => {this.props.updateCheckboxList(e.target.value)}}
                            />
                            {option}
                        </span>
                    ))
                )}
            </div></>
        );
      }
}
