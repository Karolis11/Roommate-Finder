import { useFormik } from 'formik';
import { useState } from 'react';
import { Select, MenuItem, Slider } from '@mui/material';
import axios from 'axios';
import {DropdownCheckboxList} from './DropdownCheckboxList';
import { LithuanianCities } from './LithuanianCities';
import CustomRoommates from './CustomRoommates'
import CitySelect from './CitySelect';
import '../Pages/Filters.css';

function useForceUpdate(){
    const [value, setValue] = useState(0);
    return () => setValue(value => value + 1);
}

export const FilterComponent = (props) => {

    const [rangeValues, setRangeValues] = useState([0, 500]);
    const [citySelect, setCitySelect] = useState("Vilnius");
    const [roommateCounts, setRoommateCount] = useState("1");
    const forceUpdate = useForceUpdate();


    const getListings = (values, city) => {

        axios({
            method: 'get',
            url: `https://localhost:44332/listing/sort?sort=${values.sort}&city=${encodeURIComponent(city)}`,
        })
        .then((response) => {
            props.updateListings(response.data);             
        })
    }

    const getFilteredListings = (event) => {

        axios({
            method: 'get',
            url: `https://localhost:44332/listing/filter`,
            params: {
                lowPrice: rangeValues[0],
                highPrice: rangeValues[1],
                city: citySelect,
                count: roommateCounts
                }
        })
            .then((response) => {
                console.log(response.data);
                props.updateListings(response.data);
            })
    }

    const formik = useFormik({
        initialValues: {
            sort: 0,
            priceRange: [100, 500],
        },
        onSubmit: (values) => {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition((position) => {
                    axios({
                        method: "get",
                        url: `https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=${position.coords.latitude}&longitude=${position.coords.longitude}&localityLanguage=default`
                    })
                    .then((response) => {
                        getListings(values, response.data.city);
                    })
                });
            } else {
                getListings(values, null);
            } 
        }
    });
    return (
        
        <div className='filter-top-container'>
            <label htmlFor="sort">Sort By</label>
            <Select
                name="sort"
                onChange={(e) => {formik.handleChange(e); formik.submitForm(formik.values);}}
                value={formik.values.sort}
            >
                <MenuItem value={0}>Maximum price</MenuItem>
                <MenuItem value={1}>Number of roommates</MenuItem>
                <MenuItem value={2}>City</MenuItem>
            </Select>
            <div className="filter-text">Price range:</div>
            <Slider
                getAriaLabel={() => 'Price range'}
                value={rangeValues}
                min={0}
                max={5000}
                name="range"
                onChange={(e) => { setRangeValues(e.target.value) }}
                valueLabelDisplay="auto"
                style={{width: "200px"}}
            />
            <div className="filter-text">   City:  <label></label>
                <select
                    type="text"
                    value={citySelect}
                    onChange={(event) => setCitySelect(event.target.value)}>
                    {
                        LithuanianCities.map(opt => <option>{opt}</option>)
                    }
                </select>
            </div>
            <div className="filter-text">Roommate Count:
                <select
                    type="text"
                    value={roommateCounts}
                    onChange={(event) => setRoommateCount(+event.target.value)}>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                </select>
            </div>
                <div1 className="filter-button"
                    onClick={getFilteredListings}
                > FILTER</div1>
            </div>
    );
}